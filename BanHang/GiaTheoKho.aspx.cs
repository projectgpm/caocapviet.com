using BanHang.Data;
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
    public partial class GiaTheoVung : System.Web.UI.Page
    {
        dtGiaTheoVung data = new dtGiaTheoVung();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                if (dtSetting.LayChucNangCha(Session["IDNhom"].ToString(), 61) == false)
                    Response.Redirect("Default.aspx");
                if (!IsPostBack)
                {
                    ID_temp.Value = Session["IDNhanVien"].ToString();
                    DanhSachVung();
                }
                LoadGrid(ID_temp.Value.ToString());
            }
        }

        private void DanhSachVung()
        {
            data = new dtGiaTheoVung();
            DanhSachKho.DataSource = data.LayDanhSachKho();
            DanhSachKho.ValueField = "ID";
            DanhSachKho.TextField = "TenCuaHang";
            DanhSachKho.DataBind();
            DanhSachKho.SelectAll();

            dtVung dt1 = new dtVung();
            DataTable d2x = dt1.LayDanhSach();
            d2x.Rows.Add(-1,0 ,"Tất cả Vùng", 0);
            cmbVung.DataSource = d2x;
            cmbVung.ValueField = "ID";
            cmbVung.TextField = "TenVung";
            cmbVung.DataBind();
            cmbVung.SelectedIndex = cmbVung.Items.Count;
           
           
        }

        //private void LoadGrid(string IDKho, string HienThi)
        //{
        //      data = new dtGiaTheoVung();
        //      gridHangHoa.DataSource = data.DanhSachHangHoa_IDKho(IDKho, HienThi);
        //      gridHangHoa.DataBind();
        //}
        public void LoadGrid(string IDtemp)
        {
            data = new dtGiaTheoVung();
            gridHangHoa.DataSource = data.DanhSachTemp(IDtemp);
            gridHangHoa.DataBind();
        }
        protected void cmbVung_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbVung.Text != "")
            {
                int ID = Int32.Parse(cmbVung.Value.ToString());
                data = new dtGiaTheoVung();
                DanhSachKho.DataSource = data.LayDanhSachKho_IDVung(ID);
                DanhSachKho.ValueField = "ID";
                DanhSachKho.TextField = "TenCuaHang";
                DanhSachKho.DataBind();
                DanhSachKho.SelectAll();
            }
        }
       // protected void gridHangHoa_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        //{
        //    if (e.NewValues["GiaBan"] != null && e.NewValues["GiaBan1"] != null && e.NewValues["GiaBan2"] != null && e.NewValues["GiaBan3"] != null && e.NewValues["GiaBan4"] != null && e.NewValues["GiaBan5"] != null)
        //    {
        //        string ID = e.Keys[0].ToString();
        //        string IDDonViTinh = e.NewValues["IDDonViTinh"].ToString();
        //        string IDKho1 = cmbKho.Value.ToString();

        //        if (DanhSachKho.SelectedItems.Count != 0)
        //        {
        //            string IDHangHoa = dtCapNhatTonKho.LayIDHangHoa_HangHoaTonKho(ID).ToString();
        //            float giacu0 = dtCapNhatTonKho.GiaBan_KhoChiNhanh(IDHangHoa, IDKho1);
        //            float giacu1 = dtCapNhatTonKho.GiaBan1_KhoChiNhanh(IDHangHoa, IDKho1);
        //            float giacu2 = dtCapNhatTonKho.GiaBan2_KhoChiNhanh(IDHangHoa, IDKho1);
        //            float giacu3 = dtCapNhatTonKho.GiaBan3_KhoChiNhanh(IDHangHoa, IDKho1);
        //            float giacu4 = dtCapNhatTonKho.GiaBan4_KhoChiNhanh(IDHangHoa, IDKho1);
        //            float giacu5 = dtCapNhatTonKho.GiaBan5_KhoChiNhanh(IDHangHoa, IDKho1);
        //            float GiaBan0 = float.Parse(e.NewValues["GiaBan"].ToString());
        //            float GiaBan1 = float.Parse(e.NewValues["GiaBan1"].ToString());
        //            float GiaBan2 = float.Parse(e.NewValues["GiaBan2"].ToString());
        //            float GiaBan3 = float.Parse(e.NewValues["GiaBan3"].ToString());
        //            float GiaBan4 = float.Parse(e.NewValues["GiaBan4"].ToString());
        //            float GiaBan5 = float.Parse(e.NewValues["GiaBan5"].ToString());
        //            string IDNhanVien = Session["IDNhanVien"].ToString();
        //            string MaHang = dtHangHoa.LayMaHang(IDHangHoa);
        //            for (int i = DanhSachKho.Items.Count - 1; i >= 0; i--)
        //            {
        //                if (DanhSachKho.Items[i].Selected == true)
        //                {

        //                    // chưa lấy được giá bán củ
        //                    string IDKhoMoi = DanhSachKho.Items[i].Value.ToString();
        //                    if (giacu0 != GiaBan0)
        //                    {
        //                        dtThayDoiGia.ThemLichSu(MaHang, IDHangHoa, IDDonViTinh, giacu0.ToString(), GiaBan0.ToString(), IDNhanVien, "Thay đổi giá theo vùng(GiaBan): Chi Nhánh " + dtSetting.LayTenKho(IDKhoMoi));
        //                        data = new dtGiaTheoVung();
        //                        data.CapNhat_GiaTheoVung_NhiuKho(IDHangHoa, IDKhoMoi, GiaBan0.ToString(), "GiaBan");
        //                    }
        //                    if (giacu1 != GiaBan1)
        //                    {
        //                        dtThayDoiGia.ThemLichSu(MaHang, IDHangHoa, IDDonViTinh, giacu1.ToString(), GiaBan1.ToString(), IDNhanVien, "Thay đổi giá theo vùng(GiaBan1): Chi Nhánh " + dtSetting.LayTenKho(IDKhoMoi));
        //                        data = new dtGiaTheoVung();
        //                        data.CapNhat_GiaTheoVung_NhiuKho(IDHangHoa, IDKhoMoi, GiaBan1.ToString(), "GiaBan1");
        //                    }
        //                    if (giacu2 != GiaBan2)
        //                    {
        //                        dtThayDoiGia.ThemLichSu(MaHang, IDHangHoa, IDDonViTinh, giacu2.ToString(), GiaBan2.ToString(), IDNhanVien, "Thay đổi giá theo vùng(GiaBan2): Chi Nhánh " + dtSetting.LayTenKho(IDKhoMoi));
        //                        data = new dtGiaTheoVung();
        //                        data.CapNhat_GiaTheoVung_NhiuKho(IDHangHoa, IDKhoMoi, GiaBan2.ToString(), "GiaBan2");
        //                    }
        //                    if (giacu3 != GiaBan3)
        //                    {
        //                        dtThayDoiGia.ThemLichSu(MaHang, IDHangHoa, IDDonViTinh, giacu3.ToString(), GiaBan3.ToString(), IDNhanVien, "Thay đổi giá theo vùng(GiaBan3): Chi Nhánh " + dtSetting.LayTenKho(IDKhoMoi));
        //                        data = new dtGiaTheoVung();
        //                        data.CapNhat_GiaTheoVung_NhiuKho(IDHangHoa, IDKhoMoi, GiaBan3.ToString(), "GiaBan3");
        //                    }
        //                    if (giacu4 != GiaBan4)
        //                    {
        //                        dtThayDoiGia.ThemLichSu(MaHang, IDHangHoa, IDDonViTinh, giacu4.ToString(), GiaBan4.ToString(), IDNhanVien, "Thay đổi giá theo vùng(GiaBan4): Chi Nhánh " + dtSetting.LayTenKho(IDKhoMoi));
        //                        data = new dtGiaTheoVung();
        //                        data.CapNhat_GiaTheoVung_NhiuKho(IDHangHoa, IDKhoMoi, GiaBan4.ToString(), "GiaBan4");
        //                    }
        //                    if (giacu5 != GiaBan5)
        //                    {
        //                        dtThayDoiGia.ThemLichSu(MaHang, IDHangHoa, IDDonViTinh, giacu5.ToString(), GiaBan5.ToString(), IDNhanVien, "Thay đổi giá theo vùng(GiaBan5): Chi Nhánh " + dtSetting.LayTenKho(IDKhoMoi));
        //                        data = new dtGiaTheoVung();
        //                        data.CapNhat_GiaTheoVung_NhiuKho(IDHangHoa, IDKhoMoi, GiaBan5.ToString(), "GiaBan5");
        //                    }
        //                }
        //            }
        //            Session["updated"] = true;
        //            e.Cancel = true;
        //            gridHangHoa.CancelEdit();
        //            LoadGrid(cmbKho.Value.ToString(),cmbHienThi.Value.ToString());
        //        }
        //        else
        //        {
        //            throw new Exception("Lỗi: Chưa chọn kho để áp dụng giá mới?");
        //        }
            //}
            //else
            //{
            //    throw new Exception("Lỗi: Giá không được bỏ trống? Vui lòng kiểm tra lại.");
            //}
        //}

        //protected void gridHangHoa_CustomJSProperties(object sender, DevExpress.Web.ASPxGridViewClientJSPropertiesEventArgs e)
        //{
        //    if (Session["updated"] != null)
        //    {
        //        if (Session["updated"].ToString() == "True")
        //        {
        //            e.Properties["cpUpdated"] = true;
        //            Session["updated"] = null;
        //        }
        //    }
        //}
        protected void btnThem_Click(object sender, EventArgs e)
        {
            if (txtMaHang.Text != "" )
            {
                string MaHang = txtMaHang.Text.ToString().Trim();
                if (dtSetting.IsNumber(MaHang) == true)
                {
                    if (dtSetting.TraCuuMaHang(MaHang) == true)
                    {
                        if (DanhSachKho.SelectedItems.Count != 0)
                        {
                            string IDHangHoa = dtHangHoa.LayIDHangHoa_MaHang(MaHang);
                            string IDTemp = ID_temp.Value.ToString();
                            string IDDonViTinh = dtHangHoa.LayIDDonViTinh(IDHangHoa);
                            for (int i = DanhSachKho.Items.Count - 1; i >= 0; i--)
                            {
                                if (DanhSachKho.Items[i].Selected == true)
                                {
                                    string IDKhoApDung = DanhSachKho.Items[i].Value.ToString();
                                    float GiaBan = dtCapNhatTonKho.GiaBan_KhoChiNhanh(IDHangHoa, IDKhoApDung);
                                    float GiaBan1 = dtCapNhatTonKho.GiaBan1_KhoChiNhanh(IDHangHoa, IDKhoApDung);
                                    float GiaBan2 = dtCapNhatTonKho.GiaBan2_KhoChiNhanh(IDHangHoa, IDKhoApDung);
                                    float GiaBan3 = dtCapNhatTonKho.GiaBan3_KhoChiNhanh(IDHangHoa, IDKhoApDung);
                                    float GiaBan4 = dtCapNhatTonKho.GiaBan4_KhoChiNhanh(IDHangHoa, IDKhoApDung);
                                    float GiaBan5 = dtCapNhatTonKho.GiaBan5_KhoChiNhanh(IDHangHoa, IDKhoApDung);
                                    string GiaMuaSauthue = dtHangHoa.LayGiaMuaSauThue(IDHangHoa).ToString();
                                    data = new dtGiaTheoVung();
                                    DataTable db = data.KT_ThemChiTiet_Temp(IDHangHoa, IDTemp, IDKhoApDung);
                                    if (db.Rows.Count == 0)
                                    {
                                        data.ThemChiTiet_Temp(IDTemp, MaHang, IDHangHoa, IDDonViTinh, GiaBan.ToString(), GiaBan1.ToString(), GiaBan2.ToString(), GiaBan3.ToString(), GiaBan4.ToString(), GiaBan5.ToString(), IDKhoApDung, GiaMuaSauthue);
                                    }
                                    else
                                    {
                                        Response.Write("<script language='JavaScript'> alert('Mã hàng đã tại trong kho này.'); </script>");
                                    }
                                }
                            }
                            LoadGrid(IDTemp);
                        }
                        else
                            Response.Write("<script language='JavaScript'> alert('Vui lòng chọn kho để áp dụng.'); </script>");
                    }
                    else
                    {
                        txtMaHang.Text = "";
                        txtMaHang.Value = "";
                        txtMaHang.Focus();
                        Response.Write("<script language='JavaScript'> alert('Không tìm thấy mã hàng " + MaHang + " .'); </script>");
                        return;
                    }
                }
                else
                {
                    txtMaHang.Text = "";
                    txtMaHang.Value = "";
                    txtMaHang.Focus();
                    Response.Write("<script language='JavaScript'> alert('Mã hàng phải là số.'); </script>");
                    return;
                }
            }
            else
            {
                txtMaHang.Text = "";
                txtMaHang.Value = "";
                txtMaHang.Focus();
                Response.Write("<script language='JavaScript'> alert('Vui lòng nhập trường có dấu (*).'); </script>");
            }
        }

        protected void btnHuy_Click(object sender, EventArgs e)
        {
            data = new dtGiaTheoVung();
            data.XoaAll_temp(ID_temp.Value.ToString());
            Response.Redirect("DanhSachThayDoiGiaTheoVung.aspx");
        }

        protected void btnLuu_Click(object sender, EventArgs e)
        {
            data = new dtGiaTheoVung();
            DataTable db = data.DanhSachTemp(ID_temp.Value.ToString());
            if (db.Rows.Count > 0)
            {
                foreach (DataRow dr in db.Rows)
                {
                    string MaHang = dr["MaHang"].ToString();
                    string IDHangHoa = dr["IDHangHoa"].ToString();
                    string IDDonViTinh = dr["IDDonViTinh"].ToString();
                    string GiaBan = dr["GiaBan"].ToString();
                    string GiaBan1 = dr["GiaBan1"].ToString();
                    string GiaBan2 = dr["GiaBan2"].ToString();
                    string GiaBan3 = dr["GiaBan3"].ToString();
                    string GiaBan4 = dr["GiaBan4"].ToString();
                    string GiaBan5 = dr["GiaBan5"].ToString();
                    string IDKho = dr["IDKho"].ToString();
                    string GiaMuaSauThue = dr["GiaMuaSauThue"].ToString();
                    string IDNhanVien = Session["IDNhanVien"].ToString();
                    data = new dtGiaTheoVung();
                    data.CapNhat_GiaTheoVung_NhiuKho(IDHangHoa, IDKho, GiaBan.ToString(), "GiaBan");
                    data.CapNhat_GiaTheoVung_NhiuKho(IDHangHoa, IDKho, GiaBan1.ToString(), "GiaBan1");
                    data.CapNhat_GiaTheoVung_NhiuKho(IDHangHoa, IDKho, GiaBan2.ToString(), "GiaBan2");
                    data.CapNhat_GiaTheoVung_NhiuKho(IDHangHoa, IDKho, GiaBan3.ToString(), "GiaBan3");
                    data.CapNhat_GiaTheoVung_NhiuKho(IDHangHoa, IDKho, GiaBan4.ToString(), "GiaBan4");
                    data.CapNhat_GiaTheoVung_NhiuKho(IDHangHoa, IDKho, GiaBan5.ToString(), "GiaBan5");
                    data.ThemChiTietGiaTheoVung(MaHang, IDHangHoa, IDDonViTinh, GiaBan, GiaBan1, GiaBan2, GiaBan3, GiaBan4, GiaBan5, IDKho, IDNhanVien, GiaMuaSauThue);
                }
                data.XoaAll_temp(ID_temp.Value.ToString());
                Response.Redirect("DanhSachThayDoiGiaTheoVung.aspx");
            }
            else
            {
                txtMaHang.Text = "";
                txtMaHang.Focus();
                Response.Write("<script language='JavaScript'> alert('Danh sách thay đổi giá trống.'); </script>");
            }
        }

        protected void gridHangHoa_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            if (e.NewValues["GiaBan"] != null && e.NewValues["GiaBan1"] != null && e.NewValues["GiaBan2"] != null && e.NewValues["GiaBan3"] != null && e.NewValues["GiaBan4"] != null && e.NewValues["GiaBan5"] != null)
            {
                string GiaBan0 = e.NewValues["GiaBan"].ToString();
                string GiaBan1 = e.NewValues["GiaBan1"].ToString();
                string GiaBan2 = e.NewValues["GiaBan2"].ToString();
                string GiaBan3 = e.NewValues["GiaBan3"].ToString();
                string GiaBan4 = e.NewValues["GiaBan4"].ToString();
                string GiaBan5 = e.NewValues["GiaBan5"].ToString();
                data = new dtGiaTheoVung();
                data.CapNhatChiTiet_Temp(ID, GiaBan0, GiaBan1, GiaBan2, GiaBan3, GiaBan4, GiaBan5);
            }
            else
            {
                throw new Exception("Lỗi: Giá không được bỏ trống? Vui lòng kiểm tra lại.");
            }
            e.Cancel = true;
            gridHangHoa.CancelEdit();
            LoadGrid(ID_temp.Value.ToString());
        }

        protected void gridHangHoa_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            data = new dtGiaTheoVung();
            data.Xoa_temp(ID);
            e.Cancel = true;
            gridHangHoa.CancelEdit();
            LoadGrid(ID_temp.Value.ToString());
        }

        protected void gridHangHoa_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            Color color = (Color)ColorTranslator.FromHtml("#FF9797");
            double GiaMuaSauThue = Convert.ToDouble(e.GetValue("GiaMuaSauThue"));
            double GiaBan = Convert.ToDouble(e.GetValue("GiaBan"));
            double GiaBan1 = Convert.ToDouble(e.GetValue("GiaBan1"));
            double GiaBan2 = Convert.ToDouble(e.GetValue("GiaBan2"));
            double GiaBan3 = Convert.ToDouble(e.GetValue("GiaBan3"));
            double GiaBan4 = Convert.ToDouble(e.GetValue("GiaBan4"));
            double GiaBan5 = Convert.ToDouble(e.GetValue("GiaBan5"));
            if (GiaMuaSauThue > GiaBan || GiaMuaSauThue > GiaBan1 || GiaMuaSauThue > GiaBan2 || GiaMuaSauThue > GiaBan3 || GiaMuaSauThue > GiaBan4 || GiaMuaSauThue > GiaBan5)
                e.Row.BackColor = color;
        }
        
    }
}