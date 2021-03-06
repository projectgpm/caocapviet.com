﻿ <%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="ThongKeDonHangChiNhanh.aspx.cs" Inherits="BanHang.ThongKeDonHangChiNhanh" %>
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
            <dx:LayoutItem Caption="Hiển Thị">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server">
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
    <dx:ASPxGridView ID="gridDanhSach" runat="server" AutoGenerateColumns="False" Width="100%" KeyFieldName="IDHangHoa" OnHtmlRowPrepared="gridDanhSach_HtmlRowPrepared">
        <SettingsDetail ShowDetailRow="True" />
        <Templates>
            <DetailRow>
                <dx:ASPxGridView ID="gridChiTiet" runat="server" AutoGenerateColumns="False" KeyFieldName="ID" Width="100%" OnBeforePerformDataSelect="gridChiTiet_BeforePerformDataSelect" DataSourceID="dsChiTiet">
                            <SettingsPager Mode="ShowAllRecords">
                            </SettingsPager>
                            <Settings ShowTitlePanel="True" ShowFooter="True" />
                            <SettingsCommandButton>
                                <ShowAdaptiveDetailButton ButtonType="Image">
                                </ShowAdaptiveDetailButton>
                                <HideAdaptiveDetailButton ButtonType="Image">
                                </HideAdaptiveDetailButton>
                                <NewButton ButtonType="Image" RenderMode="Image">
                                    <Image IconID="numberformats_accounting_16x16">
                                    </Image>
                                </NewButton>
                                <EditButton ButtonType="Image" RenderMode="Image">
                                    <Image IconID="tasks_edittask_16x16office2013">
                                    </Image>
                                </EditButton>
                                <DeleteButton ButtonType="Image" RenderMode="Image">
                                    <Image IconID="actions_close_16x16devav">
                                    </Image>
                                </DeleteButton>
                            </SettingsCommandButton>
                            <SettingsText Title="CHI NHÁNH ĐẶT HÀNG" EmptyDataRow="không có dữ liệu" />
                            <Columns>
                                <dx:GridViewDataTextColumn Caption="Chi nhánh" VisibleIndex="2" FieldName="TenCuaHang">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataSpinEditColumn Caption="Số lượng đặt" VisibleIndex="4" FieldName="SoLuong">
                                    <PropertiesSpinEdit DisplayFormatString="g">
                                    </PropertiesSpinEdit>
                                </dx:GridViewDataSpinEditColumn>
                                <dx:GridViewDataSpinEditColumn Caption="Tồn kho" VisibleIndex="3" FieldName="TonKho">
                                    <PropertiesSpinEdit DisplayFormatString="g">
                                    </PropertiesSpinEdit>
                                </dx:GridViewDataSpinEditColumn>
                              
                            </Columns>
                            <Styles>
                                <Header HorizontalAlign="Center">
                                </Header>
                                <TitlePanel ForeColor="#00CC00" Font-Bold="True" Font-Size="14px">
                                </TitlePanel>
                            </Styles>
                        </dx:ASPxGridView>
                <asp:SqlDataSource ID="dsChiTiet" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT GPM_DonHangChiNhanh_ChiTiet.SoLuong, GPM_Kho.TenCuaHang, GPM_DonHangChiNhanh_ChiTiet.TonKho FROM GPM_DonHangChiNhanh_ChiTiet INNER JOIN GPM_DonHangChiNhanh ON GPM_DonHangChiNhanh_ChiTiet.IDDonHangChiNhanh = GPM_DonHangChiNhanh.ID INNER JOIN GPM_Kho ON GPM_DonHangChiNhanh_ChiTiet.IDKho = GPM_Kho.ID WHERE (GPM_DonHangChiNhanh_ChiTiet.IDHangHoa = @IDHangHoa) AND (GPM_DonHangChiNhanh.GiamSatDuyet IS NOT NULL) AND (GPM_DonHangChiNhanh.TrangThai = 0)">
                    <SelectParameters>
                        <asp:SessionParameter Name="IDHangHoa" SessionField="IDHangHoa" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </DetailRow>
        </Templates>
        <SettingsPager Mode="ShowAllRecords">
        </SettingsPager>
        <SettingsEditing Mode="PopupEditForm">
        </SettingsEditing>
        <Settings AutoFilterCondition="Contains" ShowFilterRow="True" ShowTitlePanel="True" />
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
        <SettingsPopup>
            <EditForm HorizontalAlign="WindowCenter" Modal="True" VerticalAlign="WindowCenter" />
        </SettingsPopup>
        <SettingsSearchPanel Visible="True" />
        <SettingsText CommandCancel="Hủy bỏ thao tác" CommandDelete="Xóa" CommandEdit="Sửa" CommandNew="Thêm" CommandUpdate="Lưu" ConfirmDelete="Bạn có chắc chắn muốn xóa không?. Thao tác này có thể xóa Hàng Hóa thuộc nhà cung cấp!." PopupEditFormCaption="Thông tin nhà cung cấp" Title="DANH SÁCH MÃ HÀNG CHI NHÁNH ĐẶT" EmptyDataRow="Không có dữ liệu hiển thị" SearchPanelEditorNullText="Nhập thông tin cần tìm..." />
        <Columns>
            <dx:GridViewDataTextColumn Caption="Mã Hàng" FieldName="MaHang" VisibleIndex="0">
            </dx:GridViewDataTextColumn>
            <dx:GridViewDataSpinEditColumn Caption="Tồn Kho" FieldName="TonKho" VisibleIndex="3">
                <PropertiesSpinEdit DisplayFormatString="N0" NumberFormat="Custom">
                </PropertiesSpinEdit>
            </dx:GridViewDataSpinEditColumn>
            <dx:GridViewDataSpinEditColumn Caption="Tổng Số Lượng Đặt" FieldName="SoLuongDat" VisibleIndex="4" CellStyle-Font-Bold="true">
                <PropertiesSpinEdit DisplayFormatString="N0" NumberFormat="Custom">
                </PropertiesSpinEdit>

