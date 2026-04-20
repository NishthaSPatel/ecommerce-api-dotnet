BEGIN TRANSACTION;
GO

ALTER TABLE [catalog].[ShippingRate] ADD [Name] nvarchar(max) NULL;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[catalog].[Coupon]') AND [c].[name] = N'StripeCouponId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [catalog].[Coupon] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [catalog].[Coupon] ALTER COLUMN [StripeCouponId] nvarchar(max) NULL;
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