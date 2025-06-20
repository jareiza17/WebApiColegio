--Procedimiento almacenado para insertar datos JSON en la tabla Users

CREATE PROCEDURE CreateUser
    @UserJSON NVARCHAR(MAX)
AS
BEGIN
    DECLARE @FirstName NVARCHAR(MAX), @LastName NVARCHAR(MAX), @BirthDate DATETIME2(7), 
            @Phone NVARCHAR(MAX), @RoleID INT, @StatusID INT, @JoinDate DATETIME2(7), @UserId NVARCHAR(450);
    
    SET @FirstName = JSON_VALUE(@UserJSON, '$.FirstName');
    SET @LastName = JSON_VALUE(@UserJSON, '$.LastName');
    SET @BirthDate = JSON_VALUE(@UserJSON, '$.BirthDate');
    SET @Phone = JSON_VALUE(@UserJSON, '$.Phone');
    SET @RoleID = JSON_VALUE(@UserJSON, '$.RoleID');
    SET @StatusID = JSON_VALUE(@UserJSON, '$.StatusID');
    SET @JoinDate = JSON_VALUE(@UserJSON, '$.JoinDate');
    SET @UserId = JSON_VALUE(@UserJSON, '$.UserId');
    
    INSERT INTO Users (FirstName, LastName, BirthDate, Phone, RoleID, StatusID, JoinDate, UserId)
    VALUES (@FirstName, @LastName, @BirthDate, @Phone, @RoleID, @StatusID, @JoinDate, @UserId);
END;
GO

--Obtener todos los registros en formato JSON:
CREATE PROCEDURE GetAllUsers
AS
BEGIN
    SELECT 
        ID,
        FirstName,
        LastName,
        BirthDate,
        Phone,
        RoleID,
        StatusID,
        JoinDate,
        UserId
    FROM 
        Users
    FOR JSON AUTO, ROOT('Users');
END;
GO

-- Obtener un usuario específico por su ID en formato JSON

CREATE PROCEDURE GetUserById
    @UserID INT
AS
BEGIN
    SELECT 
        ID,
        FirstName,
        LastName,
        BirthDate,
        Phone,
        RoleID,
        StatusID,
        JoinDate,
        UserId
    FROM 
        Users
    WHERE 
        ID = @UserID
    FOR JSON AUTO, ROOT('User');
END;
GO

-- Actualizar un usuario utilizando su ID
CREATE PROCEDURE UpdateUser
    @UserID INT,
    @UserJSON NVARCHAR(MAX)
AS
BEGIN
    DECLARE @FirstName NVARCHAR(MAX), @LastName NVARCHAR(MAX), @BirthDate DATETIME2(7), 
            @Phone NVARCHAR(MAX), @RoleID INT, @StatusID INT, @JoinDate DATETIME2(7), @UserId NVARCHAR(450);
    
    SET @FirstName = JSON_VALUE(@UserJSON, '$.FirstName');
    SET @LastName = JSON_VALUE(@UserJSON, '$.LastName');
    SET @BirthDate = JSON_VALUE(@UserJSON, '$.BirthDate');
    SET @Phone = JSON_VALUE(@UserJSON, '$.Phone');
    SET @RoleID = JSON_VALUE(@UserJSON, '$.RoleID');
    SET @StatusID = JSON_VALUE(@UserJSON, '$.StatusID');
    SET @JoinDate = JSON_VALUE(@UserJSON, '$.JoinDate');
    SET @UserId = JSON_VALUE(@UserJSON, '$.UserId');
    
    UPDATE Users
    SET FirstName = @FirstName, LastName = @LastName, BirthDate = @BirthDate, Phone = @Phone, 
        RoleID = @RoleID, StatusID = @StatusID, JoinDate = @JoinDate, UserId = @UserId
    WHERE ID = @UserID;
END;
GO

-- Eliminar un usuario utilizando su ID
CREATE PROCEDURE DeleteUser
    @UserID INT
AS
BEGIN
    DELETE FROM Users
    WHERE ID = @UserID;
END;
GO

-- Insertar un Nuevo Usuario
DECLARE @NewUser NVARCHAR(MAX) = N'{"FirstName": "Alice", "LastName": "Smith", "BirthDate": "1985-07-22T00:00:00Z", "Phone": "555-9876", "RoleID": 3, "StatusID": 2, "JoinDate": "2024-11-22T09:00:00Z", "UserId": "user123"}';
EXEC CreateUser @UserJSON = @NewUser;
GO

-- Obtener Todos los Usuarios
EXEC GetAllUsers;
GO

-- Obtener un Usuario por ID
EXEC GetUserById @UserID = 1;
GO

-- Actualizar un Usuario
DECLARE @UpdatedUser NVARCHAR(MAX) = N'{"FirstName": "Alice", "LastName": "Johnson", "BirthDate": "1985-07-22T00:00:00Z", "Phone": "555-1234", "RoleID": 3, "StatusID": 2, "JoinDate": "2024-11-22T09:00:00Z", "UserId": "user123"}';
EXEC UpdateUser @UserID = 1, @UserJSON = @UpdatedUser;


-- Eliminar un Usuario
EXEC DeleteUser @UserID = 1;
GO