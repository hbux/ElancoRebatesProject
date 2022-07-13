using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Data.SqlClient;

namespace ElancoLibrary.DataAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;
        private readonly ILogger<SqlDataAccess> _logger;

        public SqlDataAccess(IConfiguration config, ILogger<SqlDataAccess> logger)
        {
            _config = config;
            _logger = logger;
        } 

        /// <summary>
        ///     Gets the connection string from appsettings.json. Private modifier as only this class needs to know
        ///     about the connection string.
        /// </summary>
        /// <param name="name">This is the key name within appsettings.json.</param>
        /// <returns>a string representing the connection to a database.</returns>
        private string GetConnectionString(string name)
        {
            return _config.GetConnectionString(name);
        }

        /// <summary>
        ///     A generic data access method to retrieve data from a database.
        /// </summary>
        /// <typeparam name="T">This is the type of data model we want to retrieve.</typeparam>
        /// <typeparam name="U">An anonymous parameter to query the database with.</typeparam>
        /// <param name="storedProcedure">The name of the stored procedure to execute.</param>
        /// <param name="parameters">An anonymous parameter to query the database with.</param>
        /// <param name="connectionStringName">The name of the database connection key.</param>
        /// <returns>A list of the model type passed in to the method as type T.</returns>
        public async Task<List<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString(connectionStringName)))
            {
                try
                {
                    var data = await connection.QueryAsync<T>(storedProcedure, parameters,
                    commandType: CommandType.StoredProcedure);

                    return data.ToList();
                }
                catch (DataException ex)
                {
                    _logger.LogError(ex, "Could not establish connection with database");
                    throw;
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "An error occured while querying database");
                    throw;
                }
            }
        }

        /// <summary>
        ///     A generic data access method to store data in a database.
        /// </summary>
        /// <typeparam name="T">This is the type of data model we want to store.</typeparam>
        /// <param name="storedProcedure">The name of the stored procedure to execute.</param>
        /// <param name="parameters">An anonymous parameter to query the database with.</param>
        /// <param name="connectionStringName">he name of the database connection key.</param>
        /// <returns>An awaitable task.</returns>
        public async Task SaveData<T>(string storedProcedure, T parameters, string connectionStringName)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString(connectionStringName)))
            {
                try
                {
                    await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
                }
                catch (DataException ex)
                {
                    _logger.LogError(ex, "Could not establish connection with database");
                    throw;
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "An error occured while executing to the database");
                    throw;
                }
            }
        }
    }
}
