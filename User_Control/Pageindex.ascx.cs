using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_Control_Pageindex : System.Web.UI.UserControl
{
    public delegate void onPageIndexChange(int pageIndex);
    public event onPageIndexChange PageIndexChange;
    int currentPageIndex = 1;
    public void SetPage(int pageIndex, int pageSize)
    {
        currentPageIndex = pageIndex;
        DataTable tbl = new DataTable();
        tbl.Columns.Add("p_text");
        tbl.Columns.Add("p_value");
        int pageStart = pageIndex - (pageIndex % 20);

        for (int i = pageStart; (i < pageSize && i < pageStart + 20); i++)
        {
            DataRow r = tbl.NewRow();
            r["p_text"] = i + 1;
            r["p_value"] = i;
            tbl.Rows.Add(r);
        }
        if (pageIndex > 19)
        {
            DataRow r = tbl.NewRow();
            r["p_text"] = "...";
            r["p_value"] = pageStart - 1;
            tbl.Rows.InsertAt(r, 0);
        }
        if (pageStart + 20 < pageSize)
        {
            DataRow r = tbl.NewRow();
            r["p_text"] = "...";
            r["p_value"] = pageStart + 20;
            tbl.Rows.Add(r);
        }
        repPageIndex.DataSource = tbl;
        repPageIndex.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void repPageIndex_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "pi")
        {
            PageIndexChange(Convert.ToInt32(e.CommandArgument));
        }
    }
    protected void repPageIndex_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView r = (DataRowView)e.Item.DataItem;
            LinkButton lnk = (LinkButton)e.Item.FindControl("lnkPageIndex");
            lnk.Text = Convert.ToString(r["p_text"]);
            lnk.CommandArgument = Convert.ToString(r["p_value"]);
            if (Convert.ToInt32(r["p_value"]) == currentPageIndex) lnk.Enabled = false;
        }
    }
}