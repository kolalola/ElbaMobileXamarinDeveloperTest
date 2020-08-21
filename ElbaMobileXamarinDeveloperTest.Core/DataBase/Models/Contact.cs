using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElbaMobileXamarinDeveloperTest.Core.DataBase.Models
{
    public class Contact : IEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string ExternalId { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Height { get; set; }

        public string Biography { get; set; }

        public Temperament Temperament { get; set; }

        public DateTime StartEducationPeriod { get; set; }

        public DateTime EndEducationPeriod { get; set; }
    }
}
