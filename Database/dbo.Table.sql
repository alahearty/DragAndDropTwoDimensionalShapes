CREATE TABLE [dbo].[CanvasShape_tbl]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Width] int NOT NULL,
	[Height] int NOT NULL,
	[Stroke] NVARCHAR(100) NOT NULL,
	[IsSelected] bit NOT NULL, 
	[ShapeLeft] REAL NOT NULL ,
	[ShapeTop] REAL NOT NULL ,
	[ShapeName] NVARCHAR(50) NOT NULL ,
)
