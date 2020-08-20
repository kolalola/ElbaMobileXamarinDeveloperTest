using ElbaMobileXamarinDeveloperTest.Core.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElbaMobileXamarinDeveloperTest.Core.ViewModels
{
    public class ContactViewModel
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Height { get; set; }

        public string Biography { get; set; }

        public Temperament Temperament { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
