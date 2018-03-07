using System;
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
            //if (Session["KTBanLe"] == "GPMBanLe")
            //{
            //    if (dtSetting.LayChucNang_HienThi(Session["IDNhom"].ToString()) == true)
            //    {
                    if (!IsPostBack)
                    {
                        DanhSachHoaDon = new List<HoaDon>();
                        ThemHoaDonMoi();
                        btnNhanVien.Text = Session["TenThuNgan"].ToString();
                        txtBarcode.Focus();
                    }
                    DanhSachKhachHang();
                    XuLyThayDoiGiaTheoGio();
                    XuLyDonHangChiNhanh();
                    HuyDonHangThuMua();

                    dtBanHangLe dt = new dtBanHangLe();
                    ASPxGridViewInBuil.DataSource = dt.ThongTinHoaDonVuaBan(Session["IDKho"].ToString());
                    ASPxGridViewInBuil.DataBind();
            //    }
            //    else
            //    {
            //        Response.Redirect("DangNhap.aspx");
            //    }
            //}
            //else
            //{
            //    Response.Redirect("DangNhap.aspx");
            //}
        }

        //-Cập nhật đơn hàng tự động------------------------
        public void XuLyDonHangChiNhanh()
        {
            dtMasterPage dataMT = new dtMasterPage();
            DataTable db = dataMT.DanhSachDonHangChiNhanhChuaXuLy(DateTime.Now);
            if (db.Rows.Count > 0)
            {
                foreach (DataRow dr in db.Rows)
                {
                    string ID = dr["ID"].ToString();
                    string IDKho = dr["IDKhoLap"].ToString();
                    if (ID != "")
                    {

                        DataTable dt = dataMT.DanhSachChiTietDuyet(ID);
                        foreach (DataRow dr1 in dt.Rows)
                        {
                            // tự động xử lý đơn hàng chi nhánh sau 1 ngày
                            string IDHangHoa = dr1["IDHangHoa"].ToString();
                            int SoLuong = Int32.Parse(dr1["ThucTe"].ToString());
                            if (SoLuong > 0)
                            {
                                object TheKho = dtTheKho.ThemTheKho(dtDuyetDonHangChiNhanh.LaySoDonHang(ID), "Xác nhận đơn hàng tự động ", SoLuong.ToString(), "0", (Int32.Parse(dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa, IDKho).ToString()) + SoLuong).ToString(), "1", IDKho, IDHangHoa, "Nhập", "0", "0", "0", "0");
                                if (TheKho != null)
                                {
                                    dtCapNhatTonKho.CongTonKho(IDHangHoa, SoLuong.ToString(), IDKho);
                                }
                            }
                        }
                        dataMT = new dtMasterPage();
                        dataMT.CapNhatDonHangHoanTat(ID);
                    }
                }
            }
        }
        public void HuyDonHangThuMua()
        {
            dtMasterPage dataMT = new dtMasterPage();
            int SoNgayHuy = dtSetting.SoNgayHuyDonHangThuMua();
            DataTable db = dataMT.DanhSachDonHangThuMua(DateTime.Now, SoNgayHuy);
            if (db.Rows.Count > 0)
            {
                foreach (DataRow dr in db.Rows)
                {
                    string ID = dr["ID"].ToString();
                    if (ID != "")
                    {
                        //cập nhật thành đơn hàng hủy trong 7 ngày, số ngày lấy trong dtsetting
                        dataMT = new dtMasterPage();
                        dataMT.CapNhatTrangThaiHuyDonHangThuMua(ID);
                        // ghi lịch sử
                    }
                }
            }
        }
        public void XuLyThayDoiGiaTheoGio()
        {
            dtMasterPage dataMT = new dtMasterPage();
            DataTable db = dataMT.DanhSachHangHoaXuLyTheoGio(DateTime.Now);
            if (db.Rows.Count > 0)
            {
                foreach (DataRow dr in db.Rows)
                {
                    string ID = dr["ID"].ToString(); // id dòng lấy cập nhật trạng thái
                    string IDKho = dr["IDKho"].ToString();
                    float Gia0 = float.Parse(dr["GiaBan"].ToString());
                    float Gia1 = float.Parse(dr["GiaBan1"].ToString());
                    float Gia2 = float.Parse(dr["GiaBan2"].ToString());
                    float Gia3 = float.Parse(dr["GiaBan3"].ToString());
                    float Gia4 = float.Parse(dr["GiaBan4"].ToString());
                    float Gia5 = float.Parse(dr["GiaBan5"].ToString());
                    string IDHangHoa = dr["IDHangHoa"].ToString();
                    string MaHang = dtHangHoa.LayMaHang(IDHangHoa);
                    string IDDonViTinh = dtHangHoa.LayIDDonViTinh(IDHangHoa);
                    if (ID != "")
                    {
                        if (Gia0 != -1)
                        {
                            dtThayDoiGia.ThemLichSu(MaHang, IDHangHoa, IDDonViTinh, dtCapNhatTonKho.GiaBan_KhoChiNhanh(IDHangHoa, IDKho).ToString(), Gia0.ToString(), "1", "Hệ Thống Cập Nhật Giá Theo Giờ(GiaBan): Chi Nhánh " + dtSetting.LayTenKho(IDKho));
                            dataMT = new dtMasterPage();
                            dataMT.CapNhat_GiaTheoGio(IDHangHoa, IDKho, Gia0.ToString(), "GiaBan");
                        }
                        if (Gia1 != -1)
                        {
                            dtThayDoiGia.ThemLichSu(MaHang, IDHangHoa, IDDonViTinh, dtCapNhatTonKho.GiaBan1_KhoChiNhanh(IDHangHoa, IDKho).ToString(), Gia1.ToString(), "1", "Hệ Thống Cập Nhật Giá Theo Giờ(GiaBan1): Chi Nhánh " + dtSetting.LayTenKho(IDKho));
                            dataMT = new dtMasterPage();
                            dataMT.CapNhat_GiaTheoGio(IDHangHoa, IDKho, Gia1.ToString(), "GiaBan1");
                        }
                        if (Gia2 != -1)
                        {
                            dtThayDoiGia.ThemLichSu(MaHang, IDHangHoa, IDDonViTinh, dtCapNhatTonKho.GiaBan2_KhoChiNhanh(IDHangHoa, IDKho).ToString(), Gia2.ToString(), "1", "Hệ Thống Cập Nhật Giá Theo Giờ(GiaBan2): Chi Nhánh " + dtSetting.LayTenKho(IDKho));
                            dataMT = new dtMasterPage();
                            dataMT.CapNhat_GiaTheoGio(IDHangHoa, IDKho, Gia2.ToString(), "GiaBan2");
                        }
                        if (Gia3 != -1)
                        {
                            dtThayDoiGia.ThemLichSu(MaHang, IDHangHoa, IDDonViTinh, dtCapNhatTonKho.GiaBan3_KhoChiNhanh(IDHangHoa, IDKho).ToString(), Gia3.ToString(), "1", "Hệ Thống Cập Nhật Giá Theo Giờ(GiaBan3): Chi Nhánh " + dtSetting.LayTenKho(IDKho));
                            dataMT = new dtMasterPage();
                            dataMT.CapNhat_GiaTheoGio(IDHangHoa, IDKho, Gia3.ToString(), "GiaBan3");
                        }
                        if (Gia4 != -1)
                        {
                            dtThayDoiGia.ThemLichSu(MaHang, IDHangHoa, IDDonViTinh, dtCapNhatTonKho.GiaBan4_KhoChiNhanh(IDHangHoa, IDKho).ToString(), Gia4.ToString(), "1", "Hệ Thống Cập Nhật Giá Theo Giờ(GiaBan4): Chi Nhánh " + dtSetting.LayTenKho(IDKho));
                            dataMT = new dtMasterPage();
                            dataMT.CapNhat_GiaTheoGio(IDHangHoa, IDKho, Gia4.ToString(), "GiaBan4");
                        }
                        if (Gia5 != -1)
                        {
                            dtThayDoiGia.ThemLichSu(MaHang, IDHangHoa, IDDonViTinh, dtCapNhatTonKho.GiaBan5_KhoChiNhanh(IDHangHoa, IDKho).ToString(), Gia5.ToString(), "1", "Hệ Thống Cập Nhật Giá Theo Giờ(GiaBan5): Chi Nhánh " + dtSetting.LayTenKho(IDKho));
                            dataMT = new dtMasterPage();
                            dataMT.CapNhat_GiaTheoGio(IDHangHoa, IDKho, Gia5.ToString(), "GiaBan5");
                        }
                        dataMT = new dtMasterPage();
                        dataMT.CapNhatGiaHoanTat(ID);
                    }
                }
            }
        }
        //--------------------------------------------------
        public void BindGridChiTietHoaDon()
        {
            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
            UpdateSTT(MaHoaDon);
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
            string MaHang = tbThongTin.Rows[0]["MaHang"].ToString();
            int IDHangHoa = Int32.Parse(tbThongTin.Rows[0]["ID"].ToString());
            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
            var exitHang = DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.FirstOrDefault(item => item.IDHangHoa == IDHangHoa);
            if (exitHang != null)
            {
                int SoLuong = exitHang.SoLuong + int.Parse(txtSoLuong.Text);
                float ThanhTienOld = exitHang.ThanhTien;
                exitHang.SoLuong = SoLuong;
                float GiaBanSL = BanTheoSoLuong(exitHang.IDHangHoa, SoLuong);
                if (GiaBanSL != -1)
                {
                    exitHang.DonGia = GiaBanSL;
                }
                //else
                //{
                //    int KT_GiaApDung = dtBanHangLe.KT_GiaApDung(Session["IDKho"].ToString());
                //    switch (KT_GiaApDung)
                //    {
                //        case 1: exitHang.DonGia = float.Parse(tbThongTin.Rows[0]["GiaBan1"].ToString()); break;
                //        case 2: exitHang.DonGia = float.Parse(tbThongTin.Rows[0]["GiaBan2"].ToString()); break;
                //        case 3: exitHang.DonGia = float.Parse(tbThongTin.Rows[0]["GiaBan3"].ToString()); break;
                //        case 4: exitHang.DonGia = float.Parse(tbThongTin.Rows[0]["GiaBan4"].ToString()); break;
                //        case 5: exitHang.DonGia = float.Parse(tbThongTin.Rows[0]["GiaBan5"].ToString()); break;
                //        default: exitHang.DonGia = float.Parse(tbThongTin.Rows[0]["GiaBan"].ToString()); break;
                //    }
                //}
                exitHang.TonKho = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa.ToString(), Session["IDKho"].ToString());
                exitHang.ThanhTien = SoLuong * exitHang.DonGia;
                DanhSachHoaDon[MaHoaDon].TongTien += SoLuong * exitHang.DonGia - ThanhTienOld;
                DanhSachHoaDon[MaHoaDon].KhachCanTra = DanhSachHoaDon[MaHoaDon].TongTien - DanhSachHoaDon[MaHoaDon].GiamGia;
                DanhSachHoaDon[MaHoaDon].KhachThanhToan = DanhSachHoaDon[MaHoaDon].TongTien - DanhSachHoaDon[MaHoaDon].GiamGia;
            }
            else
            {
                ChiTietHoaDon cthd = new ChiTietHoaDon();
                cthd.IDHangHoa = IDHangHoa;
                cthd.MaHang = MaHang;
                cthd.TonKho = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa.ToString(), Session["IDKho"].ToString());
                cthd.TenHang = tbThongTin.Rows[0]["TenHangHoa"].ToString();
                cthd.SoLuong = int.Parse(txtSoLuong.Text);
                cthd.DonViTinh = tbThongTin.Rows[0]["TenDonViTinh"].ToString();
                //kt giá bán theo số lượng
                float GiaBanSL = BanTheoSoLuong(cthd.IDHangHoa, cthd.SoLuong);
                if (GiaBanSL != -1)
                {
                    cthd.DonGia = GiaBanSL;
                }
                else
                {
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
                }
                cthd.GiaMua = float.Parse(tbThongTin.Rows[0]["GiaMuaSauThue"].ToString());
                cthd.ThanhTien = int.Parse(txtSoLuong.Text) * float.Parse(cthd.DonGia.ToString());
                DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.Add(cthd);
                DanhSachHoaDon[MaHoaDon].SoLuongHang++;
                DanhSachHoaDon[MaHoaDon].TongTien += cthd.ThanhTien;
                DanhSachHoaDon[MaHoaDon].KhachCanTra = DanhSachHoaDon[MaHoaDon].TongTien - DanhSachHoaDon[MaHoaDon].GiamGia;
                DanhSachHoaDon[MaHoaDon].KhachThanhToan = DanhSachHoaDon[MaHoaDon].TongTien - DanhSachHoaDon[MaHoaDon].GiamGia;
            }
        }
        public float BanTheoSoLuong(int IDHangHoa, int SoLuongMua)
        {
            dtBanHangLe dt = new dtBanHangLe();
            float DonGia = -1;
            DataTable db = dt.DanhSachGiaTheoSoLuong(IDHangHoa);
            if (db.Rows.Count > 0)
            {
                DonGia = dt.LayGiaBanTheoSoLuong(IDHangHoa, SoLuongMua);
            }
            return DonGia;
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
                        // kiểm tra kho âm
                        string IDKho = Session["IDKho"].ToString();
                        if (dtSetting.KT_BanHang(Session["IDKho"].ToString()) == 1)
                        {
                            //1: bán âm , 0: bán không âm
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
                            var exitHang = DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.FirstOrDefault(item => item.IDHangHoa == IDHangHoa);
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
                                txtSoLuong.Text = SLTonKhoHienTai + "";
                                HienThiThongBao("Số lượng tồn kho không đủ bán? Vui lòng liên hệ kho tổng.");
                            }
                        }
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
                if (SoLuong != SoLuongMoi)
                {
                    if (dtSetting.KT_BanHang(IDKho) == 1)
                    {
                        //bán âm
                        int STT = Convert.ToInt32(gridChiTietHoaDon.GetRowValues(i, "STT"));
                        var exitHang = DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.FirstOrDefault(item => item.STT == STT);
                        int SoLuongOld = exitHang.SoLuong;
                        float ThanhTienOld = exitHang.ThanhTien;
                        exitHang.SoLuong = Convert.ToInt32(SoLuongMoi);
                        float GiaBanSL = BanTheoSoLuong(exitHang.IDHangHoa, exitHang.SoLuong);
                        if (GiaBanSL != -1)
                        {
                            exitHang.DonGia = GiaBanSL;
                        }
                        else
                        {
                            int KT_GiaApDung = dtBanHangLe.KT_GiaApDung(Session["IDKho"].ToString());
                            
                            switch (KT_GiaApDung)
                            {
                                case 1: exitHang.DonGia = dtCapNhatTonKho.GiaBan1_KhoChiNhanh((exitHang.IDHangHoa).ToString(), IDKho); break;
                                case 2: exitHang.DonGia = dtCapNhatTonKho.GiaBan2_KhoChiNhanh((exitHang.IDHangHoa).ToString(), IDKho); break;
                                case 3: exitHang.DonGia = dtCapNhatTonKho.GiaBan3_KhoChiNhanh((exitHang.IDHangHoa).ToString(), IDKho); break;
                                case 4: exitHang.DonGia = dtCapNhatTonKho.GiaBan4_KhoChiNhanh((exitHang.IDHangHoa).ToString(), IDKho); break;
                                case 5: exitHang.DonGia = dtCapNhatTonKho.GiaBan5_KhoChiNhanh((exitHang.IDHangHoa).ToString(), IDKho); break;
                                default: exitHang.DonGia = dtCapNhatTonKho.GiaBan_KhoChiNhanh((exitHang.IDHangHoa).ToString(), IDKho); break;
                            }
                        }
                        //exitHang.TonKho = dtCapNhatTonKho.SoLuong_TonKho(exitHang.IDHangHoa.ToString(), Session["IDKho"].ToString());
                        exitHang.ThanhTien = Convert.ToInt32(SoLuongMoi) * exitHang.DonGia;
                        DanhSachHoaDon[MaHoaDon].TongTien += exitHang.ThanhTien - ThanhTienOld;
                        DanhSachHoaDon[MaHoaDon].KhachCanTra = DanhSachHoaDon[MaHoaDon].TongTien - DanhSachHoaDon[MaHoaDon].GiamGia;
                        DanhSachHoaDon[MaHoaDon].KhachThanhToan = DanhSachHoaDon[MaHoaDon].TongTien - DanhSachHoaDon[MaHoaDon].GiamGia;
                    }
                    else
                    {
                        //không bán âm
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
                            float GiaBanSL = BanTheoSoLuong(exitHang.IDHangHoa, SoLuongOld);
                            if (GiaBanSL != -1)
                            {
                                exitHang.DonGia = GiaBanSL;
                            }
                            else
                            {
                                int KT_GiaApDung = dtBanHangLe.KT_GiaApDung(Session["IDKho"].ToString());
                                switch (KT_GiaApDung)
                                {
                                    case 1: exitHang.DonGia = dtCapNhatTonKho.GiaBan1_KhoChiNhanh((exitHang.IDHangHoa).ToString(), IDKho); break;
                                    case 2: exitHang.DonGia = dtCapNhatTonKho.GiaBan2_KhoChiNhanh((exitHang.IDHangHoa).ToString(), IDKho); break;
                                    case 3: exitHang.DonGia = dtCapNhatTonKho.GiaBan3_KhoChiNhanh((exitHang.IDHangHoa).ToString(), IDKho); break;
                                    case 4: exitHang.DonGia = dtCapNhatTonKho.GiaBan4_KhoChiNhanh((exitHang.IDHangHoa).ToString(), IDKho); break;
                                    case 5: exitHang.DonGia = dtCapNhatTonKho.GiaBan5_KhoChiNhanh((exitHang.IDHangHoa).ToString(), IDKho); break;
                                    default: exitHang.DonGia = dtCapNhatTonKho.GiaBan_KhoChiNhanh((exitHang.IDHangHoa).ToString(), IDKho); break;
                                }
                            }
                            exitHang.ThanhTien = Convert.ToInt32(SoLuongMoi) * exitHang.DonGia;
                            DanhSachHoaDon[MaHoaDon].TongTien += exitHang.ThanhTien - ThanhTienOld;
                            DanhSachHoaDon[MaHoaDon].KhachCanTra = DanhSachHoaDon[MaHoaDon].TongTien - DanhSachHoaDon[MaHoaDon].GiamGia;
                            DanhSachHoaDon[MaHoaDon].KhachThanhToan = DanhSachHoaDon[MaHoaDon].TongTien - DanhSachHoaDon[MaHoaDon].GiamGia;
                        }
                        else
                        {
                            txtSoLuong.Text = SLTonKhoHienTai + "";
                            HienThiThongBao("Số lượng tồn kho không đủ bán? Vui lòng liên hệ kho tổng.");
                        }
                    }
                }
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
                var itemToRemove =  DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.Single(r => r.STT == STT);
                DanhSachHoaDon[MaHoaDon].SoLuongHang--;
                DanhSachHoaDon[MaHoaDon].TongTien = DanhSachHoaDon[MaHoaDon].TongTien - itemToRemove.ThanhTien;
                DanhSachHoaDon[MaHoaDon].KhachCanTra = DanhSachHoaDon[MaHoaDon].TongTien - DanhSachHoaDon[MaHoaDon].GiamGia;
                DanhSachHoaDon[MaHoaDon].KhachThanhToan = DanhSachHoaDon[MaHoaDon].TongTien - DanhSachHoaDon[MaHoaDon].GiamGia;
                DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.Remove(itemToRemove);
                BindGridChiTietHoaDon();
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }
        protected void UpdateSTT(int MaHoaDon)
        {
            for (int i = 1; i <= DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.Count; i++)
            {
                DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon[i - 1].STT = i;
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
            if (DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.Count > 0)
            {
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
                //foreach (ChiTietHoaDon cthd in DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon)
                //{
                //    string IDHangHoa = cthd.IDHangHoa.ToString();
                //    string IDHangHoaQuiDoi = dtHangHoa.HangHoaQuiDoi(IDHangHoa) + "";
                //    if (IDHangHoaQuiDoi != "0")
                //    {
                //        int SLTonHangBan = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa, IDKho);
                //        int SLTonHangQuiDoi = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoaQuiDoi, IDKho);
                //        int SLHangMua = cthd.SoLuong;
                //        int HeSoB = dtHangHoa.HeSoQuyDoi(IDHangHoaQuiDoi);
                //        int HeSoA = dtHangHoa.HeSoQuyDoi(IDHangHoa);
                //        //int SoLanDoi = dtHangHoa.KTSoNguyen(SLHangMua - SLTonHangBan, HeSoB);
                //        if (SLHangMua > SLTonHangBan && SLTonHangQuiDoi > 0)
                //        {
                //            // thực hiện qui đổi

                //                dtCapNhatTonKho.TruTonKho(IDHangHoaQuiDoi, HeSoA * 1 + "", IDKho);
                //                dtCapNhatTonKho.CongTonKho(IDHangHoa, HeSoB * 1 + "", IDKho);
                //        }
                //    }
                //}
                object IDHoaDon = dt.InsertHoaDon(IDKho, IDNhanVien, IDKhachHang, DanhSachHoaDon[MaHoaDon]);
                HuyHoaDon();
                ccbKhachHang.Text = "";
                string jsInHoaDon = "window.open(\"InHoaDonBanLe.aspx?IDHoaDon=" + IDHoaDon + "\", \"PrintingFrame\");";
                ClientScript.RegisterStartupScript(this.GetType(), "Print", jsInHoaDon, true);
                txtBarcode.Focus();
            }
            else
            {
                HienThiThongBao("Danh sách hàng hóa trống !!!");
                txtBarcode.Focus();
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
                HienThiThongBao("Thêm khách hàng thành công !!");
                popupThemKhachHang.ShowOnPageLoad = false; return;
            }
            else
            {
                HienThiThongBao("Vui lòng nhập thông tin đầy đủ (*) !!"); return;
            }
        }
        public void DanhSachKhachHang()
        {
            dtKhachHang dtkh = new dtKhachHang();
            ccbKhachHang.DataSource = dtkh.LayDanhSachKhachHang(Session["IDKho"].ToString(),"5000000000");
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

        protected void ccbKhachHang_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)source;

            sqlKhachHang.SelectCommand = @"SELECT ID,TenKhachHang,DienThoai,DiaChi
                                        FROM (
	                                            select ID,TenKhachHang, DienThoai,DiaChi, row_number()over(order by MaKhachHang) as [rn] 
	                                            FROM GPM_KhachHang
	                                            WHERE ((TenKhachHang LIKE @TenKhachHang) OR (DienThoai LIKE @DienThoai) OR (DiaChi LIKE @DiaChi)) AND (IDKho = @IDKho) AND (DaXoa = 0)	
	                                        ) as st 
                                        where st.[rn] between @startIndex and @endIndex";
            sqlKhachHang.SelectParameters.Clear();
            sqlKhachHang.SelectParameters.Add("TenKhachHang", TypeCode.String, string.Format("%{0}%", e.Filter));
            sqlKhachHang.SelectParameters.Add("DienThoai", TypeCode.String, string.Format("%{0}%", e.Filter));
            sqlKhachHang.SelectParameters.Add("DiaChi", TypeCode.String, string.Format("%{0}%", e.Filter));
            sqlKhachHang.SelectParameters.Add("IDKho", TypeCode.Int32, Session["IDKho"].ToString());
            sqlKhachHang.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString());
            sqlKhachHang.SelectParameters.Add("endIndex", TypeCode.Int64, (e.EndIndex + 1).ToString());
            comboBox.DataSource = sqlKhachHang;
            comboBox.DataBind();
        }
        

        protected void ccbKhachHang_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            long value = 0;
            if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;
            sqlKhachHang.SelectCommand = @"SELECT ID,TenKhachHang,DienThoai,DiaChi
                                        FROM GPM_KhachHang
                                        WHERE (GPM_KhachHang.ID = @ID)";
            sqlKhachHang.SelectParameters.Clear();
            sqlKhachHang.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            comboBox.DataSource = sqlKhachHang;
            comboBox.DataBind();
        }

        protected void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (txtTimKiem.Text != "")
            {
                string TuKhoa = txtTimKiem.Text.ToString();
                dtBanHangLe dt = new dtBanHangLe();
                DataTable db = dt.LayThongHoaDon(TuKhoa);
                if (db.Rows.Count > 0)
                {
                     ASPxGridViewInBuil.DataSource = dt.LayThongHoaDon(TuKhoa);
                    ASPxGridViewInBuil.DataBind();
                }
                else
                {
                    txtTimKiem.Focus();
                    ASPxGridViewInBuil.DataSource = null;
                    ASPxGridViewInBuil.DataBind();
                    HienThiThongBao("Không tìm thấy dữ liệu cần tìm?");
                }
            }
            else
            {
                txtTimKiem.Focus();
                ASPxGridViewInBuil.DataSource = null;
                ASPxGridViewInBuil.DataBind();
                HienThiThongBao("Vui lòng nhập thông tin cần tìm?");
            }
        }

        protected void btnQuyDoiHangHoa_Click(object sender, EventArgs e)
        {
            Response.Redirect("BangKeBanHang.aspx");
        }

        protected void ASPxButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("ThayDoiMatKhau.aspx");
        }
        protected void btnHuyQuiDoi_Click(object sender, EventArgs e)
        {
            ClearQuiDoi();
            popupQuiDoi.ShowOnPageLoad = false;
        }
        public void ClearQuiDoi()
        {
            cmbMaHang.Focus();
            txtSoLuongQuyDoi.Text = "";
            cmbMaHang.Text = "";
            cmbHangHoaQuyDoi.Text = "";
            cmbHangHoaQuyDoi.DataSource = null;
            cmbHangHoaQuyDoi.DataBind();
        }

        protected void cmbMaHang_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            ASPxComboBox comboBox = (ASPxComboBox)source;
            SqlQuiDoi.SelectCommand = @"SELECT [ID], [MaHang], [TenHangHoa], [GiaBan] , [TenDonViTinh]
                                        FROM (
	                                        select GPM_HangHoa.ID, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa, GPM_HangHoaTonKho.GiaBan, GPM_DonViTinh.TenDonViTinh, 
	                                        row_number()over(order by GPM_HangHoa.MaHang) as [rn] 
	                                        FROM GPM_DonViTinh INNER JOIN GPM_HangHoa ON GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh 
                                                               INNER JOIN GPM_HangHoaTonKho ON GPM_HangHoaTonKho.IDHangHoa = GPM_HangHoa.ID
	                                        WHERE ((GPM_HangHoa.MaHang LIKE @MaHang) OR GPM_HangHoa.TenHangHoa LIKE @TenHang) AND (GPM_HangHoa.IDTrangThaiHang = 1 OR GPM_HangHoa.IDTrangThaiHang = 3) AND (GPM_HangHoaTonKho.IDKho = @IDKho) AND (GPM_HangHoaTonKho.DaXoa = 0)	
	                                        ) as st 
                                        where st.[rn] between @startIndex and @endIndex";
            SqlQuiDoi.SelectParameters.Clear();
            SqlQuiDoi.SelectParameters.Add("MaHang", TypeCode.String, string.Format("%{0}%", e.Filter));
            SqlQuiDoi.SelectParameters.Add("TenHang", TypeCode.String, string.Format("%{0}%", e.Filter));
            SqlQuiDoi.SelectParameters.Add("IDKho", TypeCode.Int32, Session["IDKho"].ToString());
            SqlQuiDoi.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString());
            SqlQuiDoi.SelectParameters.Add("endIndex", TypeCode.Int64, (e.EndIndex + 1).ToString());
            comboBox.DataSource = SqlQuiDoi;
            comboBox.DataBind();
        }
        protected void cmbMaHang_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            long value = 0;
            if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
                return;
            ASPxComboBox comboBox = (ASPxComboBox)source;
            SqlQuiDoi.SelectCommand = @"SELECT GPM_HangHoa.ID, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa, GPM_HangHoaTonKho.GiaBan, GPM_DonViTinh.TenDonViTinh 
                                        FROM GPM_DonViTinh INNER JOIN GPM_HangHoa ON GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh 
                                                           INNER JOIN GPM_HangHoaTonKho ON GPM_HangHoaTonKho.IDHangHoa = GPM_HangHoa.ID 
                                        WHERE (GPM_HangHoa.ID = @ID) AND (GPM_HangHoa.IDTrangThaiHang = 1 OR GPM_HangHoa.IDTrangThaiHang = 3)";
            SqlQuiDoi.SelectParameters.Clear();
            SqlQuiDoi.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
            comboBox.DataSource = SqlQuiDoi;
            comboBox.DataBind();
        }
        protected void cmbMaHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dtBanHangLe dt = new dtBanHangLe();
                if (cmbMaHang.Text.Trim() != "")
                {
                    txtTonKhoB.Text = "";
                    DataTable tbThongTin;
                    if (cmbMaHang.Value == null)
                    {
                        tbThongTin = dt.LayThongTinHangHoa(cmbMaHang.Text.ToString(), Session["IDKho"].ToString());
                    }
                    else
                    {
                        tbThongTin = dt.LayThongTinHangHoa(cmbMaHang.Value.ToString(), Session["IDKho"].ToString());
                    }

                    if (tbThongTin.Rows.Count > 0)
                    {
                        DataRow dr = tbThongTin.Rows[0];
                        int IDHangHoa = Int32.Parse(dr["ID"].ToString());
                        txtTonKhoA.Text = dtCapNhatTonKho.SoLuong_TonKho(IDHangHoa.ToString(), Session["IDKho"].ToString()).ToString();
                        //lấy danh sách hàng qui đổi, đổ vào comboboxB
                        dtHeThongQuyDoi db = new dtHeThongQuyDoi();
                        DataTable quidoi = db.DanhSachHangHoaQuiDoi(IDHangHoa.ToString());
                        if (quidoi.Rows.Count > 0)
                        {
                            cmbHangHoaQuyDoi.DataSource = quidoi;
                            cmbHangHoaQuyDoi.DataBind();
                            cmbHangHoaQuyDoi.TextField = "TenHangHoa";
                            cmbHangHoaQuyDoi.ValueField = "ID";
                            btnQuiDoi.Enabled = true;
                        }
                        else
                        {
                            ClearQuiDoi();
                            cmbMaHang.Focus();
                            HienThiThongBao("Không có hàng qui đổi?");
                        }
                    }
                    else
                    {
                        ClearQuiDoi();
                        cmbMaHang.Focus();
                        HienThiThongBao("Mã hàng không tồn tại!!");
                    }
                }
            }
            catch (Exception ex)
            {
                ClearQuiDoi();
                HienThiThongBao("Error: " + ex);
            }
        }

        protected void cmbHangHoaQuyDoi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbHangHoaQuyDoi.Text != "")
            {
                txtSoLuongQuyDoi.Text = "0";
                txtTonKhoB.Text = dtCapNhatTonKho.SoLuong_TonKho(cmbHangHoaQuyDoi.Value.ToString(), Session["IDKho"].ToString()).ToString();
            }
        }

        protected void btnQuiDoi_Click(object sender, EventArgs e)
        {
            if (txtSoLuongQuyDoi.Text != "" && cmbMaHang.Text != "" && cmbHangHoaQuyDoi.Text != "")
            {
                int SoLuongQuiDoi = Int32.Parse(txtSoLuongQuyDoi.Text.ToString());// số lượng cần qui đổi
                int SLTonQuiDoi = Int32.Parse(txtTonKhoB.Text.ToString());// số lượng tồn của mặt hàng qui đổi
                if (SoLuongQuiDoi > SLTonQuiDoi)
                {
                    txtSoLuongQuyDoi.Focus();
                    HienThiThongBao("Số lượng hàng qui đổi không đủ !!");
                }
                else if (SoLuongQuiDoi <= 0)
                {
                    txtSoLuongQuyDoi.Focus();
                    HienThiThongBao("Số lượng qui đổi phải lớn hơn 0 !!");
                }
                else
                {
                    int SLTonHienTai = Int32.Parse(txtTonKhoA.Text.ToString());// số lượng tồn của hàng hóa cần qui đổi.
                    dtHeThongQuyDoi dt = new dtHeThongQuyDoi();
                    int HeSoB = dt.LayHeSoHangHoa(cmbHangHoaQuyDoi.Value.ToString());
                    int HeSoA = dt.LayHeSoHangHoa(cmbMaHang.Value.ToString());
                    int SLDaDoi = SoLuongQuiDoi * (HeSoB / HeSoA);
                    object TheKho1 = dtTheKho.ThemTheKho("", "Qui đổi hàng hóa: " + dtTheKho.LayTenKho_ID(Session["IDKho"].ToString()), SLDaDoi.ToString(), "0", (Int32.Parse(dtCapNhatTonKho.SoLuong_TonKho(cmbMaHang.Value.ToString(), Session["IDKho"].ToString()).ToString()) + SLDaDoi).ToString(), Session["IDNhanVien"].ToString(), Session["IDKho"].ToString(), cmbMaHang.Value.ToString(), "Nhập", "0", "0", "0", "0");
                    object TheKho2 = dtTheKho.ThemTheKho("", "Qui đổi hàng hóa : " + dtTheKho.LayTenKho_ID(Session["IDKho"].ToString()), "0", SoLuongQuiDoi.ToString(), (Int32.Parse(dtCapNhatTonKho.SoLuong_TonKho(cmbHangHoaQuyDoi.Value.ToString(), Session["IDKho"].ToString()).ToString()) - SoLuongQuiDoi).ToString(), Session["IDNhanVien"].ToString(), Session["IDKho"].ToString(), cmbHangHoaQuyDoi.Value.ToString(), "Xuất", "0", "0", "0", "0");
                    if (TheKho1 != null && TheKho2 != null)
                    {
                        dtCapNhatTonKho.TruTonKho(cmbHangHoaQuyDoi.Value.ToString(), SoLuongQuiDoi.ToString(), Session["IDKho"].ToString());
                        dtCapNhatTonKho.CongTonKho(cmbMaHang.Value.ToString(), SLDaDoi.ToString(), Session["IDKho"].ToString());
                    }
                    popupQuiDoi.ShowOnPageLoad = false;
                    HienThiThongBao("Qui đổi hàng hóa thành công !!");
                }
            }
            else
            {
                //txtSoLuongQuyDoi.Focus();
                HienThiThongBao("Vui lòng nhập đầy đủ thông tin !!");
            }
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
        public string MaHang { get; set; }
        public int IDHangHoa { get; set; }
        public string TenHang { get; set; }
        public int MaDonViTinh { get; set; }
        public int TonKho { get; set; }
        public string DonViTinh { get; set; }
        public int SoLuong { get; set; }
        public float DonGia { get; set; }
        public float GiaMua { get; set; }
        public float ThanhTien { get; set; }

        public ChiTietHoaDon()
        {
            TonKho = 0;
            SoLuong = 0;
            DonGia = 0;
            ThanhTien = 0;
        }
    }
}