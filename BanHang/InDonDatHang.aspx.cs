﻿using BanHang.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraReports.UI;
using System.IO;
using DevExpress.XtraPrinting;
using System.Data.SqlClient;
using BanHang.Data;
namespace BanHang
{
    public partial class InDonDatHang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string ID = Request.QueryString["ID"];
            //rpDonDatHang rp = new rpDonDatHang();
            //rp.Parameters["ID"].Value = ID;
            //rp.Parameters["ID"].Visible = false;

            //viewerReport.Report = rp;

            using (MemoryStream ms = new MemoryStream())
            {
                rpDonDatHang r = new rpDonDatHang();
                r.Parameters["ID"].Value = Request.QueryString["ID"];
                r.CreateDocument();
                PdfExportOptions opts = new PdfExportOptions();
                opts.ShowPrintDialogOnOpen = true;
                r.ExportToPdf(ms, opts);
                ms.Seek(0, SeekOrigin.Begin);
                byte[] report = ms.ToArray();
                Page.Response.ContentType = "application/pdf";
                Page.Response.Clear();
                Page.Response.OutputStream.Write(report, 0, report.Length);
                Page.Response.End();
            } 
        }
    }
}