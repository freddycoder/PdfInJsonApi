using System.ComponentModel.DataAnnotations;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PdfInJsonApi.Data;

namespace PdfInJsonApi.Controllers;

/// <summary>
/// The PdfController.
/// </summary>
[ApiController]
[Route("[controller]")]
public class PdfController : ControllerBase
{
    /// <summary>
    /// Default constructor
    /// </summary>
    public PdfController(PdfContext context, IMapper mapper, ILogger<PdfController> logger)
    {
        Mapper = mapper;
        Context = context;
        Logger = logger;
    }

    /// <summary>
    /// The mapper
    /// </summary>
    public IMapper Mapper { get; }

    /// <summary>
    /// The context
    /// </summary>
    public PdfContext Context { get; }

    /// <summary>
    /// The logger
    /// </summary>
    public ILogger<PdfController> Logger { get; }

    /// <summary>
    /// Get all Pdfs without their content
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(PdfListModel), 200)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken, [Range(0, int.MaxValue)] int page = 0, [Range(1, 100)] int pageSize = 20)
    {
        return Ok(await Context.Pdfs.AsNoTracking()
                                    .OrderBy(p => p.Id)
                                    .Skip(page * pageSize)
                                    .Take(pageSize)
                                    .ProjectTo<PdfListModel>(Mapper.ConfigurationProvider)
                                    .ToListAsync(cancellationToken));
    }

    /// <summary>
    /// Get a Pdf by its primary key
    /// </summary>
    [HttpGet("{id}", Name = "Pdf")]
    [ProducesResponseType(typeof(PdfModel), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        var item = await Context.Pdfs.FindAsync(new object [] { id }, cancellationToken);
        
        if (item == null)
        {
            return NotFound();
        }

        return Ok(Mapper.Map<PdfModel>(item));
    }

    /// <summary>
    /// Create a new Pdf
    /// </summary>
    /// <response code="201">Returns the newly created item</response>
    /// <response code="400">If the item is null</response>
    /// <response code="400">If the base64 content is not valid</response>
    [HttpPost]
    [ProducesResponseType(typeof(PdfListModel), 201)]
    [ProducesResponseType(400)]
    [RequestSizeLimit(int.MaxValue)]
    public async Task<IActionResult> Post([FromBody] PdfModel item, CancellationToken cancellationToken)
    {
        if (item == null)
        {
            Logger.LogWarning("Pdf is null");
            return BadRequest();
        }

        if (!item.IsValidBase64()) 
        {
            Logger.LogWarning("Pdf is not valid base64");
            return BadRequest("The base64 content is not valid.");
        }

        var entity = await Context.Pdfs.AddAsync(Mapper.Map<PdfDbModel>(item), cancellationToken);

        await Context.SaveChangesAsync(cancellationToken);

        return CreatedAtRoute("Pdf", new { id = entity.Entity.Id }, Mapper.Map<PdfListModel>(entity.Entity));
    }
}
