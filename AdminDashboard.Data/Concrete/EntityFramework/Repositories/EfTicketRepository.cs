using AdminDashboard.Data.Abstract;
using AdminDashboard.Entities.Concrete;
using AdminDashboard.Shared.Data.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminDashboard.Data.Concrete.EntityFramework.Repositories
{
    public class EfTicketRepository : EfEntityRepositoryBase<Ticket>, ITicketRepository
    {
        public EfTicketRepository(DbContext context) : base(context)
        {
        }

    }
}
