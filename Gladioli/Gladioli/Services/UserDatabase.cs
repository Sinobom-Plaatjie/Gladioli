using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gladioli.Services
{
    public class UserDatabase
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<UserDatabase> Instance = new AsyncLazy<UserDatabase>(async () =>
        {
            var instance = new UserDatabase();
            CreateTableResult result = await Database.CreateTableAsync<SigningDataModel>();
            return instance;
        });

        public UserDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);

            Database.CreateTableAsync<SigningDataModel>().Wait();
        }

        public Task<List<SigningDataModel>> GetItemsAsync()
        {
            return Database.Table<SigningDataModel>().ToListAsync();
        }

        public Task<List<SigningDataModel>> GetItemsNotDoneAsync()
        {
            return Database.QueryAsync<SigningDataModel>("SELECT * FROM [SigningDataModel]");
        }

        public Task<SigningDataModel> GetItemAsync(int id)
        {
            return Database.Table<SigningDataModel>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(SigningDataModel item)
        {
            //return Database.InsertAsync(item);

            if (item.Id != 0)
            {
                return Database.UpdateAsync(item);
            }

            return Database.InsertAsync(item);

        }

        public Task<int> DeleteItemAsync(SigningDataModel item)
        {
            return Database.DeleteAsync(item);
        }

        public async Task<bool> Login(string userName, string password)
        {
            var user = await Database.Table<SigningDataModel>().Where(x => x.Username == userName).FirstOrDefaultAsync();

            if (user != null)
            {
                if (password == user.Password)
                    return true;

            }

            return false;
        }
    }
}
