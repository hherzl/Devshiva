using System;
using System.ComponentModel.DataAnnotations;

namespace WideWorldImporters.API.Models
{
    public class PostStockItemsRequestModel
    {
        [Key]
        public int? StockItemID { get; set; }

        [Required]
        [StringLength(200)]
        public string StockItemName { get; set; }

        [Required]
        public int? SupplierID { get; set; }

        public int? ColorID { get; set; }

        [Required]
        public int? UnitPackageID { get; set; }

        [Required]
        public int? OuterPackageID { get; set; }

        [StringLength(100)]
        public string Brand { get; set; }

        [StringLength(40)]
        public string Size { get; set; }

        [Required]
        public int? LeadTimeDays { get; set; }

        [Required]
        public int? QuantityPerOuter { get; set; }

        [Required]
        public bool? IsChillerStock { get; set; }

        [StringLength(100)]
        public string Barcode { get; set; }

        [Required]
        public decimal? TaxRate { get; set; }

        [Required]
        public decimal? UnitPrice { get; set; }

        public decimal? RecommendedRetailPrice { get; set; }

        [Required]
        public decimal? TypicalWeightPerUnit { get; set; }

        public string MarketingComments { get; set; }

        public string InternalComments { get; set; }

        public string CustomFields { get; set; }

        public string Tags { get; set; }

        [Required]
        public string SearchDetails { get; set; }

        [Required]
        public int? LastEditedBy { get; set; }

        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidTo { get; set; }
    }

    public class PutStockItemsRequestModel
    {
        [Required]
        [StringLength(200)]
        public string StockItemName { get; set; }

        [Required]
        public int? SupplierID { get; set; }

        public int? ColorID { get; set; }

        [Required]
        public decimal? UnitPrice { get; set; }
    }

    public static class Extensions
    {
        public static StockItem ToEntity(this PostStockItemsRequestModel requestModel)
            => new StockItem
            {
                StockItemID = requestModel.StockItemID,
                StockItemName = requestModel.StockItemName,
                SupplierID = requestModel.SupplierID,
                ColorID = requestModel.ColorID,
                UnitPackageID = requestModel.UnitPackageID,
                OuterPackageID = requestModel.OuterPackageID,
                Brand = requestModel.Brand,
                Size = requestModel.Size,
                LeadTimeDays = requestModel.LeadTimeDays,
                QuantityPerOuter = requestModel.QuantityPerOuter,
                IsChillerStock = requestModel.IsChillerStock,
                Barcode = requestModel.Barcode,
                TaxRate = requestModel.TaxRate,
                UnitPrice = requestModel.UnitPrice,
                RecommendedRetailPrice = requestModel.RecommendedRetailPrice,
                TypicalWeightPerUnit = requestModel.TypicalWeightPerUnit,
                MarketingComments = requestModel.MarketingComments,
                InternalComments = requestModel.InternalComments,
                CustomFields = requestModel.CustomFields,
                Tags = requestModel.Tags,
                SearchDetails = requestModel.SearchDetails,
                LastEditedBy = requestModel.LastEditedBy,
                ValidFrom = requestModel.ValidFrom,
                ValidTo = requestModel.ValidTo
            };
    }
}
