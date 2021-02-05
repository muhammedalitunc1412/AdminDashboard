using AdminDashboard.Shared.Entities.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdminDashboard.Entities.Dtos
{
    public class TicketAddDto : DtoGetBase
    {

        [DisplayName("TicketDetails")]
        [MaxLength(100, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır.")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır.")]
        public string TicketDetails { get; set; }

        [DisplayName("CustomerName")]
        [MinLength(3, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır.")]
        public string CustomerName { get; set; }

        [DisplayName("Resim")]
        [Required(ErrorMessage = "Lütfen, bir {0} seçiniz.")]
        [DataType(DataType.Upload)]
        public IFormFile PictureFile { get; set; }
        public string CustomerPicture { get; set; }


        [DisplayName("CreatedDate")]      
        public string CreatedDate { get; set; }

        [DisplayName("Pirority")]
        [Range(1,3)]
        public int Priority { get; set; }



    }
}
