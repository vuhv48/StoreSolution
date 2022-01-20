using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreSolution.Data.Models
{
    public class Shipment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string TrackingNumber { get; set; }
        public int WarehouseId { get; set; }

        public int CreateById   { get; set; }

        public DateTime CreateDate { get; set; }

        public Warehouse Warehouse { get; set; }

        public IList<ShipmentItem> ShipmentItems { get; set; }

        public Order Order { get; set; }
    }
}
