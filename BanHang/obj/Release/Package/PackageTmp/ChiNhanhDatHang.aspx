﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="ChiNhanhDatHang.aspx.cs" Inherits="BanHang.ChiNhanhDatHang" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
     <script type="text/javascript">
         function OnMoreInfoClick(element, key) {
             popup.SetContentUrl("ChiTietDonHangChiNhanh.aspx?IDDonHangChiNhanh=" + key);
             popup.ShowAtElement();
             // alert(key);
         }

    </script>
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" ColCount="5">
        <Items>
            <dx:LayoutItem Caption="">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
                        <dx:ASPxButton ID="btnTaoDonHang" runat="server" Text="Tạo Đơn Hàng" HorizontalAlign="Right" VerticalAlign="Middle" PostBackUrl="ThemDonHangChiNhanh.aspx">
                            <Image IconID="actions_add_32x32">
                            </Image>
                            <Paddings Padding="4px" />
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
           
            <dx:LayoutItem Caption="">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxButton ID="btnDonHangDaDuyet" runat="server" PostBackUrl="DonHangDaDuyet.aspx" Text="Đơn Hàng Chờ Xử Lý">
                            <Image IconID="businessobjects_bofileattachment_32x32">
                            </Image>
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
           
            <dx:LayoutItem Caption="">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxButton ID="ASPxFormLayout1_E3" runat="server" Text="Đơn Hàng Hoàn Tất" PostBackUrl="DonHangHoanTat.aspx">
                            <Image IconID="content_checkbox_32x32">
                            </Image>
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
           
            <dx:LayoutItem Caption="">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxButton ID="ASPxFormLayout1_E2" runat="server" Text="Đơn Hàng Đã Hủy" PostBackUrl="DonHangDaHuy.aspx">
                            <Image IconID="reports_deleteheader_32x32">
                            </Image>
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutGroup Caption="Lọc" ColCount="5" ColSpan="5">
                 <Items>
                     <dx:LayoutItem Caption="Từ Ngày">
                         <LayoutItemNestedControlCollection>
                             <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
                                 <dx:ASPxDateEdit ID="dateTuNgay" runat="server" DisplayFormatString="dd/MM/yyyy" Width="100%">
                                     <ValidationSettings SetFocusOnError="True">
                                         <RequiredField IsRequired="True" />
                                     </ValidationSettings>
                                 </dx:ASPxDateEdit>
                             </dx:LayoutItemNestedControlContainer>
                         </LayoutItemNestedControlCollection>
                     </dx:LayoutItem>
                     <dx:LayoutItem Caption="Đến Ngày">
                         <LayoutItemNestedControlCollection>
                             <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
                                 <dx:ASPxDateEdit ID="dateDenNgay" runat="server" DisplayFormatString="dd/MM/yyyy" Width="100%">
                                     <ValidationSettings SetFocusOnError="True">
                                         <RequiredField IsRequired="True" />
                                     </ValidationSettings>
                                 </dx:ASPxDateEdit>
                             </dx:LayoutItemNestedControlContainer>
                         </LayoutItemNestedControlCollection>
                     </dx:LayoutItem>
                     <dx:LayoutItem Caption="">
                         <LayoutItemNestedControlCollection>
                             <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">
                                 <dx:ASPxButton ID="btnLoc" runat="server" OnClick="btnLoc_Click" Text="Lọc">
                                     <Image IconID="filter_multiplemasterfilter_32x32">
                                     </Image>
                                 </dx:ASPxButton>
                             </dx:LayoutItemNestedControlContainer>
                         </LayoutItemNestedControlCollection>
                     </dx:LayoutItem>
                     <dx:LayoutItem Caption="Hiển Thị">
                         <LayoutItemNestedControlCollection>
                             <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server">
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
            </dx:LayoutGroup>
        </Items>
      </dx:ASPxFormLayout> 
     <dx:ASPxGridView ID="gridDonDatHang" runat="server" AutoGenerateColumns="False" KeyFieldName="ID" Width="100%" OnHtmlRowPrepared="gridDonDatHang_HtmlRowPrepared">
        <SettingsEditing Mode="PopupEditForm">
        </SettingsEditing>
        <Settings AutoFilterCondition="Contains" ShowFilterRow="True" ShowTitlePanel="True" />
        <SettingsBehavior ConfirmDelete="True" />
        <SettingsCommandButton RenderMode="Image">
            <ShowAdaptiveDetailButton ButtonType="Image">
            </ShowAdaptiveDetailButton>
            <HideAdaptiveDetailButton ButtonType="Image">
            </HideAdaptiveDetailButton>
            <NewButton>
                <Image IconID="actions_add_16x16" ToolTip="Thêm">
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
        <SettingsText CommandDelete="Xóa" CommandEdit="Sửa" CommandNew="Thêm" ConfirmDelete="Bạn có chắc chắn muốn xóa không?" PopupEditFormCaption="Thông tin đơn vị tính" Title="DANH SÁCH ĐƠN ĐẶT HÀNG" EmptyDataRow="Danh sách đơn hàng trống." SearchPanelEditorNullText="Nhập thông tin cần tìm..." />
         <Columns>
             <dx:GridViewDataTextColumn Caption="Số Đơn Hàng" FieldName="SoDonHang" VisibleIndex="0">
                 <DataItemTemplate>
                    <a href="javascript:void(0);" title="Xem chi tiết" onclick="OnMoreInfoClick(this, '<%# Container.KeyValue %>')"> <%# Eval("SoDonHang") %> </a>
                </DataItemTemplate>
                 <HeaderStyle Wrap="True" />
             </dx:GridViewDataTextColumn>
             <dx:GridViewDataTextColumn Caption="Ghi Chú" FieldName="GhiChu" VisibleIndex="6">
             </dx:GridViewDataTextColumn>
             <dx:GridViewDataComboBoxColumn Caption="Người Lập" FieldName="IDNguoiLap" VisibleIndex="1">
                 <PropertiesComboBox DataSourceID="SqlNguoiDung" TextField="TenNguoiDung" ValueField="ID">
                 </PropertiesComboBox>
                 <HeaderStyle Wrap="True" />
             </dx:GridViewDataComboBoxColumn>
             <dx:GridViewDataDateColumn Caption="Ngày Lập" FieldName="NgayLap" VisibleIndex="2">
                 <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy ">
                 </PropertiesDateEdit>
             </dx:GridViewDataDateColumn>
             <dx:GridViewDataSpinEditColumn Caption="Tổng Trọng Lượng" FieldName="TongTrongLuong" VisibleIndex="5">
                 <PropertiesSpinEdit DisplayFormatString="{0:N0}KG" NumberFormat="Custom">
                 </PropertiesSpinEdit>
                 <HeaderStyle Wrap="True" />
             </dx:GridViewDataSpinEditColumn>
             <dx:GridViewDataComboBoxColumn Caption="Trạng Thái" FieldName="TrangThai" VisibleIndex="12">
                 <PropertiesComboBox>
                     <Items>
                         <dx:ListEditItem Text="Chưa xử lý" Value="0" />
                         <dx:ListEditItem Text="Đã xử lý" Value="1" />
                     </Items>
                 </PropertiesComboBox>
                 <HeaderStyle Wrap="True" />
             </dx:GridViewDataComboBoxColumn>
             <%--<dx:GridViewDataButtonEditColumn Caption="Xem Chi Tiết" VisibleIndex="13">
                
                <DataItemTemplate>
                    <a href="javascript:void(0);" onclick="OnMoreInfoClick(this, '<%# Container.KeyValue %>')">Xem </a>
                </DataItemTemplate>
                 <HeaderStyle Wrap="True" />
            </dx:GridViewDataButtonEditColumn>--%>
             <dx:GridViewDataDateColumn Caption="Ngày Đặt" FieldName="NgayDat" VisibleIndex="3">
                 <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy ">
                 </PropertiesDateEdit>
             </dx:GridViewDataDateColumn>
             <dx:GridViewDataDateColumn Caption="Ngày Giao Dự Kiến" FieldName="NgayGiaoDuKien" VisibleIndex="4">
                 <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy ">
                 </PropertiesDateEdit>
                 <HeaderStyle Wrap="True" />
             </dx:GridViewDataDateColumn>
             <dx:GridViewDataComboBoxColumn Caption="Mức Độ Ưu Tiên" FieldName="MucDoUuTien" VisibleIndex="7">
                 <PropertiesComboBox>
                     <Items>
                         <dx:ListEditItem Text="Không Ưu Tiên" Value="0" />
                         <dx:ListEditItem Text="Ưu Tiên" Value="1" />
                     </Items>
                 </PropertiesComboBox>
                 <HeaderStyle Wrap="True" />
             </dx:GridViewDataComboBoxColumn>
             <dx:GridViewDataComboBoxColumn Caption="Giám Đốc" FieldName="GiamDocDuyet" VisibleIndex="9">
                 <PropertiesComboBox DataSourceID="SqlNguoiDung" TextField="TenNguoiDung" ValueField="ID">
                 </PropertiesComboBox>
                 <HeaderStyle Wrap="True" />
             </dx:GridViewDataComboBoxColumn>
             <dx:GridViewDataComboBoxColumn Caption="Giám Sát" FieldName="GiamSatDuyet" VisibleIndex="11">
                 <PropertiesComboBox DataSourceID="SqlNguoiDung" TextField="TenNguoiDung" ValueField="ID">
                 </PropertiesComboBox>
                 <HeaderStyle Wrap="True" />
             </dx:GridViewDataComboBoxColumn>
             <dx:GridViewDataSpinEditColumn Caption="Tổng Tiền" FieldName="TongTien" VisibleIndex="8" CellStyle-Font-Bold="true">
                 <PropertiesSpinEdit DisplayFormatString="{0:N0}" NumberFormat="Custom">
                 </PropertiesSpinEdit>
             </dx:GridViewDataSpinEditColumn>
             <dx:GridViewDataComboBoxColumn Caption="Quản Lý" FieldName="KhoDuyet" VisibleIndex="10">
                 <PropertiesComboBox DataSourceID="SqlNguoiDung" TextField="TenNguoiDung" ValueField="ID">
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
     <asp:SqlDataSource ID="SqlKho" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenCuaHang] FROM [GPM_Kho] WHERE (([DaXoa] = @DaXoa) AND ([ID] = @ID))">
         <SelectParameters>
             <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
             <asp:SessionParameter Name="ID" SessionField="IDKho" Type="Int32" />
         </SelectParameters>
     </asp:SqlDataSource>
     <asp:SqlDataSource ID="SqlNguoiDung" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenNguoiDung] FROM [GPM_NguoiDung] WHERE ([DaXoa] = @DaXoa)">
         <SelectParameters>
             <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
         </SelectParameters>
     </asp:SqlDataSource>
    <dx:ASPxPopupControl ID="popup" runat="server" AllowDragging="True" AllowResize="True" 
         PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"  Width="1300px"
         Height="600px" FooterText="Thông tin chi tiết hàng hóa combo"
        HeaderText="Thông tin chi tiết đơn hàng" ClientInstanceName="popup" EnableHierarchyRecreation="True" CloseAction="CloseButton">
    </dx:ASPxPopupControl>
</asp:Content>
