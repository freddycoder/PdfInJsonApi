# nullable disable
using Microsoft.EntityFrameworkCore;

namespace PdfInJsonApi.Data;

/// <summary>
/// The context for the PdfInJsonApi database.
/// </summary>
public class PdfContext : DbContext
{

    /// <summary>
    /// Default constructor
    /// </summary>
    public PdfContext(DbContextOptions<PdfContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Table of Pdfs
    /// </summary>
    public DbSet<PdfDbModel> Pdfs { get; set; }
}