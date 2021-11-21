namespace PdfInJsonApi;

/// <summary>
/// The PdfModel class.
/// </summary>
public class PdfModel
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

    /// <summary>
    /// The pdf in a base64 string
    /// </summary>
    public string? PdfInBase64 {get;set;}

    /// <summary>
    /// A function to tell if the PfdModel instance has a valid Base64 string
    /// </summary>
    /// <returns>True if the PdfInBase64 string is valid, false otherwise</returns>
    public bool IsValidBase64()
    {
        if (PdfInBase64 == null)
        {
            return false;
        }
        try
        {
            Convert.FromBase64String(PdfInBase64);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
