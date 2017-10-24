<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChiTietDonHangHanMuc.aspx.cs" Inherits="BanHang.ChiTietDonHangHanMuc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       
        
         <dx:ASPxFormLayout ID="ASPxFormLayout2" runat="server" ColCount="2">
             <Items>
                 <dx:LayoutItem Caption="">
                     <LayoutItemNestedControlCollection>
                         <dx:LayoutItemNestedControlContainer runat="server">
                              <dx:ASPxButton ID="btnThemMoi" runat="server" Text="Thêm Mới" OnClick="btnThemMoi_Click">
                                    <Image IconID="actions_add_32x32">
                                    </Image>
                                </dx:ASPxButton>
                         </dx:LayoutItemNestedControlContainer>
                     </LayoutItemNestedControlCollection>
                 </dx:LayoutItem>
                 <dx:LayoutItem Caption="">
                     <LayoutItemNestedControlCollection>
                         <dx:LayoutItemNestedControlContainer runat="server">
                             <dx:ASPxButton ID="btnDuyetDonHang" runat="server" Text="Duyệt Đơn Hàng" OnClick="btnDuyetDonHang_Click">
            <Image IconID="actions_apply_32x32">
               
            </Image>
        </dx:ASPxButton>
                         </dx:LayoutItemNestedControlContainer>
                     </LayoutItemNestedControlCollection>
                 </dx:LayoutItem>
             </Items>
        </dx:ASPxFormLayout>
      <dx:ASPxGridView runat="server" AutoGenerateColumns="False" Width="100%" ID="gridChiTiet" KeyFieldName="ID" OnRowUpdating="gridChiTiet_RowUpdating" OnRowInserting="gridChiTiet_RowInserting" OnHtmlRowPrepared="gridChiTiet_HtmlRowPrepared">
        <SettingsPager Mode="ShowAllRecords">
        </SettingsPager>
        <SettingsEditing Mode="PopupEditForm">
        </SettingsEditing>
<Settings ShowTitlePanel="True" ShowFooter="True" ShowFilterRow="True"></Settings>

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
            <DeleteButton ButtonType="Image" RenderMode="Image">
                <Image IconID="actions_cancel_16x16" ToolTip="Xóa">
                </Image>
            </DeleteButton>
        </SettingsCommandButton>

          <SettingsDataSecurity AllowEdit="False" />

        <SettingsPopup>
            <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter" />
        </SettingsPopup>

        <SettingsSearchPanel Visible="True" />

