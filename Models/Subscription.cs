using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_app.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public int AgencyId { get; set; }
        public int OriginCityId { get; set; }
        public int DestinationCityId { get; set; }
    }
}
