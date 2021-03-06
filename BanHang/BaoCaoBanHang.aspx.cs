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
    public partial class BaoCaoBanHang : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                //if (dtSetting.LayChucNang_ThemXoaSua(Session["IDNhom"].ToString()) == false)
                //{
                //    btnNhapExcel.Enabled = false;
                //    gridKhachHang.Columns["chucnang"].Visible = false;
                //}
                if (dtSetting.LayChucNang_HienThi(Session["IDNhom"].ToString()) == true)
                {
                    if (!IsPostBack)
                    {
                        string IDNhanVien = "1"; // Session["IDThuNgan"].ToString();
                        if (Session["IDThuNgan"] != null)
                            IDNhanVien = Session["IDThuNgan"].ToString();
                        if (Session["IDNhanVien"] != null)
                            IDNhanVien = Session["IDNhanVien"].ToString();

                        dtKho dt = new dtKho();
                        DataTable da = dt.LayDanhSachKho_TheoNV(IDNhanVien);
                        da.Rows.Add(-1, "Tất cả cửa hàng");

                        cmbKho.DataSource = da;
                        cmbKho.TextField = "TenCuaHang";
                        cmbKho.ValueField = "ID";
                        cmbKho.DataBind();
                        cmbKho.SelectedIndex = da.Rows.Count;

                        dataNganhHang dtNH = new dataNganhHang();
                        DataTable daNH = dtNH.getDanhSachNganhHang("5000000");
                        daNH.Rows.Add(-1, null, "Tất cả ngành hàng", null, null, null);
                        cmbNganhHang.DataSource = daNH;
                        cmbNganhHang.TextField = "TenNganhHang";
                        cmbNganhHang.ValueField = "ID";
                        cmbNganhHang.DataBind();
                        cmbNganhHang.SelectedIndex = daNH.Rows.Count;

                        dataNhomHang dtNhomH = new dataNhomHang();
                        DataTable daNhomH = dtNhomH.getDanhSachNhomHang("5000000");
                        daNhomH.Rows.Add(-1, 1, null, "Tất cả nhóm hàng", null, null, null);

                        cmbNhomHang.DataSource = daNhomH;
                        cmbNhomHang.TextField = "TenNhomHang";
                        cmbNhomHang.ValueField = "ID";
                        cmbNhomHang.DataBind();
                        cmbNhomHang.SelectedIndex = daNhomH.Rows.Count;

                        dataHangHoa dtHH = new dataHangHoa();
                        DataTable daHH = dtHH.getDanhSachHangHoa_Ten_ID();
                        daHH.Rows.Add(-1, "Tất cả hàng hóa");

                        cmbHangHoa.DataSource = daHH;
                        cmbHangHoa.TextField = "TenHangHoa";
                        cmbHangHoa.ValueField = "ID";
                        cmbHangHoa.DataBind();
                        cmbHangHoa.SelectedIndex = daHH.Rows.Count;
                    }
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }

            //if (!IsPostBack)
            //{
            //    string IDKho = cmbKho.Value + "";
            //    string IDHH = cmbHangHoa.Value + "";
            //    string IDNH = cmbNhomHang.Value + "";
            //    string IDNganhH = cmbNganhHang.Value + "";

            //    DateTime date = DateTime.Now;
            //    int thang = date.Month;
            //    int nam = date.Year;
            //    string ngayBD = ""; string ngayKT = "";
            //    if (rbTheoNam.Checked == true)
            //    {
            //        ngayBD = nam + "-01-01 ";
            //        ngayKT = nam + "-31-12 ";
            //    }
            //    else if (rbTheoThang.Checked == true)
            //    {
            //        ngayBD = nam + "-01-" + thang + " ";
            //        ngayKT = nam + "-" + dtSetting.tinhSoNgay(thang, nam) + "-" + thang + " ";
            //    }
            //    else if (rbTuyChon.Checked == true)
            //    {
            //        ngayBD = DateTime.Parse(dateNgayBD.Value + "").ToString("yyyy-dd-MM ");
            //        ngayKT = DateTime.Parse(dateNgayKT.Value + "").ToString("yyyy-dd-MM ");
            //    }
            //    else Response.Write("<script language='JavaScript'> alert('Hãy chọn 1 hình thức báo cáo.'); </script>");

            //    ngayBD = ngayBD + "00:00:0.000";
            //    ngayKT = ngayKT + "23:59:59.999";

            //    dtBanHangLe dtx = new dtBanHangLe();
            //    DataTable dax = dtx.DanhSachHangHoaBan(IDKho, IDNganhH, IDNH, ngayBD, ngayKT);
            //    gridDanhSach.DataSource = dax;
            //    gridDanhSach.DataBind();
            //}

        }

        protected void rbTheoNam_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTheoNam.Checked == true)
            {
                rbTuyChon.Checked = false;
                rbTheoThang.Checked = false;
                dateNgayBD.Enabled = false;
                dateNgayKT.Enabled = false;
            }
        }

        protected void rbTheoThang_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTheoThang.Checked == true)
            {
                rbTuyChon.Checked = false;
                rbTheoNam.Checked = false;
                dateNgayBD.Enabled = false;
                dateNgayKT.Enabled = false;
            }
        }

        protected void rbTuyChon_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTuyChon.Checked == true)
            {
                rbTheoThang.Checked = false;
                rbTheoNam.Checked = false;
                dateNgayBD.Enabled = true;
                dateNgayKT.Enabled = true;

                dateNgayBD.Value = DateTime.Now;
                dateNgayKT.Value = DateTime.Now;
            }
        }

        protected void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            string IDKho = cmbKho.Value + "";
            string IDHH = cmbHangHoa.Value + "";
            string IDNH = cmbNhomHang.Value + "";
            string IDNganhH = cmbNganhHang.Value + "";

            DateTime date = DateTime.Now;
            int thang = date.Month;
            int nam = date.Year;
            DateTime ngayBD = DateTime.Now; DateTime ngayKT = DateTime.Now;
            if (rbTheoNam.Checked == true)
            {
                ngayBD = DateTime.Parse("01-01-" + nam);
                ngayKT = DateTime.Parse("12-31-" + nam);
            }
            else if (rbTheoThang.Checked == true)
            {
                ngayBD = DateTime.Parse(thang + "-01-" + nam);
                ngayKT = DateTime.Parse(thang + "-" + dtSetting.tinhSoNgay(thang, nam) + "-" + nam);
            }
            else if (rbTuyChon.Checked == true)
            {
                ngayBD = DateTime.Parse(dateNgayBD.Value + "");
                ngayKT = DateTime.Parse(dateNgayKT.Value + "");
            }
            else Response.Write("<script language='JavaScript'> alert('Hãy chọn 1 hình thức báo cáo.'); </script>");

            //ngayBD = ngayBD + "00:00:0.000";
            //ngayKT = ngayKT + "23:59:59.999";

            dtBanHangLe dt = new dtBanHangLe();
            DataTable da = dt.DanhSachHangHoaBan(IDKho, IDNganhH, IDNH, ngayBD.ToString("yyyy-dd-MM 00:00:00.000"), ngayKT.ToString("yyyy-dd-MM 23:59:59.999"));
            gridDanhSach.DataSource = da;
            gridDanhSach.DataBind();

            //popup.ContentUrl = "~/BaoCaoBanHang_In.aspx?IDKho=" + IDKho + "&IDHH=" + IDHH + "&IDNH=" + IDNH + "&IDNganhH=" + IDNganhH + "&NgayBD=" + ngayBD + "&NgayKT=" + ngayKT;
            //popup.ShowOnPageLoad = true;
        }

        protected void cmbNganhHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataNhomHang dtNhomH = new dataNhomHang();
            DataTable daNhomH = dtNhomH.getDanhSachNhomHang_IDNganhHang(cmbNganhHang.Value + "");
            daNhomH.Rows.Add(-1, 1, null, "Tất cả nhóm hàng", null, null, null);

            cmbNhomHang.DataSource = daNhomH;
            cmbNhomHang.TextField = "TenNhomHang";
            cmbNhomHang.ValueField = "ID";
            cmbNhomHang.DataBind();
            cmbNhomHang.SelectedIndex = daNhomH.Rows.Count;

            dataHangHoa dtHH = new dataHangHoa();
            DataTable daHH = dtHH.getDanhSachHangHoa_IDNganhHang(cmbNganhHang.Value + "");
            daHH.Rows.Add(-1, "Tất cả hàng hóa");

            cmbHangHoa.DataSource = daHH;
            cmbHangHoa.TextField = "TenHangHoa";
            cmbHangHoa.ValueField = "ID";
            cmbHangHoa.DataBind();
            cmbHangHoa.SelectedIndex = daHH.Rows.Count;
        }

        protected void cmbNhomHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Int32.Parse(cmbNhomHang.Value + "") == -1)
            {
                dataHangHoa dtHH = new dataHangHoa();
                DataTable daHH = dtHH.getDanhSachHangHoa_IDNganhHang(cmbNganhHang.Value + "");
                daHH.Rows.Add(-1, "Tất cả hàng hóa");

                cmbHangHoa.DataSource = daHH;
                cmbHangHoa.TextField = "TenHangHoa";
                cmbHangHoa.ValueField = "ID";
                cmbHangHoa.DataBind();
                cmbHangHoa.SelectedIndex = daHH.Rows.Count;
            }
            else
            {
                dataHangHoa dtHH = new dataHangHoa();
                DataTable daHH = dtHH.getDanhSachHangHoa_IDNhomHang(cmbNhomHang.Value + "");
                daHH.Rows.Add(-1, "Tất cả hàng hóa");

                cmbHangHoa.DataSource = daHH;
                cmbHangHoa.TextField = "TenHangHoa";
                cmbHangHoa.ValueField = "ID";
                cmbHangHoa.DataBind();
                cmbHangHoa.SelectedIndex = daHH.Rows.Count;
            }

        }

        protected void btnXuatExel_Click(object sender, EventArgs e)
        {
            string IDKho = cmbKho.Value + "";
            string IDHH = cmbHangHoa.Value + "";
            string IDNH = cmbNhomHang.Value + "";
            string IDNganhH = cmbNganhHang.Value + "";

            DateTime date = DateTime.Now;
            int thang = date.Month;
            int nam = date.Year;
            DateTime ngayBD = DateTime.Now; DateTime ngayKT = DateTime.Now;
            if (rbTheoNam.Checked == true)
            {
                ngayBD = DateTime.Parse("01-01-" + nam);
                ngayKT = DateTime.Parse("12-31-" + nam);
            }
            else if (rbTheoThang.Checked == true)
            {
                ngayBD = DateTime.Parse(thang + "-01-" + nam);
                ngayKT = DateTime.Parse(thang + "-" + dtSetting.tinhSoNgay(thang, nam) + "-" + nam);
            }
            else if (rbTuyChon.Checked == true)
            {
                ngayBD = DateTime.Parse(dateNgayBD.Value + "");
                ngayKT = DateTime.Parse(dateNgayKT.Value + "");
            }
            else Response.Write("<script language='JavaScript'> alert('Hãy chọn 1 hình thức báo cáo.'); </script>");

            //ngayBD = ngayBD + "00:00:0.000";
            //ngayKT = ngayKT + "23:59:59.999";

            dtBanHangLe dt = new dtBanHangLe();
            DataTable da = dt.DanhSachHangHoaBan(IDKho, IDNganhH, IDNH, ngayBD.ToString("yyyy-dd-MM 00:00:00.000"), ngayKT.ToString("yyyy-dd-MM 23:59:59.999"));
            gridDanhSach.DataSource = da;
            gridDanhSach.DataBind();

            export.WriteXlsToResponse();
        }
    }
}