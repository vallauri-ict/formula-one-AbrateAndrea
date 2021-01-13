using FormulaOneDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FormulaOneWebForm
{
    public partial class Default : System.Web.UI.Page
    {
        DbTools myTools = new DbTools();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            { 
                DropDownList.DataSource = myTools.GetTablesName();
                DropDownList.DataBind();
                GridView1.DataSource = myTools.GetTable("Country");
                GridView1.DataBind();
            }
        }

        protected void Selection_Change(object sender, EventArgs e)
        {
            GridView1.DataSource = myTools.GetTable(DropDownList.SelectedValue);
            GridView1.DataBind();
        }
    }
}