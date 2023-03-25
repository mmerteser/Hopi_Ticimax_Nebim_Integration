using Dapper;
using Hoppo.Common.Common;
using Hoppo.Common.Contracts;
using System.Data;
using System.Data.SqlClient;

namespace Hoppo.Business.DatabaseServices
{
    public class DapperDbContext : IDbContext
    {
        private IDbConnection Connection
        {
            get
            {
                return new SqlConnection(Configuration.ProductionAppConnectionString);
            }
        }

        public IEnumerable<T> Query<T>(string query)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<T>(query, commandTimeout: 400);
            }
        }

        public IEnumerable<T> Query<T>(string query, object obj)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<T>(query, obj, commandTimeout: 400);
            }
        }

        public void Execute(string query, object obj)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute(query, obj);
            }
        }
        public T Execute<T>(string sp, DynamicParameters parameters, string retVal)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();

                var returnCode = dbConnection.Execute(
                    sql: sp,
                    param: parameters,
                    commandType: CommandType.StoredProcedure);

                return parameters.Get<T>(retVal);
            }
        }

        public void Execute(string query, DynamicParameters p)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute(query, p);
            }
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string query)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return await dbConnection.QueryAsync<T>(query, commandTimeout: 400);
            }
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string query, object obj)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return await dbConnection.QueryAsync<T>(query, obj, commandTimeout: 400);
            }
        }

        public async Task ExecuteAsync(string query, object obj)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                await dbConnection.ExecuteAsync(query, obj);
            }
        }

        public async Task<T> ExecuteAsync<T>(string sp, DynamicParameters parameters, string retVal)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();

                var returnCode = await dbConnection.ExecuteAsync(
                    sql: sp,
                    param: parameters,
                    commandType: CommandType.StoredProcedure);

                return parameters.Get<T>(retVal);
            }
        }

        public async Task ExecuteAsync(string query, DynamicParameters p)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                await dbConnection.ExecuteAsync(query, p);
            }
        }
    }
}
