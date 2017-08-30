<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsHangHoa.aspx.cs" Inherits="BanHang.NewsHangHoa" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="2" Width="100%">
            <Items>
                 <dx:TabbedLayoutGroup ColSpan="2" Width="100%">
                      <Items>
                           <dx:LayoutGroup Caption="Thông tin hàng hóa" ColCount="3" Width="100%">
                      <Items>
                           <dx:LayoutItem Caption="Nhóm hàng (*)">
                                <LayoutItemNestedControlCollection>
                                      <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
                                                                <dx:ASPxComboBox ID="IDNhomHang" runat="server" DataSourceID="sqlNhomHang" TextField="TenNhomHang" ValueField="ID" Width="100%">
                                                                </dx:ASPxComboBox>
                                                                <asp:SqlDataSource ID="sqlNhomHang" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [MaNhom], [TenNhomHang] FROM [GPM_NhomHang] WHERE ([DaXoa] = @DaXoa)">
                                                                    <SelectParameters>
                                                                        <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                                                    </SelectParameters>
                                                                </asp:SqlDataSource>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Mã hàng (*)">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
                                                                <dx:ASPxTextBox ID="MaHang" runat="server" Width="100%">
                                                                </dx:ASPxTextBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Tên hàng (*)">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
                                                                <dx:ASPxTextBox ID="TenHangHoa" runat="server" Width="100%">
                                                                </dx:ASPxTextBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Đơn vị (*)">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">
                                                                <dx:ASPxComboBox ID="IDDonViTinh" runat="server" DataSourceID="sqlDonVitinh" TextField="TenDonViTinh" ValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="IDDonViTinh_SelectedIndexChanged1" Width="100%">
                                                                </dx:ASPxComboBox>
                                                                <asp:SqlDataSource ID="sqlDonVitinh" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenDonViTinh] FROM [GPM_DonViTinh] WHERE ([DaXoa] = @DaXoa)">
                                                                    <SelectParameters>
                                                                        <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                                                    </SelectParameters>
                                                                </asp:SqlDataSource>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Hãng SX (*)">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server">
                                                                <dx:ASPxComboBox ID="IDHangSanXuat" runat="server" DataSourceID="sqlHangSanXuat" TextField="TenHangSanXuat" ValueField="ID" Width="100%">
                                                                </dx:ASPxComboBox>
                                                                <asp:SqlDataSource ID="sqlHangSanXuat" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenHangSanXuat] FROM [GPM_HangSanXuat] WHERE ([DaXoa] = @DaXoa)">
                                                                    <SelectParameters>
                                                                        <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                                                    </SelectParameters>
                                                                </asp:SqlDataSource>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Hàng quy đổi (*)">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server">
                                                                <dx:ASPxComboBox ID="IDHangQuyDoi" runat="server" DataSourceID="sqlHangHoaQuyDoi" TextField="TenHangHoa" ValueField="ID" Width="100%">
                                                                </dx:ASPxComboBox>
                                                                <asp:SqlDataSource ID="sqlHangHoaQuyDoi" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenHangHoa] FROM [GPM_HangHoa] WHERE (([IDTrangThaiHang] &lt; @IDTrangThaiHang) AND ([DaXoa] = @DaXoa))">
                                                                    <SelectParameters>
                                                                        <asp:Parameter DefaultValue="5" Name="IDTrangThaiHang" Type="Int32" />
                                                                        <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                                                    </SelectParameters>
                                                                </asp:SqlDataSource>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Nhóm đặt hàng (*)">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server">
                                                                <dx:ASPxComboBox ID="IDNhomDatHang" runat="server" DataSourceID="sqlNhomDatHang" TextField="TenNhom" ValueField="ID" Width="100%">
                                                                </dx:ASPxComboBox>
                                                                <asp:SqlDataSource ID="sqlNhomDatHang" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenNhom] FROM [GPM_NhomDatHang]">
                                                                </asp:SqlDataSource>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Giá mua trước thuế (*)">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server">
                                                                <dx:ASPxSpinEdit ID="GiaMuaTruocThue" runat="server" NullText="0" Number="0" OnNumberChanged="GiaMuaTruocThue_NumberChanged" AutoPostBack="True" Width="100%" DisplayFormatString="{0:#,# VND}">
                                                                </dx:ASPxSpinEdit>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Giá bán trước thuế (*)">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server">
                                                                <dx:ASPxSpinEdit ID="GiaBanTruocThue" runat="server" NullText="0" Number="0" OnNumberChanged="GiaBanTruocThue_NumberChanged" AutoPostBack="True" Width="100%" DisplayFormatString="{0:#,# VND}">
                                                                </dx:ASPxSpinEdit>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Thuế (*)">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server">
                                                                <dx:ASPxComboBox ID="IDThue" runat="server" DataSourceID="sqlThue" TextField="TenThue" ValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="IDThue_SelectedIndexChanged" Width="100%">
                                                                </dx:ASPxComboBox>
                                                                <asp:SqlDataSource ID="sqlThue" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenThue] FROM [GPM_Thue]">
                                                                </asp:SqlDataSource>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Giá mua sau thuế (*)">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server">
                                                                <dx:ASPxSpinEdit ID="GiaMuaSauThue" runat="server" NullText="0" Number="0" Width="100%" DisplayFormatString="{0:#,# VND}">
                                                                </dx:ASPxSpinEdit>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Giá bán sau thuế (*)">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer12" runat="server">
                                                                <dx:ASPxSpinEdit ID="GiaBanSauThue" runat="server" NullText="0" Number="0" Width="100%" DisplayFormatString="{0:#,# VND}">
                                                                </dx:ASPxSpinEdit>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Hệ số">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer runat="server">
                                                                <dx:ASPxSpinEdit ID="txtHeSo" runat="server" Width="100%">
                                                                </dx:ASPxSpinEdit>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                           </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Trọng lượng (Kg) (*)">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer13" runat="server">
                                                                <dx:ASPxSpinEdit ID="TrongLuong" runat="server" Width="100%">
                                                                </dx:ASPxSpinEdit>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Hạn sử dụng (Tháng) (*)">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer14" runat="server">
                                                                <dx:ASPxSpinEdit ID="HanSuDung" runat="server" Width="100%">
                                                                </dx:ASPxSpinEdit>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Trạng thái hàng (*)">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer15" runat="server">
                                                                <dx:ASPxComboBox ID="IDTrangThaiHang" runat="server" DataSourceID="sqlTrangThaiHang" TextField="TenTrangThai" ValueField="ID" Width="100%">
                                                                </dx:ASPxComboBox>
                                                                <asp:SqlDataSource ID="sqlTrangThaiHang" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenTrangThai] FROM [GPM_TrangThaiHang] WHERE ([ID] &lt; @ID)">
                                                                    <SelectParameters>
                                                                        <asp:Parameter DefaultValue="5" Name="ID" Type="Int32" />
                                                                    </SelectParameters>
                                                                </asp:SqlDataSource>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Ghi chú">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer16" runat="server">
                                                                <dx:ASPxTextBox ID="GhiChu" runat="server" Width="100%">
                                                                </dx:ASPxTextBox>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                </Items>
                                            </dx:LayoutGroup>
                                            <dx:LayoutGroup Caption="Thông tin thêm" ColCount="3" Width="100%">
                                                <Items>
                                                    <dx:LayoutItem Caption="Giá bán 1">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer17" runat="server">
                                                                <dx:ASPxSpinEdit ID="GiaBan1" runat="server" NullText="0" Number="0" DisplayFormatString="{0:#,# VND}">
                                                                </dx:ASPxSpinEdit>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Giá bán 2">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer18" runat="server">
                                                                <dx:ASPxSpinEdit ID="GiaBan2" runat="server" NullText="0" Number="0" DisplayFormatString="{0:#,# VND}">
                                                                </dx:ASPxSpinEdit>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Giá bán 3">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer19" runat="server">
                                                                <dx:ASPxSpinEdit ID="GiaBan3" runat="server" NullText="0" Number="0" DisplayFormatString="{0:#,# VND}">
                                                                </dx:ASPxSpinEdit>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Giá bán 4">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer20" runat="server">
                                                                <dx:ASPxSpinEdit ID="GiaBan4" runat="server" NullText="0" Number="0" DisplayFormatString="{0:#,# VND}">
                                                                </dx:ASPxSpinEdit>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="Giá bán 5">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer21" runat="server">
                                                                <dx:ASPxSpinEdit ID="GiaBan5" runat="server" NullText="0" Number="0" DisplayFormatString="{0:#,# VND}">
                                                                </dx:ASPxSpinEdit>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer22" runat="server">
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="" ColSpan="3">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer23" runat="server">
                                                                <dx:ASPxGridView ID="aspGridBarcode" runat="server" AutoGenerateColumns="False" Width="100%" KeyFieldName="ID" OnRowDeleting="aspGridBarcode_RowDeleting" OnRowInserting="aspGridBarcode_RowInserting" OnRowUpdating="aspGridBarcode_RowUpdating">
                                                                    <SettingsEditing Mode="PopupEditForm">
                                                                    </SettingsEditing>
                                                                    <SettingsCommandButton>
                                                                        <ShowAdaptiveDetailButton ButtonType="Image">
                                                                        </ShowAdaptiveDetailButton>
                                                                        <HideAdaptiveDetailButton ButtonType="Image">
                                                                        </HideAdaptiveDetailButton>
                                                                        
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
                                                                        <EditForm HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                                        <CustomizationWindow HorizontalAlign="WindowCenter" VerticalAlign="WindowCenter" />
                                                                    </SettingsPopup>
                                                                    <EditFormLayoutProperties ColCount="2">
                                                                        <Items>
                                                                            <dx:GridViewColumnLayoutItem ColumnName="Trạng thái Barcode">
                                                                            </dx:GridViewColumnLayoutItem>
                                                                            <dx:GridViewColumnLayoutItem ColumnName="Barcode">
                                                                            </dx:GridViewColumnLayoutItem>
                                                                            <dx:EditModeCommandLayoutItem ColSpan="2" HorizontalAlign="Right">
                                                                            </dx:EditModeCommandLayoutItem>
                                                                        </Items>
                                                                    </EditFormLayoutProperties>
                                                                    <Columns>
                                                                        <dx:GridViewCommandColumn ShowDeleteButton="True" ShowEditButton="True" ShowInCustomizationForm="True" ShowNewButtonInHeader="True" VisibleIndex="3">
                                                                        </dx:GridViewCommandColumn>
                                                                        <dx:GridViewDataTextColumn Caption="Barcode" ShowInCustomizationForm="True" VisibleIndex="1" FieldName="Barcode">
                                                                        </dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataComboBoxColumn Caption="Trạng thái Barcode" ShowInCustomizationForm="True" VisibleIndex="0" FieldName="IDTrangThaiBarcode">
                                                                            <PropertiesComboBox DataSourceID="sqlTrangThaiBarcode" TextField="TenTrangThai" ValueField="ID">
                                                                            </PropertiesComboBox>
                                                                        </dx:GridViewDataComboBoxColumn>
                                                                        <dx:GridViewDataDateColumn Caption="Ngày cập nhật" ShowInCustomizationForm="True" VisibleIndex="2" FieldName="NgayCapNhat">
                                                                            <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy">
                                                                            </PropertiesDateEdit>
                                                                        </dx:GridViewDataDateColumn>
                                                                    </Columns>
                                                                </dx:ASPxGridView>
                                                                <asp:SqlDataSource ID="sqlTrangThaiBarcode" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenTrangThai] FROM [GPM_TrangThai_Barcode]"></asp:SqlDataSource>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                </Items>
                                            </dx:LayoutGroup>
                                        </Items>
                                    </dx:TabbedLayoutGroup>
                                    <dx:LayoutItem Caption="" HorizontalAlign="Right">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer24" runat="server">
                                                <dx:ASPxButton ID="Luu" runat="server" Text="Lưu" OnClick="Luu_Click" Width="120px">
                                                    <Image IconID="actions_additem_16x16">
                                                    </Image>
                                                </dx:ASPxButton>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>
                                <dx:LayoutItem Caption="" HorizontalAlign="Left">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server">
                                            <dx:ASPxButton ID="btnHuy" runat="server" OnClick="btnHuy_Click" Text="Hủy" Width="120px">
                                                <Image IconID="actions_cancel_16x16">
                                                </Image>
                                            </dx:ASPxButton>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                 </dx:LayoutItem>
                                </Items>
        </dx:ASPxFormLayout>
    
    </div>
    </form>
</body>
</html>
