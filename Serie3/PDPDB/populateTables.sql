USE PDPDB
SET NOCOUNT ON;

-- Populate USER
INSERT INTO [User](username) VALUES ('Ricardo');
INSERT INTO [User](username) VALUES ('Luís');
INSERT INTO [User](username) VALUES ('Pedro');
INSERT INTO [User](username) VALUES ('João');
INSERT INTO [User](username) VALUES ('Teresa');
INSERT INTO [User](username) VALUES ('Lídia');
INSERT INTO [User](username) VALUES ('Sara');
INSERT INTO [User](username) VALUES ('Elsa');
INSERT INTO [User](username) VALUES ('Mário');
INSERT INTO [User](username) VALUES ('Cristina');
INSERT INTO [User](username) VALUES ('Maria');
INSERT INTO [User](username) VALUES ('Miguel');

-- Populate Role
INSERT INTO [Role](roleName) VALUES ('Admin');
INSERT INTO [Role](roleName) VALUES ('Director');
INSERT INTO [Role](roleName) VALUES ('Manager');
INSERT INTO [Role](roleName) VALUES ('Auditor');
INSERT INTO [Role](roleName) VALUES ('User');
INSERT INTO [Role](roleName) VALUES ('Guest');

-- Populate RoleHierarchy
INSERT INTO [RoleHierarchy](roleId, juniorRoleId) VALUES (1, 2);
INSERT INTO [RoleHierarchy](roleId, juniorRoleId) VALUES (1, 3);
INSERT INTO [RoleHierarchy](roleId, juniorRoleId) VALUES (2, 5);
INSERT INTO [RoleHierarchy](roleId, juniorRoleId) VALUES (3, 5);
INSERT INTO [RoleHierarchy](roleId, juniorRoleId) VALUES (4, 5);
INSERT INTO [RoleHierarchy](roleId, juniorRoleId) VALUES (5, 6);

-- Populate UserAssignment
--Admin
INSERT INTO [UserAssignment](userId, roleId) VALUES (1, 1);
--Director
INSERT INTO [UserAssignment](userId, roleId) VALUES (2, 2);
--Manager
INSERT INTO [UserAssignment](userId, roleId) VALUES (3, 3);
--Auditor
INSERT INTO [UserAssignment](userId, roleId) VALUES (4, 4);
--User
INSERT INTO [UserAssignment](userId, roleId) VALUES (5, 5);
INSERT INTO [UserAssignment](userId, roleId) VALUES (6, 5);
INSERT INTO [UserAssignment](userId, roleId) VALUES (7, 5);
INSERT INTO [UserAssignment](userId, roleId) VALUES (8, 5);
INSERT INTO [UserAssignment](userId, roleId) VALUES (9, 5);
INSERT INTO [UserAssignment](userId, roleId) VALUES (10, 5);
--Guest
INSERT INTO [UserAssignment](userId, roleId) VALUES (11, 6);
INSERT INTO [UserAssignment](userId, roleId) VALUES (12, 6)

-- Populate Action
INSERT INTO [Action](actionName) VALUES ('Criar ficheiros e pastas');
INSERT INTO [Action](actionName) VALUES ('Ver o conteúdo de ficheiros e listar conteúdo de pastas');
INSERT INTO [Action](actionName) VALUES ('Alterar ficheiros');
INSERT INTO [Action](actionName) VALUES ('Eliminar ficheiros e pastas');
INSERT INTO [Action](actionName) VALUES ('Executar ficheiros');

-- Populate Resources
INSERT INTO [Resource](resourceName) VALUES ('/folder');
INSERT INTO [Resource](resourceName) VALUES ('/folder/file1.txt');
INSERT INTO [Resource](resourceName) VALUES ('/folder/file2.txt');
INSERT INTO [Resource](resourceName) VALUES ('/folder/file3.txt');
INSERT INTO [Resource](resourceName) VALUES ('/program.exe');

