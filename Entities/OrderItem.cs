using System;

namespace MarketingDAL.Entities
{
    public class OrderItem
    {
        public int OrderItemID { get; set; }
        public int OrderID { get; set; }        // зовнішній ключ на Order
        public int ProductID { get; set; }      // зовнішній ключ на Product
        public int Quantity { get; set; }
    }
}
