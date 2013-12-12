if OBJECT_ID('PDPDB') is null
	CREATE DATABASE PDPDB;
GO

USE PDPDB;

-- DROP
if OBJECT_ID('User') is not null
	drop table [User];
if OBJECT_ID('Role') is not null
	drop table [Role];
GO

-- CREATE
CREATE TABLE [User]
	(
	userId int IDENTITY,		
	username varchar(64) UNIQUE,
	constraint PK_userId primary key (userId)
	);
GO

CREATE TABLE [Role]
	(
	roleId int IDENTITY,		
	rolename varchar(64) UNIQUE,
	constraint PK_roleId primary key (roleId)
	);
GO
