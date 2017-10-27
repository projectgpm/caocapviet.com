<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PhanQuyen.aspx.cs" Inherits="BanHang.PhanQuyen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <dx:ASPxGridView ID="gridPhanQuyen" runat="server" Width="100%" AutoGenerateColumns="False" KeyFieldName="ID" OnRowUpdating="gridPhanQuyen_RowUpdating" OnHtmlRowPrepared="gridPhanQuyen_HtmlRowPrepared" OnRowDeleting="gridPhanQuyen_RowDeleting" OnRowInserting="gridPhanQuyen_RowInserting">
            <SettingsPager NumericButtonCount="20" PageSize="20">
            </SettingsPager>
            <SettingsEditing Mode="PopupEditForm">
            </SettingsEditing>
            <Settings ShowTitlePanel="True" />
            <SettingsBehavior ConfirmDelete="True" />
<SettingsCommandButton>
<ShowAdaptiveDetailButton ButtonType="Image"></ShowAdaptiveDetailButton>

<HideAdaptiveDetailButton ButtonType="Image"></HideAdaptiveDetailButton>
    <NewButton>
        <Image IconID="actions_add_16x16">
        </Image>
    </NewButton>
    <UpdateButton ButtonType="Image" RenderMode="Image">
        <Image IconID="actions_save_32x32devav">
        </Image>
    </UpdateButton>
    <CancelButton ButtonType="Image" RenderMode="Image">
        <Image IconID="actions_close_32x32">
        </Image>
    </CancelButton>
    <EditButton>
        <Image IconID="actions_edit_16x16devav">
        </Image>
    </EditButton>
    <DeleteButton>
        <Image IconID="actions_cancel_16x16">
        </Image>
    </DeleteButton>
</SettingsCommandButton>

            <SettingsPopup>
                <EditForm HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
            </SettingsPopup>

            <SettingsSearchPanel Visible="True" />

            <SettingsText CommandBatchEditCancel="Hủy thao tác" CommandBatchEditUpdate="Lưu tất cả" Title="DANH SÁCH QUYỀN  ĐƯỢC TRUY CẬP" CommandDelete="Xóa" CommandEdit="Sửa" CommandNew="Thêm mới" EmptyDataRow="Danh sách trống" PopupEditFormCaption="Cấp Quyền" SearchPanelEditorNullText="Nhập thông tin cần tìm..." ConfirmDelete="Bạn muốn xóa ??" />
            <EditFormLayoutProperties ColCount="2">
                <Items>
                    <dx:GridViewColumnLayoutItem ColSpan="2" ColumnName="Danh Mục">
                    </dx:GridViewColumnLayoutItem>
                    <dx:GridViewColumnLayoutItem ColumnName="Hiển Thị">
                    </dx:GridViewColumnLayoutItem>
                    <dx:GridViewColumnLayoutItem ColumnName="Thêm/ Xóa /Sửa">
                    </dx:GridViewColumnLayoutItem>
                    <dx:EditModeCommandLayoutItem ColSpan="2" HorizontalAlign="Right">
                    </dx:EditModeCommandLayoutItem>
                </Items>
            </EditFormLayoutProperties>
            <Columns>
                <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="4">
                </dx:GridViewCommandColumn>
                <dx:GridViewDataCheckColumn Caption="Hiển Thị" FieldName="TrangThai" VisibleIndex="1">
                </dx:GridViewDataCheckColumn>
                <dx:GridViewDataCheckColumn Caption="Thêm/ Xóa /Sửa" FieldName="ChucNang" VisibleIndex="2">
                </dx:GridViewDataCheckColumn>
                <dx:GridViewDataDateColumn Caption="Ngày Cập Nhật" FieldName="NgayCapNhat" VisibleIndex="3">
                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy hh:mm:ss">
                    </PropertiesDateEdit>
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataComboBoxColumn Caption="Danh Mục" FieldName="IDMenu" VisibleIndex="0">
                    <PropertiesComboBox DataSourceID="SqlDanhMucMenu" TextField="TenDanhMuc" ValueField="ID">
                        <ValidationSettings SetFocusOnError="True">
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                    </PropertiesComboBox>
                </dx:GridViewDataComboBoxColumn>
            </Columns>
            <Styles>
<Header HorizontalAlign="Center" Font-Bold="True"></Header>

<AlternatingRow Enabled="True"></AlternatingRow>

<TitlePanel HorizontalAlign="Left" Font-Bold="True"></TitlePanel>
</Styles>
        </dx:ASPxGridView>
    
        <asp:SqlDataSource ID="SqlDanhMucMenu" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenDanhMuc] FROM [GPM_Menu]"></asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>
