using System;

namespace ElbaMobileXamarinDeveloperTest.Core.DataBase.Repositories.DownloadsHistory
{
    public interface IDownloadsHistoryRepository
    {
        void CreateOrUpdateHistory(DateTime updateDate);

        bool IsNeedToUpdate(TimeSpan timeSpan);
    }
}
