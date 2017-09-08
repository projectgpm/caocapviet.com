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
                    txtNguoiDuyet.Text = Session["TenDangNhap"].ToString();
                    btnThem.Enabled = false;
                }
                if (cmbSoDonHang.Text != "")
                {
                    LoadDanhSach(cmbSoDonHang.Value.ToString(), IDDonHangDuyet_Temp.Value.ToString());
                }
            }
        }

        public void LoadDanhSach(string IDDonHangThuMua, string IDTemp)
        {
            data = new dtDuyetDonHangThuMua();
            gridDanhSachHangHoa.DataSource = data.DanhSachChiTiet_Temp(IDDonHangThuMua, IDTemp);
            gridDanhSachHangHoa.DataBind();
        }
        public double TinhTongTien()
        {
            if (cmbSoDonHang.Text != "")
            {
                data = new dtDuyetDonHangThuMua();
                DataTable db = data.DanhSachChiTiet_Temp(cmbSoDonHang.Value.ToString(), IDDonHangDuyet_Temp.Value.ToString());
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
                DataTable db = data.DanhSachChiTiet_Temp(cmbSoDonHang.Value.ToString(), IDDonHangDuyet_Temp.Value.ToString());
                if (db.Rows.Count != 0)
                {
                    double Tong = 0;
                    foreach (DataRow dr in db.Rows)
                    {
                        double TrongLuong = double.Parse(dr["TrongLuong"].ToString());
                        Tong = Tong + (TrongLuong);
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
                data = new dtDuyetDonHangThuMua();
                data.Xoa_Temp_ID(IDDonHangDuyet_Temp.Value.ToString());
                Random ran = new Random();
                int Temp = ran.Next(1000, 9999);
                IDDonHangDuyet_Temp.Value = Temp.ToString();
                string ID = cmbSoDonHang.Value.ToString();
                DataTable db = data.LayDanhSachDonHang_ID(ID);
                if (db.Rows.Count > 0)
                {
                    DataRow dr = db.Rows[0];
                    cmbNguoiLap.Value = dr["IDNguoiLap"].ToString();
                    txtNgayDatHang.Date = DateTime.Parse(dr["NgayDat"].ToString());
                    cmbChiNhanhLap.Value = dr["IDKhoLap"].ToString();
                    txtGhiChu.Text = dr["GhiChu"].ToString();
                    cmbNhaCungCap.Value = dr["IDNhaCungCap"].ToString();
                    string IDTemp = IDDonHangDuyet_Temp.Value.ToString();
                    int TrangThaiDonHang = Int32.Parse(dr["IDTrangThaiDonHang"].ToString());
                    if (TrangThaiDonHang != 4)
                    {
                        //đơn hàng chưa xử lý lần nào
                        DataTable dt = data.DanhSachChiTiet(ID);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in dt.Rows)
                            {
                                string MaHang = dr1["MaHang"].ToString();
                                string IDHangHoa = dr1["IDHangHoa"].ToString();
                                string IDDonViTinh = dr1["IDDonViTinh"].ToString();
                                string TrongLuong = dr1["TrongLuong"].ToString();
                                string SoLuong = dr1["SoLuong"].ToString();
                                string GhiChu = dr1["GhiChu"].ToString();
                                data = new dtDuyetDonHangThuMua();
                                DataTable dbt = data.KTChiTietDonHang_Temp(IDHangHoa, ID, IDTemp);
                                if (dbt.Rows.Count == 0)
                                {
                                    data.ThemChiTietDonHang_Temp(ID, MaHang, IDHangHoa, IDDonViTinh, TrongLuong, SoLuong, GhiChu, IDTemp);
                                }
                            }
                            LoadDanhSach(ID, IDTemp);
                        }
                        else
                        {
                            Response.Write("<script language='JavaScript'> alert('Danh sách hàng hóa trống.'); </script>");
                            return;
                        }
                    }
                    else
                    {
                        //đơn hàng đã xử lý 1 phần. lấy dữ liệu trong bảng log
                        data = new dtDuyetDonHangThuMua();
                        DataTable dt = data.DanhSachChiTiet_LOG(cmbSoDonHang.Text.ToString().Trim(), cmbSoDonHang.Value.ToString());
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr1 in dt.Rows)
                            {
                                string MaHang = dr1["MaHang"].ToString();
                                string IDHangHoa = dr1["IDHangHoa"].ToString();
                                string IDDonViTinh = dr1["IDDonViTinh"].ToString();
                                string TrongLuong = dr1["TrongLuong"].ToString();
                                string SoLuong = dr1["SoLuong"].ToString();
                                string GhiChu = dr1["GhiChu"].ToString();
                                data = new dtDuyetDonHangThuMua();
                                data.ThemChiTietDonHang_Temp(ID, MaHang, IDHangHoa, IDDonViTinh, TrongLuong, SoLuong, GhiChu, IDTemp);
                            }
                            LoadDanhSach(ID, IDTemp);
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
                    return;
                }
            }
        }

        protected void gridDanhSachHangHoa_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            if (cmbSoDonHang.Text != "")
            {
                if (e.NewValues["ThucTe"] == null)
                {
                    throw new Exception("Lỗi: Số lượng thực tế không được bỏ trống. ");
                }
                else
                {
                    string ID = e.Keys[0].ToString();
                    int SoLuongThucTe = Int32.Parse(e.NewValues["ThucTe"].ToString());
                    int SoLuong = Int32.Parse(e.NewValues["SoLuong"].ToString());
                    string IDTemp = IDDonHangDuyet_Temp.Value.ToString();
                    if (SoLuongThucTe >= 0)
                    {
                        if (SoLuongThucTe > SoLuong)
                        {
                            throw new Exception("Lỗi: Số lượng thực tế phải nhỏ hơn hoặc bằng Số lượng đặt ");
                        }
                        else
                        {
                            string MaHang = e.NewValues["MaHang"].ToString();
                            string IDHangHoa = dtHangHoa.LayIDHangHoa_MaHang(MaHang.Trim());
                            data = new dtDuyetDonHangThuMua();
                            data.CapNhatChiTietDonHang(ID, IDHangHoa, IDTemp, SoLuongThucTe, SoLuong - SoLuongThucTe);
                        }
                    }
                    else
                    {
                        throw new Exception("Lỗi: Số lượng không được âm? Vui lòng kiểm tra lại. ");
                    }
                    e.Cancel = true;
                    gridDanhSachHangHoa.CancelEdit();
                    LoadDanhSach(cmbSoDonHang.Value.ToString(), IDDonHangDuyet_Temp.Value.ToString());
                }
            }
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {

        }

        protected void btnHuy_Click(object sender, EventArgs e)
        {
            string IDTemp = IDDonHangDuyet_Temp.Value.ToString();
            data = new dtDuyetDonHangThuMua();
            data.Xoa_Temp_ID(IDTemp);
            Response.Redirect("DanhSachPhieuDatHang.aspx");
        }

        protected void txtNgayDuyet_Init(object sender, EventArgs e)
        {
            txtNgayDuyet.Date = DateTime.Now;
        }

        protected void txtNgayGiao_Init(object sender, EventArgs e)
        {
            txtNgayGiao.Date = DateTime.Now;
        }
    }
}