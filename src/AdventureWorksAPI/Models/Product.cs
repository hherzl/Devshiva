using System;

namespace AdventureWorksAPI.Models
{
    public class Product
    {
        public Int32? ProductID { get; set; }

        public String Name { get; set; }

        public String ProductNumber { get; set; }

        public Boolean? MakeFlag { get; set; }

        public Boolean? FinishedGoodsFlag { get; set; }

        public Int16? SafetyStockLevel { get; set; }

        public Int16? ReorderPoint { get; set; }

        public Decimal? StandardCost { get; set; }

        public Decimal? ListPrice { get; set; }

        public Int32? DaysToManufacture { get; set; }

        public DateTime? SellStartDate { get; set; }

        public Guid? rowguid { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
