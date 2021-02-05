using AdminDashboard.Entities.Concrete;
using AdminDashboard.Entities.Dtos;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminDashboard.Service.AutoMapper.Profiles
{
   public class TicketProfile:Profile
    {
        public TicketProfile()
        {
            CreateMap<TicketAddDto, Ticket>();
            CreateMap<TicketUpdateDto, Ticket>();
            CreateMap<Ticket, TicketUpdateDto>();

        }
    }
}
