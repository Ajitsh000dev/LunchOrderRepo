﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunchOrder.Models.Entitys
{
    public class User
    {
        public int UserID { get; set; }
        public int SchoolID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public string phone { get; set; }
        public int verified { get; set; }
        public DateTime addedon { get; set; }
    }

}
