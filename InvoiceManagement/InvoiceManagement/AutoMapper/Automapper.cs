using AutoMapper;
using InvoiceManagement.Model.ViewModel;
namespace InvoiceManagement.AutoMapper
{
    public interface IAutomapper
    {
        public static IAutomapper Instance { get; set; }

    }
    public class Automapper : IAutomapper
    {
        public static Mapper Instance
        {
            get
            {

                return Config();
            }
            set
            {
                Instance = value;
            }
        }
        public static Mapper Config()
        {
            return new Mapper(
                new MapperConfiguration(config =>
                {
                    config.CreateMap<InvoiceDetailsViewModel, InvoiceDetailsDataModel>();
                    config.CreateMap<InvoiceDetailsDataModel, InvoiceDetailsViewModel>();
                    config.CreateMap<List<InvoiceDetailsDataModel>, List<InvoiceDetailsViewModel>>();
                })
                
                );
        }
    }
}
