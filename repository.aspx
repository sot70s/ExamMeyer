<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="repository.aspx.cs" Inherits="repository" %>

<%@ Register Src="~/User_Control/Pageindex.ascx" TagPrefix="uc1" TagName="Pageindex" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="https://www.amcharts.com/lib/3/plugins/export/export.css" type="text/css" media="all" />
    <script type="text/javascript" src="https://www.amcharts.com/lib/3/amcharts.js"></script>
    <script type="text/javascript" src="https://www.amcharts.com/lib/3/serial.js"></script>
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="padding-left: 150px; padding-right: 150px; padding-top: 40px;">
        <div style="height: 450px; width: 100%;" id="chartdiv"></div>
        <br />

        <table>
            <tr>
                <td style="padding-left: 15px;" colspan="3">
                    <asp:Label Font-Size="12" ForeColor="#808080" runat="server" ID="lbUser"></asp:Label>
                    <div style="color: #999999;">Repositories on Github</div>
                </td>
            </tr>
            <tr>
                <td style="padding-left: 15px; padding-bottom: 5px; width: 70px;">
                    <div runat="server" id="lbPage" visible="false">Pages :</div>
                </td>
                <td style="padding-bottom: 5px;">
                    <uc1:Pageindex runat="server" ID="ViewPageIndex" />
                </td>
                <td style="padding-bottom: 5px;">
                    <asp:HyperLink runat="server" ID="hplBack" CssClass="btn btn-danger btn-xs" Text="BACK"></asp:HyperLink><%-- NavigateUrl="index.aspx"--%>
                </td>
            </tr>
        </table>

        <asp:Repeater runat="server" ID="rptRepo" OnItemDataBound="rptRepo_ItemDataBound">
            <ItemTemplate>
                <div class="col-sm-4" style="padding-bottom: 20px;">
                    <div style="border: 1px solid #cccccc; padding: 15px; background-color: #ffffff">
                        <table style="width: 100%;">
                            <tr>
                                <td colspan="2" style="height: 90px; vertical-align: top;">
                                    <asp:HyperLink runat="server" Font-Size="12" ID="hlinkRepo" ForeColor="#808080" NavigateUrl='<%#Eval("clone_url") %>' Text='<%#Eval("repo_name") %>' Target="_blank"></asp:HyperLink><br />
                                    <asp:Label runat="server" Font-Size="9" ForeColor="#999999" Text='<%#Eval("desc") %>'></asp:Label><br />
                                    <asp:Label runat="server" Font-Size="8" ForeColor="red" Text='<%#Eval("language") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid #cccccc; text-align: center;">
                                    <div runat="server" style="font-size: small; color: #808080;">STARS</div>
                                    <asp:Label runat="server" Font-Size="12" ForeColor="#999999" Text='<%#Eval("star_count") %>'></asp:Label>
                                </td>
                                <td style="border: 1px solid #cccccc; text-align: center;">
                                    <div runat="server" style="font-size: small; color: #808080;">VIEWS</div>
                                    <asp:Label runat="server" Font-Size="12" ForeColor="#999999" Text='<%#Eval("watchers_count") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:HiddenField runat="server" ID="hdnLogin" Value='<%#Eval("login") %>' />
                                    <asp:HiddenField runat="server" ID="hdnRepoName" Value='<%#Eval("repo_name") %>' />
                                    <asp:HiddenField runat="server" ID="hdnBranch" Value='<%#Eval("default_branch") %>' />
                                    <asp:HyperLink runat="server" ID="hplDownload" CssClass="btn btn-primary" Text="Download" Width="100%" Target="_blank"></asp:HyperLink>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>

