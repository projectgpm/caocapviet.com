﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChiTietDonHangThuMua.aspx.cs" Inherits="BanHang.ChiTietDonHangThuMua" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="5" Width="100%">
            <Items>
                <dx:LayoutItem Caption="Lý do hủy" ColSpan="5" RowSpan="2">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server">
                            <dx:ASPxMemo ID="memoLyDo" runat="server" Width="100%">
                            </dx:ASPxMemo>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server">
                            <dx:ASPxButton ID="btnHuyDonHang" runat="server" OnClick="btnHuyDonHang_Click1" Text="Hủy đơn hàng" style="height: 34px">
                                <Image IconID="edit_delete_16x16">
                                </Image>
                            </dx:ASPxButton>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server">
                            <dx:ASPxButton ID="ASPxFormLayout1_E1" runat="server" OnClick="ASPxFormLayout1_E1_Click" Text="In">
                                <Image IconID="print_defaultprinternetwork_16x16">
                                </Image>
                            </dx:ASPxButton>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
            </Items>
        </dx:ASPxFormLayout>
        <dx:ASPxGridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gridChiTiet" KeyFieldName="ID" OnRowUpdating="gridChiTiet_RowUpdating">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
<Settings ShowTitlePanel="True" ShowFooter="True" ShowFilterRow="True"></Settings>

        <SettingsBehavior ConfirmDelete="True" />

        <SettingsCommandButton>
        <ShowAdaptiveDetailButton ButtonType="Image"></ShowAdaptiveDetailButton>

        <HideAdaptiveDetailButton ButtonType="Image"></HideAdaptiveDetailButton>
            <NewButton ButtonType="Image" RenderMode="Image">
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

<SettingsText Title="THÔNG TIN CHI TIẾT" CommandDelete="Xóa" ConfirmDelete="Bạn chắc chắn muốn xóa?" CommandEdit="Sửa" EmptyDataRow="Danh sách hàng hóa trống." SearchPanelEditorNullText="Nhập thông tin cần tìm..."></SettingsText>
        <EditFormLayoutProperties>
            <Items>
                <dx:GridViewColumnLayoutItem ColumnName="Số Lượng">
                </dx:GridViewColumnLayoutItem>
                <dx:EditModeCommandLayoutItem HorizontalAlign="Right">
                </dx:EditModeCommandLayoutItem>
            </Items>
        </EditFormLayoutProperties>
