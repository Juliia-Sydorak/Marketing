using System;

namespace MarketingDAL.Entities
{
    public class Order
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }         // зовнішній ключ на User
        public DateTime OrderDate { get; set; }
    }
}
