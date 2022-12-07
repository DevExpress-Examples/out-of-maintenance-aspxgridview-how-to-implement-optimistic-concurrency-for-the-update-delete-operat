<%@ Page Language="vb" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>ASPxGridView - How to implement optimistic concurrency for the update/delete operations</title>
</head>
<body>
	<form id="form1" runat="server">
		<div>
			<dx:ASPxGridView ID="grid" runat="server" KeyFieldName="CategoryID"
				OnRowUpdated="ASPxGridView1_RowUpdated"
				OnRowUpdating="ASPxGridView1_RowUpdating"
				OnRowDeleting="ASPxGridView1_RowDeleting"
				OnRowInserting="ASPxGridView1_RowInserting"
				OnRowDeleted="ASPxGridView1_RowDeleted"
				OnCustomJSProperties="grid_CustomJSProperties"
				AutoGenerateColumns="False">
				<Columns>
					<dx:GridViewCommandColumn ShowNewButtonInHeader="true" ShowEditButton="true" ShowDeleteButton="true"></dx:GridViewCommandColumn>
					<dx:GridViewDataColumn FieldName="CategoryID"></dx:GridViewDataColumn>
					<dx:GridViewDataTextColumn FieldName="CategoryName"></dx:GridViewDataTextColumn>
					<dx:GridViewDataTextColumn FieldName="Description"></dx:GridViewDataTextColumn>
					<dx:GridViewDataTextColumn FieldName="Version" Visible="false"></dx:GridViewDataTextColumn>
				</Columns>
				<SettingsPager PageSize="4"></SettingsPager>
			</dx:ASPxGridView>
			<dx:ASPxHiddenField ID="oldValuesStorage" runat="server" />
			<asp:AccessDataSource runat="server" ID="dsCategories" SelectCommand="SELECT [CategoryID], [CategoryName], [Description], [Version] FROM [Categories]" DataFile="~/App_Data/nwind2.mdb"
				ConflictDetection="CompareAllValues"
				DeleteCommand="DELETE FROM [Categories] WHERE [CategoryID] = ? AND (([Version] = ?) OR ([Version] IS NULL AND ? IS NULL))"
				InsertCommand="INSERT INTO [Categories] ([CategoryID], [CategoryName], [Description], [Version]) VALUES (?, ?, ?, ?)"
				OldValuesParameterFormatString="original_{0}"
				UpdateCommand="UPDATE [Categories] SET [CategoryName] = ?, [Description] = ?, [Version] = ? WHERE [CategoryID] = ? AND (([Version] = ?) OR ([Version] IS NULL AND ? IS NULL))">
				<DeleteParameters>
					<asp:Parameter Name="original_CategoryID" Type="Int32" />
					<asp:Parameter Name="original_Version" Type="Int32" />
					<asp:Parameter Name="original_Version" Type="Int32" />
				</DeleteParameters>
				<InsertParameters>
					<asp:Parameter Name="CategoryID" Type="Int32" />
					<asp:Parameter Name="CategoryName" Type="String" />
					<asp:Parameter Name="Description" Type="String" />
					<asp:Parameter Name="Version" Type="Int32" />
				</InsertParameters>
				<UpdateParameters>
					<asp:Parameter Name="CategoryName" Type="String" />
					<asp:Parameter Name="Description" Type="String" />
					<asp:Parameter Name="Version" Type="Int32" />
					<asp:Parameter Name="original_CategoryID" Type="Int32" />
					<asp:Parameter Name="original_Version" Type="Int32" />
					<asp:Parameter Name="original_Version" Type="Int32" />
				</UpdateParameters>
			</asp:AccessDataSource>
		</div>
	</form>
</body>
</html>