<SettingsText Title="THÔNG TIN CHI TIẾT" CommandDelete="Xóa" ConfirmDelete="Bạn chắc chắn muốn xóa?" CommandEdit="Sửa" EmptyDataRow="Chi tiết đơn hàng trống" SearchPanelEditorNullText="Nhập thông tin cần tìm..." CommandNew="Thêm"></SettingsText>
<Columns>
    
    <dx:GridViewDataButtonEditColumn Caption="Sửa Số Lượng" Width="70px" 
                                        VisibleIndex="12" Name="chucnang">
                <DataItemTemplate>
                     
                    <dx:ASPxButton ID="BtnSuaSoLuong" runat="server" CommandName="SuaSoLuongHang"
                        CommandArgument='<%# Eval("ID") %>' 
                        onclick="BtnSuaSoLuong_Click" RenderMode="Link">
                        <Image IconID="actions_edit_16x16devav">
                        </Image>
                    </dx:ASPxButton>
                     
                </DataItemTemplate>
                <CellStyle HorizontalAlign="Center">
                </CellStyle>
            </dx:GridViewDataButtonEditColumn>
    <dx:GridViewDataSpinEditColumn Caption="Số Lượng Đặt" FieldName="SoLuong" VisibleIndex="8">
        <propertiesspinedit DisplayFormatString="N0"></propertiesspinedit>
        <HeaderStyle Wrap="True" />
    </dx:GridViewDataSpinEditColumn>
    <dx:GridViewDataTextColumn Caption="Mã Hàng" FieldName="MaHang" VisibleIndex="0" Name="chucnang">
    </dx:GridViewDataTextColumn>
    <dx:GridViewDataSpinEditColumn Caption="Trọng Lượng" FieldName="TrongLuong" VisibleIndex="4" ReadOnly="True">
        <PropertiesSpinEdit DisplayFormatString="{0:N0}" NumberFormat="Custom">
        </PropertiesSpinEdit>
        <HeaderStyle Wrap="True" />
    </dx:GridViewDataSpinEditColumn>
    <dx:GridViewDataComboBoxColumn Caption="Hàng Hóa" FieldName="IDHangHoa" VisibleIndex="1">
        <PropertiesComboBox DataSourceID="SqlHangHoa" TextField="TenHangHoa" ValueField="ID">
        </PropertiesComboBox>
    </dx:GridViewDataComboBoxColumn>
    <dx:GridViewDataComboBoxColumn Caption="Đơn Vị Tính" FieldName="IDDonViTinh" VisibleIndex="2">
        <PropertiesComboBox DataSourceID="SqlDonViTinh" TextField="TenDonViTinh" ValueField="ID">
        </PropertiesComboBox>
    </dx:GridViewDataComboBoxColumn>
    <dx:GridViewDataTextColumn Caption="Ghi Chú" FieldName="GhiChu" VisibleIndex="11">
    </dx:GridViewDataTextColumn>
    <dx:GridViewDataSpinEditColumn Caption="Tồn Kho" FieldName="TonKho" VisibleIndex="3">
        <propertiesspinedit DisplayFormatString="N0" NumberFormat="Custom"></propertiesspinedit>
    </dx:GridViewDataSpinEditColumn>
    <dx:GridViewDataSpinEditColumn Caption="Thành Tiền" FieldName="ThanhTien" VisibleIndex="10">
        <PropertiesSpinEdit DisplayFormatString="{0:N0}" NumberFormat="Custom">
        </PropertiesSpinEdit>
    </dx:GridViewDataSpinEditColumn>
    <dx:GridViewDataSpinEditColumn Caption="Đơn Giá" FieldName="DonGia" VisibleIndex="9">
        <PropertiesSpinEdit DisplayFormatString="{0:N0}" NumberFormat="Custom">
        </PropertiesSpinEdit>
    </dx:GridViewDataSpinEditColumn>
  <dx:GridViewDataSpinEditColumn Caption="Số Lượng Đặt Trước" FieldName="SoLuongDatTruoc" VisibleIndex="7">
        <PropertiesSpinEdit DisplayFormatString="N0" NumberFormat="Custom">
        </PropertiesSpinEdit>
        <HeaderStyle Wrap="True" />
    </dx:GridViewDataSpinEditColumn>
    <dx:GridViewDataSpinEditColumn Caption="Tuần Suất Bán Hàng" FieldName="TanSuatBanHang" VisibleIndex="6">
        <PropertiesSpinEdit DisplayFormatString="N0" NumberFormat="Custom">
        </PropertiesSpinEdit>
        <HeaderStyle Wrap="True" />
    </dx:GridViewDataSpinEditColumn>
    <dx:GridViewDataSpinEditColumn Caption="Số Lượng Đề Nghị" FieldName="SoLuongDeNghi" VisibleIndex="5">
        <PropertiesSpinEdit DisplayFormatString="N0" NumberFormat="Custom">
        </PropertiesSpinEdit>
        <HeaderStyle Wrap="True" />
    </dx:GridViewDataSpinEditColumn>
</Columns>

        <TotalSummary>
            <dx:ASPxSummaryItem DisplayFormat="Tổng = {0:N0}" FieldName="SoLuong" ShowInColumn="Số Lượng Đặt" SummaryType="Sum" />
            <dx:ASPxSummaryItem DisplayFormat="Tổng mặt hàng : {0:N0}" FieldName="MaHang" ShowInColumn="Hàng Hóa" SummaryType="Count" />
            <dx:ASPxSummaryItem DisplayFormat="Tổng = {0:N0} VNĐ" FieldName="ThanhTien" ShowInColumn="Thành Tiền" SummaryType="Sum" />
        </TotalSummary>

<Styles>
<Header HorizontalAlign="Center" Font-Bold="True"></Header>

<AlternatingRow Enabled="True"></AlternatingRow>

