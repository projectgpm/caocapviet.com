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
    public partial class TraCuuMaHang : System.Web.UI.Page
    {
        dtTraCuuMaHang data = new dtTraCuuMaHang();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                if (dtSetting.LayChucNang_HienThi(Session["IDNhom"].ToString()) == true)
                {
                    if (txtMaHangTraCuu.Text != "")
                    {
                        string MaHang = txtMaHangTraCuu.Value.ToString();
                        DataTable db = data.TraCuuMaHang(MaHang);
                        if (db.Rows.Count > 0)
                        {
                            LoadDanhSachBarcode(dtHangHoa.LayIDHangHoa_MaHang(MaHang));
                           // LoadDanhSachHangQuiDoi(dtHangHoa.LayIDHangHoa_MaHang(MaHang));
                            LoadDanhSachGiaBanTheoSoLuong(dtHangHoa.LayIDHangHoa_MaHang(MaHang));
                            LoadDanhSachCombo(dtHangHoa.LayIDHangHoa_MaHang(MaHang));
                        }
                    }
                }
                else
                    Response.Redirect("Default.aspx");
            }
        }
        public void LoadDanhSachBarcode(string IDHangHoa)
        {
            data = new dtTraCuuMaHang();
            gridBarcode.DataSource = data.DanhSachBarcode(IDHangHoa);
            gridBarcode.DataBind();
        }
        public void LoadDanhSachHangQuiDoi(string IDHangHoa)
        {
            data = new dtTraCuuMaHang();
           // gridHangQuiDoi.DataSource = data.DanhSachHangHoaQuiDoi(IDHangHoa);
           // gridHangQuiDoi.DataBind();
        }
        public void LoadDanhSachGiaBanTheoSoLuong(string IDHangHoa)
        {
            data = new dtTraCuuMaHang();
            gridSoLuong.DataSource = data.DanhSachGiaTheoSoLuong(IDHangHoa);
            gridSoLuong.DataBind();
        }
        public void LoadDanhSachCombo(string IDHangHoa)
        {
            data = new dtTraCuuMaHang();
            gridCombo.DataSource = data.DanhSachHangComBo(IDHangHoa);
            gridCombo.DataBind();
        }
        protected void btnTraCuu_Click(object sender, EventArgs e)
        {
            if (txtMaHangTraCuu.Text != "")
            {
                string MaHang = txtMaHangTraCuu.Value.ToString();
                if (dtSetting.IsNumber(MaHang) == true)
                {
                    Clear();
                    DataTable db = data.TraCuuMaHang(MaHang);
                    if (db.Rows.Count > 0)
                    {
                        txtSoLuongDangDat.Text = dtTraCuuMaHang.SoLuongDatHang(MaHang, Session["IDKho"].ToString());
                        DataRow dr = db.Rows[0];
                        cmbNhomHang.Value =  dr["IDNhomHang"].ToString();
                        txtMaHang.Text = dr["MaHang"].ToString();
                        txtTenHangHoa.Text = dr["TenHangHoa"].ToString();
                        cmbDonViTinh.Value = dr["IDDonViTinh"].ToString();
                        txtHeSo.Text = dr["HeSo"].ToString();
                        cmbHangSanXuat.Value = dr["IDHangSanXuat"].ToString();
                        cmbThue.Value = dr["IDThue"].ToString();
                        cmbNguoiDatHang.Value = dr["IDNhomDatHang"].ToString();
                        txtTrongLuong.Text = dr["TrongLuong"].ToString();
                        cmbTrangThaiHang.Value = dr["IDTrangThaiHang"].ToString();
                        txtHangSuDung.Text = dr["HanSuDung"].ToString();
                        txtGhiChu.Text = dr["GhiChu"].ToString();
                        string IDKho = Session["IDKho"].ToString();
                        txtTonKho.Text = dtCapNhatTonKho.SoLuong_TonKho(dtHangHoa.LayIDHangHoa_MaHang(MaHang), IDKho) + "";
                        // tính tần suất bán hàng trong 10 ngày.
                        txtTanSuatBanHang.Text = dtDonHangChiNhanh.TuanSuatBanHang(DateTime.Now, dtHangHoa.LayIDHangHoa_MaHang(MaHang), -dtSetting.LaySoNgayBanHang(), Session["IDKho"].ToString()) + "";
                        txtNganhHang.Text = dtTraCuuMaHang.TenNganhHang(dr["IDNhomHang"].ToString());
                        LoadDanhSachBarcode(dtHangHoa.LayIDHangHoa_MaHang(MaHang));
                        txtGiaBan0.Text = dtCapNhatTonKho.GiaBan_KhoChiNhanh(dtHangHoa.LayIDHangHoa_MaHang(MaHang), IDKho) + "";
                        txtGiaBan1.Text = dtCapNhatTonKho.GiaBan1_KhoChiNhanh(dtHangHoa.LayIDHangHoa_MaHang(MaHang), IDKho) + "";
                        txtGiaBan2.Text = dtCapNhatTonKho.GiaBan2_KhoChiNhanh(dtHangHoa.LayIDHangHoa_MaHang(MaHang), IDKho) + "";
                        txtGiaBan3.Text = dtCapNhatTonKho.GiaBan3_KhoChiNhanh(dtHangHoa.LayIDHangHoa_MaHang(MaHang), IDKho) + "";
                        txtGiaBan4.Text = dtCapNhatTonKho.GiaBan4_KhoChiNhanh(dtHangHoa.LayIDHangHoa_MaHang(MaHang), IDKho) + "";
                        txtGiaBan5.Text = dtCapNhatTonKho.GiaBan5_KhoChiNhanh(dtHangHoa.LayIDHangHoa_MaHang(MaHang), IDKho) + "";
                      //  LoadDanhSachHangQuiDoi(dtHangHoa.LayIDHangHoa_MaHang(MaHang));
                        LoadDanhSachGiaBanTheoSoLuong(dtHangHoa.LayIDHangHoa_MaHang(MaHang));
                        LoadDanhSachCombo(dtHangHoa.LayIDHangHoa_MaHang(MaHang));

                        // hàng hóa quy đổi
                        string IDHangQuiDoi =  dtTraCuuMaHang.LayIDHangQuiDoi(dtHangHoa.LayIDHangHoa_MaHang(MaHang));
                        if (Int32.Parse(IDHangQuiDoi) != 0)
                        {
                            DataTable dbQD = data.DanhSachHangQuiDoi(IDHangQuiDoi);
                            if (dbQD.Rows.Count > 0)
                            {
                                DataRow drQD = dbQD.Rows[0];
                                txtMaHangQD.Text = drQD["MaHang"].ToString();
                                txtTenHangHoaQD.Text = drQD["TenHangHoa"].ToString();
                                txtTrongLuongQD.Text = drQD["TrongLuong"].ToString();
                                txtTonKhoQD.Text = dtCapNhatTonKho.SoLuong_TonKho(IDHangQuiDoi, IDKho) + "";
                                cmbTrangThaiHangQD.Value = drQD["IDTrangThaiHang"].ToString();
                                cmbDVTQD.Value = drQD["IDDonViTinh"].ToString();
                            }
                        }
                        else
                        {
                            txtMaHangQD.Text = "";
                            txtTenHangHoaQD.Text = "";
                            txtTrongLuongQD.Text = "";
                            txtTonKhoQD.Text = "";
                            cmbTrangThaiHangQD.Text = "";
                            cmbDVTQD.Text = "";
                        }
                       
                    }
                    else
                    {
                        Clear();
                        Response.Write("<script language='JavaScript'> alert('Không tìm thấy mã hàng " + MaHang + " .'); </script>");
                        return;
                    }
                }
                else
                {
                    Clear();
                    Response.Write("<script language='JavaScript'> alert('Mã hàng phải là số.'); </script>");
                    return;
                }
            }
            else
            {
                Clear();
                Response.Write("<script language='JavaScript'> alert('Vui lòng nhập mã hàng cần tìm.'); </script>");
                return;
            }
        }
        public void Clear()
        {
            txtMaHangTraCuu.Text = "";
            txtMaHangTraCuu.Focus();
           // gridHangQuiDoi.DataSource = null;
            gridBarcode.DataSource = null;
            gridBarcode.DataBind();
            gridCombo.DataSource = null;
            gridCombo.DataBind();
            gridSoLuong.DataSource = null;
            gridSoLuong.DataBind();
            //gridHangQuiDoi.DataBind();
            cmbNhomHang.Text = "";
            txtMaHang.Text = "";
            txtTenHangHoa.Text = "";
            cmbDonViTinh.Text = "";
            txtHeSo.Text = "";
            cmbHangSanXuat.Text = "";
            cmbThue.Text = "";
            cmbNguoiDatHang.Text = "";
            txtTrongLuong.Text = "";
            cmbTrangThaiHang.Text = "";
            txtHangSuDung.Text = "";
            txtGiaBan5.Text = "";
            txtGiaBan4.Text = "";
            txtGiaBan3.Text = "";
            txtGiaBan2.Text = "";
            txtGiaBan1.Text = "";
            txtGiaBan0.Text = "";
            txtTanSuatBanHang.Text = "";
            txtTonKho.Text = "";
            txtNganhHang.Text = "";
            txtMaHangQD.Text = "";
            txtTenHangHoaQD.Text = "";
            txtTrongLuongQD.Text = "";
            txtTonKhoQD.Text = "";
            cmbTrangThaiHangQD.Text = "";
            cmbDVTQD.Text = "";
        }
    }
}