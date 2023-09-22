using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test_app.DataAccess.DatabaseContext;
using test_app.DataAccess.Repositories.Interfaces;
using test_app.Models;

namespace test_app.DataAccess.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly AirlineContext _context;

        public SubscriptionRepository(AirlineContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public List<Subscription> GetSubscriptionsByAgencyId(int agencyId)
        {
            return _context.Subscriptions.Where(f => f.AgencyId == agencyId).ToList();

        }
    }
}
