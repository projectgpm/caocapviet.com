using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class TonKhoBanDau : System.Web.UI.Page
    {
        dtKhoHang data = new dtKhoHang();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                if (dtSetting.LayChucNang_HienThi(Session["IDNhom"].ToString()) == true)
                {
                    if (Session["IDNhom"].ToString() != "1")
                        gridTonKhoBanDau.Columns["chucnang"].Visible = false;
                    LoadGrid(cmbHienThi.Value.ToString());
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }
        private void LoadGrid(string HienThi)
        {
            data = new dtKhoHang();
            gridTonKhoBanDau.DataSource = data.LayDanhSachHangTrongKho(Session["IDKho"].ToString(), HienThi);
            gridTonKhoBanDau.DataBind();
        }

        protected void cmbHienThi_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid(cmbHienThi.Value.ToString());
        }

        protected void gridTonKhoBanDau_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            //string ID = e.Keys[0].ToString();
            string IDHangHoa = e.NewValues["IDHangHoa"].ToString();
            string SoLuong  = e.NewValues["SoLuongCon"].ToString();
            int SoLuongTon = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa, Session["IDKho"].ToString());
            string TrangThai = "Không Thay Đổi";
            if ((Int32.Parse(SoLuong) - SoLuongTon) > 0)
                TrangThai = "Nhập";
            else
                TrangThai = "Xuất";
            object TheKho = dtTheKho.ThemTheKho("", "Nhân Viên Điều Chỉnh Stock", "0", "0", SoLuong, Session["IDNhanVien"].ToString(), Session["IDKho"].ToString(), IDHangHoa, TrangThai, "0", "0", "0", SoLuong);
            if (TheKho != null)
            {
                dtCapNhatTonKho.CapNhatKho(IDHangHoa, SoLuong, Session["IDKho"].ToString());
            }
           
            e.Cancel = true;
            gridTonKhoBanDau.CancelEdit();
            LoadGrid(cmbHienThi.Value.ToString());
        }

        //protected void btnXuatExel_Click(object sender, EventArgs e)
        //{
        //    dataHangHoa da = new dataHangHoa();
        //    HangHoa.DataSource = da.getDanhSachHangHoa_Export();
        //    HangHoa.DataBind();

        //    gridHangHoa.WriteXlsToResponse();

        //}

        //protected void btnXuatPDF_Click(object sender, EventArgs e)
        //{
        //    dataHangHoa da = new dataHangHoa();
        //    HangHoa.DataSource = da.getDanhSachHangHoa_Export();
        //    HangHoa.DataBind();
        //    gridHangHoa.WritePdfToResponse();

        //}

        protected void btnXuatExcel_Click(object sender, EventArgs e)
        {
            XuatDuLieu.WriteXlsToResponse();
        }

        protected void btnXuatPDF_Click(object sender, EventArgs e)
        {
            XuatDuLieu.WritePdfToResponse();
        }
    }
}