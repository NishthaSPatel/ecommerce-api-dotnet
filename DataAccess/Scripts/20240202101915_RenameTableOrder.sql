BEGIN TRANSACTION;
GO

DROP TABLE [catalog].[OrderItem];
GO

DROP TABLE [stripe].[OrderPayment];
GO

DROP TABLE [catalog].[Order];
GO

CREATE TABLE [catalog].[Checkout] (
    [Id] bigint NOT NULL IDENTITY,
    [ProductId] bigint NOT NULL,
    [Name] nvarchar(max) NULL,
    [Quantity] int NOT NULL,
    [CouponId] bigint NOT NULL,
    [TaxRateId] bigint NULL,
    [ShippingRateId] bigint NULL,
    [CheckoutObject] nvarchar(max) NULL,
    [IsDeleted] bit NOT NULL DEFAULT (((0))),
    CONSTRAINT [PK_Checkout] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Checkout_Coupon] FOREIGN KEY ([CouponId]) REFERENCES [catalog].[Coupon] ([Id]),
    CONSTRAINT [FK_Checkout_Product] FOREIGN KEY ([ProductId]) REFERENCES [production].[Product] ([Id]),
    CONSTRAINT [FK_Checkout_ShippingRate] FOREIGN KEY ([ShippingRateId]) REFERENCES [catalog].[ShippingRate] ([Id]),
    CONSTRAINT [FK_Checkout_TaxRate] FOREIGN KEY ([TaxRateId]) REFERENCES [catalog].[TaxRate] ([Id])
);
GO

CREATE TABLE [stripe].[CheckoutPayment] (
    [Id] bigint NOT NULL IDENTITY,
    [CheckoutId] bigint NOT NULL,
    [PaymentId] bigint NOT NULL,
    [IsDeleted] bit NOT NULL DEFAULT (((0))),
    CONSTRAINT [PK_CheckoutPayment] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OrderPayment_Order] FOREIGN KEY ([CheckoutId]) REFERENCES [catalog].[Checkout] ([Id]),
    CONSTRAINT [FK_OrderPayment_Payment] FOREIGN KEY ([PaymentId]) REFERENCES [stripe].[Payment] ([Id])
);
GO

CREATE INDEX [IX_Checkout_CouponId] ON [catalog].[Checkout] ([CouponId]);
GO

CREATE INDEX [IX_Checkout_ProductId] ON [catalog].[Checkout] ([ProductId]);
GO

CREATE INDEX [IX_Checkout_ShippingRateId] ON [catalog].[Checkout] ([ShippingRateId]);
GO

CREATE INDEX [IX_Checkout_TaxRateId] ON [catalog].[Checkout] ([TaxRateId]);
GO

CREATE INDEX [IX_CheckoutPayment_CheckoutId] ON [stripe].[CheckoutPayment] ([CheckoutId]);
GO

CREATE INDEX [IX_CheckoutPayment_PaymentId] ON [stripe].[CheckoutPayment] ([PaymentId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240202101915_RenameTableOrder', N'8.0.0');
GO

COMMIT;
GO