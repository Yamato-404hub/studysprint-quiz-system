    <%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageStudents.aspx.cs" Inherits="StudySprint.Pages.Admin.ManageStudent.ManageStudents" %>

    <asp:Content id="Content1" contentplaceholderid="head" runat="server">
        <link href="../../../Assets/css/style_admin.css" rel="stylesheet" type="text/css" />
        <style>
            .filter-wrapper {
                margin-bottom: 20px;
                display: flex;
                align-items: center;
                gap: 12px;
            }
        
            .filter-label {
                font-weight: 600;
                color: #1E293B;
                font-size: 0.95rem;
            }

            .filter-dropdown {
                padding: 8px 16px;
                border-radius: 20px;
                border: 1px solid #E2E8F0;
                background-color: #F8FAFC;
                font-family: inherit;
                font-weight: 600;
                color: #1E293B;
                cursor: pointer;
                outline: none;
                transition: all 0.2s ease;
            }

            .filter-dropdown:focus {
                border-color: #1E293B;
                box-shadow: 0 0 0 2px rgba(30, 41, 59, 0.1);
            }

            .btn-danger {
                background-color: #EF4444 !important; 
                color: #ffffff !important;
            }

            .btn-danger:hover {
                background-color: #dc2626 !important;
            }

            .action-buttons {
                margin-top: 25px;
                display: flex;
                gap: 12px;
                flex-wrap: wrap;
            }
        </style>  
    </asp:Content>

    <asp:Content id="Content2" contentplaceholderid="MainContent" runat="server">
        <div class="manage-students-container">
            <h2 class="page-title">Student List</h2>

            <div class="filter-wrapper">
                <label class="filter-label">Filter by Status:</label>
                <asp:dropdownlist id="ddlStudentStatus" runat="server" autopostback="true" onselectedindexchanged="ddlStudentStatus_SelectedIndexChanged" cssclass="filter-dropdown">
                    <asp:listitem text="Active Students" value="1" selected="True"></asp:listitem>
                    <asp:listitem text="Deactivated Students" value="0"></asp:listitem>
                    <asp:listitem text="All Students" value="All"></asp:listitem>
                </asp:dropdownlist>
            </div>

            <asp:gridview id="gvStudents" runat="server" autogeneratecolumns="false" datakeynames="StudentID" cssclass="student-grid" gridlines="None">
                <columns>
                    <asp:templatefield>
                        <itemtemplate>
                            <asp:checkbox id="chkSelect" runat="server" />
                        </itemtemplate>
                    </asp:templatefield>
                    <asp:boundfield headertext="Student ID" datafield="StudentID" />
                    <asp:boundfield headertext="Full Name" datafield="FullName" />
                    <asp:boundfield headertext="Email" datafield="Email" />
                    <asp:boundfield headertext="Date Entered" datafield="DateRegistered" />
                </columns>

                <EmptyDataTemplate>
                    <div class="empty-data">No student records found based on your filter criteria.</div>
                </EmptyDataTemplate>
            </asp:gridview>

            <asp:label id="lblMessage" runat="server" cssclass="form-error-msg"></asp:label>
        
            <div class="action-buttons">
                <asp:button id="btnViewProgress" runat="server" onclick="btnViewProgress_Click1" text="View Student Progress" cssclass="btn-action" />
                <asp:button id="btnAddStudent" runat="server" onclick="btnAddStudent_Click" text="Add Student" cssclass="btn-action" />
                <asp:button id="btnStatusAction" runat="server" onclick="btnStatusAction_Click" text="Deactivate Account" cssclass="btn-action" />
                <asp:button id="btnDeleteStudent" runat="server" onclick="btnDeleteStudent_Click" text="Delete Student" cssclass="btn-action btn-danger" />
            </div>
        </div>
    </asp:Content>