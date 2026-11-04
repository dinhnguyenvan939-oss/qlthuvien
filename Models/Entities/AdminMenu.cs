namespace qlthuvien.Models.Entities;

public class AdminMenu
{
    public long AdminMenuID { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public int ItemLevel { get; set; }
    public int ParentLevel { get; set; }
    public int ItemOrder { get; set; }
    public bool IsActive { get; set; } = true;
    public string? ItemTarget { get; set; }
    public string? AreaName { get; set; }
    public string? ControllerName { get; set; }
    public string? ActionName { get; set; }
    public string? Icon { get; set; }
    public string? IdName { get; set; }
    public string? RequiredRole { get; set; }
}