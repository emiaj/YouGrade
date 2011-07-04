DROP TABLE dbo.Answer
GO
DROP TABLE dbo.Alternative
GO
DROP TABLE dbo.QuestionDef
GO
DROP TABLE dbo.ExamTake
GO
DROP TABLE dbo.ExamDef
GO
DROP TABLE dbo.[User]
GO
/****** Object:  Table [dbo].[ExamDef]    Script Date: 05/29/2011 12:11:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ExamDef](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Description] [varchar](100) NOT NULL,
	[MinimumOfCorrectAnswers] [int] NOT NULL,
	[Duration] [time](7) NOT NULL,
 CONSTRAINT [PK_ExamDef] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 05/29/2011 12:11:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Login] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Firstname] [varchar](50) NOT NULL,
	[Lastname] [varchar](50) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[QuestionDef]    Script Date: 05/29/2011 12:11:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[QuestionDef](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ExamId] [int] NOT NULL,
	[Text] [varchar](2000) NOT NULL,
	[Url] [varchar](500) NOT NULL,
	StartSeconds int NOT NULL,
	[Explanation] [varchar](2000) NOT NULL,
	[Mark] [varchar](2000) NOT NULL,
	[IsMultiSelect] [bit] NOT NULL,
 CONSTRAINT [PK_QuestionDef] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ExamTake]    Script Date: 05/29/2011 12:11:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ExamTake](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ExamId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[StartDateTime] [datetime] NOT NULL,
	[Duration] [time](7) NOT NULL,
	[Grade] [int] NOT NULL,
	[Status] [char](10) NOT NULL,
 CONSTRAINT [PK_ExamTake] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Alternative]    Script Date: 05/29/2011 12:11:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Alternative](
	[QuestionId] [int] NOT NULL,
	[Id] [char](1) NOT NULL,
	[Text] [varchar](1000) NOT NULL,
	[Correct] [bit] NOT NULL,
 CONSTRAINT [PK_Alternative_1] PRIMARY KEY CLUSTERED 
(
	[QuestionId] ASC,
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Answer]    Script Date: 05/29/2011 12:11:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Answer](
	[ExamTakeId] [int] NOT NULL,
	[QuestionId] [int] NOT NULL,
	[AlternativeId] [char](1) NOT NULL,
	[IsChecked] [bit] NOT NULL,
 CONSTRAINT [PK_Answer] PRIMARY KEY CLUSTERED 
(
	[ExamTakeId] ASC,
	[QuestionId] ASC,
	[AlternativeId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  ForeignKey [FK_Alternative_QuestionDef]    Script Date: 05/29/2011 12:11:42 ******/
ALTER TABLE [dbo].[Alternative]  WITH CHECK ADD  CONSTRAINT [FK_Alternative_QuestionDef] FOREIGN KEY([QuestionId])
REFERENCES [dbo].[QuestionDef] ([Id])
GO
ALTER TABLE [dbo].[Alternative] CHECK CONSTRAINT [FK_Alternative_QuestionDef]
GO
/****** Object:  ForeignKey [FK_Answer_Alternative]    Script Date: 05/29/2011 12:11:42 ******/
ALTER TABLE [dbo].[Answer]  WITH CHECK ADD  CONSTRAINT [FK_Answer_Alternative] FOREIGN KEY([QuestionId], [AlternativeId])
REFERENCES [dbo].[Alternative] ([QuestionId], [Id])
GO
ALTER TABLE [dbo].[Answer] CHECK CONSTRAINT [FK_Answer_Alternative]
GO
/****** Object:  ForeignKey [FK_Answer_ExamTake]    Script Date: 05/29/2011 12:11:42 ******/
ALTER TABLE [dbo].[Answer]  WITH CHECK ADD  CONSTRAINT [FK_Answer_ExamTake] FOREIGN KEY([ExamTakeId])
REFERENCES [dbo].[ExamTake] ([Id])
GO
ALTER TABLE [dbo].[Answer] CHECK CONSTRAINT [FK_Answer_ExamTake]
GO
/****** Object:  ForeignKey [FK_ExamTake_ExamDef]    Script Date: 05/29/2011 12:11:42 ******/
ALTER TABLE [dbo].[ExamTake]  WITH CHECK ADD  CONSTRAINT [FK_ExamTake_ExamDef] FOREIGN KEY([ExamId])
REFERENCES [dbo].[ExamDef] ([Id])
GO
ALTER TABLE [dbo].[ExamTake] CHECK CONSTRAINT [FK_ExamTake_ExamDef]
GO
/****** Object:  ForeignKey [FK_ExamTake_User]    Script Date: 05/29/2011 12:11:42 ******/
ALTER TABLE [dbo].[ExamTake]  WITH CHECK ADD  CONSTRAINT [FK_ExamTake_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[ExamTake] CHECK CONSTRAINT [FK_ExamTake_User]
GO
/****** Object:  ForeignKey [FK_QuestionDef_ExamDef]    Script Date: 05/29/2011 12:11:42 ******/
ALTER TABLE [dbo].[QuestionDef]  WITH CHECK ADD  CONSTRAINT [FK_QuestionDef_ExamDef] FOREIGN KEY([ExamId])
REFERENCES [dbo].[ExamDef] ([Id])
GO
ALTER TABLE [dbo].[QuestionDef] CHECK CONSTRAINT [FK_QuestionDef_ExamDef]
GO
