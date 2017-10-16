<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="HangHoa.aspx.cs" Inherits="BanHang.HangHoa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <script type="text/javascript">
        function OnMoreInfoClick_HangHoa(element, key) {
            popup.SetContentUrl("HangHoa_ChiTiet.aspx?IDHangHoa=" + key);
            popup.ShowAtElement();
            // alert(key);
        };
        function OnMoreInfoClick_Barcode(element, key) {
            popup.SetContentUrl("HangHoa_Barcode.aspx?IDHangHoa=" + key);
            popup.ShowAtElement();
            // alert(key);
        };
        function OnMoreInfoClick_GiaTheoSL(element, key) {
            popup.SetContentUrl("HangHoa_GiaTheoSL.aspx?IDHangHoa=" + key);
            popup.ShowAtElement();
            // alert(key);
        };
</script>
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="5">
        <Items>
            <dx:LayoutItem Caption="">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxButton ID="XuatFilePDF" runat="server" OnClick="XuatFilePDF_Click" Text="Xuất PDF">
                            <Image IconID="export_exporttopdf_16x16">
                            </Image>
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxButton ID="btnXuatExcel" runat="server" OnClick="btnXuatExcel_Click" Text="Xuất Excel">
                             <Image IconID="export_exporttoxls_16x16">
                             </Image>
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxButton ID="btnNhapExel" runat="server" OnClick="btnNhapExcel_Click" Text="Nhập Exel">
                            <Image IconID="export_exporttoxls_16x16">
                            </Image>
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxButton ID="btnTheMoi" runat="server" OnClick="btnTheMoi_Click" Text="Thêm mã hàng">
                            <Image IconID="actions_addfile_16x16">
                            </Image>
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Hiển thị">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxComboBox ID="cmbSoLuongXem" runat="server" AutoPostBack="True" SelectedIndex="0" OnSelectedIndexChanged="cmbSoLuongXem_SelectedIndexChanged">
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
    <br />
    <dx:ASPxPanel ID="ASPxPanel1" runat="server" Width="100%"><PanelCollection>
    <dx:PanelContent runat="server">
        <dx:ASPxGridViewExporter ID="HangHoaExport" runat="server" ExportedRowType="All" FileName="HangHoaExport" GridViewID="HangHoaExport1">
        </dx:ASPxGridViewExporter>
    </dx:PanelContent>
    </PanelCollection>
    </dx:ASPxPanel>
    <dx:ASPxPanel ID="ASPxPanel2" runat="server" Width="100%" DefaultButton="ASPxButton1">
        <PanelCollection>
        <dx:PanelContent ID="PanelContent1" runat="server">
            <dx:ASPxGridView ID="gridHangHoa" runat="server" AutoGenerateColumns="False" Width="100%" KeyFieldName="ID" OnRowDeleting="gridHangHoa_RowDeleting1">
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
                        <DeleteButton ButtonType="Link" RenderMode="Link">
                            <Image IconID="actions_cancel_16x16" ToolTip="Xóa">
                            </Image>
                        </DeleteButton>
                    </SettingsCommandButton>
                    <SettingsPopup>
                        <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter" />
                    </SettingsPopup>
                    <SettingsSearchPanel Visible="True" />
                    <SettingsText CommandDelete="Xóa" CommandEdit="Sửa" CommandNew="Thêm" ConfirmDelete="Bạn có chắc chắn muốn xóa không?" PopupEditFormCaption="Thông tin hàng hóa" Title="DANH SÁCH HÀNG HÓA" EmptyDataRow="Danh sách hàng hóa trống" SearchPanelEditorNullText="Nhập thông tin cần tìm..." />
                    <Columns>
                        <dx:GridViewCommandColumn ShowDeleteButton="True" ShowInCustomizationForm="True" VisibleIndex="3" Name="chucnang2">
                        </dx:GridViewCommandColumn>
                        <dx:GridViewDataTextColumn Caption="ID" FieldName="ID" ShowInCustomizationForm="True" VisibleIndex="4" Visible="False">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataButtonEditColumn Caption="Hàng hóa" VisibleIndex="0" Name="chucnang1">
                            <DataItemTemplate>
                                <a href="javascript:void(0);" onclick="OnMoreInfoClick_HangHoa(this, '<%# Container.KeyValue %>')">Xem</a>
                            </DataItemTemplate>
                            <HeaderStyle Wrap="True" />
                        </dx:GridViewDataButtonEditColumn>
                        <dx:GridViewDataButtonEditColumn Caption="Barcode" VisibleIndex="1">
                            <DataItemTemplate>
                                <a href="javascript:void(0);" onclick="OnMoreInfoClick_Barcode(this, '<%# Container.KeyValue %>')">Xem</a>
                            </DataItemTemplate>
                            <HeaderStyle Wrap="True" />
                        </dx:GridViewDataButtonEditColumn>
                        <dx:GridViewDataButtonEditColumn Caption="Giá theo SL" VisibleIndex="2">
                            <DataItemTemplate>
                                <a href="javascript:void(0);" onclick="OnMoreInfoClick_GiaTheoSL(this, '<%# Container.KeyValue %>')">Xem</a>
                            </DataItemTemplate>
                            <HeaderStyle Wrap="True" />
                        </dx:GridViewDataButtonEditColumn>
                        <dx:GridViewDataTextColumn FieldName="TenHangHoa" ShowInCustomizationForm="True" VisibleIndex="8" Caption="Tên Hàng Hóa">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="MaHang" ShowInCustomizationForm="True" VisibleIndex="7" Caption="Mã Hàng">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataComboBoxColumn Caption="Đơn Vị Tính" FieldName="IDDonViTinh" ShowInCustomizationForm="True" VisibleIndex="9">
                            <PropertiesComboBox TextField="TenDonViTinh" ValueField="ID" DataSourceID="sqlDonViTinh">
                            </PropertiesComboBox>
                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataSpinEditColumn Caption="Hệ Số" FieldName="HeSo" ShowInCustomizationForm="True" VisibleIndex="10">
                            <PropertiesSpinEdit DisplayFormatString="g">
                            </PropertiesSpinEdit>
                            <HeaderStyle Wrap="True" />
                        </dx:GridViewDataSpinEditColumn>
                        <dx:GridViewDataSpinEditColumn Caption="Giá Mua Trước Thuế" FieldName="GiaMuaTruocThue" ShowInCustomizationForm="True" VisibleIndex="14">
                            <PropertiesSpinEdit DisplayFormatString="{0:#,#} đ" DisplayFormatInEditMode="True" NumberFormat="Custom">
                            </PropertiesSpinEdit>
                        </dx:GridViewDataSpinEditColumn>
                        <dx:GridViewDataSpinEditColumn Caption="Giá Bán Trước Thuế" FieldName="GiaBanTruocThue" ShowInCustomizationForm="True" VisibleIndex="15">
                            <PropertiesSpinEdit DisplayFormatString="{0:#,#} đ" DisplayFormatInEditMode="True" NumberFormat="Custom">
                            </PropertiesSpinEdit>
                        </dx:GridViewDataSpinEditColumn>
                        <dx:GridViewDataSpinEditColumn Caption="Giá Mua Sau Thuế" FieldName="GiaMuaSauThue" ShowInCustomizationForm="True" VisibleIndex="16">
                            <PropertiesSpinEdit DisplayFormatString="{0:#,#} đ" DisplayFormatInEditMode="True" NumberFormat="Custom">
                            </PropertiesSpinEdit>
                        </dx:GridViewDataSpinEditColumn>
                        <dx:GridViewDataSpinEditColumn Caption="Giá Bán Sau Thuế" FieldName="GiaBan" ShowInCustomizationForm="True" VisibleIndex="17">
                            <PropertiesSpinEdit DisplayFormatString="{0:#,#} đ" DisplayFormatInEditMode="True" NumberFormat="Custom">
                            </PropertiesSpinEdit>
                        </dx:GridViewDataSpinEditColumn>
                        <dx:GridViewDataTextColumn FieldName="GhiChu" ShowInCustomizationForm="True" VisibleIndex="26" Caption="Ghi Chú">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataSpinEditColumn Caption="Trọng Lượng (Kg)" FieldName="TrongLuong" ShowInCustomizationForm="True" VisibleIndex="23">
                            <PropertiesSpinEdit DisplayFormatString="{0:n} KG" NumberFormat="Custom">
                            </PropertiesSpinEdit>
                            <HeaderStyle Wrap="True" />
                        </dx:GridViewDataSpinEditColumn>
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
            <asp:SqlDataSource ID="sqlDonViTinh" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenDonViTinh] FROM [GPM_DonViTinh]"></asp:SqlDataSource>
        </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxPanel>
    <%--popup chi tiet don hang--%>
    <dx:ASPxPopupControl ID="popup" runat="server" AllowDragging="True" AllowResize="True" 
         PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"  Width="1250px"
         Height="650px" FooterText="Thông tin chi tiết hàng hóa"
        HeaderText="Thông tin chi tiết" ClientInstanceName="popup" EnableHierarchyRecreation="True" CloseAction="CloseButton">
    </dx:ASPxPopupControl>
     </asp:Content>
