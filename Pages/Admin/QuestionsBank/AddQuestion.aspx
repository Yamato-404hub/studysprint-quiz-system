<%@ Page Title="Add Question" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddQuestion.aspx.cs" Inherits="StudySprint.Pages.Admin.QuestionsBank.AddQuestion" %>

<asp:Content id="Content1" contentplaceholderid="head" runat="server">
    <style>
        .form-container {
            max-width: 650px;
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
            background-color: var(--text-muted);
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
        <h2 class="page-title">Add New Question</h2>
        <hr />

        <asp:label id="lblMessage" runat="server" visible="false"></asp:label>

        <div class="form-group">
            <label>Quiz *</label>
            <asp:dropdownlist id="ddlQuiz" runat="server" cssclass="form-control"></asp:dropdownlist>
            <asp:requiredfieldvalidator id="rfvQuiz" runat="server" controltovalidate="ddlQuiz" initialvalue="" errormessage="Please select a quiz." cssclass="text-danger" display="Dynamic" validationgroup="AddQuestion"></asp:requiredfieldvalidator>
        </div>

        <div class="form-group">
            <label>Question *</label>
            <asp:textbox id="txtQuestion" runat="server" cssclass="form-control" textmode="MultiLine" rows="3" placeholder="Enter question text"></asp:textbox>
            <asp:requiredfieldvalidator id="rfvQuestion" runat="server" controltovalidate="txtQuestion" errormessage="Question is required." cssclass="text-danger" display="Dynamic" validationgroup="AddQuestion"></asp:requiredfieldvalidator>
        </div>

        <div class="form-group">
            <label>Option A *</label>
            <asp:textbox id="txtOptionA" runat="server" cssclass="form-control" placeholder="Enter option A"></asp:textbox>
            <asp:requiredfieldvalidator id="rfvOptionA" runat="server" controltovalidate="txtOptionA" errormessage="Option A is required." cssclass="text-danger" display="Dynamic" validationgroup="AddQuestion"></asp:requiredfieldvalidator>
        </div>

        <div class="form-group">
            <label>Option B *</label>
            <asp:textbox id="txtOptionB" runat="server" cssclass="form-control" placeholder="Enter option B"></asp:textbox>
            <asp:requiredfieldvalidator id="rfvOptionB" runat="server" controltovalidate="txtOptionB" errormessage="Option B is required." cssclass="text-danger" display="Dynamic" validationgroup="AddQuestion"></asp:requiredfieldvalidator>
        </div>

        <div class="form-group">
            <label>Option C *</label>
            <asp:textbox id="txtOptionC" runat="server" cssclass="form-control" placeholder="Enter option C"></asp:textbox>
            <asp:requiredfieldvalidator id="rfvOptionC" runat="server" controltovalidate="txtOptionC" errormessage="Option C is required." cssclass="text-danger" display="Dynamic" validationgroup="AddQuestion"></asp:requiredfieldvalidator>
        </div>

        <div class="form-group">
            <label>Option D *</label>
            <asp:textbox id="txtOptionD" runat="server" cssclass="form-control" placeholder="Enter option D"></asp:textbox>
            <asp:requiredfieldvalidator id="rfvOptionD" runat="server" controltovalidate="txtOptionD" errormessage="Option D is required." cssclass="text-danger" display="Dynamic" validationgroup="AddQuestion"></asp:requiredfieldvalidator>
        </div>

        <div class="form-group">
            <label>Correct Answer *</label>
            <asp:dropdownlist id="ddlCorrect" runat="server" cssclass="form-control">
                <asp:listitem text="-- Select Answer --" value=""></asp:listitem>
                <asp:listitem text="A" value="A"></asp:listitem>
                <asp:listitem text="B" value="B"></asp:listitem>
                <asp:listitem text="C" value="C"></asp:listitem>
                <asp:listitem text="D" value="D"></asp:listitem>
            </asp:dropdownlist>
            <asp:requiredfieldvalidator id="rfvCorrect" runat="server" controltovalidate="ddlCorrect" initialvalue="" errormessage="Please select the correct answer." cssclass="text-danger" display="Dynamic" validationgroup="AddQuestion"></asp:requiredfieldvalidator>
        </div>

        <div class="button-group">
            <asp:button id="btnAddQuestion" runat="server" text="Add Question" cssclass="btn-action" validationgroup="AddQuestion" onclick="btnAddQuestion_Click" />
            <asp:button id="btnClear" runat="server" text="Clear" cssclass="btn-action btn-outline" causesvalidation="false" onclick="btnClear_Click" />
            <asp:hyperlink id="hlBack" runat="server" navigateurl="ManageQuestions.aspx" cssclass="btn-action btn-outline">Back to List</asp:hyperlink>
        </div>

    </div>
</asp:Content>