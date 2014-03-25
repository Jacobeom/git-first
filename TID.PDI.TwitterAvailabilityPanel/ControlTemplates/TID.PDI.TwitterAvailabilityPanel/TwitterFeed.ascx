<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TwitterFeed.ascx.cs" Inherits="TID.PDI.TwitterAvailabilityPanel.ControlTemplates.TID.PDI.TwitterAvailabilityPanel.TwitterFeed" %>
<%@ Import Namespace="TID.PDI.TwitterAvailabilityPanel.ControlTemplates.TID.PDI.TwitterAvailabilityPanel" %>

<style type="text/css">
    .apiResponse  
    {
        overflow-x: hidden;
        overflow-y: scroll;    
    }
</style>

<div id="TwitterFeedWrapper">
<table id="TwitterTable" width="100%"  cellspacing="0" cellpadding="2" border="0">
    <tbody>
    <tr>
        <td class="twitterLogo">
            <img width="26" height="24" alt="" src="<%= TwitterImage %>" />
         </td>
        <td class="twitterTitle">
            <a target="_blank" href="https://twitter.com/#!/tdpdikpi"> @tdpdikpi</a>
        </td>
    </tr>
    <tr>
    <td id="twitterBox" class="twitterContent" colspan="2">
        <table width="100%" cellspacing="0" cellpadding="0" border="0">
            <tbody>
                <tr>
                    <td>
                    <div ID="apiResponse" class="apiResponse" runat="server" style="height: 100%">
                        <asp:Repeater id="tweetsRepeater" runat="server" >
                        <HeaderTemplate>
                            <table width='100%' cellpadding='0' cellspacing='5' border='0'>
                        </HeaderTemplate>
     
                         <ItemTemplate>
                             <tr style="background-color: <%# Container.ItemIndex % 2 == 0 ? "#E8F6F9" : "white" %>" >
                                 <td class='TweetDate'>
                                     <div style='float: left; width: 50%'><%# ((TweeterDto) Container.DataItem).CreatedDate.ToString("dd/MM/yyyy") %></div>
                                     <div style='float: left; width: 50%; text-align: right'><%# ((TweeterDto) Container.DataItem).CreatedDate.ToString("hh:mm") %></div>
                                 </td>
                             </tr>
                              <tr style="background-color: <%# Container.ItemIndex % 2 == 0 ? "#E8F6F9" : "white" %>"  >
                                  <td class='Tweet'>
                                      <%# ((TweeterDto) Container.DataItem).Text %>
                                  </td>
                              </tr>
                         </ItemTemplate>
        
                        <FooterTemplate>
                        </table>
                        </FooterTemplate>
                        </asp:Repeater>
                    </div>
                    </td>
                </tr>
            </tbody>
        </table>
    </td>
    </tr>
    <tr>
        <td class="twitterBar" colspan="2"></td>
    </tr>
    </tbody>
</table>
</div>