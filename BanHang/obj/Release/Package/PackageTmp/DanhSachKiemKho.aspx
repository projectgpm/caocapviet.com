<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="DanhSachKiemKho.aspx.cs" Inherits="BanHang.DanhSachKiemKho" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <%--popup chi tiet don hang--%>
     <script type="text/javascript">
         function OnMoreInfoClick(element, key) {
             popup.SetContentUrl("ChiTietPhieuKiemKho.aspx?IDPhieuKiemKho=" + key);
             popup.ShowAtElement();
             // alert(key);
         }

    </script>
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="5">
        <Items>
            <dx:LayoutItem Caption="">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
                        <dx:ASPxButton ID="btnKiemKho" runat="server" Text="Thêm Phiếu Kiểm Kho" HorizontalAlign="Right" VerticalAlign="Middle" PostBackUrl="KiemKho.aspx">
                            <Image IconID="actions_add_32x32">
                            </Image>
                            <Paddings Padding="4px" />
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
             <dx:LayoutGroup Caption="Lọc" ColCount="5" ColSpan="5">
                 <Items>
                     <dx:LayoutItem Caption="Từ Ngày">
                         <LayoutItemNestedControlCollection>
                             <dx:LayoutItemNestedControlContainer runat="server">
                                 <dx:ASPxDateEdit ID="dateTuNgay" runat="server" DisplayFormatString="dd/MM/yyyy" Width="100%">
                                     <ValidationSettings SetFocusOnError="True">
                                         <RequiredField IsRequired="True" />
                                     </ValidationSettings>
                                 </dx:ASPxDateEdit>
                             </dx:LayoutItemNestedControlContainer>
                         </LayoutItemNestedControlCollection>
                     </dx:LayoutItem>
                     <dx:LayoutItem Caption="Đến Ngày">
                         <LayoutItemNestedControlCollection>
                             <dx:LayoutItemNestedControlContainer runat="server">
                                 <dx:ASPxDateEdit ID="dateDenNgay" runat="server" DisplayFormatString="dd/MM/yyyy" Width="100%">
                                     <ValidationSettings SetFocusOnError="True">
                                         <RequiredField IsRequired="True" />
                                     </ValidationSettings>
                                 </dx:ASPxDateEdit>
                             </dx:LayoutItemNestedControlContainer>
                         </LayoutItemNestedControlCollection>
                     </dx:LayoutItem>
                     <dx:LayoutItem Caption="">
                         <LayoutItemNestedControlCollection>
                             <dx:LayoutItemNestedControlContainer runat="server">
                                 <dx:ASPxButton ID="btnLoc" runat="server" OnClick="btnLoc_Click" Text="Lọc">
                                     <Image IconID="filter_multiplemasterfilter_32x32">
                                     </Image>
                                 </dx:ASPxButton>
                             </dx:LayoutItemNestedControlContainer>
                         </LayoutItemNestedControlCollection>
                     </dx:LayoutItem>
                     <dx:LayoutItem Caption="Hiển Thị">
                         <LayoutItemNestedControlCollection>
                             <dx:LayoutItemNestedControlContainer runat="server">
                                 <dx:ASPxComboBox ID="cmbHienThi" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmbHienThi_SelectedIndexChanged" SelectedIndex="0">
                                     <Items>
                                         <dx:ListEditItem Selected="True" Text="50" Value="50" />
                                         <dx:ListEditItem Text="100" Value="100" />
                                         <dx:ListEditItem Text="200" Value="200" />
                                         <dx:ListEditItem Text="500" Value="500" />
                                         <dx:ListEditItem Text="1000" Value="1000" />
                                         <dx:ListEditItem Text="Tất Cả" Value="50000000000000" />
                                     </Items>
                                 </dx:ASPxComboBox>
                             </dx:LayoutItemNestedControlContainer>
                         </LayoutItemNestedControlCollection>
                     </dx:LayoutItem>
                 </Items>
            </dx:LayoutGroup>
        </Items>
      </dx:ASPxFormLayout> 
    <dx:ASPxGridView ID="gridDanhSachKiemKho" runat="server" AutoGenerateColumns="False" Width="100%" KeyFieldName="ID">
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

        

        <SettingsText CommandDelete="Xóa" CommandEdit="Sửa" CommandNew="Thêm" Title="DANH SÁCH KIỂM KHO" ConfirmDelete="Bạn chắc chắn muốn xóa?" EmptyDataRow="Danh sách kiểm kho trống." SearchPanelEditorNullText="Nhập thông tin cần tìm..."/>
        <Columns>
            <dx:GridViewDataButtonEditColumn Caption="Xem Chi Tiết" VisibleIndex="7">
                
                <DataItemTemplate>
                    <a href="javascript:void(0);" onclick="OnMoreInfoClick(this, '<%# Container.KeyValue %>')">Xem </a>
                </DataItemTemplate>
                <HeaderStyle Wrap="True" />
            </dx:GridViewDataButtonEditColumn>
            <dx:GridViewDataComboBoxColumn Caption="Chi Nhánh Điều Chỉnh" FieldName="IDKho" VisibleIndex="0">
                <PropertiesComboBox DataSourceID="sqlKhoHang" TextField="TenCuaHang" ValueField="ID">
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataTextColumn Caption="Ghi Chú" VisibleIndex="3" FieldName="GhiChu">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataComboBoxColumn Caption="Người Điều Chỉnh" VisibleIndex="1" FieldName="IDNguoiDung">
                <PropertiesComboBox DataSourceID="sqlNguoiDung" TextField="TenNguoiDung" ValueField="ID">
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataDateColumn Caption="Ngày Điều Chỉnh" VisibleIndex="2" FieldName="NgayKiemKho">
                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataComboBoxColumn Caption="Trạng Thái" FieldName="TrangThai" VisibleIndex="4">
                <PropertiesComboBox>
                    <Items>
                        <dx:ListEditItem Text="Đã duyệt" Value="1" />
                        <dx:ListEditItem Text="Chưa duyệt" Value="0" />
                    </Items>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataComboBoxColumn Caption="Người Duyệt" FieldName="IDNguoiDuyet" VisibleIndex="5">
                <PropertiesComboBox DataSourceID="sqlNguoiDung" TextField="TenNguoiDung" ValueField="ID">
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataDateColumn Caption="Ngày Duyệt" FieldName="NgayDuyet" VisibleIndex="6">
                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy hh:mm:ss">
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

    <asp:SqlDataSource ID="sqlNguoiDung" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenNguoiDung] FROM [GPM_NguoiDung] WHERE ([DaXoa] = @DaXoa)">
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:SqlDataSource ID="sqlKhoHang" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenCuaHang] FROM [GPM_Kho] WHERE ([DaXoa] = @DaXoa)">
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

    <%--popup chi tiet don hang--%>
     <dx:ASPxPopupControl ID="popup" runat="server" AllowDragging="True" AllowResize="True" 
         PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"  Width="1100px"
         Height="600px" FooterText="Thông tin chi tiết đơn đặt hàng"
        HeaderText="Thông tin chi tiết phiếu kiểm kho" ClientInstanceName="popup" EnableHierarchyRecreation="True" CloseAction="CloseButton">
    </dx:ASPxPopupControl>
    </asp:Content>
