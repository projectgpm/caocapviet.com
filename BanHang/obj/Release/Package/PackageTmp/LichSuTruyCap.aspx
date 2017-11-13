 <%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="LichSuTruyCap.aspx.cs" Inherits="BanHang.LichSuTruyCap" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
      <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="6" Width="100%">
        <Items>
            <dx:LayoutGroup Caption="Lọc" ColCount="5" ColSpan="6">
                <Items>
                    <dx:LayoutItem Caption="Từ Ngày">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server">
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
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server">
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
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server">
                                <dx:ASPxButton ID="btnLoc" runat="server" OnClick="btnLoc_Click" Text="Lọc">
                                    <Image IconID="filter_multiplemasterfilter_32x32">
                                    </Image>
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Hiển Thị">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server">
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
    <dx:ASPxGridView ID="gridLichSuTruyCap" runat="server" AutoGenerateColumns="False" KeyFieldName="ID" Width="100%">
        <SettingsEditing Mode="PopupEditForm">
        </SettingsEditing>
        <Settings ShowFilterRow="True" ShowTitlePanel="True" />
       
       
<SettingsCommandButton>
<ShowAdaptiveDetailButton ButtonType="Image"></ShowAdaptiveDetailButton>

<HideAdaptiveDetailButton ButtonType="Image"></HideAdaptiveDetailButton>
</SettingsCommandButton>

       
        <SettingsSearchPanel Visible="True" />
        <SettingsText CommandDelete="Xóa" CommandEdit="Sửa" CommandNew="Thêm" ConfirmDelete="Bạn có chắc chắn muốn xóa không?" PopupEditFormCaption="Thông tin danh mục thuế" Title="DANH SÁCH LỊCH SỬ TRUY CẬP HỆ THỐNG" EmptyDataRow="Danh sách trống." SearchPanelEditorNullText="Nhập thông tin cần tìm..." />
        <Columns>
            <dx:GridViewDataTextColumn Caption="Tên Chức Năng" FieldName="TenChucNang" VisibleIndex="0">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Đối Tượng" FieldName="NoiDung" VisibleIndex="2">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataComboBoxColumn Caption="Tên Nhân Viên" FieldName="IDNhanVien" VisibleIndex="3">
                <PropertiesComboBox DataSourceID="sqlNguoiDung" TextField="TenNguoiDung" ValueField="ID">
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataComboBoxColumn Caption="Nhóm Người Dùng" FieldName="IDNhom" VisibleIndex="4">
                <PropertiesComboBox DataSourceID="sqlNhomNguoiDung" TextField="TenNhom" ValueField="ID">
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataDateColumn Caption="Thời Gian" FieldName="ThoiGian" VisibleIndex="6">
                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy hh:mm:ss tt">
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataComboBoxColumn Caption="Chi Nhánh" FieldName="IDKho" VisibleIndex="7">
                <PropertiesComboBox DataSourceID="sqlKho" TextField="TenCuaHang" ValueField="ID">
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataTextColumn Caption="Tên PC" FieldName="TenMay" VisibleIndex="5">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Hành Động" FieldName="HanhDong" VisibleIndex="1">
            </dx:GridViewDataTextColumn>
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
<asp:SqlDataSource ID="sqlKho" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenCuaHang] FROM [GPM_Kho] WHERE ([DaXoa] = @DaXoa)">
    <SelectParameters>
        <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqlNhomNguoiDung" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenNhom] FROM [GPM_NhomNguoiDung]">
    <SelectParameters>
        <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqlNguoiDung" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenNguoiDung] FROM [GPM_NguoiDung] WHERE ([DaXoa] = @DaXoa)">
    <SelectParameters>
        <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
</asp:Content>
