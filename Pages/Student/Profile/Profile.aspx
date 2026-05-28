<%@ Page Title="Profile" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="StudySprint.Pages.Student.Profile.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../../Assets/css/style_student.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="content-wrapper">
        
        <div class="header-row">
            <h2 class="page-title">Profile</h2>
            <asp:button ID="btnUpdateInfo" runat="server" Text="Update Information" CssClass="btn-action" OnClick="btnUpdateInfo_Click" />
        </div>

        <div class="profile-card">
            
            <div class="profile-info-grid">
                
                <div class="profile-group">
                    <span class="profile-label">Email</span>
                    <asp:label ID="lblEmail" runat="server" CssClass="profile-value-box"></asp:label>
                </div>

                <div class="profile-group">
                    <span class="profile-label">Username</span>
                    <asp:label ID="lblUsername" runat="server" CssClass="profile-value-box"></asp:label>
                </div>

                <div class="profile-group">
                    <span class="profile-label">Full Name</span>
                    <asp:label ID="lblFullName" runat="server" CssClass="profile-value-box"></asp:label>
                </div>

                <div class="profile-group">
                    <span class="profile-label">Password</span>
                    <asp:label ID="lblPassword" runat="server" CssClass="profile-value-box">••••••••</asp:label>
                </div>

            </div>

        </div>
    </div>
</asp:Content>