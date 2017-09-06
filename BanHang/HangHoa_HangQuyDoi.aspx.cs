﻿using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class HangHoa_HangQuyDoi : System.Web.UI.Page
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
            gridHangHoaQuyDoi.DataSource = data.GetListHangHoaQuyDoi(IDHangHoa);
            gridHangHoaQuyDoi.DataBind();
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
                string IDHangHoa = Request.QueryString["IDHangHoa"];
                dataHangHoa data = new dataHangHoa();
                DataTable daMaHang = data.getHangHoa_MaHang(MaHang);
                if (daMaHang.Rows.Count != 0)
                {
                    if (!dtHangHoa.KiemTraMaHang_HangQuyDoi(IDHangHoa, daMaHang.Rows[0]["ID"].ToString()))
                    {
                        data.insertHangHoa_QuyDoi(IDHangHoa, daMaHang.Rows[0]["ID"].ToString());
                        e.Cancel = true;
                        gridHangHoaQuyDoi.CancelEdit();
                    }
                    else throw new Exception("Mã hàng đã có trong danh sách.");
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
                string IDHangHoa = Request.QueryString["IDHangHoa"];
                string ID = e.Keys[0].ToString();
                dataHangHoa data = new dataHangHoa();
                DataTable daMaHang = data.getHangHoa_MaHang(MaHang);
                if (daMaHang.Rows.Count != 0)
                {
                    if (!dtHangHoa.KiemTraMaHang_HangQuyDoi(IDHangHoa, daMaHang.Rows[0]["ID"].ToString()))
                    {
                        data.updateHangHoa_QuyDoi(ID, daMaHang.Rows[0]["ID"].ToString());
                        e.Cancel = true;
                        gridHangHoaQuyDoi.CancelEdit();
                    }
                    else throw new Exception("Mã hàng đã có trong danh sách.");
                }
                else throw new Exception("Mã hàng không tồn tại.");
            }
            else throw new Exception("Không được bỏ trống dữ liệu.");

            Load();
        }
    }
}