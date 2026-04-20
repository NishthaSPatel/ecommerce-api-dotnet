BEGIN TRANSACTION;
GO

ALTER TABLE [production].[Product] ADD [ProductObject] nvarchar(max) NULL;
GO

CREATE TABLE [production].[MediaType] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(255) NOT NULL,
    [Description] nvarchar(max) NULL DEFAULT (('')),
    [IsDeleted] bit NOT NULL DEFAULT (((0))),
    CONSTRAINT [PK_MediaType] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [auth].[RoleType] (
    [Id] bigint NOT NULL IDENTITY,
    [RoleTypeParentId] bigint NULL,
    [Name] nvarchar(255) NOT NULL,
    [Description] nvarchar(max) NULL DEFAULT (('')),
    [AuthRoleId] nvarchar(max) NULL,
    [SortOrder] int NOT NULL DEFAULT (((999))),
    [IsDeleted] bit NOT NULL DEFAULT (((0))),
    CONSTRAINT [PK_RoleType] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RoleType_RoleType] FOREIGN KEY ([RoleTypeParentId]) REFERENCES [auth].[RoleType] ([Id])
);
GO

CREATE TABLE [production].[Sku] (
    [Id] bigint NOT NULL IDENTITY,
    [ProductId] bigint NOT NULL,
    [Name] nvarchar(255) NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [IsDeleted] bit NOT NULL DEFAULT (((0))),
    CONSTRAINT [PK_Sku] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Sku_Product] FOREIGN KEY ([ProductId]) REFERENCES [production].[Product] ([Id])
);
GO

CREATE TABLE [production].[Media] (
    [Id] bigint NOT NULL IDENTITY,
    [MediaTypeId] bigint NOT NULL,
    [Name] nvarchar(255) NOT NULL,
    [Description] nvarchar(max) NULL DEFAULT (('')),
    [URL] nvarchar(max) NULL,
    [IsDeleted] bit NOT NULL DEFAULT (((0))),
    CONSTRAINT [PK_Media] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Media_MediaType] FOREIGN KEY ([MediaTypeId]) REFERENCES [production].[MediaType] ([Id])
);
GO

CREATE TABLE [auth].[Role] (
    [Id] bigint NOT NULL IDENTITY,
    [RoleTypeId] bigint NOT NULL,
    [UserId] bigint NOT NULL,
    [IsDeleted] bit NOT NULL DEFAULT (((0))),
    CONSTRAINT [PK_Role] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Role_RoleType] FOREIGN KEY ([RoleTypeId]) REFERENCES [auth].[RoleType] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Role_User] FOREIGN KEY ([UserId]) REFERENCES [auth].[User] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Media_MediaTypeId] ON [production].[Media] ([MediaTypeId]);
GO

CREATE INDEX [IX_Role_RoleTypeId] ON [auth].[Role] ([RoleTypeId]);
GO

CREATE INDEX [IX_Role_UserId] ON [auth].[Role] ([UserId]);
GO

CREATE INDEX [IX_RoleType_RoleTypeParentId] ON [auth].[RoleType] ([RoleTypeParentId]);
GO

CREATE INDEX [IX_Sku_ProductId] ON [production].[Sku] ([ProductId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231204105912_AddRoleAndMediaTables', N'8.0.0');
GO

COMMIT;
GO