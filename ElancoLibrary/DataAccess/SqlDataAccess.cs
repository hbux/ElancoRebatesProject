using Dapper;
using ElancoLibrary.Models.Offers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElancoLibrary.DataAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private IConfiguration _config;

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
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
                var data = await connection.QueryAsync<T>(storedProcedure, parameters,
                    commandType: CommandType.StoredProcedure);

                return data.ToList();
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
                await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
