namespace PdfInJsonApi;

class PdfMetadataModel 
{
    /// <summary>
    /// The primary key for the Pdf entity.
    /// </summary>
    public Guid? Id { get; set; }

    /// <summary>
    /// The date the pdf was created
    /// </summary>
    public DateTimeOffset DateAjout {get;set;}

    /// <summary>
    /// The name of the file
    /// </summary>
    public string? Name {get;set;}
}