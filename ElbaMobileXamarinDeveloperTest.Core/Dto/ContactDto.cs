﻿using ElbaMobileXamarinDeveloperTest.Core.DataBase.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElbaMobileXamarinDeveloperTest.Core.Dto
{
    public class ContactDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Height { get; set; }

        public string Biography { get; set; }

        public TemperamentDto Temperament { get; set; }
        
        public IntervalDto EducationPeriod { get; set; }
    }
}
