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
    public partial class NewsHangHoa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (Request.QueryString["IDHH"] != null)
                //{
                //    string IDHH = Request.QueryString["IDHH"].ToString();
                //    dataHangHoa data = new dataHangHoa();
                //    aspGridBarcode.DataSource = data.GetListBarCode((object)IDHH);
                //    aspGridBarcode.DataBind();

                //    DataTable da = data.getDanhSachHangHoa_ID(IDHH);
                //    if (da.Rows.Count != 0)
                //    {
                //        DataRow dr = da.Rows[0];
                //        IDNhomHang.Value = dr["IDNhomHang"].ToString();
                //        MaHang.Text = dr["MaHang"].ToString();
                //        TenHangHoa.Text = dr["TenHangHoa"].ToString();
                //        IDDonViTinh.Value = dr["IDDonViTinh"].ToString();
                //        txtHeSo.Value = dr["HeSo"].ToString();
                //        IDHangSanXuat.Value = dr["IDHangSanXuat"].ToString();
                //        IDThue.Value = dr["IDThue"].ToString();
                //        IDHangQuyDoi.Value = dr["IDHangQuyDoi"].ToString();
                //        IDNhomDatHang.Value = dr["IDNhomDatHang"].ToString();
                //        GiaMuaTruocThue.Value = dr["GiaMuaTruocThue"].ToString();
                //        GiaBanTruocThue.Value = dr["GiaBanTruocThue"].ToString();
                //        GiaMuaSauThue.Value = dr["GiaMuaSauThue"].ToString();
                //        GiaBanSauThue.Value = dr["GiaBanSauThue"].ToString();
                //        GiaBan1.Value = dr["GiaBan1"].ToString();
                //        GiaBan2.Value = dr["GiaBan2"].ToString();
                //        GiaBan3.Value = dr["GiaBan3"].ToString();
                //        GiaBan4.Value = dr["GiaBan4"].ToString();
                //        GiaBan5.Value = dr["GiaBan5"].ToString();
                //        TrongLuong.Value = dr["TrongLuong"].ToString();
                //        HanSuDung.Value = dr["HanSuDung"].ToString();
                //        IDTrangThaiHang.Value = dr["IDTrangThaiHang"].ToString();
                //        GhiChu.Value = dr["GhiChu"].ToString();
                //    }
                //    else
                //    {
                //        int Max = Int32.Parse(dataHangHoa.LayID_Max().ToString().Substring(2));
                //        string MaHangx = IDDonViTinh.Value + (((Max + 1) * 0.0001).ToString().Replace(".", ""));
                //        MaHang.Text = MaHangx;
                //    }
                //}
            }
        }

        protected void IDThue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GiaBanTruocThue.Value != null && GiaMuaTruocThue.Value != null)
            {
                dtDanhMucThue data = new dtDanhMucThue();
                int TiLe = data.LayTiLeThue(IDThue.Value.ToString());
                GiaBanSauThue.Value = Int32.Parse(GiaBanTruocThue.Value.ToString()) + ((Int32.Parse(GiaBanTruocThue.Value.ToString()) * TiLe) / 100);
                GiaMuaSauThue.Value = Int32.Parse(GiaMuaTruocThue.Value.ToString()) + ((Int32.Parse(GiaMuaTruocThue.Value.ToString()) * TiLe) / 100);
            }
        }

        protected void aspGridBarcode_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            dataHangHoa data = new dataHangHoa();
            int ID = Int32.Parse(e.Keys["ID"].ToString());
            data.XoaBarCode(ID);
            e.Cancel = true;
            string IDHH = Request.QueryString["IDHH"].ToString();
            aspGridBarcode.DataSource = data.GetListBarCode((object)IDHH);
            aspGridBarcode.DataBind();

            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Hàng Hóa ID:" + ID, Session["IDKho"].ToString(), "Danh Mục", "Xoa Barcode"); 
        }

        protected void aspGridBarcode_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            dataHangHoa data = new dataHangHoa();
            string IDHH = Request.QueryString["IDHH"].ToString();
            string IDTrangThaiBarcode = e.NewValues["IDTrangThaiBarcode"].ToString();
            string BarCode = e.NewValues["Barcode"] != null ? e.NewValues["Barcode"].ToString() : "";
            data.ThemBarCode(IDHH,IDTrangThaiBarcode, BarCode);
            e.Cancel = true;
            aspGridBarcode.CancelEdit();
            aspGridBarcode.DataSource = data.GetListBarCode((object)IDHH);
            aspGridBarcode.DataBind();
        }

        protected void aspGridBarcode_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            dataHangHoa data = new dataHangHoa();
            int ID = Int32.Parse(e.Keys["ID"].ToString());
            string IDHH = Request.QueryString["IDHH"].ToString();
            string IDTrangThaiBarcode = e.NewValues["IDTrangThaiBarcode"].ToString();
            string BarCode = e.NewValues["Barcode"] != null ? e.NewValues["Barcode"].ToString() : "";
            data.CapNhatBarCode(ID,IDHH, IDTrangThaiBarcode, BarCode);
            e.Cancel = true;
            aspGridBarcode.CancelEdit();
            aspGridBarcode.DataSource = data.GetListBarCode((object)IDHH);
            aspGridBarcode.DataBind();
        }

        protected void IDDonViTinh_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (IsPostBack && Request.QueryString["IDHH"] != null)
            {
                //string IDHH = Request.QueryString["IDHH"].ToString();
                //dataHangHoa data = new dataHangHoa();
                //int maHang = 0;
                //DataTable da = data.getDanhSachHangHoa_ID(IDHH);
                //if (da.Rows.Count != 0)
                //{
                //    DataRow dr = da.Rows[0];
                //    maHang = Int32.Parse(dr["MaHang"].ToString().Substring(2)) - 1;
                //}
                //else
                //{
                //    maHang = Int32.Parse(dataHangHoa.LayID_Max().ToString().Substring(2));
                //}
                //string MaHangx = IDDonViTinh.Value + (((maHang + 1) * 0.0001).ToString().Replace(".", ""));
                //MaHang.Text = MaHangx;
            }
        }

        protected void Luu_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["IDHH"] != null)
            {
                string IDHH = Request.QueryString["IDHH"].ToString();
                dataHangHoa data = new dataHangHoa();
                DataTable dta = data.GetListBarCode(Int32.Parse(IDHH));

                if (IDNhomHang.Value != null && MaHang.Value != null && TenHangHoa.Value != null && IDDonViTinh.Value != null &&
                         IDHangSanXuat.Value != null && IDThue.Value != null && IDNhomDatHang.Value != null &&
                         GiaMuaTruocThue.Value != null && GiaBanTruocThue.Value != null && GiaMuaSauThue.Value != null && GiaBanSauThue.Value != null &&
                         GiaBan1.Value != null && GiaBan2.Value != null && GiaBan3.Value != null && GiaBan4.Value != null && GiaBan5.Value != null &&
                         TrongLuong.Value != null && HanSuDung.Value != null && IDTrangThaiHang.Value != null && txtHeSo.Value != null)
                {
                    if (dta.Rows.Count != 0)
                    {
                        string idNhomHang = IDNhomHang.Value.ToString();
                        string maHang = MaHang.Value.ToString();
                        string tenHangHoa = TenHangHoa.Value.ToString();
                        string idDonViTinh = IDDonViTinh.Value.ToString();
                        string HeSo = txtHeSo.Value.ToString();
                        string idHangSX = IDHangSanXuat.Value.ToString();
                        string idThue = IDThue.Value.ToString();
                        string idHangHoaQuyDoi = "0";
                        if (IDHangQuyDoi.Value != null)
                            idHangHoaQuyDoi = IDHangQuyDoi.Value.ToString();
                        else idHangHoaQuyDoi = IDHH;
                        string idNhomDatHang = IDNhomDatHang.Value.ToString();
                        string giaMuaTruocThue = GiaMuaTruocThue.Value.ToString();
                        string giaBanTruocThue = GiaBanTruocThue.Value.ToString();
                        string giaMuaSauThue = GiaMuaSauThue.Value.ToString();
                        string giaBanSauThue = GiaBanSauThue.Value.ToString();
                        string giaBan1 = GiaBan1.Value.ToString();
                        string giaBan2 = GiaBan2.Value.ToString();
                        string giaBan3 = GiaBan3.Value.ToString();
                        string giaBan4 = GiaBan4.Value.ToString();
                        string giaBan5 = GiaBan5.Value.ToString();
                        string trongLuong = TrongLuong.Value.ToString();
                        string hanSuDung = HanSuDung.Value.ToString();
                        string idTrangThaiHang = IDTrangThaiHang.Value.ToString();
                        string ghiChu = GhiChu.Value == null ? "" : GhiChu.Value.ToString();

                        int ktHH = data.KiemTraHangHoa_Null(IDHH);
                        if (ktHH == 0)
                        {
                            // Thêm mới.
                            if (dtHangHoa.KiemTraMaHang(maHang) == false)
                            {
                                data.SuaThongTinHangHoa(IDHH, idNhomHang, maHang, tenHangHoa, idDonViTinh, HeSo, idHangSX, idThue, idHangHoaQuyDoi
                                , idNhomDatHang, giaMuaTruocThue, giaBanTruocThue, giaMuaSauThue, giaBanSauThue, giaBan1, giaBan2, giaBan3, giaBan4, giaBan5, trongLuong, hanSuDung, idTrangThaiHang, ghiChu);

                                dtKho dtX = new dtKho();
                                DataTable dataKho = dtX.LayDanhSachKho();
                                for (int i = 0; i < dataKho.Rows.Count; i++)
                                {
                                    DataRow dr = dataKho.Rows[i];
                                    string idKho = dr["ID"].ToString();

                                    dtLichSuKho.ThemLichSu(IDHH, Session["IDNhanVien"].ToString(), 0 + "", "Thêm hàng hóa vào kho:" + tenHangHoa, Session["IDKho"].ToString());
                                    data.ThemHangVaoTonKho(idKho, IDHH, 0 + "", giaBanSauThue, giaBan1, giaBan2, giaBan3, giaBan4, giaBan5);
                                }

                                dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Hàng hóa:" + TenHangHoa, Session["IDKho"].ToString(), "Danh Mục", "Thêm"); 
                                Response.Write("<script language='JavaScript'> alert('Thêm hàng hóa thành công.'); </script>");
                                Response.Redirect("HangHoa.aspx");
                            }
                            else
                            {
                                Response.Write("<script language='JavaScript'> alert('Mã hàng đã tồn tại.'); </script>");
                            }

                        }
                        else
                        {
                            // Cập nhật.
                            DataTable da = data.getDanhSachHangHoa_ID(IDHH);
                            if (da.Rows.Count != 0)
                            {
                                DataRow dr = da.Rows[0];
                                IDNhomHang.Value = dr["IDNhomHang"].ToString();
                                string MH = dr["MaHang"].ToString();
                                if (MH.CompareTo(maHang) != 0)
                                {
                                    if (dtHangHoa.KiemTraMaHang(maHang) == false)
                                    {
                                        data.SuaThongTinHangHoa(IDHH, idNhomHang, maHang, tenHangHoa, idDonViTinh, HeSo, idHangSX, idThue, idHangHoaQuyDoi
                                    , idNhomDatHang, giaMuaTruocThue, giaBanTruocThue, giaMuaSauThue, giaBanSauThue, giaBan1, giaBan2, giaBan3, giaBan4, giaBan5, trongLuong, hanSuDung, idTrangThaiHang, ghiChu);

                                        dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Hàng hóa:" + TenHangHoa, Session["IDKho"].ToString(), "Danh Mục", "Cập nhật"); 
                                        Response.Write("<script language='JavaScript'> alert('Cập nhật hàng hóa thành công.'); </script>");
                                        Response.Redirect("HangHoa.aspx");
                                    }
                                    else
                                    {
                                        Response.Write("<script language='JavaScript'> alert('Mã hàng đã tồn tại.'); </script>");
                                    }
                                }
                                else
                                {
                                    data.SuaThongTinHangHoa(IDHH, idNhomHang, maHang, tenHangHoa, idDonViTinh, HeSo, idHangSX, idThue, idHangHoaQuyDoi
                                    , idNhomDatHang, giaMuaTruocThue, giaBanTruocThue, giaMuaSauThue, giaBanSauThue, giaBan1, giaBan2, giaBan3, giaBan4, giaBan5, trongLuong, hanSuDung, idTrangThaiHang, ghiChu);

                                    dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Hàng hóa:" + TenHangHoa, Session["IDKho"].ToString(), "Danh Mục", "Cập nhật"); 
                                    Response.Write("<script language='JavaScript'> alert('Cập nhật hàng hóa thành công.'); </script>");
                                    Response.Redirect("HangHoa.aspx");
                                }
                            }
                            
                        }
                        
                    }
                    else Response.Write("<script language='JavaScript'> alert('Danh sách barcode không được rỗng.'); </script>");
                }
                else
                    Response.Write("<script language='JavaScript'> alert('Không bỏ trống các trường (*).'); </script>");
            }else Response.Write("<script language='JavaScript'> alert('Quá trình thêm đang gặp lỗi, vui lòng mở lại trang.'); </script>");
        }

        protected void btnHuy_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["IDHH"] != null)
            {
                dataHangHoa data = new dataHangHoa();
                string IDHH = Request.QueryString["IDHH"].ToString();
                data.XoaHangHoa_Delete(IDHH);
            }
            Response.Redirect("HangHoa.aspx");
        }

        protected void GiaMuaTruocThue_NumberChanged(object sender, EventArgs e)
        {
            if (GiaMuaSauThue.Value != null && IDThue.Value!= null)
            {
                dtDanhMucThue data = new dtDanhMucThue();
                int TiLe = data.LayTiLeThue(IDThue.Value.ToString());
                GiaMuaSauThue.Value = Int32.Parse(GiaMuaTruocThue.Value.ToString()) + ((Int32.Parse(GiaMuaTruocThue.Value.ToString()) * TiLe) / 100);
            }
        }

        protected void GiaBanTruocThue_NumberChanged(object sender, EventArgs e)
        {
            if (GiaBanSauThue.Value != null && IDThue.Value != null)
            {
                dtDanhMucThue data = new dtDanhMucThue();
                int TiLe = data.LayTiLeThue(IDThue.Value.ToString());
                GiaBanSauThue.Value = Int32.Parse(GiaBanTruocThue.Value.ToString()) + ((Int32.Parse(GiaBanTruocThue.Value.ToString()) * TiLe) / 100);
            }
        }
    }
}