using System;
using System.Collections.Generic;
using System.Web;
using DevExpress.Web.ASPxScheduler;
using DevExpress.Web.ASPxScheduler.Internal;
using DevExpress.XtraScheduler;
using DevExpress.Web.ASPxEditors;

public class CustomAppointmentFormTemplateContainer : AppointmentFormTemplateContainer {
    public CustomAppointmentFormTemplateContainer(ASPxScheduler control)
        : base(control) {
    }

    public decimal Field1 {
        get {
            object val = Appointment.CustomFields["Field1"];
            return (val == DBNull.Value) ? 0 : Convert.ToDecimal(val);
        }
    }
}


public class CustomAppointmentFormController : AppointmentFormController {
    public CustomAppointmentFormController(ASPxScheduler control, Appointment apt)
        : base(control, apt) {
    }

    public decimal Field1 {
        get { return (decimal)EditedAppointmentCopy.CustomFields["Field1"]; }
        set { EditedAppointmentCopy.CustomFields["Field1"] = value; } 
    }

    decimal SourceField1 {
        get { return (decimal)SourceAppointment.CustomFields["Field1"]; }
        set { SourceAppointment.CustomFields["Field1"] = value; }
    }

    protected override void ApplyCustomFieldsValues() {
        SourceField1 = Field1;
    }

    public override bool IsAppointmentChanged() {
        //if (base.IsAppointmentChanged)
        //    return true;

        decimal v1 = -1;
        decimal v2 = -1;

        try { v1 = Field1; }
        catch (Exception) { }

        try { v2 = SourceField1; }
        catch (Exception) { }

        return !v1.Equals(v2);
    }
}



public class CustomAppointmentSaveCallbackCommand : AppointmentFormSaveCallbackCommand {
    public CustomAppointmentSaveCallbackCommand(ASPxScheduler control)
        : base(control) {
    }
    protected internal new CustomAppointmentFormController Controller {
        get { return (CustomAppointmentFormController)base.Controller; }
    }

    protected override void AssignControllerValues() {
        ASPxComboBox edtField1 = (ASPxComboBox)FindControlByID("edtField1");

        Controller.Field1 = Convert.ToDecimal(edtField1.Value);

        base.AssignControllerValues();
    }

    protected override AppointmentFormController CreateAppointmentFormController(Appointment apt) {
        return new CustomAppointmentFormController(Control, apt);
    }

    protected override bool CanContinueExecute()
    {
        return Controller.IsAppointmentChanged();
    }
}