-- Populate Permission
INSERT INTO [Permission](resourceId, actionId) VALUES (1, 1);
INSERT INTO [Permission](resourceId, actionId) VALUES (1, 2);
INSERT INTO [Permission](resourceId, actionId) VALUES (1, 3);
INSERT INTO [Permission](resourceId, actionId) VALUES (1, 4);
INSERT INTO [Permission](resourceId, actionId) VALUES (1, 5);
INSERT INTO [Permission](resourceId, actionId) VALUES (2, 2);
INSERT INTO [Permission](resourceId, actionId) VALUES (2, 3);
INSERT INTO [Permission](resourceId, actionId) VALUES (2, 4);
INSERT INTO [Permission](resourceId, actionId) VALUES (3, 2);
INSERT INTO [Permission](resourceId, actionId) VALUES (3, 3);
INSERT INTO [Permission](resourceId, actionId) VALUES (3, 4);
INSERT INTO [Permission](resourceId, actionId) VALUES (4, 2);
INSERT INTO [Permission](resourceId, actionId) VALUES (5, 5);

-- Populate PermissionAssignment
--Admin
INSERT INTO [PermissionAssignment](roleId, resourceId, actionId) VALUES (1, 1, 1);
INSERT INTO [PermissionAssignment](roleId, resourceId, actionId) VALUES (1, 1, 2);
INSERT INTO [PermissionAssignment](roleId, resourceId, actionId) VALUES (1, 1, 3);
INSERT INTO [PermissionAssignment](roleId, resourceId, actionId) VALUES (1, 1, 4);
INSERT INTO [PermissionAssignment](roleId, resourceId, actionId) VALUES (1, 2, 2);
INSERT INTO [PermissionAssignment](roleId, resourceId, actionId) VALUES (1, 2, 3);
INSERT INTO [PermissionAssignment](roleId, resourceId, actionId) VALUES (1, 2, 4);
INSERT INTO [PermissionAssignment](roleId, resourceId, actionId) VALUES (1, 3, 2);
INSERT INTO [PermissionAssignment](roleId, resourceId, actionId) VALUES (1, 3, 3);
INSERT INTO [PermissionAssignment](roleId, resourceId, actionId) VALUES (1, 3, 4);
INSERT INTO [PermissionAssignment](roleId, resourceId, actionId) VALUES (1, 4, 2);
INSERT INTO [PermissionAssignment](roleId, resourceId, actionId) VALUES (1, 5, 5);
-- Director
INSERT INTO [PermissionAssignment](roleId, resourceId, actionId) VALUES (2, 1, 1);
INSERT INTO [PermissionAssignment](roleId, resourceId, actionId) VALUES (2, 1, 2);
INSERT INTO [PermissionAssignment](roleId, resourceId, actionId) VALUES (2, 1, 3);
INSERT INTO [PermissionAssignment](roleId, resourceId, actionId) VALUES (2, 1, 4);
INSERT INTO [PermissionAssignment](roleId, resourceId, actionId) VALUES (2, 1, 5);
-- Manager
INSERT INTO [PermissionAssignment](roleId, resourceId, actionId) VALUES (3, 1, 1);
INSERT INTO [PermissionAssignment](roleId, resourceId, actionId) VALUES (3, 1, 3);
INSERT INTO [PermissionAssignment](roleId, resourceId, actionId) VALUES (3, 1, 4);
INSERT INTO [PermissionAssignment](roleId, resourceId, actionId) VALUES (3, 1, 5);
INSERT INTO [PermissionAssignment](roleId, resourceId, actionId) VALUES (3, 3, 2);
INSERT INTO [PermissionAssignment](roleId, resourceId, actionId) VALUES (3, 4, 2);
-- Auditor
INSERT INTO [PermissionAssignment](roleId, resourceId, actionId) VALUES (4, 1, 2);
-- User
INSERT INTO [PermissionAssignment](roleId, resourceId, actionId) VALUES (5, 1, 1);
INSERT INTO [PermissionAssignment](roleId, resourceId, actionId) VALUES (5, 1, 5);
-- Guest
INSERT INTO [PermissionAssignment](roleId, resourceId, actionId) VALUES (6, 5, 5);
