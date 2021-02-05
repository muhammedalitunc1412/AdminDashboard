using AdminDashboard.Entities.Concrete;
using AdminDashboard.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminDashboard.Entities.Dtos
{
    public class TicketListDto : DtoGetBase
    {
        public IList<Ticket> Tickets { get; set; }
    }
}
