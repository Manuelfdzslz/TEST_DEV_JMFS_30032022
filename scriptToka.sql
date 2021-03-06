USE [TokaDev]
GO
/****** Object:  Table [dbo].[Tb_PersonasFisicas]    Script Date: 03/05/2022 05:31:15 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_PersonasFisicas](
	[IdPersonaFisica] [int] IDENTITY(1,1) NOT NULL,
	[FechaRegistro] [datetime] NULL,
	[FechaActualizacion] [datetime] NULL,
	[Nombre] [varchar](50) NULL,
	[ApellidoPaterno] [varchar](50) NULL,
	[ApellidoMaterno] [varchar](50) NULL,
	[RFC] [varchar](13) NULL,
	[FechaNacimiento] [date] NULL,
	[UsuarioAgrega] [int] NULL,
	[Activo] [bit] NULL,
 CONSTRAINT [PK_Tb_PersonasFisicas] PRIMARY KEY CLUSTERED 
(
	[IdPersonaFisica] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tb_User]    Script Date: 03/05/2022 05:31:15 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](250) NOT NULL,
	[Pasword] [varchar](50) NOT NULL,
	[Active] [bit] NOT NULL,
	[FechaRegistro] [datetime] NULL,
	[FechaActualizacion] [datetime] NULL,
 CONSTRAINT [PK_Tb_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tb_UserInfo]    Script Date: 03/05/2022 05:31:15 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_UserInfo](
	[UserInfoID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Name] [varchar](250) NOT NULL,
	[Lastname] [varchar](250) NOT NULL,
	[FechaRegistro] [datetime] NULL,
 CONSTRAINT [PK_Tb_UserInfo] PRIMARY KEY CLUSTERED 
(
	[UserInfoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tb_UserToken]    Script Date: 03/05/2022 05:31:15 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tb_UserToken](
	[TokenID] [int] IDENTITY(1,1) NOT NULL,
	[Token] [varchar](max) NOT NULL,
	[UserID] [int] NOT NULL,
	[Activo] [bit] NOT NULL,
	[FechaRegistro] [datetime] NULL,
 CONSTRAINT [PK_Tb_UserToken] PRIMARY KEY CLUSTERED 
(
	[TokenID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Tb_PersonasFisicas] ON 

INSERT [dbo].[Tb_PersonasFisicas] ([IdPersonaFisica], [FechaRegistro], [FechaActualizacion], [Nombre], [ApellidoPaterno], [ApellidoMaterno], [RFC], [FechaNacimiento], [UsuarioAgrega], [Activo]) VALUES (1, CAST(N'2022-05-03T15:27:29.293' AS DateTime), NULL, N'cosme', N'Fulanito', N'alba', N'XAXX010101001', CAST(N'2024-02-15' AS Date), 12, 0)
INSERT [dbo].[Tb_PersonasFisicas] ([IdPersonaFisica], [FechaRegistro], [FechaActualizacion], [Nombre], [ApellidoPaterno], [ApellidoMaterno], [RFC], [FechaNacimiento], [UsuarioAgrega], [Activo]) VALUES (2, CAST(N'2022-05-03T15:33:26.510' AS DateTime), NULL, N'javier', N'Lopez', N'segoviano', N'XAXX010101022', CAST(N'1980-10-12' AS Date), 12, 1)
INSERT [dbo].[Tb_PersonasFisicas] ([IdPersonaFisica], [FechaRegistro], [FechaActualizacion], [Nombre], [ApellidoPaterno], [ApellidoMaterno], [RFC], [FechaNacimiento], [UsuarioAgrega], [Activo]) VALUES (3, CAST(N'2022-05-03T15:47:13.963' AS DateTime), NULL, N'franco', N'lopez', N'escamilla', N'XAXX010101001', CAST(N'1980-10-12' AS Date), 12, 0)
INSERT [dbo].[Tb_PersonasFisicas] ([IdPersonaFisica], [FechaRegistro], [FechaActualizacion], [Nombre], [ApellidoPaterno], [ApellidoMaterno], [RFC], [FechaNacimiento], [UsuarioAgrega], [Activo]) VALUES (4, CAST(N'2022-05-03T17:17:58.047' AS DateTime), NULL, N'javier', N'Lopez', N'segoviano', N'XAXX010101001', CAST(N'1980-10-12' AS Date), 12, 1)
SET IDENTITY_INSERT [dbo].[Tb_PersonasFisicas] OFF
SET IDENTITY_INSERT [dbo].[Tb_User] ON 

INSERT [dbo].[Tb_User] ([UserID], [Email], [Pasword], [Active], [FechaRegistro], [FechaActualizacion]) VALUES (12, N'admin@admin.com', N'admin123', 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Tb_User] OFF
SET IDENTITY_INSERT [dbo].[Tb_UserInfo] ON 

INSERT [dbo].[Tb_UserInfo] ([UserInfoID], [UserID], [Name], [Lastname], [FechaRegistro]) VALUES (1, 12, N'administrador', N'root', NULL)
SET IDENTITY_INSERT [dbo].[Tb_UserInfo] OFF
SET IDENTITY_INSERT [dbo].[Tb_UserToken] ON 

INSERT [dbo].[Tb_UserToken] ([TokenID], [Token], [UserID], [Activo], [FechaRegistro]) VALUES (1, N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhZG1pbmlzdHJhZG9yIHJvb3QiLCJqdGkiOiIxMiIsIkZuIjoiYWRtaW5pc3RyYWRvciIsIkxuIjoicm9vdCIsIm5iZiI6MTY1MTYwOTU5MiwiZXhwIjoxNjUyNDczNTkyLCJpYXQiOjE2NTE2MDk1OTIsImlzcyI6InRva2EudGVzdCIsImF1ZCI6InRva2EudGVzdCJ9.dr17WBtlOUfUC7dIFDaWYkZRg_UbQ7_ZRHVsu5jkk3M', 12, 0, CAST(N'2022-05-03T15:26:32.793' AS DateTime))
INSERT [dbo].[Tb_UserToken] ([TokenID], [Token], [UserID], [Activo], [FechaRegistro]) VALUES (2, N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhZG1pbmlzdHJhZG9yIHJvb3QiLCJqdGkiOiIxMiIsIkZuIjoiYWRtaW5pc3RyYWRvciIsIkxuIjoicm9vdCIsIm5iZiI6MTY1MTYxNjU2NywiZXhwIjoxNjUyNDgwNTY3LCJpYXQiOjE2NTE2MTY1NjcsImlzcyI6InRva2EudGVzdCIsImF1ZCI6InRva2EudGVzdCJ9.e9toCNgzbjV5SxToQ5aDJgA16NHzFIn-2nCqCMTWsjY', 12, 1, CAST(N'2022-05-03T17:22:47.110' AS DateTime))
SET IDENTITY_INSERT [dbo].[Tb_UserToken] OFF
SET ANSI_PADDING ON
GO
/****** Object:  Index [CK_Tb_User_Email]    Script Date: 03/05/2022 05:31:15 p. m. ******/
ALTER TABLE [dbo].[Tb_User] ADD  CONSTRAINT [CK_Tb_User_Email] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Tb_PersonasFisicas] ADD  CONSTRAINT [DF_Tb_PersonasFisicas_FechaRegistro]  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[Tb_PersonasFisicas] ADD  CONSTRAINT [DF_Tb_PersonasFisicas_Activo]  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Tb_User] ADD  CONSTRAINT [DF_Tb_User_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Tb_UserInfo]  WITH CHECK ADD  CONSTRAINT [FK_Tb_UserInfo_Tb_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[Tb_User] ([UserID])
GO
ALTER TABLE [dbo].[Tb_UserInfo] CHECK CONSTRAINT [FK_Tb_UserInfo_Tb_User]
GO
ALTER TABLE [dbo].[Tb_UserToken]  WITH CHECK ADD  CONSTRAINT [FK_Tb_UserToken_Tb_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[Tb_User] ([UserID])
GO
ALTER TABLE [dbo].[Tb_UserToken] CHECK CONSTRAINT [FK_Tb_UserToken_Tb_User]
GO
/****** Object:  StoredProcedure [dbo].[sp_ActualizarPersonaFisica]    Script Date: 03/05/2022 05:31:15 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ActualizarPersonaFisica]
(
    @IdPersonaFisica INT,
    @Nombre VARCHAR(50),
    @ApellidoPaterno VARCHAR(50),
    @ApellidoMaterno VARCHAR(50),
    @RFC VARCHAR(13),
    @FechaNacimiento DATE,
    @UsuarioAgrega INT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @ID INT,
            @ERROR VARCHAR(500);
    BEGIN TRY
        IF NOT EXISTS
        (
            SELECT *
            FROM dbo.Tb_PersonasFisicas
            WHERE IdPersonaFisica = @IdPersonaFisica
                  AND Activo = 1
        )
        BEGIN
            SELECT @ERROR = 'La persona fisica no existe.';
            THROW 50000, @ERROR, 1;
        END;

        UPDATE dbo.Tb_PersonasFisicas
        SET Nombre = @Nombre,
            ApellidoPaterno = @ApellidoPaterno,
            ApellidoMaterno = @ApellidoMaterno,
            RFC = @RFC,
            FechaNacimiento = @FechaNacimiento
        WHERE IdPersonaFisica = @IdPersonaFisica;
        SELECT @IdPersonaFisica AS ERROR,
               'Registro exitoso' AS MENSAJEERROR;
    END TRY
    BEGIN CATCH
        PRINT ERROR_MESSAGE();
        SELECT ERROR_NUMBER() * -1 AS ERROR,
               ISNULL(@ERROR, 'Error al actualizar el registro.') AS MENSAJEERROR;
    END CATCH;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_AgregarPersonaFisica]    Script Date: 03/05/2022 05:31:15 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===================================================================
CREATE PROCEDURE [dbo].[sp_AgregarPersonaFisica]
(
    @Nombre VARCHAR(50),
    @ApellidoPaterno VARCHAR(50),
    @ApellidoMaterno VARCHAR(50),
    @RFC VARCHAR(13),
    @FechaNacimiento DATE,
    @UsuarioAgrega INT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @ID INT,
            @ERROR VARCHAR(500);
    BEGIN TRY
        IF LEN(@RFC) != 13
        BEGIN
            SELECT @ERROR = 'El RFC no es válido';
            THROW 50000, @ERROR, 1;
        END;
        IF EXISTS
        (
            SELECT *
            FROM dbo.Tb_PersonasFisicas
            WHERE RFC = @RFC
                  AND Activo = 1
        )
        BEGIN
            SELECT @ERROR = 'El RFC ya existe en el sistema';
            THROW 50000, @ERROR, 1;
        END;

        INSERT INTO dbo.Tb_PersonasFisicas
        (
            FechaRegistro,
            FechaActualizacion,
            Nombre,
            ApellidoPaterno,
            ApellidoMaterno,
            RFC,
            FechaNacimiento,
            UsuarioAgrega,
            Activo
        )
        VALUES
        (   GETDATE(),        -- FechaRegistro - datetime
            NULL,             -- FechaActualizacion - datetime
            @Nombre,          -- Nombre - varchar(50)
            @ApellidoPaterno, -- ApellidoPaterno - varchar(50)
            @ApellidoMaterno, -- ApellidoMaterno - varchar(50)
            @RFC,             -- RFC - varchar(13)
            @FechaNacimiento, -- FechaNacimiento - date
            @UsuarioAgrega,   -- UsuarioAgrega - int
            1                 -- Activo - bit
            );

        SELECT @ID = SCOPE_IDENTITY();
        SELECT @ID AS ERROR,
               'Registro exitoso' AS MENSAJEERROR;
    END TRY
    BEGIN CATCH
        PRINT ERROR_MESSAGE();
        SELECT ERROR_NUMBER() * -1 AS ERROR,
               ISNULL(@ERROR, 'Error al guardar el registro.') AS MENSAJEERROR;
    END CATCH;
END;
GO
/****** Object:  StoredProcedure [dbo].[sp_EliminarPersonaFisica]    Script Date: 03/05/2022 05:31:15 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_EliminarPersonaFisica]
(@IdPersonaFisica INT)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @ID INT,
            @ERROR VARCHAR(500);
    BEGIN TRY
        IF NOT EXISTS
        (
            SELECT *
            FROM dbo.Tb_PersonasFisicas
            WHERE IdPersonaFisica = @IdPersonaFisica
                  AND Activo = 1
        )
        BEGIN
            SELECT @ERROR = 'La persona fisica no existe.';
            THROW 50000, @ERROR, 1;
        END;

        UPDATE dbo.Tb_PersonasFisicas
        SET Activo = 0
        WHERE IdPersonaFisica = @IdPersonaFisica;
		 SELECT @IdPersonaFisica AS ERROR,
               'Actualizacion exitosa' AS MENSAJEERROR;
    END TRY
    BEGIN CATCH
        PRINT ERROR_MESSAGE();
        SELECT ERROR_NUMBER() * -1 AS ERROR,
               ISNULL(@ERROR, 'Error al actualizar el registro.') AS MENSAJEERROR;
    END CATCH;
END;
GO
