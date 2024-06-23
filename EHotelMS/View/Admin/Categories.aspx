<%@ Page Title="" Language="C#" MasterPageFile="~/View/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Categories.aspx.cs" Inherits="EHotelMS.View.Admin.Categories" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MyBody" runat="server">
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-5"></div>
            <div class="col-md-5"></div>
            <div class="col-md-2">
                <label id="LogUser" runat="server" class="text-success"></label>
            </div>
        </div>
        <div class="row">
            <div class="col-4"></div>
            <div class="col-4">
                <h1 class="text-success text-center">Room Category Management</h1>
            </div>
            <div class="col-4"></div>
        </div>
        <div class="row">
            <div class="col-md-3">
                <form>
                    <div class="mb-3">
                        <label for="CatNameTb" class="form-label">Category name</label>
                        <input type="text" class="form-control" id="CatNameTb" runat="server">
                    </div>
                    <div class="mb-3">
                        <label for="RemarkTb" class="form-label">RemarkTb</label>
                        <input type="text" class="form-control" id="RemarkTb" runat="server">
                    </div>
                    <div class="row">
                        <div class="col d-grid">
                            <asp:Button ID="EditBtn" runat="server" Text="Edit selected" class="btn btn-warning btn-block" OnClick="EditBtn_Click" />
                        </div>
                        <div class="col d-grid">
                            <asp:Button ID="DeleteBtn" runat="server" Text="Delete selected" class="btn btn-danger btn-block" OnClick="DeleteBtn_Click" />
                        </div>
                    </div>
                    <br />
                    <div class="d-grid">
                        <asp:Button ID="SaveBtn" runat="server" Text="Save to new" class="btn btn-success btn-block" OnClick="SaveBtn_Click" />
                        <label id="ErrMsg" runat="server" class="text-danger"></label>
                    </div>
                </form>
            </div>
            <div class="col-md-9">
                <asp:GridView ID="CategoriesGV" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" OnSelectedIndexChanged="CategoriesGV_SelectedIndexChanged" BorderStyle="Solid" BorderWidth="2px" BorderColor="#41A666">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" Text="Select"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#41a666" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BorderStyle="Solid" BorderWidth="2px" BorderColor="#042611" Font-Bold="True" ForeColor="#333333" BackColor="lightgray" />
                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
