CREATE   PROCEDURE [dbo].[sp_GetDayWiseLossStats]
    @FromDate DATE = NULL,
    @ToDate DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- Agar date nahi aayi to default pichle 7 din (Today mila kar) lega
    IF @ToDate IS NULL
        SET @ToDate 
