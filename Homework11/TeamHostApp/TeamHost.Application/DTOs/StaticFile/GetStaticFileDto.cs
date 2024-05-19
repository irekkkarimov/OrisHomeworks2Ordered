namespace TeamHost.Application.DTOs.StaticFile;

public class GetStaticFileDto
{
    /// <summary>
    /// Путь
    /// </summary>
    public string? Path { get; set; }

    /// <summary>
    /// Размер в КБ
    /// </summary>
    public int? Size { get; set; }

    /// <summary>
    /// Наименование
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Расширение
    /// </summary>
    public string? Extension { get; set; }
    
    /// <summary>
    /// Является ли главным изображением игры
    /// </summary>
    public bool IsMainImage { get; set; } = false;
}