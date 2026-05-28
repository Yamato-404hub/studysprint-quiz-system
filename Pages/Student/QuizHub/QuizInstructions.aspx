<%@ Page Title="Quiz Instructions"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="QuizInstructions.aspx.cs"
    Inherits="StudySprint.Pages.Student.QuizHub.QuizInstructions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../../Assets/css/style_student.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2"
    ContentPlaceHolderID="MainContent"
    runat="server">

<div class="content-wrapper">

    <!-- Header -->
    <div style="
        display:flex;
        justify-content:space-between;
        align-items:center;
        margin-bottom:30px;
    ">

        <h1 class="page-title">
            Quiz Instructions
        </h1>

        <asp:Button ID="btnBack"
            runat="server"
            Text="← Back"
            CssClass="btn-secondary"
            OnClick="btnBack_Click" />

    </div>

    <!-- Main Card -->
    <div class="dashboard-card"
        style="
        max-width:850px;
        margin:auto;
        padding:50px;
    ">

        <!-- Quiz Title -->
        <h2 style="
            text-align:center;
            font-size:42px;
            margin-bottom:35px;
        ">

            <asp:Label ID="lblTitle"
                runat="server">
            </asp:Label>

        </h2>

        <!-- Description -->
        <div style="
            background:#F8FAFD;
            border-radius:15px;
            padding:25px;
            border:1px solid #E2E8F0;
        ">

            <p style="
                font-size:20px;
                color:#555;
                line-height:1.8;
                margin:0;
            ">

                <asp:Label ID="lblDescription"
                    runat="server">
                </asp:Label>

            </p>

        </div>

        <!-- Quiz Info -->
        <div class="dashboard-card"
            style="
            margin-top:35px;
        ">

            <div style="
                display:flex;
                justify-content:space-between;
                margin-bottom:20px;
                font-size:22px;
            ">

                <span>
                    <strong>Total Questions</strong>
                </span>

                <span>
                    <asp:Label ID="lblQuestionCount"
                        runat="server">
                    </asp:Label>
                </span>

            </div>

            <div style="
                display:flex;
                justify-content:space-between;
                font-size:22px;
            ">

                <span>
                    <strong>Time Limit</strong>
                </span>

                <span>

                    <asp:Label ID="lblTimeLimit"
                        runat="server">
                    </asp:Label>

                    minutes

                </span>

            </div>

        </div>

        <!-- Rules -->
        <div style="
            margin-top:35px;
            padding:25px;
            background:#FFF8E7;
            border-radius:15px;
            border:1px solid #FFE4A3;
        ">

            <h3 style="
                color:#D97706;
                margin-bottom:15px;
            ">
                Important Rules
            </h3>

            <ul style="
                line-height:2;
                font-size:18px;
                padding-left:25px;
            ">

                <li>Select an answer before moving to the next question.</li>

                <li>You cannot go back to previous questions.</li>

                <li>If time runs out, the quiz will restart automatically.</li>

                <li>Your score will be saved automatically after submission.</li>

            </ul>

        </div>

        <!-- Start Button -->
        <div style="
            text-align:center;
            margin-top:50px;
        ">

            <asp:Button ID="btnStartQuiz"
                runat="server"
                Text="Start Quiz"
                CssClass="btn-primary"
                style="
                    width:250px;
                    font-size:20px;
                "
                OnClick="btnStartQuiz_Click" />

        </div>

    </div>

</div>

</asp:Content>