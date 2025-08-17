-- ************************************************************
--  PARA SOFKA 
--  POSTULANTE: RICARDO NAVAS - DESARROLLADOR FULLSTACK
--  SCRIPT BASE DE DATOS - PRUEBA BACKEND MOVIMIENTOS BANCARIOS
--  SQLSERVER 2014 MANAGMENT STUDIO
--  TABLA 1: PERSONA 
--  ***********************************************************

USE BaseDatos;
GO


CREATE TABLE Persona (
    PersonaId INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Genero VARCHAR(50) NOT NULL, 
    Edad INT NOT NULL CHECK (Edad >= 16),
    Identificacion INT UNIQUE,
    Direccion NVARCHAR(200) NOT NULL,
    Telefono NVARCHAR(20) NOT NULL
);
GO

--  ************************************************
--  TABLA 2: CLIENTE CON SU CLAVE FORANEA PersonaId
--  ************************************************

CREATE TABLE Cliente (
    ClienteId INT PRIMARY KEY,
    Contrasena NVARCHAR(100) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    CONSTRAINT FK_Cliente_Persona FOREIGN KEY (ClienteId)
        REFERENCES Persona(PersonaId)
        ON UPDATE CASCADE
        ON DELETE CASCADE
);
GO


-- ***********************************************
-- TABLA 3: CUENTA CON SU CLAVE FORANEA ClienteId
-- ***********************************************

CREATE TABLE Cuenta (
    CuentaId INT IDENTITY(1,1) PRIMARY KEY,
    NumeroCuenta NVARCHAR(50) NOT NULL UNIQUE,
    TipoCuenta NVARCHAR(15),
    SaldoInicial DECIMAL(18,2) NOT NULL CHECK (SaldoInicial >= 0),
    Estado BIT NOT NULL DEFAULT 1,
    ClienteId INT NOT NULL,
    CONSTRAINT FK_Cuenta_Cliente FOREIGN KEY (ClienteId)
        REFERENCES Cliente(ClienteId)
        ON UPDATE CASCADE
        ON DELETE CASCADE
);
GO

-- ***********************************************
-- TABLA 4: MOVIMIENTO CON SU CLAVE FORANEA CuentaId
-- ***********************************************

CREATE TABLE Movimiento (
    MovimientoId INT IDENTITY(1,1) PRIMARY KEY,
    CuentaId INT NOT NULL,
    Fecha DATETIME NOT NULL DEFAULT GETDATE(),
    TipoMovimiento VARCHAR(10),
    Valor DECIMAL(18,2) NOT NULL,
    Saldo DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_Movimiento_Cuenta FOREIGN KEY (CuentaId)
        REFERENCES Cuenta(CuentaId)
        ON UPDATE CASCADE
        ON DELETE CASCADE
);
GO

-- ***********************************************
-- INSERCION DE DATOS EN TABLAS
-- ***********************************************

-- PERSONA

INSERT INTO Persona (Nombre, Genero, Edad, Identificacion, Direccion, Telefono) VALUES 
('Jose Lema', 'Masculino', 30, '1702655887', 'Otavalo sn y principal', '098254785'),
('Marianela Montalvo', 'Femenino', 28, '1768932418', 'Amazonas y NNUU', '097548965'),
('Juan Osorio', 'Masculino', 35, '1122334455', '13 junio y Equinoccial', '098874587');

GO


-- CLIENTE
INSERT INTO Cliente (ClienteId, Contrasena, Estado) VALUES 
(1, '1234', 1),
(2, '5678', 1),
(3, '1245', 1);

GO

-- CUENTA
INSERT INTO Cuenta (NumeroCuenta, TipoCuenta, SaldoInicial, Estado, ClienteId) VALUES
('478758', 'Ahorro', 2000, 1, 1), 
('225487', 'Corriente', 100, 1, 2), 
('495878', 'Ahorro', 0, 1, 3),      
('496825', 'Ahorro', 540, 1, 2);    

GO

-- MOVIMIENTO
INSERT INTO Movimiento (CuentaId, Fecha, TipoMovimiento, Valor, Saldo) VALUES
(2, '10/2/2022', 'Deposito', 600, 700),  
(4, '8/2/2022', 'Retiro', -540, 0);     

GO


   



