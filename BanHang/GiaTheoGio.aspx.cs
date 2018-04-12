using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class GiaTheoGio : System.Web.UI.Page
    {
        dtGiaTheoGio data = new dtGiaTheoGio();
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
                    Loadgrid();
                    if (dtSetting.LayChucNang_ThemXoaSua(Session["IDNhom"].ToString()) == false)
                        btnThayDoiGia.Enabled = false;
                }
                else
                    Response.Redirect("Default.aspx");
            }
        }

        private void Loadgrid()
        {
            data = new dtGiaTheoGio();
            gridHangHoa.DataSource = data.DanhSach();
            gridHangHoa.DataBind();
        }

        protected void gridHangHoa_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            data = new dtGiaTheoGio();
            data.Xoa(ID);
            e.Cancel = true;
            gridHangHoa.CancelEdit();
            Loadgrid();
        }

        protected void gridHangHoa_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string ID = e.Keys[0].ToString();
            if (e.NewValues["GiaBan"] != null)
            {
                string GiaBan0 = e.NewValues["GiaBan"].ToString();
                data = new dtGiaTheoGio();
                data.CapNhatChiTiet(ID, GiaBan0);
            }
            else
            {
                throw new Exception("Lỗi: Giá không được bỏ trống? Vui lòng kiểm tra lại.");
            }
            e.Cancel = true;
            gridHangHoa.CancelEdit();
            Loadgrid();
        }
    }
}