<TitlePanel HorizontalAlign="Left" Font-Bold="True"></TitlePanel>
</Styles>
  
</dx:ASPxGridView>
            <asp:SqlDataSource ID="SqlDonViTinh" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenDonViTinh] FROM [GPM_DonViTinh] WHERE ([DaXoa] = @DaXoa)">
                <SelectParameters>
                    <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlHangHoa" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenHangHoa] FROM [GPM_HangHoa] WHERE (([DaXoa] = @DaXoa) AND ([TenHangHoa] IS NOT NULL))">
                <SelectParameters>
                    <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                  
                </SelectParameters>
            </asp:SqlDataSource>
           <dx:ASPxPopupControl ID="popupSuaSoLuong" runat="server" HeaderText="Sửa số lượng hàng" Width="500px" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">

        <ContentCollection>
<dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
    <dx:ASPxFormLayout ID="formSuaSoLuong" runat="server" ColCount="2" Width="100%">
        <Items>
            <dx:LayoutItem Caption="Mã hàng" ColSpan="2">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
                        <dx:ASPxTextBox ID="txtMaHangSua" runat="server" Width="100%" Enabled="False">
                        </dx:ASPxTextBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Tên hàng" ColSpan="2">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
                        <dx:ASPxTextBox ID="txtTenHangSua" runat="server" Width="100%" Enabled="False">
                        </dx:ASPxTextBox>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="Số lượng" ColSpan="2">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">
                        <dx:ASPxSpinEdit ID="txtSoLuongSua" runat="server" DisplayFormatString="g" HorizontalAlign="Center" Number="0" NumberType="Integer">
                        </dx:ASPxSpinEdit>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="" HorizontalAlign="Right" ShowCaption="False">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server">
                        <dx:ASPxButton ID="btnLuuSuaSL" runat="server" OnClick="btnLuuSuaSL_Click">
                            <Image IconID="save_save_32x32office2013">
                            </Image>
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem HorizontalAlign="Left" ShowCaption="False">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server">
                        <dx:ASPxButton ID="btnHuySuaSl" runat="server" OnClick="btnHuySuaSl_Click">
                            <Image IconID="actions_close_32x32">
                            </Image>
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
        </Items>
    </dx:ASPxFormLayout>
    <asp:HiddenField ID="hdfIDSuaSL" runat="server" />
            </dx:PopupControlContentControl>
</ContentCollection>

    </dx:ASPxPopupControl>
        <dx:ASPxPopupControl ID="popupThemMoi" runat="server" HeaderText="Thêm mới" Width="1100px" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">

        <ContentCollection>
