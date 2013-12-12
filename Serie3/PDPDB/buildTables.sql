DECLARE @dbname nvarchar(128);
SET @dbname = 'PDPDB';

IF (NOT EXISTS (SELECT name 
			FROM master.dbo.sysdatabases 
			WHERE ('[' + name + ']' = @dbname OR name = @dbname)))
	CREATE DATABASE PDPDB;
GO

USE PDPDB;

-- DROP
if OBJECT_ID('Action') is not null
	drop table [Action];
if OBJECT_ID('Permission') is not null
	drop table [Permission];
if OBJECT_ID('Resource') is not null
	drop table [Resouce];
if OBJECT_ID('Role') is not null
	drop table [Role];
if OBJECT_ID('User') is not null
	drop table [User];
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

CREATE TABLE [Resource]
	(
	resourceId int IDENTITY,		
	resourceName varchar(64) UNIQUE,
	constraint PK_resourceId primary key (resourceId)
	);
GO

CREATE TABLE [Permission]
	(
	permissionId int IDENTITY,		
	permissionName varchar(64) UNIQUE,
	constraint PK_permissionId primary key (permissionId)
	);
GO

CREATE TABLE [Action]
	(
	actionId int IDENTITY,		
	actionName varchar(64) UNIQUE,
	constraint PK_actionId primary key (actionId)
	);
GO