using BanHang.Data;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class ChiTietDonHangHanMuc : System.Web.UI.Page
    {
        dtChiTietDonHangChiNhanh data = new dtChiTietDonHangChiNhanh();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] == "GPM")
            {
                if (dtSetting.LayChucNangCha(Session["IDNhom"].ToString(), 100) == false)
                {
                    Response.Redirect("Default.aspx");
                }
                else
                {
                    if (dtSetting.LayChucNangCha(Session["IDNhom"].ToString(), 100) == false)
                        btnDuyetDonHang.Enabled = false;
                    string IDDonHangChiNhanh = Request.QueryString["IDDonHangChiNhanh"];
                    if (IDDonHangChiNhanh != null)
                    {
                        if (Session["IDNhom"].ToString() != "4")
                        {
                            gridChiTiet.Columns["chucnang"].Visible = false;
                            btnThemMoi.Enabled = false;
                        }
                        LoadGrid(IDDonHangChiNhanh.ToString());
                    }
                }
            }
            else
            {
                Response.Redirect("DangNhap.aspx");
            }
        }
        private void LoadGrid(string IDDonHangChiNhanh)
        {

            data = new dtChiTietDonHangChiNhanh();
            gridChiTiet.DataSource = data.DanhSachChiTiet(IDDonHangChiNhanh);
            gridChiTiet.DataBind();
        }
        protected void gridChiTiet_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {

        }

        protected void gridChiTiet_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            string IDDonHangChiNhanh = Request.QueryString["IDDonHangChiNhanh"];
            int SoLuong = Int32.Parse(e.NewValues["SoLuong"].ToString());
            if (SoLuong >= 0)
            {
                string GhiChu = e.NewValues["GhiChu"] == null ? "" : e.NewValues["GhiChu"].ToString();
                string MaHang = e.NewValues["MaHang"].ToString();
                string IDHangHoa = dtHangHoa.LayIDHangHoa_MaHang(MaHang.Trim());

                data = new dtChiTietDonHangChiNhanh();
                data.CapNhatChiTietDonHang(IDDonHangChiNhanh, IDHangHoa, SoLuong, GhiChu);
                data.CapNhat_TongTrongLuong(IDDonHangChiNhanh, TinhTrongLuong().ToString(), TinhTongTien().ToString());
                e.Cancel = true;
                gridChiTiet.CancelEdit();
                LoadGrid(IDDonHangChiNhanh);
                dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Chi tiết đơn hàng chi nhánh", Session["IDKho"].ToString(), "Nhập xuất tồn", "Cập nhật");
            }
            else
            {
                throw new Exception("Lỗi: Số lượng phải >= 0 ?");
            }
        }
        public double TinhTrongLuong()
        {
            string IDDonHangChiNhanh = Request.QueryString["IDDonHangChiNhanh"];
            data = new dtChiTietDonHangChiNhanh();
            DataTable db = data.DanhSachChiTiet(IDDonHangChiNhanh);
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
        public double TinhTongTien()
        {
            string IDDonHangChiNhanh = Request.QueryString["IDDonHangChiNhanh"];
            data = new dtChiTietDonHangChiNhanh();
            DataTable db = data.DanhSachChiTiet(IDDonHangChiNhanh);
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
        protected void BtnSuaSoLuong_Click(object sender, EventArgs e)
        {
            string IDDonHangChiNhanh = Request.QueryString["IDDonHangChiNhanh"];
            string ID = (((ASPxButton)sender).CommandArgument).ToString();
            LoadGrid(IDDonHangChiNhanh.ToString());
            object MaHang = gridChiTiet.GetRowValuesByKeyValue(ID, "MaHang");
            object TenHang = gridChiTiet.GetRowValuesByKeyValue(ID, "IDHangHoa");
            object SoLuong = gridChiTiet.GetRowValuesByKeyValue(ID, "SoLuong");
            txtMaHangSua.Text = MaHang.ToString();
            txtTenHangSua.Text = dtHangHoa.LayTenHangHoa(TenHang.ToString());
            txtSoLuongSua.Text = SoLuong.ToString();
            hdfIDSuaSL.Value = ID;
            popupSuaSoLuong.ShowOnPageLoad = true;
        }
        protected void btnLuuSuaSL_Click(object sender, EventArgs e)
        {
            string ID = hdfIDSuaSL.Value;
            string IDDonHangChiNhanh = Request.QueryString["IDDonHangChiNhanh"];
            int SoLuong = Int32.Parse(txtSoLuongSua.Text);
            if (SoLuong >= 0)
            {
                data = new dtChiTietDonHangChiNhanh();
                data.CapNhatChiTietDonHangGiamSat(IDDonHangChiNhanh, dtHangHoa.LayIDHangHoa_MaHang(txtMaHangSua.Text.ToString()), SoLuong, "; " +Session["TenDangNhap"].ToString());
                data.CapNhat_TongTrongLuong(IDDonHangChiNhanh, TinhTrongLuong().ToString(), TinhTongTien().ToString());
                LoadGrid(IDDonHangChiNhanh);
                dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Chi tiết đơn hàng chi nhánh", Session["IDKho"].ToString(), "Nhập xuất tồn", "Cập nhật");
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Số lượng phải >= 0.'); </script>");
            }
            popupSuaSoLuong.ShowOnPageLoad = false;
        }

        protected void btnHuySuaSl_Click(object sender, EventArgs e)
        {
            popupSuaSoLuong.ShowOnPageLoad = false;
        }

        protected void gridChiTiet_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            Color color = (Color)ColorTranslator.FromHtml("#FF9797");
            int TrangThaiThem = Convert.ToInt32(e.GetValue("TrangThaiThem"));// lấy giá trị
            int SoLuongDeNghi = Convert.ToInt32(e.GetValue("SoLuongDeNghi"));// lấy giá trị
            if (TrangThaiThem == 1 || SoLuongDeNghi < 0)
                e.Row.BackColor = color;
        }
        protected void btnThemMoi_Click(object sender, EventArgs e)
        {
            string IDDonHangChiNhanh = Request.QueryString["IDDonHangChiNhanh"];
            dtDonHangChiNhanh data1 = new dtDonHangChiNhanh();
            data1.XoaChiTietDonHang_Nhap(IDDonHangChiNhanh);
            LoadGrid2(IDDonHangChiNhanh);
            popupThemMoi.ShowOnPageLoad = true;
        }

        protected void btnThem_Temp_Click(object sender, EventArgs e)
        {
            if (cmbHangHoa.Text != "")
            {
                int SoLuong = Int32.Parse(txtSoLuong.Text.ToString());
                if (SoLuong > 0)
                {
                    string IDDonHangChiNhanh = Request.QueryString["IDDonHangChiNhanh"];
                    string IDHangHoa = cmbHangHoa.Value.ToString();
                    string IDDonViTinh = dtHangHoa.LayIDDonViTinh(IDHangHoa);
                    string MaHang = dtHangHoa.LayMaHang(IDHangHoa);
                    float TrongLuong = dtHangHoa.LayTrongLuong(IDHangHoa);
                    string TonKho = txtTonKho.Text.ToString();
                    string GhiChu = txtGhiChuHangHoa.Text == null ? "" : txtGhiChuHangHoa.Text.ToString();
                    //string IDDonHangChiNhanh = Session["IDNhanVien"].ToString();
                    float DonGia = dtCapNhatTonKho.GiaBan_KhoChiNhanh(IDHangHoa, dtDonHangChiNhanh.LayIDKhoTheoDonHang(IDDonHangChiNhanh));

                    int SoNgayBan = dtSetting.LaySoNgayBanHang();
                    int SoLuongBan = dtDonHangChiNhanh.TuanSuatBanHang(DateTime.Now, cmbHangHoa.Value.ToString(), -SoNgayBan, dtDonHangChiNhanh.LayIDKhoTheoDonHang(IDDonHangChiNhanh));
                    string SoLuongGoiY = (SoLuongBan - Int32.Parse(TonKho)).ToString();
                    string SoLuongDaDat = dtDonHangChiNhanh.SoLuongDatHang(cmbHangHoa.Value.ToString(), dtDonHangChiNhanh.LayIDKhoTheoDonHang(IDDonHangChiNhanh));
                   // txtTanSuatBanhang.Text = SoLuongBan.ToString();


                    DataTable db = dtDonHangChiNhanh.KTChiTietDonHang_Temp(IDHangHoa, IDDonHangChiNhanh);// kiểm tra hàng hóa
                    if (db.Rows.Count == 0)
                    {
                        dtDonHangChiNhanh data1 = new dtDonHangChiNhanh();
                        data1.ThemChiTietDonHang_Temp(IDDonHangChiNhanh, MaHang, IDHangHoa, IDDonViTinh, (SoLuong * TrongLuong).ToString(), SoLuong, TonKho, GhiChu + "; " + Session["TenDangNhap"].ToString(), DonGia, SoLuongGoiY, SoLuongBan.ToString(), SoLuongDaDat);
                        CLear();
                    }
                    else
                    {
                        dtDonHangChiNhanh data1 = new dtDonHangChiNhanh();
                        data1.CapNhatChiTietDonHang_temp(IDDonHangChiNhanh, IDHangHoa, SoLuong, (SoLuong * TrongLuong).ToString(), TonKho, GhiChu + "; " + Session["TenDangNhap"].ToString(), DonGia);
                        CLear();
                    }
                    LoadGrid2(IDDonHangChiNhanh);
                }
                else
                {
                    Response.Write("<script language='JavaScript'> alert('Số Lượng phải > 0.'); </script>");
                    return;
                }
            }
        }
        private void LoadGrid2(string IDDonHangChiNhanh)
        {
            dtDonHangChiNhanh data1 = new dtDonHangChiNhanh();
            gridDanhSachHangHoa.DataSource = data1.DanhSachDonDatHangClient_Temp(IDDonHangChiNhanh);
            gridDanhSachHangHoa.DataBind();
        }
        public void CLear()
        {
            cmbHangHoa.Text = "";
            txtTonKho.Text = "";
            txtSoLuong.Text = "";
            txtTrongLuong.Text = "";
            txtSoLuongGoiY.Text = "";
            txtGhiChuHangHoa.Text = "";
        }
        protected void cmbHangHoa_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            long value = 0;
            if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;
            dsHangHoa.SelectCommand = @"SELECT GPM_HangHoa.ID, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa, GPM_DonViTinh.TenDonViTinh 
                                        FROM GPM_DonViTinh INNER JOIN GPM_HangHoa ON GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh
                                        WHERE (GPM_HangHoa.ID = @ID) AND  (GPM_HangHoa.IDTrangThaiHang = 1) AND (GPM_HangHoa.IDNhomDatHang != 3)";
            dsHangHoa.SelectParameters.Clear();
            dsHangHoa.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            comboBox.DataSource = dsHangHoa;
            comboBox.DataBind();
        }

        protected void cmbHangHoa_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            string IDDonHangChiNhanh = Request.QueryString["IDDonHangChiNhanh"];
            ASPxComboBox comboBox = (ASPxComboBox)source;

            dsHangHoa.SelectCommand = @"SELECT [ID], [MaHang], [TenHangHoa], [GiaMuaTruocThue] , [TenDonViTinh]
                                        FROM (
	                                        select GPM_HangHoa.ID, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa,GPM_HangHoa.GiaMuaTruocThue, GPM_DonViTinh.TenDonViTinh, 
	                                        row_number()over(order by GPM_HangHoa.MaHang) as [rn] 
	                                        FROM GPM_DonViTinh INNER JOIN GPM_HangHoa ON GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh           
	                                        WHERE ((GPM_HangHoa.MaHang LIKE @MaHang)) AND (GPM_HangHoa.DaXoa = 0) AND  ((GPM_HangHoa.IDTrangThaiHang = 1) OR (GPM_HangHoa.IDTrangThaiHang = 3) OR (GPM_HangHoa.IDTrangThaiHang = 6))
	                                        ) as st 
                                        where st.[rn] between @startIndex and @endIndex";

            dsHangHoa.SelectParameters.Clear();
            // dsHangHoa.SelectParameters.Add("TenHang", TypeCode.String, string.Format("%{0}%", e.Filter));
            dsHangHoa.SelectParameters.Add("MaHang", TypeCode.String, string.Format("%{0}%", e.Filter));
            dsHangHoa.SelectParameters.Add("IDKho", TypeCode.Int32, dtDonHangChiNhanh.LayIDKhoTheoDonHang(IDDonHangChiNhanh));
            dsHangHoa.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString());
            dsHangHoa.SelectParameters.Add("endIndex", TypeCode.Int64, (e.EndIndex + 1).ToString());
            comboBox.DataSource = dsHangHoa;
            comboBox.DataBind();
        }

        protected void cmbHangHoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbHangHoa.Text != "")
            {
                string IDDonHangChiNhanh = Request.QueryString["IDDonHangChiNhanh"];
                txtTrongLuong.Text = dtHangHoa.LayTrongLuong(cmbHangHoa.Value.ToString()).ToString();
                int TonKho = dtCapNhatTonKho.SoLuong_TonKho(cmbHangHoa.Value.ToString(), dtDonHangChiNhanh.LayIDKhoTheoDonHang(IDDonHangChiNhanh));
                txtTonKho.Text = TonKho.ToString();
                txtSoLuong.Text = "0";
                // tính tần suất bán hàng trong 10 ngày.
                int SoNgayBan = dtSetting.LaySoNgayBanHang();
                int SoLuongBan = dtDonHangChiNhanh.TuanSuatBanHang(DateTime.Now, cmbHangHoa.Value.ToString(), -SoNgayBan, dtDonHangChiNhanh.LayIDKhoTheoDonHang(IDDonHangChiNhanh));
                txtSoLuongGoiY.Text = (SoLuongBan - TonKho).ToString();
            }
        }

        protected void gridDanhSachHangHoa_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string IDDonHangChiNhanh = Request.QueryString["IDDonHangChiNhanh"];
            string ID = e.Keys[0].ToString();
            dtDonHangChiNhanh data1 = new dtDonHangChiNhanh();
            data1.XoaChiTietDonHang_Temp_ID(ID);
            e.Cancel = true;
            gridDanhSachHangHoa.CancelEdit();
            LoadGrid2(IDDonHangChiNhanh);
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            string IDDonHangChiNhanh = Request.QueryString["IDDonHangChiNhanh"];
            dtDonHangChiNhanh data1 = new dtDonHangChiNhanh();
            DataTable dt = data1.DanhSachDonDatHangClient_Temp(IDDonHangChiNhanh);
            if (dt.Rows.Count != 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string IDHangHoa = dr["IDHangHoa"].ToString();
                    string MaHang = dr["MaHang"].ToString();
                    string IDDonViTinh = dr["IDDonViTinh"].ToString();
                    string TrongLuong = dr["TrongLuong"].ToString();
                    string SoLuong = dr["SoLuong"].ToString();
                    string TonKho = dr["TonKho"].ToString();
                    string GhiChuHangHoa = dr["GhiChu"].ToString();
                    string TrangThai = dr["TrangThai"].ToString();
                    string DonGia = dr["DonGia"].ToString();
                    string ThanhTien = dr["ThanhTien"].ToString();
                    string SoLuongDeNghi = dr["SoLuongDeNghi"].ToString();
                    string TanSuatBanHang = dr["TanSuatBanHang"].ToString();
                    string SoLuongDatTruoc = dr["SoLuongDatTruoc"].ToString();
                    data1 = new dtDonHangChiNhanh();
                    data1.ThemChiTietDonHangClientGiamSat(IDDonHangChiNhanh, MaHang, IDHangHoa, IDDonViTinh, TrongLuong, SoLuong, TonKho, GhiChuHangHoa, TrangThai, dtDonHangChiNhanh.LayIDKhoTheoDonHang(IDDonHangChiNhanh), DonGia, ThanhTien, SoLuongDeNghi, TanSuatBanHang, SoLuongDatTruoc);
                }
                data = new dtChiTietDonHangChiNhanh();
                data.CapNhat_TongTrongLuong(IDDonHangChiNhanh, TinhTrongLuong().ToString(), TinhTongTien().ToString());
                data1 = new dtDonHangChiNhanh();
                data1.XoaChiTietDonHang_Nhap(IDDonHangChiNhanh);
                dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), " Giám sát Thêm Đặt Hàng Chi Nhánh", dtDonHangChiNhanh.LayIDKhoTheoDonHang(IDDonHangChiNhanh), "Nhập xuất tồn", "Thêm");
                LoadGrid(IDDonHangChiNhanh);
                popupThemMoi.ShowOnPageLoad = false;
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Danh sách hàng hóa rỗng.'); </script>");
                return;
            }
        }

        protected void btnHuy_Click(object sender, EventArgs e)
        {
            string IDDonHangChiNhanh = Request.QueryString["IDDonHangChiNhanh"];
            dtDonHangChiNhanh data1 = new dtDonHangChiNhanh();
            data1.XoaChiTietDonHang_Nhap(IDDonHangChiNhanh);
            popupThemMoi.ShowOnPageLoad = false;
        }

        protected void btnDuyetDonHang_Click(object sender, EventArgs e)
        {
            string IDDonHangChiNhanh = Request.QueryString["IDDonHangChiNhanh"];
            if (IDDonHangChiNhanh != null)
            {
                string IDNhomNguoiDung = Session["IDNhom"].ToString();
                string IDNhanVien = Session["IDNhanVien"].ToString();
                if (IDNhomNguoiDung == "6")
                {
                    // giám đốc
                    data = new dtChiTietDonHangChiNhanh();
                    data.GiamDocDuyet(IDDonHangChiNhanh, IDNhanVien); // ghi nhật ký
                }
                if (IDNhomNguoiDung == "5")
                {
                    // kho
                    data = new dtChiTietDonHangChiNhanh();
                    data.QuanLyDuyet(IDDonHangChiNhanh, IDNhanVien);
                }
                if (IDNhomNguoiDung == "4")
                {
                    // giám sát
                    data = new dtChiTietDonHangChiNhanh();
                    data.GiamSatDuyet(IDDonHangChiNhanh, IDNhanVien);
                }
                Response.Write("<script language='JavaScript'> alert('Duyệt đơn hàng thành công.'); </script>");
                gridChiTiet.Columns["chucnang"].Visible = false;
                btnThemMoi.Enabled = false;
                btnDuyetDonHang.Enabled = false;
                LoadGrid(IDDonHangChiNhanh);
            }
        }

        protected void gridDanhSachHangHoa_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            Color color = (Color)ColorTranslator.FromHtml("#FF9797");
            int SoLuongDeNghi = Convert.ToInt32(e.GetValue("SoLuongDeNghi"));// lấy giá trị
            if (SoLuongDeNghi < 0)
                e.Row.BackColor = color;
        }
    }
}