<%@ Control Language="vb" AutoEventWireup="true" CodeFile="SelectionToolTip.ascx.vb" Inherits="UserForms_SelectionToolTip" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.12.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors"
	TagPrefix="dxe" %>
<div runat="server" id="buttonDiv">
	<dxe:ASPxButton ID="btnShowMenu" runat="server" AutoPostBack="False" AllowFocus="False">
		<Border BorderWidth="0px" />
		<Paddings Padding="0px" />
		<FocusRectPaddings Padding="4px" />
		<FocusRectBorder BorderStyle="None" BorderWidth="0px" />
	</dxe:ASPxButton>
</div>    
<script type="text/javascript" id="dxss_">
	ASPxClientSelectionToolTip = _aspxCreateClass(ASPxClientToolTipBase, {
		Initialize: function() {
			ASPxClientUtils.AttachEventToElement(this.controls.buttonDiv, "click", _aspxCreateDelegate(this.OnButtonDivClick, this));
		},
		OnButtonDivClick: function(s,e) {
			this.ShowViewMenu(s);
		}
	});    
</script>