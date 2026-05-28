<%@ Page Title="My Progress" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyProgress.aspx.cs" Inherits="StudySprint.Pages.Student.MyProgress.MyProgress" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../../Assets/css/style_student.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="content-wrapper">

        <div style="display: flex; justify-content: space-between; align-items: center; margin-bottom: 30px; width: 100%;">
            
            <h1 class="page-title" style="margin-bottom: 0;">My Progress</h1>
            
        </div>

        <div class="progress-card">
            
            <h2 class="progress-section-title">Quiz History</h2>

            <div class="table-responsive-wrapper">
                <asp:gridview ID="gvResults"
                    runat="server"
                    AutoGenerateColumns="False"
                    DataKeyNames="ResultID"
                    OnRowCommand="gvResults_RowCommand"
                    CssClass="styled-table"
                    GridLines="None">

                    <columns>
                        <asp:boundfield DataField="Title" HeaderText="Quiz Title" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" />
                        <asp:boundfield DataField="ScoreText" HeaderText="Score" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"/>
                        <asp:boundfield DataField="DateTaken" HeaderText="Date Taken" DataFormatString="{0:MMMM dd, yyyy}" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"/>
                        
                        <asp:templatefield HeaderText="Review" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                            <itemtemplate>
                                <asp:button ID="btnView"
                                    runat="server"
                                    Text="View"
                                    CommandName="ViewResult"
                                    CommandArgument='<%# Eval("ResultID") %>'
                                    CssClass="btn-action" />
                            </itemtemplate>
                        </asp:templatefield>
                    </columns>

                    <emptydatatemplate>
                        <div class="empty-data" style="text-align:center; padding:30px; color:#64748B; font-style:italic;">
                            No quiz history found yet.
                        </div>
                    </emptydatatemplate>

                </asp:gridview>
            </div>

        </div>

    </div>

</asp:Content>