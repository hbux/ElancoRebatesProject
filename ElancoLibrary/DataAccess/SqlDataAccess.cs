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

        private string GetConnectionString(string name = "ElancoData")
        {
            return _config.GetConnectionString(name);
        }

        public async Task<List<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString(connectionStringName)))
            {
                var data = await connection.QueryAsync<T>(storedProcedure, parameters,
                    commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
        }

        public async Task<List<OfferModel>> LoadOffers(string connectionStringName)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString(connectionStringName)))
            {
                var offers = await connection.QueryAsync<OfferModel>("SELECT * FROM Offer");

                foreach (OfferModel offer in offers)
                {
                    var offerDetails = await connection.QueryAsync<OfferDetails>("SELECT * FROM OfferDetails WHERE OfferId = @Id", new { Id = offer.Id });
                    
                    var offerProducts = await connection.QueryAsync<ProductModel>(
                        "SELECT Product.Id, Product.Name, Product.ImageName, Product.ImageType " +
                        "FROM Product " +
                        "INNER JOIN OfferProducts " +
                        "ON OfferProducts.ProductId = Product.Id " +
                        "WHERE OfferProducts.OfferId = @Id", new { Id = offer.Id });

                    offer.Details = offerDetails.ToList();
                    offer.Products = offerProducts.ToList();
                }

                return offers.ToList();
            }
        }

        public async Task SaveData<T>(string storedProcedure, T parameters, string connectionStringName)
        {
            using (IDbConnection connection = new SqlConnection(GetConnectionString(connectionStringName)))
            {
                await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
