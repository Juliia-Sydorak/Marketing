using System;

namespace MarketingDAL.Entities
{
    public class Discount
    {
        public int DiscountID { get; set; }
        public int UserID { get; set; }         // зовнішній ключ на User
        public decimal DiscountPercent { get; set; }
        public DateTime SetDate { get; set; }
    }
}
