<%@ Page Title="Result Summary"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="ResultSummary.aspx.cs"
    Inherits="StudySprint.Pages.Student.QuizHub.ResultSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../../Assets/css/style_student.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2"
    ContentPlaceHolderID="MainContent"
    runat="server">

<div class="content-wrapper">

    <div class="dashboard-card result-summary-card">

        <h1 class="page-title">
            Quiz Completed
        </h1>

        <p style="font-size:20px; color:#64748B; margin-bottom:40px;">
            Your quiz result has been successfully saved.
        </p>

        <div style="display:flex; justify-content:center; gap:24px; flex-wrap:wrap; margin-bottom:40px;">

            <div class="dashboard-card" style="width:220px; text-align:center; border-top:6px solid #2563EB;">
                <h3 style="color:#64748B;">Score</h3>
                <asp:Label ID="lblScore"
                    runat="server"
                    style="font-size:42px; font-weight:bold; color:#2563EB;" />
            </div>

            <div class="dashboard-card" style="width:220px; text-align:center; border-top:6px solid #16A34A;">
                <h3 style="color:#64748B;">Correct</h3>
                <asp:Label ID="lblCorrect"
                    runat="server"
                    style="font-size:42px; font-weight:bold; color:#16A34A;" />
            </div>

            <div class="dashboard-card" style="width:220px; text-align:center; border-top:6px solid #EF4444;">
                <h3 style="color:#64748B;">Incorrect</h3>
                <asp:Label ID="lblIncorrect"
                    runat="server"
                    style="font-size:42px; font-weight:bold; color:#EF4444;" />
            </div>

        </div>

        <div style="display:flex; justify-content:center; gap:18px; flex-wrap:wrap;">

            <asp:Button ID="btnTryAgain"
                runat="server"
                Text="Try Again"
                CssClass="btn-primary"
                OnClick="btnTryAgain_Click" />

            <asp:Button ID="btnReviewAnswers"
                runat="server"
                Text="Review Answers"
                CssClass="btn-primary"
                OnClick="btnReviewAnswers_Click" />

        </div>

    </div>

</div>

</asp:Content>