<Columns>
    
    <dx:GridViewCommandColumn Name="chucnang" ShowEditButton="True" VisibleIndex="10" ShowClearFilterButton="True">
    </dx:GridViewCommandColumn>
    
    <dx:GridViewDataSpinEditColumn Caption="Số Lượng" FieldName="SoLuong" VisibleIndex="4">
        <propertiesspinedit DisplayFormatString="N0">
            <ValidationSettings SetFocusOnError="True">
                <RequiredField IsRequired="True" />
            </ValidationSettings>
        </propertiesspinedit>
    </dx:GridViewDataSpinEditColumn>
    <dx:GridViewDataSpinEditColumn Caption="Đơn Giá(-VAT)" FieldName="DonGia" VisibleIndex="5">
        <propertiesspinedit DisplayFormatString="N0" DisplayFormatInEditMode="True"></propertiesspinedit>
    </dx:GridViewDataSpinEditColumn>
    <dx:GridViewDataSpinEditColumn Caption="Thành Tiền(-VAT)" FieldName="ThanhTien" VisibleIndex="7" ReadOnly="True">
        <propertiesspinedit DisplayFormatString="N0"></propertiesspinedit>
    </dx:GridViewDataSpinEditColumn>
    <dx:GridViewDataTextColumn Caption="Mã Hàng" FieldName="MaHang" VisibleIndex="0" ReadOnly="True">
    </dx:GridViewDataTextColumn>
    <dx:GridViewDataSpinEditColumn Caption="Trọng Lượng" FieldName="TrongLuong" VisibleIndex="3" ReadOnly="True">
        <PropertiesSpinEdit DisplayFormatString="{0:n} KG" NumberFormat="Custom">
        </PropertiesSpinEdit>
    </dx:GridViewDataSpinEditColumn>
    <dx:GridViewDataComboBoxColumn Caption="Hàng Hóa" FieldName="IDHangHoa" ReadOnly="True" VisibleIndex="1">
        <PropertiesComboBox DataSourceID="SqlHangHoa" TextField="TenHangHoa" ValueField="ID">
        </PropertiesComboBox>
    </dx:GridViewDataComboBoxColumn>
    <dx:GridViewDataComboBoxColumn Caption="Đơn Vị Tính" FieldName="IDDonViTinh" VisibleIndex="2" ReadOnly="True">
        <PropertiesComboBox DataSourceID="SqlDonViTinh" TextField="TenDonViTinh" ValueField="ID">
        </PropertiesComboBox>
    </dx:GridViewDataComboBoxColumn>
    <dx:GridViewDataSpinEditColumn Caption="Đơn Giá(+VAT)" FieldName="DonGia2" VisibleIndex="6">
        <PropertiesSpinEdit DisplayFormatString="N0" NumberFormat="Custom">
        </PropertiesSpinEdit>
    </dx:GridViewDataSpinEditColumn>
    <dx:GridViewDataSpinEditColumn Caption="Thành Tiền(+VAT)" FieldName="ThanhTien2" VisibleIndex="8">
        <PropertiesSpinEdit DisplayFormatString="N0" NumberFormat="Custom">
        </PropertiesSpinEdit>
    </dx:GridViewDataSpinEditColumn>
    <dx:GridViewDataTextColumn Caption="Ghi chú" FieldName="GhiChu" VisibleIndex="9">
    </dx:GridViewDataTextColumn>
</Columns>

            <TotalSummary>
                <dx:ASPxSummaryItem DisplayFormat="Tổng = {0:N0}" FieldName="ThanhTien2" ShowInColumn="Thành Tiền(+VAT)" SummaryType="Sum" />
                <dx:ASPxSummaryItem DisplayFormat="Tổng = {0:N0}" FieldName="ThanhTien" ShowInColumn="Thành Tiền(-VAT)" SummaryType="Sum" />
                <dx:ASPxSummaryItem DisplayFormat="Tổng = {0:N0}" FieldName="SoLuong" ShowInColumn="Số Lượng" SummaryType="Sum" />
                <dx:ASPxSummaryItem DisplayFormat="Tổng mặt hàng : {0}" FieldName="MaHang" ShowInColumn="Mã Hàng" SummaryType="Count" />
            </TotalSummary>

<Styles>
<Header HorizontalAlign="Center" Font-Bold="True"></Header>

<AlternatingRow Enabled="True"></AlternatingRow>

<TitlePanel HorizontalAlign="Left" Font-Bold="True"></TitlePanel>
</Styles>
  
</dx:ASPxGridView>
            <asp:SqlDataSource ID="SqlDonViTinh" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenDonViTinh] FROM [GPM_DonViTinh] WHERE ([DaXoa] = @DaXoa)">
                <SelectParameters>
                    <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlHangHoa" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenHangHoa] FROM [GPM_HangHoa] WHERE (([DaXoa] = @DaXoa) AND ([TenHangHoa] IS NOT NULL))">
                <SelectParameters>
                    <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
    </div>
         <dx:ASPxPopupControl ID="popup" runat="server" AllowDragging="True" AllowResize="True" 
         PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"  Width="1100px"
         Height="600px" FooterText="Thông tin chi tiết hàng hóa combo"
        HeaderText="Thông tin chi tiết đơn hàng" ClientInstanceName="popup" EnableHierarchyRecreation="True" CloseAction="CloseButton">
    </dx:ASPxPopupControl>
        <iframe name="PrintingFrame" style="display: block; width:0px; height:0px;"></iframe>
    </form>
</body>
</html>
