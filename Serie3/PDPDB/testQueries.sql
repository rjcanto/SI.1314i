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
INNER JOIN [User]
ON ([User].userId = UserAssignment.userId)
WHERE ([User].username = 'Luís');

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
WHERE (PermissionAssignment.resourceId = 2 
AND PermissionAssignment.actionId = 2 
AND [User].username = 'Elsa');


--Get Roles with recursivity
WITH Roles (SeniorRoleId, JuniorRoleId, [Level])
AS
(
    -- Anchor
    SELECT
        rh.roleId AS SeniorRoleId,
        rh.juniorRoleId,
        0 AS [Level]
    FROM
        RoleHierarchy rh
    WHERE
        rh.roleId NOT IN (SELECT juniorRoleId FROM RoleHierarchy)
    UNION ALL
    -- Recursive
    SELECT
        rh.roleId AS SeniorRoleId,
        rh.juniorRoleId,
        [Level] + 1
    FROM
        RoleHierarchy rh
        INNER JOIN Roles r ON r.juniorRoleId = rh.roleId
)
SELECT
    r1.rolename AS SeniorRole,
    r2.rolename AS JuniorRole,
    rh.[Level]
FROM
    Roles rh
    INNER JOIN Role r1 ON r1.roleId = rh.SeniorRoleId
    INNER JOIN Role r2 ON r2.roleId = rh.JuniorRoleId
ORDER BY
    [Level], SeniorRole