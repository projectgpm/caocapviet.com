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
    public partial class HangHoa_Page : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["IDHH"] != null)
                {
                    string IDHH = Request.QueryString["IDHH"].ToString();
                    dataHangHoa data = new dataHangHoa();
                    //aspGridBarcode.DataSource = data.GetListBarCode((object)IDHH);
                    //aspGridBarcode.DataBind();

                    DataTable da = data.getDanhSachHangHoa_ID(IDHH);
                    if (da.Rows.Count != 0)
                    {

                    }
                }
                else
                {
                    dataHangHoa data = new dataHangHoa();
                    IDHangHoa.Value = data.insertHangHoa_Temp() + "";

                    cmbTrangThaiHang.SelectedIndex = 0;
                    cmbNhomDatHang.SelectedIndex = 0;
                }
            }
            Load();
        }

        public void Load()
        {
            dataHangHoa data = new dataHangHoa();

            gridHangHoaBarcode.DataSource = data.GetListBarCode(IDHangHoa.Value);
            gridHangHoaBarcode.DataBind();

            gridHangHoaGiaTheoSL.DataSource = data.GetListHangHoa_GiaTheoSL(IDHangHoa.Value); ;
            gridHangHoaGiaTheoSL.DataBind();

            gridHangHoaQuyDoi.DataSource = data.GetListHangHoaQuyDoi(IDHangHoa.Value);
            gridHangHoaQuyDoi.DataBind();

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

        protected void txtGiaMuaTruocThue_ValueChanged(object sender, EventArgs e)
        {
            if (cmbThue.Text == "")
            {
                txtGiaMuaTruocThue.Value = 0;
                Response.Write("<script language='JavaScript'> alert('Vui lòng chọn thuế trước.'); </script>");
            }
            else
            {
                dtDanhMucThue data = new dtDanhMucThue();
                float TiLe = data.LayTiLeThue(cmbThue.Value + "") / (float) 100;
                txtGiaMuaSauThue.Value = Int32.Parse(txtGiaMuaTruocThue.Value + "") + (Int32.Parse(txtGiaMuaTruocThue.Value + "") * TiLe);
            }
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
                dtDanhMucThue data = new dtDanhMucThue();
                float TiLe = data.LayTiLeThue(cmbThue.Value + "") / (float)100;
                txtGiaMuaTruocThue.Value = Int32.Parse(txtGiaMuaSauThue.Value + "") / (1 + TiLe);
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
                dtDanhMucThue data = new dtDanhMucThue();
                float TiLe = data.LayTiLeThue(cmbThue.Value + "") / (float)100;
                txtGiaBanSauThue.Value = Int32.Parse(txtGiaBanTruocThue.Value + "") + (Int32.Parse(txtGiaBanTruocThue.Value + "") * TiLe);
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
                dtDanhMucThue data = new dtDanhMucThue();
                float TiLe = data.LayTiLeThue(cmbThue.Value + "") / (float)100;
                txtGiaBanTruocThue.Value = Int32.Parse(txtGiaBanSauThue.Value + "") / (1 + TiLe);
            }
        }

        protected void gridHangHoaBarcode_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            if (e.NewValues["IDTrangThaiBarcode"] != null && e.NewValues["Barcode"] != null)
            {
                string IDTrangThaiBarcode = e.NewValues["IDTrangThaiBarcode"].ToString();
                string Barcode = e.NewValues["Barcode"].ToString();
                string ID = e.Keys[0].ToString();

                dataHangHoa data = new dataHangHoa();
                data.updateHangHoa_Barcode(ID, IDTrangThaiBarcode, Barcode);

                e.Cancel = true;
                gridHangHoaBarcode.CancelEdit();
            }
            else throw new Exception("Không được bỏ trống dữ liệu.");

            Load();
        }

        protected void gridHangHoaBarcode_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {

            if (e.NewValues["IDTrangThaiBarcode"] != null && e.NewValues["Barcode"] != null)
            {
                string IDTrangThaiBarcode = e.NewValues["IDTrangThaiBarcode"].ToString();
                string Barcode = e.NewValues["Barcode"].ToString();

                dataHangHoa data = new dataHangHoa();
                data.insertHangHoa_Barcode(IDHangHoa.Value + "",IDTrangThaiBarcode, Barcode);

                e.Cancel = true;
                gridHangHoaBarcode.CancelEdit();
            }
            else throw new Exception("Không được bỏ trống dữ liệu.");

            Load();
        }

        protected void gridHangHoaBarcode_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            dataHangHoa data = new dataHangHoa();
            data.deleteHangHoa_Barcode(ID);
            e.Cancel = true;
            gridHangHoaBarcode.CancelEdit();
            Load();
        }

        protected void gridHangHoaQuyDoi_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            dataHangHoa data = new dataHangHoa();
            data.deleteHangHoaQuyDoi(ID);
            e.Cancel = true;
            gridHangHoaQuyDoi.CancelEdit();
            Load();
        }

        protected void gridHangHoaQuyDoi_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            if (e.NewValues["MaHang"] != null)
            {
                string MaHang = e.NewValues["MaHang"].ToString();
                dataHangHoa data = new dataHangHoa();
                DataTable daMaHang = data.getHangHoa_MaHang(MaHang);
                if (daMaHang.Rows.Count != 0)
                {
                    data.insertHangHoa_QuyDoi(IDHangHoa.Value + "", daMaHang.Rows[0]["ID"].ToString());
                    e.Cancel = true;
                    gridHangHoaQuyDoi.CancelEdit();
                }
                else throw new Exception("Mã hàng không tồn tại.");
            }
            else throw new Exception("Không được bỏ trống dữ liệu.");

            Load();
        }

        protected void gridHangHoaQuyDoi_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            if (e.NewValues["MaHang"] != null)
            {
                string MaHang = e.NewValues["MaHang"].ToString();
                string ID = e.Keys[0].ToString();
                dataHangHoa data = new dataHangHoa();
                DataTable daMaHang = data.getHangHoa_MaHang(MaHang);
                if (daMaHang.Rows.Count != 0)
                {
                    data.updateHangHoa_QuyDoi(ID, daMaHang.Rows[0]["ID"].ToString());
                    e.Cancel = true;
                    gridHangHoaQuyDoi.CancelEdit();
                }
                else throw new Exception("Mã hàng không tồn tại.");
            }
            else throw new Exception("Không được bỏ trống dữ liệu.");

            Load();
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
        }

        protected void gridHangHoaGiaTheoSL_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            if (e.NewValues["SoLuongBD"] != null && e.NewValues["SoLuongKT"] != null && e.NewValues["GiaBan"] != null)
            {
                string SoLuongBD = e.NewValues["SoLuongBD"].ToString();
                string SoLuongKT = e.NewValues["SoLuongKT"].ToString();
                string GiaBan = e.NewValues["GiaBan"].ToString();

                dataHangHoa data = new dataHangHoa();
                data.insertHangHoa_GiaTheoSL(IDHangHoa.Value + "", SoLuongBD, SoLuongKT, GiaBan);

                e.Cancel = true;
                gridHangHoaGiaTheoSL.CancelEdit();
            }
            else throw new Exception("Không được bỏ trống dữ liệu.");

            Load();
        }

        protected void gridHangHoaGiaTheoSL_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            dataHangHoa data = new dataHangHoa();
            data.deleteHangHoa_TheoSL(ID);
            e.Cancel = true;
            gridHangHoaGiaTheoSL.CancelEdit();
            Load();
        }

        protected void btnHuy_Click(object sender, EventArgs e)
        {
            dataHangHoa data = new dataHangHoa();
            data.XoaHangHoa_Delete(IDHangHoa.Value + "");
            data.XoaHangHoaQuyDoi_Delete(IDHangHoa.Value + "");
            data.XoaHangHoaBarCode_Delete(IDHangHoa.Value + "");
            data.XoaHangHoaGiaTheoSL_Delete(IDHangHoa.Value + "");

            Response.Redirect("HangHoa.aspx");
        }

        protected void btnLuuHangHoa_Click(object sender, EventArgs e)
        {
            O_HangHoa hh = new O_HangHoa();

            if (cmbNhomHang.Value != null && txtMaHang.Value != null && txtTenHang.Value != null && cmbDonViTinh.Value != null && txtHeSo.Value != null && cmbHangSX.Value != null && cmbThue.Value != null)
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

                hh.GiaMuaTruocThue = txtGiaMuaTruocThue.Value + "";
                hh.GiaBanTruocThue = txtGiaBanTruocThue.Value + "";
                hh.GiaMuaSauThue = txtGiaMuaSauThue.Value + "";
                hh.GiaBanSauThue = txtGiaBanSauThue.Value + "";
                hh.GiaBan1 = txtGiaBan1.Value + "";
                hh.GiaBan2 = txtGiaBan2.Value + "";
                hh.GiaBan3 = txtGiaBan3.Value + "";
                hh.GiaBan4 = txtGiaBan4.Value + "";
                hh.GiaBan5 = txtGiaBan5.Value + "";

                dataHangHoa data = new dataHangHoa();
                data.updateHangHoa(IDHangHoa.Value + "", hh);

                Response.Redirect("HangHoa.aspx");
            }
            else Response.Write("<script language='JavaScript'> alert('Các trường (*) không được bỏ trống.'); </script>");
        }
    }

    //[Serializable]
    //public class HangHoaBarCode
    //{
    //    public int STT { get; set; }
    //    public int IDTrangThaiBarcode { get; set; }
    //    public string Barcode { get; set; }
    //    public DateTime NgayCapNhat { get; set; }
    //    public HangHoaBarCode(int stt, int IDTT, string bc)
    //    {
    //        STT = 1;
    //        IDTrangThaiBarcode = 1;
    //        Barcode = bc;
    //        NgayCapNhat = DateTime.Now;
    //    }
    //}

    //[Serializable]
    //public class HangHoaQuyDoi
    //{
    //    public int STT { get; set; }
    //    public int MaHang { get; set; }
    //    public int IDHangHoa { get; set; }
    //    public int IDDonViTinh { get; set; }
    //    public int HeSo { get; set; }
    //    public HangHoaQuyDoi()
    //    {
    //        STT = 1;
    //        MaHang = 1;
    //        IDHangHoa = 1;
    //        IDDonViTinh = 1;
    //        HeSo = 1;
    //    }
    //}

    //[Serializable]
    //public class GiaHangHoaTheoSoLuong
    //{
    //    public int STT { get; set; }
    //    public int SL1 { get; set; }
    //    public int SL2 { get; set; }
    //    public float GiaBan { get; set; }
    //    public DateTime NgayCapNhat { get; set; }
    //    public GiaHangHoaTheoSoLuong()
    //    {
    //        STT = 1;
    //        SL1 = 1;
    //        SL2 = 1;
    //        GiaBan = 1;
    //        NgayCapNhat = DateTime.Now;
    //    }
    //}
}