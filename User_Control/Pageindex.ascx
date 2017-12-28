<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Pageindex.ascx.cs" Inherits="User_Control_Pageindex" %>
<div style="vertical-align: baseline; text-align: right; padding-right: 8px;">
    <%--Text="&gt;&gt;"--%><%--Text="&lt;&lt;"--%>
    <style>
        .hlink:link {
            color: black;
            background-color: transparent;
            text-decoration: none;
        }
    </style>
    <table>
        <tr>
            <asp:Repeater ID="repPageIndex" runat="server" OnItemCommand="repPageIndex_ItemCommand" OnItemDataBound="repPageIndex_ItemDataBound">
                <ItemTemplate>
                    <td style="padding-right: 4px;">
                        <asp:LinkButton ID="lnkPageIndex" CssClass="hlink" runat="server" CommandName="pi"></asp:LinkButton>
                    </td>
                </ItemTemplate>
            </asp:Repeater>
        </tr>
    </table>
</div>
