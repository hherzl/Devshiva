using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace WideWorldImporters.API.Models
{
    public static class IDbConnectionExtensions
    {
        public static async Task<int> CountStockItemsAsync(this IDbConnection connection, int? lastEditedBy = null, int? colorID = null, int? outerPackageID = null, int? supplierID = null, int? unitPackageID = null)
        {
            // Create string builder for query
            var query = new StringBuilder();

            // Create sql statement
            query.Append(" select ");
            query.Append("  count([StockItemID]) ");
            query.Append(" from ");
            query.Append("  [Warehouse].[StockItems] ");
            query.Append(" where ");
            query.Append("  (@lastEditedBy is null or [LastEditedBy] = @lastEditedBy) and ");
            query.Append("  (@colorID is null or [ColorID] = @colorID) and ");
            query.Append("  (@outerPackageID is null or [OuterPackageID] = @outerPackageID) and ");
            query.Append("  (@supplierID is null or [SupplierID] = @supplierID) and ");
            query.Append("  (@unitPackageID is null or [UnitPackageID] = @unitPackageID)  ");

            // Create parameters collection
            var parameters = new DynamicParameters();

            // Add parameters to collection
            parameters.Add("@lastEditedBy", lastEditedBy);
            parameters.Add("@colorID", colorID);
            parameters.Add("@outerPackageID", outerPackageID);
            parameters.Add("@supplierID", supplierID);
            parameters.Add("@unitPackageID", unitPackageID);

            // Retrieve result from database and convert to typed list
            return await connection.ExecuteScalarAsync<int>(new CommandDefinition(query.ToString(), parameters));
        }

        public static async Task<IEnumerable<StockItem>> GetStockItemsAsync(this IDbConnection connection, int pageSize = 10, int pageNumber = 1, int? lastEditedBy = null, int? colorID = null, int? outerPackageID = null, int? supplierID = null, int? unitPackageID = null)
        {
            // Create string builder for query
            var query = new StringBuilder();

            // Create sql statement
            query.Append(" select ");
            query.Append("  [StockItemID], ");
            query.Append("  [StockItemName], ");
            query.Append("  [SupplierID], ");
            query.Append("  [ColorID], ");
            query.Append("  [UnitPackageID], ");
            query.Append("  [OuterPackageID], ");
            query.Append("  [Brand], ");
            query.Append("  [Size], ");
            query.Append("  [LeadTimeDays], ");
            query.Append("  [QuantityPerOuter], ");
            query.Append("  [IsChillerStock], ");
            query.Append("  [Barcode], ");
            query.Append("  [TaxRate], ");
            query.Append("  [UnitPrice], ");
            query.Append("  [RecommendedRetailPrice], ");
            query.Append("  [TypicalWeightPerUnit], ");
            query.Append("  [MarketingComments], ");
            query.Append("  [InternalComments], ");
            query.Append("  [CustomFields], ");
            query.Append("  [Tags], ");
            query.Append("  [SearchDetails], ");
            query.Append("  [LastEditedBy], ");
            query.Append("  [ValidFrom], ");
            query.Append("  [ValidTo] ");
            query.Append(" from ");
            query.Append("  [Warehouse].[StockItems] ");
            query.Append(" where ");
            query.Append("  (@lastEditedBy is null or [LastEditedBy] = @lastEditedBy) and ");
            query.Append("  (@colorID is null or [ColorID] = @colorID) and ");
            query.Append("  (@outerPackageID is null or [OuterPackageID] = @outerPackageID) and ");
            query.Append("  (@supplierID is null or [SupplierID] = @supplierID) and ");
            query.Append("  (@unitPackageID is null or [UnitPackageID] = @unitPackageID)  ");
            query.Append(" order by ");
            query.Append("  [StockItemID] ");
            query.Append(" offset @pageSize * (@pageNumber - 1) rows ");
            query.Append(" fetch next @pageSize rows only ");

            // Create parameters collection
            var parameters = new DynamicParameters();

            // Add parameters to collection
            parameters.Add("@pageSize", pageSize);
            parameters.Add("@pageNumber", pageNumber);
            parameters.Add("@lastEditedBy", lastEditedBy);
            parameters.Add("@colorID", colorID);
            parameters.Add("@outerPackageID", outerPackageID);
            parameters.Add("@supplierID", supplierID);
            parameters.Add("@unitPackageID", unitPackageID);

            // Retrieve result from database and convert to typed list
            return await connection.QueryAsync<StockItem>(new CommandDefinition(query.ToString(), parameters));
        }

        public static async Task<StockItem> GetStockItemAsync(this IDbConnection connection, StockItem entity)
        {
            // Create string builder for query
            var query = new StringBuilder();

            // Create sql statement
            query.Append(" select ");
            query.Append("  [StockItemID], ");
            query.Append("  [StockItemName], ");
            query.Append("  [SupplierID], ");
            query.Append("  [ColorID], ");
            query.Append("  [UnitPackageID], ");
            query.Append("  [OuterPackageID], ");
            query.Append("  [Brand], ");
            query.Append("  [Size], ");
            query.Append("  [LeadTimeDays], ");
            query.Append("  [QuantityPerOuter], ");
            query.Append("  [IsChillerStock], ");
            query.Append("  [Barcode], ");
            query.Append("  [TaxRate], ");
            query.Append("  [UnitPrice], ");
            query.Append("  [RecommendedRetailPrice], ");
            query.Append("  [TypicalWeightPerUnit], ");
            query.Append("  [MarketingComments], ");
            query.Append("  [InternalComments], ");
            query.Append("  [CustomFields], ");
            query.Append("  [Tags], ");
            query.Append("  [SearchDetails], ");
            query.Append("  [LastEditedBy], ");
            query.Append("  [ValidFrom], ");
            query.Append("  [ValidTo] ");
            query.Append(" from ");
            query.Append("  [Warehouse].[StockItems] ");
            query.Append(" where ");
            query.Append("  [StockItemID] = @stockItemID ");

            // Create parameters collection
            var parameters = new DynamicParameters();

            // Add parameters to collection
            parameters.Add("stockItemID", entity.StockItemID);

            // Retrieve result from database and convert to entity class
            return await connection.QueryFirstOrDefaultAsync<StockItem>(query.ToString(), parameters);
        }

        public static async Task<StockItem> GetStockItemByStockItemNameAsync(this IDbConnection connection, StockItem entity)
        {
            // Create string builder for query
            var query = new StringBuilder();

            // Create sql statement
            query.Append(" select ");
            query.Append("  [StockItemID], ");
            query.Append("  [StockItemName], ");
            query.Append("  [SupplierID], ");
            query.Append("  [ColorID], ");
            query.Append("  [UnitPackageID], ");
            query.Append("  [OuterPackageID], ");
            query.Append("  [Brand], ");
            query.Append("  [Size], ");
            query.Append("  [LeadTimeDays], ");
            query.Append("  [QuantityPerOuter], ");
            query.Append("  [IsChillerStock], ");
            query.Append("  [Barcode], ");
            query.Append("  [TaxRate], ");
            query.Append("  [UnitPrice], ");
            query.Append("  [RecommendedRetailPrice], ");
            query.Append("  [TypicalWeightPerUnit], ");
            query.Append("  [MarketingComments], ");
            query.Append("  [InternalComments], ");
            query.Append("  [CustomFields], ");
            query.Append("  [Tags], ");
            query.Append("  [SearchDetails], ");
            query.Append("  [LastEditedBy], ");
            query.Append("  [ValidFrom], ");
            query.Append("  [ValidTo] ");
            query.Append(" from ");
            query.Append("  [Warehouse].[StockItems] ");
            query.Append(" where ");
            query.Append("  [StockItemName] = @stockItemName  ");

            // Create parameters collection
            var parameters = new DynamicParameters();

            // Add parameters to collection
            parameters.Add("stockItemName", entity.StockItemName);

            // Retrieve result from database and convert to entity class
            return await connection.QueryFirstOrDefaultAsync<StockItem>(query.ToString(), parameters);
        }

        public static async Task<int> AddStockItemAsync(this IDbConnection connection, StockItem entity)
        {
            // Create string builder for query
            var query = new StringBuilder();

            // Create sql statement
            query.Append(" insert into ");
            query.Append("  [Warehouse].[StockItems] ");
            query.Append("  ( ");
            query.Append("   [StockItemID], ");
            query.Append("   [StockItemName], ");
            query.Append("   [SupplierID], ");
            query.Append("   [ColorID], ");
            query.Append("   [UnitPackageID], ");
            query.Append("   [OuterPackageID], ");
            query.Append("   [Brand], ");
            query.Append("   [Size], ");
            query.Append("   [LeadTimeDays], ");
            query.Append("   [QuantityPerOuter], ");
            query.Append("   [IsChillerStock], ");
            query.Append("   [Barcode], ");
            query.Append("   [TaxRate], ");
            query.Append("   [UnitPrice], ");
            query.Append("   [RecommendedRetailPrice], ");
            query.Append("   [TypicalWeightPerUnit], ");
            query.Append("   [MarketingComments], ");
            query.Append("   [InternalComments], ");
            query.Append("   [CustomFields], ");
            query.Append("   [Tags], ");
            query.Append("   [SearchDetails], ");
            query.Append("   [LastEditedBy], ");
            query.Append("   [ValidFrom], ");
            query.Append("   [ValidTo] ");
            query.Append("  ) ");
            query.Append(" values ");
            query.Append(" ( ");
            query.Append("  @stockItemID, ");
            query.Append("  @stockItemName, ");
            query.Append("  @supplierID, ");
            query.Append("  @colorID, ");
            query.Append("  @unitPackageID, ");
            query.Append("  @outerPackageID, ");
            query.Append("  @brand, ");
            query.Append("  @size, ");
            query.Append("  @leadTimeDays, ");
            query.Append("  @quantityPerOuter, ");
            query.Append("  @isChillerStock, ");
            query.Append("  @barcode, ");
            query.Append("  @taxRate, ");
            query.Append("  @unitPrice, ");
            query.Append("  @recommendedRetailPrice, ");
            query.Append("  @typicalWeightPerUnit, ");
            query.Append("  @marketingComments, ");
            query.Append("  @internalComments, ");
            query.Append("  @customFields, ");
            query.Append("  @tags, ");
            query.Append("  @searchDetails, ");
            query.Append("  @lastEditedBy, ");
            query.Append("  @validFrom, ");
            query.Append("  @validTo ");
            query.Append(" ) ");

            // Create parameters collection
            var parameters = new DynamicParameters();

            // Add parameters to collection
            parameters.Add("stockItemID", entity.StockItemID);
            parameters.Add("stockItemName", entity.StockItemName);
            parameters.Add("supplierID", entity.SupplierID);
            parameters.Add("colorID", entity.ColorID);
            parameters.Add("unitPackageID", entity.UnitPackageID);
            parameters.Add("outerPackageID", entity.OuterPackageID);
            parameters.Add("brand", entity.Brand);
            parameters.Add("size", entity.Size);
            parameters.Add("leadTimeDays", entity.LeadTimeDays);
            parameters.Add("quantityPerOuter", entity.QuantityPerOuter);
            parameters.Add("isChillerStock", entity.IsChillerStock);
            parameters.Add("barcode", entity.Barcode);
            parameters.Add("taxRate", entity.TaxRate);
            parameters.Add("unitPrice", entity.UnitPrice);
            parameters.Add("recommendedRetailPrice", entity.RecommendedRetailPrice);
            parameters.Add("typicalWeightPerUnit", entity.TypicalWeightPerUnit);
            parameters.Add("marketingComments", entity.MarketingComments);
            parameters.Add("internalComments", entity.InternalComments);
            parameters.Add("customFields", entity.CustomFields);
            parameters.Add("tags", entity.Tags);
            parameters.Add("searchDetails", entity.SearchDetails);
            parameters.Add("lastEditedBy", entity.LastEditedBy);
            parameters.Add("validFrom", entity.ValidFrom);
            parameters.Add("validTo", entity.ValidTo);

            // Execute query in database
            return await connection.ExecuteAsync(new CommandDefinition(query.ToString(), parameters));
        }

        public static async Task<int> UpdateStockItemAsync(this IDbConnection connection, StockItem entity)
        {
            // Create string builder for query
            var query = new StringBuilder();

            // Create sql statement
            query.Append(" update ");
            query.Append("  [Warehouse].[StockItems] ");
            query.Append(" set ");
            query.Append("  [StockItemName] = @stockItemName, ");
            query.Append("  [SupplierID] = @supplierID, ");
            query.Append("  [ColorID] = @colorID, ");
            query.Append("  [UnitPackageID] = @unitPackageID, ");
            query.Append("  [OuterPackageID] = @outerPackageID, ");
            query.Append("  [Brand] = @brand, ");
            query.Append("  [Size] = @size, ");
            query.Append("  [LeadTimeDays] = @leadTimeDays, ");
            query.Append("  [QuantityPerOuter] = @quantityPerOuter, ");
            query.Append("  [IsChillerStock] = @isChillerStock, ");
            query.Append("  [Barcode] = @barcode, ");
            query.Append("  [TaxRate] = @taxRate, ");
            query.Append("  [UnitPrice] = @unitPrice, ");
            query.Append("  [RecommendedRetailPrice] = @recommendedRetailPrice, ");
            query.Append("  [TypicalWeightPerUnit] = @typicalWeightPerUnit, ");
            query.Append("  [MarketingComments] = @marketingComments, ");
            query.Append("  [InternalComments] = @internalComments, ");
            query.Append("  [CustomFields] = @customFields, ");
            query.Append("  [Tags] = @tags, ");
            query.Append("  [SearchDetails] = @searchDetails, ");
            query.Append("  [LastEditedBy] = @lastEditedBy, ");
            query.Append("  [ValidFrom] = @validFrom, ");
            query.Append("  [ValidTo] = @validTo ");
            query.Append(" where ");
            query.Append("  [StockItemID] = @stockItemID ");

            // Create parameters collection
            var parameters = new DynamicParameters();

            // Add parameters to collection
            parameters.Add("stockItemName", entity.StockItemName);
            parameters.Add("supplierID", entity.SupplierID);
            parameters.Add("colorID", entity.ColorID);
            parameters.Add("unitPackageID", entity.UnitPackageID);
            parameters.Add("outerPackageID", entity.OuterPackageID);
            parameters.Add("brand", entity.Brand);
            parameters.Add("size", entity.Size);
            parameters.Add("leadTimeDays", entity.LeadTimeDays);
            parameters.Add("quantityPerOuter", entity.QuantityPerOuter);
            parameters.Add("isChillerStock", entity.IsChillerStock);
            parameters.Add("barcode", entity.Barcode);
            parameters.Add("taxRate", entity.TaxRate);
            parameters.Add("unitPrice", entity.UnitPrice);
            parameters.Add("recommendedRetailPrice", entity.RecommendedRetailPrice);
            parameters.Add("typicalWeightPerUnit", entity.TypicalWeightPerUnit);
            parameters.Add("marketingComments", entity.MarketingComments);
            parameters.Add("internalComments", entity.InternalComments);
            parameters.Add("customFields", entity.CustomFields);
            parameters.Add("tags", entity.Tags);
            parameters.Add("searchDetails", entity.SearchDetails);
            parameters.Add("lastEditedBy", entity.LastEditedBy);
            parameters.Add("validFrom", entity.ValidFrom);
            parameters.Add("validTo", entity.ValidTo);
            parameters.Add("stockItemID", entity.StockItemID);

            // Execute query in database
            return await connection.ExecuteAsync(new CommandDefinition(query.ToString(), parameters));
        }

        public static async Task<int> RemoveStockItemAsync(this IDbConnection connection, StockItem entity)
        {
            // Create string builder for query
            var query = new StringBuilder();

            // Create sql statement
            query.Append(" delete from ");
            query.Append("  [Warehouse].[StockItems] ");
            query.Append(" where ");
            query.Append("  [StockItemID] = @stockItemID ");

            // Create parameters collection
            var parameters = new DynamicParameters();

            // Add parameters to collection
            parameters.Add("stockItemID", entity.StockItemID);

            // Execute query in database
            return await connection.ExecuteAsync(new CommandDefinition(query.ToString(), parameters));
        }
    }
}
