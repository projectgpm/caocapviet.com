<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PhanQuyenVaoChiNhanh.aspx.cs" Inherits="BanHang.PhanQuyenVaoChiNhanh" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <dx:ASPxGridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gridDanhSachQuyen" KeyFieldName="ID" OnRowUpdating="gridDanhSachQuyen_RowUpdating" OnRowDeleting="gridDanhSachQuyen_RowDeleting" OnRowInserting="gridDanhSachQuyen_RowInserting">
        <SettingsPager Mode="ShowAllRecords">
        </SettingsPager>
        <SettingsEditing Mode="PopupEditForm">
        </SettingsEditing>
<Settings ShowTitlePanel="True" ShowFooter="True" ShowFilterRow="True"></Settings>

        <SettingsBehavior ConfirmDelete="True" />

        <SettingsCommandButton>
        <ShowAdaptiveDetailButton ButtonType="Image"></ShowAdaptiveDetailButton>

        <HideAdaptiveDetailButton ButtonType="Image"></HideAdaptiveDetailButton>
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
            <EditButton>
                <Image IconID="actions_edit_16x16devav" ToolTip="Sửa">
                </Image>
            </EditButton>
            <DeleteButton>
                <Image IconID="actions_cancel_16x16" ToolTip="Xóa">
                </Image>
            </DeleteButton>
        </SettingsCommandButton>

        <SettingsPopup>
            <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter" />
        </SettingsPopup>

        <SettingsSearchPanel Visible="True" />

<SettingsText Title="THÔNG TIN QUYỀN ĐƯỢC PHÉP TRUY CẬP VÀO CHI NHÁNH" CommandDelete="Xóa" ConfirmDelete="Bạn chắc chắn muốn xóa?" CommandEdit="Sửa" EmptyDataRow="Không có quyền vào chi nhánh khác" SearchPanelEditorNullText="Nhập thông tin cần tìm..." CommandNew="Thêm mới"></SettingsText>
        <EditFormLayoutProperties>
            <Items>
                <dx:GridViewColumnLayoutItem ColumnName="Chi Nhánh">
                </dx:GridViewColumnLayoutItem>
                <dx:EditModeCommandLayoutItem HorizontalAlign="Right">
                </dx:EditModeCommandLayoutItem>
            </Items>
        </EditFormLayoutProperties>
        <Columns>
            <dx:GridViewCommandColumn ShowDeleteButton="True" ShowNewButtonInHeader="True" VisibleIndex="3" Name="chucnang">
            </dx:GridViewCommandColumn>
            <dx:GridViewDataComboBoxColumn Caption="Người Dùng" FieldName="IDNhanVien" VisibleIndex="0">
                <PropertiesComboBox DataSourceID="SqlNhanVien" TextField="TenNguoiDung" ValueField="ID">
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataComboBoxColumn Caption="Chi Nhánh" FieldName="IDKho" VisibleIndex="1">
                <PropertiesComboBox DataSourceID="SqlKHo" TextField="TenCuaHang" ValueField="ID">
                    <ValidationSettings SetFocusOnError="True">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataDateColumn Caption="Ngày Cập Nhật" FieldName="NgayCapNhat" VisibleIndex="2">
                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>
        </Columns>

<Styles>
<Header HorizontalAlign="Center" Font-Bold="True"></Header>

<AlternatingRow Enabled="True"></AlternatingRow>

<TitlePanel HorizontalAlign="Left" Font-Bold="True"></TitlePanel>
</Styles>
  
</dx:ASPxGridView>
            <asp:SqlDataSource ID="SqlKHo" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenCuaHang] FROM [GPM_Kho] WHERE ([DaXoa] = @DaXoa)">
                <SelectParameters>
                    <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlNhanVien" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenNguoiDung] FROM [GPM_NguoiDung] WHERE ([DaXoa] = @DaXoa)">
            <SelectParameters>
                <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>

    </div>
    </form>
</body>
</html>
