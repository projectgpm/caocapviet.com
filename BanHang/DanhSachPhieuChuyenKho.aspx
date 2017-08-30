<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="DanhSachPhieuChuyenKho.aspx.cs" Inherits="BanHang.DanhSachPhieuChuyenKho" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
       <%--popup chi tiet don hang--%>
     <script type="text/javascript">
         function OnMoreInfoClick(element, key) {
             popup.SetContentUrl("ChiTietPhieuChuyenKho.aspx?IDPhieuChuyenKho=" + key);
             popup.ShowAtElement();
             // alert(key);
         }

    </script>
    <br />
    <dx:ASPxButton ID="btnThemPhieuChuyenKho" runat="server" Text="Tạo phiếu chuyển kho" HorizontalAlign="Right" VerticalAlign="Middle" PostBackUrl="PhieuChuyenKho.aspx">
        <Image IconID="actions_add_32x32">
        </Image>
        <Paddings Padding="4px" />
    </dx:ASPxButton>
    <dx:ASPxGridView ID="gridPhieuChuyenKho" runat="server" AutoGenerateColumns="False" Width="100%" KeyFieldName="ID">
        <SettingsPager Mode="ShowAllRecords">
        </SettingsPager>
        <Settings ShowFilterRow="True" ShowTitlePanel="True" />


        <SettingsBehavior ConfirmDelete="True" />
        <SettingsCommandButton>
            <ShowAdaptiveDetailButton ButtonType="Image">
            </ShowAdaptiveDetailButton>
            <HideAdaptiveDetailButton ButtonType="Image">
            </HideAdaptiveDetailButton>
            <DeleteButton ButtonType="Image" RenderMode="Image">
                <Image IconID="actions_cancel_16x16" ToolTip="Xóa đơn hàng">
                </Image>
            </DeleteButton>
        </SettingsCommandButton>
        <SettingsSearchPanel Visible="True" />
        <SettingsText CommandDelete="Xóa" CommandEdit="Sửa" CommandNew="Thêm" Title="DANH SÁCH PHIẾU CHUYỂN KHO" ConfirmDelete="Bạn chắc chắn muốn xóa?"/>
        <Columns>
            <dx:GridViewDataComboBoxColumn Caption="Kho nhập" VisibleIndex="3" FieldName="IDKhoNhap">
                <PropertiesComboBox DataSourceID="sqlKho" TextField="TenCuaHang" ValueField="ID">
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataDateColumn Caption="Ngày xuất" VisibleIndex="5" FieldName="NgayXuat">
                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataButtonEditColumn Caption="Xem Chi Tiết" VisibleIndex="12">
                
                <DataItemTemplate>
                    <a href="javascript:void(0);" onclick="OnMoreInfoClick(this, '<%# Container.KeyValue %>')">Xem </a>
                </DataItemTemplate>
            </dx:GridViewDataButtonEditColumn>
            <dx:GridViewDataComboBoxColumn Caption="Kho xuất" FieldName="IDKhoXuat" VisibleIndex="2">
                <PropertiesComboBox DataSourceID="sqlKho" TextField="TenCuaHang" ValueField="ID">
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataComboBoxColumn Caption="Nhân viên xuất" FieldName="IDNhanVienXuat" VisibleIndex="4">
                <PropertiesComboBox DataSourceID="sqlDNhanVien" TextField="TenNguoiDung" ValueField="ID">
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataComboBoxColumn Caption="Nhân viên nhập" FieldName="IDNhanVienNhap" VisibleIndex="6">
                <PropertiesComboBox DataSourceID="sqlDNhanVien" DisplayFormatString="dd/MM/yyyy" TextField="TenNguoiDung" ValueField="ID">
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataSpinEditColumn Caption="Số mặt hàng" FieldName="SoMatHang" VisibleIndex="8">
                <PropertiesSpinEdit DisplayFormatString="g">
                </PropertiesSpinEdit>
            </dx:GridViewDataSpinEditColumn>
            <dx:GridViewDataSpinEditColumn Caption="Trọng lượng" FieldName="TrongLuong" VisibleIndex="9">
                <PropertiesSpinEdit DisplayFormatString="{0:n} KG" NumberFormat="Custom">
                </PropertiesSpinEdit>
            </dx:GridViewDataSpinEditColumn>
            <dx:GridViewDataComboBoxColumn Caption="Trạng thái chuyển hàng" FieldName="IDTrangThai" VisibleIndex="11">
                <PropertiesComboBox DataSourceID="sqlTrangThaiChuyenHang" TextField="TenTrangThai" ValueField="ID">
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataSpinEditColumn Caption="Tổng tiền" FieldName="TongTien" VisibleIndex="10">
                <PropertiesSpinEdit DisplayFormatString="{0:#,# VND}" NumberFormat="Custom">
                </PropertiesSpinEdit>
            </dx:GridViewDataSpinEditColumn>
            <dx:GridViewDataDateColumn Caption="Ngày nhập" FieldName="NgayNhap" VisibleIndex="7">
                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataComboBoxColumn Caption="Người lập phiếu" FieldName="IDNhanVienLap" VisibleIndex="0">
                <PropertiesComboBox DataSourceID="sqlDNhanVien" TextField="TenNguoiDung" ValueField="ID">
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataDateColumn Caption="Ngày lập" FieldName="NgayLap" VisibleIndex="1">
                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>
        </Columns>
        <Styles>
            <Header Font-Bold="True" HorizontalAlign="Center">
            </Header>
            <AlternatingRow Enabled="True">
            </AlternatingRow>
            <TitlePanel Font-Bold="True" HorizontalAlign="Left">
            </TitlePanel>
        </Styles>
    </dx:ASPxGridView>

        <asp:SqlDataSource ID="sqlTrangThaiChuyenHang" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenTrangThai] FROM [GPM_TrangThaiChuyenHang]"></asp:SqlDataSource>
       <asp:SqlDataSource ID="sqlDNhanVien" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenNguoiDung] FROM [GPM_NguoiDung] WHERE ([DaXoa] = @DaXoa)">
           <SelectParameters>
               <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
           </SelectParameters>
       </asp:SqlDataSource>
       <asp:SqlDataSource ID="sqlKho" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenCuaHang] FROM [GPM_Kho] WHERE ([DaXoa] = @DaXoa)">
           <SelectParameters>
               <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
           </SelectParameters>
       </asp:SqlDataSource>

    <%--popup chi tiet don hang--%>
     <dx:ASPxPopupControl ID="popup" runat="server" AllowDragging="True" AllowResize="True" 
         PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"  Width="1100px"
         Height="600px" FooterText="Thông tin chi tiết"
        HeaderText="Thông tin chi tiết phiếu chuyển kho" ClientInstanceName="popup" EnableHierarchyRecreation="True" CloseAction="CloseButton">
    </dx:ASPxPopupControl>
</asp:Content>
