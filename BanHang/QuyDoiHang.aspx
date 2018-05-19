<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="QuyDoiHang.aspx.cs" Inherits="BanHang.QuyDoiHang" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
    <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Width="100%">
        <Items>
            <dx:LayoutGroup Caption="" ColCount="3" Width="800px">
                <Items>
                    <dx:LayoutItem Caption="Hàng Hóa 1">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxComboBox ID="cmbHH1" runat="server" ValueType="System.String" 
                                        DropDownWidth="600" DropDownStyle="DropDown" 
                                        ValueField="ID"
                                        NullText="Nhập Barcode hoặc mã hàng ......." TextFormatString="{0} - {1}"
                                        EnableCallbackMode="true" CallbackPageSize="10" DataSourceID="sqlHH" 
                                        
                                        >                                    
                                        <Columns>
                                            <dx:ListBoxColumn FieldName="MaHang" Width="80px" Caption="Mã Hàng" />
                                            <dx:ListBoxColumn FieldName="TenHangHoa" Width="250px" Caption="Tên Hàng Hóa"/>
                                        </Columns>
                                        <DropDownButton Visible="False">
                                        </DropDownButton>
                                    </dx:ASPxComboBox>
                                <asp:SqlDataSource ID="sqlHH" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [MaHang], [TenHangHoa] FROM [GPM_HangHoa] WHERE (([DaXoa] = @DaXoa) AND ([IDHangQuyDoi] = @IDHangQuyDoi))">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="0" Name="DaXoa" Type="Int32" />
                                        <asp:Parameter DefaultValue="0" Name="IDHangQuyDoi" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="Hàng Hóa 2">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxComboBox ID="cmbHH2" runat="server" ValueType="System.String" 
                                        DropDownWidth="600" DropDownStyle="DropDown" 
                                        ValueField="ID"
                                        NullText="Nhập Barcode hoặc mã hàng ......." TextFormatString="{0} - {1}"
                                        EnableCallbackMode="true" CallbackPageSize="10" DataSourceID="sqlHH" 
                                        
                                        >                                    
                                        <Columns>
                                            <dx:ListBoxColumn FieldName="MaHang" Width="80px" Caption="Mã Hàng" />
                                            <dx:ListBoxColumn FieldName="TenHangHoa" Width="250px" Caption="Tên Hàng Hóa"/>
                                        </Columns>
                                        <DropDownButton Visible="False">
                                        </DropDownButton>
                                    </dx:ASPxComboBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxButton ID="btnSave" runat="server" Text="Lưu" OnClick="btnSave_Click">
                                    <Image IconID="export_export_16x16">
                                    </Image>
                                </dx:ASPxButton>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
            <dx:LayoutGroup Caption="Danh sách hàng hóa">
                <Items>
                    <dx:LayoutItem Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer runat="server">
                                <dx:ASPxGridView ID="gridDanhSach" runat="server" AutoGenerateColumns="False" KeyFieldName="ID" Width="100%">
                                    <SettingsPager Mode="EndlessPaging" PageSize="200">
                                    </SettingsPager>
                                    <Settings ShowFilterRow="True" />
                                    <SettingsCommandButton>
                                        <ShowAdaptiveDetailButton ButtonType="Image">
                                        </ShowAdaptiveDetailButton>
                                        <HideAdaptiveDetailButton ButtonType="Image">
                                        </HideAdaptiveDetailButton>
                                    </SettingsCommandButton>
                                    <Columns>
                                        <dx:GridViewDataComboBoxColumn Caption="Hàng Hóa 1" FieldName="ID" ShowInCustomizationForm="True" VisibleIndex="0">
                                            <PropertiesComboBox DataSourceID="sqlHangHoa" TextField="TenHangHoa" ValueField="ID">
                                            </PropertiesComboBox>
                                        </dx:GridViewDataComboBoxColumn>
                                        <dx:GridViewDataComboBoxColumn Caption="Hàng Quy Đổi" FieldName="IDHangQuyDoi" ShowInCustomizationForm="True" VisibleIndex="4">
                                            <PropertiesComboBox DataSourceID="sqlHangHoa" TextField="TenHangHoa" ValueField="ID">
                                            </PropertiesComboBox>
                                        </dx:GridViewDataComboBoxColumn>
                                        <dx:GridViewDataTextColumn Caption="Mã Hàng" FieldName="MaHang" ShowInCustomizationForm="True" VisibleIndex="1">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn Caption="Tên Hang" FieldName="TenHangHoa" ShowInCustomizationForm="True" VisibleIndex="2">
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                </dx:ASPxGridView>
                                <asp:SqlDataSource ID="sqlHangHoa" runat="server" ConnectionString="<%$ ConnectionStrings:BanHangConnectionString %>" SelectCommand="SELECT [ID], [TenHangHoa] FROM [GPM_HangHoa]"></asp:SqlDataSource>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                </Items>
            </dx:LayoutGroup>
        </Items>
        <SettingsItems HorizontalAlign="Center" Width="100%" />
    </dx:ASPxFormLayout>
</asp:Content>
