﻿using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class DanhSachPhieuDatHang : System.Web.UI.Page
    {
        dtDuyetDonHangThuMua data = new dtDuyetDonHangThuMua();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }

            else
            {
                if (Int32.Parse(Session["IDKho"].ToString()) != 1)
                {
                    btnDonHangDaDuyet.Visible = false;
                    btnDuyetDonHang.Visible = false;
                    btnThemDonHang.Visible = false;
                }
                LoadGrid();
            }
        }

        private void LoadGrid()
        {
            data = new dtDuyetDonHangThuMua();
            gridDonDatHang.DataSource = data.DanhSachDuyet_ThuMua();
            gridDonDatHang.DataBind();
        }
    }
}