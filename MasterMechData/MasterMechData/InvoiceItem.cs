using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMechData
{
    public class InvoiceItem
    {
        public int? InvoiceItemSNo;
        public int? InvoiceSNo;
        public int? ItemNo;
        public string ItemDesc;
        public string ItemType;
        public string ItemCatg;
        public double? ItemPrice;
        public string ItemUOM;
        public string ItemSts;
        public double? CGSTRate;
        public double? SGSTRate;
        public double? IGSTRate;
        public string UPCCode;
        public string HSNCode;
        public string SACCode;
        public double? Qty;
        public double? SGSTAmount;
        public double? CGSTAmount;
        public double? IGSTAmount;
        public double? DiscountAmount;
        public double? NetAmt;
        public double? TotalAmount;
        public DateTime Created;
        public string CreatedBy;
        public DateTime? Modified;
        public string ModifiedBy;
        public char Deleted;
        public DateTime? DeletedOn;
        public string DeletedBy;
    }

}
