USE [master]
GO
/****** Object:  Database [FinisDB]    Script Date: 15/05/2017 22:56:39 ******/
CREATE DATABASE [FinisDB]
GO
ALTER DATABASE [FinisDB] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FinisDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FinisDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FinisDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FinisDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FinisDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FinisDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [FinisDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FinisDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FinisDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FinisDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FinisDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FinisDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FinisDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FinisDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FinisDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FinisDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FinisDB] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [FinisDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FinisDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [FinisDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FinisDB] SET  MULTI_USER 
GO
ALTER DATABASE [FinisDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FinisDB] SET ENCRYPTION ON
GO
ALTER DATABASE [FinisDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [FinisDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 100, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO)
GO
USE [FinisDB]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 15/05/2017 22:56:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Autores]    Script Date: 15/05/2017 22:56:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Autores](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[exemplar_id] [int] NULL,
	[nome] [nvarchar](50) NOT NULL,
	[email] [nvarchar](50) NULL,
	[telefone] [nvarchar](max) NULL,
	[celular] [nvarchar](max) NULL,
	[enderecoId] [int] NULL,
	[user_insert] [nvarchar](max) NULL,
	[user_update] [nvarchar](max) NULL,
	[date_insert] [datetime] NULL,
	[date_update] [datetime] NULL,
 CONSTRAINT [PK_dbo.Autores] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Avaliacoes]    Script Date: 15/05/2017 22:56:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Avaliacoes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_insert] [nvarchar](max) NULL,
	[user_update] [nvarchar](max) NULL,
	[date_insert] [datetime] NULL,
	[date_update] [datetime] NULL,
 CONSTRAINT [PK_dbo.Avaliacoes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Cidades]    Script Date: 15/05/2017 22:56:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cidades](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [nvarchar](30) NOT NULL,
	[estadoId] [int] NOT NULL,
	[user_insert] [nvarchar](max) NULL,
	[user_update] [nvarchar](max) NULL,
	[date_insert] [datetime] NULL,
	[date_update] [datetime] NULL,
 CONSTRAINT [PK_dbo.Cidades] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Clientes]    Script Date: 15/05/2017 22:56:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clientes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[saldoCreditoParcial] [decimal](18, 2) NOT NULL,
	[saldoCreditoEspecial] [decimal](18, 2) NOT NULL,
	[dataNascimento] [datetime] NOT NULL,
	[genero] [int] NOT NULL,
	[rg] [nvarchar](20) NOT NULL,
	[nome] [nvarchar](50) NOT NULL,
	[email] [nvarchar](50) NULL,
	[telefone] [nvarchar](max) NULL,
	[celular] [nvarchar](max) NULL,
	[enderecoId] [int] NULL,
	[user_insert] [nvarchar](max) NULL,
	[user_update] [nvarchar](max) NULL,
	[date_insert] [datetime] NULL,
	[date_update] [datetime] NULL,
 CONSTRAINT [PK_dbo.Clientes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Enderecos]    Script Date: 15/05/2017 22:56:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Enderecos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[logradouro] [nvarchar](30) NOT NULL,
	[numero] [int] NOT NULL,
	[complemento] [nvarchar](30) NULL,
	[bairro] [nvarchar](20) NOT NULL,
	[cep] [int] NOT NULL,
	[cidadeId] [int] NOT NULL,
	[user_insert] [nvarchar](max) NULL,
	[user_update] [nvarchar](max) NULL,
	[date_insert] [datetime] NULL,
	[date_update] [datetime] NULL,
 CONSTRAINT [PK_dbo.Enderecos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Estados]    Script Date: 15/05/2017 22:56:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estados](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [nvarchar](30) NOT NULL,
	[sigla] [nvarchar](2) NOT NULL,
	[paisId] [int] NOT NULL,
	[user_insert] [nvarchar](max) NULL,
	[user_update] [nvarchar](max) NULL,
	[date_insert] [datetime] NULL,
	[date_update] [datetime] NULL,
 CONSTRAINT [PK_dbo.Estados] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Exemplares]    Script Date: 15/05/2017 22:56:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Exemplares](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[titulo] [nvarchar](30) NOT NULL,
	[conservacao] [int] NOT NULL,
	[ano] [datetime] NOT NULL,
	[descricao] [nvarchar](200) NULL,
	[vendaOnline] [bit] NOT NULL,
	[user_insert] [nvarchar](max) NULL,
	[user_update] [nvarchar](max) NULL,
	[date_insert] [datetime] NULL,
	[date_update] [datetime] NULL,
	[editora_id] [int] NULL,
	[idioma_id] [int] NULL,
	[sessao_id] [int] NULL,
 CONSTRAINT [PK_dbo.Exemplares] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Fornecedores]    Script Date: 15/05/2017 22:56:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Fornecedores](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[cnpj] [nvarchar](50) NULL,
	[nome] [nvarchar](50) NOT NULL,
	[email] [nvarchar](50) NULL,
	[telefone] [nvarchar](max) NULL,
	[celular] [nvarchar](max) NULL,
	[enderecoId] [int] NULL,
	[user_insert] [nvarchar](max) NULL,
	[user_update] [nvarchar](max) NULL,
	[date_insert] [datetime] NULL,
	[date_update] [datetime] NULL,
	[Discriminator] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.Fornecedores] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Idiomas]    Script Date: 15/05/2017 22:56:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Idiomas](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [nvarchar](20) NOT NULL,
	[user_insert] [nvarchar](max) NULL,
	[user_update] [nvarchar](max) NULL,
	[date_insert] [datetime] NULL,
	[date_update] [datetime] NULL,
	[pais_id] [int] NULL,
 CONSTRAINT [PK_dbo.Idiomas] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Paises]    Script Date: 15/05/2017 22:56:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Paises](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [nvarchar](30) NOT NULL,
	[sigla] [nvarchar](2) NOT NULL,
	[user_insert] [nvarchar](max) NULL,
	[user_update] [nvarchar](max) NULL,
	[date_insert] [datetime] NULL,
	[date_update] [datetime] NULL,
 CONSTRAINT [PK_dbo.Paises] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Sessoes]    Script Date: 15/05/2017 22:56:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sessoes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [nvarchar](20) NOT NULL,
	[user_insert] [nvarchar](max) NULL,
	[user_update] [nvarchar](max) NULL,
	[date_insert] [datetime] NULL,
	[date_update] [datetime] NULL,
 CONSTRAINT [PK_dbo.Sessoes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Transacoes]    Script Date: 15/05/2017 22:56:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transacoes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[valor] [decimal](18, 2) NOT NULL,
	[data] [datetime] NOT NULL,
	[tipoTransacao] [int] NOT NULL,
	[user_insert] [nvarchar](max) NULL,
	[user_update] [nvarchar](max) NULL,
	[date_insert] [datetime] NULL,
	[date_update] [datetime] NULL,
	[cliente_id] [int] NULL,
 CONSTRAINT [PK_dbo.Transacoes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Index [IX_enderecoId]    Script Date: 15/05/2017 22:56:47 ******/
CREATE NONCLUSTERED INDEX [IX_enderecoId] ON [dbo].[Autores]
(
	[enderecoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
/****** Object:  Index [IX_exemplar_id]    Script Date: 15/05/2017 22:56:47 ******/
CREATE NONCLUSTERED INDEX [IX_exemplar_id] ON [dbo].[Autores]
(
	[exemplar_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
/****** Object:  Index [IX_estadoId]    Script Date: 15/05/2017 22:56:47 ******/
CREATE NONCLUSTERED INDEX [IX_estadoId] ON [dbo].[Cidades]
(
	[estadoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
/****** Object:  Index [IX_enderecoId]    Script Date: 15/05/2017 22:56:47 ******/
CREATE NONCLUSTERED INDEX [IX_enderecoId] ON [dbo].[Clientes]
(
	[enderecoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
/****** Object:  Index [IX_cidadeId]    Script Date: 15/05/2017 22:56:47 ******/
CREATE NONCLUSTERED INDEX [IX_cidadeId] ON [dbo].[Enderecos]
(
	[cidadeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
/****** Object:  Index [IX_paisId]    Script Date: 15/05/2017 22:56:47 ******/
CREATE NONCLUSTERED INDEX [IX_paisId] ON [dbo].[Estados]
(
	[paisId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
/****** Object:  Index [IX_editora_id]    Script Date: 15/05/2017 22:56:47 ******/
CREATE NONCLUSTERED INDEX [IX_editora_id] ON [dbo].[Exemplares]
(
	[editora_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
/****** Object:  Index [IX_idioma_id]    Script Date: 15/05/2017 22:56:47 ******/
CREATE NONCLUSTERED INDEX [IX_idioma_id] ON [dbo].[Exemplares]
(
	[idioma_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
/****** Object:  Index [IX_sessao_id]    Script Date: 15/05/2017 22:56:47 ******/
CREATE NONCLUSTERED INDEX [IX_sessao_id] ON [dbo].[Exemplares]
(
	[sessao_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
/****** Object:  Index [IX_enderecoId]    Script Date: 15/05/2017 22:56:47 ******/
CREATE NONCLUSTERED INDEX [IX_enderecoId] ON [dbo].[Fornecedores]
(
	[enderecoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
/****** Object:  Index [IX_pais_id]    Script Date: 15/05/2017 22:56:47 ******/
CREATE NONCLUSTERED INDEX [IX_pais_id] ON [dbo].[Idiomas]
(
	[pais_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
/****** Object:  Index [IX_cliente_id]    Script Date: 15/05/2017 22:56:47 ******/
CREATE NONCLUSTERED INDEX [IX_cliente_id] ON [dbo].[Transacoes]
(
	[cliente_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO
ALTER TABLE [dbo].[Autores]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Autores_dbo.Enderecos_enderecoId] FOREIGN KEY([enderecoId])
REFERENCES [dbo].[Enderecos] ([id])
GO
ALTER TABLE [dbo].[Autores] CHECK CONSTRAINT [FK_dbo.Autores_dbo.Enderecos_enderecoId]
GO
ALTER TABLE [dbo].[Autores]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Autores_dbo.Exemplares_exemplar_id] FOREIGN KEY([exemplar_id])
REFERENCES [dbo].[Exemplares] ([id])
GO
ALTER TABLE [dbo].[Autores] CHECK CONSTRAINT [FK_dbo.Autores_dbo.Exemplares_exemplar_id]
GO
ALTER TABLE [dbo].[Cidades]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Cidades_dbo.Estados_estadoId] FOREIGN KEY([estadoId])
REFERENCES [dbo].[Estados] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Cidades] CHECK CONSTRAINT [FK_dbo.Cidades_dbo.Estados_estadoId]
GO
ALTER TABLE [dbo].[Clientes]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Clientes_dbo.Enderecos_enderecoId] FOREIGN KEY([enderecoId])
REFERENCES [dbo].[Enderecos] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Clientes] CHECK CONSTRAINT [FK_dbo.Clientes_dbo.Enderecos_enderecoId]
GO
ALTER TABLE [dbo].[Enderecos]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Enderecos_dbo.Cidades_cidadeId] FOREIGN KEY([cidadeId])
REFERENCES [dbo].[Cidades] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Enderecos] CHECK CONSTRAINT [FK_dbo.Enderecos_dbo.Cidades_cidadeId]
GO
ALTER TABLE [dbo].[Estados]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Estados_dbo.Paises_paisId] FOREIGN KEY([paisId])
REFERENCES [dbo].[Paises] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Estados] CHECK CONSTRAINT [FK_dbo.Estados_dbo.Paises_paisId]
GO
ALTER TABLE [dbo].[Exemplares]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Exemplares_dbo.Fornecedores_editora_id] FOREIGN KEY([editora_id])
REFERENCES [dbo].[Fornecedores] ([id])
GO
ALTER TABLE [dbo].[Exemplares] CHECK CONSTRAINT [FK_dbo.Exemplares_dbo.Fornecedores_editora_id]
GO
ALTER TABLE [dbo].[Exemplares]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Exemplares_dbo.Idiomas_idioma_id] FOREIGN KEY([idioma_id])
REFERENCES [dbo].[Idiomas] ([id])
GO
ALTER TABLE [dbo].[Exemplares] CHECK CONSTRAINT [FK_dbo.Exemplares_dbo.Idiomas_idioma_id]
GO
ALTER TABLE [dbo].[Exemplares]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Exemplares_dbo.Sessoes_sessao_id] FOREIGN KEY([sessao_id])
REFERENCES [dbo].[Sessoes] ([id])
GO
ALTER TABLE [dbo].[Exemplares] CHECK CONSTRAINT [FK_dbo.Exemplares_dbo.Sessoes_sessao_id]
GO
ALTER TABLE [dbo].[Fornecedores]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Fornecedores_dbo.Enderecos_enderecoId] FOREIGN KEY([enderecoId])
REFERENCES [dbo].[Enderecos] ([id])
GO
ALTER TABLE [dbo].[Fornecedores] CHECK CONSTRAINT [FK_dbo.Fornecedores_dbo.Enderecos_enderecoId]
GO
ALTER TABLE [dbo].[Idiomas]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Idiomas_dbo.Paises_pais_id] FOREIGN KEY([pais_id])
REFERENCES [dbo].[Paises] ([id])
GO
ALTER TABLE [dbo].[Idiomas] CHECK CONSTRAINT [FK_dbo.Idiomas_dbo.Paises_pais_id]
GO
ALTER TABLE [dbo].[Transacoes]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Transacoes_dbo.Clientes_cliente_id] FOREIGN KEY([cliente_id])
REFERENCES [dbo].[Clientes] ([id])
GO
ALTER TABLE [dbo].[Transacoes] CHECK CONSTRAINT [FK_dbo.Transacoes_dbo.Clientes_cliente_id]
GO
USE [master]
GO
ALTER DATABASE [FinisDB] SET  READ_WRITE 
GO
