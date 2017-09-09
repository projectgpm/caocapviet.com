using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class ChiTietDonHangXuLyTrong2Ngay : System.Web.UI.Page
    {
        dtCapNhatPhieuNhapHang data = new dtCapNhatPhieuNhapHang();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] == "GPM")
            {
                string IDDonHang = Request.QueryString["IDDonHang"];
                if (IDDonHang != null)
                {

                    LoadGrid(IDDonHang.ToString());
                }
            }
            else
            {
                Response.Redirect("DangNhap.aspx");
            }
        }
        private void LoadGrid(string IDDonHangThuMua)
        {
            data = new dtCapNhatPhieuNhapHang();
            gridChiTiet.DataSource = data.DanhSachChiTiet_Duyet_ThuMua(IDDonHangThuMua);
            gridChiTiet.DataBind();
        }
        protected void gridChiTiet_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {

            Color color = (Color)ColorTranslator.FromHtml("#FF9797");
            int ChenhLech = Convert.ToInt32(e.GetValue("ChenhLech"));
            if (ChenhLech > 0)
                e.Row.BackColor = color;
        }

        protected void gridChiTiet_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            if (e.NewValues["ThucTe"] == null)
            {
                throw new Exception("Lỗi: Số lượng thực tế không được bỏ trống. ");
            }
            else
            {
                string IDDonHang = Request.QueryString["IDDonHang"];
                string ID = e.Keys[0].ToString();
                int SoLuongThucTe = Int32.Parse(e.NewValues["ThucTe"].ToString());
                int SoLuong = Int32.Parse(e.NewValues["SoLuong"].ToString());
                if (SoLuongThucTe >= 0)
                {
                    // xử lý cập nhật ở đây
                    //if (SoLuongThucTe > SoLuong)
                    //{
                    //    throw new Exception("Lỗi: Số lượng thực tế phải nhỏ hơn hoặc bằng Số lượng đặt ");
                    //}
                    //else
                    //{
                    //    string MaHang = e.NewValues["MaHang"].ToString();
                    //    string IDHangHoa = dtHangHoa.LayIDHangHoa_MaHang(MaHang.Trim());
                    //    data = new dtDuyetDonHangThuMua();
                    //    data.CapNhatChiTietDonHang(ID, IDHangHoa, IDTemp, SoLuongThucTe, SoLuong - SoLuongThucTe);
                    //}
                }
                else
                {
                    throw new Exception("Lỗi: Số lượng không được âm? Vui lòng kiểm tra lại. ");
                }
                e.Cancel = true;
                gridChiTiet.CancelEdit();
                LoadGrid(IDDonHang.ToString());
            }
        }
    }
}