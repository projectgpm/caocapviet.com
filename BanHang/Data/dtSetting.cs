using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Web;

namespace BanHang.Data
{
    public class dtSetting
    {
        public static int LaySoNgayDuocSuaDonHangDaXuLy()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT CapNhatDonHangXuLy FROM [Setting]";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return Int32.Parse(dr["CapNhatDonHangXuLy"].ToString());
                    }
                    else return 0;
                }
            }
        }
        public static int KT_ChuyenAm()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT ChuyenAm FROM [Setting]";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return Int32.Parse(dr["ChuyenAm"].ToString());
                    }
                    else return -1;
                }
            }
        }
        public static int LaySoNgayBanHang()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT TuanSuatBanHang FROM [Setting]";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return Int32.Parse(dr["TuanSuatBanHang"].ToString());
                    }
                    else return 0;
                }
            }
        }
        public static int KT_BanHang(string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT TrangThaiBanHang FROM [GPM_Kho] WHERE [ID] = '" + IDKho + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return Int32.Parse(dr["TrangThaiBanHang"].ToString());
                    }
                    else return -1;
                }
            }
        }


        public static int LaySoNgayTraHang()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT SoNgayTraHang FROM [Setting]";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return Int32.Parse(dr["SoNgayTraHang"].ToString());
                    }
                    else return 0;
                }
            }
        }

        public static bool IsNumber(string pValue)
        {
            foreach (Char c in pValue)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }
        public DataTable LayTenDatabase()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string Database;
                string cmdText = "SELECT DatabaseName FROM [Setting]";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public static int LayTrangThaiMenu(string IDNhomNguoiDung, int IDMenu)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT TrangThai FROM [GPM_PhanQuyen] WHERE [IDNhomNguoiDung] = '" + IDNhomNguoiDung + "' AND [IDMenu] = '" + IDMenu + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        return Int32.Parse(dr["TrangThai"].ToString());
                    }
                    else return -1;
                }
            }
        }
        public static int LayTrangThaiMenu_ChucNang(string IDNhomNguoiDung, int IDMenu)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT ChucNang FROM [GPM_PhanQuyen] WHERE [IDNhomNguoiDung] = '" + IDNhomNguoiDung + "' AND [IDMenu] = '" + IDMenu + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        DataRow dr = tb.Rows[0];
                        int KT = 0;
                        KT = Int32.Parse(dr["ChucNang"].ToString());
                        if (KT == 0)
                            return 1;
                        return 0;

                    }
                    else return -1;
                }
            }
        }
        public static string convertDauSangKhongDau(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D').ToUpper();
        }
        public static int LayIDKho(int IDKho)
        {
            return IDKho;
        }
        public static int kiemTraChuyenDoiDau()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT ChuyenDoiDau FROM [Setting]";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    if (tb.Rows.Count != 0)
                    {
                        return Int32.Parse(tb.Rows[0]["ChuyenDoiDau"].ToString());
                    }
                    else return -1;
                }
            }
        }

        public static PhysicalAddress GetMacAddress()
        {
            NetworkInterface[] nic1 = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.Name.ToString().CompareTo("Wi-Fi") == 0 || nic.Name.ToString().CompareTo("Ethernet") == 0)
                {
                    return nic.GetPhysicalAddress();
                }
            }
            return null;
        }

        // -----------
        public static string GetSHA1HashData(string data)
        {
            //create new instance of md5
            SHA1 sha1 = SHA1.Create();

            byte[] hashData = sha1.ComputeHash(System.Text.Encoding.UTF8.GetBytes(data + 123));

            System.Text.StringBuilder returnValue = new System.Text.StringBuilder();

            for (int i = 0; i < hashData.Length; i++)
            {
                returnValue.Append(hashData[i].ToString("x"));
            }

            return returnValue.ToString();
        }
        public static void setData_Setting(string KeyKichHoat, string user)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [Setting] SET [KeyKichHoat] = @KeyKichHoat, [NguoiKichHoat] = @NguoiKichHoat";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@KeyKichHoat", KeyKichHoat);
                        myCommand.Parameters.AddWithValue("@NguoiKichHoat", user);
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }
        public static DataTable getData_Setting()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    con.Open();
                    string cmdText = "SELECT KeyKichHoat FROM [Setting]";
                    using (SqlCommand command = new SqlCommand(cmdText, con))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable tb = new DataTable();
                        tb.Load(reader);
                        return tb;
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        // Key code..
        public static int setKeyCode(string Key, string user)
        {
            PhysicalAddress address = GetMacAddress();
            string strAddress = address.ToString();

            if (Key.CompareTo("1231123") == 0)
            {
                string sha1Address = GetSHA1HashData(strAddress);
                setData_Setting(sha1Address, user);
                return 1;
            }
            return -1;

        }
        public static int getKeyCode()
        {
            PhysicalAddress address = GetMacAddress();
            string strAddress = address.ToString();
            string sha1Address = GetSHA1HashData(strAddress);

            DataTable da = getData_Setting();
            if (da.Rows.Count != 0)
            {
                DataRow dr = da.Rows[0];
                string macAddress = dr["KeyKichHoat"].ToString();
                if (macAddress.CompareTo(sha1Address) == 0)
                    return 1;
            }
            return -1;
        }
    }
}