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
    public partial class DanhMucCombo : System.Web.UI.Page
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
                {
                    gridDanhMucCombo.Columns["iconaction"].Visible = false;
                    btnThemHangHoaComBo.Enabled = false;
                }
                if (dtSetting.LayTrangThaiMenu(Session["IDNhom"].ToString(), 8) == 1)
                {
                    LoadGrid();
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }

        private void LoadGrid()
        {
            data = new dtHangCombo();
            gridDanhMucCombo.DataSource = data.DanhSachHangHoaCombo(Session["IDKho"].ToString());
            gridDanhMucCombo.DataBind();
        }

        protected void gridDanhMucCombo_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            int SLTonKho = dtCapNhatTonKho.SoLuong_TonKho(ID, Session["IDKho"].ToString());
            if (SLTonKho == 0)
            {
                data = new dtHangCombo();
                data.XoaHangCombo(ID);
                dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Danh Mục Combo:" + ID, Session["IDKho"].ToString(), "Danh Mục", "Xóa");
            }
            else
            {
                throw new Exception("Lỗi: Vui lòng cập nhật số lượng hàng hóa về 0");
            }
            e.Cancel = true;
            gridDanhMucCombo.CancelEdit();
            LoadGrid();
        }

        protected void gridDanhMucCombo_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            int ID = Int32.Parse(e.Keys[0].ToString());
            string MaHang = e.NewValues["MaHang"].ToString();
            string TenHangHoa = e.NewValues["TenHangHoa"].ToString();
            string IDDonViTinh = e.NewValues["IDDonViTinh"].ToString();
            string IDNhomHang = e.NewValues["IDNhomHang"].ToString();
            string GiaBanSauThue = e.NewValues["GiaBanSauThue"].ToString();
            string TrongLuong = e.NewValues["TrongLuong"].ToString();
            string IDTrangThaiHang = e.NewValues["IDTrangThaiHang"].ToString();
            string HanSuDung = e.NewValues["HanSuDung"].ToString();
            string GhiChu = e.NewValues["GhiChu"] == null ? "" : e.NewValues["GhiChu"].ToString();
            if (dtSetting.kiemTraChuyenDoiDau() == 1)
                TenHangHoa = dtSetting.convertDauSangKhongDau(TenHangHoa).ToUpper();
            if (dtSetting.IsNumber(MaHang) == true)
            {
                if (dtHangCombo.KiemTraMa_ID(MaHang, ID.ToString()) == true)
                {
                    data = new dtHangCombo();
                    data.CapNhat(ID, MaHang, TenHangHoa, IDDonViTinh, IDNhomHang, GiaBanSauThue, TrongLuong, IDTrangThaiHang, HanSuDung, GhiChu, Session["IDKho"].ToString());
                }
                else
                {
                    if ((dtHangHoa.KiemTraMaHang(MaHang)) == false)
                    {
                        data = new dtHangCombo();
                        data.CapNhat(ID, MaHang, TenHangHoa, IDDonViTinh, IDNhomHang, GiaBanSauThue, TrongLuong, IDTrangThaiHang, HanSuDung, GhiChu, Session["IDKho"].ToString());

                    }
                    else
                    {
                        throw new Exception("Lỗi: Mã hàng đã tồn tại.");
                    }
                }
            }
            else
            {
                throw new Exception("Lỗi: Mã hàng phải là số.");
            }
            e.Cancel = true;
            gridDanhMucCombo.CancelEdit();
            LoadGrid();
            //---------------------------------------------------------------------------------------------------
            //int ID = Int32.Parse(e.Keys[0].ToString());
            //string TenHangHoa = e.NewValues["TenHangHoa"] == null ? "" : e.NewValues["TenHangHoa"].ToString();
            //if (TenHangHoa != "")
            //{
            //    string TrangThai = e.NewValues["IDTrangThaiHang"].ToString();
            //    string TrongLuong = e.NewValues["TrongLuong"].ToString();
            //    string TongTien = e.NewValues["GiaBanSauThue"].ToString();
            //    string HanSuDung = e.NewValues["HanSuDung"].ToString();
            //    int SLMoi = Int32.Parse(e.NewValues["SoLuongCon"].ToString());
            //    int SLTonKho =  dtCapNhatTonKho.SoLuong_TonKho(ID.ToString(),Session["IDKho"].ToString());
            //    float GiaBanSauVAT = dtHangHoa.LayGiaBanSauThue(ID.ToString());
            //    if (SLTonKho != SLMoi)
            //    {
            //        DataTable db = data.DanhSachHangHoaCombo_IDHangHoaComBo(ID.ToString());
            //        if (db.Rows.Count > 0)
            //        {
            //            int kt = 0;
            //            foreach (DataRow dr in db.Rows)
            //            {
            //                string IDHangHoa = dr["IDHangHoa"].ToString();
            //                int SLHang = Int32.Parse(dr["SoLuong"].ToString());
            //                int SLTon = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa,Session["IDKho"].ToString());
            //                int SLTon_SUM = (SLTonKho * SLHang) + SLTon; // số lượng tồn của từng hàng hóa trong combo
            //                if (SLHang * SLMoi > SLTon_SUM)
            //                {
            //                    kt = 1;
            //                    throw new Exception("Lỗi: Hàng tồn kho không đủ: " + dtHangHoa.LayTenHangHoa(IDHangHoa) + " : Số lượng cần(" + (SLHang * SLMoi) + ") :  Số lượng tồn(" + SLTon_SUM + ")");
            //                }
            //            }
            //            if (kt == 0)
            //            {
            //                //throw new Exception("đủ hàng:");
            //                foreach (DataRow dr in db.Rows)
            //                {
            //                    string IDHangHoa = dr["IDHangHoa"].ToString();
            //                    int SLHang = Int32.Parse(dr["SoLuong"].ToString());
            //                    int SLTon = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa, Session["IDKho"].ToString());
            //                    int SLTon_SUM = (SLTonKho * SLHang) + SLTon; // số lượng tổng tồn của từng hàng hóa trong combo, đã gã hàng
            //                    if (SLHang * SLMoi <= SLTon_SUM)
            //                    {
            //                        //đủ hàng
            //                        int SL_SUM = (SLTon_SUM - (SLMoi * SLHang));
            //                        dtLichSuKho.ThemLichSu(IDHangHoa, Session["IDNhanVien"].ToString(), ((-1) * (SL_SUM - SLTon)).ToString(), "Cập nhật số lượng chi tiết hàng combo:" + dtHangHoa.LayTenHangHoa(IDHangHoa) + "", Session["IDKho"].ToString());
            //                        dtCapNhatTonKho.CapNhatKho(IDHangHoa, SL_SUM.ToString(), Session["IDKho"].ToString());
            //                    }
            //                }
            //                dtLichSuKho.ThemLichSu(ID.ToString(), Session["IDNhanVien"].ToString(), ((-1) * (SLMoi - SLTonKho)).ToString(), "Cập nhật số lượng hàng combo:" + dtHangHoa.LayTenHangHoa(ID.ToString()) + "", Session["IDKho"].ToString());
            //                dtCapNhatTonKho.CapNhatKho(ID.ToString(), SLMoi.ToString(), Session["IDKho"].ToString());
            //            }
            //        }
            //        else
            //        {
            //            throw new Exception("Lỗi: Không có hàng hóa trong combo?");
            //        }
            //        dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Danh Mục Combo:" + TenHangHoa, Session["IDKho"].ToString(), "Danh Mục", "Cập nhật số lượng");  
            //    }
            //    else if (GiaBanSauVAT != float.Parse(TongTien))
            //    {
            //        dtHangHoa.ThemLichSuThayDoiGia(ID.ToString(), dtHangHoa.LayIDDonViTinh(ID.ToString()), dtHangHoa.LayGiaBanSauThue(ID.ToString()), float.Parse(TongTien), Session["IDNhanVien"].ToString(), dtHangHoa.LayMaHang(ID.ToString()));
            //        data = new dtHangCombo();
            //        data.CapNhatHangHoa_Combo(ID, TrangThai, TrongLuong, TongTien, HanSuDung, TenHangHoa);
            //        dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Danh Mục Combo:" + TenHangHoa, Session["IDKho"].ToString(), "Danh Mục", "Cập nhật thông tin");
            //    }
            //    else
            //    {
            //        data = new dtHangCombo();
            //        data.CapNhatHangHoa_Combo(ID, TrangThai, TrongLuong, TongTien, HanSuDung, TenHangHoa);
            //        dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Danh Mục Combo:" + TenHangHoa, Session["IDKho"].ToString(), "Danh Mục", "Cập nhật thông tin");
            //    }
            //}
            //else
            //{
            //    throw new Exception("Lỗi: Vui lòng nhập Tên hàng hóa");
            //}
            //e.Cancel = true;
            //gridDanhMucCombo.CancelEdit();
            //LoadGrid();
        }


        protected void BtnSuaSoLuong_Click(object sender, EventArgs e)
        {
            string ID = (((ASPxButton)sender).CommandArgument).ToString();
            LoadGrid();
            object MaHang = gridDanhMucCombo.GetRowValuesByKeyValue(ID, "MaHang");
            object TenHang = gridDanhMucCombo.GetRowValuesByKeyValue(ID, "TenHangHoa");
            object SoLuong = gridDanhMucCombo.GetRowValuesByKeyValue(ID, "SoLuongCon");
            txtMaHangSua.Text = MaHang.ToString();
            txtTenHangSua.Text = TenHang.ToString();
            txtSoLuongSua.Text = SoLuong.ToString();
            hdfIDSuaSL.Value = ID;
            popupSuaSoLuong.ShowOnPageLoad = true;
        }

        protected void btnLuuSuaSL_Click(object sender, EventArgs e)
        {
            string ID = hdfIDSuaSL.Value;
            data = new dtHangCombo();
            int SLMoi = Int32.Parse(txtSoLuongSua.Text);
            if (SLMoi >= 0)
            {
                int SLTonKho = dtCapNhatTonKho.SoLuong_TonKho(ID.ToString(), Session["IDKho"].ToString());
                if (SLMoi > SLTonKho)
                {
                    //gộp hàng combo
                    DataTable db = data.DanhSachHangHoaCombo_IDHangHoaComBo(ID.ToString());
                    if (db.Rows.Count > 0)
                    {
                        //kiểm tra số lượng hàng con của combo
                        int kt = 0;
                        foreach (DataRow dr in db.Rows)
                        {
                            string IDHangHoa = dr["IDHangHoa"].ToString();
                            int SLHang = Int32.Parse(dr["SoLuong"].ToString());
                            int SLTon = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa, Session["IDKho"].ToString());
                            int SLTon_SUM = (SLTonKho * SLHang) + SLTon; // số lượng tồn của từng hàng hóa trong combo
                            if (SLHang * SLMoi > SLTon_SUM)
                            {
                                kt = 1;
                                Response.Write("<script language='JavaScript'> alert('Lỗi: Hàng tồn kho không đủ: " + dtHangHoa.LayTenHangHoa(IDHangHoa) + " : Số lượng cần(" + (SLHang * SLMoi) + ") :  Số lượng tồn(" + SLTon_SUM + ")'); </script>");
                                break;
                            }
                        }

                        if (kt == 0)
                        {
                            //throw new Exception("đủ hàng:");
                            foreach (DataRow dr in db.Rows)
                            {
                                string IDHangHoa = dr["IDHangHoa"].ToString();
                                int SLHang = Int32.Parse(dr["SoLuong"].ToString());
                                int SLTon = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa, Session["IDKho"].ToString());
                                int SLTon_SUM = (SLTonKho * SLHang) + SLTon; // số lượng tổng tồn của từng hàng hóa trong combo, đã gã hàng
                                if (SLHang * SLMoi <= SLTon_SUM)
                                {
                                    //đủ hàng
                                    int SL_SUM = (SLTon_SUM - (SLMoi * SLHang));
                                    dtLichSuKho.ThemLichSu(IDHangHoa, Session["IDNhanVien"].ToString(), ((-1) * (SL_SUM - SLTon)).ToString(), "Gộp Hàng Combo:" + dtHangHoa.LayTenHangHoa(IDHangHoa) + "", Session["IDKho"].ToString());
                                    dtCapNhatTonKho.CapNhatKho(IDHangHoa, SL_SUM.ToString(), Session["IDKho"].ToString());
                                }
                            }
                            dtLichSuKho.ThemLichSu(ID.ToString(), Session["IDNhanVien"].ToString(), ((-1) * (SLMoi - SLTonKho)).ToString(), "Cập nhật số lượng hàng combo:" + dtHangHoa.LayTenHangHoa(ID.ToString()) + "", Session["IDKho"].ToString());
                            dtCapNhatTonKho.CapNhatKho(ID.ToString(), SLMoi.ToString(), Session["IDKho"].ToString());
                        }
                    }
                    else 
                    {
                        Response.Write("<script language='JavaScript'> alert('Lỗi: Không có hàng trong combo này? Vui lòng kiểm tra lại.'); </script>");
                    }
                }
                else if (SLMoi < SLTonKho)
                {
                    // tách hàng combo
                    DataTable db = data.DanhSachHangHoaCombo_IDHangHoaComBo(ID.ToString());
                    if (db.Rows.Count > 0)
                    {
                        int SLTachHang = SLTonKho - SLMoi;// sl hàng hóa combo rã vd:5
                        foreach (DataRow dr in db.Rows)
                        {
                            string IDHangHoa = dr["IDHangHoa"].ToString();
                            int SLHang = Int32.Parse(dr["SoLuong"].ToString());//vd: 7
                            int SLTon = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa, Session["IDKho"].ToString());// vd: 10
                            int SLTon_SUM = (SLTachHang * SLHang) + SLTon; //(5 * 7) + 10 = 45
                            dtCapNhatTonKho.CapNhatKho(IDHangHoa, SLTon_SUM.ToString(), Session["IDKho"].ToString()); // vd: 45
                            dtLichSuKho.ThemLichSu(IDHangHoa, Session["IDNhanVien"].ToString(), (SLHang * SLTachHang).ToString(), "Tách Hàng Combo:" + dtHangHoa.LayTenHangHoa(IDHangHoa) + "", Session["IDKho"].ToString());
                        }
                        dtLichSuKho.ThemLichSu(ID.ToString(), Session["IDNhanVien"].ToString(), ((-1) * (SLTachHang)).ToString(), "Cập nhật số lượng hàng combo:" + dtHangHoa.LayTenHangHoa(ID.ToString()) + "", Session["IDKho"].ToString());
                        dtCapNhatTonKho.CapNhatKho(ID.ToString(), SLMoi.ToString(), Session["IDKho"].ToString());
                    }
                    else
                    {
                        Response.Write("<script language='JavaScript'> alert('Lỗi: Không có hàng trong combo này? Vui lòng kiểm tra lại.'); </script>");
                    }
                }
            }
            else 
            {
                Response.Write("<script language='JavaScript'> alert('Lỗi: Số lượng phải lớn hơn hoặc bằng 0.'); </script>");
            }
            LoadGrid();
            popupSuaSoLuong.ShowOnPageLoad = false;
        }

        protected void btnHuySuaSl_Click(object sender, EventArgs e)
        {
            popupSuaSoLuong.ShowOnPageLoad = false;
        }
    }
}