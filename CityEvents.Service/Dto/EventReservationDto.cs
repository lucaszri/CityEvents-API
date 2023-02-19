using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityEvents.Service.Dto
{
    public class EventReservationDto
    {
        public long IdEvent { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nome é obrigatório!")]
        public string PersonName { get; set; }
        [Required]
        public long Quantity { get; set; }
    }
}
