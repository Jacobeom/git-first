<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TwitterIssueDetailUserControl.ascx.cs" Inherits="TID.PDI.TwitterIssueDetail.TwitterIssueDetail.TwitterIssueDetailUserControl" %>
<%@ Assembly Name="TwitterAvailability, Version=1.0.0.0, Culture=neutral, PublicKeyToken=69f6e8d0173f365a" %>
<%@ Import Namespace="TwitterAvailability.Dto" %>

<Sharepoint:CssLink runat="server" />
<SharePoint:CssRegistration ID="CssRegistration1" runat="server"  Name="<% $SPUrl:~SiteCollection/Style Library/TID.PDI.TwitterIssueDetail/Styles/Detail.css%>"  />
<SharePoint:CssRegistration ID="CssRegistration2" runat="server" Name="<% $SPUrl:~SiteCollection/Style Library/TID.PDI.TwitterIssueDetail/Styles/jquery-ui.css%>" />
<SharePoint:CssRegistration ID="CssRegistration3" runat="server" Name="<% $SPUrl:~SiteCollection/Style Library/TID.PDI.TwitterIssueDetail/Styles/jquery-ui-timepicker-addon.css%>" />

<SharePoint:ScriptLink ID="ScriptLink1" runat="server" Name="~sitecollection/Style Library/TID.PDI.TwitterIssueDetail/Scripts/Detail.js" Localizable="False"/>
<SharePoint:ScriptLink ID="ScriptLink3" runat="server" Name="~sitecollection/Style Library/TID.PDI.TwitterIssueDetail/Scripts/jquery-1.8.3.js" Localizable="False"/>
<SharePoint:ScriptLink ID="ScriptLink2" runat="server" Name="~sitecollection/Style Library/TID.PDI.TwitterIssueDetail/Scripts/jquery-ui-1.9.2.custom.js" Localizable="False"/>
<SharePoint:ScriptLink ID="ScriptLink4" runat="server" Name="~sitecollection/Style Library/TID.PDI.TwitterIssueDetail/Scripts/jquery-ui-timepicker-addon.js" Localizable="False"/>

<script type="text/javascript">
    
    $(document).ready(function () {
        $('#<%= this.DateTime.ClientID %>').datetimepicker({
                dateFormat: "dd/mm/yy",
                timeFormat: 'HH:mm'
            }
        );
        $('#<%= this.DateTime.ClientID %>').datetimepicker('setDate', (new Date()));
    });

</script>
<div id="udodet_viewer">
    
<p style="color:red" runat="server" id="error_p" visible="false" > 
        There has been an error loading data.
</p>

<div align="center">
<a href="<%=string.Format(UDOUrl,HistoryTwitterIssues[0].IntenalId.Split('.')[0]) %>" style=" alignment-adjust: central" target="_blank"><h3> UDO  Details Link </h3> </a>
</div>

<asp:Repeater id="TwitterHistoryRepeater" runat="server">
 <HeaderTemplate>
    <table class="udo_details" visible="true" cellspacing="0" border="0">
    <tr>
        <th style="width: 20%">Publish Date</th>
        <th style="width: 20%">Effective Date</th>
        <th style="width: 60%">Message</th>
    </tr>     
 </HeaderTemplate>
 <ItemTemplate>
     <tr class="row_odd">
        <td> <%# ((TwitterIssue)Container.DataItem).TwitterDate.ToString("dd/MM/yyyy hh:mm") %> </td>
        <td> <%# ((TwitterIssue)Container.DataItem).EffectiveDate.Value.ToString("dd/MM/yyyy hh:mm") %> </td>
        <td> <%# ((TwitterIssue)Container.DataItem).OriginalMessage %> </td>
     </tr>
 </ItemTemplate>   
 <AlternatingItemTemplate>
     <tr class="row_even">
        <td> <%# ((TwitterIssue)Container.DataItem).TwitterDate.ToString("dd/MM/yyyy hh:mm") %> </td>
        <td> <%# ((TwitterIssue)Container.DataItem).EffectiveDate.Value.ToString("dd/MM/yyyy hh:mm") %> </td>
        <td> <%# ((TwitterIssue)Container.DataItem).OriginalMessage %> </td>
     </tr>
 </AlternatingItemTemplate>   
 <FooterTemplate>
    </table>
 </FooterTemplate>  
