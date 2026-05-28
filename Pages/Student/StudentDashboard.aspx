<%@ Page Title="Student Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentDashboard.aspx.cs" Inherits="StudySprint.Pages.Student.StudentDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        :root {
            --bg-body: #F1F5F9;
            --primary-blue: #2563EB;
            --text-dark: #0F172A;
            --text-muted: #64748B;
            --card-white: #FFFFFF;
            --shadow-soft: 0 10px 25px -5px rgba(0, 0, 0, 0.05), 0 8px 10px -6px rgba(0, 0, 0, 0.05);
        }

        .dashboard-container {
            max-width: 1250px;
            margin: 40px auto;
            padding: 0 25px;
            font-family: 'Inter', -apple-system, sans-serif;
        }

        /* Welcome Section */
        .welcome-header {
            margin-bottom: 40px;
        }

        .welcome-title {
            font-size: 36px;
            font-weight: 800;
            color: var(--text-dark);
            margin-bottom: 10px;
        }

        .welcome-subtitle {
            font-size: 16px;
            color: var(--text-muted);
        }

        /* Main Grid Layout */
        .dashboard-main {
            display: grid;
            grid-template-columns: 2fr 1fr;
            gap: 30px;
            align-items: start;
        }

        /* Recommended Quizzes Section */
        .section-heading {
            font-size: 20px;
            font-weight: 700;
            color: var(--text-dark);
            margin-bottom: 20px;
            display: flex;
            align-items: center;
            gap: 10px;
        }

        .quiz-grid {
            display: grid;
            grid-template-columns: repeat(2, 1fr);
            gap: 20px;
        }

        .quiz-card {
            background: var(--card-white);
            border-radius: 20px;
            padding: 25px;
            box-shadow: var(--shadow-soft);
            border: 1px solid #E2E8F0;
            transition: transform 0.2s ease;
        }

        .quiz-card:hover {
            transform: translateY(-5px);
        }

        .quiz-icon {
            width: 50px;
            height: 50px;
            background: #EEF2FF;
            border-radius: 12px;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 24px;
            margin-bottom: 15px;
        }

        .quiz-card h3 {
            font-size: 19px;
            font-weight: 700;
            margin-bottom: 8px;
            color: var(--text-dark);
        }

        .quiz-info {
            font-size: 14px;
            color: var(--text-muted);
            margin-bottom: 20px;
        }

        .badge {
            display: inline-block;
            padding: 4px 12px;
            border-radius: 20px;
            font-size: 12px;
            font-weight: 700;
            margin-bottom: 15px;
            text-transform: uppercase;
        }

        .badge-beginner {
            background: #DCFCE7;
            color: #166534;
        }

        .badge-intermediate {
            background: #DBEAFE;
            color: #1E40AF;
        }

        .badge-advanced {
            background: #FEE2E2;
            color: #991B1B;
        }

        /* Side Card: Past Results */
        .side-card {
            background: var(--card-white);
            border-radius: 20px;
            padding: 25px;
            box-shadow: var(--shadow-soft);
            border: 1px solid #E2E8F0;
        }

        .results-table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 15px;
        }

        .results-table th {
            text-align: left;
            font-size: 12px;
            color: var(--text-muted);
            padding-bottom: 10px;
            border-bottom: 1px solid #F1F5F9;
        }

        .results-table td {
            padding: 12px 0;
            font-size: 14px;
            color: var(--text-dark);
            border-bottom: 1px solid #F8FAFC;
        }

        .score-text {
            font-weight: 700;
            color: var(--primary-blue);
        }

        /* Quote Section */
        .quote-container {
            margin-top: 60px;
            padding: 40px;
            background: linear-gradient(135deg, #1E293B, #0F172A);
            border-radius: 24px;
            text-align: center;
            color: #FFFFFF;
        }

        .quote-text {
            font-size: 20px;
            font-style: italic;
            font-weight: 500;
            margin-bottom: 15px;
            line-height: 1.6;
            opacity: 0.9;
        }

        .quote-author {
            font-size: 14px;
            font-weight: 600;
            color: #94A3B8;
            text-transform: uppercase;
            letter-spacing: 1px;
        }

        .btn-start {
            display: inline-block;
            background: var(--primary-blue);
            color: #fff;
            padding: 10px 20px;
            border-radius: 12px;
            text-decoration: none;
            font-size: 14px;
            font-weight: 600;
            transition: background 0.2s;
        }

        .btn-start:hover {
            background: #1D4ED8;
            color: #fff;
            text-decoration: none;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="dashboard-container">
        
        <div class="welcome-header">
            <h1 class="welcome-title">Welcome back, <asp:label ID="lblStudentName" runat="server" Text="Student"></asp:label>!</h1>
            <p class="welcome-subtitle">Continue improving your knowledge with StudySprint quizzes.</p>
        </div>

        <div class="dashboard-main">
            
            <div class="recommended-section">
                <h2 class="section-heading">Recommended Quizzes</h2>
                
                <div class="quiz-grid">
                    <asp:repeater ID="rptRecommended" runat="server">
                        <itemtemplate>
                            <div class="quiz-card">
                                <div class="quiz-icon"><i class="fa-solid fa-book-open-reader"></i></div>
                                <span class="badge badge-beginner">Beginner</span>
                                <h3><%# Eval("Title") %></h3>
                                <p class="quiz-info">Test your basics with this curated quiz bank.</p>
                                <asp:hyperlink ID="lnkStart" runat="server" NavigateUrl='<%# "~/Pages/Student/QuizHub/TakeQuiz.aspx?QuizID=" + Eval("QuizID") %>' CssClass="btn-start">Start Quiz</asp:hyperlink>
                            </div>
                        </itemtemplate>
                    </asp:repeater>
                </div>

            </div>

            <div class="results-section">
                <div class="side-card">
                    <h2 class="section-heading">Past Results</h2>
                    
                    <asp:gridview ID="gvPastResults" runat="server" AutoGenerateColumns="False" CssClass="results-table" GridLines="None">
                        <columns>
                            <asp:boundfield DataField="Title" HeaderText="QUIZ" />
                            
                            <asp:templatefield HeaderText="SCORE">
                                <itemtemplate>
                                    <span class="score-text"><%# Eval("Score") %> / <%# Eval("MaxScore") %></span>
                                </itemtemplate>
                            </asp:templatefield>
                            
                            <asp:boundfield DataField="DateTaken" HeaderText="DATE" DataFormatString="{0:MMM dd}" />
                        </columns>
                    </asp:gridview>

                </div>
            </div>

        </div>

        <div class="quote-container">
            <p class="quote-text">"<asp:label ID="lblQuote" runat="server"></asp:label>"</p>
            <p class="quote-author">— <asp:label ID="lblAuthor" runat="server"></asp:Label></p>
        </div>

    </div>

</asp:Content>