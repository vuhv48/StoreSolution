﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSolution.Data.Models
{
    public class ShipmentItem
    {
        public int Id { get; set; }
        public int ShipmentId { get; set; }
        public int OrderItemId { get; set; }
        public int ProductId    { get; set; }
        public int Quantity { get; set; }

        public Product Product { get; set; }
        public Shipment Shipment { get; set; }

    }
}
