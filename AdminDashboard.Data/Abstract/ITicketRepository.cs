using AdminDashboard.Entities.Concrete;
using AdminDashboard.Shared.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminDashboard.Data.Abstract
{
    public interface ITicketRepository : IEntityRepository<Ticket>

    {
    }
}
