using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using BanHang.Object;

namespace BanHang.Data
{
    public class dtBanHangLe
    {
        public float LayGiaBanTheoSoLuong(int IDHangHoa, int SLMua)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT GiaBan FROM GPM_HangHoa_GiaTheoSL WHERE IDHangHoa = '" + IDHangHoa + "' AND '" + SLMua + "' >= SoLuongBD AND '" + SLMua + "' <= SoLuongKT AND DaXoa = 0";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return float.Parse(dr["GiaBan"].ToString().Trim());
                    }
                    return -1;
                }
            }
        }
        public DataTable DanhSachGiaTheoSoLuong(int IDHangHoa)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT GiaBan FROM GPM_HangHoa_GiaTheoSL WHERE IDHangHoa = '" + IDHangHoa + "' AND DaXoa = 0";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable tb = new DataTable();
                        tb.Load(reader);
                        return tb;
                    }
                }
            }
        }
        public void ThemHangQuyDoi(int IDHangHoaQuiDoi, int SoLuong, int SoLuongCon, string IDKho, string IDNguoiDung, int IDHangHoa, int MaHoaDon)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_LOG_QuiDoi] ([IDHangHoaQuiDoi],[IDHangHoa],[SoLuong],[IDKho],[SoLuongCon],[IDNguoiDung],[MaHoaDon]) VALUES (@IDHangHoaQuiDoi,@IDHangHoa,@SoLuong,@IDKho,@SoLuongCon,@IDNguoiDung,@MaHoaDon)";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@MaHoaDon", MaHoaDon);
                        myCommand.Parameters.AddWithValue("@IDHangHoaQuiDoi", IDHangHoaQuiDoi);
                        myCommand.Parameters.AddWithValue("@IDNguoiDung", IDNguoiDung);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@IDKho", IDKho);
                        myCommand.Parameters.AddWithValue("@SoLuongCon", SoLuongCon);
                        myCommand.ExecuteNonQuery();
                    }
                    myConnection.Close();
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình thêm dữ liệu gặp lỗi");
                }
            }
        }
        public void CapNhatHangQuyDoi(int IDHangHoaQuiDoi, int SoLuong, int SoLuongCon, string IDKho, string IDNguoiDung, int IDHangHoa)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_LOG_QuiDoi] SET [SoLuong] = @SoLuong , [SoLuongCon] = @SoLuongCon WHERE [IDHangHoaQuiDoi] = @IDHangHoaQuiDoi  AND  [IDHangHoa] = @IDHangHoa AND [IDKho] = @IDKho AND [IDNguoiDung] = @IDNguoiDung";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDHangHoaQuiDoi", IDHangHoaQuiDoi);
                        myCommand.Parameters.AddWithValue("@IDNguoiDung", IDNguoiDung);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@SoLuong", SoLuong);
                        myCommand.Parameters.AddWithValue("@IDKho", IDKho);
                        myCommand.Parameters.AddWithValue("@SoLuongCon", SoLuongCon);
                        myCommand.ExecuteNonQuery();
                    }
                    myConnection.Close();
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình thêm dữ liệu gặp lỗi");
                }
            }
        }
        public DataTable LayThongHoaDon(string TuKhoa)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "select TOP 10 GPM_HoaDon.ID,GPM_HoaDon.[MaHoaDon],GPM_KhachHang.TenKhachHang,GPM_HoaDon.NgayBan from GPM_HoaDon,GPM_KhachHang WHERE GPM_HoaDon.IDKhachHang = GPM_KhachHang.ID AND (GPM_HoaDon.[MaHoaDon] = '" + TuKhoa + "' OR GPM_KhachHang.TenKhachHang = N'" + TuKhoa + "') ORDER BY GPM_HoaDon.ID DESC";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable tb = new DataTable();
                        tb.Load(reader);
                        return tb;
                    }
                }
            }
        }

        public DataTable LayThongHoaDon_BaoCao(string NgayBD, string NgayKT, string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "select GPM_ChiTietHoaDon.GiaMua,GPM_ChiTietHoaDon.GiaBan,GPM_ChiTietHoaDon.SoLuong from GPM_ChiTietHoaDon,GPM_HoaDon where GPM_ChiTietHoaDon.IDHangHoa = GPM_HoaDon.ID and GPM_HoaDon.IDKho = '" + IDKho + "' and GPM_HoaDon.NgayBan >= '" + NgayBD + "' and GPM_HoaDon.NgayBan <= '" + NgayKT + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable tb = new DataTable();
                        tb.Load(reader);
                        return tb;
                    }
                }
            }
        }

        public DataTable LayThongChiTietHoaDon_ID(string IDHoaDon)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "select GPM_ChiTietHoaDon.GiaBan,GPM_ChiTietHoaDon.SoLuong,GPM_ChiTietHoaDon.ThanhTien,GPM_HangHoa.TenHangHoa,GPM_HangHoa.MaHang,GPM_DonViTinh.TenDonViTinh from GPM_ChiTietHoaDon,GPM_HangHoa,GPM_DonViTinh WHERE GPM_HangHoa.ID = GPM_ChiTietHoaDon.IDHangHoa AND GPM_HangHoa.IDDonViTinh = GPM_DonViTinh.ID AND GPM_ChiTietHoaDon.IDHoaDon = " + IDHoaDon;
                using (SqlCommand command = new SqlCommand(cmdText, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable tb = new DataTable();
                        tb.Load(reader);
                        return tb;
                    }
                }
            }
        }

        public static int KT_GiaApDung(string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT GiaApDung FROM [GPM_Kho] WHERE [ID]  =  '" + IDKho + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return Int32.Parse(dr["GiaApDung"].ToString());
                    }
                    else return 0;
                }
            }
        }
        public DataTable LayThongTinHangHoa(string Barcode, string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "";
                cmdText = "SELECT HH.ID, HH.TenHangHoa,HH.MaHang, Dvi.TenDonViTinh, HHTK.GiaBan,HHTK.GiaBan1,HHTK.GiaBan2,HHTK.GiaBan3,HHTK.GiaBan4,HHTK.GiaBan5, HH.GiaMuaSauThue " +
                                 "FROM GPM_HangHoa AS HH " +
                                 "INNER JOIN GPM_HangHoaTonKho AS HHTK ON HH.ID = HHTK.IDHangHoa " +
                                 "INNER JOIN GPM_DonViTinh as DVi ON HH.IDDonViTinh = DVi.ID " +
                                 "LEFT OUTER JOIN GPM_HangHoa_Barcode AS BC ON HHTK.IDHangHoa = BC.IDHangHoa " +
                                 "WHERE (BC.Barcode = @Barcode OR CONVERT(NVARCHAR(250), HHTK.IDHangHoa) = @Barcode) AND (HH.IDTrangThaiHang = 1 OR HH.IDTrangThaiHang = 3 OR HH.IDTrangThaiHang = 6) AND HHTK.IDKho = @IDKho AND HHTK.DaXoa = 0";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                {
                    command.Parameters.AddWithValue("@Barcode", Barcode);
                    command.Parameters.AddWithValue("@IDKho", IDKho);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable tb = new DataTable();
                        tb.Load(reader);
                        return tb;
                    }
                }
            }
            //using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            //{
            //    con.Open();
            //    string cmdText = "";
            //    cmdText = "SELECT HH.ID, HH.TenHangHoa,HH.MaHang, Dvi.TenDonViTinh, HHTK.GiaBan, HH.GiaMuaSauThue " +
            //                     "FROM GPM_HangHoa AS HH " +
            //                     "INNER JOIN GPM_HangHoaTonKho AS HHTK ON HH.ID = HHTK.IDHangHoa " +
            //                     "INNER JOIN GPM_DonViTinh as DVi ON HH.IDDonViTinh = DVi.ID " +
            //                     "LEFT OUTER JOIN GPM_HangHoa_Barcode AS BC ON HHTK.IDHangHoa = BC.IDHangHoa " +
            //                     "WHERE (BC.Barcode = @Barcode OR CONVERT(NVARCHAR(250), HHTK.IDHangHoa) = @Barcode) AND HHTK.IDKho = @IDKho AND HHTK.DaXoa = 0";
            //    using (SqlCommand command = new SqlCommand(cmdText, con))
            //    {
            //        command.Parameters.AddWithValue("@Barcode", Barcode);
            //        command.Parameters.AddWithValue("@IDKho", IDKho);
            //        using (SqlDataReader reader = command.ExecuteReader())
            //        {
            //            DataTable tb = new DataTable();
            //            tb.Load(reader);
            //            return tb;
            //        }
            //    }
            //}
        }

        public object InsertHoaDon(string IDKho, string IDNhanVien, string IDKhachHang, HoaDon hoaDon)
        {
            object IDHoaDon = null;
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                SqlTransaction trans = null;
                try
                {
                    con.Open();
                    trans = con.BeginTransaction();
                    string CompuMaHoaDon = @"SELECT 
                                          REPLICATE('0', 5 - LEN((count(ID) + 1))) + 
                                          CAST((count(ID) + 1) AS varchar) + '-' + 
                                          CAST(@IDKho AS varchar) + '-' + 
                                          FORMAT(GETDATE() , 'ddMMyy')
                                          as 'Mã Hóa Đơn'  
                                          from GPM_HoaDon 
                                          where (IDKho = @IDKho)
                                          AND DATEDIFF(dd,NgayBan, GetDate()) = 0";
                    object MaHoaDon;
                    using (SqlCommand cmd = new SqlCommand(CompuMaHoaDon, con, trans))
                    {
                        cmd.Parameters.AddWithValue("@IDKho", dtSetting.LayMaKho(IDKho));
                        MaHoaDon = cmd.ExecuteScalar();
                    }
                    if (MaHoaDon != null)
                    {
                        string InsertHoaDon = "INSERT INTO [GPM_HoaDon] ([IDKho], [IDKhachHang],[IDNhanVien],[NgayBan],[SoLuongHang],[TongTien],[GiamGia],[KhachCanTra],[KhachThanhToan], [MaHoaDon]) " +
                                              "OUTPUT INSERTED.ID " +
                                              "VALUES (@IDKho, @IDKhachHang, @IDNhanVien, getdate(), @SoLuongHang, @TongTien, @GiamGia, @KhachCanTra, @KhachThanhToan, @MaHoaDon)";

                        using (SqlCommand cmd = new SqlCommand(InsertHoaDon, con, trans))
                        {
                            cmd.Parameters.AddWithValue("@IDKho", IDKho);
                            cmd.Parameters.AddWithValue("@IDKhachHang", IDKhachHang);
                            cmd.Parameters.AddWithValue("@IDNhanVien", IDNhanVien);
                            cmd.Parameters.AddWithValue("@SoLuongHang", hoaDon.SoLuongHang);
                            cmd.Parameters.AddWithValue("@TongTien", hoaDon.TongTien);
                            cmd.Parameters.AddWithValue("@GiamGia", hoaDon.GiamGia);
                            cmd.Parameters.AddWithValue("@KhachCanTra", hoaDon.KhachCanTra);
                            cmd.Parameters.AddWithValue("@KhachThanhToan", hoaDon.KhachThanhToan);
                            cmd.Parameters.AddWithValue("@MaHoaDon", MaHoaDon);
                            IDHoaDon = cmd.ExecuteScalar();
                        }
                        if (IDHoaDon != null)
                        {
                            foreach (ChiTietHoaDon cthd in hoaDon.ListChiTietHoaDon)
                            {
                                dtHangHoa dtHH = new dtHangHoa();

                                string InsertChiTietHoaDon = "INSERT INTO [GPM_ChiTietHoaDon] ([IDHoaDon],[IDHangHoa],[GiaMua],[GiaBan] ,[SoLuong],[ChietKhau],[ThanhTien],[NgayBan],[IDKho]) " +
                                                             "VALUES (@IDHoaDon, @IDHangHoa, @GiaMua, @GiaBan, @SoLuong, @ChietKhau, @ThanhTien,getdate(),@IDKho)";
                                using (SqlCommand cmd = new SqlCommand(InsertChiTietHoaDon, con, trans))
                                {
                                    cmd.Parameters.AddWithValue("@IDHoaDon", IDHoaDon);
                                    cmd.Parameters.AddWithValue("@IDHangHoa", cthd.IDHangHoa);
                                    cmd.Parameters.AddWithValue("@GiaMua", cthd.GiaMua);
                                    cmd.Parameters.AddWithValue("@GiaBan", cthd.DonGia);
                                    cmd.Parameters.AddWithValue("@SoLuong", cthd.SoLuong);
                                    cmd.Parameters.AddWithValue("@ChietKhau", 0.0);
                                    cmd.Parameters.AddWithValue("@ThanhTien", cthd.ThanhTien);
                                    cmd.Parameters.AddWithValue("@IDKho", IDKho);
                                    cmd.ExecuteNonQuery();
                                }
                                string UpdateLichSuBanHang = "DECLARE @SoLuongCu INT = 0 " +
                                                             "SELECT @SoLuongCu = SoLuongCon FROM [GPM_HangHoaTonKho] WHERE IDHangHoa = @IDHangHoa  AND IDKho = @IDKho " +
                                                             "DECLARE @SoLuongMoi INT = @SoLuongCu - @SoLuongBan " +
                                                             "INSERT INTO [GPM_LichSuKho] ([IDHangHoa], [IDNhanVien], [SoLuong], [SoLuongMoi], [NoiDung],[NgayCapNhat]) VALUES (@IDHangHoa, @IDNhanVien, @SoLuongCu, @SoLuongMoi, @NoiDung, GetDate()) " +
                                                             "UPDATE [GPM_HangHoaTonKho] SET SoLuongCon = @SoLuongMoi, NgayCapNhat = GetDate() WHERE IDHangHoa = @IDHangHoa AND IDKho = @IDKho";
                                using (SqlCommand cmd = new SqlCommand(UpdateLichSuBanHang, con, trans))
                                {
                                    cmd.Parameters.AddWithValue("@SoLuongBan", cthd.SoLuong);
                                    cmd.Parameters.AddWithValue("@IDHangHoa", cthd.IDHangHoa);
                                    cmd.Parameters.AddWithValue("@IDKho", IDKho);
                                    cmd.Parameters.AddWithValue("@IDNhanVien", IDNhanVien);
                                    cmd.Parameters.AddWithValue("@NoiDung", "Bán hàng lẻ");
                                    cmd.ExecuteNonQuery();
                                }
                                //ghi thẻ kho
                                int TonKhoHangHoa = 0;
                                string cmdText1 = " SELECT SoLuongCon FROM [GPM_HangHoaTonKho] WHERE [IDHangHoa] = '" + cthd.IDHangHoa + "' AND IDKho =" + IDKho;
                                using (SqlCommand cmd1 = new SqlCommand(cmdText1, con, trans))
                                using (SqlDataReader reade1r = cmd1.ExecuteReader())
                                {
                                    DataTable tb1 = new DataTable();
                                    tb1.Load(reade1r);
                                    if (tb1.Rows.Count != 0)
                                    {
                                        DataRow dr1 = tb1.Rows[0];
                                        TonKhoHangHoa = Int32.Parse(dr1["SoLuongCon"].ToString());
                                    }
                                    else TonKhoHangHoa = 0;
                                }

                                string cmdText = "INSERT INTO [GPM_TheKho] ([MaDonHang], [NgayLap], [DienGiai], [NhapTrongKy],[XuatTrongKy],[TonCuoiKy], [IDNhanVien],[IDKho],[IDHangHoa],[LoaiPhieu],[XuatKhac],[XuatTra],[KiemKho]) OUTPUT INSERTED.ID  VALUES (@MaDonHang,getdate(),@DienGiai, @NhapTrongKy,@XuatTrongKy,@TonCuoiKy,@IDNhanVien,@IDKho,@IDHangHoa,@LoaiPhieu,@XuatKhac,@XuatTra,@KiemKho)";
                                using (SqlCommand cmd = new SqlCommand(cmdText, con, trans))
                                {
                                    cmd.Parameters.AddWithValue("@XuatKhac", 0);
                                    cmd.Parameters.AddWithValue("@XuatTra", 0);
                                    cmd.Parameters.AddWithValue("@KiemKho", 0);
                                    cmd.Parameters.AddWithValue("@LoaiPhieu", "Xuất");
                                    cmd.Parameters.AddWithValue("@IDHangHoa", cthd.IDHangHoa);
                                    cmd.Parameters.AddWithValue("@MaDonHang", MaHoaDon);
                                    cmd.Parameters.AddWithValue("@DienGiai", "Bán Hàng Lẻ");
                                    cmd.Parameters.AddWithValue("@NhapTrongKy", 0);
                                    cmd.Parameters.AddWithValue("@XuatTrongKy", cthd.SoLuong);
                                    cmd.Parameters.AddWithValue("@TonCuoiKy", (TonKhoHangHoa));
                                    cmd.Parameters.AddWithValue("@IDNhanVien", IDNhanVien);
                                    cmd.Parameters.AddWithValue("@IDKho", IDKho);
                                    cmd.ExecuteNonQuery();
                                }
                                if (TonKhoHangHoa < 0)
                                {
                                    string IDHangHoa = cthd.IDHangHoa.ToString();
                                    int IDHangHoaQuiDoi = 0;
                                    string IDHangHoaQD = "SELECT IDHangQuyDoi FROM GPM_HangHoa WHERE ID = '" + IDHangHoa + "'";
                                    using (SqlCommand cmd1 = new SqlCommand(IDHangHoaQD, con, trans))
                                    using (SqlDataReader reade1r = cmd1.ExecuteReader())
                                    {
                                        DataTable tb1 = new DataTable();
                                        tb1.Load(reade1r);
                                        if (tb1.Rows.Count != 0)
                                        {
                                            DataRow dr1 = tb1.Rows[0];
                                            IDHangHoaQuiDoi = Int32.Parse(dr1["IDHangQuyDoi"].ToString());
                                        }
                                        else IDHangHoaQuiDoi = 0;
                                    }
                                    if (IDHangHoaQuiDoi != 0)
                                    {
                                        int TonKhoQuiDoi = 0;
                                        string TonKhoQD = " SELECT SoLuongCon FROM [GPM_HangHoaTonKho] WHERE [IDHangHoa] = '" + IDHangHoaQuiDoi + "' AND IDKho =" + IDKho;
                                        using (SqlCommand cmd1 = new SqlCommand(TonKhoQD, con, trans))
                                        using (SqlDataReader reade1r = cmd1.ExecuteReader())
                                        {
                                            DataTable tb1 = new DataTable();
                                            tb1.Load(reade1r);
                                            if (tb1.Rows.Count != 0)
                                            {
                                                DataRow dr1 = tb1.Rows[0];
                                                TonKhoQuiDoi = Int32.Parse(dr1["SoLuongCon"].ToString());
                                            }
                                            else TonKhoQuiDoi = 0;
                                        }
                                        if (TonKhoQuiDoi > 0)
                                        {
                                            // qui đối
                                            int HeSoB = 0, HeSoA = 0;
                                            // hệ số b
                                            string HeSoHangB = "SELECT HeSo FROM GPM_HangHoa WHERE ID = '" + IDHangHoaQuiDoi + "'";
                                            using (SqlCommand cmd1 = new SqlCommand(HeSoHangB, con, trans))
                                            using (SqlDataReader reade1r = cmd1.ExecuteReader())
                                            {
                                                DataTable tb1 = new DataTable();
                                                tb1.Load(reade1r);
                                                if (tb1.Rows.Count != 0)
                                                {
                                                    DataRow dr1 = tb1.Rows[0];
                                                    HeSoB = Int32.Parse(dr1["HeSo"].ToString());
                                                }
                                                else HeSoB = 0;
                                            }
                                            // hệ số a
                                            string HeSoHangA = "SELECT HeSo FROM GPM_HangHoa WHERE ID = '" + IDHangHoa + "'";
                                            using (SqlCommand cmd1 = new SqlCommand(HeSoHangA, con, trans))
                                            using (SqlDataReader reade1r = cmd1.ExecuteReader())
                                            {
                                                DataTable tb1 = new DataTable();
                                                tb1.Load(reade1r);
                                                if (tb1.Rows.Count != 0)
                                                {
                                                    DataRow dr1 = tb1.Rows[0];
                                                    HeSoA = Int32.Parse(dr1["HeSo"].ToString());
                                                }
                                                else HeSoA = 0;
                                            }
                                            int SoLanDoi = 1;
                                            HangQuyDoi h1 = new HangQuyDoi(), h2 = new HangQuyDoi();
                                            h1.Heso = HeSoA;
                                            h1.Slcon = TonKhoHangHoa;
                                            h2.Heso = HeSoB;
                                            h2.Slcon = TonKhoQuiDoi;

                                            HQuyDoi(h1, h2);
                                            string cmbXuLyKho = "UPDATE [GPM_HangHoaTonKho] SET [SoLuongCon] = @SoLuongConA, [NgayCapNhat] = getdate() WHERE [IDHangHoa] = @IDHangHoaA AND [IDKho] = @IDKho " +
                                                "UPDATE [GPM_HangHoaTonKho] SET [SoLuongCon] = @SoLuongConB, [NgayCapNhat] = getdate() WHERE [IDHangHoa] = @IDHangHoaB AND [IDKho] = @IDKho";
                                            using (SqlCommand cmd = new SqlCommand(cmbXuLyKho, con, trans))
                                            {
                                                cmd.Parameters.AddWithValue("@IDHangHoaA", IDHangHoa);
                                                cmd.Parameters.AddWithValue("@SoLuongConA", h1.Slcon);
                                                cmd.Parameters.AddWithValue("@IDHangHoaB", IDHangHoaQuiDoi);
                                                cmd.Parameters.AddWithValue("@SoLuongConB", h2.Slcon);
                                                cmd.Parameters.AddWithValue("@IDKho", IDKho);
                                                cmd.ExecuteNonQuery();
                                            }

                                            string cmdTextThemTheKho = "INSERT INTO [GPM_TheKho] ([MaDonHang], [NgayLap], [DienGiai], [NhapTrongKy],[XuatTrongKy],[TonCuoiKy], [IDNhanVien],[IDKho],[IDHangHoa],[LoaiPhieu],[XuatKhac],[XuatTra],[KiemKho]) OUTPUT INSERTED.ID  VALUES (@MaDonHang,getdate(),@DienGiai, @NhapTrongKy,@XuatTrongKy,@TonCuoiKy,@IDNhanVien,@IDKho,@IDHangHoa,@LoaiPhieu,@XuatKhac,@XuatTra,@KiemKho)";
                                            using (SqlCommand cmd = new SqlCommand(cmdTextThemTheKho, con, trans))
                                            {
                                                cmd.Parameters.AddWithValue("@XuatKhac", 0);
                                                cmd.Parameters.AddWithValue("@XuatTra", 0);
                                                cmd.Parameters.AddWithValue("@KiemKho", 0);
                                                cmd.Parameters.AddWithValue("@LoaiPhieu", "Xuất");
                                                cmd.Parameters.AddWithValue("@IDHangHoa", IDHangHoaQuiDoi);
                                                cmd.Parameters.AddWithValue("@MaDonHang", MaHoaDon);
                                                cmd.Parameters.AddWithValue("@DienGiai", "Qui Đổi Bán Hàng Lẻ");
                                                cmd.Parameters.AddWithValue("@NhapTrongKy", 0);
                                                cmd.Parameters.AddWithValue("@XuatTrongKy", TonKhoQuiDoi - h2.Slcon);//số lượng xuất
                                                cmd.Parameters.AddWithValue("@TonCuoiKy", h2.Slcon);// tồn kho còn lại
                                                cmd.Parameters.AddWithValue("@IDNhanVien", IDNhanVien);
                                                cmd.Parameters.AddWithValue("@IDKho", IDKho);
                                                cmd.ExecuteNonQuery();
                                            }

                                            cmdTextThemTheKho = "INSERT INTO [GPM_TheKho] ([MaDonHang], [NgayLap], [DienGiai], [NhapTrongKy],[XuatTrongKy],[TonCuoiKy], [IDNhanVien],[IDKho],[IDHangHoa],[LoaiPhieu],[XuatKhac],[XuatTra],[KiemKho]) OUTPUT INSERTED.ID  VALUES (@MaDonHang,getdate(),@DienGiai, @NhapTrongKy,@XuatTrongKy,@TonCuoiKy,@IDNhanVien,@IDKho,@IDHangHoa,@LoaiPhieu,@XuatKhac,@XuatTra,@KiemKho)";
                                            using (SqlCommand cmd = new SqlCommand(cmdTextThemTheKho, con, trans))
                                            {
                                                cmd.Parameters.AddWithValue("@XuatKhac", 0);
                                                cmd.Parameters.AddWithValue("@XuatTra", 0);
                                                cmd.Parameters.AddWithValue("@KiemKho", 0);
                                                cmd.Parameters.AddWithValue("@LoaiPhieu", "Nhập");
                                                cmd.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                                                cmd.Parameters.AddWithValue("@MaDonHang", MaHoaDon);
                                                cmd.Parameters.AddWithValue("@DienGiai", "Qui Đổi Bán Hàng Lẻ");
                                                cmd.Parameters.AddWithValue("@NhapTrongKy", h1.Slcon - TonKhoHangHoa);
                                                cmd.Parameters.AddWithValue("@XuatTrongKy", 0);//số lượng xuất
                                                cmd.Parameters.AddWithValue("@TonCuoiKy", h1.Slcon);// tồn kho còn lại
                                                cmd.Parameters.AddWithValue("@IDNhanVien", IDNhanVien);
                                                cmd.Parameters.AddWithValue("@IDKho", IDKho);
                                                cmd.ExecuteNonQuery();
                                            }

                                            //if ((TonKhoQuiDoi * HeSoB) > TonKhoHangHoa)
                                            //{
                                            //    string cmdTextThemTheKho = "INSERT INTO [GPM_TheKho] ([MaDonHang], [NgayLap], [DienGiai], [NhapTrongKy],[XuatTrongKy],[TonCuoiKy], [IDNhanVien],[IDKho],[IDHangHoa],[LoaiPhieu],[XuatKhac],[XuatTra],[KiemKho]) OUTPUT INSERTED.ID  VALUES (@MaDonHang,getdate(),@DienGiai, @NhapTrongKy,@XuatTrongKy,@TonCuoiKy,@IDNhanVien,@IDKho,@IDHangHoa,@LoaiPhieu,@XuatKhac,@XuatTra,@KiemKho)";
                                            //    using (SqlCommand cmd = new SqlCommand(cmdTextThemTheKho, con, trans))
                                            //    {
                                            //        cmd.Parameters.AddWithValue("@XuatKhac", 0);
                                            //        cmd.Parameters.AddWithValue("@XuatTra", 0);
                                            //        cmd.Parameters.AddWithValue("@KiemKho", 0);
                                            //        cmd.Parameters.AddWithValue("@LoaiPhieu", "Xuất");
                                            //        cmd.Parameters.AddWithValue("@IDHangHoa", IDHangHoaQuiDoi);
                                            //        cmd.Parameters.AddWithValue("@MaDonHang", MaHoaDon);
                                            //        cmd.Parameters.AddWithValue("@DienGiai", "Qui Đổi Bán Hàng Lẻ");
                                            //        cmd.Parameters.AddWithValue("@NhapTrongKy", 0);
                                            //        cmd.Parameters.AddWithValue("@XuatTrongKy", HeSoA * SoLanDoi);//số lượng xuất
                                            //        cmd.Parameters.AddWithValue("@TonCuoiKy", TonKhoQuiDoi - (HeSoA * SoLanDoi));// tồn kho còn lại
                                            //        cmd.Parameters.AddWithValue("@IDNhanVien", IDNhanVien);
                                            //        cmd.Parameters.AddWithValue("@IDKho", IDKho);
                                            //        cmd.ExecuteNonQuery();
                                            //    }
                                            //    string cmbXuLyKho = "UPDATE [GPM_HangHoaTonKho] SET [SoLuongCon] = [SoLuongCon] - @SoLuongCon, [NgayCapNhat] = getdate() WHERE [IDHangHoa] = @IDHangHoa AND [IDKho] = @IDKho";
                                            //    using (SqlCommand cmd = new SqlCommand(cmbXuLyKho, con, trans))
                                            //    {
                                            //        cmd.Parameters.AddWithValue("@IDHangHoa", IDHangHoaQuiDoi);
                                            //        cmd.Parameters.AddWithValue("@SoLuongCon", HeSoA * SoLanDoi);
                                            //        cmd.Parameters.AddWithValue("@IDKho", IDKho);
                                            //        cmd.ExecuteNonQuery();
                                            //    }
                                            //    cmdTextThemTheKho = "INSERT INTO [GPM_TheKho] ([MaDonHang], [NgayLap], [DienGiai], [NhapTrongKy],[XuatTrongKy],[TonCuoiKy], [IDNhanVien],[IDKho],[IDHangHoa],[LoaiPhieu],[XuatKhac],[XuatTra],[KiemKho]) OUTPUT INSERTED.ID  VALUES (@MaDonHang,getdate(),@DienGiai, @NhapTrongKy,@XuatTrongKy,@TonCuoiKy,@IDNhanVien,@IDKho,@IDHangHoa,@LoaiPhieu,@XuatKhac,@XuatTra,@KiemKho)";
                                            //    using (SqlCommand cmd = new SqlCommand(cmdTextThemTheKho, con, trans))
                                            //    {
                                            //        cmd.Parameters.AddWithValue("@XuatKhac", 0);
                                            //        cmd.Parameters.AddWithValue("@XuatTra", 0);
                                            //        cmd.Parameters.AddWithValue("@KiemKho", 0);
                                            //        cmd.Parameters.AddWithValue("@LoaiPhieu", "Nhập");
                                            //        cmd.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                                            //        cmd.Parameters.AddWithValue("@MaDonHang", MaHoaDon);
                                            //        cmd.Parameters.AddWithValue("@DienGiai", "Qui Đổi Bán Hàng Lẻ");
                                            //        cmd.Parameters.AddWithValue("@NhapTrongKy", HeSoB * SoLanDoi);
                                            //        cmd.Parameters.AddWithValue("@XuatTrongKy", (-1) * TonKhoHangHoa);//
                                            //        cmd.Parameters.AddWithValue("@TonCuoiKy", TonKhoHangHoa + ( HeSoB * SoLanDoi));//
                                            //        cmd.Parameters.AddWithValue("@IDNhanVien", IDNhanVien);
                                            //        cmd.Parameters.AddWithValue("@IDKho", IDKho);
                                            //        cmd.ExecuteNonQuery();
                                            //    }
                                            //    cmbXuLyKho = "UPDATE [GPM_HangHoaTonKho] SET [SoLuongCon] = [SoLuongCon] + @SoLuongCon, [NgayCapNhat] = getdate() WHERE [IDHangHoa] = @IDHangHoa AND [IDKho] = @IDKho";
                                            //    using (SqlCommand cmd = new SqlCommand(cmbXuLyKho, con, trans))
                                            //    {
                                            //        cmd.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                                            //        cmd.Parameters.AddWithValue("@SoLuongCon", HeSoB * SoLanDoi);
                                            //        cmd.Parameters.AddWithValue("@IDKho", IDKho);
                                            //        cmd.ExecuteNonQuery();
                                            //    }
                                            //}
                                        }
                                    }
                                }
                            }
                        }
                    }
                    trans.Commit();
                    con.Close();
                }
                catch (Exception ex)
                {
                    if (trans != null) trans.Rollback();
                    throw new Exception("Quá trình lưu dữ liệu có lỗi xảy ra, vui lòng tải lại trang và thanh toán lại !!");
                }
            }
            return IDHoaDon;
        }
        public void HQuyDoi(HangQuyDoi b1, HangQuyDoi b2)
        {
            HangQuyDoi h1 = b1, h2 = b2;
            for (int i = 1; i < h2.Slcon; i++)
            {
                if (((h2.Heso * i) + (h1.Heso * h1.Slcon)) >= 0)
                {
                    h1.Slcon = ((h2.Heso * i) + (h1.Heso * h1.Slcon)) / h1.Heso;
                    h2.Slcon = h2.Slcon - i;
                    break;
                }
            }
            if (h2.Slcon >= 0)
            {
                b1 = h1; b2 = h2;
            }
        }
    }
}