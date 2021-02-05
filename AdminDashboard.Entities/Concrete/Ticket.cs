using AdminDashboard.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminDashboard.Entities.Concrete
{
    public class Ticket : EntityBase, IEntity
    {

        public string CustomerName { get; set; }
        public int Priority { get; set; }
        public string TicketDetails { get; set; }
        public string CustomerPicture { get; set; }
        public string CreatedDate { get; set; }


    }
}
