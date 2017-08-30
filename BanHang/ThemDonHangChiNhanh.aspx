<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="ThemDonHangChiNhanh.aspx.cs" Inherits="BanHang.ThemDonHangChiNhanh" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
         
<dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="3" Width="100%">
    <Items>
        <dx:LayoutGroup Caption="Thông tin đơn hàng" ColCount="3" ColSpan="3" RowSpan="3">
            <Items>
                <dx:LayoutItem Caption="Số Đơn Hàng">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">
                            <dx:ASPxTextBox ID="txtSoDonHang" runat="server" Width="100%" Enabled="False">
                            </dx:ASPxTextBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Người Lập">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
                            <dx:ASPxTextBox ID="txtNguoiLap" runat="server" Width="100%" Enabled="False">
                            </dx:ASPxTextBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Ngày Lập">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server">
                            <dx:ASPxDateEdit ID="txtNgayLap" runat="server" Width="100%" OnInit="txtNgayLap_Init">
                            </dx:ASPxDateEdit>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Tổng Trọng Lượng (kg)">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server">
                            <dx:ASPxSpinEdit ID="txtTongTrongLuong" runat="server" Width="100%" Enabled="False">
                            </dx:ASPxSpinEdit>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Tổng Tiền">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
                            <dx:ASPxSpinEdit ID="txtTongTien" runat="server" AutoPostBack="True" Width="100%" DisplayFormatString="N0" Enabled="False">
                            </dx:ASPxSpinEdit>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Kho Lập">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
                            <dx:ASPxComboBox ID="cmbKhoLap" runat="server" Enabled="False" Width="100%" DataSourceID="SqlKho" TextField="TenCuaHang" ValueField="ID">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlKho" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenCuaHang] FROM [GPM_Kho] WHERE ([ID] = @ID)">
                                <SelectParameters>
                                    <asp:SessionParameter Name="ID" SessionField="IDKho" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Ghi Chú" ColSpan="3">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server">
                            <dx:ASPxTextBox ID="txtGhiChu" runat="server" Width="100%">
                            </dx:ASPxTextBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
            </Items>
        </dx:LayoutGroup>
        <dx:LayoutGroup Caption="Hàng Hóa" ColCount="3" ColSpan="3" RowSpan="3">
            <Items>
                <dx:LayoutItem Caption="Hàng Hóa">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server">
                            <dx:ASPxComboBox ID="cmbHangHoa" runat="server" 
                                AutoPostBack="True" OnSelectedIndexChanged="cmbHangHoa_SelectedIndexChanged" 
                                DropDownWidth="750px" DropDownStyle="DropDownList"   TextFormatString="{0} - {1}"
                                EnableCallbackMode="true" Width="100%" 
                                OnItemsRequestedByFilterCondition="cmbHangHoa_ItemsRequestedByFilterCondition"
                                OnItemRequestedByValue="cmbHangHoa_ItemRequestedByValue"
                                ValueField="ID"
                                NullText="Vui lòng chọn hàng hóa.........."
                                >
                                <Columns>
                                    <dx:ListBoxColumn Caption="Mã Hàng" FieldName="MaHang" Width="70px" />
                                    <dx:ListBoxColumn Caption="Tên Hàng Hóa" FieldName="TenHangHoa" Width="100%" />
                                    <dx:ListBoxColumn Caption="Giá Bán" FieldName="GiaBanSauThue" Width="100px" />   
                                    <dx:ListBoxColumn Caption="ĐVT" FieldName="TenDonViTinh" Width="100px" />          
                                </Columns>
                                 <DropDownButton Visible="False">
                                        </DropDownButton>
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="dsHangHoa" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" ></asp:SqlDataSource>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Tồn Kho">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server">
                            <dx:ASPxComboBox ID="txtTonKho" runat="server" Enabled="False" Width="100%">
                            </dx:ASPxComboBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Số Lượng">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer12" runat="server">
                            <dx:ASPxSpinEdit ID="txtSoLuong" runat="server" Width="100%">
                            </dx:ASPxSpinEdit>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Trọng Lượng">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer13" runat="server">
                            <dx:ASPxTextBox ID="txtTrongLuong" runat="server" Enabled="False" Width ="100%">
                            </dx:ASPxTextBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server">
                            <dx:ASPxUploadControl ID="UploadFileExcel" runat="server" AllowedFileExtensions=".xls" Width="100%">
                            </dx:ASPxUploadControl>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="" HorizontalAlign="Left">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer14" runat="server">
                            <dx:ASPxButton ID="btnThem_Temp" runat="server" Text="Thêm" OnClick="btnThem_Temp_Click">
                                <Image IconID="actions_add_32x32">
                                </Image>
                            </dx:ASPxButton>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
            </Items>
        </dx:LayoutGroup>
        <dx:LayoutGroup ColSpan="3" Caption="Danh sách hàng hóa" ColCount="3">
            <Items>
                <dx:LayoutItem Caption="" ColSpan="3">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer15" runat="server">
                                                
                            <dx:ASPxGridView ID="gridDanhSachHangHoa" runat="server" AutoGenerateColumns="False" KeyFieldName="ID" Width="100%">
                                 <SettingsBehavior ConfirmDelete="True" />
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
                                <SettingsText CommandBatchEditCancel="Hủy tất cả" CommandBatchEditUpdate="Lưu tất cả" Title="DANH SÁCH HÀNG HÓA GIÁ THEO CHI NHÁNH" ConfirmDelete="Bạn chắc chắn muốn xóa?" />
                                <Columns>                                    
                                    <dx:GridViewDataTextColumn Caption="Mã Hàng" FieldName="MaHang" ShowInCustomizationForm="True" VisibleIndex="0">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataSpinEditColumn Caption="Thành Tiền" FieldName="ThanhTien" ShowInCustomizationForm="True" VisibleIndex="6">
                                        <PropertiesSpinEdit DisplayFormatString="N0" NumberFormat="Custom">
                                        </PropertiesSpinEdit>
                                    </dx:GridViewDataSpinEditColumn>
