<%@ Page Title="Quiz Hub" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="QuizHub.aspx.cs" Inherits="StudySprint.Pages.Student.QuizHub.QuizHub" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../../Assets/css/style_student.css" rel="stylesheet" type="text/css" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="content-wrapper">

        <h1 class="page-title">Quiz Hub</h1>

        <div class="card-grid">

            <asp:Repeater ID="rptQuizzes" runat="server">

                <ItemTemplate>

                    <div class="dashboard-card">

                        <h2>
                            <%# Eval("Title") %>
                        </h2>

                        <p>
                            <%# Eval("Description") %>
                        </p>

                        <p style="margin-bottom: 1rem">
                            <strong>Time Limit:</strong>
                            <%# Eval("TimeLimit") %>
                            minutes
                        </p>

                        <asp:Button ID="btnStart"
                            runat="server"
                            Text="Start Quiz"
                            CommandArgument='<%# Eval("QuizID") %>'
                            OnClick="btnStart_Click"
                            UseSubmitBehavior="false"
                            CssClass="btn-primary" />

                    </div>

                </ItemTemplate>

            </asp:Repeater>

        </div>

    </div>

</asp:Content>