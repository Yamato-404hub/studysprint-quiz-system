<%@ Page Title="Edit Quiz" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditQuiz.aspx.cs" Inherits="StudySprint.Pages.Admin.ManageQuizzes.EditQuiz" %>

<asp:Content id="Content1" contentplaceholderid="head" runat="server">
    <style>
        .form-container {
            max-width: 600px;
            margin: 40px auto;
            padding: 40px;
            background-color: var(--card-bg);
            border-radius: var(--radius-lg);
            box-shadow: var(--shadow-md);
            font-family: 'Segoe UI', Tahoma, sans-serif;
        }

        .page-title {
            font-size: 1.8rem;
            font-weight: 800;
            color: var(--text-main);
            margin-top: 0;
            margin-bottom: 10px;
        }

        hr {
            border: none;
            border-top: 1px solid var(--border-color);
            margin-bottom: 25px;
        }

        .form-group {
            margin-bottom: 20px;
        }

        .form-group label {
            display: block;
            font-weight: 600;
            color: var(--text-main);
            margin-bottom: 8px;
        }

        .form-control {
            width: 100%;
            padding: 12px 16px;
            background-color: #F1F5F9;
            border: 1px solid transparent;
            border-radius: var(--radius-md);
            transition: all 0.3s ease;
            font-family: inherit;
            color: var(--text-main);
            box-sizing: border-box;
        }

        .form-control:focus {
            outline: none;
            background-color: var(--card-bg);
            border-color: var(--text-main);
            box-shadow: 0 0 0 3px rgba(30, 41, 59, 0.1);
        }

        .form-control[readonly] {
            background-color: var(--border-color);
            color: var(--text-muted);
            cursor: not-allowed;
            border: 1px dashed #CBD5E1;
        }
        .form-control[readonly]:focus {
            box-shadow: none;
            border-color: #CBD5E1;
        }

        .button-group {
            display: flex;
            gap: 15px;
            margin-top: 35px;
        }

        .btn-action {
            flex: 1; 
            background-color: var(--text-main);
            color: var(--card-bg);
            padding: 12px 24px;
            border-radius: 25px;
            font-size: 1rem;
            font-weight: 600;
            border: none;
            cursor: pointer;
            transition: background-color 0.2s ease, transform 0.1s ease, border-color 0.2s ease;
            text-align: center;
            text-decoration: none;
            display: inline-block;
        }

        .btn-action:hover {
            background-color: #424245;
            color: var(--card-bg);
        }

        .btn-action:active {
            transform: scale(0.98);
        }

        .btn-outline {
            background-color: transparent;
            color: var(--text-main);
            border: 2px solid var(--border-color);
        }

        .btn-outline:hover {
            background-color: transparent;
            border-color: var(--text-main);
            color: var(--text-main);
        }

        .text-danger { 
            color: var(--error-color); 
            font-size: 0.85rem; 
            display: block; 
            margin-top: 6px; 
            font-weight: 500;
        }
        
        .alert { 
            display: block; 
            padding: 14px 16px; 
            margin-bottom: 25px; 
            border-radius: var(--radius-md); 
            font-weight: 600; 
            font-size: 0.95rem; 
            text-align: center; 
            box-sizing: border-box;
        }
        .alert-success { background-color: #dcfce7; color: #166534; border: 1px solid #bbf7d0; }
        .alert-danger { background-color: #fee2e2; color: #991b1b; border: 1px solid #fecaca; }
    </style>
</asp:Content>

<asp:Content id="Content2" contentplaceholderid="MainContent" runat="server">
    <div class="form-container">
        <h2 class="page-title">Edit Quiz Details</h2>
        <hr />

        <asp:label id="lblMessage" runat="server" visible="false"></asp:label>

        <div class="form-group">
            <label>Quiz ID</label>
            <asp:textbox id="txtQuizID" runat="server" cssclass="form-control" readonly="true"></asp:textbox>
        </div>

        <div class="form-group">
            <label for="txtTitle">Quiz Title *</label>
            <asp:textbox id="txtTitle" runat="server" cssclass="form-control"></asp:textbox>
            <asp:requiredfieldvalidator id="rfvTitle" runat="server" controltovalidate="txtTitle" 
                errormessage="Quiz title cannot be empty." cssclass="text-danger" display="Dynamic"></asp:requiredfieldvalidator>
        </div>

        <div class="form-group">
            <label for="txtDescription">Description</label>
            <asp:textbox id="txtDescription" runat="server" textmode="MultiLine" rows="4" cssclass="form-control"></asp:textbox>
        </div>

        <div class="form-group">
            <label for="txtDuration">Duration (Minutes) *</label>
            <asp:textbox id="txtDuration" runat="server" cssclass="form-control" textmode="Number"></asp:textbox>
            <asp:requiredfieldvalidator id="rfvDuration" runat="server" controltovalidate="txtDuration" 
                errormessage="Duration is required." cssclass="text-danger" display="Dynamic"></asp:requiredfieldvalidator>
            <asp:rangevalidator id="rvDuration" runat="server" controltovalidate="txtDuration" 
                minimumvalue="1" maximumvalue="300" type="Integer" 
                errormessage="Duration must be between 1 and 300 minutes." cssclass="text-danger" display="Dynamic"></asp:rangevalidator>
        </div>

        <div class="button-group">
            <asp:button id="btnUpdate" runat="server" text="Update Quiz" cssclass="btn-action" onclick="btnUpdate_Click" />
            <asp:hyperlink id="hlBack" runat="server" navigateurl="~/Pages/Admin/ManageQuizzes/ManageQuizzes.aspx" cssclass="btn-action btn-outline">Cancel</asp:hyperlink>
        </div>
    </div>
</asp:Content>