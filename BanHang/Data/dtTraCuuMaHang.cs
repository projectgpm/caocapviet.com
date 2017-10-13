using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtTraCuuMaHang
    {
        public static string TenNganhHang(string IDNhomHang)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "  SELECT [GPM_NganhHang].TenNganhHang FROM [GPM_NhomHang],[GPM_NganhHang] WHERE [GPM_NganhHang].ID = [GPM_NhomHang].[IDNganhHang] AND  [GPM_NhomHang].ID = '" + IDNhomHang + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return dr["TenNganhHang"].ToString();
                    }
                    else return "";
                }
            }
        }
        public static string LayIDHangQuiDoi(string IDHangHoa)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "  SELECT IDHangQuyDoi  FROM GPM_HangHoa WHERE ID = '" + IDHangHoa + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return dr["IDHangQuyDoi"].ToString();
                    }
                    else return "0";
                }
            }
        }
        public DataTable DanhSachHangComBo(string IDHangHoa)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "select * from [GPM_HangHoa_Combo] where [IDHangHoaCombo] =  '" + IDHangHoa + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public DataTable DanhSachBarcode(string IDHangHoa)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_HangHoa_Barcode] WHERE [IDHangHoa] = N'" + IDHangHoa + "' AND DaXoa = 0";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public DataTable DanhSachGiaTheoSoLuong(string IDHangHoa)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_HangHoa_GiaTheoSL] WHERE [IDHangHoa] = N'" + IDHangHoa + "' AND DaXoa = 0";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public DataTable DanhSachHangHoaQuiDoi(string IDHangHoa)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT [GPM_HangHoa].MaHang as MaHangHoa,[GPM_HangHoa].IDDonViTinh,[GPM_HangHoa_QuyDoi].* FROM [GPM_HangHoa_QuyDoi],[GPM_HangHoa] WHERE [GPM_HangHoa_QuyDoi].IDHangHoa =[GPM_HangHoa].ID  AND [GPM_HangHoa_QuyDoi].[IDHangHoa] = N'" + IDHangHoa + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public DataTable TraCuuMaHang(string MaHang)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_HangHoa] WHERE [MaHang] = N'" + MaHang + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public DataTable DanhSachHangQuiDoi(string IDHangQuyDoi)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_HangHoa] WHERE [ID] = N'" + IDHangQuyDoi + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
    }
}