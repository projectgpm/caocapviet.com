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
    public partial class DuyetDonHangThuMua : System.Web.UI.Page
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
                if (!IsPostBack)
                {
                    //data = new dtDuyetDonHangChiNhanh();
                   // data.Xoa_ALL_Temp();
                    txtNguoiDuyet.Text = Session["TenDangNhap"].ToString();
                    btnThem.Enabled = false;
                }
                if (cmbSoDonHang.Text != "")
                {
                    LoadDanhSach(cmbSoDonHang.Value.ToString());
                }
            }
        }

        protected void gridDanhSachHangHoa_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            if (cmbSoDonHang.Text != "")
            {
                string ID = e.Keys[0].ToString();
                data = new dtDuyetDonHangThuMua();
                int SoLuongThucTe = Int32.Parse(e.NewValues["ThucTe"].ToString());
                int SoLuong = Int32.Parse(e.NewValues["SoLuong"].ToString());
                if (SoLuongThucTe >= 0)
                {
                    if (SoLuongThucTe > SoLuong)
                    {
                        throw new Exception("Lỗi: Số lượng thực tế <= Số lượng ");
                    }
                    else
                    {
                        string MaHang = e.NewValues["MaHang"].ToString();
                        string IDHangHoa = dtHangHoa.LayIDHangHoa_MaHang(MaHang.Trim());
                        float DonGia = float.Parse(e.NewValues["DonGia"].ToString());
                        data = new dtDuyetDonHangThuMua();
                        data.CapNhatChiTietDonHang(ID, IDHangHoa, SoLuongThucTe, DonGia, DonGia * SoLuongThucTe, SoLuong - SoLuongThucTe);
                        //dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Đơn hàng thu mua", Session["IDKho"].ToString(), "Nhập xuất tồn", "Cập nhật");
                    }
                }
                else
                {
                    throw new Exception("Lỗi: Số lượng không được âm? ");
                }
                e.Cancel = true;
                gridDanhSachHangHoa.CancelEdit();
                LoadDanhSach(cmbSoDonHang.Value.ToString());
            }
        }
        protected void cmbSoDonHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSoDonHang.Text != "")
            {
                btnThem.Enabled = true;
                data = new dtDuyetDonHangThuMua();
                data.Xoa_ALL_Temp();
                string ID = cmbSoDonHang.Value.ToString();
                DataTable db = data.LayDanhSachDonHang_ID(ID);
                if (db.Rows.Count > 0)
                {
                    DataRow dr = db.Rows[0];
                    cmbNguoiLap.Value = dr["IDNguoiLap"].ToString();
                    txtNgayLap.Date = DateTime.Parse(dr["NgayLap"].ToString());
                    cmbNhaCungCap.Value = dr["IDNhaCungCap"].ToString();
                    txtGhiChu.Text = dr["GhiChu"].ToString();
                    int TrangThai = Int32.Parse(dr["IDTrangThaiDonHang"].ToString());
                    if (TrangThai != 4)
                    {
                        DataTable dt = data.DanhSachChiTiet(ID);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in dt.Rows)
                            {
                                string IDHangHoa = dr1["IDHangHoa"].ToString();
                                string MaHang = dr1["MaHang"].ToString();
                                string IDDonViTinh = dr1["IDDonViTinh"].ToString();
                                string TrongLuong = dr1["TrongLuong"].ToString();
                                string SoLuong = dr1["SoLuong"].ToString();
                                string DonGia = dr1["DonGia"].ToString();
                                string ThanhTien = dr1["ThanhTien"].ToString();
                                data = new dtDuyetDonHangThuMua();
                                data.ThemChiTietDonHang_Temp(ID, MaHang, IDHangHoa, IDDonViTinh, TrongLuong, SoLuong, DonGia);
                            }
                            LoadDanhSach(ID);
                        }
                        else
                        {
                            Response.Write("<script language='JavaScript'> alert('Danh sách hàng hóa trống.'); </script>");
                        }
                    }
                    else
                    {
                        DataTable dt = data.DanhSachChiTiet_LOG(cmbSoDonHang.Text.ToString().Trim());
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in dt.Rows)
                            {
                                string IDHangHoa = dr1["IDHangHoa"].ToString();
                                string MaHang = dr1["MaHang"].ToString();
                                string IDDonViTinh = dr1["IDDonViTinh"].ToString();
                                string TrongLuong = dr1["TrongLuong"].ToString();
                                string SoLuong = dr1["SoLuong"].ToString();
                                string DonGia = dr1["DonGia"].ToString();
                                string ThanhTien = dr1["ThanhTien"].ToString();
                                data = new dtDuyetDonHangThuMua();
                                data.ThemChiTietDonHang_Temp(ID, MaHang, IDHangHoa, IDDonViTinh, TrongLuong, SoLuong, DonGia);
                            }
                            LoadDanhSach(ID);
                        }
                        else
                        {
                            Response.Write("<script language='JavaScript'> alert('Danh sách hàng hóa trống.'); </script>");
                        }
                    }
                }
                else
                {
                    Response.Write("<script language='JavaScript'> alert('Thông tin đơn hàng rỗng.'); </script>");
                }
            }
        }
        public void LoadDanhSach(string IDDonHangThuMua)
        {
            data = new dtDuyetDonHangThuMua();
            gridDanhSachHangHoa.DataSource = data.DanhSachChiTiet_Temp(IDDonHangThuMua);
            gridDanhSachHangHoa.DataBind();
        }
        protected void btnThem_Click(object sender, EventArgs e)
        {
            if (cmbSoDonHang.Text != "")
            {
                string SoDonHang = cmbSoDonHang.Text.Trim();
                string IDNguoiLap = cmbNguoiLap.Value.ToString();
                DateTime NgayLap = DateTime.Parse(txtNgayLap.Text);
                string TongTrongLuong = TinhTrongLuong().ToString();
                string TongTien = TinhTongTien().ToString();
                string IDNguoiDuyet = Session["IDNhanVien"].ToString();
                DateTime NgayDuyet = DateTime.Parse(txtNgayDuyet.Text);
                string GhiChu = txtGhiChu.Text == null ? "" : txtGhiChu.Text.ToString();
                string IDNhaCungCap = cmbNhaCungCap.Value.ToString();

                int TrangThai = Int32.Parse(cmbTrangThaiDonHang.Value.ToString());
                if (TrangThai != 4)
                {
                    // đơn hàng k treo
                    DataTable db = data.DanhSachChiTiet_Temp(cmbSoDonHang.Value.ToString());
                    data = new dtDuyetDonHangThuMua();
                    object ID = data.ThemDuyetDonHang(SoDonHang, IDNguoiLap, TongTrongLuong, TongTien, IDNguoiDuyet, GhiChu, NgayLap, NgayDuyet, IDNhaCungCap, TrangThai);
                    if (ID != null)
                    {
                        data = new dtDuyetDonHangThuMua();
                        foreach (DataRow dr1 in db.Rows)
                        {
                            int kt = 0;
                            string IDHangHoa = dr1["IDHangHoa"].ToString();
                            string MaHang = dr1["MaHang"].ToString();
                            string IDDonViTinh = dr1["IDDonViTinh"].ToString();
                            string TrongLuong = dr1["TrongLuong"].ToString();
                            string SoLuong = dr1["SoLuong"].ToString();
                            string DonGia = dr1["DonGia"].ToString();
                            string ThanhTien = dr1["ThanhTien"].ToString();
                            string ChenhLech = dr1["ChenhLech"].ToString();
                            string ThucTe = dr1["ThucTe"].ToString();
                            if (Int32.Parse(SoLuong) != Int32.Parse(ThucTe))
                            {
                                kt = 1;
                            }
                            data = new dtDuyetDonHangThuMua();
                            dtCapNhatTonKho.CongTonKho(IDHangHoa, ThucTe, Session["IDKho"].ToString());
                            data.ThemChiTietDonHang_Duyet(ID, MaHang, IDHangHoa, IDDonViTinh, TrongLuong, SoLuong, DonGia, ThanhTien, ChenhLech, ThucTe);
                            if (kt == 1)
                            {
                                data = new dtDuyetDonHangThuMua();
                                data.CapNhatChenhLech(ID);
                            }
                            DataTable LOG = data.KiemTra_LOG(cmbSoDonHang.Text.ToString().Trim(), IDHangHoa);
                            if (LOG.Rows.Count != 0)
                            {
                                data = new dtDuyetDonHangThuMua();
                                data.Xoa_LOG(cmbSoDonHang.Text.ToString().Trim(), IDHangHoa);
                            }
                        }
                        data = new dtDuyetDonHangThuMua();
                        data.CapNhatTrangThaiThuMua(cmbSoDonHang.Value.ToString(), TrangThai);
                        data.Xoa_ALL_Temp();
                    }

                    dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Đơn hàng thu mua", Session["IDKho"].ToString(), "Nhập xuất tồn", "Duyệt đơn hàng");
                    Response.Redirect("DanhSachPhieuDatHang.aspx");
                }
                else
                {
                    // đơn hàng treo
                    DataTable db = data.DanhSachChiTiet_Temp(cmbSoDonHang.Value.ToString());
                    data = new dtDuyetDonHangThuMua();
                    object ID = data.ThemDuyetDonHang(SoDonHang, IDNguoiLap, TongTrongLuong, TongTien, IDNguoiDuyet, GhiChu, NgayLap, NgayDuyet, IDNhaCungCap, TrangThai);
                    if (ID != null)
                    {
                        data = new dtDuyetDonHangThuMua();
                        foreach (DataRow dr1 in db.Rows)
                        {
                            int kt = 0;
                            string IDHangHoa = dr1["IDHangHoa"].ToString();
                            string MaHang = dr1["MaHang"].ToString();
                            string IDDonViTinh = dr1["IDDonViTinh"].ToString();
                            string TrongLuong = dr1["TrongLuong"].ToString();
                            string SoLuong = dr1["SoLuong"].ToString();
                            string DonGia = dr1["DonGia"].ToString();
                            string ThanhTien = dr1["ThanhTien"].ToString();
                            string ChenhLech = dr1["ChenhLech"].ToString();
                            string ThucTe = dr1["ThucTe"].ToString();
                            if (Int32.Parse(SoLuong) != Int32.Parse(ThucTe))
                            {
                                kt = 1;
                            }
                            data = new dtDuyetDonHangThuMua();
                            dtCapNhatTonKho.CongTonKho(IDHangHoa, ThucTe, Session["IDKho"].ToString());
                            data = new dtDuyetDonHangThuMua();
                            data.ThemChiTietDonHang_Duyet(ID, MaHang, IDHangHoa, IDDonViTinh, TrongLuong, SoLuong, DonGia, ThanhTien, ChenhLech, ThucTe);
                            DataTable LOG = data.KiemTra_LOG(cmbSoDonHang.Text.ToString().Trim(), IDHangHoa);
                            if (LOG.Rows.Count == 0)
                            {
                                data = new dtDuyetDonHangThuMua();
                                data.ThemChiTietDonHang_LOG(cmbSoDonHang.Text.ToString().Trim(), MaHang, IDHangHoa, IDDonViTinh, TrongLuong, ChenhLech, DonGia, Int32.Parse(ChenhLech) * float.Parse(DonGia));
                            }
                            else
                            {
                                data = new dtDuyetDonHangThuMua();
                                data.CapNhatChiTietDonHang_LOG(cmbSoDonHang.Text.ToString().Trim(), IDHangHoa, ChenhLech, DonGia, Int32.Parse(ChenhLech) * float.Parse(DonGia));
                            }

                            if (kt == 1)
                            {
                                data = new dtDuyetDonHangThuMua();
                                data.CapNhatChenhLech(ID);
                            }
                        }
                        data = new dtDuyetDonHangThuMua();
                        data.CapNhatTrangThaiThuMua(cmbSoDonHang.Value.ToString(), TrangThai);
                        data.Xoa_ALL_Temp();
                    }

                    dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Đơn hàng thu mua", Session["IDKho"].ToString(), "Nhập xuất tồn", "Duyệt đơn hàng treo");
                    Response.Redirect("DanhSachPhieuDatHang.aspx");
                }
            }
        }

        protected void btnHuy_Click(object sender, EventArgs e)
        {
            Response.Redirect("DanhSachPhieuDatHang.aspx");
        }

        protected void txtNgayDuyet_Init(object sender, EventArgs e)
        {
            txtNgayDuyet.Date = DateTime.Today;
        }
        public double TinhTongTien()
        {
            if (cmbSoDonHang.Text != "")
            {
                data = new dtDuyetDonHangThuMua();
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
                data = new dtDuyetDonHangThuMua();
                DataTable db = data.DanhSachChiTiet_Temp(cmbSoDonHang.Value.ToString());
                if (db.Rows.Count != 0)
                {
                    double Tong = 0;
                    foreach (DataRow dr in db.Rows)
                    {
                        double TrongLuong = double.Parse(dr["TrongLuong"].ToString());
                        int SoLuong = Int32.Parse(dr["ThucTe"].ToString());
                        Tong = Tong + (TrongLuong * SoLuong);
                    }
                    return Tong;
                }
                else
                    return 0;
            }
            return 0;
        }

        protected void cmbTrangThaiDonHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbTrangThaiDonHang.Text != "")
            {
                cmbSoDonHang.Enabled = true;
            }
        }

        
    }
}