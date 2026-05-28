<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewStudentProgress.aspx.cs" Inherits="StudySprint.Pages.Admin.ManageStudent.ViewStudentProgress" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../../Assets/css/style_admin.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="manage-students-container">
        
        <div class="header-row">
            <h2 class="page-title">View Student Progress</h2>
            <asp:hyperlink ID="lnkBack" runat="server" NavigateUrl="ManageStudents.aspx" CssClass="back-link">&lt; Back</asp:hyperlink>
        </div>

        <h4 class="student-name-label">Name: <asp:label ID="lblStudentName" runat="server"></asp:label></h4>

        <asp:gridview ID="gvProgress" 
            runat="server" 
            AutoGenerateColumns="False" 
            CssClass="student-grid progress-grid" 
            GridLines="None">
            
            <columns>
                <asp:boundfield DataField="Title" HeaderText="Title" />
                
                <asp:boundfield DataField="DisplayScore" HeaderText="Score" />

                <asp:boundfield DataField="DateTaken" HeaderText="Date" DataFormatString="{0:MMMM dd, yyyy}" />
            </columns>
            
            <emptydatatemplate>
                <div class="empty-data">No quiz records found for this student.</div>
            </emptydatatemplate>
        </asp:gridview>
        
    </div>
</asp:Content>