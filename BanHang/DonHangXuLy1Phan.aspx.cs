﻿using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class DonHangXuLy1Phan : System.Web.UI.Page
    {
        dtDuyetDonHangChiNhanh data = new dtDuyetDonHangChiNhanh();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                LoadGrid();
            }
        }
        private void LoadGrid()
        {
            data = new dtDuyetDonHangChiNhanh();
            gridDonDatHang.DataSource = data.LayDanhSachDonHangXuLy1Phan();
            gridDonDatHang.DataBind();
        }
        protected void gridDonDatHang_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            Color color = (Color)ColorTranslator.FromHtml("#FF9797");
            int MucDoUuTien = Convert.ToInt32(e.GetValue("MucDoUuTien"));// lấy giá trị
            if (MucDoUuTien == 1)
                e.Row.BackColor = color;
        }
    }
}