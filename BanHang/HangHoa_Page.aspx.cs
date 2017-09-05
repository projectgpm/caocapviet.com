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
                }
            }
            Load();
        }

        public void Load()
        {
            dataHangHoa data = new dataHangHoa();

            gridHangHoaBarcode.DataSource = data.GetListBarCode(IDHangHoa.Value);
            gridHangHoaBarcode.DataBind();

            //gridHangHoaGiaTheoSL.DataSource = GiaHangHoaTheoSoLuong;
            //gridHangHoaGiaTheoSL.DataBind();

            //gridHangHoaQuyDoi.DataSource = HangHoaQuyDoi;
            //gridHangHoaQuyDoi.DataBind();

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

        }

        protected void gridHangHoaQuyDoi_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {

        }

        protected void gridHangHoaQuyDoi_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {

        }

        protected void gridHangHoaGiaTheoSL_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {

        }

        protected void gridHangHoaGiaTheoSL_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {

        }

        protected void gridHangHoaGiaTheoSL_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {

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