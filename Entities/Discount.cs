using System;

namespace MarketingDAL.Entities
{
    public class Discount
    {
        public int DiscountID { get; set; }
        public int UserID { get; set; }  
        public decimal DiscountPercent { get; set; }
        public DateTime SetDate { get; set; }
    }
}
