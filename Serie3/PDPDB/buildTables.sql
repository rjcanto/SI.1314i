DECLARE @dbname nvarchar(128);
SET @dbname = 'PDPDB';

IF (NOT EXISTS (SELECT name 
			FROM master.dbo.sysdatabases 
			WHERE ('[' + name + ']' = @dbname OR name = @dbname)))
	CREATE DATABASE PDPDB;
GO

USE PDPDB;

-- Deltas
if OBJECT_ID('UserRoles') is not null
	drop table [UserRoles];
if OBJECT_ID('RolePermissions') is not null
	drop table [RolePermissions];

-- DROP
if OBJECT_ID('PermissionAssignment') is not null
	drop table [PermissionAssignment];
if OBJECT_ID('Permission') is not null
	drop table [Permission];
if OBJECT_ID('RoleHierarchy') is not null
	drop table [RoleHierarchy];
if OBJECT_ID('UserAssignment') is not null
	drop table [UserAssignment];
if OBJECT_ID('Action') is not null
	drop table [Action];
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
	constraint PK_roleId primary key (roleId),
	);
GO

CREATE TABLE [Resource]
	(
	resourceId int IDENTITY,		
	resourceName varchar(64) UNIQUE,
	constraint PK_resourceId primary key (resourceId)
	);
GO

CREATE TABLE [Action]
	(
	actionId int IDENTITY,		
	actionName varchar(64) UNIQUE,
	constraint PK_actionId primary key (actionId)
	);
GO

CREATE TABLE UserAssignment
	(
	userId int,		
	roleId int,
	constraint PK_UserAssignment primary key (userId, roleId),
	constraint FK_UserAssignment_userId foreign key(userId) references [User](userId),
	constraint FK_UserAssignment_roleId foreign key(roleId) references [Role](roleId)
	);
GO

CREATE TABLE RoleHierarchy
	(
	roleId int,		
	juniorRoleId int,
	constraint PK_JuniorRoles primary key (roleId, juniorRoleId),
	constraint FK_JuniorRoles_userId foreign key(roleId) references [Role](roleId),
	constraint FK_JuniorRoles_juniorRoleId foreign key(roleId) references [Role](roleId)
	);
GO

CREATE TABLE Permission
	(
	resourceId int,
	actionId int,
	constraint PK_PermissionResource primary key (actionId, resourceId),
	constraint FK_PermissionResource_actionId foreign key(actionId) references [Action](actionId),
	constraint FK_PermissionResource_resourceId foreign key(resourceId) references [Resource](resourceId)
	);
GO

CREATE TABLE PermissionAssignment
	(
	roleId int,
	actionId int,
	resourceId int,
	constraint PK_PermissionAssignment primary key (roleId, actionId, resourceId),	
	constraint FK_PermissionAssignment_roleId foreign key(roleId) references [Role](roleId),
	constraint FK_PermissionAssignment_actionId_resourceId foreign key(actionId, resourceId) references [Permission](actionId, resourceId)
	);
GO
