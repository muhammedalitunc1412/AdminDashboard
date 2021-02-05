using AdminDashboard.Data.Abstract;
using AdminDashboard.Entities.Concrete;
using AdminDashboard.Entities.Dtos;
using AdminDashboard.Service.Abstract;
using AdminDashboard.Shared.Utilities.Results.Abstract;
using AdminDashboard.Shared.Utilities.Results.ComplexTypes;
using AdminDashboard.Shared.Utilities.Results.Concrete;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdminDashboard.Service.Concrete
{
    public class TicketManager : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TicketManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IDataResult<TicketDto>> Add(TicketAddDto ticketAddDto)
        {
            var ticket = _mapper.Map<Ticket>(ticketAddDto);
            var addedTicket = await _unitOfWork.Tickets.AddAsync(ticket);
            await _unitOfWork.SaveAsync();
            return new DataResult<TicketDto>(ResultStatus.Success,message: $"{ticketAddDto.CustomerName} isimli müşterinin bileti başarıyla eklenmiştir.",data: new TicketDto
            {
                Ticket = addedTicket,
                ResultStatus = ResultStatus.Success,
                Message = $"{ticketAddDto.CustomerName} adlı müşterinin bileti başarıyla eklenmiştir."
            });
        }
            public async Task<IResult> Delete(int ticketId)
        {
            var ticket = await _unitOfWork.Tickets.GetAsync(c => c.Id == ticketId);
            if (ticket != null)
            {
                await _unitOfWork.Tickets.DeleteAsync(ticket);
                await _unitOfWork.SaveAsync();
                return new Result(ResultStatus.Success, $"{ticket.CustomerName} isimli kullanıcının bileti başarıyla veritabanından silinmiştir.");
            }
            return new Result(ResultStatus.Error, "Böyle bir bilet bulunamadı.");
        }
        public async Task<IDataResult<TicketDto>> Get(int ticketId)
        {
            var ticket = await _unitOfWork.Tickets.GetAsync(c => c.Id == ticketId);
            if (ticket != null)
            {
                return new DataResult<TicketDto>(ResultStatus.Success, new TicketDto
                {
                    Ticket = ticket,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<TicketDto>(ResultStatus.Error, "Böyle bir bilet bulunamadı.", new TicketDto
            {
                Ticket = null,
                ResultStatus = ResultStatus.Error,
                Message = "Böyle bir bilet bulunamadı."
            });
        }
        public async Task<IDataResult<TicketListDto>> GetAll()
        {
            var tickets = await _unitOfWork.Tickets.GetAllAsync(null);
            if (tickets.Count > -1)
            {
                return new DataResult<TicketListDto>(ResultStatus.Success, new TicketListDto
                {
                    Tickets = tickets,
                    ResultStatus = ResultStatus.Success
                });
            }
            return new DataResult<TicketListDto>(ResultStatus.Error,message: "Hiç bir bilet bulunamadı.",data: new TicketListDto
            {
                Tickets = null,
                ResultStatus = ResultStatus.Error,
                Message = "Hiç bir bilet bulunamadı."
            });
        }

        public async Task<IDataResult<TicketUpdateDto>> GetTicketUpdateDto(int ticketId)
        {
            var result = await _unitOfWork.Tickets.AnyAsync(c => c.Id == ticketId);
            if (result)
            {
                var ticket = await _unitOfWork.Tickets.GetAsync(c => c.Id == ticketId);
                var ticketUpdateDto = _mapper.Map<TicketUpdateDto>(ticket);
                return new DataResult<TicketUpdateDto>(ResultStatus.Success, ticketUpdateDto);
            }
            else
            {
                return new DataResult<TicketUpdateDto>(ResultStatus.Error, message:"Böyle bir bilet bulunamadı.",data: null);
            }
        }

        public async Task<IDataResult<TicketDto>> Update(TicketUpdateDto ticketUpdateDto)
        {
            var oldTicket = await _unitOfWork.Tickets.GetAsync(c => c.Id == ticketUpdateDto.Id);
            var ticket = _mapper.Map<TicketUpdateDto, Ticket>(ticketUpdateDto, oldTicket);     
            var updatedTicket = await _unitOfWork.Tickets.UpdateAsync(ticket);
            await _unitOfWork.SaveAsync();
            return new DataResult<TicketDto>(ResultStatus.Success, message: $"adlı müşterinin bileti başarıyla güncellenmiştir.",data: new TicketDto
            {
                Ticket = updatedTicket,
                ResultStatus = ResultStatus.Success,
                Message = $"{ ticketUpdateDto.CustomerName } adlı müşterinin bileti başarıyla güncellenmiştir."
            });
        }
    }
}
