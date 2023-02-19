using CityEvents.Service.Dto;
using CityEvents.Service.Entity;
using CityEvents.Service.Interface;
using Dapper;
using MySqlConnector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityEvents.Infra.Data.Repository
{
    public class EventReservationRepository : IEventReservationRepository
    {
        private string _stringConnection;
        public EventReservationRepository()
        {
            _stringConnection = Environment.GetEnvironmentVariable("DATABASE_CONFIG");
        }

        public async Task<bool> AddReservation(EventReservationEntity eventReservationEntity)
        {
            string query = "INSERT INTO EventReservation(idEvent, personName, quantity) VALUES (@idEvent, @personName, @quantity)";
            DynamicParameters param = new(eventReservationEntity);
            
            using MySqlConnection conn = new(_stringConnection);

            int linhasAfetadas = await conn.ExecuteAsync(query, param);

            return linhasAfetadas > 0;
        }
        public async Task<bool> EditReservation(int quantity ,int idReservation)
        {
            string query = "UPDATE EventReservation SET quantity = @quantity WHERE idReservation = @idReservation";

            DynamicParameters param = new();
            param.Add("idReservation", idReservation);
            param.Add("quantity", quantity);

            using MySqlConnection conn = new(_stringConnection);

            int linhasAfetadas = await conn.ExecuteAsync(query, param);

            return linhasAfetadas > 0;
        }
        public async Task<bool> DeleteReservation(int idReservation)
        {
            string query = "DELETE FROM EventReservation WHERE idReservation = @idReservation";

            DynamicParameters param = new();
            param.Add("idReservation", idReservation);

            using MySqlConnection conn = new(_stringConnection);

            int linhasAfetadas = await conn.ExecuteAsync(query, param);

            return linhasAfetadas > 0;

        }
        public async Task<EventReservationEntity> SearchReservation(string personName, string title)
        {
            string query = "SELECT * FROM EventReservation INNER JOIN CityEvent ON CityEvent.IdEvent = EventReservation.IdEvent " +
                "WHERE personName = @personName AND title LIKE @title";

            DynamicParameters param = new();
            title = $"%{title}%";
            param.Add("personName", personName);
            param.Add("title", title);

            using MySqlConnection conn = new(_stringConnection);

            return await conn.QueryFirstOrDefaultAsync<EventReservationEntity>(query, param);
        }
    }
}
