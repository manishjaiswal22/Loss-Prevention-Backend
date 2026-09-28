
CREATE   PROCEDURE [dbo].[sp_GetIncidentReportGrid]
    @Search     NVARCHAR(100) = NULL,   -- Search: EPC, Article No, Description, Store
    @EventType  NVARCHAR(50)  = 'All',  -- 'All', 'Theft', 'Untagged' (UI Tabs)
    @FromDate   DATE          = 
