﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtMasterPage
    {
        public DataTable DanhSachDonHangChiNhanhChuaXuLy(DateTime NgayHomNay)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT ID,SoDonHang,IDKhoLap FROM [GPM_DuyetHangChiNhanh] WHERE [GPM_DuyetHangChiNhanh].IDTrangThaiXuLy != 2 AND [GPM_DuyetHangChiNhanh].TrangThaiDuyet = 0 AND [NgayDuyet] < '" + NgayHomNay.ToString("yyyy-MM-dd") + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public DataTable DanhSachChiTietDuyet(string ID)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM [GPM_DuyetHangChiNhanh_ChiTiet] WHERE IDDuyetHangChiNhanh = '" + ID + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public void CapNhatDonHangHoanTat(string IDDonHang)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_DuyetHangChiNhanh] SET [TrangThaiDuyet] = 1 WHERE [ID] = '" + IDDonHang + "'";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
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

        public DataTable DanhSachDonHangThuMua(DateTime NgayHomNay, int SoNgayHuy)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT ID FROM [GPM_ThuMua_DonHang] WHERE TrangThai = 0 AND TrangThaiDonHang = 0 AND NgayLap < '" + NgayHomNay.AddDays(-SoNgayHuy).ToString("yyyy-MM-dd") + "'  AND [SoDonHang] is not null";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public void CapNhatTrangThaiHuyDonHangThuMua(string ID)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    myConnection.Open();
                    string cmdText = "UPDATE [GPM_ThuMua_DonHang] SET [TrangThaiDonHang] = 1, [NgayCapNhat] = getdate() WHERE [ID] = '" + ID + "'";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.ExecuteNonQuery();
                    }
                    myConnection.Close();
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình cập nhật dữ liệu gặp lỗi");
                }
            }
        }
    }
}