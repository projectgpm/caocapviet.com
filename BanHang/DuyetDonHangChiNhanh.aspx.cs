using BanHang.Data;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class DuyetDonHangChiNhanh : System.Web.UI.Page
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
                if (!IsPostBack)
                {
                    //data = new dtDuyetDonHangChiNhanh();
                    //data.Xoa_ALL_Temp();
                    //txtNguoiDuyet.Text = Session["TenDangNhap"].ToString();
                    //cmbKhoDuyet.Value = Session["IDKho"].ToString();
                    //btnThem.Enabled = false;
                }
                
            }
        }
        public void LoadDanhSach(string SoDonHang)
        {
            data = new dtDuyetDonHangChiNhanh();
            gridDanhSachHangHoa.DataSource = data.DanhSachChiTiet_Temp(SoDonHang);
            gridDanhSachHangHoa.DataBind();
        }
        protected void txtNgayDuyet_Init(object sender, EventArgs e)
        {
            txtNgayDuyet.Date = DateTime.Today;
        }

        protected void gridDanhSachHangHoa_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            if (cmbSoDonHang.Text != "")
            {
                string ID = e.Keys[0].ToString();
                data = new dtDuyetDonHangChiNhanh();
                data.Xoa_Temp(ID);
                e.Cancel = true;
                gridDanhSachHangHoa.CancelEdit();
                LoadDanhSach(cmbSoDonHang.Value.ToString());

                dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Đơn hàng chi nhánh", Session["IDKho"].ToString(), "Nhập xuất tồn", "xóa");
            }
        }

        protected void gridDanhSachHangHoa_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            if (cmbSoDonHang.Text != "")
            {
                string ID = e.Keys[0].ToString();
                data = new dtDuyetDonHangChiNhanh();
                int SoLuong = Int32.Parse(e.NewValues["SoLuong"].ToString());
                if (SoLuong >= 0)
                {
                    //string MaHang = e.NewValues["MaHang"].ToString();
                    //string IDHangHoa = dtHangHoa.LayIDHangHoa_MaHang(MaHang.Trim());
                    //float DonGia = dtHangHoa.LayGiaBanSauThue(IDHangHoa);
                    //data = new dtDuyetDonHangChiNhanh();
                    //data.CapNhatChiTietDonHang(ID, IDHangHoa, SoLuong, DonGia, DonGia * SoLuong);
                    //txtTongTien.Text = TinhTongTien().ToString();
                    //txtTongTrongLuong.Text = TinhTrongLuong().ToString();
                }
                else
                {
                    throw new Exception("Lỗi: Số lượng không được âm? ");
                }
                e.Cancel = true;
                gridDanhSachHangHoa.CancelEdit();
                LoadDanhSach(cmbSoDonHang.Value.ToString());

                dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Đơn hàng chi nhánh", Session["IDKho"].ToString(), "Nhập xuất tồn", "Cập nhật");
            }
        }
        public double TinhTongTien()
        {
            if (cmbSoDonHang.Text != "")
            {
                data = new dtDuyetDonHangChiNhanh();
                DataTable db = data.DanhSachChiTiet_Temp(cmbSoDonHang.Value.ToString());
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
            return 0;
        }
        public double TinhTrongLuong()
        {
            if (cmbSoDonHang.Text != "")
            {
                data = new dtDuyetDonHangChiNhanh();
                DataTable db = data.DanhSachChiTiet_Temp(cmbSoDonHang.Value.ToString());
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
            return 0;
        }
        protected void cmbSoDonHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSoDonHang.Text != "")
            {
                btnThem.Enabled = true;
                data = new dtDuyetDonHangChiNhanh();
                data.Xoa_ALL_Temp();
                string ID = cmbSoDonHang.Value.ToString();
                DataTable db = data.LayDanhSachDonHang_ID(ID);
                if (db.Rows.Count > 0)
                {
                    //DataRow dr = db.Rows[0];
                    //cmbNguoiLap.Value = dr["IDNguoiLap"].ToString();
                    //txtNgayLap.Text = dr["NgayLap"].ToString();
                    //txtTongTien.Text = dr["TongTien"].ToString();
                    //txtTongTrongLuong.Text = dr["TongTrongLuong"].ToString();
                    //cmbKhoLap.Value = dr["IDKho"].ToString();
                    //txtGhiChu.Text = dr["GhiChu"].ToString();
                    //DataTable dt = data.DanhSachChiTiet(ID);
                    //if (dt.Rows.Count > 0)
                    //{
                    //    foreach (DataRow dr1 in dt.Rows)
                    //    {
                    //        string IDHangHoa = dr1["IDHangHoa"].ToString();
                    //        string MaHang = dr1["MaHang"].ToString();
                    //        string IDDonViTinh = dr1["IDDonViTinh"].ToString();
                    //        string TrongLuong = dr1["TrongLuong"].ToString();
                    //        string SoLuong = dr1["SoLuong"].ToString();
                    //        string DonGia = dr1["DonGia"].ToString();
                    //        string ThanhTien = dr1["ThanhTien"].ToString();
                    //        data = new dtDuyetDonHangChiNhanh();
                    //        data.ThemChiTietDonHang_Temp(ID, MaHang, IDHangHoa, IDDonViTinh, TrongLuong, SoLuong, DonGia, ThanhTien);
                    //    }
                    //    LoadDanhSach(ID);
                    //}
                    //else
                    //{
                    //    Response.Write("<script language='JavaScript'> alert('Danh sách hàng hóa trống.'); </script>");
                    //}
                }
                else
                {
                    Response.Write("<script language='JavaScript'> alert('Thông tin đơn hàng rỗng.'); </script>");
                }
            }
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
           // if (cmbSoDonHang.Text != "")
           // {
            //    string SoDonHang = cmbSoDonHang.Text.Trim();
            //    string IDNguoiLap = cmbNguoiLap.Value.ToString();
            //    DateTime NgayLap = DateTime.Parse(txtNgayLap.Text);
            //    string TongTrongLuong = txtTongTrongLuong.Text;
            //    string TongTien = txtTongTien.Text;
            //    string IDKhoLap = cmbKhoLap.Value.ToString();
            //    string IDKhoDuyet = cmbKhoDuyet.Value.ToString();
            //    string IDNguoiDuyet = Session["IDNhanVien"].ToString();
            //    DateTime NgayDuyet = DateTime.Parse(txtNgayDuyet.Text);
            //    string GhiChu = txtGhiChu.Text == null ? "" : txtGhiChu.Text.ToString();

            //    if (dtSetting.KT_ChuyenAm() == 1)
            //    {
            //        DataTable db = data.DanhSachChiTiet_Temp(cmbSoDonHang.Value.ToString());
            //        data = new dtDuyetDonHangChiNhanh();
            //        object ID = data.ThemDuyetDonHang(SoDonHang, IDNguoiLap, TongTrongLuong, TongTien, IDKhoLap, IDKhoDuyet, IDNguoiDuyet, GhiChu, NgayLap, NgayDuyet);
            //        if (ID != null)
            //        {
            //            data = new dtDuyetDonHangChiNhanh();
            //            foreach (DataRow dr1 in db.Rows)
            //            {
            //                string IDHangHoa = dr1["IDHangHoa"].ToString();
            //                string MaHang = dr1["MaHang"].ToString();
            //                string IDDonViTinh = dr1["IDDonViTinh"].ToString();
            //                string TrongLuong = dr1["TrongLuong"].ToString();
            //                string SoLuong = dr1["SoLuong"].ToString();
            //                string DonGia = dr1["DonGia"].ToString();
            //                string ThanhTien = dr1["ThanhTien"].ToString();
            //                data = new dtDuyetDonHangChiNhanh();
            //                dtCapNhatTonKho.TruTonKho(IDHangHoa, SoLuong, IDKhoDuyet);
            //                dtCapNhatTonKho.CongTonKho(IDHangHoa, SoLuong, IDKhoLap);
            //                data.ThemChiTietDonHang_Duyet(ID, MaHang, IDHangHoa, IDDonViTinh, TrongLuong, SoLuong, DonGia, ThanhTien);
            //            }
            //            data = new dtDuyetDonHangChiNhanh();
            //            data.CapNhatTrangThaiClient(cmbSoDonHang.Value.ToString());
            //            data.Xoa_ALL_Temp();
            //        }

            //        dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Đơn hàng chi nhánh", Session["IDKho"].ToString(), "Nhập xuất tồn", "Duyệt");
            //        Response.Redirect("DonHangDaDuyet.aspx");
            //    }
            //    else
            //    {
            //        int kt = 0;
            //        DataTable db = data.DanhSachChiTiet_Temp(cmbSoDonHang.Value.ToString());
            //        foreach (DataRow dr1 in db.Rows)
            //        {
            //            string IDHangHoa = dr1["IDHangHoa"].ToString();
            //            int SoLuong = Int32.Parse(dr1["SoLuong"].ToString());
            //            int SLTon = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa, Session["IDKho"].ToString());
            //            if (SLTon < SoLuong)
            //            {
            //                kt = 1;
            //                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Lỗi: Số lượng tồn kho không đủ:" + dtHangHoa.LayTenHangHoa(IDHangHoa) + "');", true);
            //                //throw new Exception("Lỗi: Số lượng tồn kho không đủ: " + dtHangHoa.LayTenHangHoa(IDHangHoa));
            //                return;
            //            }
            //        }
            //        if (kt == 0)
            //        {
            //            data = new dtDuyetDonHangChiNhanh();
            //            object ID = data.ThemDuyetDonHang(SoDonHang, IDNguoiLap, TongTrongLuong, TongTien, IDKhoLap, IDKhoDuyet, IDNguoiDuyet, GhiChu, NgayLap, NgayDuyet);
            //            if (ID != null)
            //            {
            //                data = new dtDuyetDonHangChiNhanh();
            //                foreach (DataRow dr1 in db.Rows)
            //                {
            //                    string IDHangHoa = dr1["IDHangHoa"].ToString();
            //                    string MaHang = dr1["MaHang"].ToString();
            //                    string IDDonViTinh = dr1["IDDonViTinh"].ToString();
            //                    string TrongLuong = dr1["TrongLuong"].ToString();
            //                    string SoLuong = dr1["SoLuong"].ToString();
            //                    string DonGia = dr1["DonGia"].ToString();
            //                    string ThanhTien = dr1["ThanhTien"].ToString();
            //                    data = new dtDuyetDonHangChiNhanh();
            //                    dtCapNhatTonKho.TruTonKho(IDHangHoa, SoLuong, IDKhoDuyet);
            //                    dtCapNhatTonKho.CongTonKho(IDHangHoa, SoLuong, IDKhoLap);
            //                    data.ThemChiTietDonHang_Duyet(ID, MaHang, IDHangHoa, IDDonViTinh, TrongLuong, SoLuong, DonGia, ThanhTien);
            //                }
            //                data = new dtDuyetDonHangChiNhanh();
            //                data.CapNhatTrangThaiClient(cmbSoDonHang.Value.ToString());

            //                data.Xoa_ALL_Temp();
            //            }

            //            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Đơn hàng chi nhánh", Session["IDKho"].ToString(), "Nhập xuất tồn", "Duyệt");
            //            Response.Redirect("DonHangDaDuyet.aspx");
            //        }
            //    }
            //}
        }

        protected void btnHuy_Click(object sender, EventArgs e)
        {
            Response.Redirect("DonDatHangChiNhanh.aspx");
        }

        protected void BtnSuaSoLuong_Click(object sender, EventArgs e)
        {
            //string ID = (((ASPxButton)sender).CommandArgument).ToString();
            //LoadDanhSach(cmbSoDonHang.Value.ToString());
            //object MaHang = gridDanhSachHangHoa.GetRowValuesByKeyValue(ID, "MaHang");
            //object TenHang = gridDanhSachHangHoa.GetRowValuesByKeyValue(ID, "IDHangHoa");
            //object SoLuong = gridDanhSachHangHoa.GetRowValuesByKeyValue(ID, "SoLuong");
            //txtMaHangSua.Text = MaHang.ToString();
            //txtTenHangSua.Text = dtHangHoa.LayTenHangHoa(TenHang.ToString());
            //txtSoLuongSua.Text = SoLuong.ToString();
            //hdfIDSuaSL.Value = ID;
            //popupSuaSoLuong.ShowOnPageLoad = true;
        }

        protected void btnHuySuaSl_Click(object sender, EventArgs e)
        {
          //  popupSuaSoLuong.ShowOnPageLoad = false;
        }

        protected void btnLuuSuaSL_Click(object sender, EventArgs e)
        {
            //string ID = hdfIDSuaSL.Value;
            //data = new dtDuyetDonHangChiNhanh();
            //int SoLuong = Int32.Parse(txtSoLuongSua.Text);
            //if (SoLuong >= 0)
            //{
            //    //string MaHang = txtMaHangSua.Text;
            //    //string IDHangHoa = dtHangHoa.LayIDHangHoa_MaHang(MaHang.Trim());
            //    //float DonGia = dtHangHoa.LayGiaBanSauThue(IDHangHoa);
            //    //data = new dtDuyetDonHangChiNhanh();
            //    //data.CapNhatChiTietDonHang(ID, IDHangHoa, SoLuong, DonGia, DonGia * SoLuong);
            //    //txtTongTien.Text = TinhTongTien().ToString();
            //    //txtTongTrongLuong.Text = TinhTrongLuong().ToString();
            //}
            //else
            //{
            //    throw new Exception("Lỗi: Số lượng không được âm? ");
            //}
            //LoadDanhSach(cmbSoDonHang.Value.ToString());
            //popupSuaSoLuong.ShowOnPageLoad = false;
        }
    }
}