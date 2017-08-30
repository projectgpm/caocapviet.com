<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="DuyetDonHangThuMua.aspx.cs" Inherits="BanHang.DuyetDonHangThuMua" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="3" Width="100%">
    <Items>
        <dx:LayoutGroup Caption="Thông tin đơn hàng" ColCount="3" ColSpan="3" RowSpan="3">
            <Items>
                <dx:LayoutItem Caption="Trạng Thái Đơn Hàng">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer runat="server">
                            <dx:ASPxComboBox ID="cmbTrangThaiDonHang" runat="server" Width="100%" DataSourceID="SqlTrangThaiHang" TextField="TenTrangThai" ValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="cmbTrangThaiDonHang_SelectedIndexChanged">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlTrangThaiHang" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenTrangThai] FROM [GPM_TrangThaiDonHang] WHERE ([ID] &lt;&gt; @ID)">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="3" Name="ID" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Số Đơn Hàng">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">
                            <dx:ASPxComboBox ID="cmbSoDonHang" runat="server" Width="100%" 
                                DataSourceID="SqlSoDonHangThuMua" TextField="SoDonHang" 
                                ValueField="ID"
                                 NullText="Vui lòng chọn số đơn hàng..."
                                DropDownWidth="750px" DropDownStyle="DropDownList"   TextFormatString="{0}" AutoPostBack="True" OnSelectedIndexChanged="cmbSoDonHang_SelectedIndexChanged" Enabled="False"
                                >
                                <Columns>
                                    <dx:ListBoxColumn Caption="Số Đơn Hàng" FieldName="SoDonHang" Width="170px" />
                                    <dx:ListBoxColumn Caption="Người Lập" FieldName="TenNguoiDung" Width="100%" />
                                    <dx:ListBoxColumn Caption="Ngày Lập Phiếu" FieldName="NgayLap" Width="100px" />   
                                    <dx:ListBoxColumn Caption="Tổng Tiền" FieldName="TongTien" Width="100px" />          
                                </Columns>
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlSoDonHangThuMua" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [GPM_ThuMua_DonHang].[ID], [GPM_ThuMua_DonHang].[SoDonHang], [GPM_ThuMua_DonHang].[NgayLap], GPM_ThuMua_DonHang.[TongTien] ,[GPM_NguoiDung].TenNguoiDung FROM [GPM_ThuMua_DonHang],[GPM_NguoiDung] WHERE (GPM_ThuMua_DonHang.IDTrangThaiDonHang > 2  AND [GPM_NguoiDung].ID = GPM_ThuMua_DonHang.IDNguoiLap  AND (GPM_ThuMua_DonHang.[IDKhoLap] IS NOT NULL) AND (GPM_ThuMua_DonHang.[SoDonHang] IS NOT NULL)) ORDER BY GPM_ThuMua_DonHang.[ID] DESC">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Nhà Cung Cấp">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
                            <dx:ASPxComboBox ID="cmbNhaCungCap"  Width="100%" runat="server" Enabled="False" DataSourceID="SqlNhaCungCap" TextField="TenNhaCungCap" ValueField="ID">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlNhaCungCap" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenNhaCungCap] FROM [GPM_NhaCungCap] WHERE ([DaXoa] = @DaXoa)">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Người Lập">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
                            <dx:ASPxComboBox ID="cmbNguoiLap" runat="server" DataSourceID="SqlNguoiDung" Enabled="False" TextField="TenNguoiDung" ValueField="ID" Width="100%">
                            </dx:ASPxComboBox>
                            <asp:SqlDataSource ID="SqlNguoiDung" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenNguoiDung] FROM [GPM_NguoiDung] WHERE ([DaXoa] = @DaXoa)">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Ngày Lập">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server">
                            <dx:ASPxDateEdit ID="txtNgayLap" runat="server" Enabled="False" Width="100%">
                            </dx:ASPxDateEdit>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Người Duyệt">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server">
                            <dx:ASPxTextBox ID="txtNguoiDuyet" runat="server" Enabled="False" Width="100%">
                            </dx:ASPxTextBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Ngày Duyệt">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server">
                            <dx:ASPxDateEdit ID="txtNgayDuyet" runat="server" Width="100%" OnInit="txtNgayDuyet_Init">
                            </dx:ASPxDateEdit>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Ghi Chú" ColSpan="2">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server">
                            <dx:ASPxTextBox ID="txtGhiChu" runat="server" Width="100%">
                            </dx:ASPxTextBox>
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
                                                
                            <dx:ASPxGridView ID="gridDanhSachHangHoa" runat="server" AutoGenerateColumns="False" KeyFieldName="ID" Width="100%" OnRowUpdating="gridDanhSachHangHoa_RowUpdating">
                                 <SettingsEditing Mode="Batch">
                                 </SettingsEditing>
                                 <Settings ShowFooter="True" />
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
                                    <UpdateButton>
                                        <Image ToolTip="Lưu">
                                        </Image>
                                    </UpdateButton>
                                    <CancelButton>
                                        <Image ToolTip="Hủy thao tác">
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
                                <SettingsText CommandBatchEditCancel="Hủy tất cả" CommandBatchEditUpdate="Lưu tất cả" Title="DANH SÁCH HÀNG HÓA GIÁ THEO CHI NHÁNH" ConfirmDelete="Bạn chắc chắn muốn xóa?" />
                                <Columns>
                                    <dx:GridViewDataTextColumn Caption="Mã Hàng" FieldName="MaHang" ShowInCustomizationForm="True" VisibleIndex="0" ReadOnly="True">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataSpinEditColumn Caption="Thành Tiền" FieldName="ThanhTien" ShowInCustomizationForm="True" VisibleIndex="8" ReadOnly="True">
                                        <PropertiesSpinEdit DisplayFormatString="N0" NumberFormat="Custom">
                                        </PropertiesSpinEdit>
                                    </dx:GridViewDataSpinEditColumn>
