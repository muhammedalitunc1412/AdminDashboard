using AdminDashboard.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminDashboard.WEBUI.Areas.Admin.Models
{
    public class TicketUpdateAjaxViewModel
    {
        public TicketUpdateDto TicketUpdateDto { get; set; }
        public string TicketUpdatePartial { get; set; }
        public TicketDto TicketDto { get; set; }
    }
}
