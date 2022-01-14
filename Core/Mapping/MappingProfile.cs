using AutoMapper;
using Entity;
using TrashManagementApi_Data;

namespace Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //container mapping
            CreateMap<Container_DataModel, ContainerDto>();
            CreateMap<ContainerDto, Container_DataModel>();

            //vehicle mapping
            CreateMap<Vehicle_DataModel, VehicleDto>();
            CreateMap<VehicleDto, Vehicle_DataModel>();
        }

    }
}
