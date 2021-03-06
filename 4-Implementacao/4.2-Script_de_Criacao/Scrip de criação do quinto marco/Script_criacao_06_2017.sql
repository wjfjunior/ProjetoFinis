USE [FinisDB]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 19/06/2017 12:44:24 ******/
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
/****** Object:  Table [dbo].[Autores]    Script Date: 19/06/2017 12:44:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Autores](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [nvarchar](50) NOT NULL,
	[email] [nvarchar](50) NULL,
	[telefone] [nvarchar](15) NULL,
	[celular] [nvarchar](15) NULL,
	[enderecoId] [int] NULL,
	[user_insert] [nvarchar](max) NULL,
	[user_update] [nvarchar](max) NULL,
	[date_insert] [datetime] NULL,
	[date_update] [datetime] NULL,
	[Exemplar_id] [int] NULL,
 CONSTRAINT [PK_dbo.Autores] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Avaliacoes]    Script Date: 19/06/2017 12:44:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Avaliacoes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[dataEntrada] [datetime] NOT NULL,
	[quantidadeExemplares] [int] NOT NULL,
	[creditoEspecial] [decimal](18, 2) NOT NULL,
	[creditoParcial] [decimal](18, 2) NOT NULL,
	[situacao] [int] NOT NULL,
	[observacao] [nvarchar](200) NULL,
	[clienteId] [int] NOT NULL,
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
/****** Object:  Table [dbo].[Cidades]    Script Date: 19/06/2017 12:44:29 ******/
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
/****** Object:  Table [dbo].[Clientes]    Script Date: 19/06/2017 12:44:29 ******/
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
	[telefone] [nvarchar](15) NULL,
	[celular] [nvarchar](15) NULL,
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
/****** Object:  Table [dbo].[Editoras]    Script Date: 19/06/2017 12:44:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Editoras](
	[id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Editoras] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Enderecos]    Script Date: 19/06/2017 12:44:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Enderecos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[logradouro] [nvarchar](30) NULL,
	[numero] [int] NOT NULL,
	[complemento] [nvarchar](30) NULL,
	[bairro] [nvarchar](20) NULL,
	[cep] [int] NOT NULL,
	[cidadeId] [int] NULL,
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
/****** Object:  Table [dbo].[Estados]    Script Date: 19/06/2017 12:44:29 ******/
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
/****** Object:  Table [dbo].[Exemplares]    Script Date: 19/06/2017 12:44:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Exemplares](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[titulo] [nvarchar](30) NOT NULL,
	[conservacao] [int] NOT NULL,
	[isbn] [nvarchar](32) NULL,
	[ano] [int] NOT NULL,
	[edicao] [int] NOT NULL,
	[precoCompra] [decimal](18, 2) NOT NULL,
	[precoVenda] [decimal](18, 2) NOT NULL,
	[descricao] [nvarchar](200) NULL,
	[peso] [decimal](18, 2) NOT NULL,
	[vendaOnline] [int] NOT NULL,
	[quantidade] [int] NOT NULL,
	[editoraId] [int] NOT NULL,
	[idiomaId] [int] NOT NULL,
	[sessaoId] [int] NOT NULL,
	[user_insert] [nvarchar](max) NULL,
	[user_update] [nvarchar](max) NULL,
	[date_insert] [datetime] NULL,
	[date_update] [datetime] NULL,
 CONSTRAINT [PK_dbo.Exemplares] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Fornecedores]    Script Date: 19/06/2017 12:44:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Fornecedores](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [nvarchar](50) NULL,
	[cnpj] [nvarchar](50) NULL,
	[telefone] [nvarchar](15) NULL,
	[email] [nvarchar](50) NULL,
	[tipoFornecedor] [int] NOT NULL,
	[user_insert] [nvarchar](max) NULL,
	[user_update] [nvarchar](max) NULL,
	[date_insert] [datetime] NULL,
	[date_update] [datetime] NULL,
 CONSTRAINT [PK_dbo.Fornecedores] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Idiomas]    Script Date: 19/06/2017 12:44:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Idiomas](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [nvarchar](20) NOT NULL,
	[paisId] [int] NOT NULL,
	[user_insert] [nvarchar](max) NULL,
	[user_update] [nvarchar](max) NULL,
	[date_insert] [datetime] NULL,
	[date_update] [datetime] NULL,
 CONSTRAINT [PK_dbo.Idiomas] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
/****** Object:  Table [dbo].[Paises]    Script Date: 19/06/2017 12:44:30 ******/
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
/****** Object:  Table [dbo].[Sessoes]    Script Date: 19/06/2017 12:44:30 ******/
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
/****** Object:  Table [dbo].[Transacoes]    Script Date: 19/06/2017 12:44:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transacoes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[valor] [decimal](18, 2) NOT NULL,
	[data] [datetime] NOT NULL,
	[tipoTransacao] [int] NOT NULL,
	[tipoCredito] [int] NOT NULL,
	[clienteId] [int] NOT NULL,
	[user_insert] [nvarchar](max) NULL,
	[user_update] [nvarchar](max) NULL,
	[date_insert] [datetime] NULL,
	[date_update] [datetime] NULL,
 CONSTRAINT [PK_dbo.Transacoes] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
ALTER TABLE [dbo].[Autores]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Autores_dbo.Enderecos_enderecoId] FOREIGN KEY([enderecoId])
REFERENCES [dbo].[Enderecos] ([id])
GO
ALTER TABLE [dbo].[Autores] CHECK CONSTRAINT [FK_dbo.Autores_dbo.Enderecos_enderecoId]
GO
ALTER TABLE [dbo].[Autores]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Autores_dbo.Exemplares_Exemplar_id] FOREIGN KEY([Exemplar_id])
REFERENCES [dbo].[Exemplares] ([id])
GO
ALTER TABLE [dbo].[Autores] CHECK CONSTRAINT [FK_dbo.Autores_dbo.Exemplares_Exemplar_id]
GO
ALTER TABLE [dbo].[Avaliacoes]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Avaliacoes_dbo.Clientes_clienteId] FOREIGN KEY([clienteId])
REFERENCES [dbo].[Clientes] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Avaliacoes] CHECK CONSTRAINT [FK_dbo.Avaliacoes_dbo.Clientes_clienteId]
GO
ALTER TABLE [dbo].[Cidades]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Cidades_dbo.Estados_estadoId] FOREIGN KEY([estadoId])
REFERENCES [dbo].[Estados] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Cidades] CHECK CONSTRAINT [FK_dbo.Cidades_dbo.Estados_estadoId]
GO
ALTER TABLE [dbo].[Clientes]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Clientes_dbo.Enderecos_enderecoId] FOREIGN KEY([enderecoId])
REFERENCES [dbo].[Enderecos] ([id])
GO
ALTER TABLE [dbo].[Clientes] CHECK CONSTRAINT [FK_dbo.Clientes_dbo.Enderecos_enderecoId]
GO
ALTER TABLE [dbo].[Editoras]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Editoras_dbo.Fornecedores_id] FOREIGN KEY([id])
REFERENCES [dbo].[Fornecedores] ([id])
GO
ALTER TABLE [dbo].[Editoras] CHECK CONSTRAINT [FK_dbo.Editoras_dbo.Fornecedores_id]
GO
ALTER TABLE [dbo].[Enderecos]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Enderecos_dbo.Cidades_cidadeId] FOREIGN KEY([cidadeId])
REFERENCES [dbo].[Cidades] ([id])
GO
ALTER TABLE [dbo].[Enderecos] CHECK CONSTRAINT [FK_dbo.Enderecos_dbo.Cidades_cidadeId]
GO
ALTER TABLE [dbo].[Estados]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Estados_dbo.Paises_paisId] FOREIGN KEY([paisId])
REFERENCES [dbo].[Paises] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Estados] CHECK CONSTRAINT [FK_dbo.Estados_dbo.Paises_paisId]
GO
ALTER TABLE [dbo].[Exemplares]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Exemplares_dbo.Editoras_editoraId] FOREIGN KEY([editoraId])
REFERENCES [dbo].[Editoras] ([id])
GO
ALTER TABLE [dbo].[Exemplares] CHECK CONSTRAINT [FK_dbo.Exemplares_dbo.Editoras_editoraId]
GO
ALTER TABLE [dbo].[Exemplares]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Exemplares_dbo.Idiomas_idiomaId] FOREIGN KEY([idiomaId])
REFERENCES [dbo].[Idiomas] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Exemplares] CHECK CONSTRAINT [FK_dbo.Exemplares_dbo.Idiomas_idiomaId]
GO
ALTER TABLE [dbo].[Exemplares]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Exemplares_dbo.Sessoes_sessaoId] FOREIGN KEY([sessaoId])
REFERENCES [dbo].[Sessoes] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Exemplares] CHECK CONSTRAINT [FK_dbo.Exemplares_dbo.Sessoes_sessaoId]
GO
ALTER TABLE [dbo].[Idiomas]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Idiomas_dbo.Paises_paisId] FOREIGN KEY([paisId])
REFERENCES [dbo].[Paises] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Idiomas] CHECK CONSTRAINT [FK_dbo.Idiomas_dbo.Paises_paisId]
GO
ALTER TABLE [dbo].[Transacoes]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Transacoes_dbo.Clientes_clienteId] FOREIGN KEY([clienteId])
REFERENCES [dbo].[Clientes] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Transacoes] CHECK CONSTRAINT [FK_dbo.Transacoes_dbo.Clientes_clienteId]
GO
