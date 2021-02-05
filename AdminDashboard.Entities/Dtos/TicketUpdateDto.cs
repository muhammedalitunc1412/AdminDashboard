using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AdminDashboard.Entities.Dtos
{
    public class TicketUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [DisplayName("TicketDetails")]
        [MaxLength(100, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır.")]
        [MinLength(5, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır.")]
        public string TicketDetails { get; set; }

        [DisplayName("CustomerName")]
        [MinLength(3, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır.")]
        public string CustomerName { get; set; }

        [DisplayName("CustomerPicture")]
        [MaxLength(250, ErrorMessage = "{0} alanı {1} karakterden büyük olmamalıdır.")]
        [MinLength(1, ErrorMessage = "{0} alanı {1} karakterden küçük olmamalıdır.")]
        public string CustomerPicture { get; set; }


        [DisplayName("CreatedDate")]
        public string CreatedDate { get; set; }

        [DisplayName("Pirority")]
        [Range(1, 3)]
        public int Priority { get; set; }

    }
}