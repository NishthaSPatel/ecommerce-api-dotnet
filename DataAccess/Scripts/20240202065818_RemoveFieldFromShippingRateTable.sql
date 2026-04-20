BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[catalog].[ShippingRate]') AND [c].[name] = N'ShippingMethod');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [catalog].[ShippingRate] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [catalog].[ShippingRate] DROP COLUMN [ShippingMethod];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240202065818_RemoveFieldFromShippingRateTable', N'8.0.0');
GO

COMMIT;
GO