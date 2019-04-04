<!-- default file list -->
*Files to look at*:

* [Default.aspx](./CS/WebSite/Default.aspx) (VB: [Default.aspx](./VB/WebSite/Default.aspx))
* [Default.aspx.cs](./CS/WebSite/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/WebSite/Default.aspx.vb))
<!-- default file list end -->
# ASPxGridView - How to implement optimistic concurrency for the update/delete operations


<p>This approach is based on adding a field to the underlying data source that will contain information about the current row's version. All you need is to store this field's value and then assign it to e.OldValues["row_version_field_name"] in the ASPxGridView.RowUpdating/ASPxGridView.RowDeleting events. </p><p>Then the data source will automatically get old values for an edited/deleted record and compare it with actual data.</p><p><a href="http://www.asp.net/web-forms/tutorials/data-access/accessing-the-database-directly-from-an-aspnet-page/implementing-optimistic-concurrency-with-the-sqldatasource-vb"><u>Implementing Optimistic Concurrency with the SqlDataSource</u></a><u><br />
</u><a href="https://www.devexpress.com/Support/Center/p/E3213">How to implement optimistic concurrency for the update operation</a></p>

<br/>


