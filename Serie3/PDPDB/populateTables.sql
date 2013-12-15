USE PDPDB

-- Populate USER
INSERT INTO [User](username) values ('Ricardo');
INSERT INTO [User](username) values ('Luís');
INSERT INTO [User](username) values ('Pedro');
INSERT INTO [User](username) values ('João');
INSERT INTO [User](username) values ('Teresa');
INSERT INTO [User](username) values ('Lídia');
INSERT INTO [User](username) values ('Sara');
INSERT INTO [User](username) values ('Elsa');
INSERT INTO [User](username) values ('Mário');
INSERT INTO [User](username) values ('Cristina');
INSERT INTO [User](username) values ('Maria');
INSERT INTO [User](username) values ('Miguel');

-- Populate ROLE
INSERT INTO [Role](roleName) values ('Admin');
INSERT INTO [Role](roleName) values ('Manager');
INSERT INTO [Role](roleName) values ('User');
INSERT INTO [Role](roleName) values ('Guest');

-- Populate UserRoles
--Admin
INSERT INTO [UserRoles](userId, roleId) values (1, 1);
INSERT INTO [UserRoles](userId, roleId) values (2, 1);
--Manager
INSERT INTO [UserRoles](userId, roleId) values (3, 2);
INSERT INTO [UserRoles](userId, roleId) values (4, 2);
--User
INSERT INTO [UserRoles](userId, roleId) values (5, 3);
INSERT INTO [UserRoles](userId, roleId) values (6, 3);
INSERT INTO [UserRoles](userId, roleId) values (7, 3);
INSERT INTO [UserRoles](userId, roleId) values (8, 3);
INSERT INTO [UserRoles](userId, roleId) values (9, 3);
INSERT INTO [UserRoles](userId, roleId) values (10, 3);
--Guest
INSERT INTO [UserRoles](userId, roleId) values (11, 4);
INSERT INTO [UserRoles](userId, roleId) values (12, 4)

--Populate Permission
--INSERT INTO [Permission](permissionName) values ('');

--Populate RolePermissions
--INSERT INTO [RolePermissions](roleId, permissionId) values (11, 4);