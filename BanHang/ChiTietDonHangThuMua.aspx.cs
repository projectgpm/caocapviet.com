using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class ChiTietDonHangThuMua : System.Web.UI.Page
    {
        dtThuMuaDatHang data = new dtThuMuaDatHang();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] == "GPM")
            {
                string IDDonHangThuMua = Request.QueryString["IDDonHangThuMua"];
                if (IDDonHangThuMua != null)
                {
                    if (dtThuMuaDatHang.LayTrangThaiDonHang(IDDonHangThuMua) == 1 || dtThuMuaDatHang.DonHangHuy(IDDonHangThuMua) == 1)
                    {
                        memoLyDo.Visible = false;
                        btnHuyDonHang.Visible = false;
                        gridChiTiet.Columns["chucnang"].Visible = false;
                    }
                    
                    LoadGrid(IDDonHangThuMua.ToString());
                }
            }
            else
            {
                Response.Redirect("DangNhap.aspx");
            }
        }
        private void LoadGrid(string IDDonHangThuMua)
        {
            data = new dtThuMuaDatHang();
            gridChiTiet.DataSource = data.DanhSachChiTiet(IDDonHangThuMua);
            gridChiTiet.DataBind();
        }

        protected void gridChiTiet_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            string IDDonHangThuMua = Request.QueryString["IDDonHangThuMua"];
            int SoLuong = Int32.Parse(e.NewValues["SoLuong"].ToString());
            if (SoLuong >= 0)
            {
                data = new dtThuMuaDatHang();
                data.CapNhatChiTietDonHang(ID,SoLuong);
                int GiaTri = dtThuMuaDatHang.LayTyLeChietKhau(IDDonHangThuMua);
                double TongTien = TinhTongTien();
                double Tylegiam = (GiaTri * (0.01));
                double TienGiam = TongTien * Tylegiam;
                double TienSauCK = (TongTien - TienGiam);
                data.CapNhat_TongTien_TongTrongLuong(IDDonHangThuMua, TinhTongTien().ToString(), TinhTrongLuong().ToString(), TienSauCK.ToString());
                e.Cancel = true;
                gridChiTiet.CancelEdit();
                LoadGrid(IDDonHangThuMua);

                dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Chi tiết đơn hàng thu mua", Session["IDKho"].ToString(), "Nhập xuất tồn", "Cập nhật");
            }
            else
            {
                throw new Exception("Lỗi: Số lượng >= 0 ?");
            }
        }
        public double TinhTongTien()
        {
            string IDDonHangThuMua = Request.QueryString["IDDonHangThuMua"];
            data = new dtThuMuaDatHang();
            DataTable db = data.DanhSachChiTiet(IDDonHangThuMua);
            if (db.Rows.Count != 0)
            {
                double TongTien = 0;
                foreach (DataRow dr in db.Rows)
                {
                    double ThanhTien = double.Parse(dr["ThanhTien"].ToString());
                    TongTien = TongTien + ThanhTien;
                }
                return TongTien;
            }
            else
                return 0;
        }
        public double TinhTrongLuong()
        {
            string IDDonHangThuMua = Request.QueryString["IDDonHangThuMua"];
            data = new dtThuMuaDatHang();
            DataTable db = data.DanhSachChiTiet(IDDonHangThuMua);
            if (db.Rows.Count != 0)
            {
                double Tong = 0;
                foreach (DataRow dr in db.Rows)
                {
                    double TrongLuong = double.Parse(dr["TrongLuong"].ToString());
                    int SoLuong = Int32.Parse(dr["SoLuong"].ToString());
                    Tong = Tong + (TrongLuong * SoLuong);
                }
                return Tong;
            }
            else
                return 0;
        }

        protected void btnHuyDonHang_Click1(object sender, EventArgs e)
        {
            string IDDonHangThuMua = Request.QueryString["IDDonHangThuMua"];
            if (IDDonHangThuMua != null && memoLyDo.Text != "")
            {
                data = new dtThuMuaDatHang();
                data.CapNhatTrangThaiDonHang(IDDonHangThuMua, memoLyDo.Text);
                btnHuyDonHang.Enabled = false;
                memoLyDo.Enabled = false;
                gridChiTiet.Columns["chucnang"].Visible = false;
                LoadGrid(IDDonHangThuMua.ToString());
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Vui lòng nhập lý do hủy đơn hàng.'); </script>");
            }
        }

        protected void ASPxFormLayout1_E1_Click(object sender, EventArgs e)
        {
            //popup.ContentUrl = "~/InDonDatHang.aspx?ID=" + Request.QueryString["IDDonHangThuMua"];
            //popup.ShowOnPageLoad = true;
            string jsInHoaDon = "window.open(\"InDonDatHang.aspx?ID=" + Request.QueryString["IDDonHangThuMua"] + "\", \"PrintingFrame\");";
            ClientScript.RegisterStartupScript(this.GetType(), "Print", jsInHoaDon, true);
        }
    }
}