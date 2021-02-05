using AdminDashboard.Entities.Concrete;
using AdminDashboard.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminDashboard.Entities.Dtos
{
    public class TicketDto : DtoGetBase
    {
        public Ticket Ticket { get; set; }
    }
}
