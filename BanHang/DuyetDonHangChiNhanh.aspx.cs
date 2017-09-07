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
                    data = new dtDuyetDonHangChiNhanh();
                    //data.Xoa_ALL_Temp();
                    txtNguoiDuyet.Text = Session["TenDangNhap"].ToString();
                    cmbChiNhanhDuyet.Value = Session["IDKho"].ToString();
                    cmbHangHoa.Enabled = false;
                    txtGhiChuHangHoa.Enabled = false;
                    btnThem_Temp.Enabled = false;
                    txtSoLuong.Enabled = false;
                }
                if (cmbSoDonHang.Text != "")
                {
                    string IDTemp = IDDonHangDuyet_Temp.Value.ToString();
                    string IDDonHangChiNhanh = cmbSoDonHang.Value.ToString();
                    LoadDanhSach(IDDonHangChiNhanh, IDTemp);
                }
            }
        }
        public void LoadDanhSach(string SoDonHang, string IDTemp)
        {
            data = new dtDuyetDonHangChiNhanh();
            gridDanhSachHangHoa.DataSource = data.DanhSachChiTiet_Temp(SoDonHang, IDTemp, Session["IDKho"].ToString());
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
                // lây trạng thái = 1 mới được xóa
                int TrangThai  = dtDuyetDonHangChiNhanh.LayTrangThai(ID);
                if (TrangThai == 1)
                {
                    data.Xoa_Temp(ID);
                }
                else
                {
                    throw new Exception("Lỗi: Chỉ xóa những cái Kho mới thêm? Vui lòng kiểm tra lại.  ");
                }
                e.Cancel = true;
                gridDanhSachHangHoa.CancelEdit();
                LoadDanhSach(cmbSoDonHang.Value.ToString(), IDDonHangDuyet_Temp.Value.ToString());
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
                    int TonKhoTong = Int32.Parse(e.NewValues["TonKhoTong"].ToString());
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
                            data = new dtDuyetDonHangChiNhanh();
                            string IDTemp = IDDonHangDuyet_Temp.Value.ToString();
                            data.CapNhatChiTietDonHang(ID, IDHangHoa, IDTemp, SoLuongThucTe, SoLuong - SoLuongThucTe);
                        }
                        if (SoLuongThucTe > TonKhoTong)
                        {
                            // cập nhật ghi chú
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
       
        public double TinhTrongLuong()
        {
            if (cmbSoDonHang.Text != "")
            {
                data = new dtDuyetDonHangChiNhanh();
                DataTable db = data.DanhSachChiTiet_Temp(cmbSoDonHang.Value.ToString(), IDDonHangDuyet_Temp.Value.ToString(), Session["IDKho"].ToString());
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
                data = new dtDuyetDonHangChiNhanh();
                //data.Xoa_ALL_Temp();
                cmbHangHoa.Enabled = true;
                txtGhiChuHangHoa.Enabled = true;
                btnThem_Temp.Enabled = true;
                txtSoLuong.Enabled = true;
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
                    cmbChiNhanhLap.Value = dr["IDKho"].ToString();
                    txtGhiChu.Text = dr["GhiChu"].ToString();
                    DataTable dt = data.DanhSachChiTiet(ID);
                    string IDTemp = IDDonHangDuyet_Temp.Value.ToString();
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dt.Rows)
                        {
                            string MaHang = dr1["MaHang"].ToString();
                            string IDHangHoa = dr1["IDHangHoa"].ToString();
                            string IDDonViTinh = dr1["IDDonViTinh"].ToString();
                            string TrongLuong = dr1["TrongLuong"].ToString();
                            string SoLuong = dr1["SoLuong"].ToString();
                            string TonKho = dr1["TonKho"].ToString();
                            string GhiChu = dr1["GhiChu"].ToString();
                            string TrangThai = dr1["TrangThai"].ToString();
                            string IDDonHangChiNhanh = dr1["IDDonHangChiNhanh"].ToString();
                            string IDKho = dr1["IDKho"].ToString();
                            data = new dtDuyetDonHangChiNhanh();
                            DataTable dbt = data.KTChiTietDonHang_Temp(IDHangHoa, IDDonHangChiNhanh, IDTemp);
                            if (dbt.Rows.Count == 0)
                            {
                                data.ThemChiTietDonHang_Temp(ID, MaHang, IDHangHoa, IDDonViTinh, TrongLuong, SoLuong, TonKho, GhiChu, TrangThai, IDKho, IDTemp);
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
                    Response.Write("<script language='JavaScript'> alert('Thông tin đơn hàng rỗng.'); </script>");
                    return;
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
            string IDTemp = IDDonHangDuyet_Temp.Value.ToString();
            data = new dtDuyetDonHangChiNhanh();
            data.Xoa_Temp_ID(IDTemp);
            Response.Redirect("DonDatHangChiNhanh.aspx");
        }
        protected void txtNgayGiao_Init(object sender, EventArgs e)
        {
            txtNgayGiao.Date = DateTime.Today;
        }

        protected void gridDanhSachHangHoa_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
        {
            Color color = (Color)ColorTranslator.FromHtml("#FF9797");
            int TrangThai = Convert.ToInt32(e.GetValue("TrangThai"));
            if (TrangThai == 1)
                e.Row.BackColor = color;

            int ThucTe = Convert.ToInt32(e.GetValue("ThucTe"));
            float TonKhoTong = Convert.ToInt32(e.GetValue("TonKhoTong"));
            if(ThucTe > TonKhoTong)
                e.Row.BackColor = color;
        }

        protected void btnThem_Temp_Click(object sender, EventArgs e)
        {
            int SoLuong = Int32.Parse(txtSoLuong.Text.ToString());
            string IDTemp = IDDonHangDuyet_Temp.Value.ToString();
            string IDDonHangChiNhanh = cmbSoDonHang.Value.ToString();
            if (SoLuong > 0)
            {
                string IDHangHoa = cmbHangHoa.Value.ToString();
                string IDDonViTinh = dtHangHoa.LayIDDonViTinh(IDHangHoa);
                string MaHang = dtHangHoa.LayMaHang(IDHangHoa);
                float TrongLuong = dtHangHoa.LayTrongLuong(IDHangHoa);
                string GhiChu = txtGhiChuHangHoa.Text == null ? "" : txtGhiChuHangHoa.Text.ToString();
                data = new dtDuyetDonHangChiNhanh();
                DataTable dbt = data.KTChiTietDonHang_Temp(IDHangHoa, IDDonHangChiNhanh, IDTemp);
                if (dbt.Rows.Count == 0)
                {
                    string IDKho = "1";
                    DataTable db = data.LayDanhSachDonHang_ID(IDDonHangChiNhanh);
                    if (db.Rows.Count > 0)
                    {
                        DataRow dr = db.Rows[0];
                        IDKho = dr["IDKho"].ToString();
                    }
                    data.ThemMoiChiTiet(IDDonHangChiNhanh, MaHang, IDHangHoa, IDDonViTinh, (TrongLuong * SoLuong).ToString(), SoLuong.ToString(), "0", GhiChu, "1", IDKho, IDTemp, "0", SoLuong.ToString());
                }
                else
                {
                    Response.Write("<script language='JavaScript'> alert('Hàng hóa đã tồn tại.Vui lòng kiểm tra lại? '); </script>");
                    cmbHangHoa.Text = "";
                    txtTonKho.Text = "";
                    txtSoLuong.Text = "";
                    txtGhiChuHangHoa.Text = "";
                    LoadDanhSach(IDDonHangChiNhanh, IDTemp);
                    return;
                }
                LoadDanhSach(IDDonHangChiNhanh, IDTemp);
                cmbHangHoa.Text = "";
                txtTonKho.Text = "";
                txtSoLuong.Text = "";
                txtGhiChuHangHoa.Text = "";
            }
            else
            {
                LoadDanhSach(IDDonHangChiNhanh, IDTemp);
                Response.Write("<script language='JavaScript'> alert('Số Lượng phải > 0.'); </script>");
                return;
            }
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
            dsHangHoa.SelectParameters.Add("IDKho", TypeCode.Int32, Session["IDKho"].ToString());
            dsHangHoa.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString());
            dsHangHoa.SelectParameters.Add("endIndex", TypeCode.Int64, (e.EndIndex + 1).ToString());
            comboBox.DataSource = dsHangHoa;
            comboBox.DataBind();
        }

        protected void cmbHangHoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbHangHoa.Text != "")
            {
                int TonKho = dtCapNhatTonKho.SoLuong_TonKho(cmbHangHoa.Value.ToString(), Session["IDKho"].ToString());
                txtTonKho.Text = TonKho.ToString();
                txtSoLuong.Text = "0";
            }
        }

       
    }
}