<dx:PopupControlContentControl ID="popupThemMoi2" runat="server">
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="2" Width="100%">
    <Items>
        <dx:LayoutGroup Caption="Hàng Hóa" ColCount="2" ColSpan="2" RowSpan="3">
            <Items>
                <dx:LayoutItem Caption="Hàng Hóa">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer16" runat="server">
                            <dx:ASPxComboBox ID="cmbHangHoa" runat="server" 
                                AutoPostBack="True" OnSelectedIndexChanged="cmbHangHoa_SelectedIndexChanged" 
                                DropDownWidth="750px" DropDownStyle="DropDownList"   TextFormatString="{0}"
                                EnableCallbackMode="true" Width="100%" 
                                OnItemsRequestedByFilterCondition="cmbHangHoa_ItemsRequestedByFilterCondition"
                                OnItemRequestedByValue="cmbHangHoa_ItemRequestedByValue"
                                ValueField="ID"
                                NullText="Vui lòng chọn hàng hóa.........."
                                >
                                <Columns>
                                    <dx:ListBoxColumn Caption="Mã Hàng" FieldName="MaHang" Width="70px" />
                                    <dx:ListBoxColumn Caption="Tên Hàng Hóa" FieldName="TenHangHoa" Width="100%" />
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
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer17" runat="server">
                            <dx:ASPxComboBox ID="txtTonKho" runat="server" Enabled="False" Width="100%">
                            </dx:ASPxComboBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Số Lượng Gợi Ý">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer18" runat="server">
                            <dx:ASPxSpinEdit ID="txtSoLuongGoiY" runat="server" Width="100%" Enabled="False">
                            </dx:ASPxSpinEdit>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Số Lượng Đặt">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer19" runat="server">
                            <dx:ASPxSpinEdit ID="txtSoLuong" runat="server" Width="100%">
                            </dx:ASPxSpinEdit>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Trọng Lượng">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer20" runat="server">
                            <dx:ASPxTextBox ID="txtTrongLuong" runat="server" Enabled="False" Width ="100%">
                            </dx:ASPxTextBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="Ghi Chú">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer21" runat="server">
                            <dx:ASPxTextBox ID="txtGhiChuHangHoa" runat="server" Width="100%">
                            </dx:ASPxTextBox>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="" ColSpan="2" HorizontalAlign="Right">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer22" runat="server">
                            <dx:ASPxButton ID="btnThem_Temp" runat="server" OnClick="btnThem_Temp_Click" Text="Thêm">
                                <Image IconID="actions_add_32x32">
                                </Image>
                            </dx:ASPxButton>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
            </Items>
        </dx:LayoutGroup>
        <dx:LayoutGroup ColSpan="2" Caption="Danh sách hàng hóa" ColCount="2">
            <Items>
                <dx:LayoutItem Caption="" ColSpan="2">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer24" runat="server">
                                                
                            <dx:ASPxGridView ID="gridDanhSachHangHoa" runat="server" AutoGenerateColumns="False" KeyFieldName="ID" Width="100%" OnRowDeleting="gridDanhSachHangHoa_RowDeleting" OnHtmlRowPrepared="gridDanhSachHangHoa_HtmlRowPrepared">
                                 <SettingsPager Mode="ShowAllRecords">
                                 </SettingsPager>
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
                                    <DeleteButton>
                                        <Image IconID="actions_cancel_16x16" ToolTip="Xóa">
                                        </Image>
                                    </DeleteButton>
                                </SettingsCommandButton>
                                <SettingsText CommandBatchEditCancel="Hủy tất cả" CommandBatchEditUpdate="Lưu tất cả" Title="DANH SÁCH HÀNG HÓA GIÁ THEO CHI NHÁNH" ConfirmDelete="Bạn chắc chắn muốn xóa?" EmptyDataRow="Danh sách hàng hóa trống." CommandDelete="Xóa" />
                                <Columns>                                    
                                    <dx:GridViewCommandColumn ShowDeleteButton="True" ShowInCustomizationForm="True" VisibleIndex="12">
                                    </dx:GridViewCommandColumn>
                                    <dx:GridViewDataTextColumn Caption="Mã Hàng" FieldName="MaHang" ShowInCustomizationForm="True" VisibleIndex="0">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataSpinEditColumn Caption="Số Lượng Đặt" FieldName="SoLuong" ShowInCustomizationForm="True" VisibleIndex="8">
