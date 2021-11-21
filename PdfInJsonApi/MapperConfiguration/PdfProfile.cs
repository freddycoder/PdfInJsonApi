namespace PdfInJsonApi.MapperConfiguration
{
    using AutoMapper;
    using PdfInJsonApi.Data;

    /// <summary>
    /// AutoMapper profile for the Pdf entity.
    /// </summary>
    public class PdfProfile : Profile
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public PdfProfile()
        {
            CreateMap<PdfModel, PdfDbModel>()
                .ForMember(d => d.Content, o => o.MapFrom(s => Convert.FromBase64String(s.PdfInBase64 ?? "")))
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.Created, o => o.MapFrom(s => s.DateAjout))
                .ReverseMap()
                .ForMember(d => d.PdfInBase64, o => o.MapFrom(s => Convert.ToBase64String(s.Content ?? Array.Empty<byte>())))
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.DateAjout, o => o.MapFrom(s => s.Created));

            CreateMap<PdfMetadataModel, PdfDbModel>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.Created, o => o.MapFrom(s => s.DateAjout))
                .ReverseMap()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.DateAjout, o => o.MapFrom(s => s.Created));
        }
    }
}	