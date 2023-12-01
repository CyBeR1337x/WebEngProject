CREATE DATABASE MusicDB;
USE MusicDB;

CREATE TABLE Singer(
	sid VARCHAR(20) PRIMARY KEY,
	sname CHAR(30),
	country CHAR(30),
	gender CHAR(1),
	email CHAR(100),
	password CHAR(8),
);

CREATE TABLE Album (
	sid VARCHAR(20),
	ano INT,
	title CHAR(20),
	date DATE,
	coverPath VARCHAR(MAX),
	CONSTRAINT PK_SIDANO PRIMARY KEY (sid, ano),
	CONSTRAINT FK_SID FOREIGN KEY(sid) REFERENCES Singer(sid)
);

CREATE TABLE Song(
	sid varchar(20),
	ano int,
	sno int,
	stitle varchar(30),
	filepath varchar(MAX),
	CONSTRAINT PK_ABC PRIMARY KEY(sid, ano, sno),
	CONSTRAINT FK_YZ FOREIGN KEY(sid, ano) REFERENCES Album(sid, ano)
);

