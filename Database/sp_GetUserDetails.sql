USE [RFID_ReaderDB];
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

-- ============================================================================
-- Author:        Antigravity / Development Team
-- Create date:   28-09-2026
-- Description:   Fetches user details from tbl_User_Master with flexible filters:
--                - By UserId
--                - By Username
--                - By Username & Password (for login / credential verification)
--                - By IsStatus (Active / Inactive)
--                - All users if no filters provided
-- ============================================================================
CREATE OR ALTER PROCEDURE [dbo].[sp_GetUserDetails]
    @UserId    INT           = NULL,
    @Username  NVARCHAR(50)  = NULL,
    @Password  NVARCHAR(50)  = NULL,
    @IsStatus  INT           = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        u.user_id                                    AS UserId,
        u.username                                   AS Username,
        u.[password]                                 AS [Password],
        u.isStatus                                   AS IsStatus,
        CASE 
            WHEN u.isStatus = 1 THEN 'Active'
            ELSE 'Inactive'
        END                                          AS StatusDescription,
        u.creation_datetime                          AS CreationDateTime,
        CONVERT(VARCHAR(10), u.creation_datetime, 105) AS FormattedCreationDate -- DD-MM-YYYY format
    FROM dbo.tbl_User_Master u WITH (NOLOCK)
    WHERE (@UserId IS NULL OR u.user_id = @UserId)
      AND (@Username IS NULL OR u.username = @Username)
      AND (@Password IS NULL OR u.[password] = @Password)
      AND (@IsStatus IS NULL OR u.isStatus = @IsStatus)
    ORDER BY u.user_id ASC;
END
GO
