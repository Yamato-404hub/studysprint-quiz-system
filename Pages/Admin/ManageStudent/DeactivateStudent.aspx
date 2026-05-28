<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DeactivateStudent.aspx.cs" Inherits="StudySprint.Pages.Admin.ManageStudent.DeactivateStudent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../../Assets/css/style_admin.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="manage-students-container">
        <div class="header-row">
            <h2 class="page-title">Deactivate Account</h2>
            <asp:hyperlink ID="lnkBack" runat="server" NavigateUrl="ManageStudents.aspx" CssClass="back-link">&lt; Back</asp:hyperlink>
        </div>

        <div class="form-card">
            <div class="warning-header">
                <h3>Confirm Deactivation</h3>
                <p>Please review the student details below. This action will suspend their access to StudySprint.</p>
            </div>

            <div class="data-display-group">
                <span class="data-label">Email</span>
                <asp:label ID="lblEmail" runat="server" CssClass="data-value"></asp:label>
            </div>

            <div class="data-display-group">
                <span class="data-label">Username</span>
                <asp:label ID="lblUsername" runat="server" CssClass="data-value"></asp:label>
            </div>

            <div class="data-display-group">
                <span class="data-label">Full Name</span>
                <asp:label ID="lblFullName" runat="server" CssClass="data-value"></asp:label>
            </div>

            <div class="form-action-row mt-wide">
                <asp:button ID="btnDeactivate" runat="server" OnClick="btnDeactivate_Click1" Text="Deactivate Account" CssClass="btn-action btn-destructive" />
            </div>
        </div>
    </div>
</asp:Content>