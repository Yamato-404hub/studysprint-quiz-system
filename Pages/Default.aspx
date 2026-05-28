<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="StudySprint.Pages.Default" %>

<asp:Content id="Content1" contentplaceholderid="MainContent" runat="server">
    
    <div class="homepage-container">
        
        <section class="hero-section">
            <div class="bg-shape shape-1"></div>
            <div class="bg-shape shape-2"></div>
            <div class="bg-shape shape-3"></div>

            <div class="hero-content">
                <h1 class="hero-title">Welcome to <span>StudySprint</span></h1>
                <p class="hero-subtitle">Improve your knowledge through quick, focused quizzes and real-time self-assessment. Master your subjects one sprint at a time.</p>
                
                <div class="hero-action-buttons">
                    <a href="Register.aspx" class="btn-pill btn-solid">Sign Up</a>
                    <a href="Login.aspx" class="btn-pill btn-outline">Login</a>
                </div>
            </div>
        </section>

        <section class="features-section">
            
            <div class="feature-card">
                <div class="feature-icon-base">
                    <i class="fa-solid fa-stopwatch"></i>
                </div>
                <h3>Practice Quizzes</h3>
                <p>Test your knowledge with carefully curated quizzes on various topics designed to challenge your limits.</p>
            </div>

            <div class="feature-card">
                <div class="feature-icon-base">
                    <i class="fa-solid fa-bolt"></i>
                </div>
                <h3>Instant Feedback</h3>
                <p>Get immediate results and understand your mistakes with detailed analytics to improve faster.</p>
            </div>

            <div class="feature-card">
                <div class="feature-icon-base">
                    <i class="fa-solid fa-chart-line"></i>
                </div>
                <h3>Track Progress</h3>
                <p>Monitor your improvement over time, track your learning streaks, and identify areas for growth.</p>
            </div>

        </section>

    </div>

</asp:Content>