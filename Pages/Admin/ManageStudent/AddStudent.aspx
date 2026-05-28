<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddStudent.aspx.cs" Inherits="StudySprint.Pages.Admin.ManageStudent.AddStudent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../../Assets/css/style_admin.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="manage-students-container">
        <div class="header-row">
            <h2 class="page-title">Add Student</h2>
            <asp:hyperlink ID="lnkBack" runat="server" NavigateUrl="ManageStudents.aspx" CssClass="back-link">&lt; Back</asp:hyperlink>
        </div>

        <div class="form-card">
            <div class="form-input-group">
                <label class="form-label">Full Name</label>
                <asp:textbox ID="txtFullName" runat="server" CssClass="form-field" Required="true"></asp:textbox>
            </div>
            
            <div class="form-input-group">
                <label class="form-label">Username</label>
                <asp:textbox ID="txtUsername" runat="server" CssClass="form-field" Required="true"></asp:textbox>
            </div>
            
            <div class="form-input-group">
                <label class="form-label">Email</label>
                <asp:textbox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-field" Required="true"></asp:textbox>
            </div>
            
            <div class="form-input-group">
                <label class="form-label">Temporary Password</label>
                <asp:textbox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-field" Required="true"></asp:textbox>
            </div>
            
            <asp:label ID="lblMessage" runat="server" CssClass="form-error-msg"  style="color: "></asp:label>
            
            <div class="form-action-row">
                <asp:button ID="btnCreateStudent" runat="server" OnClick="btnCreateStudent_Click1" Text="Add Student" CssClass="btn-action" />
            </div>
        </div>
    </div>
</asp:Content>