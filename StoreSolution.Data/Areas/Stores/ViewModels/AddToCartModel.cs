﻿namespace StoreSolution.Data.Areas.Stores.ViewModels
{
    public class AddToCartModel
    {
        public int ProductId { get; set; }

        public string? VariationName { get; set; }

        public int Quantity { get; set; }
    }
}
