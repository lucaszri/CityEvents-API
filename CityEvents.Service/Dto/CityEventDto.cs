using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityEvents.Service.Dto
{
    public class CityEventDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Titulo é obrigatório!")]
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime DateHourEvent { get; set; } = DateTime.MinValue;
        [Required(AllowEmptyStrings = false, ErrorMessage = "Local é obrigatório!")]
        public string Local { get; set; }
        public string? Address { get; set; }
        public decimal? Price { get; set; }
        public bool Status { get; set; }
    }
}
