﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="HangHoa_Page.aspx.cs" Inherits="BanHang.HangHoa_Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <br/>
    <dx:ASPxLabel ID="IDHangHoa" runat="server" Text="IDHangHoa" Visible="False">
    </dx:ASPxLabel>
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%">
        <Items>
            <dx:LayoutGroup Caption="Thông tin hàng hóa" ColCount="3">
                    <Items>
                        <dx:LayoutItem Caption="Ngành hàng">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
                                    <dx:ASPxComboBox ID="cmbNganhHang" runat="server" DataSourceID="sqlNganhHang" TextField="TenNganhHang" ValueField="ID" Width="100%" Enabled="False">
                                    </dx:ASPxComboBox>
                                    <asp:SqlDataSource ID="sqlNganhHang" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenNganhHang], [MaNganh] FROM [GPM_NganhHang] WHERE ([DaXoa] = @DaXoa)">
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Nhóm hàng (*)">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">
                                    <dx:ASPxComboBox TextFormatString="{1}" ID="cmbNhomHang" runat="server" Width="100%" DataSourceID="sqlNhomHang" AutoPostBack="True"  ValueType="System.String"  DropDownWidth="400" DropDownStyle="DropDown" ValueField="ID" OnSelectedIndexChanged="cmbNhomHang_SelectedIndexChanged" AllowMouseWheel="False">
                                        <Columns>
                                            <dx:ListBoxColumn Caption="Mã Nhóm" FieldName="MaNhom" Width="100px" />
                                            <dx:ListBoxColumn Caption="Tên nhóm hàng" FieldName="TenNhomHang" Width="100%" />
                                        </Columns>
                                    </dx:ASPxComboBox>
                                    <asp:SqlDataSource ID="sqlNhomHang" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenNhomHang], [MaNhom] FROM [GPM_NhomHang] WHERE ([DaXoa] = @DaXoa)">
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Mã hàng (*)">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server">
                                    <dx:ASPxTextBox ID="txtMaHang" runat="server" Width="100%">
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Tên hàng (*)">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server">
                                    <dx:ASPxTextBox ID="txtTenHang" runat="server" Width="100%">
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Đơn vị tính (*)">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server">
                                    <dx:ASPxComboBox TextFormatString="{1}" ID="cmbDonViTinh" runat="server" Width="100%" DataSourceID="sqlDonViTinh"   ValueType="System.String"  DropDownWidth="400" DropDownStyle="DropDown" ValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="cmbDonViTinh_SelectedIndexChanged" AllowMouseWheel="False">
                                        <Columns>
                                            <%--<dx:ListBoxColumn Caption="ID" FieldName="ID" Width="100px" />--%>
                                            <dx:ListBoxColumn Caption="Mã ĐVT" FieldName="MaDonVi" Width="100%" />
                                            <dx:ListBoxColumn Caption="Tên đơn vị tính" FieldName="TenDonViTinh" Width="100px" />
                                            <dx:ListBoxColumn Caption="Mô tả" FieldName="MoTa" Width="100%" />
                                        </Columns>
                                    </dx:ASPxComboBox>
                                    <asp:SqlDataSource ID="sqlDonViTinh" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenDonViTinh], [MaDonVi], [MoTa] FROM [GPM_DonViTinh] WHERE ([DaXoa] = @DaXoa)">
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Hệ số (*)">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server">
                                    <dx:ASPxSpinEdit ID="txtHeSo" runat="server" Width="100%" AllowMouseWheel="False" Number="1">
                                    </dx:ASPxSpinEdit>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Hãng SX (*)">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server">
                                    <dx:ASPxComboBox TextFormatString="{0}" ID="cmbHangSX" runat="server" Width="100%" DataSourceID="sqlHangSX"   ValueType="System.String"  DropDownWidth="400" DropDownStyle="DropDown" ValueField="ID" AllowMouseWheel="False">
                                        <Columns>
                                           <%-- <dx:ListBoxColumn Caption="ID" FieldName="ID" Width="100px" />--%>
                                            <dx:ListBoxColumn Caption="Tên hãng SX" FieldName="TenNSX" Width="100%" />
                                            <dx:ListBoxColumn Caption="Điện thoại" FieldName="DienThoai" Width="100px" />
                                            <dx:ListBoxColumn Caption="Mã số thuế" FieldName="MaSoThue" Width="100px" />
                                        </Columns>
                                    </dx:ASPxComboBox>
                                    <asp:SqlDataSource ID="sqlHangSX" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenNSX], [DienThoai], [MaSoThue] FROM [GPM_HangSanXuat] WHERE ([DaXoa] = @DaXoa)">
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Nhóm đặt hàng">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server">
                                     <dx:ASPxComboBox ID="cmbNhomDatHang" runat="server" Width="100%" DataSourceID="sqlNhomDatHang"   ValueType="System.String"  DropDownWidth="400" DropDownStyle="DropDown" ValueField="ID" AllowMouseWheel="False">
                                        <Columns>
                                            <dx:ListBoxColumn Caption="Người đặt hàng" FieldName="TenNhom" Width="100%" />
                                        </Columns>
                                    </dx:ASPxComboBox>
                                     <asp:SqlDataSource ID="sqlNhomDatHang" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT * FROM [GPM_NhomDatHang]"></asp:SqlDataSource>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Hàng quy đổi">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server">
                                     <dx:ASPxComboBox TextFormatString="{1}" ID="cmbHangQuyDoi" runat="server" Width="100%" ValueType="System.String"  DropDownWidth="400" DropDownStyle="DropDown" ValueField="ID" AllowMouseWheel="False">
                                        <Columns>
                                            <dx:ListBoxColumn Caption="Mã hàng" FieldName="MaHang" Width="100px" />
                                            <dx:ListBoxColumn Caption="Tên hàng" FieldName="TenHangHoa" Width="100%" />
                                        </Columns>
                                    </dx:ASPxComboBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Thuế (*)">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer12" runat="server">
                                    <dx:ASPxComboBox TextFormatString="{0}"  ID="cmbThue" runat="server" Width="100%" DataSourceID="sqlThue"   ValueType="System.String"  DropDownWidth="400" DropDownStyle="DropDown" ValueField="ID"  AutoPostBack="True" OnSelectedIndexChanged="cmbThue_SelectedIndexChanged" AllowMouseWheel="False">
                                        <Columns>
                                            <dx:ListBoxColumn Caption="Tên thuế" FieldName="TenThue" Width="100px" />
                                            <dx:ListBoxColumn Caption="Tỉ lệ" FieldName="TiLe" Width="100%" />
                                            <dx:ListBoxColumn Caption="Ghi chú" FieldName="GhiChu" Width="100%" />
                                        </Columns>
                                    </dx:ASPxComboBox>
                                    <asp:SqlDataSource ID="sqlThue" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenThue], [TiLe], [GhiChu] FROM [GPM_Thue] WHERE ([DaXoa] = @DaXoa)">
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Giá mua trước thuế">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer13" runat="server">
                                    <dx:ASPxSpinEdit ID="txtGiaMuaTruocThue" runat="server" Increment="5000" NullText="0" Width="100%" AutoPostBack="True" OnValueChanged="txtGiaMuaTruocThue_ValueChanged" DisplayFormatString="N0" AllowMouseWheel="False">
                                    </dx:ASPxSpinEdit>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Giá mua sau thuế">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer14" runat="server">
                                    <dx:ASPxSpinEdit ID="txtGiaMuaSauThue" runat="server" Increment="5000" NullText="0" Width="100%" AutoPostBack="True" OnValueChanged="txtGiaMuaSauThue_ValueChanged" DisplayFormatString="N0" AllowMouseWheel="False">
                                    </dx:ASPxSpinEdit>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Giá bán trước thuế">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer15" runat="server">
                                    <dx:ASPxSpinEdit ID="txtGiaBanTruocThue" runat="server" Increment="5000" NullText="0" Width="100%" AutoPostBack="True" OnValueChanged="txtGiaBanTruocThue_ValueChanged" DisplayFormatString="N0" AllowMouseWheel="False">
                                    </dx:ASPxSpinEdit>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Giá bán sau thuế">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer16" runat="server">
                                    <dx:ASPxSpinEdit ID="txtGiaBanSauThue" runat="server" Increment="5000" NullText="0" Width="100%" AutoPostBack="True" OnValueChanged="txtGiaBanSauThue_ValueChanged" DisplayFormatString="N0" AllowMouseWheel="False">
                                    </dx:ASPxSpinEdit>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Trọng lượng (Kg)">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer17" runat="server">
                                    <dx:ASPxSpinEdit ID="txtTrongLuong" runat="server" NullText="0" Width="100%" DisplayFormatString="{0} KG" AllowMouseWheel="False">
                                    </dx:ASPxSpinEdit>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Hạn sử dụng">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer18" runat="server">
                                    <dx:ASPxTextBox ID="txtHangSuDung" runat="server" Width="100%">
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Trạng thái hàng">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer19" runat="server">
                                    <dx:ASPxComboBox ID="cmbTrangThaiHang" runat="server" Width="100%" DataSourceID="sqlTrangThaiHang"   ValueType="System.String"  DropDownWidth="400" DropDownStyle="DropDown" ValueField="ID" AllowMouseWheel="False">
                                        <Columns>
                                            <dx:ListBoxColumn Caption="Trạng thái" FieldName="TenTrangThai" Width="100%" />
                                        </Columns>
                                    </dx:ASPxComboBox>
                                    <asp:SqlDataSource ID="sqlTrangThaiHang" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT * FROM [GPM_TrangThaiHang] WHERE (([ID] &lt;&gt; @ID) AND ([ID] &lt;&gt; @ID2) AND ([ID] &lt;&gt; @ID3))">
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="5" Name="ID" Type="Int32" />
                                            <asp:Parameter DefaultValue="6" Name="ID2" />
                                            <asp:Parameter DefaultValue="7" Name="ID3" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                        <dx:LayoutItem Caption="Ghi chú">
                            <LayoutItemNestedControlCollection>
                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer20" runat="server">
                                    <dx:ASPxTextBox ID="txtGhiChu" runat="server" Width="100%">
                                    </dx:ASPxTextBox>
                                </dx:LayoutItemNestedControlContainer>
                            </LayoutItemNestedControlCollection>
                        </dx:LayoutItem>
                    </Items>
                </dx:LayoutGroup>
            <dx:LayoutGroup Caption="Barcode hàng hóa">
                <Items>
                    <dx:LayoutItem Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxGridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gridHangHoaBarcode" KeyFieldName="ID" OnRowDeleting="gridHangHoaBarcode_RowDeleting" OnRowInserting="gridHangHoaBarcode_RowInserting" OnRowUpdating="gridHangHoaBarcode_RowUpdating">
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

                                    <SettingsText CommandDelete="Xóa" ConfirmDelete="Bạn chắc chắn muốn xóa?" CommandEdit="Sửa" CommandNew="Thêm" EmptyDataRow="Danh sách barcode trống" PopupEditFormCaption="Thông tin barcode"></SettingsText>
                                    <EditFormLayoutProperties>
                                        <Items>
                                            <dx:GridViewColumnLayoutItem ColumnName="Trạng thái barcode" Name="TenTrangThai">
                                            </dx:GridViewColumnLayoutItem>
                                            <dx:GridViewColumnLayoutItem ColumnName="Barcode" Name="Barcode">
                                            </dx:GridViewColumnLayoutItem>
                                            <dx:EditModeCommandLayoutItem HorizontalAlign="Right">
                                            </dx:EditModeCommandLayoutItem>
                                        </Items>
                                    </EditFormLayoutProperties>
                                    <Columns>

                                        <dx:GridViewCommandColumn Name="chucnang" ShowEditButton="True" VisibleIndex="7" ShowDeleteButton="True" ShowNewButtonInHeader="True">
                                        </dx:GridViewCommandColumn>

                                        <dx:GridViewDataTextColumn Caption="Barcode" FieldName="Barcode" VisibleIndex="1">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataComboBoxColumn Caption="Trạng thái barcode" FieldName="IDTrangThaiBarcode" VisibleIndex="0">
                                            <PropertiesComboBox DataSourceID="sqlTrangThaiBarcode" TextField="TenTrangThai" ValueField="ID">
                                            </PropertiesComboBox>
                                        </dx:GridViewDataComboBoxColumn>
                                        <dx:GridViewDataDateColumn Caption="Ngày cập nhật" FieldName="NgayCapNhat" ShowInCustomizationForm="True" VisibleIndex="6">
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
                                <asp:SqlDataSource ID="sqlTrangThaiBarcode" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT * FROM [GPM_TrangThai_Barcode]"></asp:SqlDataSource>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
            
            <dx:LayoutGroup Caption="Giá bán theo số lượng">
                <Items>
                    <dx:LayoutItem Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
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

                                    <SettingsText CommandDelete="Xóa" ConfirmDelete="Bạn chắc chắn muốn xóa?" CommandEdit="Sửa" CommandNew="Thêm" EmptyDataRow="Danh sách hàng hóa trống" PopupEditFormCaption="Thông tin"></SettingsText>
                                    <EditFormLayoutProperties>
                                        <Items>
                                            <dx:GridViewColumnLayoutItem ColumnName="Số lượng bắt đầu" Name="SL1">
                                            </dx:GridViewColumnLayoutItem>
                                            <dx:GridViewColumnLayoutItem ColumnName="Số lượng kết thúc" Name="SL2">
                                            </dx:GridViewColumnLayoutItem>
                                            <dx:GridViewColumnLayoutItem ColumnName="Giá bán" Name="GiaBan">
                                            </dx:GridViewColumnLayoutItem>
                                            <dx:EditModeCommandLayoutItem HorizontalAlign="Right">
                                            </dx:EditModeCommandLayoutItem>
                                        </Items>
                                    </EditFormLayoutProperties>
                                    <Columns>

                                        <dx:GridViewCommandColumn Name="chucnang" ShowEditButton="True" VisibleIndex="7" ShowDeleteButton="True" ShowNewButtonInHeader="True">
                                        </dx:GridViewCommandColumn>

                                        <dx:GridViewDataSpinEditColumn Caption="Số lượng bắt đầu" FieldName="SoLuongBD" VisibleIndex="0">
                                            <PropertiesSpinEdit DisplayFormatString="g">
                                            </PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn Caption="Số lượng kết thúc" FieldName="SoLuongKT" ShowInCustomizationForm="True" VisibleIndex="1">
                                            <PropertiesSpinEdit DisplayFormatString="g">
                                            </PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn Caption="Giá bán" FieldName="GiaBan" ShowInCustomizationForm="True" VisibleIndex="3">
                                            <PropertiesSpinEdit DisplayFormatString="N0" NumberFormat="Custom" DisplayFormatInEditMode="True">
                                            </PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataDateColumn Caption="Ngày cập nhật" FieldName="NgayCapNhat" ShowInCustomizationForm="True" VisibleIndex="6">
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
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
            <dx:LayoutGroup Caption="" ColCount="2">
                <Items>
                    <dx:LayoutItem Caption="" HorizontalAlign="Right">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxButton ID="btnLuuHangHoa" runat="server" HorizontalAlign="Right" Text="Lưu Hàng Hóa" OnClick="btnLuuHangHoa_Click">
                                    <Image IconID="save_saveto_16x16">
                                    </Image>
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" HorizontalAlign="Left">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxButton ID="btnHuy" runat="server" HorizontalAlign="Left" Text="Hủy Hàng Hóa" OnClick="btnHuy_Click">
                                    <Image IconID="actions_cancel_16x16">
                                    </Image>
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
        </Items>
    </dx:ASPxFormLayout>
</asp:Content>
