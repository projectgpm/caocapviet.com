using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtQLKho
    {
        public static float LayTongHoaDon(string IDKho, string ngayBD, string ngayKT)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT SUM(KhachCanTra) AS Tong FROM GPM_HoaDon WHERE IDKho = '" + IDKho + "' AND NgayBan >= '" + ngayBD + "' AND NgayBan <= '" + ngayKT + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    try
                    {
                        if (tb.Rows.Count != 0)
                            return float.Parse(tb.Rows[0]["Tong"].ToString());
                    }
                    catch (Exception) { }
                    return 0;
                }
            }
        }

        public static int LayTongPhieuChuyenKho(string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT COUNT(ID) AS Tong FROM GPM_PhieuChuyenKho WHERE IDTrangThai = 1 AND DaXoa = 0 AND (IDKhoNhap = '" + IDKho + "' OR IDKhoXuat = '" + IDKho + "')";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    try
                    {
                        if (tb.Rows.Count != 0)
                            return int.Parse(tb.Rows[0]["Tong"].ToString());
                    }
                    catch (Exception) { }
                    return 0;
                }
            }
        }


        public static int LayTongSoLuongHangHoaDon(string IDKho, string ngayBD, string ngayKT)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT SUM(GPM_ChiTietHoaDon.SoLuong) AS Tong FROM GPM_HoaDon,GPM_ChiTietHoaDon WHERE GPM_HoaDon.ID = GPM_ChiTietHoaDon.IDHoaDon AND GPM_HoaDon.IDKho = '" + IDKho + "' AND GPM_HoaDon.NgayBan >= '" + ngayBD + "' AND GPM_HoaDon.NgayBan <= '" + ngayKT + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    try
                    {
                        if (tb.Rows.Count != 0)
                            return int.Parse(tb.Rows[0]["Tong"].ToString());
                    }
                    catch (Exception) { }
                    return 0;
                }
            }
        }

        public static int LayTongSoLuongHangDoiTra(string IDKho, string ngayBD, string ngayKT)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT SUM(GPM_PhieuKhachHangTraHang_ChiTiet.SoLuong) AS Tong FROM GPM_PhieuKhachHangTraHang,GPM_PhieuKhachHangTraHang_ChiTiet WHERE GPM_PhieuKhachHangTraHang.ID = GPM_PhieuKhachHangTraHang_ChiTiet.IDPhieuKhachHangTraHang AND GPM_PhieuKhachHangTraHang.IDKho = '" + IDKho + "' AND GPM_PhieuKhachHangTraHang.NgayDoi >= '" + ngayBD + "' AND GPM_PhieuKhachHangTraHang.NgayDoi <= '" + ngayKT + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    try
                    {
                        if (tb.Rows.Count != 0)
                            return int.Parse(tb.Rows[0]["Tong"].ToString());
                    }
                    catch (Exception) { }
                    return 0;
                }
            }
        }
    }
}