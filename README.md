# studysprint-quiz-system
ASP.NET-based quiz and result management system with timer and review features.
# StudySprint Quiz System

## Overview
StudySprint is a web-based quiz and result management system developed using ASP.NET and SQL Server.

The system allows students to take quizzes, review answers, and track their performance through an interactive and user-friendly platform.

This project was developed as an academic group project at Asia Pacific University of Technology & Innovation (APU).

## Features

### Student Features
- Take quizzes online
- Timer-based quiz system
- Automatic score calculation
- Review submitted answers
- View result summaries

### Admin Features
- Create and manage quizzes
- Add questions and answer options
- Publish or deactivate quizzes
- Manage quiz content through database integration

## Database Design

The system uses a relational database structure designed for quiz management and answer tracking.

### Main Entities
- Student
- Admin
- Quiz
- Question
- Option
- Result
- StudentAnswer

### Database Relationships
- Admin creates quizzes
- Quiz contains multiple questions
- Questions contain multiple options
- Students take quizzes
- Results are recorded for each attempt
- Student answers are linked to selected options
- 

## ERD (Entity Relationship Diagram)

## Tech Stack

### Backend
- ASP.NET Web Forms
- C#

### Database
- SQL Server

### Frontend
- HTML
- CSS

### Tools
- Visual Studio
- GitHub

## System Functions

### Quiz Timer
Implemented a countdown timer system that automatically handles quiz timing and submission flow.

### Result Summary
Displays:
- Total score
- Quiz completion information
- Performance results

### Answer Review
Allows students to:
- Review selected answers
- Compare correct and incorrect responses
- Improve learning understanding
- 

## What I Learned

Through this project, I learned:

- ASP.NET web application development
- SQL database integration
- Session handling
- Team-based software development
- Database relationship design
- Backend logic implementation
- UI/UX workflow structure

## Future Improvements

- User authentication system
- AI-generated quiz questions
- Leaderboard and ranking system
- Mobile responsive design
- Analytics dashboard
- Question difficulty analysis

## Contributors

Academic Group Project  
Asia Pacific University of Technology & Innovation (APU)

## Project Status

Completed academic project with core quiz management functionality implemented.
