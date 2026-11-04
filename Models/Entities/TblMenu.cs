namespace qlthuvien.Models.Entities;

public class TblMenu
{
    public int MenuID { get; set; }
    public string? MenuName { get; set; }
    public bool? IsActive { get; set; }
    public string? ControllerName { get; set; }
    public string? ActionName { get; set; }
    public int Levels { get; set; }
    public int ParentID { get; set; }
    public string? Link { get; set; }
    public int MenuOrder { get; set; }
    public int Postition { get; set; }
}