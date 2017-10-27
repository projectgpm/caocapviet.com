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
    public partial class ChiTietPhieuChuyenKho : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] == "GPM")
            {
                string IDPhieuChuyenKho = Request.QueryString["IDPhieuChuyenKho"];
                if (IDPhieuChuyenKho != null)
                {
                    LoadGrid(IDPhieuChuyenKho);
                }
            }
            else
            {
                Response.Redirect("DangNhap.aspx");
            }
            
        }
        private void LoadGrid(string IDPhieuChuyenKho)
        {
            dtPhieuChuyenKho data = new dtPhieuChuyenKho();
            DataTable da = data.DanhSachChiTietPhieuChuyenKho(IDPhieuChuyenKho);
            gridHangHoaChiTiet.DataSource = da;
            gridHangHoaChiTiet.DataBind();

            int IDNhom = Int32.Parse(Session["IDNhom"].ToString());
            int IDNhanVien = Int32.Parse(Session["IDNhanVien"].ToString());

            DataTable daPhieu = data.DanhSachPhieuChuyenKho_Kho(IDPhieuChuyenKho);
            int IDKhoXuat = Int32.Parse(daPhieu.Rows[0]["IDKhoXuat"].ToString());
            int IDKhoNhan = Int32.Parse(daPhieu.Rows[0]["IDKhoNhap"].ToString());
            int IDTrangThai = Int32.Parse(daPhieu.Rows[0]["IDTrangThai"].ToString());
            int DaXoa = Int32.Parse(daPhieu.Rows[0]["DaXoa"].ToString());

            if (DaXoa == 1 || IDTrangThai == 4)
            {
                btnXacNhanKhoNhan.Enabled = false;
                btnXacNhanKhoTong.Enabled = false;
                btnXacNhanKhoXuat.Enabled = false;
                gridHangHoaChiTiet.Columns["chucnang"].Visible = false;
            }

            if (IDTrangThai == 1)
            {
                btnXacNhanKhoNhan.Enabled = false;
                btnXacNhanKhoTong.Enabled = false;

                int QKhoXuat = data.ktXacNhan(IDNhanVien + "",IDKhoXuat + "");
                if (QKhoXuat == 0) btnXacNhanKhoXuat.Enabled = false;

            }

            if (IDTrangThai == 2)
            {
                btnXacNhanKhoTong.Enabled = false;
                btnXacNhanKhoXuat.Enabled = false;
                gridHangHoaChiTiet.Columns["chucnang"].Visible = false;

                int QKhoNhan = data.ktXacNhan(IDNhanVien + "", IDKhoNhan + "");
                if (QKhoNhan == 0) btnXacNhanKhoNhan.Enabled = false;
            }

            if (IDTrangThai == 3)
            {
                btnXacNhanKhoNhan.Enabled = false;
                btnXacNhanKhoXuat.Enabled = false;
                gridHangHoaChiTiet.Columns["chucnang"].Visible = false;

                int QKhoTong = data.ktXacNhan(IDNhanVien + "", 1 + "");
                if (QKhoTong == 0) btnXacNhanKhoTong.Enabled = false;
            }
        }

        protected void gridHangHoaChiTiet_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            int ID = Int32.Parse(e.Keys[0].ToString());
            string IDPhieuChuyenKho = Request.QueryString["IDPhieuChuyenKho"];
            dtPhieuChuyenKho dt = new dtPhieuChuyenKho();

            dt.XoaChiTietPhieuChuyenKho_Update(ID + "");

            DataTable da = dt.ChiTietTongSoLuongHangHoa(IDPhieuChuyenKho);
            if (da.Rows.Count != 0)
            {
                string tongTrongLuong = da.Rows[0]["TongTrongLuong"].ToString();
                string tongSoLuong = da.Rows[0]["TongSoLuong"].ToString();

                dt.CapNhatPhieuChuyenKho_2(IDPhieuChuyenKho, tongSoLuong, tongTrongLuong);

                dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Chi tiết phiếu chuyển kho", Session["IDKho"].ToString(), "Nhập xuất tồn", "Xóa");
            }

            e.Cancel = true;
            gridHangHoaChiTiet.CancelEdit();
            LoadGrid(IDPhieuChuyenKho);
        }

        protected void gridHangHoaChiTiet_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            if (e.NewValues["MaHang"] != null && e.NewValues["SoLuong"] != null)
            {
                string IDPhieuChuyenKho = Request.QueryString["IDPhieuChuyenKho"];
                string MaHang = e.NewValues["MaHang"].ToString();
                string SoLuong = e.NewValues["SoLuong"].ToString();
                string GhiChu = e.NewValues["GhiChu"] == null ? "" : e.NewValues["GhiChu"].ToString();
                string IDHH = dtHangHoa.LayIDHangHoa_MaHang(MaHang);
                float TrongLuong = dtHangHoa.LayTrongLuong(IDHH) * Int32.Parse(SoLuong);

                dtPhieuChuyenKho dt = new dtPhieuChuyenKho();
                DataTable dx = dt.KiemTraHangHoa(IDPhieuChuyenKho, IDHH);
                if (dx.Rows.Count == 0)
                {
                    dt.ThemChiTietPhieuChuyenKho(IDPhieuChuyenKho, IDHH, SoLuong, TrongLuong + "", GhiChu);
                    DataTable da = dt.ChiTietTongSoLuongHangHoa(IDPhieuChuyenKho);
                    if (da.Rows.Count != 0)
                    {
                        string tongTrongLuong = da.Rows[0]["TongTrongLuong"].ToString();
                        string tongSoLuong = da.Rows[0]["TongSoLuong"].ToString();

                        dt.CapNhatPhieuChuyenKho_2(IDPhieuChuyenKho, tongSoLuong, tongTrongLuong);
                    }
                }
                else
                {
                    dt.CapNhatChiTietPhieuChuyenKho(IDPhieuChuyenKho, IDHH, (Int32.Parse(dx.Rows[0]["SoLuong"].ToString()) + Int32.Parse(SoLuong)) + "", (float.Parse(dx.Rows[0]["TrongLuong"].ToString()) + TrongLuong) + "", GhiChu);
                    DataTable da = dt.ChiTietTongSoLuongHangHoa(IDPhieuChuyenKho);
                    if (da.Rows.Count != 0)
                    {
                        string tongTrongLuong = da.Rows[0]["TongTrongLuong"].ToString();
                        string tongSoLuong = da.Rows[0]["TongSoLuong"].ToString();

                        dt.CapNhatPhieuChuyenKho_2(IDPhieuChuyenKho, tongSoLuong, tongTrongLuong);
                    }
                }

                e.Cancel = true;
                gridHangHoaChiTiet.CancelEdit();
                LoadGrid(IDPhieuChuyenKho);
            }
            else throw new Exception("Không được bỏ trống dữ liệu.");
        }

        protected void btnXacNhanKhoNhan_Click(object sender, EventArgs e)
        {
            int IDNhom = Int32.Parse(Session["IDNhom"].ToString());
            int IDNhanVien = Int32.Parse(Session["IDNhanVien"].ToString());

            string IDPhieuChuyenKho = Request.QueryString["IDPhieuChuyenKho"];
            dtPhieuChuyenKho dt = new dtPhieuChuyenKho();
            DataTable da = dt.DanhSachPhieuChuyenKho_Kho(IDPhieuChuyenKho);
            int IDKhoNhan = Int32.Parse(da.Rows[0]["IDKhoNhap"].ToString());

            if (IDNhom == 3)
            {
                dt.DuyetChuyenKho_Nhap_CH1(IDPhieuChuyenKho, IDNhanVien + "");
            }
            else if (IDNhom == 4)
            {
                dt.DuyetChuyenKho_Nhap_GS1(IDPhieuChuyenKho, IDNhanVien + "");
            }

            LoadGrid(IDPhieuChuyenKho);
            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Phiếu chuyển kho", Session["IDKho"].ToString(), "Nhập xuất tồn", "Duyệt chuyển kho");
        }

        protected void btnXacNhanKhoXuat_Click(object sender, EventArgs e)
        {
            int IDNhom = Int32.Parse(Session["IDNhom"].ToString());
            int IDNhanVien = Int32.Parse(Session["IDNhanVien"].ToString());

            string IDPhieuChuyenKho = Request.QueryString["IDPhieuChuyenKho"];
            dtPhieuChuyenKho dt = new dtPhieuChuyenKho();
            DataTable da = dt.DanhSachPhieuChuyenKho_Kho(IDPhieuChuyenKho);
            int IDKhoXuat = Int32.Parse(da.Rows[0]["IDKhoXuat"].ToString());

            if (IDNhom == 3)
            {
                dt.DuyetChuyenKho_Xuat_CH1(IDPhieuChuyenKho, IDNhanVien + "");
            }
            else if (IDNhom == 4)
            {
                dt.DuyetChuyenKho_Xuat_GS1(IDPhieuChuyenKho, IDNhanVien + "");
            }

            LoadGrid(IDPhieuChuyenKho);
            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Phiếu chuyển kho", Session["IDKho"].ToString(), "Nhập xuất tồn", "Duyệt chuyển kho");
        }

        protected void btnXacNhanKhoTong_Click(object sender, EventArgs e)
        {
            int IDNhom = Int32.Parse(Session["IDNhom"].ToString());
            int IDNhanVien = Int32.Parse(Session["IDNhanVien"].ToString());

            string IDPhieuChuyenKho = Request.QueryString["IDPhieuChuyenKho"];
            dtPhieuChuyenKho dt = new dtPhieuChuyenKho();
            DataTable da = dt.DanhSachPhieuChuyenKho_Kho(IDPhieuChuyenKho);
            int IDKhoNhan = Int32.Parse(da.Rows[0]["IDKhoNhap"].ToString());
            int IDKhoXuat = Int32.Parse(da.Rows[0]["IDKhoXuat"].ToString());

            dt.DuyetChuyenKho_HoanThanh(IDPhieuChuyenKho, IDNhanVien + "");

            // Cộng, trừ tồn kho của 2 hệ thống...
            DataTable dataChiTiet = dt.DanhSachChiTietPhieuChuyenKho(IDPhieuChuyenKho);
            string SoPhieu = dtPhieuChuyenKho.MaPhieuChuyenKho(IDPhieuChuyenKho);
            for (int i = 0; i < dataChiTiet.Rows.Count; i++)
            {
                string IDHangHoa = dataChiTiet.Rows[i]["IDHangHoa"].ToString();
                string SoLuong = dataChiTiet.Rows[i]["SoLuong"].ToString();
                //dtCapNhatTonKho.TruTonKho(IDHangHoa, SoLuong, IDKhoXuat + "");
                //dtCapNhatTonKho.CongTonKho(IDHangHoa, SoLuong, IDKhoNhan + "");
                object TheKho1 = dtTheKho.ThemTheKho(SoPhieu, "Chuyển Kho " + dtTheKho.LayTenKho_ID(IDKhoXuat.ToString()) + " Sang " + dtTheKho.LayTenKho_ID(IDKhoNhan.ToString()), SoLuong, "0", (Int32.Parse(dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa, IDKhoNhan.ToString()).ToString()) + Int32.Parse(SoLuong)).ToString(), Session["IDNhanVien"].ToString(), Session["IDKho"].ToString(), IDHangHoa, "Nhập", "0", "0", "0", "0");
                object TheKho2 = dtTheKho.ThemTheKho(SoPhieu, "Chuyển Kho " + dtTheKho.LayTenKho_ID(IDKhoXuat.ToString()) + " Sang " + dtTheKho.LayTenKho_ID(IDKhoNhan.ToString()), "0", SoLuong, (Int32.Parse(dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa, IDKhoXuat.ToString()).ToString()) - Int32.Parse(SoLuong)).ToString(), Session["IDNhanVien"].ToString(), Session["IDKho"].ToString(), IDHangHoa, "Xuất", "0", "0", "0", "0");
                if (TheKho1 != null && TheKho2 != null)
                {
                    dtCapNhatTonKho.TruTonKho(IDHangHoa, SoLuong, IDKhoXuat + "");
                    dtCapNhatTonKho.CongTonKho(IDHangHoa, SoLuong, IDKhoNhan + "");
                }
            }

            LoadGrid(IDPhieuChuyenKho);
            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Phiếu chuyển kho", Session["IDKho"].ToString(), "Nhập xuất tồn", "Duyệt chuyển kho");

        }

    }
}