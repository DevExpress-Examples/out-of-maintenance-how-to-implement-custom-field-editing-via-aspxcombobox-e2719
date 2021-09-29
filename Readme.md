<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128547157/15.2.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E2719)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# How to implement custom field editing via ASPxComboBox


<p>This example demonstrates how to use <strong>ASPxComboBox </strong>in case of a custom field editing on a custom appointment form. This control (ID="edtField1") is defined in the ~/CustomSchedulerForms/CustomAppointmentForm.ascx markup. The actual databinding (with a lookup logic) is handled in its <strong>Init </strong>event. The selected value is transfered back to the controller in the <strong>CustomAppointmentSaveCallbackCommand.AssignControllerValues</strong> method.</p><p>This example also illustrates how you can implement and use the <strong>IsAppointmentChanged </strong>method within your custom <strong>AppointmentFormSaveCallbackCommand </strong>descendant. The <strong>AppointmentFormSaveCallbackCommand.CanContinueExecute</strong> method is overridden so that the end-user should change the appointment's <i>Price </i>field to save an appointment. Otherwise any changes are not saved.</p><p>Prior to running this example, create a <strong>CarsXtraScheduling </strong>database on your local SQL Server instance. The database script is attached to the project (<i>CarsXtraScheduling.sql</i> file). You should modify the script to include the correct database path.</p><p><strong>See also:</strong><br />
<a href="https://docs.devexpress.com/AspNet/5464/components/scheduler/examples/customization/custom-form-and-custom-fields/how-to-customize-the-appointment-editing-form-for-working-with-custom-fields">How to modify the appointment editing form for working with custom fields (step-by-step guide)</a></p>

<br/>


