using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdminDashboard.Data.Abstract
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        ITicketRepository Tickets { get; } 
      
  
        Task<int> SaveAsync();
    }
}
