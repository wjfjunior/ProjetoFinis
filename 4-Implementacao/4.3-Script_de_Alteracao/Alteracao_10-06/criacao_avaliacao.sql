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