﻿using BanHang.Data;
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
    public partial class KiemKho : System.Web.UI.Page
    {
        dtKiemKho data = new dtKiemKho();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                //if (dtSetting.LayTrangThaiMenu_ChucNang(Session["IDNhom"].ToString(), 21) == 1)
                //    Response.Redirect("Default.aspx");
                //if (dtSetting.LayTrangThaiMenu(Session["IDNhom"].ToString(), 21) == 1)
                //{
                    if (!IsPostBack)
                    {
                        //data = new dtKiemKho();
                       // object IDPhieuKiemKho = data.ThemPhieu_Temp();
                       
                        txtNguoiLapPhieu.Text = Session["TenDangNhap"].ToString();
                        cmbKho.Value = Session["IDKho"].ToString();
                    }
                    LoadGrid(IDPhieuKiemKho_Temp.Value.ToString());
                //}
                //else
                //{
                //    Response.Redirect("Default.aspx");
                //}
            }
        }

        protected void gridDanhSachHangHoa_Temp_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            string IDPhieuKiemKho = IDPhieuKiemKho_Temp.Value.ToString();
            data = new dtKiemKho();
            data.XoaPhieuKiemKho_Temp_ID(ID);
            e.Cancel = true;
            gridDanhSachHangHoa_Temp.CancelEdit();
            LoadGrid(IDPhieuKiemKho);
        }
        //protected void btnInsertHang_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (txtBarcode.Text.Trim() != "")
        //        {
        //            int IDPhieuKiemKho = Int32.Parse(IDPhieuKiemKho_Temp.Value);
        //            int IDHangHoa = -1;
        //            if (dtKiemKho.LayIDHangHoa_Barcode(txtBarcode.Text.ToString()) != -1)
        //            {
        //                IDHangHoa = dtKiemKho.LayIDHangHoa_Barcode(txtBarcode.Text.ToString());
        //            }
        //            else if (dtKiemKho.LayIDHangHoa_HangHoa(txtBarcode.Value.ToString()) != -1)
        //            {
        //                IDHangHoa = dtKiemKho.LayIDHangHoa_HangHoa(txtBarcode.Value.ToString());
        //            }
        //            if (IDHangHoa != -1)
        //            {
        //                int TonKho = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa.ToString(), Session["IDKho"].ToString());
        //                int ChechLech = -TonKho;
        //                string MaHang = dtHangHoa.LayMaHang(IDHangHoa.ToString());
        //                string IDDonViTinh = dtHangHoa.LayIDDonViTinh(IDHangHoa.ToString());
        //                DataTable dt = data.KTChiTietPhieuKiemKho_Temp(IDHangHoa.ToString(), IDPhieuKiemKho.ToString());
        //                if (dt.Rows.Count == 0)
        //                {
        //                    data = new dtKiemKho();
        //                    data.ThemPhieuKiemKho_Temp(IDPhieuKiemKho.ToString(), IDHangHoa.ToString(), TonKho, ChechLech, MaHang, IDDonViTinh,"0");
        //                    LoadGrid(IDPhieuKiemKho.ToString());
        //                }
        //            }
        //            else
        //                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Mã hàng không tồn tại!!');", true);
        //        }
        //        txtBarcode.Focus();
        //        txtBarcode.Text = "";
        //        txtBarcode.Value = "";
        //    }
        //    catch (Exception ex)
        //    {
        //        txtBarcode.Focus();
        //        txtBarcode.Text = "";
        //        txtBarcode.Value = "";
        //        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Mã hàng không tồn tại!!');", true);
        //    }
        //}
        

        protected void btnHuy_Click(object sender, EventArgs e)
        {
            data = new dtKiemKho();
            string IDPhieuKiemKho = IDPhieuKiemKho_Temp.Value.ToString();
            data.XoaPhieuKiemKho_Temp_IDPhieuKiemKho(IDPhieuKiemKho);
            Response.Redirect("DanhSachKiemKho.aspx");
        }
        public void LoadGrid(string IDPhieuKiemKho)
        {
            data = new dtKiemKho();
            gridDanhSachHangHoa_Temp.DataSource = data.DanhSachKiemKhoTemp_IDPhieuKiemKho(IDPhieuKiemKho);
            gridDanhSachHangHoa_Temp.DataBind();
        }
        protected void gridDanhSachHangHoa_Temp_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string IDPhieuKiemKho = IDPhieuKiemKho_Temp.Value.ToString();
            string ID = e.Keys[0].ToString();
            int ThucTe = Int32.Parse(e.NewValues["ThucTe"].ToString());
            if (ThucTe >= 0)
            {
                string IDHangHoa = e.NewValues["IDHangHoa"].ToString();
                int TonKho = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa,Session["IDKho"].ToString());
                data = new dtKiemKho();
                data.CapNhatPhieuKiemKho_Temp(ID, ThucTe, ThucTe - TonKho);
                e.Cancel = true;
                gridDanhSachHangHoa_Temp.CancelEdit();
                LoadGrid(IDPhieuKiemKho);
            }
            else
            {
                throw new Exception("Lỗi: Số lượng thực tế phải  >= 0");
            }
        }

