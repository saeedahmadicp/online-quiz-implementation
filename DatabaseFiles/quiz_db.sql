DROP DATABASE IF EXISTS quiz_db;
CREATE DATABASE quiz_db;
USE quiz_db; 



CREATE TABLE Quiz_User
(
UserID Integer primary key auto_increment,
FullName VARCHAR(50) not null,
Address VARCHAR(70),
Email VARCHAR(30) not null unique,
UserPassword varchar(130) not null,
PhoneNumber VARCHAR(20),
constraint User_AltPK unique (UserID)
);

Create Table Quiz_Examiner
(
ExaminerID integer auto_increment,
UserID integer not null, 
GradeScale Varchar(6),
Department Varchar(10) not null,
foreign key (UserID) references Quiz_User(UserID),
primary key(ExaminerID)
);

Create Table Quiz_Student
(
StudentID integer auto_increment,
marks integer default 0,
UserID Integer,
Semester Integer not null,
Department varchar(5) not null,
foreign key (UserID) references Quiz_User(UserID),
Primary key(StudentID)
);

Create Table Quiz_Admin(
AdminID integer auto_increment,
UserID integer,
foreign key (UserID) references Quiz_User(UserID),
primary key(AdminID)
);


CREATE TABLE Question(
QuestionNumber Integer auto_increment,
QuestionStatement blob not null,
QuestionType Varchar(15) not null,
ComplexityLevel Varchar(15) not null,
Marks integer not null,
primary key(QuestionNumber)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE DESC_Question(
StudentID integer,
QuestionNumber integer,
Answer Varchar(500), 
foreign key (StudentID) references Quiz_User(UserID),
foreign key (QuestionNumber) references Question(QuestionNumber)
);

CREATE TABLE MCQS_Question(
QuestionNumber integer,
OptionA blob not null,
OptionB blob not null,
OptionC blob not null,
OptionD blob not null,
CorrectOption blob not null,
foreign key (QuestionNumber) references Question(QuestionNumber)
)ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE TF_Question(
QuestionNumber integer,
CorrectOption blob not null,
foreign key (QuestionNumber) references Question(QuestionNumber)
)ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;     

Create Table QuizSetting(
NoOfQuestions integer not null,
IsShuffle bool default false,
IsAnswerShuffle bool default false,
timeLimit integer not null,
SetOfQuestionsAtOnce integer default 1,
startTime timestamp not null,
IsNegativeMarking bool default false
);