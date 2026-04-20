BEGIN TRANSACTION;
GO

ALTER TABLE [catalog].[Order] ADD [Name] nvarchar(max) NULL;
GO

ALTER TABLE [catalog].[Order] ADD [OrderObject] nvarchar(max) NULL;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[catalog].[ShippingRate]') AND [c].[name] = N'StripeShippingRateId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [catalog].[ShippingRate] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [catalog].[ShippingRate] ALTER COLUMN [StripeShippingRateId] nvarchar(max) NULL;
GO

ALTER TABLE [catalog].[ShippingRate] ADD [Name] nvarchar(max) NULL;
GO

ALTER TABLE [catalog].[ShippingRate] ADD [OrderObject] nvarchar(max) NULL;
GO

ALTER TABLE [catalog].[Coupon] ADD [CouponObject] nvarchar(max) NULL;
GO

ALTER TABLE [catalog].[Coupon] ADD [Name] nvarchar(max) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240117110517_ModifyCouponTable', N'8.0.0');
GO

COMMIT;
GO