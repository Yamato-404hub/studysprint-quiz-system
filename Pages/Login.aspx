<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="StudySprint.Pages.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="auth-wrapper">
        <div class="auth-card login-card">
            <h1>Welcome Back</h1>
            <p class="subtitle">Sign in to continue your learning journey</p>

            <div class="input-group">
                <label>Username:</label>
                <asp:textbox id="txtUsername" runat="server" cssclass="form-input" placeholder="Enter your username"></asp:textbox>
                
                <%-- Basic empty check --%>
                <asp:requiredfieldvalidator id="rfvUsername" runat="server" controltovalidate="txtUsername" 
                    errormessage="Username is required" display="Dynamic" cssclass="error-text" />
            </div>

            <div class="input-group">
                <label>Password:</label>
                <asp:textbox id="txtPassword" runat="server" cssclass="form-input" textmode="Password" placeholder="Enter your password"></asp:textbox>
                
                <%-- Ensure password isn't blank before postback --%>
                <asp:requiredfieldvalidator id="rfvPassword" runat="server" controltovalidate="txtPassword" 
                    errormessage="Password is required" display="Dynamic" cssclass="error-text" />

            </div>

            <asp:button id="btnLogin" runat="server" text="Login" cssclass="btn-submit" onclick="btnLogin_Click" />
            
            <%-- For displaying 'Invalid credentials' errors from backend --%>
            <asp:label id="lblMessage" runat="server" cssclass="status-label"></asp:label>

            <div class="admin-tip" style="font-size: 0.85rem; color: var(--text-muted); margin-top: 15px;">
                Tip: Use username "admin" to access admin dashboard
            </div>
        </div>
    </div>
</asp:Content>