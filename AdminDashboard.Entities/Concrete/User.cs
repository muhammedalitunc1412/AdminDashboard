﻿using AdminDashboard.Shared.Entities.Abstract;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminDashboard.Entities.Concrete
{
    public class User :IdentityUser<int>
    {    
        public string Picture { get; set; }
     
    }
}
