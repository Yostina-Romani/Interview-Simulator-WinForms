
---

# Interview Simulator - WinForms Project

## Description

A **C# Windows Forms Application** for simulating interview questions and scoring answers.
Users can log in, answer questions from a MySQL database, and get scored automatically based on **keywords** in their answers.

---

## Features

* **Login Form** for user authentication
* **Question Navigation** (Next / Previous)
* **Answer Submission** stored in MySQL database
* **Automatic Scoring** based on keywords
* **Display Correct Keywords** and Total Score
* **Simple and User-friendly UI**

---

## Technologies Used

* **C#** (WinForms)
* **MySQL** database
* **Visual Studio 2022** (or any WinForms compatible IDE)

---

## Database Setup

1. Create a database named `interview_simulator`
2. Create a `user` table for login:

```sql
CREATE TABLE user (
    id INT PRIMARY KEY AUTO_INCREMENT,
    username VARCHAR(50) NOT NULL,
    pass VARCHAR(50) NOT NULL
);
```

3. Create a `questions` table:

```sql
CREATE TABLE questions (
    id INT PRIMARY KEY AUTO_INCREMENT,
    question TEXT,
    keywords TEXT,
    answer TEXT
);
```

4. Insert example user:

```sql
INSERT INTO user(username, pass) VALUES('admin', '1234');
```

5. Insert sample questions with keywords.

---

## How to Run

1. Clone the repository:

```bash
git clone https://github.com/USERNAME/Interview-Simulator-WinForms.git
```

2. Open the solution in **Visual Studio**
3. Update the `connStr` in `Form1.cs` and `Form2.cs` with your MySQL credentials
4. Run the project → Login with your credentials → Start answering questions

---



## Notes

* Currently, passwords are stored as plain text (for learning purposes only)
* The scoring system is **keyword-based**
* Can be extended with multiple users, hashed passwords, and more advanced scoring logic

---

