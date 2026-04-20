IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF SCHEMA_ID(N'production') IS NULL EXEC(N'CREATE SCHEMA [production];');
GO

IF SCHEMA_ID(N'stripe') IS NULL EXEC(N'CREATE SCHEMA [stripe];');
GO

IF SCHEMA_ID(N'auth') IS NULL EXEC(N'CREATE SCHEMA [auth];');
GO

CREATE TABLE [production].[Brand] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(255) NOT NULL,
    [Description] nvarchar(max) NULL DEFAULT (('')),
    [IsDeleted] bit NOT NULL DEFAULT (((0))),
    CONSTRAINT [PK_Brand] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [production].[Category] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(255) NOT NULL,
    [Description] nvarchar(max) NULL DEFAULT (('')),
    [IsDeleted] bit NOT NULL DEFAULT (((0))),
    CONSTRAINT [PK_Category] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [production].[ProductType] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(255) NOT NULL,
    [Description] nvarchar(max) NULL DEFAULT (('')),
    [SortOrder] int NOT NULL DEFAULT (((999))),
    [IsDeleted] bit NOT NULL DEFAULT (((0))),
    CONSTRAINT [PK_ProductType] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [auth].[UserType] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(255) NOT NULL,
    [Description] nvarchar(max) NULL DEFAULT (('')),
    [SortOrder] int NOT NULL DEFAULT (((999))),
    [IsDeleted] bit NOT NULL DEFAULT (((0))),
    CONSTRAINT [PK_UserType] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [production].[Product] (
    [Id] bigint NOT NULL IDENTITY,
    [BrandId] bigint NOT NULL,
    [CategoryId] bigint NOT NULL,
    [ProductTypeId] bigint NOT NULL,
    [Name] nvarchar(255) NOT NULL,
    [Description] nvarchar(max) NULL DEFAULT (('')),
    [IsDeleted] bit NOT NULL DEFAULT (((0))),
    CONSTRAINT [PK_Product] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Product_Brand] FOREIGN KEY ([BrandId]) REFERENCES [production].[Brand] ([Id]),
    CONSTRAINT [FK_Product_Category] FOREIGN KEY ([CategoryId]) REFERENCES [production].[Category] ([Id]),
    CONSTRAINT [FK_Product_ProductType] FOREIGN KEY ([ProductTypeId]) REFERENCES [production].[ProductType] ([Id])
);
GO

CREATE TABLE [auth].[User] (
    [Id] bigint NOT NULL IDENTITY,
    [UserTypeId] bigint NOT NULL,
    [Name] nvarchar(255) NOT NULL,
    [Email] nvarchar(2000) NOT NULL,
    [Description] nvarchar(4000) NULL,
    [SsoIdentifier] nvarchar(200) NULL,
    [IsGoogleLogin] bit NOT NULL DEFAULT (((0))),
    [IsFacebookLogin] bit NOT NULL DEFAULT (((0))),
    [IsMicrosoftLogin] bit NOT NULL DEFAULT (((0))),
    [IsAppleLogin] bit NOT NULL DEFAULT (((0))),
    [SortOrder] int NOT NULL DEFAULT (((999))),
    [IsDeleted] bit NOT NULL DEFAULT (((0))),
    [LastLogin] datetime2 NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_User_UserType] FOREIGN KEY ([UserTypeId]) REFERENCES [auth].[UserType] ([Id])
);
GO

CREATE TABLE [stripe].[Product] (
    [Id] bigint NOT NULL,
    [StripeProductId] nvarchar(255) NULL,
    [Price] decimal(18,2) NULL,
    [StripePriceId] nvarchar(255) NULL,
    [IsDeleted] bit NOT NULL DEFAULT (((0))),
    CONSTRAINT [PK_Product] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Product_Product] FOREIGN KEY ([Id]) REFERENCES [production].[Product] ([Id])
);
GO

CREATE TABLE [stripe].[Customer] (
    [Id] bigint NOT NULL,
    [StripeCustomerId] nvarchar(255) NULL,
    [IsDeleted] bit NOT NULL DEFAULT (((0))),
    CONSTRAINT [PK_Customer] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Customer_User] FOREIGN KEY ([Id]) REFERENCES [auth].[User] ([Id])
);
GO

CREATE INDEX [IX_Product_BrandId] ON [production].[Product] ([BrandId]);
GO

CREATE INDEX [IX_Product_CategoryId] ON [production].[Product] ([CategoryId]);
GO

CREATE INDEX [IX_Product_ProductTypeId] ON [production].[Product] ([ProductTypeId]);
GO

CREATE INDEX [IX_User_UserTypeId] ON [auth].[User] ([UserTypeId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231130054028_UserAndStripeTables', N'8.0.0');
GO

COMMIT;
GO