using AutoMapper;
using InvoiceManagement.Model;
using InvoiceManagement.Model.ResponseModel;
using InvoiceManagement.Model.ViewModel;
namespace InvoiceManagement.AutoMapper
{
    
    public class InvoiceProfile : Profile
    {
        public InvoiceProfile()
        {
            CreateMap<InvoiceDetailsDataModel, InvoiceDetailsViewModel>();
            CreateMap<InvoiceDetailsViewModel, InvoiceDetailsDataModel>();
            CreateMap<InvoiceDetailsViewModel, InvoiceDetailsResponseModel>()
                 //.ForMember(dest => dest.id, opt => opt.MapFrom(src => src.id))
                 //.ForMember(dest => dest.amount, opt => opt.MapFrom(src => src.amount))
                 //.ForMember(dest => dest.paid_amount, opt => opt.MapFrom(src => src.paid_amount))
                .ForMember(dest => dest.status,opt => opt.MapFrom(src =>Enum.GetName(typeof(InvoiceStatus) ,src.status)))
                .ForMember(dest => dest.due_date, opt => opt.MapFrom(src => src.due_date.ToString("yyyy-mm-dd")));
            
        }
    }
}
