﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using BanHang.Data;
using System.Data;

namespace BanHang
{
    public partial class BanHangLe1 : System.Web.UI.Page
    {
        public List<HoaDon> DanhSachHoaDon
        {
            get
            {
                if (ViewState["DanhSachHoaDon"] == null)
                    return new List<HoaDon>();
                else
                    return (List<HoaDon>)ViewState["DanhSachHoaDon"];
            }
            set
            {
                ViewState["DanhSachHoaDon"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            dtBanHangLe dt = new dtBanHangLe();
            ASPxGridViewInBuil.DataSource = dt.LayThongHoaDon();
            ASPxGridViewInBuil.DataBind();
            txtBarcode.Focus();
            //if (Session["KTBanLe"] == "GPMBanLe")
            //{
            //    if (!IsPostBack)
            //    {
            //        DanhSachHoaDon = new List<HoaDon>();
            //        ThemHoaDonMoi();
            //        ConnectServer.updateDatabase();
            //    }
            //    btnNhanVien.Text = Session["TenThuNgan"].ToString();                
            //}
            //else
            //{
            //    Response.Redirect("DangNhap.aspx");
            //}
            if (Session["KTBanLe"] == "GPMBanLe")
            {
                if (!IsPostBack)
                {
                    DanhSachHoaDon = new List<HoaDon>();
                    ThemHoaDonMoi();
                    btnNhanVien.Text = Session["TenThuNgan"].ToString();
                }
                DanhSachKhachHang();
            }
            else
            {
                Response.Redirect("DangNhap.aspx");
            }
        }

        public void BindGridChiTietHoaDon()
        {
            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
            gridChiTietHoaDon.DataSource = DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon;
            gridChiTietHoaDon.DataBind();
            formLayoutThanhToan.DataSource = DanhSachHoaDon[MaHoaDon];
            formLayoutThanhToan.DataBind();
        }

        public void ThemHoaDonMoi()
        {
            HoaDon hd = new HoaDon();
            DanhSachHoaDon.Add(hd);
            Tab tabHoaDonNew = new Tab();
            int SoHoaDon = DanhSachHoaDon.Count;
            tabHoaDonNew.Name = SoHoaDon.ToString();
            tabHoaDonNew.Text = "Hóa đơn " + SoHoaDon.ToString();
            tabHoaDonNew.Index = SoHoaDon - 1;
            tabControlSoHoaDon.Tabs.Add(tabHoaDonNew);
            tabControlSoHoaDon.ActiveTabIndex = SoHoaDon - 1;
            // txtTienThua.Text = "";
            //  txtKhachThanhToan.Text = "";
            BindGridChiTietHoaDon();
        }
        public void HuyHoaDon()
        {
            txtTienThua.Text = "";
            txtKhachThanhToan.Text = "";
            int indexTabActive = tabControlSoHoaDon.ActiveTabIndex;
            DanhSachHoaDon.RemoveAt(indexTabActive);
            tabControlSoHoaDon.Tabs.RemoveAt(indexTabActive);
            for (int i = 0; i < tabControlSoHoaDon.Tabs.Count; i++)
            {
                tabControlSoHoaDon.Tabs[i].Text = "Hóa đơn " + (i + 1).ToString();
            }
            if (DanhSachHoaDon.Count == 0)
            {
                ThemHoaDonMoi();
            }
            else
            {
                BindGridChiTietHoaDon();
            }
        }
        public void ThemHangVaoChiTietHoaDon(DataTable tbThongTin)
        {
            int MaHang = int.Parse(tbThongTin.Rows[0]["MaHang"].ToString());
            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
            var exitHang = DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.FirstOrDefault(item => item.MaHang == MaHang);
            if (exitHang != null)
            {
                int SoLuong = exitHang.SoLuong + int.Parse(txtSoLuong.Text);
                float ThanhTienOld = exitHang.ThanhTien;
                exitHang.SoLuong = SoLuong;
                exitHang.ThanhTien = SoLuong * exitHang.DonGia;
                DanhSachHoaDon[MaHoaDon].TongTien += SoLuong * exitHang.DonGia - ThanhTienOld;
                DanhSachHoaDon[MaHoaDon].KhachCanTra = DanhSachHoaDon[MaHoaDon].TongTien - DanhSachHoaDon[MaHoaDon].GiamGia;
            }
            else
            {
                ChiTietHoaDon cthd = new ChiTietHoaDon();
                cthd.STT = DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.Count + 1;
                cthd.MaHang = MaHang;
                cthd.TenHang = tbThongTin.Rows[0]["TenHangHoa"].ToString();
                cthd.SoLuong = int.Parse(txtSoLuong.Text);
                cthd.DonViTinh = tbThongTin.Rows[0]["TenDonViTinh"].ToString();
                //cthd.DonGia = float.Parse(tbThongTin.Rows[0]["GiaBan"].ToString());
                int KT_GiaApDung = dtBanHangLe.KT_GiaApDung(Session["IDKho"].ToString());
                switch (KT_GiaApDung)
                {
                    case 1: cthd.DonGia = float.Parse(tbThongTin.Rows[0]["GiaBan1"].ToString()); break;
                    case 2: cthd.DonGia = float.Parse(tbThongTin.Rows[0]["GiaBan2"].ToString()); break;
                    case 3: cthd.DonGia = float.Parse(tbThongTin.Rows[0]["GiaBan3"].ToString()); break;
                    case 4: cthd.DonGia = float.Parse(tbThongTin.Rows[0]["GiaBan4"].ToString()); break;
                    case 5: cthd.DonGia = float.Parse(tbThongTin.Rows[0]["GiaBan5"].ToString()); break;
                    default: cthd.DonGia = float.Parse(tbThongTin.Rows[0]["GiaBan"].ToString()); break;
                }
                cthd.GiaMua = float.Parse(tbThongTin.Rows[0]["GiaMuaSauThue"].ToString());
                cthd.ThanhTien = int.Parse(txtSoLuong.Text) * float.Parse(cthd.DonGia.ToString());

                DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.Add(cthd);
                DanhSachHoaDon[MaHoaDon].SoLuongHang++;
                DanhSachHoaDon[MaHoaDon].TongTien += cthd.ThanhTien;
                DanhSachHoaDon[MaHoaDon].KhachCanTra = DanhSachHoaDon[MaHoaDon].TongTien - DanhSachHoaDon[MaHoaDon].GiamGia;
            }
        }

        protected void btnInsertHang_Click(object sender, EventArgs e)
        {
            try
            {
                dtBanHangLe dt = new dtBanHangLe();
                if (txtBarcode.Text.Trim() != "")
                {
                    DataTable tbThongTin;
                    if (txtBarcode.Value == null)
                    {
                        tbThongTin = dt.LayThongTinHangHoa(txtBarcode.Text.ToString(), Session["IDKho"].ToString());
                    }
                    else
                    {
                        tbThongTin = dt.LayThongTinHangHoa(txtBarcode.Value.ToString(), Session["IDKho"].ToString());
                    }

                    if (tbThongTin.Rows.Count > 0)
                    {
                        //ban đầu
                        //ThemHangVaoChiTietHoaDon(tbThongTin);
                        //BindGridChiTietHoaDon();
                        //endbandau

                        //==============================
                        // kiểm tra kho âm
                        // Bắt đầu Sửa
                        string IDKho = Session["IDKho"].ToString();
                        if (dtSetting.KT_BanHang(Session["IDKho"].ToString()) == 1)
                        {
                            DataRow dr = tbThongTin.Rows[0];
                            int IDHangHoa = Int32.Parse(dr["ID"].ToString());
                            int SLMua = Int32.Parse(txtSoLuong.Text.ToString());
                            int SLTonKhoHienTai = 0;
                            SLTonKhoHienTai = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa.ToString(), IDKho);
                            int MaHang = int.Parse(tbThongTin.Rows[0]["MaHang"].ToString());
                            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
                            var exitHang = DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.FirstOrDefault(item => item.MaHang == MaHang);
                            if (exitHang != null)
                            {
                                int SoLuong = exitHang.SoLuong;
                                SLTonKhoHienTai = SLTonKhoHienTai - SoLuong;
                            }
                            if (SLTonKhoHienTai < SLMua)
                            {
                                if (dtHangHoa.HangHoaQuiDoi(IDHangHoa.ToString()) != IDHangHoa)
                                {
                                    int IDHangHoaQuiDoi = dtHangHoa.HangHoaQuiDoi(IDHangHoa.ToString());
                                    int ToKhoQuiDoi = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoaQuiDoi.ToString(), IDKho);
                                    int HeSo = dtHangHoa.HeSoQuyDoi(IDHangHoaQuiDoi.ToString());
                                    int TongQuiDoi = (ToKhoQuiDoi * HeSo) + SLTonKhoHienTai;
                                    if (TongQuiDoi >= SLMua)
                                    {
                                        HienThiThongBao("Cảnh báo: Hàng hóa đang qui đổi?");
                                    }
                                    else
                                    {
                                        HienThiThongBao("Cảnh báo: Hàng quy đổi không đủ số lượng: Tổng qui đổi:" + TongQuiDoi);
                                    }
                                }
                                else
                                {
                                    HienThiThongBao("Số lượng tồn kho hiện tại không đủ bán");
                                }
                            }
                            ThemHangVaoChiTietHoaDon(tbThongTin);
                            BindGridChiTietHoaDon();
                        }
                        else
                        {
                            DataRow dr = tbThongTin.Rows[0];
                            int IDHangHoa = Int32.Parse(dr["ID"].ToString());
                            int SLMua = Int32.Parse(txtSoLuong.Text.ToString());
                            int SLTonKhoHienTai = 0;
                            SLTonKhoHienTai = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa.ToString(), IDKho);


                            // lấy sl có trong lưới
                            int MaHang = int.Parse(tbThongTin.Rows[0]["MaHang"].ToString());
                            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
                            var exitHang = DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.FirstOrDefault(item => item.MaHang == MaHang);
                            if (exitHang != null)
                            {
                                int SoLuong = exitHang.SoLuong;
                                SLTonKhoHienTai = SLTonKhoHienTai - SoLuong;
                            }

                            if (SLTonKhoHienTai >= SLMua)
                            {
                                ThemHangVaoChiTietHoaDon(tbThongTin);
                                BindGridChiTietHoaDon();
                            }
                            else
                            {
                                if (dtHangHoa.HangHoaQuiDoi(IDHangHoa.ToString()) != IDHangHoa)
                                {
                                    int IDHangHoaQuiDoi = dtHangHoa.HangHoaQuiDoi(IDHangHoa.ToString());
                                    int ToKhoQuiDoi = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoaQuiDoi.ToString(), IDKho);
                                    int HeSo = dtHangHoa.HeSoQuyDoi(IDHangHoaQuiDoi.ToString());
                                    int TongQuiDoi = (ToKhoQuiDoi * HeSo) + SLTonKhoHienTai;
                                    if (TongQuiDoi >= SLMua)
                                    {
                                        ThemHangVaoChiTietHoaDon(tbThongTin);
                                        BindGridChiTietHoaDon();
                                        HienThiThongBao("Cảnh báo: Hàng hóa đang qui đổi?");
                                    }
                                    else
                                    {
                                        txtSoLuong.Text = TongQuiDoi + "";
                                        HienThiThongBao("Cảnh báo: Hàng quy đổi không đủ số lượng: Tổng qui đổi:" + TongQuiDoi);
                                    }
                                }
                                else
                                {
                                    txtSoLuong.Text = SLTonKhoHienTai + "";
                                    HienThiThongBao("Số lượng tồn kho không đủ bán? Vui lòng liên hệ kho tổng");

                                }
                            }
                        }
                        //end Luân Sửa
                    }
                    else
                        HienThiThongBao("Mã hàng không tồn tại!!");
                }
                txtBarcode.Focus();
                txtBarcode.Text = "";
                txtBarcode.Value = "";
                txtSoLuong.Text = "1";
            }
            catch (Exception ex)
            {
                HienThiThongBao("Error: " + ex);
            }
        }
        public void HienThiThongBao(string thongbao)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + thongbao + "');", true);
        }
        protected void btnUpdateGridHang_Click(object sender, EventArgs e)
        {
            BatchUpdate();
            BindGridChiTietHoaDon();
        }

        private void BatchUpdate()
        {
            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
            string IDKho = Session["IDKho"].ToString();
            for (int i = 0; i < gridChiTietHoaDon.VisibleRowCount; i++)
            {
                object SoLuong = gridChiTietHoaDon.GetRowValues(i, "SoLuong");
                ASPxSpinEdit spineditSoLuong = gridChiTietHoaDon.FindRowCellTemplateControl(i, (GridViewDataColumn)gridChiTietHoaDon.Columns["SoLuong"], "txtSoLuongChange") as ASPxSpinEdit;
                object SoLuongMoi = spineditSoLuong.Value;
                //int STT = Convert.ToInt32(gridChiTietHoaDon.GetRowValues(i, "STT"));
                //var exitHang = DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.FirstOrDefault(item => item.STT == STT);
                //int SoLuongOld = exitHang.SoLuong;
                //float ThanhTienOld = exitHang.ThanhTien;
                //exitHang.SoLuong = Convert.ToInt32(SoLuongMoi);
                //exitHang.ThanhTien = Convert.ToInt32(SoLuongMoi) * exitHang.DonGia;
                //DanhSachHoaDon[MaHoaDon].TongTien += exitHang.ThanhTien - ThanhTienOld;
                //DanhSachHoaDon[MaHoaDon].KhachCanTra = DanhSachHoaDon[MaHoaDon].TongTien - DanhSachHoaDon[MaHoaDon].GiamGia;

                //sửa bán âm 
                if (SoLuong != SoLuongMoi)
                {
                    if (dtSetting.KT_BanHang(IDKho) == 1)
                    {
                        int MaHang = Convert.ToInt32(gridChiTietHoaDon.GetRowValues(i, "MaHang"));
                        dtHangHoa dtt = new dtHangHoa();
                        int IDHangHoa = Int32.Parse(dtHangHoa.LayIDHangHoa_MaHang(MaHang + ""));
                        int SLTonKhoHienTai = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa.ToString(), IDKho);
                        if (SLTonKhoHienTai < Int32.Parse(SoLuongMoi + ""))
                        {
                            if (dtHangHoa.HangHoaQuiDoi(IDHangHoa.ToString()) != IDHangHoa)
                            {
                                int IDHangHoaQuiDoi = dtHangHoa.HangHoaQuiDoi(IDHangHoa.ToString());
                                int HeSo = dtHangHoa.HeSoQuyDoi(IDHangHoaQuiDoi.ToString());
                                int ToKhoQuiDoi = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoaQuiDoi.ToString(), IDKho);
                                int TongQuiDoi = (ToKhoQuiDoi * HeSo) + SLTonKhoHienTai;
                                if (TongQuiDoi >= Int32.Parse(SoLuongMoi + ""))
                                {
                                    HienThiThongBao("Cảnh báo: Hàng hóa đang qui đổi?");
                                }
                                else
                                {
                                    HienThiThongBao("Cảnh báo: Hàng quy đổi không đủ số lượng: Tổng qui đổi:" + TongQuiDoi);
                                }
                            }
                            else
                            {
                                HienThiThongBao("Số lượng tồn kho hiện tại không đủ bán");
                            }
                        }
                        int STT = Convert.ToInt32(gridChiTietHoaDon.GetRowValues(i, "STT"));
                        var exitHang = DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.FirstOrDefault(item => item.STT == STT);
                        int SoLuongOld = exitHang.SoLuong;
                        float ThanhTienOld = exitHang.ThanhTien;
                        exitHang.SoLuong = Convert.ToInt32(SoLuongMoi);
                        exitHang.ThanhTien = Convert.ToInt32(SoLuongMoi) * exitHang.DonGia;
                        DanhSachHoaDon[MaHoaDon].TongTien += exitHang.ThanhTien - ThanhTienOld;
                        DanhSachHoaDon[MaHoaDon].KhachCanTra = DanhSachHoaDon[MaHoaDon].TongTien - DanhSachHoaDon[MaHoaDon].GiamGia;
                    }
                    else
                    {
                        int MaHang = Convert.ToInt32(gridChiTietHoaDon.GetRowValues(i, "MaHang"));
                        dtHangHoa dtt = new dtHangHoa();
                        int IDHangHoa = Int32.Parse(dtHangHoa.LayIDHangHoa_MaHang(MaHang + ""));
                        int SLTonKhoHienTai = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa.ToString(), IDKho);

                        if (SLTonKhoHienTai >= Int32.Parse(SoLuongMoi + ""))
                        {
                            int STT = Convert.ToInt32(gridChiTietHoaDon.GetRowValues(i, "STT"));
                            var exitHang = DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.FirstOrDefault(item => item.STT == STT);
                            int SoLuongOld = exitHang.SoLuong;
                            float ThanhTienOld = exitHang.ThanhTien;
                            exitHang.SoLuong = Convert.ToInt32(SoLuongMoi);
                            exitHang.ThanhTien = Convert.ToInt32(SoLuongMoi) * exitHang.DonGia;
                            DanhSachHoaDon[MaHoaDon].TongTien += exitHang.ThanhTien - ThanhTienOld;
                            DanhSachHoaDon[MaHoaDon].KhachCanTra = DanhSachHoaDon[MaHoaDon].TongTien - DanhSachHoaDon[MaHoaDon].GiamGia;
                        }
                        else
                        {
                            if (dtHangHoa.HangHoaQuiDoi(IDHangHoa.ToString()) != IDHangHoa)
                            {
                                int IDHangHoaQuiDoi = dtHangHoa.HangHoaQuiDoi(IDHangHoa.ToString());
                                int ToKhoQuiDoi = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoaQuiDoi.ToString(), IDKho);
                                int HeSo = dtHangHoa.HeSoQuyDoi(IDHangHoaQuiDoi.ToString());
                                int TongQuiDoi = (ToKhoQuiDoi * HeSo) + SLTonKhoHienTai;
                                if (TongQuiDoi >= Int32.Parse(SoLuongMoi.ToString()))
                                {
                                    int STT = Convert.ToInt32(gridChiTietHoaDon.GetRowValues(i, "STT"));
                                    var exitHang = DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.FirstOrDefault(item => item.STT == STT);
                                    int SoLuongOld = exitHang.SoLuong;
                                    float ThanhTienOld = exitHang.ThanhTien;
                                    exitHang.SoLuong = Convert.ToInt32(SoLuongMoi);
                                    exitHang.ThanhTien = Convert.ToInt32(SoLuongMoi) * exitHang.DonGia;
                                    DanhSachHoaDon[MaHoaDon].TongTien += exitHang.ThanhTien - ThanhTienOld;
                                    DanhSachHoaDon[MaHoaDon].KhachCanTra = DanhSachHoaDon[MaHoaDon].TongTien - DanhSachHoaDon[MaHoaDon].GiamGia;
                                    HienThiThongBao("Cảnh báo: Hàng hóa đang qui đổi?");
                                }
                                else
                                {
                                    HienThiThongBao("Cảnh báo: Hàng quy đổi không đủ số lượng: Tổng qui đổi:" + TongQuiDoi);
                                }
                            }
                            else
                            {
                                txtSoLuong.Text = SLTonKhoHienTai + "";
                                HienThiThongBao("Số lượng tồn kho không đủ bán? Vui lòng liên hệ kho tổng");

                            }
                        }
                    }

                }
                //end sửa bán âm
            }
        }

        protected void txtKhachThanhToan_TextChanged(object sender, EventArgs e)
        {
            txtTienThua.Text = "";
            float TienKhachThanhToan;
            bool isNumeric = float.TryParse(txtKhachThanhToan.Text, out TienKhachThanhToan);
            if (!isNumeric)
            {
                HienThiThongBao("Nhập không đúng số tiền !!"); return;
            }
            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
            DanhSachHoaDon[MaHoaDon].KhachThanhToan = TienKhachThanhToan;
            DanhSachHoaDon[MaHoaDon].TienThua = TienKhachThanhToan - DanhSachHoaDon[MaHoaDon].TongTien;
            txtTienThua.Text = DanhSachHoaDon[MaHoaDon].TienThua.ToString();

        }

        protected void BtnXoaHang_Click(object sender, EventArgs e)
        {
            try
            {
                int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
                int STT = Convert.ToInt32(((ASPxButton)sender).CommandArgument);
                var itemToRemove = DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.Single(r => r.STT == STT);
                DanhSachHoaDon[MaHoaDon].SoLuongHang--;
                DanhSachHoaDon[MaHoaDon].TongTien = DanhSachHoaDon[MaHoaDon].TongTien - itemToRemove.ThanhTien;
                DanhSachHoaDon[MaHoaDon].KhachCanTra = DanhSachHoaDon[MaHoaDon].TongTien - DanhSachHoaDon[MaHoaDon].GiamGia;
                DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.Remove(itemToRemove);
                BindGridChiTietHoaDon();
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }

        protected void btnHuyHoaDon_Click(object sender, EventArgs e)
        {
            HuyHoaDon();
        }

        protected void btnThemHoaDon_Click(object sender, EventArgs e)
        {
            ThemHoaDonMoi();
        }

        protected void tabControlSoHoaDon_ActiveTabChanged(object source, TabControlEventArgs e)
        {
            BindGridChiTietHoaDon();
        }

        protected void btnThanhToan_Click(object sender, EventArgs e)
        {
            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
            float TienKhachThanhToan;
            bool isNumeric = float.TryParse(txtKhachThanhToan.Text, out TienKhachThanhToan);
            if (!isNumeric)
            {
                HienThiThongBao("Nhập không đúng số tiền !!"); return;
            }
            if (TienKhachThanhToan < DanhSachHoaDon[MaHoaDon].KhachCanTra)
            {
                HienThiThongBao("Thanh toán chưa đủ số tiền !!"); return;
            }
            DanhSachHoaDon[MaHoaDon].KhachThanhToan = TienKhachThanhToan;
            dtBanHangLe dt = new dtBanHangLe();
            string IDKho = Session["IDKho"].ToString();
            string IDNhanVien = Session["IDThuNgan"].ToString();
            string IDKhachHang = "1";
            if (ccbKhachHang.Value != null)
                IDKhachHang = ccbKhachHang.Value.ToString();
            if (dtSetting.KT_BanHang(IDKho) == 1)
            {
                foreach (ChiTietHoaDon cthd in DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon)
                {
                    string IDHangHoa = dtHangHoa.LayIDHangHoa_MaHang(cthd.MaHang + "");
                    string IDHangHoaQuiDoi = dtHangHoa.HangHoaQuiDoi(IDHangHoa) + "";

                    int SLTonHangBan = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa, IDKho);
                    int SLTonHangQuiDoi = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoaQuiDoi, IDKho);
                    int SLHangMua = cthd.SoLuong;
                    if (SLHangMua > SLTonHangBan)
                    {
                        // thực hiện qui đổi
                        int HeSo = dtHangHoa.HeSoQuyDoi(IDHangHoaQuiDoi);
                        int SoLanDoi = dtHangHoa.KTSoNguyen(SLHangMua - SLTonHangBan, HeSo);
                        dtCapNhatTonKho.TruTonKho(IDHangHoaQuiDoi, SoLanDoi + "", IDKho);
                        dtCapNhatTonKho.CongTonKho(IDHangHoa, HeSo * SoLanDoi + "", IDKho);
                    }
                }
                object IDHoaDon = dt.InsertHoaDon(IDKho, IDNhanVien, IDKhachHang, DanhSachHoaDon[MaHoaDon]);
                HuyHoaDon();
                ccbKhachHang.Text = "";
                string jsInHoaDon = "window.open(\"InHoaDonBanLe.aspx?IDHoaDon=" + IDHoaDon + "\", \"PrintingFrame\");";
                ClientScript.RegisterStartupScript(this.GetType(), "Print", jsInHoaDon, true);
            }
            else
            {
                // bán hàng không đc âm
                int kt = 0;
                foreach (ChiTietHoaDon cthd in DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon)
                {
                    string IDHangHoa = dtHangHoa.LayIDHangHoa_MaHang(cthd.MaHang + "");
                    string IDHangHoaQuiDoi = dtHangHoa.HangHoaQuiDoi(IDHangHoa) + "";
                    int SLTonHangBan = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa, IDKho);
                    int SLTonHangQuiDoi = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoaQuiDoi, IDKho);
                    int SLHangMua = cthd.SoLuong;
                    if (SLHangMua > SLTonHangBan)
                    {
                        // thực hiện qui đổi
                        int HeSo = dtHangHoa.HeSoQuyDoi(IDHangHoaQuiDoi);
                        int SoLanDoi = dtHangHoa.KTSoNguyen(SLHangMua - SLTonHangBan, HeSo);
                        if (SoLanDoi > SLTonHangQuiDoi)
                        {
                            kt = 1;
                        }
                        // trường hợp không âm chưa làm xong
                    }
                }
                if (kt == 0)
                {
                    foreach (ChiTietHoaDon cthd in DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon)
                    {
                        string IDHangHoa = dtHangHoa.LayIDHangHoa_MaHang(cthd.MaHang + "");
                        string IDHangHoaQuiDoi = dtHangHoa.HangHoaQuiDoi(IDHangHoa) + "";
                        int HeSo = dtHangHoa.HeSoQuyDoi(IDHangHoaQuiDoi);
                        int SLHangMua = cthd.SoLuong;
                        int SLTonHangBan = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa, IDKho);
                        int SoLanDoi = dtHangHoa.KTSoNguyen(SLHangMua - SLTonHangBan, HeSo);
                        dtCapNhatTonKho.TruTonKho(IDHangHoaQuiDoi, SoLanDoi + "", IDKho);
                        dtCapNhatTonKho.CongTonKho(IDHangHoa, HeSo * SoLanDoi + "", IDKho);
                    }
                    object IDHoaDon = dt.InsertHoaDon(IDKho, IDNhanVien, IDKhachHang, DanhSachHoaDon[MaHoaDon]);
                    HuyHoaDon();
                    ccbKhachHang.Text = "";
                    string jsInHoaDon = "window.open(\"InHoaDonBanLe.aspx?IDHoaDon=" + IDHoaDon + "\", \"PrintingFrame\");";
                    ClientScript.RegisterStartupScript(this.GetType(), "Print", jsInHoaDon, true);
                }
                else
                {
                    HienThiThongBao("Hàng qui đổi không đủ?"); return;
                }
            }
        }

        protected void btnHuyKhachHang_Click(object sender, EventArgs e)
        {
            popupThemKhachHang.ShowOnPageLoad = false;
        }

        protected void ASPxButton1_Click(object sender, EventArgs e)
        {
            popupThemKhachHang.ShowOnPageLoad = true;
        }

        protected void btnThemKhachHang_Click(object sender, EventArgs e)
        {
            if (cmbNhomKhachHang.Text != "" && txtTenKhachHang.Text != "")
            {
                int IDNhom = Int32.Parse(cmbNhomKhachHang.Value.ToString());
                string TenKH = txtTenKhachHang.Text;
                string SDT = txtSoDienThoai.Text == null ? "" : txtSoDienThoai.Text;
                string DC = txtDiaChi.Text == null ? "" : txtDiaChi.Text;
                dtKhachHang dtkh = new dtKhachHang();
                string MaKh = "";
                string Barcode = "";
                object ID = dtkh.ThemKhachHang(IDNhom, MaKh, TenKH, DateTime.Now, "", DC, SDT, "", Barcode, "", Session["IDKho"].ToString());

                if (ID != null)
                {
                    dtkh = new dtKhachHang();
                    dtkh.CapNhatMaKhachHang(ID, (Session["IDKho"].ToString() + "." + ID).ToString(), (Session["IDKho"].ToString() + "." + ID).Replace(".", ""));
                }
                DanhSachKhachHang();
                txtTenKhachHang.Text = "";
                cmbNhomKhachHang.Text = "";
                txtSoDienThoai.Text = "";
                txtDiaChi.Text = "";
                HienThiThongBao("Thêm khách hàng thành công !!"); return;
                popupThemKhachHang.ShowOnPageLoad = false;
            }
            else
            {
                HienThiThongBao("Vui lòng nhập thông tin đầy đủ !!"); return;
            }
        }
        public void DanhSachKhachHang()
        {
            dtKhachHang dtkh = new dtKhachHang();
            ccbKhachHang.DataSource = dtkh.LayDanhSachKhachHang(Session["IDKho"].ToString());
            ccbKhachHang.TextField = "TenKhachHang";
            ccbKhachHang.ValueField = "ID";
            ccbKhachHang.DataBind();
        }

        protected void txtBarcode_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)source;

            dsHangHoa.SelectCommand = @"SELECT [ID], [MaHang], [TenHangHoa], [GiaBan] , [TenDonViTinh]
                                        FROM (
	                                        select GPM_HangHoa.ID, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa, GPM_HangHoaTonKho.GiaBan, GPM_DonViTinh.TenDonViTinh, 
	                                        row_number()over(order by GPM_HangHoa.MaHang) as [rn] 
	                                        FROM GPM_DonViTinh INNER JOIN GPM_HangHoa ON GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh 
                                                               INNER JOIN GPM_HangHoaTonKho ON GPM_HangHoaTonKho.IDHangHoa = GPM_HangHoa.ID
	                                        WHERE ((GPM_HangHoa.MaHang LIKE @MaHang) OR GPM_HangHoa.TenHangHoa LIKE @TenHang) AND (GPM_HangHoa.IDTrangThaiHang = 1 OR GPM_HangHoa.IDTrangThaiHang = 3 OR GPM_HangHoa.IDTrangThaiHang = 6) AND (GPM_HangHoaTonKho.IDKho = @IDKho) AND (GPM_HangHoaTonKho.DaXoa = 0)	
	                                        ) as st 
                                        where st.[rn] between @startIndex and @endIndex";

            dsHangHoa.SelectParameters.Clear();
            dsHangHoa.SelectParameters.Add("MaHang", TypeCode.String, string.Format("%{0}%", e.Filter));
            dsHangHoa.SelectParameters.Add("TenHang", TypeCode.String, string.Format("%{0}%", e.Filter));
            dsHangHoa.SelectParameters.Add("IDKho", TypeCode.Int32, Session["IDKho"].ToString());
            dsHangHoa.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString());
            dsHangHoa.SelectParameters.Add("endIndex", TypeCode.Int64, (e.EndIndex + 1).ToString());
            comboBox.DataSource = dsHangHoa;
            comboBox.DataBind();
        }

        protected void txtBarcode_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            long value = 0;
            if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;
            dsHangHoa.SelectCommand = @"SELECT GPM_HangHoa.ID, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa, GPM_HangHoaTonKho.GiaBan, GPM_DonViTinh.TenDonViTinh 
                                        FROM GPM_DonViTinh INNER JOIN GPM_HangHoa ON GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh 
                                                           INNER JOIN GPM_HangHoaTonKho ON GPM_HangHoaTonKho.IDHangHoa = GPM_HangHoa.ID 
                                        WHERE (GPM_HangHoa.ID = @ID) AND (GPM_HangHoa.IDTrangThaiHang = 1 OR GPM_HangHoa.IDTrangThaiHang = 3 OR GPM_HangHoa.IDTrangThaiHang = 6)";

            dsHangHoa.SelectParameters.Clear();
            dsHangHoa.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            comboBox.DataSource = dsHangHoa;
            comboBox.DataBind();
        }



    }
    [Serializable]
    public class HoaDon
    {
        public int IDHoaDon { get; set; }
        public int SoLuongHang { get; set; }
        public float TongTien { get; set; }
        public float GiamGia { get; set; }
        public float KhachCanTra { get; set; }
        public float KhachThanhToan { get; set; }
        public float TienThua { get; set; }
        public List<ChiTietHoaDon> ListChiTietHoaDon { get; set; }
        public HoaDon()
        {
            SoLuongHang = 0;
            TongTien = 0;
            GiamGia = 0;
            KhachCanTra = 0;
            KhachThanhToan = 0;
            TienThua = 0;
            ListChiTietHoaDon = new List<ChiTietHoaDon>();
        }
    }
    [Serializable]
    public class ChiTietHoaDon
    {
        public int STT { get; set; }
        public int MaHang { get; set; }
        public string TenHang { get; set; }
        public int MaDonViTinh { get; set; }
        public string DonViTinh { get; set; }
        public int SoLuong { get; set; }
        public float DonGia { get; set; }
        public float GiaMua { get; set; }
        public float ThanhTien { get; set; }

        public ChiTietHoaDon()
        {
            SoLuong = 0;
            DonGia = 0;
            ThanhTien = 0;
        }
    }
}