using ElbaMobileXamarinDeveloperTest.Core.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElbaMobileXamarinDeveloperTest.Core.DataBase.Repositories.DownloadsHistory
{
    public interface IDownloadsHistoryRepository
    {
        void CreateOrUpdateHistory(DateTime updateDate);

        bool IsNeedToUpdate(TimeSpan timeSpan);
    }
}