<dx:GridViewDataSpinEditColumn FieldName="DonGia" ShowInCustomizationForm="True" Caption="Đơn Giá" VisibleIndex="5">
<PropertiesSpinEdit DisplayFormatString="N0" NumberFormat="Custom"></PropertiesSpinEdit>
</dx:GridViewDataSpinEditColumn>
                                    <dx:GridViewDataSpinEditColumn Caption="Số Lượng" FieldName="SoLuong" ShowInCustomizationForm="True" VisibleIndex="4">
<PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                                    </dx:GridViewDataSpinEditColumn>
                                    <dx:GridViewDataComboBoxColumn Caption="Tên Hàng Hóa" FieldName="IDHangHoa" ShowInCustomizationForm="True" VisibleIndex="1">
                                        <PropertiesComboBox DataSourceID="SqlDanhSachHangHoa" TextField="TenHangHoa" ValueField="ID">
                                        </PropertiesComboBox>
                                    </dx:GridViewDataComboBoxColumn>
                                    <dx:GridViewDataComboBoxColumn Caption="ĐVT" FieldName="IDDonViTinh" ShowInCustomizationForm="True" VisibleIndex="2">
                                        <PropertiesComboBox DataSourceID="SqlDanhSachDonViTinh" TextField="TenDonViTinh" ValueField="ID">
                                        </PropertiesComboBox>
                                    </dx:GridViewDataComboBoxColumn>
                                    <dx:GridViewDataSpinEditColumn Caption="Trọng Lượng" FieldName="TrongLuong" ShowInCustomizationForm="True" VisibleIndex="3">
                                        <PropertiesSpinEdit DisplayFormatString="g">
                                        </PropertiesSpinEdit>
                                    </dx:GridViewDataSpinEditColumn>
                                    <dx:GridViewDataButtonEditColumn Caption="Xóa" ShowInCustomizationForm="True" Width="50px" 
                                        VisibleIndex="7">
                                        <DataItemTemplate>
                                            <dx:ASPxButton ID="BtnXoaHang" runat="server" CommandName="XoaHang"
                                                CommandArgument='<%# Eval("ID") %>' 
                                                onclick="BtnXoaHang_Click" RenderMode="Link">
                                                <Image IconID="actions_cancel_32x32">
                                                </Image>
                                            </dx:ASPxButton>
                                        </DataItemTemplate>
                                        <CellStyle HorizontalAlign="Center">
                                        </CellStyle>
                                    </dx:GridViewDataButtonEditColumn>
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
                                                
                            <asp:SqlDataSource ID="SqlDanhSachDonViTinh" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenDonViTinh] FROM [GPM_DonViTinh] WHERE ([DaXoa] = @DaXoa)">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDanhSachHangHoa" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenHangHoa] FROM [GPM_HangHoa] WHERE (([DaXoa] = @DaXoa) AND ([IDTrangThaiHang] = 1 OR [IDTrangThaiHang] = 3 OR [IDTrangThaiHang] = 6))">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                    
                                </SelectParameters>
                            </asp:SqlDataSource>
                                                
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="" HorizontalAlign="Right" ColSpan="2">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer16" runat="server">
                            <dx:ASPxButton ID="btnThem" runat="server" Text="Lưu" OnClick="btnThem_Click">
                                <Image IconID="save_saveto_32x32">
                                </Image>
                            </dx:ASPxButton>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer17" runat="server">
                            <dx:ASPxButton ID="btnHuy" runat="server" Text="Hủy" OnClick="btnHuy_Click">
                                <Image IconID="save_saveandclose_32x32">
                                </Image>
                            </dx:ASPxButton>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
            </Items>
        </dx:LayoutGroup>
    </Items>
</dx:ASPxFormLayout>
             
    <asp:HiddenField ID="IDDonDatHang_Temp" runat="server" />
</asp:Content>
