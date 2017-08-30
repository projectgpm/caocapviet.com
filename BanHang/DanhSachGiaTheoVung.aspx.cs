using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class DanhSachGiaTheoVung : System.Web.UI.Page
    {
        dtGiaTheoVung data = new dtGiaTheoVung();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                if (dtSetting.LayTrangThaiMenu_ChucNang(Session["IDNhom"].ToString(), 9) == 1)
                {
                    gridHangHoa.Columns["giaban"].Visible = false;
                    btnGiaTheoKho.Enabled = false;
                }
                if (dtSetting.LayTrangThaiMenu(Session["IDNhom"].ToString(), 9) == 1)
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
            data = new dtGiaTheoVung();
            gridHangHoa.DataSource = data.DanhSachHangHoa_ALL();
            gridHangHoa.DataBind();
        }

        protected void gridHangHoa_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            float GiaMoi = float.Parse(e.NewValues["GiaBan"].ToString());
            string IDKho = e.NewValues["IDKho"].ToString();
            string IDHangHoa = dtHangHoa.LayIDHangHoa_MaHang(e.NewValues["MaHang"].ToString());
            float giacu = dtCapNhatTonKho.GiaBan_KhoChiNhanh(IDHangHoa, IDKho);// lưu lại lịch sử thay đổi giá 

            dtHangHoa.ThemLichSuThayDoiGia(ID.ToString(), dtHangHoa.LayIDDonViTinh(ID.ToString()), giacu, GiaMoi, Session["IDNhanVien"].ToString(), dtHangHoa.LayMaHang(ID.ToString()));

            dtGiaTheoVung.CapNhat_GiaTheoVung(ID, IDKho, GiaMoi);
            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Kho", Session["IDKho"].ToString(), "Danh Mục", "Cập nhật giá theo vùng");
            e.Cancel = true;
            gridHangHoa.CancelEdit();
            LoadGrid();

            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Danh sách giá theo vùng", Session["IDKho"].ToString(), "Danh mục", "Cập nhật");
        }
    }
}