Imports System
Imports System.Collections.Generic
Imports System.Web
Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.Web.ASPxScheduler.Internal
Imports DevExpress.XtraScheduler
Imports DevExpress.Web

Public Class CustomAppointmentFormTemplateContainer
    Inherits AppointmentFormTemplateContainer

    Public Sub New(ByVal control As ASPxScheduler)
        MyBase.New(control)
    End Sub

    Public ReadOnly Property Field1() As Decimal
        Get
            Dim val As Object = Appointment.CustomFields("Field1")
            Return If(val Is DBNull.Value, 0, Convert.ToDecimal(val))
        End Get
    End Property
End Class


Public Class CustomAppointmentFormController
    Inherits AppointmentFormController

    Public Sub New(ByVal control As ASPxScheduler, ByVal apt As Appointment)
        MyBase.New(control, apt)
    End Sub

    Public Property Field1() As Decimal
        Get
            Return CDec(EditedAppointmentCopy.CustomFields("Field1"))
        End Get
        Set(ByVal value As Decimal)
            EditedAppointmentCopy.CustomFields("Field1") = value
        End Set
    End Property

    Private Property SourceField1() As Decimal
        Get
            Return CDec(SourceAppointment.CustomFields("Field1"))
        End Get
        Set(ByVal value As Decimal)
            SourceAppointment.CustomFields("Field1") = value
        End Set
    End Property

    Protected Overrides Sub ApplyCustomFieldsValues()
        SourceField1 = Field1
    End Sub

    Public Overrides Function IsAppointmentChanged() As Boolean
        'if (base.IsAppointmentChanged)
        '    return true;

        Dim v1 As Decimal = -1
        Dim v2 As Decimal = -1

        Try
            v1 = Field1
        Catch e1 As Exception
        End Try

        Try
            v2 = SourceField1
        Catch e2 As Exception
        End Try

        Return Not v1.Equals(v2)
    End Function
End Class



Public Class CustomAppointmentSaveCallbackCommand
    Inherits AppointmentFormSaveCallbackCommand

    Public Sub New(ByVal control As ASPxScheduler)
        MyBase.New(control)
    End Sub
    Protected Friend Shadows ReadOnly Property Controller() As CustomAppointmentFormController
        Get
            Return CType(MyBase.Controller, CustomAppointmentFormController)
        End Get
    End Property

    Protected Overrides Sub AssignControllerValues()
        Dim edtField1 As ASPxComboBox = CType(FindControlByID("edtField1"), ASPxComboBox)

        Controller.Field1 = Convert.ToDecimal(edtField1.Value)

        MyBase.AssignControllerValues()
    End Sub

    Protected Overrides Function CreateAppointmentFormController(ByVal apt As Appointment) As AppointmentFormController
        Return New CustomAppointmentFormController(Control, apt)
    End Function

    Protected Overrides Function CanContinueExecute() As Boolean
        Return Controller.IsAppointmentChanged()
    End Function
End Class