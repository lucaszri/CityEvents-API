using CityEvents.Service.Dto;
using CityEvents.Service.Entity;
using CityEvents.Service.Interface;
using Dapper;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityEvents.Infra.Data.Repository
{
    public class CityEventRepository : ICityEventRepository

    {
        private string _stringConnection;
        public CityEventRepository()
        {
            _stringConnection = Environment.GetEnvironmentVariable("DATABASE_CONFIG");
        }

        public async Task<bool> AddEvent(CityEventEntity cityEvent)
        {
            string query = "INSERT INTO CityEvent (Title,description,dateHourEvent,local,address,price,status) " +
                "VALUES(@Title,@description,@dateHourEvent,@local,@address,@price,@status)";
            DynamicParameters param = new(cityEvent);
            using MySqlConnection conn = new MySqlConnection(_stringConnection);
            int linhasAfetadas = await conn.ExecuteAsync(query, param);
            return linhasAfetadas > 0;
        }

        public async Task<bool> EditEvent(CityEventEntity cityEvent, int eventId)
        {
            string query = "UPDATE CityEvent  set Title = @title,description = @description,dateHourEvent = @dateHourEvent,local = @local," +
                "address = @address,price = @price,status = TRUE where idEvent=@id";

            var param = new DynamicParameters(new
            {
                cityEvent.Title,
                cityEvent.Description,
                cityEvent.DateHourEvent,
                cityEvent.Local,
                cityEvent.Address,
                cityEvent.Price,

            });
            param.Add("id", eventId);

            using MySqlConnection conn = new MySqlConnection(_stringConnection);

            int linhasAfetadas = await conn.ExecuteAsync(query, param);

            return linhasAfetadas > 0;
        }

        public async Task<bool> DeleteEvent(int eventId)
        {
            string query = "DELETE FROM CityEvent where idEvent = @eventId";

            DynamicParameters param = new();
            param.Add("eventId", eventId);

            using MySqlConnection conn = new(_stringConnection);

            int linhasAfetadas = await conn.ExecuteAsync(query, param);

            return linhasAfetadas > 0;
        }

        public async Task<CityEventEntity> SearchTitle(string title)
        {
            var query = "SELECT * FROM CityEvent WHERE Title LIKE @title";
            title = $"%{title}%";

            var param = new DynamicParameters(title);
            param.Add("Title", title);

            using MySqlConnection conn = new(_stringConnection);
           
                return await conn.QueryFirstOrDefaultAsync<CityEventEntity>(query, param);
        }

        public async Task<CityEventEntity> SearchEvent(string local, DateTime date)
        {
            string query = @"SELECT * FROM CityEvent WHERE local = @local and DATE(dateHourEvent) = @date";
            DynamicParameters param = new();
            param.Add("local", local);
            param.Add("date", date);

            using MySqlConnection conn = new(_stringConnection);

            return await conn.QueryFirstOrDefaultAsync<CityEventEntity>(query, param);
        }

        public async Task<CityEventEntity> SearchEvent(decimal minPrice, decimal maxPrice, DateTime date)
        {
            string query = "SELECT * FROM CityEvent where DATE(dateHourEvent) = @date and price between @minPrice and @maxPrice";

            DynamicParameters param = new();
            param.Add("date", date);
            param.Add("minPrice", minPrice);
            param.Add("maxPrice", maxPrice);

            using MySqlConnection conn = new(_stringConnection);

            return await conn.QueryFirstOrDefaultAsync<CityEventEntity>(query, param);

        }
    }
}
