<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="uShareInstaller.ascx.cs" Inherits="Governor.Umbraco.uShare.Installer.uShareInstaller" %>
<%@ Register Assembly="controls" Namespace="umbraco.uicontrols" TagPrefix="umb" %>

<style>
    .uShare .dashboardWrapper h3
    {
        font-size: 24.5px;
        font-weight: normal;
        margin: 0 0 15px 45px;
    }
    .uShare .dashboardWrapper h4
    {
        margin: 15px 0 5px 0;
        line-height: 20px;
        font-size: 17.5px;
    }
    .uShare .dashboardWrapper .btn
    {
        width: 150px;
    }
    .uShare .provider-label
    {
        display: inline-block;
        width: 130px;
    }
    .uShare .provider-input
    {
        margin: 2px;
        width: 400px;
    }
</style>

<umb:PropertyPanel ID="PropertyPanel1" runat="server">
    <div class="uShare">
        <div class="dashboardWrapper">
            <h3>uShare</h3>
            <img src="/App_Plugins/uShare/icon.png" alt="uShare" class="dashboardIcon" />
            <asp:MultiView runat="server" ID="mvConfigureSettings">
                <asp:View runat="server" ID="viewConfigure">
                    <umb:Feedback runat="server" ID="SuccessInstall" type="success" Text="Package installed successfully." />
                    <umb:Feedback runat="server" ID="FailureInstall" type="error" Text="Package failed to install." Visible="false" />
                    <p>
                        Please configure uShare with your social media provider settings below.<br />
                        Configurations can manually be updated in <i>uShare.config</i> in the configurations folder.
                    </p>
                    <h4>Facebook</h4>
                    <div class="provider-settings">
                        <asp:Label runat="server" ID="lblFacebookKey" CssClass="provider-label" AssociatedControlID="txtFacebookKey">Client api key</asp:Label>
                        <asp:TextBox runat="server" ID="txtFacebookKey" CssClass="provider-input"></asp:TextBox>
                        <br />
                        <asp:Label runat="server" ID="lblFacebookSecret" CssClass="provider-label" AssociatedControlID="txtFacebookSecret">Client api secret</asp:Label>
                        <asp:TextBox runat="server" ID="txtFacebookSecret" CssClass="provider-input"></asp:TextBox>
                        <br />
                        <asp:Label runat="server" ID="lblFacebookPageId" CssClass="provider-label" AssociatedControlID="txtFacebookPageId">Page username/id</asp:Label>
                        <asp:TextBox runat="server" ID="txtFacebookPageId" CssClass="provider-input"></asp:TextBox>
                    </div>
                    <h4>LinkedIn</h4>
                    <div class="provider-settings">
                        <asp:Label runat="server" ID="lblLinkedInKey" CssClass="provider-label" AssociatedControlID="txtLinkedInKey">Client api key</asp:Label>
                        <asp:TextBox runat="server" ID="txtLinkedInKey" CssClass="provider-input"></asp:TextBox>
                        <br />
                        <asp:Label runat="server" ID="lblLinkedInSecret" CssClass="provider-label" AssociatedControlID="txtLinkedInSecret">Client api secret</asp:Label>
                        <asp:TextBox runat="server" ID="txtLinkedInSecret" CssClass="provider-input"></asp:TextBox>
                    </div>
                    <h4>Twitter</h4>
                    <div class="provider-settings">
                        <asp:Label runat="server" ID="lblTwitterKey" CssClass="provider-label" AssociatedControlID="txtTwitterKey">Client api key</asp:Label>
                        <asp:TextBox runat="server" ID="txtTwitterKey" CssClass="provider-input"></asp:TextBox>
                        <br />
                        <asp:Label runat="server" ID="lblTwitterSecret" CssClass="provider-label" AssociatedControlID="txtTwitterSecret">Client api secret</asp:Label>
                        <asp:TextBox runat="server" ID="txtTwitterSecret" CssClass="provider-input"></asp:TextBox>
                    </div>
                    <br />
                    <asp:LinkButton runat="server" ID="btnSaveConfigurations" Text="Save" CssClass="btn" OnClick="btnSaveConfigurations_Click" />
                </asp:View>
                <asp:View runat="server" ID="viewSaved">
                    <umb:Feedback runat="server" ID="SuccessConfigure" type="success" Text="Configurations saved successfully." />
                    <umb:Feedback runat="server" ID="FailureConfigure" type="error" Text="Configurations failed to save." Visible="false" />
                    <p>Configurations can manually be updated in <i>uShare.config</i> in the configurations folder.</p>
                </asp:View>
            </asp:MultiView>
        </div>
    </div>
</umb:PropertyPanel>