//        protected void txtBarcode_ItemRequestedByValue(object source, DevExpress.Web.ListEditItemRequestedByValueEventArgs e)
//        {
//            long value = 0;
//            if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
//                return;
//            ASPxComboBox comboBox = (ASPxComboBox)source;
//            dsHangHoa.SelectCommand = @"SELECT GPM_HangHoa.ID, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa, GPM_HangHoa.GiaMuaSauThue, GPM_DonViTinh.TenDonViTinh 
//                                        FROM GPM_DonViTinh INNER JOIN GPM_HangHoa ON GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh 
//                                                           INNER JOIN GPM_HangHoaTonKho ON GPM_HangHoaTonKho.IDHangHoa = GPM_HangHoa.ID 
//                                        WHERE (GPM_HangHoa.ID = @ID)";

//            dsHangHoa.SelectParameters.Clear();
//            dsHangHoa.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
//            comboBox.DataSource = dsHangHoa;
//            comboBox.DataBind();
//        }

//        protected void txtBarcode_ItemsRequestedByFilterCondition(object source, DevExpress.Web.ListEditItemsRequestedByFilterConditionEventArgs e)
//        {
//            ASPxComboBox comboBox = (ASPxComboBox)source;

//            dsHangHoa.SelectCommand = @"SELECT [ID], [MaHang], [TenHangHoa], [GiaMuaSauThue] , [TenDonViTinh]
//                                        FROM (
//	                                        select GPM_HangHoa.ID, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa, GPM_HangHoa.GiaMuaSauThue, GPM_DonViTinh.TenDonViTinh, 
//	                                        row_number()over(order by GPM_HangHoa.MaHang) as [rn] 
//	                                        FROM GPM_DonViTinh INNER JOIN GPM_HangHoa ON GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh 
//                                                               INNER JOIN GPM_HangHoaTonKho ON GPM_HangHoaTonKho.IDHangHoa = GPM_HangHoa.ID
//	                                        WHERE (GPM_HangHoa.MaHang LIKE @MaHang OR GPM_HangHoa.TenHangHoa LIKE @TenHang) AND (GPM_HangHoaTonKho.IDKho = @IDKho) AND (GPM_HangHoaTonKho.DaXoa = 0)	
//	                                        ) as st 
//                                        where st.[rn] between @startIndex and @endIndex";

