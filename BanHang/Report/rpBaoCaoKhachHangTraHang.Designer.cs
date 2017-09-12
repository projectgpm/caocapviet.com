namespace BanHang.Report
{
    partial class rpBaoCaoKhachHangTraHang
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.strNgay = new DevExpress.XtraReports.Parameters.Parameter();
            this.strKhachHang = new DevExpress.XtraReports.Parameters.Parameter();
            this.strKhoNhap = new DevExpress.XtraReports.Parameters.Parameter();
            this.NgayBD = new DevExpress.XtraReports.Parameters.Parameter();
            this.NgayKT = new DevExpress.XtraReports.Parameters.Parameter();
            this.IDKhachHang = new DevExpress.XtraReports.Parameters.Parameter();
            this.IDKhoNhap = new DevExpress.XtraReports.Parameters.Parameter();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Dpi = 100F;
            this.Detail.HeightF = 100F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 100F;
            this.TopMargin.HeightF = 47.91666F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 100F;
            this.BottomMargin.HeightF = 100F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // PageHeader
            // 
            this.PageHeader.Dpi = 100F;
            this.PageHeader.HeightF = 354.6297F;
            this.PageHeader.Name = "PageHeader";
            // 
            // strNgay
            // 
            this.strNgay.Name = "strNgay";
            // 
            // strKhachHang
            // 
            this.strKhachHang.Name = "strKhachHang";
            // 
            // strKhoNhap
            // 
            this.strKhoNhap.Name = "strKhoNhap";
            // 
            // NgayBD
            // 
            this.NgayBD.Name = "NgayBD";
            // 
            // NgayKT
            // 
            this.NgayKT.Name = "NgayKT";
            // 
            // IDKhachHang
            // 
            this.IDKhachHang.Name = "IDKhachHang";
            // 
            // IDKhoNhap
            // 
            this.IDKhoNhap.Name = "IDKhoNhap";
            // 
            // rpBaoCaoKhachHangTraHang
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader});
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(0, 0, 48, 100);
            this.PageHeight = 850;
            this.PageWidth = 1100;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.strNgay,
            this.strKhachHang,
            this.strKhoNhap,
            this.NgayBD,
            this.NgayKT,
            this.IDKhachHang,
            this.IDKhoNhap});
            this.Version = "16.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.Parameters.Parameter strNgay;
        private DevExpress.XtraReports.Parameters.Parameter strKhachHang;
        private DevExpress.XtraReports.Parameters.Parameter strKhoNhap;
        private DevExpress.XtraReports.Parameters.Parameter NgayBD;
        private DevExpress.XtraReports.Parameters.Parameter NgayKT;
        private DevExpress.XtraReports.Parameters.Parameter IDKhachHang;
        private DevExpress.XtraReports.Parameters.Parameter IDKhoNhap;
    }
}
