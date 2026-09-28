CREATE PROCEDURE [dbo].[sp_SaveSingleTag]  
(  
    @SessionID INT,  
    @EPC NVARCHAR(100),  
    @TID NVARCHAR(100) = NULL,  
    @RSSI NVARCHAR(50),  
    @AntennaID INT,  
    @TagStatus VARCHAR(20)  
)  
AS  
BEGIN  
    SET NOCOUNT ON;  

