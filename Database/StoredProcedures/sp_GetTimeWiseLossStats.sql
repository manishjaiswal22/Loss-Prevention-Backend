
CREATE   PROCEDURE [dbo].[sp_GetTimeWiseLossStats]  
    @ReportDate DATE = NULL  
AS  
BEGIN  
    SET NOCOUNT ON;  
  
    -------------------------------------------------------------------------
    -- 1. Date Validation: Agar date pass na ho 
