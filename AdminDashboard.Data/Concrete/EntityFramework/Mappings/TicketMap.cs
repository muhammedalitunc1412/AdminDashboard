using AdminDashboard.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminDashboard.Data.Concrete.EntityFramework.Mappings
{
    public class TicketMap : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(a => a.Id);

            builder.ToTable("Tickets");

            builder.HasData(
                new Ticket
                {
                    Id = 1,                       
                    CustomerPicture = "defaultUser.png",
                    TicketDetails = "1. Kişinin bileti",
                    Priority=3,
                    CustomerName="Muhammed Ali TUNÇ",
                    CreatedDate="31/01/2021"
                    
                  
                },
                new Ticket
                {
                    Id = 2,
                    CustomerPicture = "defaultUser.png",
                    TicketDetails = "2. Kişinin bileti",
                    Priority = 3,
                    CustomerName = "Muhammed Ali TUNÇ",
                    CreatedDate = "31/01/2021"




                },
                new Ticket
                {
                    Id = 3,
                    CustomerPicture = "defaultUser.png",
                    TicketDetails = "3. Kişinin bileti",
                    Priority = 3,
                    CustomerName = "Muhammed Ali TUNÇ",
                    CreatedDate = "31/01/2021"



                }
            );
        }
    }
}
