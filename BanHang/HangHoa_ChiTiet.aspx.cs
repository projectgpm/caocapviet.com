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
    public partial class HangHoa_ChiTiet : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Load();
            }
        }

        public void Load()
        {
            dataHangHoa data = new dataHangHoa();
            string IDHangHoa = Request.QueryString["IDHangHoa"];

            DataTable da = data.getDanhSachHangHoa_ID_2(IDHangHoa);
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

            txtGiaMuaTruocThue.Value = da.Rows[0]["GiaMuaTruocThue"].ToString();
            txtGiaBanTruocThue.Value = da.Rows[0]["GiaBanTruocThue"].ToString();
            txtGiaMuaSauThue.Value = da.Rows[0]["GiaMuaSauThue"].ToString();
            txtGiaBanSauThue.Value = da.Rows[0]["GiaBan"].ToString();
            txtGiaBan1.Value = da.Rows[0]["GiaBan1"].ToString();
            txtGiaBan2.Value = da.Rows[0]["GiaBan2"].ToString();
            txtGiaBan3.Value = da.Rows[0]["GiaBan3"].ToString();
            txtGiaBan4.Value = da.Rows[0]["GiaBan4"].ToString();
            txtGiaBan5.Value = da.Rows[0]["GiaBan5"].ToString();

        }

        protected void btnLuuHangHoa_Click(object sender, EventArgs e)
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

                    hh.GiaMuaTruocThue = txtGiaMuaTruocThue.Value + "";
                    hh.GiaBanTruocThue = txtGiaBanTruocThue.Value + "";
                    hh.GiaMuaSauThue = txtGiaMuaSauThue.Value + "";
                    hh.GiaBanSauThue = txtGiaBanSauThue.Value + "";
                    hh.GiaBan1 = txtGiaBan1.Value + "";
                    hh.GiaBan2 = txtGiaBan2.Value + "";
                    hh.GiaBan3 = txtGiaBan3.Value + "";
                    hh.GiaBan4 = txtGiaBan4.Value + "";
                    hh.GiaBan5 = txtGiaBan5.Value + "";

                    data.updateHangHoa_update(IDHangHoa, hh);
                }
                else Response.Write("<script language='JavaScript'> alert('Mã hàng đã tồn tại.'); </script>");

            }
            else Response.Write("<script language='JavaScript'> alert('Các trường (*) không được bỏ trống.'); </script>");
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
                float TiLe = data.LayTiLeThue(cmbThue.Value + "") / (float)100;
                txtGiaMuaSauThue.Value = Int32.Parse(txtGiaMuaTruocThue.Value + "") + (Int32.Parse(txtGiaMuaTruocThue.Value + "") * TiLe);
            }
        }
    }
}