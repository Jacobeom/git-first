<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="System.ComponentModel" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Assembly Name="TwitterAvailability, Version=1.0.0.0, Culture=neutral, PublicKeyToken=69f6e8d0173f365a" %>
<%@ Import Namespace="TwitterAvailability.Dto" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TwitterIssueUserControl.ascx.cs" Inherits="TID.PDI.TwitterAvailabilityPanel.ControlTemplates.TID.PDI.TwitterAvailabilityPanel.TwitterIssueUserControl" %>


<asp:Repeater runat="server" id="TwitterIssueRepeater" OnItemDataBound="TwitterIssueRepeaterItemBound">
<HeaderTemplate>
</HeaderTemplate>
<ItemTemplate>
    <td class="status" style="background-color: <%#  BackgroundColor(((DateTime)Container.DataItem))  %>;border-left: dotted 1px #64C3D6 " >
        <table width="100%" class="itemTable" style="height: 70px" >
            <tr style="height: 18px;border-bottom: dotted 1px #64C3D6" >
                <td align="left"> <asp:PlaceHolder ID="BTPPlaceHolder" runat="server"  /> </td>
            </tr>
            
             <tr style="height: 18px;border-bottom: dotted 1px #64C3D6" >
                <td align="left"> <asp:PlaceHolder ID="VBTPXPlaceHolder" runat="server" /> </td>
            </tr>
            
            <tr style="height: 18px;border-bottom: dotted 1px #64C3D6" >
                <td align="left"> <asp:PlaceHolder ID="TEFOPPlaceHolder" runat="server" />  </td>
            </tr>
        </table>
    </td>
</ItemTemplate>
<FooterTemplate>
</FooterTemplate>
</asp:Repeater>