//            dsHangHoa.SelectParameters.Clear();
//            dsHangHoa.SelectParameters.Add("MaHang", TypeCode.String, string.Format("%{0}%", e.Filter));
//            dsHangHoa.SelectParameters.Add("TenHang", TypeCode.String, string.Format("%{0}%", e.Filter));
//            dsHangHoa.SelectParameters.Add("IDKho", TypeCode.Int32, Session["IDKho"].ToString());
//            dsHangHoa.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString());
//            dsHangHoa.SelectParameters.Add("endIndex", TypeCode.Int64, (e.EndIndex + 1).ToString());
//            comboBox.DataSource = dsHangHoa;
//            comboBox.DataBind();
//        }

        protected void cmbKe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbKe.Text != "")
            {
                // kiểm kho phải kiểm từng kệ.
                data = new dtKiemKho();
                data.XoaPhieuKiemKho_Temp_IDPhieuKiemKho(IDPhieuKiemKho_Temp.Value.ToString());
                Random ran = new Random();
                IDPhieuKiemKho_Temp.Value = ran.Next(100000, 999999).ToString();
                dtKe k = new dtKe();
                DataTable db = k.DanhSachChiTietKe(cmbKe.Value.ToString());
                if (db.Rows.Count > 0)
                {
                    string IDPhieuKiemKho = IDPhieuKiemKho_Temp.Value.ToString();
                    foreach (DataRow dr in db.Rows)
                    {
                        string IDHangHoa = dr["IDHangHoa"].ToString();
                        string MaHang = dr["MaHang"].ToString();
                        string IDonViTinh = dr["IDonViTinh"].ToString();
                        int TonKho = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa, Session["IDKho"].ToString());
                        int ChechLech = -TonKho;
                        DataTable dt = data.KTChiTietPhieuKiemKho_Temp(IDHangHoa, IDPhieuKiemKho, cmbKe.Value.ToString());
                        if (dt.Rows.Count == 0)
                        {
                            data = new dtKiemKho();
                            data.ThemPhieuKiemKho_Temp(IDPhieuKiemKho, IDHangHoa, TonKho, ChechLech, MaHang, IDonViTinh, cmbKe.Value.ToString());
                        }
                    }
                    LoadGrid(IDPhieuKiemKho);
                }
                else
                {
                    Response.Write("<script language='JavaScript'> alert('Kệ chưa có hàng hóa.'); </script>");
                }
            }
        }

        protected void txtNgayLapPhieu_Init(object sender, EventArgs e)
        {
            txtNgayLapPhieu.Date = DateTime.Today;
        }

        protected void btnLuu_Click(object sender, EventArgs e)
        {
            string IDPhieuKiemKho = IDPhieuKiemKho_Temp.Value.ToString();
            DataTable db = data.DanhSachKiemKhoTemp_IDPhieuKiemKho(IDPhieuKiemKho);
            if (db.Rows.Count > 0)
            {
                string IDNguoiDung = Session["IDNhanVien"].ToString();
                DateTime NgayKiemKho = DateTime.Parse(txtNgayLapPhieu.Text.ToString());
                string IDKho = Session["IDKho"].ToString();
                string GhiChu = txtGhiChu.Text == null ? "" : txtGhiChu.Text.ToString();
                data = new dtKiemKho();
                object ID1 = data.ThemPhieu_Temp();
                if (ID1 != null)
                {
                    data.CapNhatPhieuKiemKho(ID1, IDNguoiDung, NgayKiemKho, IDKho, GhiChu);
                    foreach (DataRow dr in db.Rows)
                    {
                        string IDHangHoa = dr["IDHangHoa"].ToString();
                        string TonKho = dr["TonKho"].ToString();
                        string ChenhLech = dr["ChenhLech"].ToString();
                        string ThucTe = dr["ThucTe"].ToString();
                        string MaHang = dr["MaHang"].ToString();
                        string IDDonViTinh = dr["IDDonViTinh"].ToString();
                        string IDKe = dr["IDKe"].ToString();
                        data = new dtKiemKho();
                        data.ThemPhieuKiemKho(ID1, IDHangHoa, TonKho, ChenhLech, ThucTe, MaHang, IDDonViTinh, IDKe);
                    }
                    data = new dtKiemKho();
                    data.XoaPhieuKiemKho_Temp_IDPhieuKiemKho(IDPhieuKiemKho);
                    Response.Redirect("DanhSachKiemKho.aspx");
                    //dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Kiểm Kho", Session["IDKho"].ToString(), "Nhập xuất tồn", "Thêm");  
                }
                else
                {
                    Response.Write("<script language='JavaScript'> alert('Cập nhật không thành công.'); </script>");
                }
            }
            else
            {
                cmbKe.Focus();
                Response.Write("<script language='JavaScript'> alert('Danh sách kiểm kho rỗng.'); </script>");
            }
        }
    }
}