using API_ProjFinal_Eventos.Filters;
using AutoMapper;
using CityEvents.Infra.Data.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CityEvents.Service.Dto;
using CityEvents.Service.Entity;
using CityEvents.Service.Interface;
using CityEvents.Service.Service;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

     
            var chaveCripto = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("SECRET_KEY"));
            //Add JWT
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(options =>
                        {
                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuerSigningKey = true,
                                IssuerSigningKey = new SymmetricSecurityKey(chaveCripto),
                                ValidateIssuer = true,
                                ValidIssuer = "APIPessoa.com",
                                ValidateAudience = true,
                                ValidAudience = "EventosApi.com"
                            };
                        });

            //Add Filters
            builder.Services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ExcecaoGeralFilter));
            });

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