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
    public partial class ThemHangHoaCombo : System.Web.UI.Page
    {
        dtHangCombo data = new dtHangCombo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                if (dtSetting.LayTrangThaiMenu_ChucNang(Session["IDNhom"].ToString(), 8) == 1)
                    Response.Redirect("Default.aspx");
                if (dtSetting.LayTrangThaiMenu(Session["IDNhom"].ToString(), 8) == 1)
                {
                    if (!IsPostBack)
                    {
                        data = new dtHangCombo();
                        //data.XoaHangHoa_Null();
                        object IDHangHoaComBo = data.ThemIDHangHoa_Temp();
                        IDHangHoaComBo_Temp.Value = IDHangHoaComBo.ToString();
                        txtSoLuong.Text = "0";
                        txtMaHang.Text = "121212";
                    }
                    LoadGrid(Int32.Parse(IDHangHoaComBo_Temp.Value.ToString()));
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }

        protected void btnHuy_Click(object sender, EventArgs e)
        {
            data = new dtHangCombo();
            int IDHangHoaComBo = Int32.Parse(IDHangHoaComBo_Temp.Value.ToString());
            data.XoaHangHoa_Temp_IDHangCombo(IDHangHoaComBo);
            //data.XoaHangHoa_Null();
            Response.Redirect("DanhMucCombo.aspx");
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            //if (txtMaHang.Text != "" && cmbDonViTinh.Text != "" && txtTenHangHoa.Text != "" && txtGiaBan.Text != "" && txtBarcode.Text != "" && txtTrongLuong.Text != "" && txtHanSuDung.Text !="")
            //{
            //    data = new dtHangCombo();
            //    int IDHangHoaComBo = Int32.Parse(IDHangHoaComBo_Temp.Value.ToString());
            //    DataTable dt = data.DanhSachHangHoaCombo_Temp(IDHangHoaComBo);
            //    if (dt.Rows.Count != 0)
            //    {
            //        string MaHang = txtMaHang.Text.Trim();
            //        string txtTenHangComBo = txtTenHangHoa.Text.ToString();
            //        if (dtSetting.kiemTraChuyenDoiDau() == 1)
            //            txtTenHangComBo = dtSetting.convertDauSangKhongDau(txtTenHangComBo).ToUpper();
            //        int IDDonViTinh = Int32.Parse(cmbDonViTinh.Value.ToString());
            //        float GiaBan = float.Parse(txtGiaBan.Text.ToString());
            //        int SL_ComBo = Int32.Parse(txtSLCombo.Text);
            //        string barcode = txtBarcode.Text.Trim();

            //        string TrongLuong = txtTrongLuong.Text;
            //        string HanSuDung = txtHanSuDung.Text;
            //        string GhiChu = txtGhiChu.Text == null ? "" : txtGhiChu.Text.ToString();
            //        if ((dtHangHoa.KiemTraMaHang(MaHang))  ==  false)
            //        {
            //            data = new dtHangCombo();
            //            data.CapNhatHangHoa(IDHangHoaComBo, MaHang, IDDonViTinh, txtTenHangComBo, GiaBan, TrongLuong, HanSuDung, GhiChu);

            //            data.ThemHangVaoTonKho(Session["IDKho"].ToString(), IDHangHoaComBo, SL_ComBo, GiaBan);
            //            data.ThemBarCode(IDHangHoaComBo, barcode);

            //            //Thêm hàng hóa vào các kho....
            //            DataTable dta = data.LayDanhSachKho();
            //            for (int i = 0; i < dta.Rows.Count; i++)
            //            {
            //                DataRow dr = dta.Rows[i];
            //                int IDKho = Int32.Parse(dr["ID"].ToString());
            //                data = new dtHangCombo();
            //                data.ThemHangVaoTonKho(IDKho, (int)IDHangHoaComBo, 0, GiaBan);
            //            }

            //            foreach (DataRow dr in dt.Rows)
            //            {
            //                string IDHangHoa1 = dr["IDHangHoa"].ToString();
            //                int SoLuong1 = Int32.Parse(dr["SoLuong"].ToString());
            //                float GiaBan1 = float.Parse(dr["GiaBan"].ToString());
            //                float ThanhTien1 = float.Parse(dr["ThanhTien"].ToString());
            //                string MaHang1 = dr["MaHang"].ToString();
            //                string IDDonViTinh1 = dr["IDDonViTinh"].ToString();
            //                string TrongLuong1 = dr["TrongLuong"].ToString();
            //                data = new dtHangCombo();
            //                data.ThemHangHoa(IDHangHoaComBo, IDHangHoa1, SoLuong1, GiaBan1, ThanhTien1, MaHang1, IDDonViTinh1, TrongLuong1);
            //                //trừ tồn kho
            //                dtCapNhatTonKho.TruTonKho(IDHangHoa1, (SoLuong1 * SL_ComBo).ToString(), Session["IDKho"].ToString());
            //            }
            //            data.XoaHangHoa_Temp_IDHangCombo(IDHangHoaComBo);
            //            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Hàng hóa combo", Session["IDKho"].ToString(), "Danh Mục", "Thêm");
            //            Response.Redirect("DanhMucCombo.aspx");
            //        }
            //        else
            //        {
            //            Response.Write("<script language='JavaScript'> alert('Mã hàng đã tồn tại.'); </script>");
            //        }
            //    }
            //    else
            //    {
            //        Response.Write("<script language='JavaScript'> alert('Danh sách hàng hóa combo rỗng.'); </script>");
            //    }
            //}
            //else
            //{
            //    Response.Write("<script language='JavaScript'> alert('Vui lòng điền đầy đủ thoog tin hàng hóa combo.'); </script>");
            //}
        }
        public void TinhTongTien()
        {
            data = new dtHangCombo();
            DataTable db = data.DanhSachHangHoaCombo_Temp(Int32.Parse(IDHangHoaComBo_Temp.Value.ToString()));
            if (db.Rows.Count != 0)
            {
                double TongTien = 0;
                foreach (DataRow dr in db.Rows)
                {
                    double ThanhTien = double.Parse(dr["ThanhTien"].ToString());
                    TongTien = TongTien + ThanhTien;
                }
                txtGiaBan.Text = (TongTien).ToString();
                TinhTrongLuong();
            }
            else
            {
                txtGiaBan.Text = "0";
            }
        }
        public void TinhTrongLuong()
        {
            data = new dtHangCombo();
            DataTable db = data.DanhSachHangHoaCombo_Temp(Int32.Parse(IDHangHoaComBo_Temp.Value.ToString()));
            if (db.Rows.Count != 0)
            {
                double Tong = 0;
                foreach (DataRow dr in db.Rows)
                {
                    double TrongLuong = double.Parse(dr["TrongLuong"].ToString());
                    Tong = Tong + TrongLuong;
                }
                txtTrongLuong.Text = (Tong).ToString();
            }
            else
            {
                txtTrongLuong.Text = "0";
            }
        }
        protected void btnThem_Temp_Click(object sender, EventArgs e)
        {
            if (txtSoLuong.Text != "" && cmbHangHoa.Text != "")
            {
                int SL = Int32.Parse(txtSoLuong.Text);
                int SLTonKho = Int32.Parse(txtTonKho.Text);
                if (SL > 0)
                {

                    int IDHangHoaComBo = Int32.Parse(IDHangHoaComBo_Temp.Value.ToString());
                    float GiaBanSauThue = dtHangHoa.LayGiaBanSauThue(cmbHangHoa.Value.ToString());
                    float GiaBanTruocThue = dtHangHoa.LayGiaBanTruocThue(cmbHangHoa.Value.ToString());
                    float GiaMuaSauThue = dtHangHoa.LayGiaMuaSauThue(cmbHangHoa.Value.ToString());
                    float GiaMuaTruocThue = dtHangHoa.LayGiaMuaTruocThue(cmbHangHoa.Value.ToString());
                    string MaHang = dtHangHoa.LayMaHang(cmbHangHoa.Value.ToString());
                    string IDDonViTinh = dtHangHoa.LayIDDonViTinh(cmbHangHoa.Value.ToString());
                    float TrongLuong = dtHangHoa.LayTrongLuong(cmbHangHoa.Value.ToString());
                    string GhiChu = txtGhiChuHangHoa.Text == null ? "" : txtGhiChuHangHoa.Text.ToString();
                    data = new dtHangCombo();
                    DataTable db = data.KTHangHoa_Temp(cmbHangHoa.Value.ToString());// kiểm tra hàng hóa
                    float tong = SL * TrongLuong;
                    if (db.Rows.Count == 0)
                    {
                        data = new dtHangCombo();
                        data.ThemHangHoa_Temp(IDHangHoaComBo, cmbHangHoa.Value.ToString(), SL, GiaBanSauThue, SL * GiaBanSauThue, MaHang, IDDonViTinh, tong.ToString(), GiaBanSauThue, GiaMuaTruocThue, GiaMuaSauThue, GhiChu);
                        TinhTongTien();
                        Clear();
                    }
                    else
                    {
                        data = new dtHangCombo();
                        data.UpdateHangHoa_temp(IDHangHoaComBo, cmbHangHoa.Value.ToString(), SL, GiaBanSauThue, SL * GiaBanSauThue, MaHang, IDDonViTinh, tong.ToString(), GhiChu);
                        TinhTongTien();
                        Clear();
                    }
                    LoadGrid(IDHangHoaComBo);
                }
                else
                {
                    Response.Write("<script language='JavaScript'> alert('Số lượng > 0.'); </script>");
                }
            }
            else
            {
                Response.Write("<script language='JavaScript'> alert('Bạn chưa chọn hàng hóa hoặc số lượng.'); </script>");
            }
        }
        public void Clear()
        {
            txtTL.Text = "";
            cmbHangHoa.Text = "";
            txtSoLuong.Text = "0";
            txtTonKho.Text = "";
            txtGiaBanST.Text = "0";
            txtGhiChuHangHoa.Text = "";
        }

        protected void cmbHangHoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbHangHoa.Text != "")
            {
                txtTL.Text = dtHangHoa.LayTrongLuong(cmbHangHoa.Value.ToString()) + "";
                txtTonKho.Text = dtCapNhatTonKho.SoLuong_TonKho(cmbHangHoa.Value.ToString(), Session["IDKho"].ToString()) + "";
                txtGiaBanST.Text = dtHangHoa.LayGiaBanSauThue(cmbHangHoa.Value.ToString()).ToString();
            }
            else
            {
                txtTonKho.Text = "";
                txtSoLuong.Text = "";
                Response.Write("<script language='JavaScript'> alert('Bạn chưa chọn hàng hóa.'); </script>");
            }
        }
        private void LoadGrid(int IDHangHoaComBo)
        {
            data = new dtHangCombo();
            gridDanhSachHangHoa.DataSource = data.DanhSachHangHoaCombo_Temp(IDHangHoaComBo);
            gridDanhSachHangHoa.DataBind();

        }
        
        protected void cmbHangHoa_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)source;

            dsHangHoa.SelectCommand = @"SELECT [ID], [MaHang], [TenHangHoa], [GiaBanSauThue] , [TenDonViTinh]
                                        FROM (
	                                        select GPM_HangHoa.ID, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa,GPM_HangHoa.GiaBanSauThue, GPM_DonViTinh.TenDonViTinh, 
	                                        row_number()over(order by GPM_HangHoa.MaHang) as [rn] 
	                                        FROM GPM_DonViTinh INNER JOIN GPM_HangHoa ON GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh 
                                                              
	                                        WHERE ((GPM_HangHoa.TenHangHoa LIKE @TenHang) OR (GPM_HangHoa.MaHang LIKE @MaHang)) AND (GPM_HangHoa.DaXoa = 0) AND  (GPM_HangHoa.IDTrangThaiHang = 1 OR GPM_HangHoa.IDTrangThaiHang = 3)
	                                        ) as st 
                                        where st.[rn] between @startIndex and @endIndex";

            dsHangHoa.SelectParameters.Clear();
            dsHangHoa.SelectParameters.Add("TenHang", TypeCode.String, string.Format("%{0}%", e.Filter));
            dsHangHoa.SelectParameters.Add("MaHang", TypeCode.String, string.Format("%{0}%", e.Filter));
            dsHangHoa.SelectParameters.Add("IDKho", TypeCode.Int32, Session["IDKho"].ToString());
            dsHangHoa.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString());
            dsHangHoa.SelectParameters.Add("endIndex", TypeCode.Int64, (e.EndIndex + 1).ToString());
            comboBox.DataSource = dsHangHoa;
            comboBox.DataBind();
        }

        protected void cmbHangHoa_ItemRequestedByValue(object source, DevExpress.Web.ListEditItemRequestedByValueEventArgs e)
        {
            long value = 0;
            if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;
            dsHangHoa.SelectCommand = @"SELECT GPM_HangHoa.ID, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa, GPM_HangHoa.GiaBanSauThue, GPM_DonViTinh.TenDonViTinh 
                                        FROM GPM_DonViTinh INNER JOIN GPM_HangHoa ON GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh
                                        WHERE (GPM_HangHoa.ID = @ID) AND  (GPM_HangHoa.IDTrangThaiHang = 1 OR GPM_HangHoa.IDTrangThaiHang = 3)";

            dsHangHoa.SelectParameters.Clear();
            dsHangHoa.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            comboBox.DataSource = dsHangHoa;
            comboBox.DataBind();
        }

     
        protected void BtnXoaHang_Click(object sender, EventArgs e)
        {
            string ID = (((ASPxButton)sender).CommandArgument).ToString();
            string IDHangHoaComBo = IDHangHoaComBo_Temp.Value.ToString();
            data = new dtHangCombo();
            data.XoaHangHoa_Temp_ID(ID);
            TinhTrongLuong();
            TinhTongTien();
            LoadGrid(Int32.Parse(IDHangHoaComBo));
        }
    }
}