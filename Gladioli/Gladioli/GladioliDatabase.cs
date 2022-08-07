using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gladioli
{
    public class GladioliDatabase
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<GladioliDatabase> Instance = new AsyncLazy<GladioliDatabase>(async () =>
        {
            var instance = new GladioliDatabase();
            CreateTableResult result = await Database.CreateTableAsync<Journey>();
            return instance;
        });

        public GladioliDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        public Task<List<Journey>> GetItemsAsync()
        {
            return Database.Table<Journey>().ToListAsync();
        }

        public Task<List<Journey>> GetItemsNotDoneAsync()
        {
            return Database.QueryAsync<Journey>("SELECT * FROM [Journey]");
        }

        public Task<Journey> GetItemAsync(int id)
        {
            return Database.Table<Journey>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Journey item)
        {
            if (item.ID != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Journey item)
        {
            return Database.DeleteAsync(item);
        }
    }
}

