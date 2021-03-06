﻿using BanHang.Data;
using BanHang.Object;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class HangHoa_ChiTiet : System.Web.UI.Page
    {
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
                    dataHangHoa dt = new dataHangHoa();
                    DataTable da = dt.getDanhSachHangHoa_Ten_ID2(Request.QueryString["IDHangHoa"]);
                    da.Rows.Add(0, "000000", "Không quy đổi");
                    cmbHangQuyDoi.DataSource = da;
                    cmbHangQuyDoi.TextField = "TenHangHoa";
                    cmbHangQuyDoi.ValueField = "ID";
                    cmbHangQuyDoi.DataBind();
                    cmbHangQuyDoi.SelectedIndex = da.Rows.Count;
                    Load();
                }
            }
        }

        public void Load()
        {
            dataHangHoa data = new dataHangHoa();
            string IDHangHoa = Request.QueryString["IDHangHoa"];

            DataTable da = data.getDanhSachHangHoa_ID_2(IDHangHoa);
            cmbNganhHang.Value = da.Rows[0]["IDNganhHang"].ToString();
            cmbNhomHang.Value = da.Rows[0]["IDNhomHang"].ToString();
            txtMaHang.Value = da.Rows[0]["MaHang"].ToString();
            txtTenHang.Value = da.Rows[0]["TenHangHoa"].ToString();
            cmbDonViTinh.Value = da.Rows[0]["IDDonViTinh"].ToString();
            txtHeSo.Value = da.Rows[0]["HeSo"].ToString();
            cmbHangSX.Value = da.Rows[0]["IDHangSanXuat"].ToString();
            cmbThue.Value = da.Rows[0]["IDThue"].ToString();
            cmbNhomDatHang.Value = da.Rows[0]["IDNhomDatHang"].ToString();
            txtTrongLuong.Value = da.Rows[0]["TrongLuong"].ToString();
            txtHangSuDung.Value = da.Rows[0]["HanSuDung"].ToString();
            cmbTrangThaiHang.Value = da.Rows[0]["IDTrangThaiHang"].ToString();
            txtGhiChu.Value = da.Rows[0]["GhiChu"].ToString();
            cmbHangQuyDoi.Value = da.Rows[0]["IDHangQuyDoi"].ToString();

            txtGiaMuaTruocThue.Value = da.Rows[0]["GiaMuaTruocThue"].ToString();
            txtGiaBanTruocThue.Value = da.Rows[0]["GiaBanTruocThue"].ToString();
            txtGiaMuaSauThue.Value = da.Rows[0]["GiaMuaSauThue"].ToString();
            txtGiaBanSauThue.Value = da.Rows[0]["GiaBan"].ToString();
            txtGiaBan1.Value = da.Rows[0]["GiaBan1"].ToString();
            txtGiaBan2.Value = da.Rows[0]["GiaBan2"].ToString();
            txtGiaBan3.Value = da.Rows[0]["GiaBan3"].ToString();
            txtGiaBan4.Value = da.Rows[0]["GiaBan4"].ToString();
            txtGiaBan5.Value = da.Rows[0]["GiaBan5"].ToString();


            gridHangHoaBarcode.DataSource = data.GetListBarCode(IDHangHoa);
            gridHangHoaBarcode.DataBind();
            gridHangHoaGiaTheoSL.DataSource = data.GetListHangHoa_GiaTheoSL(IDHangHoa);
            gridHangHoaGiaTheoSL.DataBind();
        }

        protected void cmbDonViTinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtDonViTinh dt = new dtDonViTinh();
            string MaDV = dt.LayMaDonViTinh(cmbDonViTinh.Value + "");
            int Max = Int32.Parse(dataHangHoa.LayID_Count(MaDV).ToString());
            string MaHangx = (((Max + 1) * 0.001).ToString().Replace(".", ""));
            txtMaHang.Value = MaDV + MaHangx.Substring(2);
        }

        protected void cmbThue_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtGiaBanTruocThue.Value = 0;
            txtGiaBanSauThue.Value = 0;
            txtGiaMuaSauThue.Value = 0;
            txtGiaMuaTruocThue.Value = 0;
        }

        protected void txtGiaMuaSauThue_ValueChanged(object sender, EventArgs e)
        {
            if (cmbThue.Text == "")
            {
                txtGiaMuaSauThue.Value = 0;
                Response.Write("<script language='JavaScript'> alert('Vui lòng chọn thuế trước.'); </script>");
            }
            else
            {
                if (txtGiaMuaSauThue.Value != null)
                {
                    dtDanhMucThue data = new dtDanhMucThue();
                    float TiLe = data.LayTiLeThue(cmbThue.Value + "") / (float)100;
                    txtGiaMuaTruocThue.Value = Int32.Parse(txtGiaMuaSauThue.Value + "") / (1 + TiLe);
                }
            }
        }

        protected void txtGiaBanTruocThue_ValueChanged(object sender, EventArgs e)
        {
            if (cmbThue.Text == "")
            {
                txtGiaBanTruocThue.Value = 0;
                Response.Write("<script language='JavaScript'> alert('Vui lòng chọn thuế trước.'); </script>");
            }
            else
            {
                if (txtGiaBanTruocThue.Value != null)
                {
                    dtDanhMucThue data = new dtDanhMucThue();
                    float TiLe = data.LayTiLeThue(cmbThue.Value + "") / (float)100;
                    txtGiaBanSauThue.Value = Int32.Parse(txtGiaBanTruocThue.Value + "") + (Int32.Parse(txtGiaBanTruocThue.Value + "") * TiLe);
                }
            }
        }

        protected void txtGiaBanSauThue_ValueChanged(object sender, EventArgs e)
        {
            if (cmbThue.Text == "")
            {
                txtGiaBanSauThue.Value = 0;
                Response.Write("<script language='JavaScript'> alert('Vui lòng chọn thuế trước.'); </script>");
            }
            else
            {
                if (txtGiaBanSauThue.Value != null)
                {
                    dtDanhMucThue data = new dtDanhMucThue();
                    float TiLe = data.LayTiLeThue(cmbThue.Value + "") / (float)100;
                    txtGiaBanTruocThue.Value = Int32.Parse(txtGiaBanSauThue.Value + "") / (1 + TiLe);
                }
            }
        }

        protected void txtGiaMuaTruocThue_ValueChanged(object sender, EventArgs e)
        {
            if (cmbThue.Text == "")
            {
                txtGiaMuaTruocThue.Value = 0;
                Response.Write("<script language='JavaScript'> alert('Vui lòng chọn thuế trước.'); </script>");
            }
            else
            {
                if (txtGiaMuaTruocThue.Value != null)
                {
                    dtDanhMucThue data = new dtDanhMucThue();
                    float TiLe = data.LayTiLeThue(cmbThue.Value + "") / (float)100;
                    txtGiaMuaSauThue.Value = Int32.Parse(txtGiaMuaTruocThue.Value + "") + (Int32.Parse(txtGiaMuaTruocThue.Value + "") * TiLe);
                }
            }
        }

        protected void btnCapNhat_Click(object sender, EventArgs e)
        {
            O_HangHoa hh = new O_HangHoa();
            dataHangHoa data = new dataHangHoa();
            string IDHangHoa = Request.QueryString["IDHangHoa"];

            if (cmbNhomHang.Value != null && txtMaHang.Value != null && txtTenHang.Value != null && cmbDonViTinh.Value != null && txtHeSo.Value != null && cmbHangSX.Value != null && cmbThue.Value != null)
            {
                if (!data.KiemTraMaHang(IDHangHoa, txtMaHang.Value + ""))
                {
                    hh.IDNhomHang = cmbNhomHang.Value + "";
                    hh.MaHang = txtMaHang.Value + "";
                    hh.TenHangHoa = txtTenHang.Value + "";
                    hh.IDDonViTinh = cmbDonViTinh.Value + "";
                    hh.HeSo = txtHeSo.Value + "";
                    hh.IDHangSanXuat = cmbHangSX.Value + "";
                    hh.IDThue = cmbThue.Value + "";
                    hh.IDNhomDatHang = cmbNhomDatHang.Value + "";
                    hh.TrongLuong = txtTrongLuong.Value + "";
                    hh.HanSuDung = txtHangSuDung.Value + "";
                    hh.IDTrangThaiHang = cmbTrangThaiHang.Value + "";
                    hh.GhiChu = txtGhiChu.Value + "";
                    hh.IDHangQuyDoi = cmbHangQuyDoi.Value + "";

                    hh.GiaMuaTruocThue = "0" + txtGiaMuaTruocThue.Value;
                    hh.GiaBanTruocThue = "0" + txtGiaBanTruocThue.Value;
                    hh.GiaMuaSauThue = "0" + txtGiaMuaSauThue.Value;
                    hh.GiaBanSauThue = "0" + txtGiaBanSauThue.Value;
                    hh.GiaBan1 = "0" + txtGiaBan1.Value;
                    hh.GiaBan2 = "0" + txtGiaBan2.Value;
                    hh.GiaBan3 = "0" + txtGiaBan3.Value;
                    hh.GiaBan4 = "0" + txtGiaBan4.Value;
                    hh.GiaBan5 = "0" + txtGiaBan5.Value;

                    DataTable da = data.getDanhSachHangHoa_ID_2(IDHangHoa);
                    string g1 = da.Rows[0]["GiaMuaTruocThue"] == null ? "0" : da.Rows[0]["GiaMuaTruocThue"].ToString();
                    string g2 = da.Rows[0]["GiaBanTruocThue"] == null ? "0" : da.Rows[0]["GiaBanTruocThue"].ToString();
                    string g3 = da.Rows[0]["GiaMuaSauThue"] == null ? "0" : da.Rows[0]["GiaMuaSauThue"].ToString();
                    string g4 = da.Rows[0]["GiaBan"] == null ? "0" : da.Rows[0]["GiaBan"].ToString();
                    string g5 = da.Rows[0]["GiaBan1"] == null ? "0" : da.Rows[0]["GiaBan1"].ToString();
                    string g6 = da.Rows[0]["GiaBan2"] == null ? "0" : da.Rows[0]["GiaBan2"].ToString();
                    string g7 = da.Rows[0]["GiaBan3"] == null ? "0" : da.Rows[0]["GiaBan3"].ToString();
                    string g8 = da.Rows[0]["GiaBan4"] == null ? "0" : da.Rows[0]["GiaBan4"].ToString();
                    string g9 = da.Rows[0]["GiaBan5"] == null ? "0" : da.Rows[0]["GiaBan5"].ToString();

                    string IDNV = Session["IDNhanVien"].ToString();

                    if (float.Parse(g1) != float.Parse(hh.GiaMuaTruocThue))
                        dtThayDoiGia.ThemLichSu(hh.MaHang, IDHangHoa, hh.IDDonViTinh, g1.ToString(), hh.GiaMuaTruocThue, IDNV, "Thay đổi giá mua trước thuế ");
                    if (float.Parse(g2) != float.Parse(hh.GiaBanTruocThue))
                        dtThayDoiGia.ThemLichSu(hh.MaHang, IDHangHoa, hh.IDDonViTinh, g2.ToString(), hh.GiaBanTruocThue, IDNV, "Thay đổi giá bán trước thuế ");
                    if (float.Parse(g3) != float.Parse(hh.GiaMuaSauThue))
                        dtThayDoiGia.ThemLichSu(hh.MaHang, IDHangHoa, hh.IDDonViTinh, g3.ToString(), hh.GiaMuaSauThue, IDNV, "Thay đổi giá mua sau thuế ");
                    if (float.Parse(g4) != float.Parse(hh.GiaBanSauThue))
                        dtThayDoiGia.ThemLichSu(hh.MaHang, IDHangHoa, hh.IDDonViTinh, g4.ToString(), hh.GiaBanSauThue, IDNV, "Thay đổi giá bán sau thuế ");
                    if (float.Parse(g5) != float.Parse(hh.GiaBan1))
                        dtThayDoiGia.ThemLichSu(hh.MaHang, IDHangHoa, hh.IDDonViTinh, g5.ToString(), hh.GiaBan1, IDNV, "Thay đổi giá bán 1");
                    if (float.Parse(g6) != float.Parse(hh.GiaBan2))
                        dtThayDoiGia.ThemLichSu(hh.MaHang, IDHangHoa, hh.IDDonViTinh, g6.ToString(), hh.GiaBan2, IDNV, "Thay đổi giá bán 2");
                    if (float.Parse(g7) != float.Parse(hh.GiaBan3))
                        dtThayDoiGia.ThemLichSu(hh.MaHang, IDHangHoa, hh.IDDonViTinh, g7.ToString(), hh.GiaBan3, IDNV, "Thay đổi giá bán 3");
                    if (float.Parse(g8) != float.Parse(hh.GiaBan4))
                        dtThayDoiGia.ThemLichSu(hh.MaHang, IDHangHoa, hh.IDDonViTinh, g8.ToString(), hh.GiaBan4, IDNV, "Thay đổi giá bán 4");
                    if (float.Parse(g9) != float.Parse(hh.GiaBan5))
                        dtThayDoiGia.ThemLichSu(hh.MaHang, IDHangHoa, hh.IDDonViTinh, g9.ToString(), hh.GiaBan5, IDNV, "Thay đổi giá bán 5");

                    data.updateHangHoa_update(IDHangHoa, hh);
                }
                else Response.Write("<script language='JavaScript'> alert('Mã hàng đã tồn tại.'); </script>");

            }
            else Response.Write("<script language='JavaScript'> alert('Các trường (*) không được bỏ trống.'); </script>");

            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Hàng hóa chi tiết", Session["IDKho"].ToString(), "Danh mục", "Cập nhật: " + IDHangHoa);

        }


        protected void gridHangHoaBarcode_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            dataHangHoa data = new dataHangHoa();
            data.deleteHangHoa_Barcode(ID);
            e.Cancel = true;
            gridHangHoaBarcode.CancelEdit();
            Load();

            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Hàng hóa barcode", Session["IDKho"].ToString(), "Danh mục", "Xóa");
        }

        protected void gridHangHoaBarcode_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            if (e.NewValues["IDTrangThaiBarcode"] != null && e.NewValues["Barcode"] != null)
            {
                string IDTrangThaiBarcode = e.NewValues["IDTrangThaiBarcode"].ToString();
                string Barcode = e.NewValues["Barcode"].ToString();

                string IDHangHoa = Request.QueryString["IDHangHoa"];
                dataHangHoa data = new dataHangHoa();
                DataTable da = data.KiemTraBarcode(Barcode);
                if (da.Rows.Count == 0)
                {
                    data.insertHangHoa_Barcode(IDHangHoa, IDTrangThaiBarcode, Barcode);
                    e.Cancel = true;
                    gridHangHoaBarcode.CancelEdit();
                }
                else throw new Exception("Barcode đã tồn tại.");


            }
            else throw new Exception("Không được bỏ trống dữ liệu.");

            Load();

            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Hàng hóa barcode", Session["IDKho"].ToString(), "Danh mục", "Thêm");
        }

        protected void gridHangHoaBarcode_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            if (e.NewValues["IDTrangThaiBarcode"] != null && e.NewValues["Barcode"] != null)
            {
                string IDTrangThaiBarcode = e.NewValues["IDTrangThaiBarcode"].ToString();
                string Barcode = e.NewValues["Barcode"].ToString();
                string ID = e.Keys[0].ToString();

                dataHangHoa data = new dataHangHoa();
                DataTable da = data.KiemTraBarcode(Barcode);
                if (da.Rows.Count == 0)
                {
                    data.updateHangHoa_Barcode(ID, IDTrangThaiBarcode, Barcode);
                    e.Cancel = true;
                    gridHangHoaBarcode.CancelEdit();
                }
                else throw new Exception("Barcode đã tồn tại.");
            }
            else throw new Exception("Không được bỏ trống dữ liệu.");

            Load();

            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Hàng hóa barcode", Session["IDKho"].ToString(), "Danh mục", "Cập nhật");
        }
        protected void gridHangHoaGiaTheoSL_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            dataHangHoa data = new dataHangHoa();
            data.deleteHangHoa_TheoSL(ID);
            e.Cancel = true;
            gridHangHoaGiaTheoSL.CancelEdit();
            Load();

            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Hàng hóa giá theo SL", Session["IDKho"].ToString(), "Danh mục", "Xóa");
        }

        protected void gridHangHoaGiaTheoSL_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            if (e.NewValues["SoLuongBD"] != null && e.NewValues["SoLuongKT"] != null && e.NewValues["GiaBan"] != null)
            {
                string SoLuongBD = e.NewValues["SoLuongBD"].ToString();
                string SoLuongKT = e.NewValues["SoLuongKT"].ToString();
                string GiaBan = e.NewValues["GiaBan"].ToString();

                string IDHangHoa = Request.QueryString["IDHangHoa"];
                dataHangHoa data = new dataHangHoa();
                data.insertHangHoa_GiaTheoSL(IDHangHoa, SoLuongBD, SoLuongKT, GiaBan);

                e.Cancel = true;
                gridHangHoaGiaTheoSL.CancelEdit();
            }
            else throw new Exception("Không được bỏ trống dữ liệu.");

            Load();
            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Hàng hóa giá theo SL", Session["IDKho"].ToString(), "Danh mục", "Thêm");
        }

        protected void gridHangHoaGiaTheoSL_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            if (e.NewValues["SoLuongBD"] != null && e.NewValues["SoLuongKT"] != null && e.NewValues["GiaBan"] != null)
            {
                string SoLuongBD = e.NewValues["SoLuongBD"].ToString();
                string SoLuongKT = e.NewValues["SoLuongKT"].ToString();
                string GiaBan = e.NewValues["GiaBan"].ToString();
                string ID = e.Keys[0].ToString();

                dataHangHoa data = new dataHangHoa();
                data.updateHangHoa_GiaTheoSL(ID, SoLuongBD, SoLuongKT, GiaBan);

                e.Cancel = true;
                gridHangHoaGiaTheoSL.CancelEdit();
            }
            else throw new Exception("Không được bỏ trống dữ liệu.");

            Load();
            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Hàng hóa giá theo SL", Session["IDKho"].ToString(), "Danh mục", "Cập nhật");
        }
    }
}