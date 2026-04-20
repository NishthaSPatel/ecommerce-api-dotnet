BEGIN TRANSACTION;
GO

IF SCHEMA_ID(N'catalog') IS NULL EXEC(N'CREATE SCHEMA [catalog];');
GO

CREATE TABLE [catalog].[Coupon] (
    [Id] bigint NOT NULL IDENTITY,
    [StripeCouponId] bigint NOT NULL,
    [CouponCode] nvarchar(max) NOT NULL,
    [PercentOff] int NOT NULL,
    [Duration] nvarchar(max) NULL,
    [IsDeleted] bit NOT NULL DEFAULT (((0))),
    CONSTRAINT [PK_Coupon] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [stripe].[PaymentStatus] (
    [Id] bigint NOT NULL IDENTITY,
    [Name] nvarchar(255) NOT NULL,
    [Description] nvarchar(max) NULL DEFAULT (('')),
    [SortOrder] int NOT NULL DEFAULT (((999))),
    [IsDeleted] bit NOT NULL DEFAULT (((0))),
    CONSTRAINT [PK_PaymentStatus] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [catalog].[ShippingRate] (
    [Id] bigint NOT NULL IDENTITY,
    [StripeShippingRateId] bigint NOT NULL,
    [ShippingMethod] nvarchar(max) NULL,
    [Cost] decimal(18,2) NOT NULL,
    [IsDeleted] bit NOT NULL DEFAULT (((0))),
    CONSTRAINT [PK_ShippingRate] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [catalog].[TaxRate] (
    [Id] bigint NOT NULL IDENTITY,
    [StripeTaxRateId] bigint NOT NULL,
    [DisplayName] nvarchar(max) NULL,
    [Percentage] decimal(18,2) NOT NULL,
    [Inclusive] bit NOT NULL,
    [IsDeleted] bit NOT NULL DEFAULT (((0))),
    CONSTRAINT [PK_TaxRate] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [stripe].[Payment] (
    [Id] bigint NOT NULL IDENTITY,
    [StripePaymentIntentId] nvarchar(255) NOT NULL,
    [StripePaymentIntentClientSecret] nvarchar(255) NOT NULL,
    [EphemeralKey] nvarchar(255) NOT NULL,
    [Description] nvarchar(max) NULL DEFAULT (('')),
    [AmountPaid] float NOT NULL,
    [PaidAt] datetime2 NOT NULL,
    [PaymentStatusId] bigint NOT NULL,
    [IsDeleted] bit NOT NULL DEFAULT (((0))),
    CONSTRAINT [PK_Payment] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Payment_PaymentStatus] FOREIGN KEY ([PaymentStatusId]) REFERENCES [stripe].[PaymentStatus] ([Id])
);
GO

CREATE TABLE [catalog].[Order] (
    [Id] bigint NOT NULL IDENTITY,
    [ProductId] bigint NOT NULL,
    [Quantity] int NOT NULL,
    [CouponId] bigint NOT NULL,
    [TaxRateId] bigint NULL,
    [ShippingRateId] bigint NULL,
    [IsDeleted] bit NOT NULL DEFAULT (((0))),
    CONSTRAINT [PK_Order] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Order_Coupon] FOREIGN KEY ([CouponId]) REFERENCES [catalog].[Coupon] ([Id]),
    CONSTRAINT [FK_Order_Product] FOREIGN KEY ([ProductId]) REFERENCES [production].[Product] ([Id]),
    CONSTRAINT [FK_Order_ShippingRate] FOREIGN KEY ([ShippingRateId]) REFERENCES [catalog].[ShippingRate] ([Id]),
    CONSTRAINT [FK_Order_TaxRate] FOREIGN KEY ([TaxRateId]) REFERENCES [catalog].[TaxRate] ([Id])
);
GO

CREATE TABLE [catalog].[OrderItem] (
    [Id] bigint NOT NULL IDENTITY,
    [OrderId] bigint NOT NULL,
    [SkuId] bigint NOT NULL,
    [IsDeleted] bit NOT NULL DEFAULT (((0))),
    CONSTRAINT [PK_OrderItem] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OrderItem_Order] FOREIGN KEY ([OrderId]) REFERENCES [catalog].[Order] ([Id]),
    CONSTRAINT [FK_OrderItem_Sku] FOREIGN KEY ([SkuId]) REFERENCES [production].[Sku] ([Id])
);
GO

CREATE TABLE [stripe].[OrderPayment] (
    [Id] bigint NOT NULL IDENTITY,
    [OrderId] bigint NOT NULL,
    [PaymentId] bigint NOT NULL,
    [IsDeleted] bit NOT NULL DEFAULT (((0))),
    CONSTRAINT [PK_OrderPayment] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OrderPayment_Order] FOREIGN KEY ([OrderId]) REFERENCES [catalog].[Order] ([Id]),
    CONSTRAINT [FK_OrderPayment_Payment] FOREIGN KEY ([PaymentId]) REFERENCES [stripe].[Payment] ([Id])
);
GO

CREATE INDEX [IX_Order_CouponId] ON [catalog].[Order] ([CouponId]);
GO

CREATE INDEX [IX_Order_ProductId] ON [catalog].[Order] ([ProductId]);
GO

CREATE INDEX [IX_Order_ShippingRateId] ON [catalog].[Order] ([ShippingRateId]);
GO

CREATE INDEX [IX_Order_TaxRateId] ON [catalog].[Order] ([TaxRateId]);
GO

CREATE INDEX [IX_OrderItem_OrderId] ON [catalog].[OrderItem] ([OrderId]);
GO

CREATE INDEX [IX_OrderItem_SkuId] ON [catalog].[OrderItem] ([SkuId]);
GO

CREATE INDEX [IX_OrderPayment_OrderId] ON [stripe].[OrderPayment] ([OrderId]);
GO

CREATE INDEX [IX_OrderPayment_PaymentId] ON [stripe].[OrderPayment] ([PaymentId]);
GO

CREATE INDEX [IX_Payment_PaymentStatusId] ON [stripe].[Payment] ([PaymentStatusId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240116130101_AddOrderAndTaxTables', N'8.0.0');
GO

COMMIT;
GO