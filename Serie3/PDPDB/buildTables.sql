DECLARE @dbname nvarchar(128);
SET @dbname = 'PDPDB';

IF (NOT EXISTS (SELECT name 
			FROM master.dbo.sysdatabases 
			WHERE ('[' + name + ']' = @dbname OR name = @dbname)))
	CREATE DATABASE PDPDB;
GO

USE PDPDB;

-- DROP
if OBJECT_ID('RolePermissions') is not null
	drop table [RolePermissions];
if OBJECT_ID('UserRoles') is not null
	drop table [UserRoles];
if OBJECT_ID('Action') is not null
	drop table [Action];
if OBJECT_ID('Permission') is not null
	drop table [Permission];
if OBJECT_ID('Resource') is not null
	drop table [Resource];
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
	parentRoleId int,
	constraint PK_roleId primary key (roleId),
	constraint FK_Role_parentRoleId foreign key(parentRoleId) references [Role](roleId)
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

CREATE TABLE UserRoles
	(
	userId int,		
	roleId int,
	constraint PK_UserRoles primary key (userId, roleId),
	constraint FK_UserRoles_userId foreign key(userId) references [User](userId),
	constraint FK_UserRoles_roleId foreign key(roleId) references [Role](roleId)
	);
GO

CREATE TABLE RolePermissions
	(
	roleId int,
	permissionId int,
	constraint PK_RolePermissions primary key (roleId, permissionId),	
	constraint FK_RolePermissions_roleId foreign key(roleId) references [Role](roleId),
	constraint FK_RolePermissions_userId foreign key(permissionId) references [Permission](permissionId)
	);
GO