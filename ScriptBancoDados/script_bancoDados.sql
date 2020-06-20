
CREATE DATABASE BancoTesteZurich
GO

USE [BancoTesteZurich]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 19/06/2020 20:15:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Segurado]    Script Date: 19/06/2020 20:15:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Segurado](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](50) NOT NULL,
	[CPF] [nvarchar](11) NOT NULL,
	[Idade] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Segurado] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SeguradoVeiculo]    Script Date: 19/06/2020 20:15:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SeguradoVeiculo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VeiculoRefId] [int] NOT NULL,
	[SeguradoRefId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.SeguradoVeiculo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Seguro]    Script Date: 19/06/2020 20:15:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Seguro](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ValorSeguro] [float] NOT NULL,
	[VeiculoRefId] [int] NOT NULL,
	[SeguradoRefId] [int] NOT NULL,
	[TaxaRisco] [float] NOT NULL DEFAULT ((0)),
	[PremioRisco] [float] NOT NULL DEFAULT ((0)),
	[PremioComercial] [float] NOT NULL DEFAULT ((0)),
	[PremioPuro] [float] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_dbo.Seguro] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Veiculo]    Script Date: 19/06/2020 20:15:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Veiculo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MarcaModelo] [nvarchar](max) NOT NULL,
	[valor] [float] NOT NULL,
 CONSTRAINT [PK_dbo.Veiculo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[SeguradoVeiculo]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Seguradoes_dbo.Seguradoes_SeguradoRefId] FOREIGN KEY([SeguradoRefId])
REFERENCES [dbo].[Segurado] ([Id])
GO
ALTER TABLE [dbo].[SeguradoVeiculo] CHECK CONSTRAINT [FK_dbo.Seguradoes_dbo.Seguradoes_SeguradoRefId]
GO
ALTER TABLE [dbo].[SeguradoVeiculo]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Seguradoes_dbo.Veiculoes_VeiculoRefId] FOREIGN KEY([VeiculoRefId])
REFERENCES [dbo].[Veiculo] ([Id])
GO
ALTER TABLE [dbo].[SeguradoVeiculo] CHECK CONSTRAINT [FK_dbo.Seguradoes_dbo.Veiculoes_VeiculoRefId]
GO
ALTER TABLE [dbo].[Seguro]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Seguroes_dbo.Seguradoes_SeguradoRefId] FOREIGN KEY([SeguradoRefId])
REFERENCES [dbo].[Segurado] ([Id])
GO
ALTER TABLE [dbo].[Seguro] CHECK CONSTRAINT [FK_dbo.Seguroes_dbo.Seguradoes_SeguradoRefId]
GO
ALTER TABLE [dbo].[Seguro]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Seguroes_dbo.Veiculoes_VeiculoRefId] FOREIGN KEY([VeiculoRefId])
REFERENCES [dbo].[Veiculo] ([Id])
GO
ALTER TABLE [dbo].[Seguro] CHECK CONSTRAINT [FK_dbo.Seguroes_dbo.Veiculoes_VeiculoRefId]
GO
