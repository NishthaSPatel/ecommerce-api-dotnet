BEGIN TRANSACTION;
GO

ALTER TABLE [catalog].[ShippingRate] ADD [ShippingRateObject] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240202070859_AddFieldFromShippingRateTable', N'8.0.0');
GO

COMMIT;
GO