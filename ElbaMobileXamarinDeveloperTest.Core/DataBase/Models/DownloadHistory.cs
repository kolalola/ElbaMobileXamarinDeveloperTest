using SQLite;
using System;

namespace ElbaMobileXamarinDeveloperTest.Core.DataBase.Models
{
    public class DownloadHistory : IEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime DownloadDate { get; set; }
    }
}
