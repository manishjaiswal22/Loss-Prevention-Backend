CREATE PROCEDURE [dbo].[sp_GetStoreConnectionDetails]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1 StoreName,StoreCode,ReaderIP,ReaderConnType FROM STOREMASTER WHERE IsActive = 1 ORDER BY StoreId;
END
