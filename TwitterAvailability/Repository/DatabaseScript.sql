/* Product */
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](256) NOT NULL,
	[ProductKey] [nvarchar](256) NOT NULL,
	[Hidden] [bit] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


/*[TweeterIssue]*/
CREATE TABLE [dbo].[TwitterIssue](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TwitterId] [nvarchar](256) NOT NULL, 
	[IntenalId] [nvarchar](256) NOT NULL,
	[Type] [nvarchar](256) NOT NULL,
	[ProductKey] [nvarchar](256) NOT NULL,
	[CountryKey] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](256) NOT NULL,
	[OriginalMessage] [nvarchar](1024) NOT NULL,
	[TwitterDate] [datetime] NOT NULL,
	[StartingDate] [datetime]  NULL,
	[EndingDate] [datetime] NULL,
	[EffectiveDate] [datetime] NULL,
	[EstimatedMinutes] [int] NULL,
	[ProductId] [int] NOT NULL,
	[PreviousTwittId] [int] NULL,
	[Finished] [bit] NOT NULL,
	CONSTRAINT [AK_TwitterID] UNIQUE ([TwitterId]),
	CONSTRAINT [PK_TwitterIssue] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[TwitterIssue]  WITH CHECK ADD  CONSTRAINT [FK_TwitterIssue_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO

ALTER TABLE [dbo].[TwitterIssue] CHECK CONSTRAINT [FK_TwitterIssue_Product]
GO

ALTER TABLE [dbo].[TwitterIssue]  WITH CHECK ADD  CONSTRAINT [FK_TwitterIssue_TwitterIssue] FOREIGN KEY([PreviousTwittId])
REFERENCES [dbo].[TwitterIssue] ([Id])
GO

ALTER TABLE [dbo].[TwitterIssue] CHECK CONSTRAINT [FK_TwitterIssue_TwitterIssue]
GO

/*[IssueDetail]*/
CREATE TABLE [dbo].[IssueDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Description] [nvarchar](256) NOT NULL,
	[FileName] [nvarchar](256)  NULL,
	[ContentType] [nvarchar](256) NULL,
	[Data] [varbinary](max) NULL,
	[InternalID] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_IssueDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE NONCLUSTERED INDEX [InternalID] ON [dbo].[IssueDetail] 
(
	[InternalID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO


CREATE TYPE [dbo].[TableType] AS TABLE([Type] [nvarchar](255) NOT NULL)

GO

CREATE FUNCTION [dbo].[GetNodesFromLeaf]
(	
	@leafId as int
)
RETURNS TABLE 
AS
RETURN 
(
with list(level, Id, PreviousTwittId ) as
( select
    1 as level,
    TI1.Id as 'Id',
    TI1.PreviousTwittId as 'PreviousTwittId'
  from TwitterIssue TI1
    where 
		(SELECT count(T.Id) FROM TwitterIssue AS T where T.PreviousTwittId=TI1.Id) = 0 and 
		 TI1.Id = @leafId
  union all
  select
    level + 1,
    TI2.Id as 'Id',
    TI2.PreviousTwittId as 'PreviousTwittId'
  from list as L 
    join TwitterIssue as TI2
    on L.PreviousTwittId = TI2.Id
)
select
    level, Id,  PreviousTwittId
from list  

)
GO