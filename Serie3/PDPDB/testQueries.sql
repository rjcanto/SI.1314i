USE PDPDB

--getUsersWithPermission
SELECT [User].*
FROM [User] INNER JOIN UserAssignment
ON ([User].userId = UserAssignment.userId)
INNER JOIN PermissionAssignment
ON (UserAssignment.roleId = PermissionAssignment.roleId)
WHERE (PermissionAssignment.actionID = '3' AND PermissionAssignment.resourceId = '3') ;


--getUsersWithRole
SELECT [User].*
FROM [User] INNER JOIN UserAssignment
ON ([User].userID = UserAssignment.userID)
WHERE UserAssignment.roleId = '4';

--getRolesOfUser
SELECT Role.*
FROM Role INNER JOIN UserAssignment
ON (Role.roleId = UserAssignment.roleId)
INNER JOIN [User]
ON ([User].userId = UserAssignment.userID)
WHERE [User].username = 'Ricardo' ;

--getPermissionsOfUser
SELECT Permission.*
FROM Permission INNER JOIN PermissionAssignment
ON ((Permission.actionId = PermissionAssignment.actionId)
	AND (Permission.resourceId = PermissionAssignment.resourceId))
INNER JOIN Role
ON (PermissionAssignment.roleId = Role.roleId)
INNER JOIN UserAssignment
ON (UserAssignment.roleId = Role.roleId)
WHERE (Role.roleId = 1);

--getActionsAllowedOfUserWithResource
SELECT Action.*
FROM Action INNER JOIN PermissionAssignment
ON (Action.actionId = PermissionAssignment.actionId)
INNER JOIN Role
ON (PermissionAssignment.roleId = Role.roleId)
INNER JOIN UserAssignment
ON (UserAssignment.roleId = Role.roleId)
INNER JOIN [User]
ON ([User].userId = UserAssignment.userId)
WHERE (PermissionAssignment.resourceId = 1 
AND [User].username = 'Luís');

--isActionAllowedOfUserWithResource
SELECT [User].*
FROM [User] INNER JOIN UserAssignment
ON ([User].userId = UserAssignment.userId)
INNER JOIN Role
ON (UserAssignment.roleId = Role.roleId)
INNER JOIN PermissionAssignment
ON (PermissionAssignment.roleId = Role.roleId)
WHERE (PermissionAssignment.resourceId = 1 
AND PermissionAssignment.actionId = 5
AND [User].username = 'Ricardo');



-----------------------
-- Recursive Queries --
-----------------------


--isActionAllowedOfUserWithResource
SELECT 
	Role.* 
FROM 
	Role
	INNER JOIN RoleHierarchy ON Role.roleId = RoleHierarchy.juniorRoleId
WHERE
	RoleHierarchy.roleId = 3
