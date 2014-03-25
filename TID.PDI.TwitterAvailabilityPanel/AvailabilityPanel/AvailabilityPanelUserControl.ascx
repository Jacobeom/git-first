<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AvailabilityPanelUserControl.ascx.cs" Inherits="TID.PDI.TwitterAvailabilityPanel.AvailabilityPanel.AvailabilityPanelUserControl" %>
<%@ Import Namespace="TwitterAvailability.Dto" %>
<%@ Assembly Name="TwitterAvailability, Version=1.0.0.0, Culture=neutral, PublicKeyToken=69f6e8d0173f365a" %>
<%@ Register src="../TwitterFeed.ascx" tagname="TwitterFeed" tagprefix="uc1" %>


<SharePoint:CssRegistration ID="CssRegistration1" runat="server" Name="<% $SPUrl:~SiteCollection/Style Library/TID.PDI.TwitterAvailabilityPanel/Styles/AvailibilityPanel.css%>"  />
<SharePoint:CssRegistration ID="CssRegistration2" runat="server" Name="<% $SPUrl:~SiteCollection/Style Library/TID.PDI.TwitterAvailabilityPanel/Styles/jquery-ui-1.9.2.custom.css%>" />

<SharePoint:ScriptLink ID="ScriptLink1" runat="server" Name="~sitecollection/Style Library/TID.PDI.TwitterAvailabilityPanel/Scripts/AvailibilityPanel.js" Localizable="False"/>
<SharePoint:ScriptLink ID="ScriptLink2" runat="server" Name="~sitecollection/Style Library/TID.PDI.TwitterAvailabilityPanel/Scripts/jquery-ui-1.9.2.custom.js" Localizable="False"/>

<div style="width: 100%">

  <div class="title">Leyend</div>
  <table>
        <tr>
            <td><img class="red" width="18" height="18" alt="" src="<%= TwitterIssueTypeUrl(TwitterIssueType.TEFOP) %>"/> </td>
            <td>The service went down and continues down</td>
            <td><img class="red" width="18" height="18" alt="" src="<%= TwitterIssueTypeUrl(TwitterIssueType.BTP) %>"/>   </td>
            <td>A batch treatment plan is planned for some near date</td>
            <td><img class="red" width="18" height="18" alt="" src="<%= TwitterIssueTypeUrl(TwitterIssueType.VBTPX ) %>"/></td>
            <td>A batch treatment plan has already started its eXecution</td>
        </tr>
        <tr>
            <td><img class="red" width="18" height="18" alt="" src="<%= TwitterIssueTypeUrl(TwitterIssueType.TEFCL) %>"/> </td>
            <td>The service went down but the corrective actions were5 taken and brought it up</td>
            <td><img class="red" width="18" height="18" alt="" src="<%= TwitterIssueTypeUrl(TwitterIssueType.BTPH) %>"/>  </td>
            <td>A batch treatment already planned has been cHanged to another date</td>
            <td><img class="red" width="18" height="18" alt="" src="<%= TwitterIssueTypeUrl(TwitterIssueType.VBTPE ) %>"/></td>
            <td>A batch treatment plan has already started and Extended its estimated duration</td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td><img class="red" width="18" height="18" alt="" src="<%= TwitterIssueTypeUrl(TwitterIssueType.BTPC) %>"/>  </td>
            <td>A batch treatment already planned has been completely Cancelled</td>
            <td><img class="red" width="18" height="18" alt="" src="<%= TwitterIssueTypeUrl(TwitterIssueType.VBTPC) %>"/></td>
	        <td>A batch treatment plan already started has been completely Cancelled</td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td><img class="red" width="18" height="18" alt="" src="<%= TwitterIssueTypeUrl(TwitterIssueType.BTPD) %>"/></td>
            <td>A batch treatment plan has already endeD</td>
            <td><img class="red" width="18" height="18" alt="" src="<%= TwitterIssueTypeUrl(TwitterIssueType.VBTPD) %>"/></td>
            <td>A batch treatment plan has already endeD</td>    
        </tr>
   </table>
        
</div>

 <table>
        <tr>
        <td colspan="3">
              <div class="title">Service Availability Panel</div>
        </td> 
        </tr>
       <tr>
    
       <td style="width: 80%" id="panelTd">
           
           <div id="DashboardWrapper" style="width: 100%">

            <table width="100%" cellpadding="0" cellspacing="0" border="0">
            <tbody>
            <tr>
	            <td width="20%" valign="top">
	                <asp:Repeater id="productsRepeater" runat="server">
                    <HeaderTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" border="0" id="ProductNamesTable">
				        <thead>
					        <tr>
						        <th width="100%"></th>
					        </tr>
				        </thead>
				        <tbody>
	                </HeaderTemplate>
                    <ItemTemplate>
                    <tr productIndex = '<%# Container.ItemIndex+1 %>'>
                         <td width="100%" style="background-color: #E8F6F9"> <%# ((Product)Container.DataItem).ProductName %> </td>
                     </tr>
                    </ItemTemplate>
                     <AlternatingItemTemplate>
                     <tr productIndex = '<%# Container.ItemIndex+1 %>'>
                        <td width="100%"> <%# ((Product)Container.DataItem).ProductName %> </td>
                      </tr>
                    </AlternatingItemTemplate>
                    <FooterTemplate>
				        </tbody>
			        </table>
                    </FooterTemplate>
                    </asp:Repeater>
               </td>
               <td width="79%" valign="top">
                   <div id="DatesWrapper">
                       <div id="DatesCarrusel" >
                           <table id="DatesTable" width="100%" cellpadding="0" cellspacing="0" border="0" >
			        
                    
                              <asp:Repeater id="datesRepeater" runat="server">
                              <HeaderTemplate>
                                  <thead>
                                  <tr>
                              </HeaderTemplate>
                               <ItemTemplate>
                                    <th align="center"> <%# ((DateTime)Container.DataItem).ToString("dd/MM/yyyy") %> </th>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </tr>
                                    </thead>
                                </FooterTemplate>
                               </asp:Repeater>
                                <asp:Repeater id="productDatesRepeater" runat="server" OnItemDataBound="DatesRepeaterItemBound">
                                  <HeaderTemplate>
                                      <tbody>
                                  </HeaderTemplate>

                                <ItemTemplate>
                                     <tr productIndex = '<%# Container.ItemIndex+1 %>'>
                                        <asp:PlaceHolder ID="DatesRepeaterPlaceHolder" runat="server" />
                                     </tr>
                                </ItemTemplate>

                                <FooterTemplate>
                                    </tbody>
                                </FooterTemplate>
                        
                                </asp:Repeater>
                    
                           </table>
                       </div>
                   </div>
               </td>
           </tr>

           </tbody>
           </table>	
   
           <div class="dottedLine"></div>	
           <div class="DatesNavigationWrapper">
						        <div class="WeekPrev">Previous week</div>
                                <div class="WeekNext">Next week</div>
						
	        </div>		
    </div>

           
       </td>
       <td width="1%"></td>
       <td style="width: 20%" valign="top" id="feedTd">
           <uc1:TwitterFeed ID="twitterFeed" runat="server"    />
       </td>
   </tr>     
</table>





