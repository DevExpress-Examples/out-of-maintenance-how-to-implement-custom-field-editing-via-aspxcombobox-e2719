Imports System
Imports System.Web.UI
Imports DevExpress.XtraScheduler
Imports DevExpress.Web
Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.Web.ASPxScheduler.Internal
Imports System.Collections
Imports System.Collections.Generic
Imports DevExpress.XtraScheduler.Localization
Imports System.Data

Partial Public Class AppointmentForm
    Inherits SchedulerFormControl

    Public Overrides ReadOnly Property ClassName() As String
        Get
            Return "ASPxAppointmentForm"
        End Get
    End Property

    Public ReadOnly Property CanShowReminders() As Boolean
        Get
            Return CType(Parent, AppointmentFormTemplateContainer).Control.Storage.EnableReminders
        End Get
    End Property
    Public ReadOnly Property ResourceSharing() As Boolean
        Get
            Return CType(Parent, AppointmentFormTemplateContainer).Control.Storage.ResourceSharing
        End Get
    End Property
    Public ReadOnly Property ResourceDataSource() As IEnumerable
        Get
            Return CType(Parent, AppointmentFormTemplateContainer).ResourceDataSource
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        'PrepareChildControls();
        tbSubject.Focus()
    End Sub
    Public Overrides Sub DataBind()
        MyBase.DataBind()

        Dim container As AppointmentFormTemplateContainer = CType(Parent, AppointmentFormTemplateContainer)
        Dim apt As Appointment = container.Appointment
        edtLabel.SelectedIndex = container.Control.Storage.Appointments.Labels.FindIndex(Function(lb) CInt((lb.Id)) = CInt((apt.LabelKey)))
        edtStatus.SelectedIndex = container.Control.Storage.Appointments.Statuses.FindIndex(Function(st) CInt((st.Id)) = CInt((apt.StatusKey)))

        PopulateResourceEditors(apt, container)

        AppointmentRecurrenceForm1.Visible = container.ShouldShowRecurrence

        If container.Appointment.HasReminder Then
            cbReminder.Value = container.Appointment.Reminder.TimeBeforeStart.ToString()
            chkReminder.Checked = True
        Else
            cbReminder.ClientEnabled = False
        End If

        btnOk.ClientSideEvents.Click = container.SaveHandler
        btnCancel.ClientSideEvents.Click = container.CancelHandler
        btnDelete.ClientSideEvents.Click = container.DeleteHandler

        'btnDelete.Enabled = !container.IsNewAppointment;
    End Sub
    Private Sub PopulateResourceEditors(ByVal apt As Appointment, ByVal container As AppointmentFormTemplateContainer)
        If ResourceSharing Then
            Dim edtMultiResource As ASPxListBox = TryCast(ddResource.FindControl("edtMultiResource"), ASPxListBox)
            If edtMultiResource Is Nothing Then
                Return
            End If
            SetListBoxSelectedValues(edtMultiResource, apt.ResourceIds)
            Dim multiResourceString As List(Of String) = GetListBoxSeletedItemsText(edtMultiResource)
            Dim stringResourceNone As String = SchedulerLocalizer.GetString(SchedulerStringId.Caption_ResourceNone)
            ddResource.Value = stringResourceNone
            If multiResourceString.Count > 0 Then
                ddResource.Value = String.Join(", ", multiResourceString.ToArray())
            End If
            ddResource.JSProperties.Add("cp_Caption_ResourceNone", stringResourceNone)
        Else
            If Not Object.Equals(apt.ResourceId, ResourceEmpty.Id) Then
                edtResource.Value = apt.ResourceId.ToString()
            Else
                edtResource.Value = SchedulerIdHelper.EmptyResourceId
            End If
        End If
    End Sub
    Private Function GetListBoxSeletedItemsText(ByVal listBox As ASPxListBox) As List(Of String)
        Dim result As New List(Of String)()
        For Each editItem As ListEditItem In listBox.Items
            If editItem.Selected Then
                result.Add(editItem.Text)
            End If
        Next editItem
        Return result
    End Function
    Private Sub SetListBoxSelectedValues(ByVal listBox As ASPxListBox, ByVal values As IEnumerable)
        listBox.Value = Nothing
        For Each value As Object In values
            Dim item As ListEditItem = listBox.Items.FindByValue(value.ToString())
            If item IsNot Nothing Then
                item.Selected = True
            End If
        Next value
    End Sub
    Protected Overrides Sub PrepareChildControls()
        Dim container As AppointmentFormTemplateContainer = CType(Parent, AppointmentFormTemplateContainer)
        Dim control As ASPxScheduler = container.Control

        AppointmentRecurrenceForm1.EditorsInfo = New EditorsInfo(control, control.Styles.FormEditors, control.Images.FormEditors, control.Styles.Buttons)
        MyBase.PrepareChildControls()
    End Sub
    Protected Overrides Function GetChildEditors() As ASPxEditBase()
        Dim edits() As ASPxEditBase = { lblSubject, tbSubject, lblLocation, tbLocation, lblLabel, edtLabel, lblStartDate, edtStartDate, lblEndDate, edtEndDate, lblStatus, edtStatus, lblAllDay, chkAllDay, lblResource, edtResource, tbDescription, cbReminder, ddResource, chkReminder, GetMultiResourceEditor() }

        Return edits
    End Function
    Private Function GetMultiResourceEditor() As ASPxEditBase
        If ddResource IsNot Nothing Then
            Return TryCast(ddResource.FindControl("edtMultiResource"), ASPxEditBase)
        End If
        Return Nothing
    End Function
    Protected Overrides Function GetChildButtons() As ASPxButton()
        Dim buttons() As ASPxButton = { btnOk, btnCancel, btnDelete }
        Return buttons
    End Function

    Protected Sub edtField1_Init(ByVal sender As Object, ByVal e As EventArgs)
        Dim dataTable As DataTable = PrepareComboData()

        edtField1.DataSource = dataTable
        edtField1.SelectedIndex = 0

        For i As Integer = 0 To dataTable.Rows.Count - 1
            Dim nextValue As Object = dataTable.Rows(i)(edtField1.ValueField)

            If nextValue IsNot DBNull.Value AndAlso Convert.ToDecimal(nextValue) = CType(Me.Parent, CustomAppointmentFormTemplateContainer).Field1 Then
                edtField1.SelectedIndex = i
                Exit For
            End If
        Next i

        edtField1.DataBind()
    End Sub

    Protected Function PrepareComboData() As DataTable
        Dim dataTable As New DataTable("dataTable")

        dataTable.Columns.Add("Price", GetType(Decimal))
        dataTable.Columns.Add("Description", GetType(String))

        dataTable.Rows.Add(New Object() { DBNull.Value, "[Select Price]" })
        dataTable.Rows.Add(New Object() { 3, "Low" })
        dataTable.Rows.Add(New Object() { 5, "Medium" })
        dataTable.Rows.Add(New Object() { 10, "Hi" })

        Return dataTable
    End Function
End Class

