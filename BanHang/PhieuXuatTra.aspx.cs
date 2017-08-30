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
    public partial class PhieuXuatTra : System.Web.UI.Page
    {
        dtPhieuXuatTra data = new dtPhieuXuatTra();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                if (dtSetting.LayTrangThaiMenu_ChucNang(Session["IDNhom"].ToString(), 18) == 1)
                    Response.Redirect("Default.aspx");
                if (dtSetting.LayTrangThaiMenu(Session["IDNhom"].ToString(), 18) == 1)
                {
                    if (!IsPostBack)
                    {
                        data = new dtPhieuXuatTra();
                        object IDPhieuXuatTra = data.ThemPhieuXuatTra_Temp();
                        IDPhieuXuatTra_Temp.Value = IDPhieuXuatTra.ToString();
                        cmbKho.Text = Session["IDKho"].ToString();
                        txtNguoiLapPhieu.Text = Session["TenDangNhap"].ToString();
                    }
                    LoadGrid(IDPhieuXuatTra_Temp.Value.ToString());
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }
       
        protected void cmbNgayLapPhieu_Init(object sender, EventArgs e)
        {
            cmbNgayLapPhieu.Date = DateTime.Today;
        }

        protected void cmbHangHoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbHangHoa.Text != "")
            {
                txtTonKho.Text = dtCapNhatTonKho.SoLuong_TonKho(cmbHangHoa.Value.ToString(),Session["IDKho"].ToString())+"";
                txtSoLuong.Text = "0";
                txtDonGia.Text = dtHangHoa.LayGiaMuaSauThue(cmbHangHoa.Value.ToString())+"";
                cmbDonViTinh.Value = dtHangHoa.LayIDDonViTinh(cmbHangHoa.Value.ToString());
            }  
        }

        protected void txtSoLuong_NumberChanged(object sender, EventArgs e)
        {
            //if (cmbHangHoa.Value != null)
            //{
            //    if (dtSetting.KT_ChuyenAm() == 0)
            //    {
            //        int SoLuong = Int32.Parse(txtSoLuong.Text.ToString());
            //        double GiaMua = double.Parse(txtDonGia.Text.ToString());
            //        txtThanhTien.Text = (SoLuong * GiaMua).ToString();

            //    }
            //    else
            //    {
            //        int SoLuong = Int32.Parse(txtSoLuong.Text.ToString());
            //        int IDHangHoa = Int32.Parse(cmbHangHoa.Value.ToString());
            //        int SLTOn = Int32.Parse(txtTonKho.Text);
            //        if (SoLuong > SLTOn)
            //        {
            //            Response.Write("<script language='JavaScript'> alert('Số lượng vượt quá cho phép.'); </script>");
            //            txtSoLuong.Text = SLTOn.ToString();
            //            double GiaMua = double.Parse(txtDonGia.Text.ToString());
            //            txtThanhTien.Text = (SLTOn * GiaMua).ToString();

            //        }
            //        else
            //        {
            //            double GiaMua = double.Parse(txtDonGia.Text.ToString());
            //            txtThanhTien.Text = (SoLuong * GiaMua).ToString();
            //        }

            //    }

            //}
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            if (cmbHangHoa.Text != "" && txtSoLuong.Text != "")
            {
                int SoLuong = Int32.Parse(txtSoLuong.Value.ToString());
                if (SoLuong > 0)
                {
                    int SLTon = Int32.Parse(txtTonKho.Text);
                    if (dtSetting.KT_ChuyenAm() == 0)
                    {
                        if (SLTon < SoLuong)
                        {
                            txtSoLuong.Text = SLTon.ToString();
                            Response.Write("<script language='JavaScript'> alert('Hàng hóa tồn kho không đủ.'); </script>");
                        }
                        else
                        {
                            string IDHangHoa = cmbHangHoa.Value.ToString();
                            string IDPhieuXuatTra = IDPhieuXuatTra_Temp.Value.ToString();
                            DataTable db = data.KTChiTietPhieuXuatTra_Temp(IDHangHoa, IDPhieuXuatTra);
                            float GiaMua = float.Parse(txtDonGia.Text.ToString());
                            if (db.Rows.Count == 0)
                            {
                                data = new dtPhieuXuatTra();
                                data.ThemChiTietPhieuXuatTra_Temp(IDPhieuXuatTra, IDHangHoa, cmbDonViTinh.Value.ToString(), SoLuong, GiaMua, (GiaMua * SoLuong), dtHangHoa.LayMaHang(IDHangHoa));
                                Clear();
                            }
                            else
                            {
                                data = new dtPhieuXuatTra();
                                data.UpdatePhieuXuatTra_temp(IDPhieuXuatTra, IDHangHoa, SoLuong, GiaMua, (SoLuong * GiaMua));                        
                                Clear();
                            }

                            LoadGrid(IDPhieuXuatTra);

                        }
                    }
                    else
                    {
                        string IDHangHoa = cmbHangHoa.Value.ToString();
                        string IDPhieuXuatTra = IDPhieuXuatTra_Temp.Value.ToString();
                        DataTable db = data.KTChiTietPhieuXuatTra_Temp(IDHangHoa, IDPhieuXuatTra);
                        float GiaMua = float.Parse(txtDonGia.Text.ToString());
                        if (db.Rows.Count == 0)
                        {
                            data = new dtPhieuXuatTra();
                            data.ThemChiTietPhieuXuatTra_Temp(IDPhieuXuatTra, IDHangHoa, cmbDonViTinh.Value.ToString(), SoLuong, GiaMua, (GiaMua * SoLuong), dtHangHoa.LayMaHang(IDHangHoa));
                            Clear();
                        }
                        else
                        {
                            data = new dtPhieuXuatTra();
                            data.UpdatePhieuXuatTra_temp(IDPhieuXuatTra, IDHangHoa, SoLuong, GiaMua, (SoLuong * GiaMua));
                            Clear();
                        }
                        if (SLTon < SoLuong)
                        {
                            Response.Write("<script language='JavaScript'> alert('Số hàng tồn trong kho hiện tại không đủ.'); </script>");
                        }
                        LoadGrid(IDPhieuXuatTra);
                    }
                }
                else
                {
                    Response.Write("<script language='JavaScript'> alert('Số Lượng phải > 0.'); </script>");
                }
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Bạn chưa chọn hàng hóa.'); </script>");
            }
            
        }

        private void LoadGrid(string IDPhieuXuatTra)
        {
            data = new dtPhieuXuatTra();
            gridDanhSachHangHoa_Temp.DataSource = data.LayDanhSachPhieuXuatTra_Temp(IDPhieuXuatTra);
            gridDanhSachHangHoa_Temp.DataBind();
        }

        protected void gridDanhSachHangHoa_Temp_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            int ID = Int32.Parse(e.Keys[0].ToString());
            data = new dtPhieuXuatTra();
            data.XoaChiTietPhieuXuatTra_Temp_ID(ID);
            e.Cancel = true;
            gridDanhSachHangHoa_Temp.CancelEdit();
            LoadGrid(IDPhieuXuatTra_Temp.Value.ToString());
        }

        protected void btnThemPhieuXuat_Click(object sender, EventArgs e)
        {
            if (cmbNhaCungCap.Text != "")
            {
                string IDPhieuXuatTra = IDPhieuXuatTra_Temp.Value.ToString();
                data = new dtPhieuXuatTra();
                DataTable db = data.LayDanhSachPhieuXuatTra_Temp(IDPhieuXuatTra);
                if (db.Rows.Count != 0)
                {
                    string IDNhaCungCap = cmbNhaCungCap.Value.ToString();
                    DateTime NgayLapPhieu = DateTime.Parse(cmbNgayLapPhieu.Text.ToString());
                    string GhiChu = txtGhiChu.Text == null ? "" : txtGhiChu.Text.ToString();
                    string IDNhanVien = Session["IDNhanVien"].ToString();
                    float TongTien = 0;
                    foreach (DataRow dr in db.Rows)
                    {
                        float ThanhTien = float.Parse(dr["ThanhTien"].ToString());
                        TongTien = TongTien + ThanhTien;
                    }
                    data = new dtPhieuXuatTra();
                    data.CapNhatPhieuXuatTra_ID(IDPhieuXuatTra, Session["IDKho"].ToString(), IDNhaCungCap, TongTien, NgayLapPhieu, GhiChu, IDNhanVien);
                    foreach (DataRow dr in db.Rows)
                    {
                        string IDHangHoa = dr["IDHangHoa"].ToString();
                        string SoLuong = dr["SoLuong"].ToString();
                        string GiaMua = dr["GiaMua"].ToString();
                        string IDDonViTinh = dr["IDDonViTinh"].ToString();
                        string ThanhTien = dr["ThanhTien"].ToString();
                        string MaHang = dr["MaHang"].ToString();
                        string SLTon = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa, Session["IDKho"].ToString()).ToString();
                        data = new dtPhieuXuatTra();
                        data.ThemChiTietPhieuXuatTra(IDPhieuXuatTra, IDHangHoa, IDDonViTinh, SoLuong, GiaMua, ThanhTien,MaHang,SLTon);
                        dtLichSuKho.ThemLichSu(IDHangHoa, Session["IDNhanVien"].ToString(), SoLuong, "Thêm Phiếu Xuất trả",Session["IDKho"].ToString());
                        dtCapNhatTonKho.TruTonKho(IDHangHoa, SoLuong, Session["IDKho"].ToString());
                    }
                    dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Phiếu Xuất Trả", Session["IDKho"].ToString(), "Nhập xuất tồn", "Thêm");
                    data = new dtPhieuXuatTra();
                    data.XoaChiTietPhieuXuatTra_Temp(IDPhieuXuatTra);
                    Response.Redirect("DanhSachPhieuXuatTra.aspx");
                }
                else
                {
                    Response.Write("<script language='JavaScript'> alert('Danh sách hàng hóa rỗng.'); </script>");
                }
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Vui lòng chọn Nhà cung cấp.'); </script>");
            }
        }

        protected void btnHuyPhieuXuat_Click(object sender, EventArgs e)
        {
            data = new dtPhieuXuatTra();
            int ID = Int32.Parse(IDPhieuXuatTra_Temp.Value.ToString());
            if (ID != null)
            {
                data.XoaPhieuXuatTra_Temp(ID);
                data.XoaChiTietPhieuXuatTra_Temp(IDPhieuXuatTra_Temp.Value.ToString());
                Response.Redirect("DanhSachPhieuXuatTra.aspx");
            }
        }
        public void Clear()
        {
            cmbHangHoa.Text = "";
            txtSoLuong.Text = "";
            txtTonKho.Text = "";
            cmbDonViTinh.Text = "";
            txtDonGia.Text = "";
        }

        protected void cmbHangHoa_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)source;

            sqlHangHoa.SelectCommand = @"SELECT [ID], [MaHang], [TenHangHoa], [GiaMuaSauThue] , [TenDonViTinh]
                                        FROM (
	                                        select GPM_HangHoa.ID, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa, GPM_HangHoa.GiaMuaSauThue, GPM_DonViTinh.TenDonViTinh, 
	                                        row_number()over(order by GPM_HangHoa.MaHang) as [rn] 
	                                        FROM GPM_DonViTinh INNER JOIN GPM_HangHoa ON GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh 
                                                               INNER JOIN GPM_HangHoaTonKho ON GPM_HangHoaTonKho.IDHangHoa = GPM_HangHoa.ID
	                                        WHERE (GPM_HangHoa.MaHang LIKE @MaHang OR  GPM_HangHoa.TenHangHoa LIKE @TenHang) AND (GPM_HangHoaTonKho.IDKho = @IDKho) AND (GPM_HangHoaTonKho.DaXoa = 0) AND (GPM_HangHoa.IDTrangThaiHang < 5)
	                                        ) as st 
                                        where st.[rn] between @startIndex and @endIndex";

            sqlHangHoa.SelectParameters.Clear();
            sqlHangHoa.SelectParameters.Add("MaHang", TypeCode.String, string.Format("%{0}%", e.Filter));
            sqlHangHoa.SelectParameters.Add("TenHang", TypeCode.String, string.Format("%{0}%", e.Filter));
            sqlHangHoa.SelectParameters.Add("IDKho", TypeCode.Int32, Session["IDKho"].ToString());
            sqlHangHoa.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString());
            sqlHangHoa.SelectParameters.Add("endIndex", TypeCode.Int64, (e.EndIndex + 1).ToString());
            comboBox.DataSource = sqlHangHoa;
            comboBox.DataBind();
        }

        protected void cmbHangHoa_ItemRequestedByValue(object source, DevExpress.Web.ListEditItemRequestedByValueEventArgs e)
        {
            long value = 0;
            if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;
            sqlHangHoa.SelectCommand = @"SELECT GPM_HangHoa.ID, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa, GPM_HangHoa.GiaMuaSauThue, GPM_DonViTinh.TenDonViTinh 
                                        FROM GPM_DonViTinh INNER JOIN GPM_HangHoa ON GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh 
                                                           INNER JOIN GPM_HangHoaTonKho ON GPM_HangHoaTonKho.IDHangHoa = GPM_HangHoa.ID 
                                        WHERE (GPM_HangHoa.ID = @ID) AND (GPM_HangHoa.IDTrangThaiHang < 5)";

            sqlHangHoa.SelectParameters.Clear();
            sqlHangHoa.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            comboBox.DataSource = sqlHangHoa;
            comboBox.DataBind();
        }
    }
}