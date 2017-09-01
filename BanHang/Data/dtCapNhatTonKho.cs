using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtCapNhatTonKho
    {
        public static void CapNhatKho(string IDHangHoa, string SoLuongMoi, string IDKho)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_HangHoaTonKho] SET [SoLuongCon] = @SoLuongMoi, [NgayCapNhat] = getdate() WHERE [IDHangHoa] = @IDHangHoa AND [IDKho] = @IDKho";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@SoLuongMoi", SoLuongMoi);
                        myCommand.Parameters.AddWithValue("@IDKho", IDKho);
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
        public static void CongTonKho(string IDHangHoa, string SoLuongCon, string IDKho)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_HangHoaTonKho] SET [SoLuongCon] = [SoLuongCon] + @SoLuongCon, [NgayCapNhat] = getdate() WHERE [IDHangHoa] = @IDHangHoa AND [IDKho] = @IDKho";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@SoLuongCon", SoLuongCon);
                        myCommand.Parameters.AddWithValue("@IDKho", IDKho);
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
        public static void TruTonKho(string IDHangHoa, string SoLuongCon, string IDKho)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_HangHoaTonKho] SET [SoLuongCon] = [SoLuongCon] - @SoLuongCon, [NgayCapNhat] = getdate() WHERE [IDHangHoa] = @IDHangHoa AND [IDKho] = @IDKho";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.Parameters.AddWithValue("@SoLuongCon", SoLuongCon);
                        myCommand.Parameters.AddWithValue("@IDKho", IDKho);
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

        public static int SoLuong_TonKho(string IDHangHoa, string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT SoLuongCon FROM [GPM_HangHoaTonKho] WHERE [IDHangHoa] = '" + IDHangHoa + "' AND IDKho =" + IDKho;
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return Int32.Parse(dr["SoLuongCon"].ToString());
                    }
                    else return -1;
                }
            }
        }


        public static float GiaBan_Kho1(string IDHangHoa)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT GiaBanSauThue FROM [GPM_HangHoa] WHERE [ID] = '" + IDHangHoa + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return float.Parse(dr["GiaBanSauThue"].ToString());
                    }
                    else return -1;
                }
            }
        }
        public static float GiaBan_KhoChiNhanh(string IDHangHoa, string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT GiaBan FROM [GPM_HangHoaTonKho] WHERE [IDHangHoa] = '" + IDHangHoa + "' AND [IDKho] = '" + IDKho + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return float.Parse(dr["GiaBan"].ToString());
                    }
                    else return -1;
                }
            }
        }

        public static float GiaMua_Kho1(string IDHangHoa)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT GiaMuaSauThue FROM [GPM_HangHoa] WHERE [ID] = '" + IDHangHoa + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return float.Parse(dr["GiaMuaSauThue"].ToString());
                    }
                    else return -1;
                }
            }
        }
        
    }
}