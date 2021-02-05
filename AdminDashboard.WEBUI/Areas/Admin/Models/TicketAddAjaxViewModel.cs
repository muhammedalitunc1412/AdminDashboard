using AdminDashboard.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.WEBUI.Areas.Admin.Models
{
    public class TicketAddAjaxViewModel
    {
        public TicketAddDto TicketAddDto { get; set; }
        public string TicketAddPartial { get; set; }
        public TicketDto TicketDto { get; set; }
    }
}
