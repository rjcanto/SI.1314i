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

