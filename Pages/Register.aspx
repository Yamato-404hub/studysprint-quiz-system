<%@ Page Title="Create Account" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="StudySprint.Pages.Register" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="auth-wrapper">
        <div class="auth-card">
            <h1>Create Account</h1>
            <p class="subtitle">Join StudySprint to start your learning journey</p>

            <div class="input-group">
                <label>Full Name</label>
                <asp:textbox id="txtFullName" runat="server" cssclass="form-input" placeholder="Enter your full name"></asp:textbox>
                <asp:requiredfieldvalidator id="rfvFullName" runat="server" controltovalidate="txtFullName" 
                    errormessage="Full name is required" display="Dynamic" cssclass="error-text" />
            </div>

            <div class="input-group"> 
                <label>Email Address</label>
                <asp:textbox id="txtEmail" runat="server" cssclass="form-input" textmode="Email" placeholder="example@mail.com"></asp:textbox>
                
                <asp:requiredfieldvalidator id="rfvEmail" runat="server" controltovalidate="txtEmail" 
                    errormessage="Email is required" display="Dynamic" cssclass="error-text" />
                <asp:regularexpressionvalidator id="revEmail" runat="server" controltovalidate="txtEmail" 
                    errormessage="Please enter a valid email" validationexpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                    display="Dynamic" cssclass="error-text" />
            </div>

            <div class="input-group">
                <label>Username</label>
                <asp:textbox id="txtUsername" runat="server" cssclass="form-input" placeholder="Choose a username"></asp:textbox>
                <asp:requiredfieldvalidator id="rfvUser" runat="server" controltovalidate="txtUsername" 
                    errormessage="Username is required" display="Dynamic" cssclass="error-text" />
            </div>

            <div class="input-group">
                <label>Password</label>
                <asp:textbox id="txtPassword" runat="server" cssclass="form-input" textmode="Password" placeholder="••••••••"></asp:textbox>
                <asp:requiredfieldvalidator id="rfvPass" runat="server" controltovalidate="txtPassword" 
                    errormessage="Password is required" display="Dynamic" cssclass="error-text" />
            </div>
              
            <div class="input-group">
                <label>Confirm Password</label>
                <asp:textbox id="txtConfirmPassword" runat="server" cssclass="form-input" textmode="Password" placeholder="••••••••"></asp:textbox>
                
                <asp:requiredfieldvalidator id="rfvConfirmPass" runat="server" controltovalidate="txtConfirmPassword" 
                    errormessage="Please confirm your password" display="Dynamic" cssclass="error-text" />

                <asp:comparevalidator id="cvPassword" runat="server" controltocompare="txtPassword" 
                    controltovalidate="txtConfirmPassword" errormessage="Passwords do not match" 
                    display="Dynamic" cssclass="error-text" />
            </div>

            <asp:button id="btnRegister" runat="server" text="Register" cssclass="btn-submit" onclick="btnRegister_Click" />
            
            <%-- Status feedback for registration --%>
            <asp:label id="lblMessage" runat="server" cssclass="status-label"></asp:label>
        </div>
    </div>
</asp:Content>