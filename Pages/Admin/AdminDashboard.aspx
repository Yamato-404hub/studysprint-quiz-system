<%@ Page Title="Admin Dashboard" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminDashboard.aspx.cs" Inherits="StudySprint.Pages.Admin.AdminDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        :root {
            --bg-page: #F8FAFC;
            --text-title: #0F172A;
            --text-body: #64748B;
            --primary-blue: #2563EB;
            --primary-hover: #1D4ED8;
            --card-bg: #FFFFFF;
            --border-light: #E2E8F0;
            --shadow-base: 0 4px 6px -1px rgba(0, 0, 0, 0.05), 0 2px 4px -1px rgba(0, 0, 0, 0.03);
            --shadow-hover: 0 20px 25px -5px rgba(0, 0, 0, 0.1), 0 10px 10px -5px rgba(0, 0, 0, 0.04);
        }

        .dashboard-wrapper {
            max-width: 1200px;
            margin: 0 auto;
            padding: 50px 20px;
            font-family: -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, Helvetica, Arial, sans-serif;
            background-color: var(--bg-page);
            border-radius: 20px;
        }

        .dash-title {
            font-size: 38px;
            font-weight: 800;
            color: var(--text-title);
            margin-bottom: 8px;
            letter-spacing: -0.5px;
        }

        .dash-subtitle {
            font-size: 16px;
            color: var(--text-body);
            margin-bottom: 45px;
        }

        .metrics-grid {
            display: grid;
            grid-template-columns: repeat(4, 1fr);
            gap: 24px;
            margin-bottom: 60px;
        }

        .metric-card {
            background: var(--card-bg);
            border: 1px solid var(--border-light);
            border-radius: 16px;
            padding: 30px 25px;
            box-shadow: var(--shadow-base);
            transition: all 0.3s ease;
            position: relative;
            overflow: hidden;
        }

        .metric-card::before {
            content: '';
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 4px;
            background: var(--primary-blue);
            opacity: 0;
            transition: opacity 0.3s ease;
        }

        .metric-card:hover {
            transform: translateY(-5px);
            box-shadow: var(--shadow-hover);
        }

        .metric-card:hover::before {
            opacity: 1;
        }

        .metric-label {
            display: block;
            font-size: 14px;
            font-weight: 600;
            color: var(--text-body);
            text-transform: uppercase;
            letter-spacing: 0.5px;
            margin-bottom: 12px;
        }

        .metric-value {
            display: block;
            font-size: 42px;
            font-weight: 800;
            color: var(--text-title);
            line-height: 1;
        }

        .section-title {
            font-size: 26px;
            font-weight: 800;
            color: var(--text-title);
            margin-bottom: 25px;
            display: flex;
            align-items: center;
        }

        .controls-grid {
            display: grid;
            grid-template-columns: repeat(3, 1fr);
            gap: 24px;
        }

        .control-card {
            background: var(--card-bg);
            border: 1px solid var(--border-light);
            border-radius: 20px;
            padding: 40px 30px;
            display: flex;
            flex-direction: column;
            align-items: center;
            text-align: center;
            box-shadow: var(--shadow-base);
            transition: all 0.3s ease;
        }

        .control-card:hover {
            transform: translateY(-5px);
            box-shadow: var(--shadow-hover);
            border-color: #CBD5E1;
        }

        .icon-base {
            width: 70px;
            height: 70px;
            background: #EFF6FF;
            border-radius: 50%;
            display: flex;
            justify-content: center;
            align-items: center;
            font-size: 30px;
            margin-bottom: 20px;
            color: var(--primary-blue);
            transition: transform 0.3s ease;
        }

        .control-card:hover .icon-base {
            transform: scale(1.1) rotate(5deg);
        }

        .control-title {
            font-size: 20px;
            font-weight: 700;
            color: var(--text-title);
            margin-bottom: 12px;
        }

        .control-desc {
            font-size: 15px;
            color: var(--text-body);
            margin-bottom: 35px;
            line-height: 1.6;
            flex-grow: 1;
        }

        .btn-access {
            display: block;
            width: 100%;
            background: linear-gradient(135deg, var(--primary-blue), #1E40AF);
            color: #ffffff;
            text-align: center;
            padding: 14px 0;
            border-radius: 30px;
            font-size: 16px;
            font-weight: 600;
            text-decoration: none;
            box-shadow: 0 4px 12px rgba(37, 99, 235, 0.3);
            transition: all 0.2s ease;
        }

        .btn-access:hover {
            background: linear-gradient(135deg, var(--primary-hover), #1E3A8A);
            color: #ffffff;
            text-decoration: none;
            box-shadow: 0 6px 16px rgba(37, 99, 235, 0.4);
            transform: translateY(-2px);
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="dashboard-wrapper">
        
        <h1 class="dash-title">Admin Dashboard</h1>
        <p class="dash-subtitle">Manage your learning platform and monitor analytics.</p>

        <div class="metrics-grid">
            <div class="metric-card">
                <span class="metric-label">Total Students</span>
                <asp:label ID="lblTotalStudents" runat="server" CssClass="metric-value">0</asp:label>
            </div>
            <div class="metric-card">
                <span class="metric-label">Total Quiz</span>
                <asp:label ID="lblTotalQuestions" runat="server" CssClass="metric-value">0</asp:label>
            </div>
            <div class="metric-card">
                <span class="metric-label">Questions Taken</span>
                <asp:label ID="lblQuestionsTaken" runat="server" CssClass="metric-value">0</asp:label>
            </div>
            <div class="metric-card">
                <span class="metric-label">Average Score</span>
                <asp:label ID="lblAvgScore" runat="server" CssClass="metric-value">0%</asp:label>
            </div>
        </div>

        <h2 class="section-title">Admin Controls</h2>

        <div class="controls-grid">
            
            <div class="control-card">
                <div class="icon-base"><i class="fa-solid fa-file-signature"></i></div>
                <h3 class="control-title">Manage Quizzes</h3>
                <p class="control-desc">Create, edit, or delete quizzes and manage the assessment pool.</p>
                <asp:hyperlink ID="lnkManageQuestions" runat="server" NavigateUrl="~/Pages/Admin/ManageQuizzes/ManageQuizzes.aspx" CssClass="btn-access">Access Module</asp:hyperlink>
            </div>

            <div class="control-card">
                <div class="icon-base"><i class="fa-solid fa-users-gear"></i></div>
                <h3 class="control-title">Manage Users</h3>
                <p class="control-desc">View student accounts, oversee platform access, and manage user details.</p>
                <asp:hyperlink ID="lnkManageUsers" runat="server" NavigateUrl="~/Pages/Admin/ManageStudent/ManageStudents.aspx" CssClass="btn-access">Access Module</asp:hyperlink>
            </div>

            <div class="control-card">
                <div class="icon-base"><i class="fa-solid fa-chart-line"></i></div>
                <h3 class="control-title">View Results</h3>
                <p class="control-desc">Monitor student performance metrics and overall platform analytics.</p>
                <asp:hyperlink ID="lnkViewResults" runat="server" NavigateUrl="~/Pages/Admin/ManageStudent/ViewStudentProgress.aspx" CssClass="btn-access">Access Module</asp:hyperlink>
            </div>

        </div>

    </div>

</asp:Content>