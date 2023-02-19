using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityEvents.Service.Dto
{
    public class SearchEventPriceAndDateDto
    {
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public DateTime Date { get; set; }
    }
}
