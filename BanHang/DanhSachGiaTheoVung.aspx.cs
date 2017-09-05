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
                    
                }
                else
                {
                   
                    Response.Redirect("Default.aspx");
                   
                }
            }
        }

        private void LoadGrid(string IDKho)
        {
            data = new dtGiaTheoVung();
            gridHangHoa.DataSource = data.DanhSachHangHoa_IDKho(IDKho);
            gridHangHoa.DataBind();
        }

        protected void gridHangHoa_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string IDKho = cmbChiNhanh.Value.ToString();
            string ID = e.Keys[0].ToString();
            string GiaBan0 = e.NewValues["GiaBan"].ToString();
            string GiaBan1 = e.NewValues["GiaBan1"].ToString();
            string GiaBan2 = e.NewValues["GiaBan2"].ToString();
            string GiaBan3 = e.NewValues["GiaBan3"].ToString();
            string GiaBan4 = e.NewValues["GiaBan4"].ToString();
            string GiaBan5 = e.NewValues["GiaBan5"].ToString();
            string IDHangHoa = dtHangHoa.LayIDHangHoa_MaHang(e.NewValues["MaHang"].ToString());
            float giacu0 = dtCapNhatTonKho.GiaBan_KhoChiNhanh(IDHangHoa, IDKho);
            float giacu1 = dtCapNhatTonKho.GiaBan1_KhoChiNhanh(IDHangHoa, IDKho);
            float giacu2 = dtCapNhatTonKho.GiaBan2_KhoChiNhanh(IDHangHoa, IDKho);
            float giacu3 = dtCapNhatTonKho.GiaBan3_KhoChiNhanh(IDHangHoa, IDKho);
            float giacu4 = dtCapNhatTonKho.GiaBan4_KhoChiNhanh(IDHangHoa, IDKho);
            float giacu5 = dtCapNhatTonKho.GiaBan5_KhoChiNhanh(IDHangHoa, IDKho);
            //if (float.Parse(GiaBan0) != giacu0)
            //{
            //    dtHangHoa.ThemLichSuThayDoiGia(ID.ToString(), dtHangHoa.LayIDDonViTinh(ID.ToString()), giacu0, float.Parse(GiaBan0), Session["IDNhanVien"].ToString(), dtHangHoa.LayMaHang(ID.ToString()));
            //}
            //if (float.Parse(GiaBan1) != giacu1)
            //{
            //    dtHangHoa.ThemLichSuThayDoiGia(ID.ToString(), dtHangHoa.LayIDDonViTinh(ID.ToString()), giacu1, float.Parse(GiaBan1), Session["IDNhanVien"].ToString(), dtHangHoa.LayMaHang(ID.ToString()));
            //}
            //if (float.Parse(GiaBan2) != giacu2)
            //{
            //    dtHangHoa.ThemLichSuThayDoiGia(ID.ToString(), dtHangHoa.LayIDDonViTinh(ID.ToString()), giacu2, float.Parse(GiaBan2), Session["IDNhanVien"].ToString(), dtHangHoa.LayMaHang(ID.ToString()));
            //}
            //if (float.Parse(GiaBan3) != giacu3)
            //{
            //    dtHangHoa.ThemLichSuThayDoiGia(ID.ToString(), dtHangHoa.LayIDDonViTinh(ID.ToString()), giacu3, float.Parse(GiaBan3), Session["IDNhanVien"].ToString(), dtHangHoa.LayMaHang(ID.ToString()));
            //}
            //if (float.Parse(GiaBan4) != giacu4)
            //{
            //    dtHangHoa.ThemLichSuThayDoiGia(ID.ToString(), dtHangHoa.LayIDDonViTinh(ID.ToString()), giacu4, float.Parse(GiaBan4), Session["IDNhanVien"].ToString(), dtHangHoa.LayMaHang(ID.ToString()));
            //}
            //if (float.Parse(GiaBan5) != giacu5)
            //{
            //    dtHangHoa.ThemLichSuThayDoiGia(ID.ToString(), dtHangHoa.LayIDDonViTinh(ID.ToString()), giacu5, float.Parse(GiaBan5), Session["IDNhanVien"].ToString(), dtHangHoa.LayMaHang(ID.ToString()));
            //}
            dtGiaTheoVung.CapNhat_GiaTheoVung(ID, IDKho, GiaBan0, GiaBan1, GiaBan2, GiaBan3, GiaBan4, GiaBan5);
            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Kho", Session["IDKho"].ToString(), "Danh Mục", "Cập nhật giá theo vùng");
            e.Cancel = true;
            gridHangHoa.CancelEdit();
            LoadGrid(IDKho);
            dtLichSuTruyCap.ThemLichSu(Session["IDNhanVien"].ToString(), Session["IDNhom"].ToString(), "Danh sách giá theo vùng", Session["IDKho"].ToString(), "Danh mục", "Cập nhật");
        }

        protected void cmbChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbChiNhanh.Text != "")
            {
                string IDKho = cmbChiNhanh.Value.ToString();
                LoadGrid(IDKho);
            }
        }
    }
}