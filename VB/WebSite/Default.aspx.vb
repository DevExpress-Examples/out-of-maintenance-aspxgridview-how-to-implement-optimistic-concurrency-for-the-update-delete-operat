Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Partial Public Class _Default
	Inherits System.Web.UI.Page
	Private Const errorMessage As String = "This record is in use. Please refresh the page to obtain current data"
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		grid.DataSource = dsCategories
		grid.DataBind()
	End Sub
	Protected ReadOnly Property RowHashes() As Dictionary(Of Object, String)
		Get
			If Session(SessionKey) Is Nothing Then
				Session(SessionKey) = New Dictionary(Of Object, String)()
			End If
			Return CType(Session(SessionKey), Dictionary(Of Object, String))
		End Get
	End Property
	Protected ReadOnly Property SessionKey() As String
		Get
			If (Not oldValuesStorage.Contains("PageSessionKey")) Then
				oldValuesStorage("PageSessionKey") = Guid.NewGuid().ToString()
			End If
			Return oldValuesStorage("PageSessionKey").ToString()
		End Get
	End Property
	Protected Sub ASPxGridView1_RowUpdating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs)
		e.OldValues("Version") = RowHashes(e.Keys(0))
		e.NewValues("Version") = Integer.Parse(e.OldValues("Version").ToString()) + 1
	End Sub
	Protected Sub ASPxGridView1_RowDeleting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataDeletingEventArgs)
		e.Values("Version") = RowHashes(e.Keys(0)) + 1
	End Sub
	Protected Sub ASPxGridView1_RowInserting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInsertingEventArgs)
		e.NewValues("Version") = 0
	End Sub
	Protected Sub ASPxGridView1_RowUpdated(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataUpdatedEventArgs)
		If e.AffectedRecords = 0 Then
			Throw New Exception(errorMessage)
		End If
	End Sub
	Protected Sub ASPxGridView1_RowDeleted(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataDeletedEventArgs)
		If e.AffectedRecords = 0 Then
			Throw New Exception(errorMessage)
		End If
	End Sub
	Protected Sub grid_CustomJSProperties(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewClientJSPropertiesEventArgs)
		For i As Integer = 0 To grid.VisibleRowCount - 1
			RowHashes(grid.GetRowValues(i, grid.KeyFieldName)) = grid.GetRowValues(i, "Version").ToString()
		Next i
	End Sub
End Class