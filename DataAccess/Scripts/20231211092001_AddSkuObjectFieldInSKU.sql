BEGIN TRANSACTION;
GO

ALTER TABLE [production].[Sku] ADD [SkuObject] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231211092001_AddSkuObjectFieldInSKU', N'8.0.0');
GO

COMMIT;
GO