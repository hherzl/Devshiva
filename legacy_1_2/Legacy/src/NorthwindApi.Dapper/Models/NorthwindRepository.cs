using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Options;

namespace NorthwindApi.Dapper.Models
{
    public class NorthwindRepository : INorthwindRepository
    {
        public NorthwindRepository(IOptions<AppSettings> appSettings)
        {
            ConnectionString = appSettings.Value.ConnectionString;
        }

        public string ConnectionString { get; }

        public async Task<IEnumerable<Product>> GetProductsAsync(int? supplierID = null, int? categoryID = null)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var query = new StringBuilder();

                query.Append("select ");
                query.Append(" [ProductID], ");
                query.Append(" [ProductName], ");
                query.Append(" [SupplierID], ");
                query.Append(" [CategoryID], ");
                query.Append(" [QuantityPerUnit], ");
                query.Append(" [UnitPrice], ");
                query.Append(" [UnitsInStock], ");
                query.Append(" [UnitsOnOrder], ");
                query.Append(" [ReorderLevel], ");
                query.Append(" [Discontinued] ");
                query.Append("from ");
                query.Append(" [dbo].[Products] ");
                query.Append("where ");
                query.Append(" (@supplierID is null or [SupplierID] = @supplierID) and ");
                query.Append(" (@categoryID is null or [CategoryID] = @categoryID) ");

                var parameters = new DynamicParameters();

                parameters.Add("supplierID", supplierID);
                parameters.Add("categoryID", categoryID);

                return await connection.QueryAsync<Product>(new CommandDefinition(query.ToString(), parameters));
            }
        }

        public async Task<Product> GetProductAsync(Product entity)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var query = new StringBuilder();

                query.Append("select ");
                query.Append(" [ProductID], ");
                query.Append(" [ProductName], ");
                query.Append(" [SupplierID], ");
                query.Append(" [CategoryID], ");
                query.Append(" [QuantityPerUnit], ");
                query.Append(" [UnitPrice], ");
                query.Append(" [UnitsInStock], ");
                query.Append(" [UnitsOnOrder], ");
                query.Append(" [ReorderLevel], ");
                query.Append(" [Discontinued] ");
                query.Append("from ");
                query.Append(" [dbo].[Products] ");
                query.Append("where ");
                query.Append(" [ProductID] = @productID ");

                var parameters = new DynamicParameters();

                parameters.Add("productID", entity.ProductID);

                return await connection.QueryFirstOrDefaultAsync<Product>(query.ToString(), parameters);
            }
        }

        public async Task<int> CreateProductAsync(Product entity)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var query = new StringBuilder();

                query.Append("insert into ");
                query.Append(" [dbo].[Products] ");
                query.Append("( ");
                query.Append(" [ProductName], ");
                query.Append(" [SupplierID], ");
                query.Append(" [CategoryID], ");
                query.Append(" [QuantityPerUnit], ");
                query.Append(" [UnitPrice], ");
                query.Append(" [UnitsInStock], ");
                query.Append(" [UnitsOnOrder], ");
                query.Append(" [ReorderLevel], ");
                query.Append(" [Discontinued] ");
                query.Append(") ");
                query.Append("values ");
                query.Append("( ");
                query.Append(" @productName, ");
                query.Append(" @supplierID, ");
                query.Append(" @categoryID, ");
                query.Append(" @quantityPerUnit, ");
                query.Append(" @unitPrice, ");
                query.Append(" @unitsInStock, ");
                query.Append(" @unitsOnOrder, ");
                query.Append(" @reorderLevel, ");
                query.Append(" @discontinued ");
                query.Append(") ");
                query.Append("select @productID = @@identity ");

                var parameters = new DynamicParameters();

                parameters.Add("productName", entity.ProductName);
                parameters.Add("supplierID", entity.SupplierID);
                parameters.Add("categoryID", entity.CategoryID);
                parameters.Add("quantityPerUnit", entity.QuantityPerUnit);
                parameters.Add("unitPrice", entity.UnitPrice);
                parameters.Add("unitsInStock", entity.UnitsInStock);
                parameters.Add("unitsOnOrder", entity.UnitsOnOrder);
                parameters.Add("reorderLevel", entity.ReorderLevel);
                parameters.Add("discontinued", entity.Discontinued);
                parameters.Add("productID", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var affectedRows = await connection.ExecuteAsync(new CommandDefinition(query.ToString(), parameters));

                entity.ProductID = parameters.Get<int>("productID");

                return affectedRows;
            }
        }

        public async Task<int> UpdateProductAsync(Product entity)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var query = new StringBuilder();

                query.Append("update ");
                query.Append(" [dbo].[Products] ");
                query.Append("set ");
                query.Append(" [ProductName] = @productName, ");
                query.Append(" [SupplierID] = @supplierID, ");
                query.Append(" [CategoryID] = @categoryID, ");
                query.Append(" [QuantityPerUnit] = @quantityPerUnit, ");
                query.Append(" [UnitPrice] = @unitPrice, ");
                query.Append(" [UnitsInStock] = @unitsInStock, ");
                query.Append(" [UnitsOnOrder] = @unitsOnOrder, ");
                query.Append(" [ReorderLevel] = @reorderLevel ");
                query.Append("where ");
                query.Append(" [ProductID] = @productID ");

                var parameters = new DynamicParameters();

                parameters.Add("productName", entity.ProductName);
                parameters.Add("supplierID", entity.SupplierID);
                parameters.Add("categoryID", entity.CategoryID);
                parameters.Add("quantityPerUnit", entity.QuantityPerUnit);
                parameters.Add("unitPrice", entity.UnitPrice);
                parameters.Add("unitsInStock", entity.UnitsInStock);
                parameters.Add("unitsOnOrder", entity.UnitsOnOrder);
                parameters.Add("reorderLevel", entity.ReorderLevel);
                parameters.Add("productID", entity.ProductID);

                return await connection.ExecuteAsync(new CommandDefinition(query.ToString(), parameters));
            }
        }

        public async Task<int> DeleteProductAsync(Product entity)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var query = new StringBuilder();

                query.Append("delete from ");
                query.Append(" [dbo].[Products] ");
                query.Append("where ");
                query.Append(" [ProductID] = @productID ");

                var parameters = new DynamicParameters();

                parameters.Add("productID", entity.ProductID);

                return await connection.ExecuteAsync(new CommandDefinition(query.ToString(), parameters));
            }
        }
    }
}
