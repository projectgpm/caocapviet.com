﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportExcel_HangHoa.aspx.cs" Inherits="BanHang.ImportExcel_HangHoa" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%">
            <Items>
                <dx:LayoutGroup Caption="Nhập file Excel" ColCount="4" Width="300px">
                    <Items>
                        <dx:LayoutItem Caption="">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
                                    <dx:ASPxUploadControl ID="UploadFileExcel" runat="server" AllowedFileExtensions=".xls" >
                                    </dx:ASPxUploadControl>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
                                    <dx:ASPxButton ID="btnNhap" runat="server" OnClick="btnNhap_Click" Text="Upload">
                                        <Image IconID="miscellaneous_publish_16x16office2013">
                                        </Image>
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
                                    <dx:ASPxButton ID="btnThem" runat="server" OnClick="btnThem_Click" Text="Lưu tất cả dữ liệu">
                                        <Image IconID="save_saveto_16x16">
                                        </Image>
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">
                                    <dx:ASPxButton ID="btnHuy" runat="server" OnClick="btnHuy_Click" Text="Hủy">
                                        <Image IconID="actions_cancel_16x16office2013">
                                        </Image>
                                    </dx:ASPxButton>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:LayoutGroup>
                <dx:LayoutGroup Caption="Thông tin dữ liệu nhập vào">
                      <Items>
                                
                                <dx:LayoutItem Caption="" ColSpan="1">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer13" runat="server">
                                                
                                                <dx:ASPxGridView ID="gridHangHoa_Temp" runat="server" AutoGenerateColumns="False" KeyFieldName="ID" Width="100%" OnRowDeleting="gridHangHoa_Temp_RowDeleting">
        <SettingsEditing Mode="PopupEditForm">
        </SettingsEditing>
        <Settings ShowFilterRow="True" ShowTitlePanel="True" />
        <SettingsBehavior ConfirmDelete="True" />
        <SettingsCommandButton RenderMode="Image">
            <ShowAdaptiveDetailButton ButtonType="Image">
            </ShowAdaptiveDetailButton>
            <HideAdaptiveDetailButton ButtonType="Image">
            </HideAdaptiveDetailButton>
            <NewButton>
                <Image IconID="actions_add_16x16" ToolTip="Thêm mới">
                </Image>
            </NewButton>
            <UpdateButton ButtonType="Image" RenderMode="Image">
                <Image IconID="save_save_32x32office2013" ToolTip="Lưu">
                </Image>
            </UpdateButton>
            <CancelButton ButtonType="Image" RenderMode="Image">
                <Image IconID="actions_close_32x32" ToolTip="Hủy thao tác">
                </Image>
            </CancelButton>
            <EditButton ButtonType="Image" RenderMode="Image">
                <Image IconID="actions_edit_16x16devav" ToolTip="Sửa">
                </Image>
            </EditButton>
            <DeleteButton ButtonType="Image" RenderMode="Image">
                <Image IconID="actions_cancel_16x16" ToolTip="Xóa">
                </Image>
            </DeleteButton>
        </SettingsCommandButton>
        <SettingsPopup>
            <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter" />
        </SettingsPopup>
        <SettingsSearchPanel Visible="True" />
        <SettingsText CommandDelete="Xóa" CommandEdit="Sửa" CommandNew="Thêm" ConfirmDelete="Bạn có chắc chắn muốn xóa không?" PopupEditFormCaption="Thông tin khách hàng" Title="DANH SÁCH HÀNG HÓA" />
        <EditFormLayoutProperties ColCount="2">
        </EditFormLayoutProperties>
        
                <Columns>
                    <dx:GridViewCommandColumn ShowDeleteButton="True" ShowInCustomizationForm="True" VisibleIndex="0">
                    </dx:GridViewCommandColumn>
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
                                                
                                                <asp:SqlDataSource ID="sqlTrangThaiBarcode" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenTrangThai] FROM [GPM_TrangThai_Barcode]"></asp:SqlDataSource>
                                                <asp:SqlDataSource ID="sqlTrangThaiHang" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenTrangThai] FROM [GPM_TrangThaiHang]"></asp:SqlDataSource>
                                                <asp:SqlDataSource ID="sqlNhomDatHang" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenNhom] FROM [GPM_NhomDatHang]"></asp:SqlDataSource>
                                                <asp:SqlDataSource ID="sqlThue" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenThue] FROM [GPM_Thue] WHERE ([DaXoa] = @DaXoa)">
                                                    <SelectParameters>
                                                        <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                                <asp:SqlDataSource ID="sqlHangSanXuat" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenHangSanXuat] FROM [GPM_HangSanXuat] WHERE ([DaXoa] = @DaXoa)">
                                                    <SelectParameters>
                                                        <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                                <asp:SqlDataSource ID="sqlDonViTinh" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenDonViTinh] FROM [GPM_DonViTinh] WHERE ([DaXoa] = @DaXoa)">
                                                    <SelectParameters>
                                                        <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                                <asp:SqlDataSource ID="sqlNhomHang" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenNhomHang] FROM [GPM_NhomHang] WHERE ([DaXoa] = @DaXoa)">
                                                    <SelectParameters>
                                                        <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                                
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                
                            </Items>
                </dx:LayoutGroup>
            </Items>
        </dx:ASPxFormLayout>
    </div>
    </form>
</body>
</html>
