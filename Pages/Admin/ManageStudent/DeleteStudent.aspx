<%@ Page Title="Delete Student" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DeleteStudent.aspx.cs" Inherits="StudySprint.Pages.Admin.ManageStudent.DeleteStudent" %>

<asp:content id="Content1" contentplaceholderid="head" runat="server">
    <link href="../../../Assets/css/style_admin.css" rel="stylesheet" type="text/css" />
</asp:content>

<asp:content id="Content2" contentplaceholderid="MainContent" runat="server">
    <div class="manage-students-container">
        <div class="header-row">
            <h2 class="page-title">Delete Account</h2>
            <asp:hyperlink id="lnkBack" runat="server" navigateurl="ManageStudents.aspx" cssclass="back-link">&lt; Back</asp:hyperlink>
        </div>

        <div class="form-card">
            <div class="warning-header">
                <h3>Confirm Deletion</h3>
                <p>Warning: This action will permanently remove the student and all associated quiz history from StudySprint. This action cannot be undone.</p>
            </div>

            <div class="data-display-group">
                <span class="data-label">Email</span>
                <asp:label id="lblEmail" runat="server" cssclass="data-value"></asp:label>
            </div>

            <div class="data-display-group">
                <span class="data-label">Username</span>
                <asp:label id="lblUsername" runat="server" cssclass="data-value"></asp:label>
            </div>

            <div class="data-display-group">
                <span class="data-label">Full Name</span>
                <asp:label id="lblFullName" runat="server" cssclass="data-value"></asp:label>
            </div>

            <asp:label id="lblMessage" runat="server" cssclass="form-error-msg" visible="false"></asp:label>

            <div class="form-action-row mt-wide">
                <asp:button id="btnDelete" runat="server" onclick="btnDelete_Click" text="Delete Account" cssclass="btn-action btn-destructive" onclientclick="return confirm('Final Confirmation: Are you absolutely sure?');" />
            </div>
        </div>
    </div>
</asp:content>