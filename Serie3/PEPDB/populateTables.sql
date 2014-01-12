USE PEPDB
SET NOCOUNT ON;

-- Populate USER
INSERT INTO [User](username) VALUES ('ricardo');
INSERT INTO [User](username) VALUES ('luis');
INSERT INTO [User](username) VALUES ('user');
INSERT INTO [User](username) VALUES ('guest');

-- Populate Role
INSERT INTO [Role](roleName) VALUES ('admin');
INSERT INTO [Role](roleName) VALUES ('user');
INSERT INTO [Role](roleName) VALUES ('guest');

-- Populate RoleHierarchy
INSERT INTO [RoleHierarchy](roleId, juniorRoleId) VALUES (1, 2);
INSERT INTO [RoleHierarchy](roleId, juniorRoleId) VALUES (2, 3);

-- Populate UserAssignment
--Admin
INSERT INTO [UserAssignment](userId, roleId) VALUES (1, 1);
INSERT INTO [UserAssignment](userId, roleId) VALUES (2, 1);
--user
INSERT INTO [UserAssignment](userId, roleId) VALUES (3, 2);
--guest
INSERT INTO [UserAssignment](userId, roleId) VALUES (4, 3);

-- Populate Action
INSERT INTO [Action](actionName) VALUES ('get');
INSERT INTO [Action](actionName) VALUES ('post');
INSERT INTO [Action](actionName) VALUES ('put');
INSERT INTO [Action](actionName) VALUES ('head');
INSERT INTO [Action](actionName) VALUES ('delete');

-- Populate Resources
INSERT INTO [Resource](resourceName) VALUES ('/Home/Resource/ricardo');
INSERT INTO [Resource](resourceName) VALUES ('/Home/Resource/luis');
INSERT INTO [Resource](resourceName) VALUES ('/Home/Resource/user');
INSERT INTO [Resource](resourceName) VALUES ('/Home/Resource/guest');

-- Populate Permission
INSERT INTO [Permission](resourceId, actionId) VALUES (1, 1);
INSERT INTO [Permission](resourceId, actionId) VALUES (2, 1);
INSERT INTO [Permission](resourceId, actionId) VALUES (3, 1);
INSERT INTO [Permission](resourceId, actionId) VALUES (4, 1);

-- Populate PermissionAssignment
--Admin
INSERT INTO [PermissionAssignment](roleId, resourceId, actionId) VALUES (1, 1, 1);
INSERT INTO [PermissionAssignment](roleId, resourceId, actionId) VALUES (1, 2, 1);
INSERT INTO [PermissionAssignment](roleId, resourceId, actionId) VALUES (2, 3, 1);
INSERT INTO [PermissionAssignment](roleId, resourceId, actionId) VALUES (3, 4, 1);
