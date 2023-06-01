﻿namespace BusinessObject.API.Order.Response
{
    public class OrderDetailResponseModel
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public double? Discount { get; set; }
    }
}
