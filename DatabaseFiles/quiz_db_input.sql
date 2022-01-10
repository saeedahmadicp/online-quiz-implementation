use quiz_db;


insert into Question (QuestionStatement, QuestionType, complexityLevel, marks) values (AES_ENCRYPT("this is a question", "key1"), "MCQS", "Medium", 2);
insert into MCQS_Question(QuestionNumber, OptionA, OptionB, OptionC, OptionD, CorrectOption) 
values (1, AES_ENCRYPT("AAA", "key1"), AES_ENCRYPT("BBB", "key1"), AES_ENCRYPT("CCC", "key1"), AES_ENCRYPT("DDD", "key1"), AES_ENCRYPT("AAA", "key1"));

Select QuestionNumber from Question order by QuestionNumber desc limit 1;




SELECT aes_decrypt(QuestionStatement, "key1"), aes_decrypt(OptionA, "key1"), aes_decrypt(OptionB,  "key1"),
aes_decrypt(OptionC, "key1"), aes_decrypt(OptionD, "key1") 
from Question inner join MCQS_Question
on Question.QuestionNumber = MCQS_Question.QuestionNumber;







/*
insert into UserType (UserTypeId, UserType) Values (1, 'Administrator');
insert into UserType (UserTypeId, UserType) Values (3, 'Student');
insert into UserType (UserTypeId, UserType) Values (2, 'Teacher');

insert into Quiz_User(UserID, FullName, Email, PhoneNumber, UserTypeID) Values (1, 'Mahmoud', 'asb@google.com', '311222', '2');
insert into Quiz_Examiner(UserID, GradeScale, Department) Values (1, '14', 'CIS');

insert into Quiz_User(UserID, FullName, Email, PhoneNumber, UserTypeID) Values (2, 'Mahsmoud', 'assb@google.com', '3d11222', '1');
insert into Quiz_Student(UserID, Semester) Values (2, 5);

insert into Quiz_User(UserID, FullName, Email, PhoneNumber, UserTypeID) Values (3, 'Mahsmoud', 'assb@google.com', '31d1222', '3');
insert into Quiz_Admin(UserID) Values(2);



select * from Quiz_User
 inner join Quiz_Examiner where Quiz_User.UserId = Quiz_Examiner.UserID;

*/