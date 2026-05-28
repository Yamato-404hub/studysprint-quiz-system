<%@ Page Title="Review Answers"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="ReviewAnswers.aspx.cs"
    Inherits="StudySprint.Pages.Student.MyProgress.ReviewAnswers" %>

<asp:Content ID="Content1"
    ContentPlaceHolderID="head"
    runat="server">
</asp:Content>

<asp:Content ID="Content2"
    ContentPlaceHolderID="MainContent"
    runat="server">

<div class="content-wrapper">

    <!-- Page Title -->
    <div style="margin-bottom:30px;">

        <h1 class="page-title">
            Review Answers
        </h1>

        <a href="MyProgress.aspx"
           style="
           color:#64748B;
           text-decoration:none;
           font-size:18px;
        ">
            ← Back
        </a>

    </div>

    <!-- Main Card -->
    <div class="dashboard-card"
        style="
        max-width:900px;
        margin:auto;
        padding:40px;
    ">

        <!-- Question Number -->
        <div style="
            font-size:18px;
            color:#64748B;
            margin-bottom:15px;
            font-weight:600;
        ">

            <asp:Label ID="lblQuestionNo"
                runat="server">
            </asp:Label>

        </div>

        <!-- Question -->
        <div style="
            font-size:32px;
            font-weight:bold;
            margin-bottom:40px;
            color:#1E293B;
            line-height:1.4;
        ">

            <asp:Label ID="lblQuestionText"
                runat="server">
            </asp:Label>

        </div>

        <!-- Your Answer -->
        <div id="yourAnswerBox"
            runat="server"
            style="
            padding:25px;
            border-radius:18px;
            margin-bottom:25px;
        ">

            <div style="
                font-size:18px;
                font-weight:bold;
                margin-bottom:10px;
            ">
                Your Answer
            </div>

            <div style="
                font-size:24px;
                font-weight:600;
            ">

                <asp:Label ID="lblYourAnswer"
                    runat="server">
                </asp:Label>

            </div>

        </div>

        <!-- Correct Answer -->
        <div style="
            background:#ECFDF5;
            border:2px solid #22C55E;
            padding:25px;
            border-radius:18px;
            margin-bottom:40px;
        ">

            <div style="
                font-size:18px;
                font-weight:bold;
                margin-bottom:10px;
                color:#15803D;
            ">
                Correct Answer
            </div>

            <div style="
                font-size:24px;
                font-weight:600;
                color:#166534;
            ">

                <asp:Label ID="lblCorrectAnswer"
                    runat="server">
                </asp:Label>

            </div>

        </div>

        <!-- Button -->
        <div style="text-align:center;">

            <asp:Button ID="btnNext"
                runat="server"
                Text="Next Question"
                CssClass="btn-primary"
                Width="260px"
                Height="55px"
                OnClick="btnNext_Click" />

        </div>

    </div>

</div>

</asp:Content>