﻿using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class DonHangDaDuyet : System.Web.UI.Page
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
                if (dtSetting.LayChucNangCha(Session["IDNhom"].ToString(), 67) == false)
                {
                    btnDuyetDonHang.Enabled = false;
                }

                int IDKho = Int32.Parse(Session["IDKho"].ToString());
                if (IDKho != 1)
                {
                    gridDonDatHang.Columns["ChungTu"].Visible = false;
                    btnDuyetDonHang.Visible = false;
                    btnTaoDonHang.Visible = true;
                    btnDonhangXuLy1Phan.Visible = false;
                }
                else
                {
                    gridDonDatHang.Columns["ChungTu"].Visible = true;
                    btnDuyetDonHang.Visible = true;
                    btnTaoDonHang.Visible = false;
                    btnDonhangXuLy1Phan.Visible = true;
                }
                if (!IsPostBack)
                {
                    dateTuNgay.Date = DateTime.Today.AddDays(-30);
                    dateDenNgay.Date = DateTime.Today;
                }
                if (dateTuNgay.Text != "" || dateDenNgay.Text != "")
                {
                    LoadGrid(Session["IDKho"].ToString(), cmbHienThi.Value.ToString());
                }
                //LoadGrid(IDKho.ToString());
            }
        }

        private void LoadGrid(string p, string HienThi)
        {
            data = new dtDuyetDonHangChiNhanh();
            string ngayBD = dateTuNgay.Date.ToString("yyyy-MM-dd");
            string ngayKT = dateDenNgay.Date.ToString("yyyy-MM-dd");
            ngayBD = ngayBD + " 00:00:0.000";
            ngayKT = ngayKT + " 23:59:59.999";
            gridDonDatHang.DataSource = data.LayDanhSachDonHangDuyet(p,HienThi,ngayBD,ngayKT);
            gridDonDatHang.DataBind();
        }

        protected void btnLoc_Click(object sender, EventArgs e)
        {
            LoadGrid(Session["IDKho"].ToString(), cmbHienThi.Value.ToString());
        }

        protected void cmbHienThi_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid(Session["IDKho"].ToString(), cmbHienThi.Value.ToString());
        }
    }
}