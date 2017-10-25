using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtDuyetDonHangHanMuc
    {
        dtCauHinhHanMuc dt = new dtCauHinhHanMuc();
        public DataTable LayDanhSachDonHang(string IDNhom, string HienThi, string NgayBD, string NgayKT, string IDNhanVien)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT TOP " + HienThi + " * FROM [GPM_DonHangChiNhanh] WHERE [NgayDat] < '" + NgayKT + "' AND NgayDat >= '" + NgayBD + "' AND [TrangThai] = 0 AND [TongTien] <= '" + dtCauHinhHanMuc.DonGiaTheoIDNhom(IDNhom) + "'AND (" + dtSetting.LayQuyenTruyCapKho(IDNhanVien) + ") AND GiamSatDuyet is null AND KhoDuyet is null AND GiamDocDuyet is null ORDER BY TongTien DESC ";
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