<CellStyle Font-Bold="True"></CellStyle>
            </dx:GridViewDataSpinEditColumn>
            <dx:GridViewDataComboBoxColumn Caption="ĐVT" FieldName="IDDonViTinh" VisibleIndex="2">
                <PropertiesComboBox DataSourceID="SqlDVT" TextField="TenDonViTinh" ValueField="ID">
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
            <dx:GridViewDataComboBoxColumn Caption="Tên Hàng Hóa" FieldName="IDHangHoa" VisibleIndex="1">
                <PropertiesComboBox DataSourceID="SqlHangHoa" TextField="TenHangHoa" ValueField="ID">
                </PropertiesComboBox>
            </dx:GridViewDataComboBoxColumn>
        </Columns>

        <FormatConditions>
            <dx:GridViewFormatConditionHighlight FieldName="SoLuongDat" Expression="[SoLuongDat] > [TonKho]" Format="LightRedFillWithDarkRedText" />
        </FormatConditions>





         <TotalSummary>
             <dx:ASPxSummaryItem DisplayFormat="Tổng : {0}" FieldName="SoLuongDat" ShowInColumn="Tổng Số Lượng Đặt" SummaryType="Sum" />
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
       <asp:SqlDataSource ID="SqlDVT" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenDonViTinh] FROM [GPM_DonViTinh] WHERE ([DaXoa] = @DaXoa)">
           <SelectParameters>
               <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
           </SelectParameters>
       </asp:SqlDataSource>
       <asp:SqlDataSource ID="SqlHangHoa" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenHangHoa] FROM [GPM_HangHoa] WHERE (([DaXoa] = @DaXoa) AND ([TenHangHoa] IS NOT NULL))">
           <SelectParameters>
               <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
           </SelectParameters>
       </asp:SqlDataSource>
</asp:Content>
