namespace LossPrevention.Api.Models.Entities;

public class UserMaster
{
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int? IsStatus { get; set; }
    public DateTime? CreationDateTime { get; set; }
}

public class StoreMaster
{
    public int StoreId { get; set; }
    public string StoreName { get; set; } = string.Empty;
    public string StoreCode { get; set; } = string.Empty;
    public string ReaderIP { get; set; } = string.Empty;
    public string ReaderConnType { get; set; } = string.Empty;
    public int IsActive { get; set; }
}
