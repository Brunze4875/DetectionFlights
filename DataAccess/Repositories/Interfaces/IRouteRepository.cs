using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test_app.Models;

namespace test_app.DataAccess.Repositories.Interfaces
{
    public interface IRouteRepository
    {
        public List<Route> GetRoutesBySubscriptions(List<Subscription> subscriptions);
    }
}
