using System.ComponentModel.DataAnnotations;

namespace PdfInJsonApi.Data;

/// <summary>
/// PdfDbModel
/// </summary>
public class PdfDbModel
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// The date the Pdf was created
    /// </summary>
    public DateTimeOffset Created { get; set; }
    
    /// <summary>
    /// The pdf content
    /// </summary>
    [MaxLength(int.MaxValue)]
    public byte[]? Content { get; set; }
}
