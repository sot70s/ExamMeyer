<%@ Page Title="Exam Meyer" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="_index" %>

<%@ Register Src="~/User_Control/Pageindex.ascx" TagPrefix="uc1" TagName="Pageindex" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="Content/font-awesome.css" rel="stylesheet" />
    <style>
        .avatar {
            border-radius: 50%;
            width: 40px;
            height: 40px;
        }
    </style>
    <div style="padding-left: 150px; padding-right: 150px; padding-top: 40px">
        <div class="row">
            <div class="col-sm-12">
                <div class="row">
                    <div class="col-sm-5">
                        <div style="float:right;">
                            <img style="width: 400px;" src="image/git.png" />
                        </div>
                </div>
                <div class="col-sm-7">
                    <div style="text-align: left;">
                        <p style="font-size: xx-large;"><b>Quiz using GitHub API v3</b></p>
                        <p style="font-size: small;">Developed by <span style="color: red;"><b>Supachai Sittavasri</b></span></p>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width:270px;">
                                    <asp:TextBox Width="100%" CssClass="form-control" runat="server" ID="txtSearch"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button CssClass="btn btn-info" runat="server" ID="btnSearch" Text="SEARCH" OnClick="btnSearch_Click" />
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div style="font-size: small; color: red;" runat="server" id="divValid" visible="false">
                                        File not found.
                                    </div>
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>


        <table>
            <tr>
                <td style="padding-left: 15px; padding-bottom: 5px;">
                    <div runat="server" id="lbPage" visible="false">Pages :</div>
                </td>
                <td style="padding-bottom: 5px;">
                    <uc1:Pageindex runat="server" ID="ViewPageIndex" />
                </td>
            </tr>
        </table>

        <asp:Repeater runat="server" ID="rptUser">
            <ItemTemplate>
                <div class="col-sm-4" style="padding-bottom: 20px;">
                    <div style="border: 1px solid #cccccc; padding: 15px; background-color: #ffffff">
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 50px;">
                                    <asp:Image runat="server" CssClass="avatar" ImageUrl='<%#Eval("avatar") %>' />
                                </td>
                                <td style="padding-left: 5px; text-align: left;">
                                    <asp:Label runat="server" Font-Size="12" ForeColor="#999999" Text='<%#Eval("user") %>'></asp:Label>&nbsp;
                                            <asp:HyperLink runat="server" NavigateUrl='<%#Eval("url") %>' Target="_blank"><i class="fa fa-external-link" aria-hidden="true"></i></asp:HyperLink>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center;">
                                    <div style="text-align: right">
                                        <span style="color: #999999; font-size: smaller;">Score : <%#Eval("score") %></span>
                                    </div>

                                    <hr />
                                    <asp:HyperLink runat="server" Text="VIEW REPOSITORY" NavigateUrl='<%# "~/repo/" + Eval("user") %>'></asp:HyperLink>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    </div>
</asp:Content>
