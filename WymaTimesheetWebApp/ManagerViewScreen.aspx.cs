﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WymaTimesheetWebApp
{
    public partial class ManagerViewScreen : System.Web.UI.Page
    {
        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //Functions that run on page load
                string ManagerNameData = Session["ManagerName"].ToString();
                ManagerName.InnerText = Global.ReadDataString($"SELECT EMPNAME FROM EMPLOYEES WHERE RESOURCENAME='{ManagerNameData}';");

                DataTable unsignedTimesheets = new DataTable();
                unsignedTimesheets.Columns.Add("Name");
                unsignedTimesheets.Columns.Add("Date Submitted");
                ManagerView.DataSource = unsignedTimesheets;
                ManagerView.DataBind();

                Session["MangV"] = unsignedTimesheets;

                ShowFiles(ManagerNameData);

            }
            
        }

        protected void viewTimeSheet_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //Allow manager to select a timesheet and view selected 

            //Accesses Unsigned Timesheets
            DataTable unsignedTimesheets = Session["MangV"] as DataTable;
            List<Global.DataFileInfo> UnapprovedFiles = Global.UnapprovedFiles;


            if (e.CommandName == "ViewTimeSheet")
            {
                DataFile df = new DataFile();

                int index = Convert.ToInt32(e.CommandArgument);

                string usrName = Global.ReadDataString($"SELECT RESOURCENAME FROM EMPLOYEES WHERE EMPNAME='{unsignedTimesheets.Rows[index].Field<string>(0)}';");
                string date = unsignedTimesheets.Rows[index].Field<string>(1);
                string managerName = Session["ManagerName"].ToString();





                df.Read($"{usrName} {date} {managerName}");
                DataTable FileData = df;

                ManagerView.DataSource = FileData;
                ManagerView.DataBind();

                Response.Write("<script>alert('"+ usrName +"')</script>");
                

            }

        }

        private void ShowFiles(string manager)
        {

            List<Global.DataFileInfo> UnapprovedFiles = Global.UnapprovedFiles;
            DataTable unsignedTimesheets = Session["MangV"] as DataTable;

            foreach (Global.DataFileInfo data in UnapprovedFiles)
            {
                if (data.manager == manager)
                {
                    DataRow dr = unsignedTimesheets.NewRow();

                    dr["Name"] = Global.ReadDataString($"SELECT EMPNAME FROM EMPLOYEES WHERE RESOURCENAME='{data.name}';");
                    dr["Date Submitted"] = data.date;

                    unsignedTimesheets.Rows.Add(dr);
                    ManagerView.DataSource = unsignedTimesheets;
                    ManagerView.DataBind();
                    Session["MangV"] = unsignedTimesheets;
                }
            }
        }

        protected void btnMVBack(object sender, EventArgs e)
        {
            Server.Transfer("MainMenu.aspx");
        }
    }
}