using KaratIQ.Models;
using KaratIQ.Data;
using Microsoft.EntityFrameworkCore;

namespace KaratIQ.Services;

public class BillingService
{
    private readonly KaratIQContext _context;

    public BillingService(KaratIQContext context)
    {
        _context = context;
    }

    public async Task<decimal> CalculateItemValue(JewelleryItem item)
    {
        var rate = await GetCurrentRate(item.MetalType, item.Purity);
        if (rate == null) return 0;

        var metalValue = item.NetWeight * rate.RatePerGram;
        var wastageAmount = metalValue * (item.WastagePercentage / 100);
        var totalValue = metalValue + wastageAmount + item.MakingCharges;

        return totalValue;
    }

    public async Task<Invoice> CreateInvoice(List<InvoiceItem> items, int? customerId = null, bool applyGst = false, decimal discount = 0)
    {
        var invoice = new Invoice
        {
            InvoiceNumber = await GenerateInvoiceNumber(),
            CustomerId = customerId,
            IsGstApplicable = applyGst,
            Discount = discount,
            Items = items
        };

        invoice.SubTotal = items.Sum(i => i.Amount);
        invoice.TotalAmount = invoice.SubTotal - discount;

        if (applyGst)
        {
            invoice.GstAmount = invoice.TotalAmount * 0.03m; // 3% GST
            invoice.TotalAmount += invoice.GstAmount;
        }

        _context.Invoices.Add(invoice);
        await _context.SaveChangesAsync();

        return invoice;
    }

    public async Task<MetalRate?> GetCurrentRate(MetalType metalType, string purity)
    {
        return await _context.MetalRates
            .Where(r => r.MetalType == metalType && r.Purity == purity && r.IsActive)
            .OrderByDescending(r => r.EffectiveDate)
            .FirstOrDefaultAsync();
    }

    public async Task UpdateMetalRate(MetalType metalType, string purity, decimal newRate)
    {
        // Deactivate old rates
        var oldRates = await _context.MetalRates
            .Where(r => r.MetalType == metalType && r.Purity == purity && r.IsActive)
            .ToListAsync();

        foreach (var rate in oldRates)
            rate.IsActive = false;

        // Add new rate
        _context.MetalRates.Add(new MetalRate
        {
            MetalType = metalType,
            Purity = purity,
            RatePerGram = newRate,
            EffectiveDate = DateTime.Today,
            IsActive = true
        });

        await _context.SaveChangesAsync();
    }

    private async Task<string> GenerateInvoiceNumber()
    {
        var today = DateTime.Today;
        var count = await _context.Invoices
            .CountAsync(i => i.InvoiceDate.Date == today) + 1;
        
        return $"INV{today:yyyyMMdd}{count:D3}";
    }
}