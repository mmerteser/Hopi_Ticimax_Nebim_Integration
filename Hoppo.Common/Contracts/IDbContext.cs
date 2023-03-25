using Dapper;

namespace Hoppo.Common.Contracts
{
    public interface IDbContext
    {
        IEnumerable<T> Query<T>(string query);
        IEnumerable<T> Query<T>(string query, object obj);
        void Execute(string query, object obj);
        T Execute<T>(string sp, DynamicParameters parameters, string retVal);
        void Execute(string query, DynamicParameters p);
        Task<IEnumerable<T>> QueryAsync<T>(string query);
        Task<IEnumerable<T>> QueryAsync<T>(string query, object obj);
        Task ExecuteAsync(string query, object obj);
        Task<T> ExecuteAsync<T>(string sp, DynamicParameters parameters, string retVal);
        Task ExecuteAsync(string query, DynamicParameters p);
    }
}
