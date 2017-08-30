using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtGiaTheoVung
    {
        public DataTable NhomHang()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "  SELECT * FROM [GPM_NhomHang]";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public DataTable LayDanhSachHangHoa_IDNhomHang(int ID, int IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "  SELECT [GPM_HANGHOA].*, [GPM_HangHoaTonKho].GiaBan FROM [GPM_HANGHOA],[GPM_NhomHang],[GPM_HangHoaTonKho] WHERE [GPM_HANGHOA].[DaXoa] = 0 AND [GPM_HANGHOA].IDNhomHang = [GPM_NhomHang].ID AND [GPM_HangHoaTonKho].IDHangHoa = [GPM_HANGHOA].ID AND [GPM_HangHoaTonKho].IDKho= '" + IDKho + "' AND ('" + ID + "' = -1 OR [GPM_NhomHang].ID = '" + ID + "')";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public DataTable DanhSachHangHoa()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT [GPM_HangHoa].MaHang,[GPM_HangHoa].IDDonViTinh,[GPM_HangHoa].ID,[GPM_HangHoa].TenHangHoa,[GPM_HangHoa].GiaBan1,[GPM_HangHoaTonKho].GiaBan FROM [GPM_HangHoa],[GPM_HangHoaTonKho]  WHERE [GPM_HangHoa].ID = [GPM_HangHoaTonKho].IDHangHoa AND [GPM_HangHoa].DaXoa = 0 AND IDKho = 1";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public static void Update_GiaTheoVung(int IDKho, int IDHangHoa, float GiaBan)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [GPM_HangHoaTonKho] SET [GiaBan] = @GiaBan WHERE [IDHangHoa] = @IDHangHoa AND [IDKho]=@IDKho";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDKho", IDKho);
                        myCommand.Parameters.AddWithValue("@GiaBan", GiaBan);
                        myCommand.Parameters.AddWithValue("@IDHangHoa", IDHangHoa);
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }
        public static void CapNhat_GiaTheoVung(string ID, string IDKho, float GiaBan)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string strSQL = "UPDATE [GPM_HangHoaTonKho] SET [GiaBan] = @GiaBan WHERE [IDKho] = @IDKho AND [ID]= @ID";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@ID", ID);
                        myCommand.Parameters.AddWithValue("@GiaBan", GiaBan);
                        myCommand.Parameters.AddWithValue("@IDKho", IDKho);
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }
        public DataTable DanhSachHangHoa_ALL()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT [GPM_HangHoa].IDDonViTinh,[GPM_HangHoa].ID as IDHangHoa,[GPM_HangHoa].MaHang,[GPM_HangHoaTonKho].ID,[GPM_HangHoa].TenHangHoa,[GPM_HangHoa].GiaBanSauThue,[GPM_HangHoaTonKho].GiaBan,[GPM_HangHoaTonKho].IDKho FROM [GPM_HangHoa],[GPM_HangHoaTonKho]  WHERE [GPM_HangHoa].ID = [GPM_HangHoaTonKho].IDHangHoa AND [GPM_HangHoa].DaXoa = 0 AND IDKho > 1";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public DataTable LayDanhSachKho()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_Kho] WHERE [DAXOA] = 0 AND [ID] > 1";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public DataTable LayDanhSachKho_IDVung(int IDVung)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = " SELECT * FROM [GPM_Kho] WHERE [DAXOA] = 0 AND [ID] > 1 AND ('" + IDVung + "' = -1 OR [IDVung] = '" + IDVung + "')";
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