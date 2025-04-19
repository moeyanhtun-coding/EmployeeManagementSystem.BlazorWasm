namespace EmployeeManagementSystem.Share
{
    public interface IDapperService
    {
        Task<T> QueryFirstOrDefaultAsync<T>(string query, object param = null);
        Task<List<T>> QueryAsync<T>(string query, object param = null);
        Task<int> ExecuteAsync(string query, object param = null);
    }
    public class DapperService : IDapperService
    {
        private readonly string _connectionString;

        public DapperService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> ExecuteAsync(string query, object? param = null)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            var result = await dbConnection.ExecuteAsync(query, param);
            return result;
        }

        public async Task<List<T>> QueryAsync<T>(string query, object? param = null)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            var lst = await dbConnection.QueryAsync<T>(query, param);
            return lst.ToList();
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string query, object? param = null)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            var item = await dbConnection.QueryFirstOrDefaultAsync<T>(query, param); 
            return item;
        }
    }
}
