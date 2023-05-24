create database moq 
drop database moq
create table Books
(
Book_id int identity primary key,
Book_Title varchar(100),
Book_Author varchar(100),
Stock int
)
alter table Books add IsIssued bit
alter table Books add Stock int
drop table Books
drop table Students

create table Students
(
Student_id int identity primary key,
Student_Name varchar(50),
Student_Dept varchar(50),
)
drop table Students
create table Issue
(
Issued_id int identity primary key,
Student_id int,
Book_id int
)
drop table Issue
create table Users
(
user_name varchar(50),
password varchar(50)
)

select * from Books
select * from Students
select * from Issue
select * from Users


Insert into users values('sirisha','sirisha@123'),('jaya','jaya@23');
Insert into users values('sai','sai@19');

