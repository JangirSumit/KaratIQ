using System.ComponentModel.DataAnnotations;

namespace KaratIQ.Models;

public class JewelleryItem
{
    public int Id { get; set; }
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public JewelleryCategory Category { get; set; }
    [Required] public MetalType MetalType { get; set; }
    [Required] public string Purity { get; set; } = string.Empty; // 18K, 22K, 24K
    public decimal GrossWeight { get; set; }
    public decimal NetWeight { get; set; }
    public decimal StoneWeight { get; set; }
    public decimal MakingCharges { get; set; }
    public decimal WastagePercentage { get; set; }
    public int Quantity { get; set; }
    public decimal MinStockLevel { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public bool IsActive { get; set; } = true;
}

public class Customer
{
    public int Id { get; set; }
    [Required] public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public List<Order> Orders { get; set; } = new();
    public List<CustomerTransaction> Transactions { get; set; } = new();
}

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    public string Description { get; set; } = string.Empty;
    public decimal EstimatedAmount { get; set; }
    public decimal AdvanceReceived { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public DateTime? ExpectedDelivery { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public string Notes { get; set; } = string.Empty;
}

public class MetalRate
{
    public int Id { get; set; }
    public MetalType MetalType { get; set; }
    public string Purity { get; set; } = string.Empty;
    public decimal RatePerGram { get; set; }
    public DateTime EffectiveDate { get; set; } = DateTime.Today;
    public bool IsActive { get; set; } = true;
}

public class Invoice
{
    public int Id { get; set; }
    public int? CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime InvoiceDate { get; set; } = DateTime.Now;
    public List<InvoiceItem> Items { get; set; } = new();
    public decimal SubTotal { get; set; }
    public decimal GstAmount { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsGstApplicable { get; set; }
}

public class InvoiceItem
{
    public int Id { get; set; }
    public int InvoiceId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public decimal Weight { get; set; }
    public decimal Rate { get; set; }
    public decimal MakingCharges { get; set; }
    public decimal Amount { get; set; }
}

public class CustomerTransaction
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime TransactionDate { get; set; } = DateTime.Now;
    public int? OrderId { get; set; }
    public Order? Order { get; set; }
}

public enum JewelleryCategory
{
    Ring, Chain, Necklace, Bangle, Coin, Earring, Bracelet
}

public enum MetalType
{
    Gold, Silver, Platinum
}

public enum OrderStatus
{
    Pending, InMaking, Ready, Delivered, Cancelled
}

public enum TransactionType
{
    Advance, Payment, Refund, Adjustment
}