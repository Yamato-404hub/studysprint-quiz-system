<%@ Page Title="Take Quiz"
    Language="C#"
    MasterPageFile="~/Site.Master"
    AutoEventWireup="true"
    CodeBehind="TakeQuiz.aspx.cs"
    Inherits="StudySprint.Pages.Student.QuizHub.TakeQuiz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../../Assets/css/style_student.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2"
    ContentPlaceHolderID="MainContent"
    runat="server">

<div class="content-wrapper">

    <!-- Top Section -->
    <div style="
        display:flex;
        justify-content:space-between;
        align-items:center;
        margin-bottom:30px;
        flex-wrap:wrap;
        gap:20px;
    ">

        <div>
            <h1 class="page-title">
                <asp:label ID="lblQuizTitle" runat="server"></asp:label>
            </h1>

            <p style="
                font-size:20px;
                color:#64748B;
            ">
                <asp:Label ID="lblQuestionNo"
                    runat="server">
                </asp:Label>
            </p>
        </div>

        <!-- Timer -->
        <div class="dashboard-card"
            style="
            padding:18px 30px;
            min-width:180px;
            text-align:center;
        ">

            <asp:Label ID="lblTimer"
                runat="server"
                Text="Time: 60"
                Font-Size="26px"
                ForeColor="#EF4444"
                Font-Bold="true">
            </asp:Label>

        </div>

    </div>

    <!-- Time Up Message -->
    <div style="
        text-align:center;
        margin-bottom:20px;
    ">

        <asp:Label ID="lblTimeUp"
            runat="server"
            CssClass="error-text"
            Font-Size="22px"
            Font-Bold="true">
        </asp:Label>

    </div>

    <!-- Question Card -->
    <div class="dashboard-card"
        style="
        max-width:850px;
        margin:40px auto;
        padding:45px;
    ">

        <!-- Question -->
        <h2 style="
            font-size:32px;
            color:#1E293B;
            margin-bottom:35px;
            line-height:1.5;
        ">

            <asp:Label ID="lblQuestionText"
                runat="server">
            </asp:Label>

        </h2>

        <!-- Answer Options -->
        <div class="answer-options">

            <asp:RadioButtonList ID="rblAnswers"
                runat="server">
            </asp:RadioButtonList>

        </div>

        <!-- Error Message -->
        <div style="margin-top:25px;">

            <asp:Label ID="lblError"
                runat="server"
                CssClass="error-text"
                Font-Size="18px"
                Font-Bold="true">
            </asp:Label>

        </div>

    </div>

    <!-- Next Button -->
    <div style="text-align:center;">

        <asp:Button ID="btnNext"
            runat="server"
            Text="Next Question"
            CssClass="btn-primary"
            style="
                width:260px;
                font-size:20px;
            "
            OnClick="btnNext_Click" />

    </div>

</div>

<script>
    let timeLeft = <%= QuizTimeLimitSeconds %>;

    let timer = setInterval(function () {
        document.getElementById("<%= lblTimer.ClientID %>").innerText =
            "Time: " + timeLeft;

        timeLeft--;

        if (timeLeft < 0) {
            clearInterval(timer);
            alert("Time is up! Quiz will restart.");
            window.location.href = "TakeQuiz.aspx?QuizID=<%= Session["QuizID"] %>&reset=true";
        }
    }, 1000);
</script>

</asp:Content>