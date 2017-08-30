<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="HangHoa.aspx.cs" Inherits="BanHang.HangHoa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <script type="text/javascript">
    function OnMoreInfoClick(element, key) {
        window.location=("NewsHangHoa.aspx?IDHH=" + key);
        // alert(key);
    }
</script>
    <br />
    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%"><PanelCollection>
    <dx:PanelContent runat="server">
        <dx:ASPxButton ID="XuatFilePDF" runat="server" OnClick="XuatFilePDF_Click" Text="Xuất PDF">
            <Image IconID="export_exporttopdf_16x16">
            </Image>
        </dx:ASPxButton>
        <dx:ASPxButton ID="btnXuatExcel" runat="server" OnClick="btnXuatExcel_Click" Text="Xuất Excel">
             <Image IconID="export_exporttoxls_16x16">
             </Image>
        </dx:ASPxButton>
        <dx:ASPxButton ID="btnNhapExel" runat="server" OnClick="btnNhapExcel_Click" Text="Nhập Exel">
            <Image IconID="export_exporttoxls_16x16">
            </Image>
        </dx:ASPxButton>
        <dx:ASPxButton ID="btnTheMoi" runat="server" OnClick="btnTheMoi_Click" Text="Thêm HH Mới">
            <Image IconID="actions_addfile_16x16">
            </Image>
        </dx:ASPxButton>
        <dx:ASPxGridViewExporter ID="HangHoaExport" runat="server" ExportedRowType="All" FileName="HangHoaExport" GridViewID="HangHoaExport1">
        </dx:ASPxGridViewExporter>
    </dx:PanelContent>
    </PanelCollection>
    </dx:ASPxPanel>
    <dx:ASPxPanel ID="ASPxPanel2" runat="server" Width="100%" DefaultButton="ASPxButton1">
        <PanelCollection>
        <dx:PanelContent ID="PanelContent1" runat="server">
            <dx:ASPxGridView ID="gridHangHoa" runat="server" AutoGenerateColumns="False" Width="100%" KeyFieldName="ID" OnRowDeleting="gridHangHoa_RowDeleting1">
                    <SettingsDetail ShowDetailRow="True" />
                    <Templates>
                        <DetailRow>
                            <dx:ASPxGridView ID="gridBarCode" runat="server" AutoGenerateColumns="False" 
                                KeyFieldName="ID" oninit="gridBarCode_Init">
                                <SettingsCommandButton>
                                    <ShowAdaptiveDetailButton ButtonType="Image">
                                    </ShowAdaptiveDetailButton>
                                    <HideAdaptiveDetailButton ButtonType="Image">
                                    </HideAdaptiveDetailButton>
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
                                <Columns>
                                    <dx:GridViewCommandColumn ShowDeleteButton="false" ShowEditButton="false" 
                                        ShowNewButtonInHeader="false" VisibleIndex="4">
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn Caption="ID" FieldName="ID" VisibleIndex="0">
                                        <EditFormSettings Visible="False" />
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataComboBoxColumn Caption="Trạng thái barcode" FieldName="IDTrangThaiBarcode" ShowInCustomizationForm="True" VisibleIndex="1">
                                        <PropertiesComboBox DataSourceID="sqlTrangThaiBarcode" TextField="TenTrangThai" ValueField="ID">
                                        </PropertiesComboBox>
                                    </dx:GridViewDataComboBoxColumn>
                                    <dx:GridViewDataTextColumn Caption="BarCode" FieldName="Barcode" 
                                        VisibleIndex="2">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataDateColumn Caption="Ngày cập nhật"  FieldName="NgayCapNhat" ShowInCustomizationForm="True" VisibleIndex="25">
                                        <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                                        </PropertiesDateEdit>
                                    </dx:GridViewDataDateColumn>
                                </Columns>
                                <Styles>
                                    <Header HorizontalAlign="Center">
                                    </Header>
                                </Styles>
                            </dx:ASPxGridView>
                        </DetailRow>
                    </Templates>
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
                    <SettingsText CommandDelete="Xóa" CommandEdit="Sửa" CommandNew="Thêm" ConfirmDelete="Bạn có chắc chắn muốn xóa không?" PopupEditFormCaption="Thông tin hàng hóa" Title="DANH SÁCH HÀNG HÓA" />
                    <Columns>
                        <dx:GridViewCommandColumn ShowDeleteButton="True" ShowInCustomizationForm="True" VisibleIndex="1">
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn Caption="ID" FieldName="ID" ShowInCustomizationForm="True" VisibleIndex="2" Visible="False">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataButtonEditColumn Caption="Sửa" VisibleIndex="0">
                            <DataItemTemplate>
                                <a href="javascript:void(0);" onclick="OnMoreInfoClick(this, '<%# Container.KeyValue %>')">Sửa</a>
                            </DataItemTemplate>
                        </dx:GridViewDataButtonEditColumn>
                        <dx:GridViewDataTextColumn FieldName="TenHangHoa" ShowInCustomizationForm="True" VisibleIndex="5" Caption="Tên hàng hóa">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="MaHang" ShowInCustomizationForm="True" VisibleIndex="4" Caption="Mã hàng">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="GhiChu" ShowInCustomizationForm="True" VisibleIndex="24" Caption="Ghi chú">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataComboBoxColumn Caption="Nhóm hàng" FieldName="IDNhomHang" ShowInCustomizationForm="True" VisibleIndex="3">
                            <PropertiesComboBox DataSourceID="sqlNhomHang" TextField="TenNhomHang" ValueField="ID">
                            </PropertiesComboBox>
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataComboBoxColumn Caption="Đơn vị tính" FieldName="IDDonViTinh" ShowInCustomizationForm="True" VisibleIndex="6">
                            <PropertiesComboBox DataSourceID="sqlDonViTinh" TextField="TenDonViTinh" ValueField="ID">
                            </PropertiesComboBox>
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataComboBoxColumn Caption="Hãng sản xuất" FieldName="IDHangSanXuat" ShowInCustomizationForm="True" VisibleIndex="8">
                            <PropertiesComboBox DataSourceID="sqlHangSX" TextField="TenHangSanXuat" ValueField="ID">
                            </PropertiesComboBox>
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataComboBoxColumn Caption="Thuế" FieldName="IDThue" ShowInCustomizationForm="True" VisibleIndex="9">
                            <PropertiesComboBox DataSourceID="sqlThue" TextField="TenThue" ValueField="ID">
                            </PropertiesComboBox>
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataComboBoxColumn Caption="Hàng quy đổi" FieldName="IDHangQuyDoi" ShowInCustomizationForm="True" VisibleIndex="10">
                            <PropertiesComboBox DataSourceID="sqlHangHoaDoi" TextField="TenHangHoa" ValueField="ID">
                            </PropertiesComboBox>
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataComboBoxColumn Caption="Nhóm đặt hàng" FieldName="IDNhomDatHang" ShowInCustomizationForm="True" VisibleIndex="11">
                            <PropertiesComboBox DataSourceID="sqlNhomDatHang" TextField="TenNhom" ValueField="ID">
                            </PropertiesComboBox>
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataSpinEditColumn Caption="Giá mua trước thuế" FieldName="GiaMuaTruocThue" ShowInCustomizationForm="True" VisibleIndex="12">
                            <PropertiesSpinEdit DisplayFormatString="{0:#,# VND}" NumberFormat="Custom">
                            </PropertiesSpinEdit>
                        </dx:GridViewDataSpinEditColumn>
                        <dx:GridViewDataSpinEditColumn Caption="Giá bán trước thuế" FieldName="GiaBanTruocThue" ShowInCustomizationForm="True" VisibleIndex="13">
                            <PropertiesSpinEdit DisplayFormatString="{0:#,# VND}" NumberFormat="Custom">
                            </PropertiesSpinEdit>
                        </dx:GridViewDataSpinEditColumn>
                        <dx:GridViewDataSpinEditColumn Caption="Giá mua sau thuế" FieldName="GiaMuaSauThue" ShowInCustomizationForm="True" VisibleIndex="14">
                            <PropertiesSpinEdit DisplayFormatString="{0:#,# VND}" NumberFormat="Custom">
                            </PropertiesSpinEdit>
                        </dx:GridViewDataSpinEditColumn>
                        <dx:GridViewDataSpinEditColumn Caption="Giá bán sau thuế" FieldName="GiaBanSauThue" ShowInCustomizationForm="True" VisibleIndex="15">
                            <PropertiesSpinEdit DisplayFormatString="{0:#,# VND}" NumberFormat="Custom">
                            </PropertiesSpinEdit>
                        </dx:GridViewDataSpinEditColumn>
                        <dx:GridViewDataSpinEditColumn Caption="Giá bán 1" FieldName="GiaBan1" ShowInCustomizationForm="True" Visible="False" VisibleIndex="16">
                            <PropertiesSpinEdit DisplayFormatString="{0:#,# VND}" NumberFormat="Custom">
                            </PropertiesSpinEdit>
                        </dx:GridViewDataSpinEditColumn>
                        <dx:GridViewDataSpinEditColumn Caption="Giá bán 2" FieldName="GiaBan2" ShowInCustomizationForm="True" Visible="False" VisibleIndex="17">
                            <PropertiesSpinEdit DisplayFormatString="{0:#,# VND}" NumberFormat="Custom">
                            </PropertiesSpinEdit>
                        </dx:GridViewDataSpinEditColumn>
                        <dx:GridViewDataSpinEditColumn Caption="Giá bán 3" FieldName="GiaBan3" ShowInCustomizationForm="True" Visible="False" VisibleIndex="18">
                            <PropertiesSpinEdit DisplayFormatString="{0:#,# VND}" NumberFormat="Custom">
                            </PropertiesSpinEdit>
                        </dx:GridViewDataSpinEditColumn>
                        <dx:GridViewDataSpinEditColumn Caption="Giá bán 4" FieldName="GiaBan4" ShowInCustomizationForm="True" Visible="False" VisibleIndex="19">
                            <PropertiesSpinEdit DisplayFormatString="{0:#,# VND}" NumberFormat="Custom">
                            </PropertiesSpinEdit>
                        </dx:GridViewDataSpinEditColumn>
                        <dx:GridViewDataSpinEditColumn Caption="Giá bán 5" FieldName="GiaBan5" ShowInCustomizationForm="True" Visible="False" VisibleIndex="20">
                            <PropertiesSpinEdit DisplayFormatString="{0:#,# VND}" NumberFormat="Custom">
                            </PropertiesSpinEdit>
                        </dx:GridViewDataSpinEditColumn>
                        <dx:GridViewDataSpinEditColumn Caption="Trọng lượng" FieldName="TrongLuong" ShowInCustomizationForm="True" VisibleIndex="21">
                            <PropertiesSpinEdit DisplayFormatString="{0:n} KG" NumberFormat="Custom">
                            </PropertiesSpinEdit>
                        </dx:GridViewDataSpinEditColumn>
                        <dx:GridViewDataSpinEditColumn Caption="Hạng sử dụng" FieldName="HanSuDung" ShowInCustomizationForm="True" VisibleIndex="22">
                            <PropertiesSpinEdit DisplayFormatString="{0:#,# tháng}" NumberFormat="Custom">
                            </PropertiesSpinEdit>
                        </dx:GridViewDataSpinEditColumn>
                        <dx:GridViewDataComboBoxColumn Caption="Trạng thái hàng" FieldName="IDTrangThaiHang" ShowInCustomizationForm="True" VisibleIndex="23">
                            <PropertiesComboBox DataSourceID="sqlTrangThaiHang" TextField="TenTrangThai" ValueField="ID">
                            </PropertiesComboBox>
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataDateColumn FieldName="NgayCapNhat" ShowInCustomizationForm="True" VisibleIndex="25" Caption="Ngày cập nhật">
                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                            </PropertiesDateEdit>
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataTextColumn Caption="Hệ số" FieldName="HeSo" ShowInCustomizationForm="True" VisibleIndex="7">
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
            <asp:SqlDataSource ID="sqlTrangThaiBarcode" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenTrangThai] FROM [GPM_TrangThai_Barcode]"></asp:SqlDataSource>
            <asp:SqlDataSource ID="sqlTrangThaiHang" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenTrangThai] FROM [GPM_TrangThaiHang]"></asp:SqlDataSource>
            <asp:SqlDataSource ID="sqlNhomDatHang" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenNhom] FROM [GPM_NhomDatHang]"></asp:SqlDataSource>
            <asp:SqlDataSource ID="sqlHangHoaDoi" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenHangHoa] FROM [GPM_HangHoa] WHERE ([DaXoa] = @DaXoa)">
                <SelectParameters>
                    <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="sqlThue" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenThue] FROM [GPM_Thue] WHERE ([DaXoa] = @DaXoa)">
                <SelectParameters>
                    <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="sqlHangSX" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenHangSanXuat] FROM [GPM_HangSanXuat] WHERE ([DaXoa] = @DaXoa)">
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
            <dx:ASPxGridView ID="HangHoaExport1" runat="server" Visible="False">
                <SettingsCommandButton>
                    <ShowAdaptiveDetailButton ButtonType="Image">
                    </ShowAdaptiveDetailButton>
                    <HideAdaptiveDetailButton ButtonType="Image">
                    </HideAdaptiveDetailButton>
                </SettingsCommandButton>
            </dx:ASPxGridView>
        </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>
    <%--popup chi tiet don hang--%>
     </asp:Content>
