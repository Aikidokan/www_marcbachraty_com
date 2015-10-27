<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.ascx.cs" Inherits="Governor.Umbraco.uShare.UI.Umbraco.Dashboard" %>

<style>
    .uShare .dashboardWrapper h3
    {
        font-size: 24.5px;
        font-weight: normal;
        margin-left: 45px;
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
</style>

<script type="text/javascript">
    function openFacebookAuthPopup() {
        window.open('/app_plugins/ushare/usharefacebook.aspx', '_blank', 'width=450, height=260');
        //TODO: show backend notification
        //UmbClientMgr.mainWindow().UmbSpeechBubble.ShowMessage('success', 'Facebook Authorization <br /> complete', '');
    }

    function openLinkedInAuthPopup() {
        window.open('/app_plugins/ushare/usharelinkedin.aspx', '_blank', 'width=440, height=572');
        //TODO: show backend notification
        //UmbClientMgr.mainWindow().UmbSpeechBubble.ShowMessage('success', 'LinkedIn Authorization <br /> complete', '');
    }

    function openTwitterAuthPopup() {
        window.open('/app_plugins/ushare/usharetwitter.aspx', '_blank', 'width=385, height=640');
        //TODO: show backend notification
        //UmbClientMgr.mainWindow().UmbSpeechBubble.ShowMessage('success', 'Twitter Authorization <br /> complete', '');
    }
</script>

<div class="uShare">
    <div class="propertypane">
        <div class="propertyItem">
            <div class="dashboardWrapper">
                <h3>uShare Authorization / Deauthorization</h3>
                <img src="/App_Plugins/uShare/icon.png" alt="uShare" class="dashboardIcon" />
                <p>
                    Authorize or deauthorize the current Umbraco user to share content to the specific service providers.<br />
                    Please note that only single service provider access tokens are stored in association with Umbraco users.
                </p>
                <div class="service-providers">
                    <asp:MultiView runat="server" ID="mvServiceProviders">
                        <asp:View runat="server" ID="viewServiceProviders">
                            <asp:PlaceHolder runat="server" ID="phFacebook" Visible="False">
                                <div class="service-provider">
                                    <h4>Facebook</h4>
                                    <div class="btn-group">
                                        <asp:LinkButton runat="server" ID="btnFacebookAuthorize" Text="Facebook Authorize" CssClass="btn" OnClientClick="openFacebookAuthPopup(); return false;"></asp:LinkButton>
                                    </div>
                                    <div class="btn-group">
                                        <asp:LinkButton runat="server" ID="btnFacebookDeauthorize" Text="Facebook Deauthorize" CssClass="btn" OnClick="btnFacebookDeauthorize_Click" />
                                    </div>
                                </div>
                            </asp:PlaceHolder>
                            <asp:PlaceHolder runat="server" ID="phLinkedIn" Visible="False">
                                <div class="service-provider">
                                    <h4>LinkedIn</h4>
                                    <div class="btn-group">
                                        <asp:LinkButton runat="server" ID="btnLinkedInAuthorize" Text="LinkedIn Authorize" CssClass="btn" OnClientClick="openLinkedInAuthPopup(); return false;" />
                                    </div>
                                    <div class="btn-group">
                                        <asp:LinkButton runat="server" ID="btnLinkedInDeauthorize" Text="LinkedIn Deauthorize" CssClass="btn" OnClick="btnLinkedInDeauthorize_Click" />
                                    </div>
                                </div>
                            </asp:PlaceHolder>
                            <asp:PlaceHolder runat="server" ID="phTwitter" Visible="False">
                                <div class="service-provider">
                                    <h4>Twitter</h4>
                                    <div class="btn-group">
                                        <asp:LinkButton runat="server" ID="btnTwitterAuthorize" Text="Twitter Authorize" CssClass="btn" OnClientClick="openTwitterAuthPopup(); return false;" />
                                    </div>
                                    <div class="btn-group">
                                        <asp:LinkButton runat="server" ID="btnTwitterDeauthorize" Text="Twitter Deauthorize" CssClass="btn" OnClick="btnTwitterDeauthorize_Click" />
                                    </div>
                                </div>
                            </asp:PlaceHolder>
                        </asp:View>
                        <asp:View runat="server" ID="viewNoServiceProviders">
                            <p>No active service providers. See data type properties to activate.</p>
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>
        </div>
    </div>
</div>