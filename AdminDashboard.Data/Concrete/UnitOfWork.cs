using AdminDashboard.Data.Abstract;
using AdminDashboard.Data.Concrete.EntityFramework.Contexts;
using AdminDashboard.Data.Concrete.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdminDashboard.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AdminDashboardContext _context;
        private EfTicketRepository _ticketRepository;       


        public UnitOfWork(AdminDashboardContext context)
        {
            _context = context;
        }

        public ITicketRepository Tickets => _ticketRepository ?? new EfTicketRepository(_context);

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
