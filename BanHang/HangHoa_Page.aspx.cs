using BanHang.Data;
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
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    dataHangHoa data = new dataHangHoa();
                    DataTable da = data.getHangHoa_Null();
                    if (da.Rows.Count != 0)
                    {
                        IDHangHoa.Value = da.Rows[0]["ID"].ToString();
                        txtMaHang.Value = da.Rows[0]["MaHang"].ToString();
                    }
                    else IDHangHoa.Value = data.insertHangHoa_Temp() + "";
                    cmbTrangThaiHang.SelectedIndex = 0;
                    cmbNhomDatHang.SelectedIndex = 0;
                }
                Load();
            }
        }
        public void Load()
        {
            dataHangHoa data = new dataHangHoa();

            DataTable da = data.getDanhSachHangHoa_Ten_ID();
            da.Rows.Add(0, "000000", "Không quy đổi");
            cmbHangQuyDoi.DataSource = da;
            cmbHangQuyDoi.TextField = "TenHangHoa";
            cmbHangQuyDoi.ValueField = "ID";
            cmbHangQuyDoi.DataBind();
            cmbHangQuyDoi.SelectedIndex = da.Rows.Count;

            gridHangHoaBarcode.DataSource = data.GetListBarCode(IDHangHoa.Value);
            gridHangHoaBarcode.DataBind();
            
            gridHangHoaGiaTheoSL.DataSource = data.GetListHangHoa_GiaTheoSL(IDHangHoa.Value);
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
        }

        protected void gridHangHoaBarcode_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {

            if (e.NewValues["IDTrangThaiBarcode"] != null && e.NewValues["Barcode"] != null)
            {
                string IDTrangThaiBarcode = e.NewValues["IDTrangThaiBarcode"].ToString();
                string Barcode = e.NewValues["Barcode"].ToString();

                dataHangHoa data = new dataHangHoa();
                DataTable da = data.KiemTraBarcode(Barcode);
                if (da.Rows.Count == 0)
                {
                    data.insertHangHoa_Barcode(IDHangHoa.Value + "", IDTrangThaiBarcode, Barcode);

                    e.Cancel = true;
                    gridHangHoaBarcode.CancelEdit();
                }
                else throw new Exception("Barcode đã tồn tại.");
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
                hh.IDHangQuyDoi = cmbHangQuyDoi.Value + "";

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
                if (!data.KiemTraMaHang(IDHangHoa.Value + "", hh.MaHang))
                    data.updateHangHoa(IDHangHoa.Value + "", hh);
                else Response.Write("<script language='JavaScript'> alert('Mã hàng đã tồn tại'); </script>");

                Response.Redirect("HangHoa.aspx");
            }
            else Response.Write("<script language='JavaScript'> alert('Các trường (*) không được bỏ trống.'); </script>");
        }

        protected void cmbNhomHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataHangHoa data = new dataHangHoa();

            DataTable da = data.getDanhSachHangHoa_Ten_ID();
            da.Rows.Add(0, "000000", "Không quy đổi");
            cmbHangQuyDoi.DataSource = da;
            cmbHangQuyDoi.TextField = "TenHangHoa";
            cmbHangQuyDoi.ValueField = "ID";
            cmbHangQuyDoi.DataBind();
            cmbHangQuyDoi.SelectedIndex = da.Rows.Count;

            dataNhomHang da1 = new dataNhomHang();
            cmbNganhHang.Value = da1.LayIDNganhHang_ID(cmbNhomHang.Value + "");
        }
    }
}