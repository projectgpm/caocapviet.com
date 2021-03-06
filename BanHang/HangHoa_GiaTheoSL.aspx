﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HangHoa_GiaTheoSL.aspx.cs" Inherits="BanHang.HangHoa_GiaTheoSL" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dx:ASPxGridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gridHangHoaGiaTheoSL" KeyFieldName="ID" OnRowDeleting="gridHangHoaGiaTheoSL_RowDeleting" OnRowInserting="gridHangHoaGiaTheoSL_RowInserting" OnRowUpdating="gridHangHoaGiaTheoSL_RowUpdating">
            <SettingsPager Mode="ShowAllRecords">
            </SettingsPager>
            <SettingsEditing Mode="PopupEditForm">
            </SettingsEditing>
            <Settings ShowTitlePanel="True"></Settings>

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

            <SettingsText CommandDelete="Xóa" ConfirmDelete="Bạn chắc chắn muốn xóa?" CommandEdit="Sửa" CommandNew="Thêm" EmptyDataRow="Danh sách hàng hóa trống" PopupEditFormCaption="Thông tin" SearchPanelEditorNullText="Nhập thông tin cần tìm..." Title="DANH SÁCH BÁN THEO SỐ LƯỢNG"></SettingsText>
            <EditFormLayoutProperties>
                <Items>
                    <dx:GridViewColumnLayoutItem ColumnName="Số Lượng Bắt Đầu" Name="SL1">
                    </dx:GridViewColumnLayoutItem>
                    <dx:GridViewColumnLayoutItem ColumnName="Số Lượng Kết Thúc" Name="SL2">
                    </dx:GridViewColumnLayoutItem>
                    <dx:GridViewColumnLayoutItem ColumnName="Giá Bán" Name="GiaBan">
                    </dx:GridViewColumnLayoutItem>
                    <dx:EditModeCommandLayoutItem HorizontalAlign="Right">
                    </dx:EditModeCommandLayoutItem>
                </Items>
            </EditFormLayoutProperties>
            <Columns>

                <dx:GridViewCommandColumn Name="chucnang" ShowEditButton="True" VisibleIndex="7" ShowDeleteButton="True" ShowNewButtonInHeader="True">
                </dx:GridViewCommandColumn>

                <dx:GridViewDataSpinEditColumn Caption="Số Lượng Bắt Đầu" FieldName="SoLuongBD" VisibleIndex="0">
                    <PropertiesSpinEdit DisplayFormatString="g">
                    </PropertiesSpinEdit>
                </dx:GridViewDataSpinEditColumn>
                <dx:GridViewDataSpinEditColumn Caption="Số Lượng Kết Thúc" FieldName="SoLuongKT" ShowInCustomizationForm="True" VisibleIndex="1">
                    <PropertiesSpinEdit DisplayFormatString="g">
                    </PropertiesSpinEdit>
                </dx:GridViewDataSpinEditColumn>
                <dx:GridViewDataSpinEditColumn Caption="Giá Bán" FieldName="GiaBan" ShowInCustomizationForm="True" VisibleIndex="3">
                    <PropertiesSpinEdit DisplayFormatString="N0" NumberFormat="Custom" DisplayFormatInEditMode="True" Increment="5000">
                    </PropertiesSpinEdit>
                </dx:GridViewDataSpinEditColumn>
                <dx:GridViewDataDateColumn Caption="Ngày Cập Nhật" FieldName="NgayCapNhat" ShowInCustomizationForm="True" VisibleIndex="6">
                    <PropertiesDateEdit DisplayFormatInEditMode="True" DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                    </PropertiesDateEdit>
                </dx:GridViewDataDateColumn>
            </Columns>

            <Styles>
                <Header HorizontalAlign="Center" Font-Bold="True"></Header>

                <AlternatingRow Enabled="True"></AlternatingRow>

                <TitlePanel HorizontalAlign="Left" Font-Bold="True"></TitlePanel>
            </Styles>

        </dx:ASPxGridView>
    </div>
    </form>
</body>
</html>
