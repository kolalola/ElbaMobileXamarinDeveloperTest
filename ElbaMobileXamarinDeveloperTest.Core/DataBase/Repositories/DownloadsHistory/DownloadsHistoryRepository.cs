using ElbaMobileXamarinDeveloperTest.Core.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElbaMobileXamarinDeveloperTest.Core.DataBase.Repositories.DownloadsHistory
{
    public class DownloadsHistoryRepository : Repository, IDownloadsHistoryRepository
    {
        public bool IsNeedToUpdate(TimeSpan timeSpan)
        {
            lock (Locker)
            {
                var history = Database.Table<DownloadHistory>().FirstOrDefault();

                return history == null || ( history != null && (DateTime.UtcNow - history.DownloadDate).TotalMinutes > timeSpan.Minutes);
            }
        }

        public void CreateOrUpdateHistory(DateTime updateDate)
        {
            lock(Locker)
            {
                var history = Database.Table<DownloadHistory>().FirstOrDefault();

                if (history == null)
                    Database.Insert(new DownloadHistory
                    {
                        DownloadDate = updateDate
                    });
                else
                    history.DownloadDate = updateDate;

                Database.Update(history);
            }
        }

        protected override void CreateTables()
        {
            Database.CreateTable<DownloadHistory>();
        }
    }
}
