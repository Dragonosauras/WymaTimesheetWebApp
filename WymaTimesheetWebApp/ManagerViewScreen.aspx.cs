﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WymaTimesheetWebApp
{
    public partial class ManagerViewScreen : System.Web.UI.Page
    {
        //To do list:

        //show unaccepted datafiles for all employees with that they can accept
        //have a button by each one that the allows the manager to view it.
        protected void Page_Load(object sender, EventArgs e)
        {
            ManagerName.InnerText = Global.ReadDataString($"SELECT EMPNAME FROM EMPLOYEES WHERE RESOURCENAME='{Session["ManagerName"].ToString()}';");
                
        }

        protected void viewTimeSheet_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}