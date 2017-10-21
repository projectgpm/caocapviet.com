using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtTraCuuDonHang
    {
        public DataTable LayDanhSachDonHangThuMua(string SoDonHang)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_ThuMua_DonHang] WHERE SoDonHang = N'" + SoDonHang + "' ";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public static int LayIDSoDonHang(string SoDonHang)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT ID FROM [GPM_ThuMua_DonHang] WHERE SoDonHang = N'" + SoDonHang + "' ";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return Int32.Parse(dr["ID"].ToString());
                    }
                    else return 0;
                }
            }
        }
        public DataTable DanhSachChiTiet(string SoDonHang)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_ThuMua_DonHang_ChiTiet] WHERE IDDonHangThuMua ='" + LayIDSoDonHang (SoDonHang) + "'";
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