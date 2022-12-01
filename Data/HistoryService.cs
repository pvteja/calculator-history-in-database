using CalculatorWithHistory.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorWithHistory.Data
{
    public class HistoryService
    {
        private SQLiteAsyncConnection _dbConnection;

        private async Task SetUpDb()
        {
            if (_dbConnection == null)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "History.db3");
                _dbConnection = new SQLiteAsyncConnection(dbPath);
                await _dbConnection.CreateTableAsync<HistoryExpressionModel>();
            }
        }

        public async Task<int> AddExpresssion(HistoryExpressionModel historyExpressionModel)
        {
            await SetUpDb();
            return await _dbConnection.InsertAsync(historyExpressionModel);
        }

        public async Task<int> DeleteExpression(HistoryExpressionModel historyExpressionModel)
        {
            await SetUpDb();
            return await _dbConnection.DeleteAsync(historyExpressionModel);
        }

        public async Task<List<HistoryExpressionModel>> GetHistory()
        {
            await SetUpDb();
            var historyList = await _dbConnection.Table<HistoryExpressionModel>().ToListAsync();
            return historyList;
        }

        public async Task DeleteTable()
        {
            await SetUpDb();
            await _dbConnection.DeleteAllAsync<HistoryExpressionModel>();
           
        }
    }


}
