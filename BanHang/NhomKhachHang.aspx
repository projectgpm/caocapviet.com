
<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="NhomKhachHang.aspx.cs" Inherits="BanHang.NhomKhachHang" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="8" Width="10%">
        <Items>
            <dx:LayoutItem Caption="" HorizontalAlign="Left">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
                        <dx:ASPxButton ID="btnXuatPDF" runat="server" OnClick="btnXuatPDF_Click" Text="Xuất PDF">
                            <Image IconID="export_exporttopdf_16x16">
                            </Image>
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="" HorizontalAlign="Left">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
                        <dx:ASPxButton ID="btnXuatExcel" runat="server" OnClick="btnXuatExcel_Click" Text="Xuất Excel">
                            <Image IconID="export_exporttoxls_16x16">
                            </Image>
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Hiển Thị" ColSpan="2">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">
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
    </dx:ASPxFormLayout>
    <dx:ASPxGridViewExporter ID="XuatDuLieu" runat="server">
    </dx:ASPxGridViewExporter>
    <dx:ASPxGridView ID="gridNhomKhachHang" runat="server" AutoGenerateColumns="False" Width="100%" KeyFieldName="ID" OnRowDeleting="gridNhomKhachHang_RowDeleting" OnRowInserting="gridNhomKhachHang_RowInserting" OnRowUpdating="gridNhomKhachHang_RowUpdating">
        <SettingsPager Mode="ShowAllRecords" Visible="False">
        </SettingsPager>
        <SettingsEditing Mode="PopupEditForm">
        </SettingsEditing>
        <Settings ShowFilterRow="True" ShowTitlePanel="True" />
        <SettingsBehavior ConfirmDelete="True" />
        <SettingsCommandButton>
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
        <SettingsText CommandDelete="Xóa" CommandEdit="Sửa" CommandNew="Thêm" ConfirmDelete="Bạn có chắc chắn muốn xóa không?. Thao tác này có thể dẫn đễn việc xóa các Khách Hàng của nhóm!." PopupEditFormCaption="Thông tin nhóm khách hàng" Title="DANH SÁCH NHÓM KHÁCH HÀNG HÀNG" EmptyDataRow="Không có dữ liệu hiển thị" SearchPanelEditorNullText="Nhập thông tin cần tìm..." />
        <SettingsSearchPanel Visible="True" />
        <EditFormLayoutProperties>
            <Items>
                <dx:GridViewColumnLayoutItem ColumnName="Tên Nhóm" Name="TenNhomKhachHang">
                </dx:GridViewColumnLayoutItem>
                <dx:GridViewColumnLayoutItem ColumnName="Ghi Chú" Name="GhiChu">
                </dx:GridViewColumnLayoutItem>
                <dx:EditModeCommandLayoutItem HorizontalAlign="Right">
                </dx:EditModeCommandLayoutItem>
            </Items>
        </EditFormLayoutProperties>
        <Columns>
            <dx:GridViewCommandColumn ShowClearFilterButton="True" ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="5" Name="chucnang">
            </dx:GridViewCommandColumn>
            <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Tên Nhóm" FieldName="TenNhomKhachHang">
                <PropertiesTextEdit>
                    <ValidationSettings SetFocusOnError="True">
                        <RequiredField IsRequired="True" />
                    </ValidationSettings>
                </PropertiesTextEdit>
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataTextColumn Caption="Ghi Chú" FieldName="GhiChu" VisibleIndex="2">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataDateColumn Caption="Ngày Cập Nhật" FieldName="NgayCapNhat" VisibleIndex="4">
                <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                </PropertiesDateEdit>
            </dx:GridViewDataDateColumn>
            <dx:GridViewDataComboBoxColumn Caption="Chi Nhánh" FieldName="IDKho" VisibleIndex="3">
                <PropertiesComboBox DataSourceID="SqlKho" TextField="TenCuaHang" ValueField="ID">
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
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
    <asp:SqlDataSource ID="SqlKho" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenCuaHang] FROM [GPM_Kho] WHERE ([DaXoa] = @DaXoa)">
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
