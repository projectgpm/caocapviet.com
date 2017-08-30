<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="PhieuChuyenKho.aspx.cs" Inherits="BanHang.PhieuChuyenKho" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="3" Width="100%">
        <Items>
            <dx:LayoutGroup Caption="Thông tin phiếu chuyển kho" ColCount="4" ColSpan="3" RowSpan="3">
                <Items>
                    <dx:LayoutItem Caption="Người Lập Phiếu">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
                                <dx:ASPxComboBox ID="cmbNguoiLapPhieu" runat="server" DataSourceID="sqlNguoiLap" TextField="TenDangNhap" ValueField="ID" Enabled="False">
                                </dx:ASPxComboBox>
                                <asp:SqlDataSource ID="sqlNguoiLap" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenDangNhap] FROM [GPM_NguoiDung] WHERE ([DaXoa] = @DaXoa)">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Ngày Lập Phiếu">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
                                <dx:ASPxDateEdit ID="cmbNgayLapPhieu" runat="server" DateOnError="Today" DisplayFormatString="dd/MM/yyyy" OnInit="cmbNgayLapPhieu_Init">
                                </dx:ASPxDateEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Kho Xuất">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
                                <dx:ASPxComboBox ID="cmbKhoXuat" runat="server" DataSourceID="sqlKho" TextField="TenCuaHang" ValueField="ID">
                                </dx:ASPxComboBox>
                                <asp:SqlDataSource ID="sqlKho" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenCuaHang] FROM [GPM_Kho]"></asp:SqlDataSource>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Kho Nhập">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">
                                <dx:ASPxComboBox ID="cmbKhoNhap" runat="server" DataSourceID="sqlKho" TextField="TenCuaHang" ValueField="ID">
                                </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Trạng thái phiếu">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server">
                                <dx:ASPxComboBox ID="cmbTrangThaiPhieu" runat="server" DataSourceID="sqlTrangThaiHang" TextField="TenTrangThai" ValueField="ID" ReadOnly="True">
                                </dx:ASPxComboBox>
                                <asp:SqlDataSource ID="sqlTrangThaiHang" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenTrangThai] FROM [GPM_TrangThaiChuyenHang]"></asp:SqlDataSource>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Số mặt hàng">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxSpinEdit ID="txtSoMatHang" runat="server" NullText="0" ReadOnly="True">
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Trọng lượng">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxSpinEdit ID="txtTrongLuong" runat="server" NullText="0" ReadOnly="True" DisplayFormatString="{0:n} KG">
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Tổng tiền">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxSpinEdit ID="txtTongTien" runat="server" DisplayFormatString="{0:#,# VND}" ReadOnly="True">
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Ghi Chú" ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxTextBox ID="txtGhiChu" runat="server">
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
            <dx:LayoutGroup Caption="Thông tin hàng hóa" ColCount="4" ColSpan="3" RowSpan="3">
                <Items>
                    <dx:LayoutItem Caption="Hàng Hóa" ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server">
                                <dx:ASPxComboBox ID="cmbHangHoa" runat="server" ValueType="System.String" 
                        DropDownWidth="600" DropDownStyle="DropDownList"   AutoPostBack="True"
                        ValueField="ID"
                        NullText="Nhập mã hàng.." Width="100%" TextFormatString="{0} - {1} - {2}"
                        EnableCallbackMode="true" CallbackPageSize="10"  OnSelectedIndexChanged="cmbHangHoa_SelectedIndexChanged" DataSourceID="sqlHangHoa"               
                        >                                    
                        <Columns>
                            <dx:ListBoxColumn FieldName="MaHang" Width="80px" Caption="Mã Hàng" />
                            <dx:ListBoxColumn FieldName="TenHangHoa" Width="200px" Caption="Tên Hàng Hóa"/>
                            <dx:ListBoxColumn FieldName="TenDonViTinh" Width="100px" Caption="Đơn Vị Tính"/>
                        </Columns>
                        <DropDownButton Visible="False">
                        </DropDownButton>
                    </dx:ASPxComboBox>
                                <asp:SqlDataSource ID="sqlHangHoa" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [GPM_HangHoa].[ID], [GPM_HangHoa] .[TenHangHoa],[GPM_HangHoa] .[MaHang],[GPM_DonViTinh].[TenDonViTinh] FROM [GPM_HangHoa], [GPM_DonViTinh] WHERE [GPM_HangHoa].[IDDonViTinh] = [GPM_DonViTinh].ID AND [GPM_HangHoa].[DaXoa] = 0"></asp:SqlDataSource>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Tồn Kho">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server">
                                <dx:ASPxSpinEdit ID="txtTonKho" runat="server" Enabled="False">
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="ĐVT">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server">
                                <dx:ASPxComboBox ID="cmbDonViTinh" runat="server" DataSourceID="sqlDonViTinh" TextField="TenDonViTinh" ValueField="ID" Enabled="False">
                                </dx:ASPxComboBox>
                                <asp:SqlDataSource ID="sqlDonViTinh" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenDonViTinh] FROM [GPM_DonViTinh] WHERE ([DaXoa] = @DaXoa)">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Số Lượng">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server">
                                <dx:ASPxSpinEdit ID="txtSoLuong" runat="server" NullText="1" OnNumberChanged="txtSoLuong_NumberChanged" AutoPostBack="True">
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Trọng lượng">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxSpinEdit ID="txtHHTrongLuong" runat="server" DisplayFormatString="{0:n} KG" ReadOnly="True">
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Giá bán">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxSpinEdit ID="txtGiaBan" runat="server" DisplayFormatString="{0:#,# VND}" ReadOnly="True">
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Tổng tiền">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxSpinEdit ID="txtTongTienHH" runat="server" DisplayFormatString="{0:#,# VND}" ReadOnly="True">
                                </dx:ASPxSpinEdit>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Chọn file">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxUploadControl ID="fileUpload" runat="server">
                                </dx:ASPxUploadControl>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" HorizontalAlign="Left">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer13" runat="server">
                                <dx:ASPxButton ID="btnThem" runat="server" Text="Thêm" OnClick="btnThem_Click">
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
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer14" runat="server">
                                                
                                <dx:ASPxGridView ID="gridDanhSachHangHoa_Temp" runat="server" AutoGenerateColumns="False" KeyFieldName="ID" OnRowDeleting="gridDanhSachHangHoa_Temp_RowDeleting" Width="100%">
                                    <SettingsBehavior ProcessSelectionChangedOnServer="True" ConfirmDelete="True" />
                                    <SettingsCommandButton>
                                        <ShowAdaptiveDetailButton ButtonType="Image">
                                        </ShowAdaptiveDetailButton>
                                        <HideAdaptiveDetailButton ButtonType="Image">
                                        </HideAdaptiveDetailButton>
                                        <DeleteButton ButtonType="Image" RenderMode="Image">
                                            <Image IconID="actions_cancel_16x16" ToolTip="Xóa">
                                            </Image>
                                        </DeleteButton>
                                    </SettingsCommandButton>
                                    <SettingsText CommandDelete="Xóa" ConfirmDelete="Bạn chắc chắn muốn xóa?" />
                                    <Columns>
                                        <dx:GridViewDataComboBoxColumn Caption="Tên Hàng" FieldName="TenHangHoa" ShowInCustomizationForm="True" VisibleIndex="1">
                                            <PropertiesComboBox DataSourceID="sqlHangHoa" TextField="TenHangHoa" ValueField="ID">
                                            </PropertiesComboBox>
                                        </dx:GridViewDataComboBoxColumn>
                                        <dx:GridViewCommandColumn ShowDeleteButton="True" ShowInCustomizationForm="True" VisibleIndex="7">
                                        </dx:GridViewCommandColumn>
                                        <dx:GridViewDataSpinEditColumn Caption="Số Lượng" FieldName="SoLuong" ShowInCustomizationForm="True" VisibleIndex="2">
                                            <PropertiesSpinEdit DisplayFormatString="g">
                                            </PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataTextColumn Caption="Mã Hàng" FieldName="MaHang" ShowInCustomizationForm="True" VisibleIndex="0">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataComboBoxColumn Caption="ĐVT" FieldName="IDDonViTinh" ShowInCustomizationForm="True" VisibleIndex="3">
                                            <PropertiesComboBox DataSourceID="sqlDonViTinh" TextField="TenDonViTinh" ValueField="ID">
                                            </PropertiesComboBox>
                                        </dx:GridViewDataComboBoxColumn>
                                        <dx:GridViewDataSpinEditColumn Caption="Trọng Lượng" FieldName="TrongLuong" ShowInCustomizationForm="True" VisibleIndex="6">
                                            <PropertiesSpinEdit DisplayFormatString="{0:n} KG" NumberFormat="Custom">
                                            </PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn Caption="Giá bán" FieldName="GiaBan" ShowInCustomizationForm="True" VisibleIndex="4">
                                            <PropertiesSpinEdit DisplayFormatString="{0:#,# VND}" NumberFormat="Custom">
                                            </PropertiesSpinEdit>
                                        </dx:GridViewDataSpinEditColumn>
                                        <dx:GridViewDataSpinEditColumn Caption="Tổng tiền" FieldName="TongTien" ShowInCustomizationForm="True" VisibleIndex="5">
                                            <PropertiesSpinEdit DisplayFormatString="{0:#,# VND}" NumberFormat="Custom">
                                            </PropertiesSpinEdit>
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
                                                
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" HorizontalAlign="Right" ColSpan="2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer15" runat="server">
                                <dx:ASPxButton ID="btnThemPhieuChuyenKho" runat="server" Text="Lưu" OnClick="btnThemPhieuChuyenKho_Click">
                                    <Image IconID="save_saveto_32x32">
                                    </Image>
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer16" runat="server">
                                <dx:ASPxButton ID="btnHuyPhieuChuyenKho" runat="server" Text="Hủy" OnClick="btnHuyPhieuChuyenKho_Click">
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
    <asp:HiddenField ID="IDPhieuChuyenKho_Temp" runat="server" />
</asp:Content>
