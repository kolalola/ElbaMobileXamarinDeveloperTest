using SQLite;
using System;
using System.IO;

namespace ElbaMobileXamarinDeveloperTest.Core.DataBase
{
    public abstract class Repository
    {
        protected static readonly object Locker = new object();

        protected readonly SQLiteConnection Database;

        /// <summary>
        /// Только для Андройд
        /// </summary>
        protected static string DatabasePath
        {
            get
            {
                var sqliteFilename = "db.db3";

                var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var path = Path.Combine(documentsPath, sqliteFilename);
                return path;
            }
        }

        protected abstract void CreateTables();

        public Repository()
        {
            Database = new SQLiteConnection(DatabasePath);

            CreateTables();
        }
    }
}
