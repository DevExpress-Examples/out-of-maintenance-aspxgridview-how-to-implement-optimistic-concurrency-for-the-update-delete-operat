using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page {
    const string errorMessage = "This record is in use. Please refresh the page to obtain current data";
    protected void Page_Load(object sender, EventArgs e) {
        grid.DataSource = dsCategories;
        grid.DataBind();
    }
    protected Dictionary<object, string> RowHashes {
        get {
            if (Session[SessionKey] == null)
                Session[SessionKey] = new Dictionary<object, string>();
            return (Dictionary<object, string>)Session[SessionKey];
        }
    }
    protected string SessionKey {
        get {
            if (!oldValuesStorage.Contains("PageSessionKey"))
                oldValuesStorage["PageSessionKey"] = Guid.NewGuid().ToString();
            return oldValuesStorage["PageSessionKey"].ToString();
        }
    }
    protected void ASPxGridView1_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e) {
        e.OldValues["Version"] = RowHashes[e.Keys[0]];
        e.NewValues["Version"] = int.Parse(e.OldValues["Version"].ToString()) + 1;
    }
    protected void ASPxGridView1_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e) {
        e.Values["Version"] = RowHashes[e.Keys[0]] + 1;
    }
    protected void ASPxGridView1_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e) {
        e.NewValues["Version"] = 0;
    }
    protected void ASPxGridView1_RowUpdated(object sender, DevExpress.Web.Data.ASPxDataUpdatedEventArgs e) {
        if (e.AffectedRecords == 0)
            throw new Exception(errorMessage);
    }
    protected void ASPxGridView1_RowDeleted(object sender, DevExpress.Web.Data.ASPxDataDeletedEventArgs e) {
        if (e.AffectedRecords == 0)
            throw new Exception(errorMessage);
    }
    protected void grid_CustomJSProperties(object sender, DevExpress.Web.ASPxGridViewClientJSPropertiesEventArgs e) {
        for (int i = 0; i < grid.VisibleRowCount; i++)
            RowHashes[grid.GetRowValues(i, grid.KeyFieldName)] = grid.GetRowValues(i, "Version").ToString();
    }
}