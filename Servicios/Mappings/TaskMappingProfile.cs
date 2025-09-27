using AutoMapper;
using LIST.TASK.DEMO.Servicios.DTOs;

namespace LIST.TASK.DEMO.Servicios.Mappings
{
    public class TaskMappingProfile : Profile
    {
        public TaskMappingProfile()
        {
            // Mapeo de Task a TaskDto
            CreateMap<Dominio.Task, TaskDto>();

            // Mapeo de CreateTaskDto a Task
            CreateMap<CreateTaskDto, Dominio.Task>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsCompleted, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.CompletedDate, opt => opt.Ignore());

            // Mapeo de UpdateTaskDto a Task
            CreateMap<UpdateTaskDto, Dominio.Task>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
                .ForMember(dest => dest.CompletedDate, opt => opt.Ignore());
        }
    }
}