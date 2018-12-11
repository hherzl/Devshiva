using System;
using System.ComponentModel.DataAnnotations;

namespace WideWorldImporters.API.Models
{
#pragma warning disable CS1591
    public class PostStockItemsRequest
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

    public class PutStockItemsRequest
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
        public static StockItem ToEntity(this PostStockItemsRequest request)
            => new StockItem
            {
                StockItemID = request.StockItemID,
                StockItemName = request.StockItemName,
                SupplierID = request.SupplierID,
                ColorID = request.ColorID,
                UnitPackageID = request.UnitPackageID,
                OuterPackageID = request.OuterPackageID,
                Brand = request.Brand,
                Size = request.Size,
                LeadTimeDays = request.LeadTimeDays,
                QuantityPerOuter = request.QuantityPerOuter,
                IsChillerStock = request.IsChillerStock,
                Barcode = request.Barcode,
                TaxRate = request.TaxRate,
                UnitPrice = request.UnitPrice,
                RecommendedRetailPrice = request.RecommendedRetailPrice,
                TypicalWeightPerUnit = request.TypicalWeightPerUnit,
                MarketingComments = request.MarketingComments,
                InternalComments = request.InternalComments,
                CustomFields = request.CustomFields,
                Tags = request.Tags,
                SearchDetails = request.SearchDetails,
                LastEditedBy = request.LastEditedBy,
                ValidFrom = request.ValidFrom,
                ValidTo = request.ValidTo
            };
    }
#pragma warning restore CS1591
}
