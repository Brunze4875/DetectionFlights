using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test_app.Models;

namespace test_app.DataAccess.Repositories.Interfaces
{
    public interface ISubscriptionRepository
    {
        public List<Subscription> GetSubscriptionsByAgencyId(int agencyId);
    }
}
