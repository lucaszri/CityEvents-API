using AutoMapper;
using CityEvents.Infra.Data.Repository;
using CityEvents.Service.Dto;
using CityEvents.Service.Entity;
using CityEvents.Service.Interface;
using CityEvents.Service.Service;

namespace API_ProjFinal_Eventos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ICityEventRepository, CityEventRepository>();
            builder.Services.AddScoped<ICityEventService, CityEventService>();
            builder.Services.AddScoped<IEventReservationRepository, EventReservationRepository>();
            builder.Services.AddScoped<IEventReservationService, EventReservationService>();

            MapperConfiguration mapperConfig = new(mc =>
            {
                mc.CreateMap<CityEventEntity, CityEventDto>().ReverseMap();
                mc.CreateMap<EventReservationEntity, EventReservationDto>().ReverseMap();
            });

            IMapper mapper = mapperConfig.CreateMapper();

            builder.Services.AddSingleton(mapper);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}