<dx:GridViewDataSpinEditColumn FieldName="DonGia" ShowInCustomizationForm="True" Caption="Đơn Giá" VisibleIndex="7" ReadOnly="True">
<PropertiesSpinEdit DisplayFormatString="N0" NumberFormat="Custom"></PropertiesSpinEdit>
</dx:GridViewDataSpinEditColumn>
                                    <dx:GridViewDataSpinEditColumn Caption="Số Lượng" FieldName="SoLuong" ShowInCustomizationForm="True" VisibleIndex="4" ReadOnly="True">
<PropertiesSpinEdit DisplayFormatString="g"></PropertiesSpinEdit>
                                    </dx:GridViewDataSpinEditColumn>
                                    <dx:GridViewDataComboBoxColumn Caption="Tên Hàng Hóa" FieldName="IDHangHoa" ShowInCustomizationForm="True" VisibleIndex="1" ReadOnly="True">
                                        <PropertiesComboBox DataSourceID="SqlDanhSachHangHoa" TextField="TenHangHoa" ValueField="ID">
                                        </PropertiesComboBox>
                                    </dx:GridViewDataComboBoxColumn>
                                    <dx:GridViewDataComboBoxColumn Caption="ĐVT" FieldName="IDDonViTinh" ShowInCustomizationForm="True" VisibleIndex="2" ReadOnly="True">
                                        <PropertiesComboBox DataSourceID="SqlDanhSachDonViTinh" TextField="TenDonViTinh" ValueField="ID">
                                        </PropertiesComboBox>
                                    </dx:GridViewDataComboBoxColumn>
                                    <dx:GridViewDataSpinEditColumn Caption="Trọng Lượng" FieldName="TrongLuong" ShowInCustomizationForm="True" VisibleIndex="3" ReadOnly="True">
                                        <PropertiesSpinEdit DisplayFormatString="g">
                                        </PropertiesSpinEdit>
                                    </dx:GridViewDataSpinEditColumn>

                                    <dx:GridViewDataSpinEditColumn Caption="Thực Tế (*)" FieldName="ThucTe" ShowInCustomizationForm="True" VisibleIndex="5">
                                        <PropertiesSpinEdit DisplayFormatString="g">
                                        </PropertiesSpinEdit>
                                    </dx:GridViewDataSpinEditColumn>
                                    <dx:GridViewDataSpinEditColumn Caption="Chênh Lệch" FieldName="ChenhLech" ShowInCustomizationForm="True" VisibleIndex="6" ReadOnly="True">
                                        <PropertiesSpinEdit DisplayFormatString="g">
                                        </PropertiesSpinEdit>
                                    </dx:GridViewDataSpinEditColumn>

                                </Columns>
                                 <TotalSummary>
                                     <dx:ASPxSummaryItem FieldName="ThanhTien" ShowInColumn="Thành Tiền" SummaryType="Sum" DisplayFormat="Tổng tiền: {0:N0}"/>
                                     <dx:ASPxSummaryItem FieldName="TrongLuong" ShowInColumn="Trọng Lượng" SummaryType="Sum" DisplayFormat="Tổng trọng lượng: {0}"/>
                                 </TotalSummary>
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
</asp:Content>
