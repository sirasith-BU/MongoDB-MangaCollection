public class CreateMangaDTO
{
    public string Title { get; set; } = string.Empty;
    public string Publisher { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public int Start { get; set; }
    public int End { get; set; }
    public List<int> NotHave { get; set; } = [];
    public string Description { get; set; } = string.Empty;
}

public class UpdateMangaDTO
{
    public string Title { get; set; } = string.Empty;
    public string Publisher { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public int Start { get; set; }
    public int End { get; set; }
    public List<int> NotHave { get; set; } = [];
    public string Description { get; set; } = string.Empty;
}