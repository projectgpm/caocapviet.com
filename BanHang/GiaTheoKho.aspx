<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="GiaTheoKho.aspx.cs" Inherits="BanHang.GiaTheoVung" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <dx:ASPxFormLayout ID="LayoutGiaTheoVung" runat="server" Width="100%" ColCount="2">
        <Items>

            <dx:LayoutGroup Caption="Danh sách các chi nhánh" Width="100%" RowSpan="10" ColSpan="2">

                <Items>
                    <dx:LayoutItem Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxComboBox ID="cmbVung" runat="server" AutoPostBack="True"
                                     OnSelectedIndexChanged="cmbVung_SelectedIndexChanged" Width="50%"
                                     
                                    >
                                     <%--<Columns>
                                        <dx:ListBoxColumn FieldName="MaVung" Width="100px" Caption="Mã Vùng" />
                                        <dx:ListBoxColumn FieldName="TenVung" Width="200px" Caption="Tên Vùng"/>
                                    </Columns>--%>
                                </dx:ASPxComboBox>

                                <br />
                                <dx:ASPxCheckBoxList ID="DanhSachKho" runat="server" RepeatColumns="5" Width="100%">
                                </dx:ASPxCheckBoxList>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="" RowSpan="10">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
                                <asp:SqlDataSource ID="sqlKho" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenCuaHang], [MaKho],[DiaChi] FROM [GPM_Kho] WHERE ([DaXoa] = @DaXoa)">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>

            </dx:LayoutGroup>
            <dx:LayoutGroup Caption="Thông tin" ColCount="2" Width="100%" ColSpan="2">
                
                <Items>
                    <dx:LayoutItem Caption="Nhập Mã Hàng(*)">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">
                                <dx:ASPxTextBox ID="txtMaHang" runat="server" Width="100%">
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server">
                        <dx:ASPxButton ID="btnThem" runat="server" OnClick="btnThem_Click" Text="Thêm">
                            <Image IconID="actions_add_32x32">
                            </Image>
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
                </Items>
                
            </dx:LayoutGroup>

            <dx:LayoutItem Caption="" ColSpan="2" Width="100%">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxGridView ID="gridHangHoa" runat="server" AutoGenerateColumns="False" ClientInstanceName="gridHangHoa" KeyFieldName="ID" OnHtmlRowPrepared="gridHangHoa_HtmlRowPrepared" OnRowDeleting="gridHangHoa_RowDeleting" OnRowUpdating="gridHangHoa_RowUpdating" Width="100%">
                            <ClientSideEvents EndCallback="function(s, e) {
                                                console.log(s.cpUpdated);
                                                if(s.cpUpdated == true)
                                                {
                                                    alert('Cập nhật giá thành công!');
                                                }	
                                            }" />
                            <SettingsPager Mode="ShowAllRecords" NumericButtonCount="20" Visible="False">
                            </SettingsPager>
                            <SettingsEditing Mode="Batch">
                            </SettingsEditing>
                            <Settings ShowTitlePanel="True" />
                            <SettingsCommandButton>
                                <ShowAdaptiveDetailButton ButtonType="Image">
                                </ShowAdaptiveDetailButton>
                                <HideAdaptiveDetailButton ButtonType="Image">
                                </HideAdaptiveDetailButton>
                                <DeleteButton>
                                    <Image IconID="actions_cancel_16x16">
                                    </Image>
                                </DeleteButton>
                            </SettingsCommandButton>
                            <SettingsText CommandBatchEditCancel="Hủy tất cả" CommandBatchEditUpdate="Lưu tất cả" CommandDelete="Xóa" EmptyDataRow="Danh sách hàng hóa rỗng" SearchPanelEditorNullText="Nhập thông tin cần tìm" Title="DANH SÁCH HÀNG HÓA" />
                            <Columns>
                                <dx:GridViewCommandColumn ShowDeleteButton="True" ShowInCustomizationForm="True" VisibleIndex="11">
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataComboBoxColumn Caption="ĐVT" FieldName="IDDonViTinh" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="2">
                                    <PropertiesComboBox DataSourceID="sqlDonViTinh" TextField="TenDonViTinh" ValueField="ID">
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataSpinEditColumn Caption="Giá Bán" FieldName="GiaBan" ShowInCustomizationForm="True" VisibleIndex="4">
                                    <PropertiesSpinEdit DisplayFormatString="{0:N0}" NumberFormat="Custom">
                                    </PropertiesSpinEdit>
                                </dx:GridViewDataSpinEditColumn>
                                <dx:GridViewDataSpinEditColumn Caption="Giá Bán 1" FieldName="GiaBan1" ShowInCustomizationForm="True" VisibleIndex="5">
                                    <PropertiesSpinEdit DisplayFormatString="{0:N0}" NumberFormat="Custom">
                                    </PropertiesSpinEdit>
                                </dx:GridViewDataSpinEditColumn>
                                <dx:GridViewDataTextColumn Caption="Mã Hàng" FieldName="MaHang" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="0">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataSpinEditColumn Caption="Giá Bán 2" FieldName="GiaBan2" ShowInCustomizationForm="True" VisibleIndex="6">
                                    <PropertiesSpinEdit DisplayFormatString="{0:N0}" NumberFormat="Custom">
                                    </PropertiesSpinEdit>
                                </dx:GridViewDataSpinEditColumn>
                                <dx:GridViewDataSpinEditColumn Caption="Giá Bán 3" FieldName="GiaBan3" ShowInCustomizationForm="True" VisibleIndex="7">
                                    <PropertiesSpinEdit DisplayFormatString="{0:N0}" NumberFormat="Custom">
                                    </PropertiesSpinEdit>
                                </dx:GridViewDataSpinEditColumn>
                                <dx:GridViewDataSpinEditColumn Caption="Giá Bán 4" FieldName="GiaBan4" ShowInCustomizationForm="True" VisibleIndex="8">
                                    <PropertiesSpinEdit DisplayFormatString="{0:N0}" NumberFormat="Custom">
                                    </PropertiesSpinEdit>
                                </dx:GridViewDataSpinEditColumn>
                                <dx:GridViewDataSpinEditColumn Caption="Giá Bán 5" FieldName="GiaBan5" ShowInCustomizationForm="True" VisibleIndex="9">
                                    <PropertiesSpinEdit DisplayFormatString="{0:N0}" NumberFormat="Custom">
                                    </PropertiesSpinEdit>
                                </dx:GridViewDataSpinEditColumn>
                                <dx:GridViewDataComboBoxColumn Caption="Tên Hàng Hóa" FieldName="IDHangHoa" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="1">
                                    <PropertiesComboBox DataSourceID="SqlDanhSachHangHoa" TextField="TenHangHoa" ValueField="ID">
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataComboBoxColumn Caption="Chi Nhánh" FieldName="IDKho" ShowInCustomizationForm="True" VisibleIndex="10">
                                    <PropertiesComboBox DataSourceID="sqlKho" TextField="TenCuaHang" ValueField="ID">
                                    </PropertiesComboBox>
                                </dx:GridViewDataComboBoxColumn>
                                <dx:GridViewDataSpinEditColumn Caption="GIá Mua" FieldName="GiaMuaSauThue" ReadOnly="True" ShowInCustomizationForm="True" VisibleIndex="3">
                                    <PropertiesSpinEdit DisplayFormatString="N0" NumberFormat="Custom">
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
                        <asp:SqlDataSource ID="SqlDanhSachHangHoa" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenHangHoa] FROM [GPM_HangHoa] WHERE (([DaXoa] = @DaXoa) AND ([TenHangHoa] IS NOT NULL))">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <asp:SqlDataSource ID="sqlDonViTinh" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenDonViTinh] FROM [GPM_DonViTinh] WHERE ([DaXoa] = @DaXoa)">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="" HorizontalAlign="Right">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxButton ID="btnLuu" runat="server" OnClick="btnLuu_Click" Text="Lưu">
                            <Image IconID="actions_apply_32x32">
                            </Image>
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>
            <dx:LayoutItem Caption="">
                <LayoutItemNestedControlCollection>
                    <dx:LayoutItemNestedControlContainer runat="server">
                        <dx:ASPxButton ID="btnHuy" runat="server" OnClick="btnHuy_Click" Text="Hủy">
                            <Image IconID="actions_cancel_32x32">
                            </Image>
                        </dx:ASPxButton>
                    </dx:LayoutItemNestedControlContainer>
                </LayoutItemNestedControlCollection>
            </dx:LayoutItem>

        </Items>
    </dx:ASPxFormLayout>
    <asp:HiddenField ID="ID_temp" runat="server" />
</asp:Content>
