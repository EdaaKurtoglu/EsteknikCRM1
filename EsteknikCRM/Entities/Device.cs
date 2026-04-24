public class Device
{
    public string Id { get; set; } = string.Empty;
    public string SerialNumber { get; set; } = string.Empty;
    public string DeviceCode { get; set; } = string.Empty;
    public string DeviceName { get; set; } = string.Empty;
    public DateTime? CommissionDate { get; set; }
    public string? Brand { get; set; }
    public string? TopGroup { get; set; }
    public string? SubGroup { get; set; }
    public string? SpecialGroup { get; set; }
    public string? Status { get; set; }
    public string? StockCode { get; set; }
}