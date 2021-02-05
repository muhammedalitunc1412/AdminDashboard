using AdminDashboard.Entities.Concrete;
using AdminDashboard.Entities.Dtos;
using AdminDashboard.Service.Abstract;
using AdminDashboard.Shared.Utilities.Extensions;
using AdminDashboard.Shared.Utilities.Results.ComplexTypes;
using AdminDashboard.WEBUI.Areas.Admin.Models;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdminDashboard.WEBUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;


        public TicketController(ITicketService ticketService, IWebHostEnvironment env, IMapper mapper)
        {
            _ticketService = ticketService;
            _env = env;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _ticketService.GetAll();
            if (result.ResultStatus == ResultStatus.Success)
            {
                return View(result.Data);
            }

            return View();
        }
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_TicketAddPartial");
        }
        [HttpPost]
        public async Task<IActionResult> Add(TicketAddDto ticketAddDto)
        {
            if (ModelState.IsValid)
            {
                ticketAddDto.CustomerPicture = await ImageUpload(ticketAddDto);
                var ticket = _mapper.Map<Ticket>(ticketAddDto);
                var result = await _ticketService.Add(ticketAddDto);
                if (result.ResultStatus==ResultStatus.Success)
                {
                    var userAddAjaxModel = JsonSerializer.Serialize(new TicketAddAjaxViewModel
                    {
                        TicketDto = new TicketDto
                        {
                            ResultStatus = ResultStatus.Success,
                            Message = $"{ticket.CustomerName} adlı kullanıcı adına sahip, kullanıcı başarıyla eklenmiştir.",
                            Ticket = ticket
                        },
                        TicketAddPartial = await this.RenderViewToStringAsync("_UserAddPartial", ticketAddDto)
                    });
                    return Json(userAddAjaxModel);
                }
                else
                {
                    

                    var ticketAddAjaxErrorModel = JsonSerializer.Serialize(new TicketAddAjaxViewModel
                    {
                        TicketAddDto = ticketAddDto,
                       TicketAddPartial = await this.RenderViewToStringAsync("_TicketAddPartial", ticketAddDto)
                    });
                    return Json(ticketAddAjaxErrorModel);
                }

            }
            var ticketAddAjaxModelStateErrorModel = JsonSerializer.Serialize(new TicketAddAjaxViewModel
            {
                TicketAddDto = ticketAddDto,
                TicketAddPartial = await this.RenderViewToStringAsync("_TicketAddPartial", ticketAddDto)
            });
            return Json(ticketAddAjaxModelStateErrorModel);




            //if (ModelState.IsValid)
            //{
            //    var result = await _ticketService.Add(ticketAddDto);
            //    if (result.ResultStatus == ResultStatus.Success)
            //    {
            //        var ticketAddAjaxModel = JsonSerializer.Serialize(new TicketAddAjaxViewModel
            //        {
            //            TicketDto = result.Data,
            //            TicketAddPartial = await this.RenderViewToStringAsync("_TicketAddPartial", ticketAddDto)
            //        });
            //        return Json(ticketAddAjaxModel);
            //    }
            //}
            //var ticketAddAjaxErrorModel = JsonSerializer.Serialize(new TicketAddAjaxViewModel
            //{
            //    TicketAddPartial = await this.RenderViewToStringAsync("_TicketAddPartial", ticketAddDto)
            //});
            //return Json(ticketAddAjaxErrorModel);

        }

        [HttpGet]
        public async Task<IActionResult> Update(int ticketId)
        {
            var result = await _ticketService.GetTicketUpdateDto(ticketId);
            if (result.ResultStatus == ResultStatus.Success)
            {
                return PartialView("_TicketUpdatePartial", result.Data);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update(TicketUpdateDto ticketUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _ticketService.Update(ticketUpdateDto);
                if (result.ResultStatus == ResultStatus.Success)
                {
                    var ticketUpdateAjaxModel = JsonSerializer.Serialize(new TicketUpdateAjaxViewModel
                    {
                        TicketDto = result.Data,
                        TicketUpdatePartial = await this.RenderViewToStringAsync("_TicketUpdatePartial", ticketUpdateDto)
                    });
                    return Json(ticketUpdateAjaxModel);
                }
            }
            var ticketUpdateAjaxErrorModel = JsonSerializer.Serialize(new TicketUpdateAjaxViewModel
            {
                TicketUpdatePartial = await this.RenderViewToStringAsync("_TicketUpdatePartial", ticketUpdateDto)
            });
            return Json(ticketUpdateAjaxErrorModel);

        }


        [HttpPost]
        public async Task<JsonResult> Delete(int ticketId)
        {
            var result = await _ticketService.Delete(ticketId);
            var ajaxResult = JsonSerializer.Serialize(result);
            return Json(ajaxResult);
        }

        public async Task<string> ImageUpload(TicketAddDto ticketAddDto)
        {
            
            string wwwroot = _env.WebRootPath;
          
            string fileExtension = Path.GetExtension(ticketAddDto.PictureFile.FileName);
            DateTime dateTime = DateTime.Now;
         
            string fileName = $"{ticketAddDto.CustomerName}_{dateTime.FullDateAndTimeStringWithUnderscore()}{fileExtension}";
            var path = Path.Combine($"{wwwroot}/img", fileName);
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await ticketAddDto.PictureFile.CopyToAsync(stream);
            }

            return fileName;
        }
    }
}
