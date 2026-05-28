<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateInformation.aspx.cs" Inherits="StudySprint.Pages.Student.Profile.EditProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../../Assets/css/style_student.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="content-wrapper">
        
        <div class="header-row">
            <h2 class="page-title">Update Information</h2>
            <asp:hyperlink ID="lnkBack" runat="server" NavigateUrl="Profile.aspx" CssClass="back-link">&lt; Back</asp:hyperlink>
        </div>

        <div class="profile-card">
            
            <div class="profile-info-grid">
                
                <div class="profile-group">
                    <label class="profile-label">Email</label>
                    <asp:textbox ID="txtEmail" runat="server" CssClass="form-field" TextMode="Email"></asp:textbox>
                </div>
                
                <div class="profile-group">
                    <label class="profile-label">Username</label>
                    <asp:textbox ID="txtUsername" runat="server" CssClass="form-field"></asp:textbox>
                </div>
                
                <div class="profile-group">
                    <label class="profile-label">Full Name</label>
                    <asp:textbox ID="txtFullName" runat="server" CssClass="form-field"></asp:textbox>
                </div>
                
                <div class="profile-group">
                    <label class="profile-label">Password</label>
                    <asp:textbox ID="txtPassword" runat="server" CssClass="form-field" TextMode="Password" placeholder="Leave blank to keep current"></asp:textbox>
                </div>
                
            </div>
            
            <asp:label ID="lblMessage" runat="server" CssClass="form-success-msg"></asp:label>
            
            <div class="form-action-row">
                <asp:button ID="btnSave" runat="server" Text="Save Changes" CssClass="btn-action" OnClick="btnSave_Click" />
            </div>
            
        </div>
        
    </div>
    
</asp:Content>