</asp:Repeater>

<asp:Repeater id="IssuesDetailsRepeater" runat="server" OnItemCommand="IssueDetailsRepeaterItemCommand">
<HeaderTemplate>
    
<table  class="udo_details" cellspacing="0" border="0">
    <tr>
        <th style="width: 20%">Date</th>
        <th style="width: 70%">Description</th>
        <th style="width: 5%">File</th>
        <th style="width: 5%"></th>
    </tr>
 </HeaderTemplate>
 <ItemTemplate>
    <tr class="row_odd">
        <td> <%# ((IssueDetail)Container.DataItem).Date.ToString("dd/MM/yyyy hh:mm") %> </td>
        <td> <%# ((IssueDetail)Container.DataItem).Description %> </td>
        <td> <asp:Button ID="Button1" CssClass="dwld_detailfile"
                         runat="server"  UseSubmitBehavior="False" CommandName="Download" 
                         Visible="<%# !string.IsNullOrEmpty( ((IssueDetail)Container.DataItem).FileName)  %>"
                         CommandArgument = "<%# ((IssueDetail)Container.DataItem).Id.Value  %>"
                         ToolTip="<%#  !string.IsNullOrEmpty(((IssueDetail)Container.DataItem).FileName) ? ((IssueDetail)Container.DataItem).FileName : string.Empty   %>"/>
                         
        </td>
        <td> <asp:Button ID="Button2" CssClass="del_detail_bt"
                         runat="server" UseSubmitBehavior="False" CommandName="Delete"  
                         CommandArgument = "<%# ((IssueDetail)Container.DataItem).Id.Value  %>"
                         OnClientClick="javascript:if(!confirm('Are you sure you want to delete the item?'))return false;"/>
                         
        </td>
    </tr>
 </ItemTemplate>
 <AlternatingItemTemplate>
    <tr class="row_even">
        <td> <%# ((IssueDetail)Container.DataItem).Date.ToString("dd/MM/yyyy hh:mm") %> </td>
        <td> <%# ((IssueDetail)Container.DataItem).Description %> </td>
        <td> <asp:Button ID="Button2" CssClass="dwld_detailfile"
                         runat="server" UseSubmitBehavior="False" CommandName="Download" 
                         Visible="<%# !string.IsNullOrEmpty( ((IssueDetail)Container.DataItem).FileName)  %>"
                         CommandArgument = "<%# ((IssueDetail)Container.DataItem).Id.Value  %>"
                         ToolTip="<%#  string.IsNullOrEmpty(((IssueDetail)Container.DataItem).FileName) ? ((IssueDetail)Container.DataItem).FileName : string.Empty   %>"/>
                         
        </td>
        <td> <asp:Button ID="Button1" CssClass="del_detail_bt"
                         runat="server" UseSubmitBehavior="False" CommandName="Delete"  
                         CommandArgument = "<%# ((IssueDetail)Container.DataItem).Id.Value  %>"
                         OnClientClick="javascript:if(!confirm('Are you sure you want to delete the item?'))return false;"/>
                         
        </td>
    </tr>
 </AlternatingItemTemplate>
  <FooterTemplate>
    </table>
 </FooterTemplate>  
</asp:Repeater>


 <div id="edition_form" class="newdetail_form" runat="server" visible="True">
        <h4 class="title_newdetail">New detail</h4>
    
        <asp:TextBox ID="DateTime" runat="server"/>       
        <asp:TextBox ID="Description" CssClass="description_box" runat="server" TextMode="MultiLine"></asp:TextBox>
        <asp:FileUpload ID="FileUpload" runat="server" />
        <div class="buttons_cnt">
            <input id="CloseButton" type="button" runat="server" class="bt_close" value="Close" onclick="javascript:window.frameElement.commitPopup();" />            
            <asp:Button ID="AddItemButton" runat="server" CssClass="bt_newdetail" Text="Add" OnClick="AddItem" />
        </div>
        <div style="clear:both"></div>        
    </div>
</div>