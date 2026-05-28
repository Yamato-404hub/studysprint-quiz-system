<%@ Page Title="Manage Quizzes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageQuizzes.aspx.cs" Inherits="StudySprint.Pages.Admin.ManageQuizzes.ManageQuizzes" %>

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
            margin-bottom: 30px;
        }

        .page-title { 
            font-size: 2rem;
            font-weight: 800;
            color: var(--text-main);
            margin: 0;
        }

        .btn-action {
            background-color: var(--text-main);
            color: var(--card-bg);
            padding: 10px 24px;
            border-radius: 25px; 
            text-decoration: none;
            font-size: 0.95rem;
            font-weight: 600;
            border: none;
            cursor: pointer;
            transition: background-color 0.2s ease, transform 0.1s ease;
            display: inline-block;
        }
        
        .btn-action:hover {
            background-color: #424245;
            color: var(--card-bg);
            transform: translateY(-1px);
        }

        .btn-action:active {
            transform: scale(0.98);
        }

        .btn-action-sm {
            padding: 6px 16px;
            font-size: 0.85rem;
            border-radius: 18px;
            margin-right: 8px;
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

        .quiz-desc-box {
            max-height: 65px; 
            max-width: 350px; 
            overflow-y: auto; 
            padding-right: 5px;
            font-size: 0.9rem;
            color: var(--text-muted);
            white-space: normal; 
            word-break: break-all; 
        }

        .quiz-grid tr:last-child td {
            border-bottom: none;
        }

        .alert { 
            padding: 12px 16px; 
            margin-bottom: 20px; 
            border-radius: var(--radius-md); 
            font-weight: 500; 
            font-size: 0.9rem; 
        }
        .alert-success { 
            background-color: #dcfce7; 
            color: var(--primary-green); 
            border: 1px solid #bbf7d0; 
        }
        .alert-danger { 
            background-color: #fee2e2; 
            color: var(--error-color); 
            border: 1px solid #fecaca; 
        }
        .empty-message { 
            text-align: center; 
            padding: 40px; 
            color: var(--text-muted); 
            font-style: italic; 
            background: var(--card-bg); 
            border-radius: var(--radius-md); 
            box-shadow: var(--shadow-sm); 
        }
    </style>
    
    <script type="text/javascript">
        function confirmDelete() {
            return confirm("Are you sure you want to completely remove this quiz?");
        }
    </script>
</asp:Content>

<asp:Content id="Content2" contentplaceholderid="MainContent" runat="server">
    <div class="manage-quizzes-container">
        
        <div class="header-section">
            <h2 class="page-title">Quiz List</h2>
            <asp:hyperlink id="hlAddQuiz" runat="server" navigateurl="~/Pages/Admin/ManageQuizzes/AddQuiz.aspx" cssclass="btn-action" >+ Create New Quiz</asp:hyperlink>
        </div>

        <asp:label id="lblStatus" runat="server" visible="false"></asp:label>

        <asp:gridview id="gvQuizzes" runat="server" autogeneratecolumns="false" datakeynames="QuizID" 
            cssclass="quiz-grid" gridlines="None" onrowcommand="gvQuizzes_RowCommand" onrowdeleting="gvQuizzes_RowDeleting">
            <columns>
                <asp:boundfield datafield="QuizID" headertext="ID" itemstyle-width="60px" />
                <asp:boundfield datafield="Title" headertext="Quiz Title" itemstyle-width="220px" headerstyle-font-bold="true" />
                
                <asp:templatefield headertext="Description">
                    <itemtemplate>
                        <div class="quiz-desc-box">
                            <%# string.IsNullOrEmpty(Eval("Description")?.ToString()) ? "No description provided." : Eval("Description") %>
                        </div>
                    </itemtemplate>
                </asp:templatefield>
                
                <asp:boundfield datafield="TimeLimit" headertext="Duration (Mins)" itemstyle-width="130px" />
                <asp:boundfield datafield="DateCreated" headertext="Date Created" dataformatstring="{0:yyyy-MM-dd HH:mm}" itemstyle-width="160px" />
                
                <asp:templatefield headertext="Actions" itemstyle-width="160px">
                    <itemtemplate>
                        <asp:hyperlink id="lnkEdit" runat="server" text="Edit" 
                            navigateurl='<%# "EditQuiz.aspx?QuizID=" + Eval("QuizID") %>' cssclass="btn-action btn-action-sm" style="background-color: #2563EB; margin-bottom: 5px;"/>
                        
                        <asp:linkbutton id="btnDelete" runat="server" text="Delete" commandname="Delete" 
                            commandargument='<%# Eval("QuizID") %>' onclientclick="return confirmDelete();" cssclass="btn-action btn-action-sm" style="background-color: #EF4444"/>
                    </itemtemplate>
                </asp:templatefield>
            </columns>
        </asp:gridview>

        <asp:panel id="pnlNoData" runat="server" visible="false" cssclass="empty-message">
            <p>No quizzes available in the system database. Click "+ Create New Quiz" to add one.</p>
        </asp:panel>

    </div>
</asp:Content>