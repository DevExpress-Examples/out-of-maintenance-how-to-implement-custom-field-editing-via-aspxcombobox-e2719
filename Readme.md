# How to implement custom field editing via ASPxComboBox


<p>This example demonstrates how to use <strong>ASPxComboBox </strong>in case of a custom field editing on a custom appointment form. This control (ID="edtField1") is defined in the ~/CustomSchedulerForms/CustomAppointmentForm.ascx markup. The actual databinding (with a lookup logic) is handled in its <strong>Init </strong>event. The selected value is transfered back to the controller in the <strong>CustomAppointmentSaveCallbackCommand.AssignControllerValues</strong> method.</p><p>This example also illustrates how you can implement and use the <strong>IsAppointmentChanged </strong>method within your custom <strong>AppointmentFormSaveCallbackCommand </strong>descendant. The <strong>AppointmentFormSaveCallbackCommand.CanContinueExecute</strong> method is overridden so that the end-user should change the appointment's <i>Price </i>field to save an appointment. Otherwise any changes are not saved.</p><p>Prior to running this example, create a <strong>CarsXtraScheduling </strong>database on your local SQL Server instance. The database script is attached to the project (<i>CarsXtraScheduling.sql</i> file). You should modify the script to include the correct database path.</p><p><strong>See also:</strong><br />
<a href="https://www.devexpress.com/Support/Center/p/K18145">How to modify the appointment editing form for working with custom fields (step-by-step guide)</a></p>

<br/>


