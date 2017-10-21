﻿using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class TraCuuDonHangChiNhanh : System.Web.UI.Page
    {
        dtTraCuuDonHangChiNhanh data = new dtTraCuuDonHangChiNhanh();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
        }

        protected void btnTraCuu_Click(object sender, EventArgs e)
        {
            if (txtSoDonHang.Text != "")
            {
                string SoDonHang = txtSoDonHang.Text.ToString();
                DataTable db = data.DanhSachDonHangChiNhanh(SoDonHang.Trim());
                if (db.Rows.Count > 0)
                {
                    DataRow dr = db.Rows[0];
                    cmbNguoiLap.Value = dr["IDNguoiLap"].ToString();
                    txtNgayDatHang.Date = DateTime.Parse(dr["NgayDat"].ToString());
                    txtNgayGiao.Date = DateTime.Parse(dr["NgayGiaoDuKien"].ToString());
                    dateNgayLap.Date = DateTime.Parse(dr["NgayLap"].ToString());
                    cmbChiNhanh.Value = dr["IDKho"].ToString();
                    txtGhiChu.Text = dr["GhiChu"].ToString();
                    txtTrongLuong.Text = dr["TongTrongLuong"].ToString();
                    if (dr["MucDoUuTien"].ToString() == "1")
                    {
                        txtMucDoUuTien.Text = "Ưu tiên";
                    }
                    else
                    {
                        txtMucDoUuTien.Text = "Không ưu tiên";
                    }
                    int TrangThaiDonHang = Int32.Parse(dr["IDTrangThaiDonHang"].ToString());
                    if (TrangThaiDonHang == 4)
                    {
                        txtTrangThaiDonHang.Text = "Đơn Hàng Xử Lý 1 Phần";
                    }
                    else if (TrangThaiDonHang == 3)
                    {
                        txtTrangThaiDonHang.Text = "Đơn Hàng Chưa Xử Lý";
                    }
                    else if (TrangThaiDonHang == 2)
                    {
                        txtTrangThaiDonHang.Text = "Đơn Hàng Hủy";
                    }
                    else
                    {
                        txtTrangThaiDonHang.Text = "Đơn Hàng Hoàn Tất";
                    }
                    // danh sách chi tiết số đơn hàng
                    DataTable dt = data.DanhSachChiTiet(SoDonHang);
                    if (dt.Rows.Count > 0)
                    {
                        gridDanhSachHangHoa.DataSource = dt;
                        gridDanhSachHangHoa.DataBind();
                    }
                }
                else
                {
                    Clear();
                    Response.Write("<script language='JavaScript'> alert('Số đơn hàng không tồn tại.'); </script>");
                    return;
                }
            }
            else
            {
                Clear();
                Response.Write("<script language='JavaScript'> alert('Vui lòng nhập số đơn hàng.'); </script>");
            }
        }
        public void Clear()
        {
            txtSoDonHang.Text = "";
            txtSoDonHang.Focus();
            cmbNguoiLap.Text = "";
            txtNgayDatHang.Text = "";
            txtNgayGiao.Text = "";
            dateNgayLap.Text = "";
            txtTrangThaiDonHang.Text = "";
            txtGhiChu.Text = "";
            cmbChiNhanh.Text = "";
            txtMucDoUuTien.Text = "";
            txtTrongLuong.Text = "";
            gridDanhSachHangHoa.DataSource = null;
            gridDanhSachHangHoa.DataBind();
        }
        protected void btnHuy_Click(object sender, EventArgs e)
        {
            Response.Redirect("DonDatHangChiNhanh.aspx");
        }
    }
}