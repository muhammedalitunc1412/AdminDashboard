using AdminDashboard.Entities.Dtos;
using AdminDashboard.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdminDashboard.Service.Abstract
{
  public  interface ITicketService
    {
        Task<IDataResult<TicketDto>> Get(int ticketId);
        Task<IDataResult<TicketUpdateDto>> GetTicketUpdateDto(int ticketId);
        Task<IDataResult<TicketListDto>> GetAll();
        Task<IDataResult<TicketDto>> Add(TicketAddDto ticketAddDto);
        Task<IDataResult<TicketDto>> Update(TicketUpdateDto ticketUpdateDto);
        Task<IResult> Delete(int ticketId);
    }
}