<PropertiesSpinEdit DisplayFormatString="N0" NumberFormat="Custom"></PropertiesSpinEdit>
                                        <HeaderStyle Wrap="True" />
                                    </dx:GridViewDataSpinEditColumn>
                                    <dx:GridViewDataComboBoxColumn Caption="Tên Hàng Hóa" FieldName="IDHangHoa" ShowInCustomizationForm="True" VisibleIndex="1">
                                        <PropertiesComboBox DataSourceID="SqlDanhSachHangHoa" TextField="TenHangHoa" ValueField="ID">
                                        </PropertiesComboBox>
                                    </dx:GridViewDataComboBoxColumn>
                                    <dx:GridViewDataComboBoxColumn Caption="ĐVT" FieldName="IDDonViTinh" ShowInCustomizationForm="True" VisibleIndex="2">
                                        <PropertiesComboBox DataSourceID="SqlDanhSachDonViTinh" TextField="TenDonViTinh" ValueField="ID">
                                        </PropertiesComboBox>
                                    </dx:GridViewDataComboBoxColumn>
                                    <dx:GridViewDataSpinEditColumn Caption="Trọng Lượng" FieldName="TrongLuong" ShowInCustomizationForm="True" VisibleIndex="4">
                                        <PropertiesSpinEdit DisplayFormatString="g">
                                        </PropertiesSpinEdit>
                                        <HeaderStyle Wrap="True" />
                                    </dx:GridViewDataSpinEditColumn>
                                    <dx:GridViewDataSpinEditColumn Caption="Tồn Kho" FieldName="TonKho" ShowInCustomizationForm="True" VisibleIndex="3">
                                        <PropertiesSpinEdit DisplayFormatString="N0" NumberFormat="Custom">
                                        </PropertiesSpinEdit>
                                    </dx:GridViewDataSpinEditColumn>
                                    <dx:GridViewDataTextColumn Caption="Ghi Chú" FieldName="GhiChu" ShowInCustomizationForm="True" VisibleIndex="11">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataSpinEditColumn Caption="Giá Bán" FieldName="DonGia" ShowInCustomizationForm="True" VisibleIndex="9">
                                        <PropertiesSpinEdit DisplayFormatString="N0" NumberFormat="Custom">
                                        </PropertiesSpinEdit>
                                    </dx:GridViewDataSpinEditColumn>
                                    <dx:GridViewDataSpinEditColumn Caption="Thành Tiền" FieldName="ThanhTien" ShowInCustomizationForm="True" VisibleIndex="10">
                                        <PropertiesSpinEdit DisplayFormatString="N0" NumberFormat="Custom">
                                        </PropertiesSpinEdit>
                                    </dx:GridViewDataSpinEditColumn>
                                   <dx:GridViewDataSpinEditColumn Caption="Số Lượng Đặt Trước" FieldName="SoLuongDatTruoc" VisibleIndex="7">
                                        <PropertiesSpinEdit DisplayFormatString="N0" NumberFormat="Custom">
                                        </PropertiesSpinEdit>
                                        <HeaderStyle Wrap="True" />
                                    </dx:GridViewDataSpinEditColumn>
                                    <dx:GridViewDataSpinEditColumn Caption="Tuần Suất Bán Hàng" FieldName="TanSuatBanHang" VisibleIndex="6">
                                        <PropertiesSpinEdit DisplayFormatString="N0" NumberFormat="Custom">
                                        </PropertiesSpinEdit>
                                        <HeaderStyle Wrap="True" />
                                    </dx:GridViewDataSpinEditColumn>
                                    <dx:GridViewDataSpinEditColumn Caption="Số Lượng Đề Nghị" FieldName="SoLuongDeNghi" VisibleIndex="5">
                                        <PropertiesSpinEdit DisplayFormatString="N0" NumberFormat="Custom">
                                        </PropertiesSpinEdit>
                                        <HeaderStyle Wrap="True" />
                                    </dx:GridViewDataSpinEditColumn>
                                </Columns>
                                 <TotalSummary>
                                     <dx:ASPxSummaryItem DisplayFormat="Tổng mặt hàng : {0:N0}" FieldName="MaHang" ShowInColumn="Tên Hàng Hóa" SummaryType="Count" />
                                     <dx:ASPxSummaryItem  DisplayFormat="Tổng : {0:N0}" FieldName="TrongLuong" ShowInColumn="Trọng Lượng" SummaryType="Sum" />
                                     <dx:ASPxSummaryItem DisplayFormat="Tổng : {0:N0}" FieldName="SoLuong" ShowInColumn="Số Lượng Đặt" SummaryType="Sum" />
                                     <dx:ASPxSummaryItem DisplayFormat="Tổng : {0:N0} " FieldName="ThanhTien" ShowInColumn="Thành Tiền" SummaryType="Sum" />
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
                <dx:LayoutItem Caption="" HorizontalAlign="Right">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer25" runat="server">
                            <dx:ASPxButton ID="btnThem" runat="server" Text="Lưu" OnClick="btnThem_Click">
                                <Image IconID="save_saveto_32x32">
                                </Image>
                            </dx:ASPxButton>
                        </dx:LayoutItemNestedControlContainer>
                    </LayoutItemNestedControlCollection>
                </dx:LayoutItem>
                <dx:LayoutItem Caption="">
                    <LayoutItemNestedControlCollection>
                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer26" runat="server">
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
 </dx:PopupControlContentControl>
</ContentCollection>

    </dx:ASPxPopupControl>
    </div>
    </form>
</body>
</html>
