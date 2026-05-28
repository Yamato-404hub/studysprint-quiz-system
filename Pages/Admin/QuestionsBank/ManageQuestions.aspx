<%@ Page Title="Questions Bank" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageQuestions.aspx.cs" Inherits="StudySprint.Pages.Admin.QuestionsBank.ManageQuestions" %>

<asp:Content id="Content1" contentplaceholderid="head" runat="server">
    <style>
        .manage-quizzes-container {
            font-family: 'Segoe UI', Tahoma, sans-serif;
            padding: 30px;
            max-width: 1200px;
            margin: 0 auto;
        }

        .header-section {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 25px;
        }

        .page-title {
            font-size: 2rem;
            font-weight: 800;
            color: var(--text-main);
            margin: 0;
        }
        
        .page-subtitle {
            color: var(--text-muted);
            margin-top: 5px;
            font-size: 0.95rem;
        }

        .filter-card {
            background-color: var(--card-bg);
            border-radius: var(--radius-md);
            box-shadow: var(--shadow-sm);
            padding: 20px;
            margin-bottom: 30px;
            display: flex;
            gap: 15px;
            align-items: flex-end;
            flex-wrap: wrap;
        }

        .filter-group {
            display: flex;
            flex-direction: column;
            flex: 1;
            min-width: 200px;
        }

        .filter-group label {
            font-weight: 600;
            color: var(--text-main);
            margin-bottom: 8px;
            font-size: 0.9rem;
        }

        .form-control {
            width: 100%;
            padding: 10px 14px;
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

        .btn-action {
            background-color: var(--text-main);
            color: var(--card-bg);
            padding: 10px 24px;
            border-radius: 25px;
            font-size: 0.95rem;
            font-weight: 600;
            border: none;
            cursor: pointer;
            transition: background-color 0.2s ease, transform 0.1s ease;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            height: 42px;
            line-height: 22px;
            box-sizing: border-box;
        }

        .btn-action:hover {
            background-color: #424245;
            color: var(--card-bg);
            transform: translateY(-1px);
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

        .btn-action-sm {
            padding: 6px 16px;
            font-size: 0.85rem;
            border-radius: 18px;
            margin-right: 8px;
            height: auto;
            line-height: normal;
        }

        .quiz-grid {
            width: 100%;
            background-color: var(--card-bg);
            border-radius: var(--radius-md);
            box-shadow: var(--shadow-sm);
            border-collapse: collapse;
            overflow: hidden;
        }

        .quiz-grid th {
            background-color: var(--card-bg);
            color: var(--text-main);
            padding: 18px 20px;
            text-align: left;
            border-bottom: 2px solid var(--border-color);
            font-weight: 700;
        }

        .quiz-grid td {
            padding: 16px 20px;
            border-bottom: 1px solid var(--border-color);
            color: var(--text-main);
            vertical-align: middle;
        }

        .quiz-grid tr:last-child td {
            border-bottom: none;
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
    <div class="manage-quizzes-container">

        <div class="header-section">
            <div>
                <h2 class="page-title">Questions Bank</h2>
                <p class="page-subtitle">View, search, edit and delete multiple-choice questions.</p>
            </div>
            <asp:button id="btnAdd" runat="server" text="+ Add Question" cssclass="btn-action" causesvalidation="false" onclick="btnAdd_Click" />
        </div>

        <asp:label id="lblMessage" runat="server" visible="false"></asp:label>

        <div class="filter-card">
            <div class="filter-group">
                <label>Filter by Quiz</label>
                <asp:dropdownlist id="ddlQuiz" runat="server" cssclass="form-control"></asp:dropdownlist>
            </div>
            
            <div class="filter-group">
                <label>Search Question or Quiz Title</label>
                <asp:textbox id="txtSearch" runat="server" cssclass="form-control" placeholder="Enter keywords..."></asp:textbox>
            </div>

            <asp:button id="btnSearch" runat="server" text="Search" cssclass="btn-action" onclick="btnSearch_Click" />
            <asp:button id="btnClear" runat="server" text="Clear" cssclass="btn-action btn-outline" causesvalidation="false" onclick="btnClear_Click" />
        </div>

        <asp:gridview id="gvQuestions" runat="server" autogeneratecolumns="false" cssclass="quiz-grid" datakeynames="QuestionID" gridlines="None" onrowcommand="gvQuestions_RowCommand">
            <columns>
                <asp:boundfield datafield="QuestionID" headertext="ID" itemstyle-width="60px" />
                <asp:boundfield datafield="QuizTitle" headertext="Quiz" itemstyle-width="200px" headerstyle-font-bold="true" />
                <asp:boundfield datafield="QuestionText" headertext="Question" />
                <asp:boundfield datafield="OptionCount" headertext="Options" itemstyle-width="100px" itemstyle-horizontalalign="Center" headerstyle-horizontalalign="Center" />

                <asp:templatefield headertext="Action" itemstyle-width="180px">
                    <itemtemplate>
                        <asp:linkbutton id="lnkEdit" runat="server" commandname="EditQuestion" commandargument='<%# Eval("QuestionID") %>' causesvalidation="false" cssclass="btn-action btn-action-sm" style="background-color: #2563EB; margin-bottom: 5px;">Edit</asp:linkbutton>
                        
                        <asp:linkbutton id="lnkDelete" runat="server" commandname="DeleteQuestion" commandargument='<%# Eval("QuestionID") %>' causesvalidation="false" onclientclick="return confirm('Are you sure you want to delete this question?');" cssclass="btn-action btn-action-sm" style="background-color: #EF4444">Delete</asp:linkbutton>
                    </itemtemplate>
                </asp:templatefield>
            </columns>
        </asp:gridview>

    </div>
</asp:Content>