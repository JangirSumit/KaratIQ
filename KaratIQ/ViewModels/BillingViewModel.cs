using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KaratIQ.Models;
using KaratIQ.Services;
using KaratIQ.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace KaratIQ.ViewModels;

public partial class BillingViewModel : ObservableObject
{
    private readonly BillingService _billingService;
    private readonly KaratIQContext _context;

    [ObservableProperty]
    private ObservableCollection<InvoiceItem> invoiceItems = new();

    [ObservableProperty]
    private ObservableCollection<Customer> customers = new();

    [ObservableProperty]
    private Customer? selectedCustomer;

    [ObservableProperty]
    private decimal subTotal;

    [ObservableProperty]
    private decimal discount;

    [ObservableProperty]
    private decimal gstAmount;

    [ObservableProperty]
    private decimal totalAmount;

    [ObservableProperty]
    private bool isGstApplicable;

    [ObservableProperty]
    private string itemName = string.Empty;

    [ObservableProperty]
    private decimal weight;

    [ObservableProperty]
    private decimal rate;

    [ObservableProperty]
    private decimal makingCharges;

    public BillingViewModel(BillingService billingService, KaratIQContext context)
    {
        _billingService = billingService;
        _context = context;
        _ = LoadCustomers();
    }

    [RelayCommand]
    private async Task LoadCustomers()
    {
        var customers = await _context.Customers.OrderBy(c => c.Name).ToListAsync();
        Customers.Clear();
        foreach (var customer in customers)
            Customers.Add(customer);
    }

    [RelayCommand]
    private void AddItem()
    {
        if (string.IsNullOrWhiteSpace(ItemName) || Weight <= 0 || Rate <= 0) return;

        var amount = (Weight * Rate) + MakingCharges;
        
        InvoiceItems.Add(new InvoiceItem
        {
            ItemName = ItemName,
            Weight = Weight,
            Rate = Rate,
            MakingCharges = MakingCharges,
            Amount = amount
        });

        CalculateTotals();
        ClearItemForm();
    }

    [RelayCommand]
    private void RemoveItem(InvoiceItem item)
    {
        InvoiceItems.Remove(item);
        CalculateTotals();
    }

    [RelayCommand]
    private async Task GenerateInvoice()
    {
        if (!InvoiceItems.Any()) return;

        var invoice = await _billingService.CreateInvoice(
            InvoiceItems.ToList(),
            SelectedCustomer?.Id,
            IsGstApplicable,
            Discount
        );

        await Shell.Current.DisplayAlert("Success", $"Invoice {invoice.InvoiceNumber} generated!", "OK");
        ClearInvoice();
    }

    private void CalculateTotals()
    {
        SubTotal = InvoiceItems.Sum(i => i.Amount);
        var discountedAmount = SubTotal - Discount;
        
        if (IsGstApplicable)
        {
            GstAmount = discountedAmount * 0.03m;
            TotalAmount = discountedAmount + GstAmount;
        }
        else
        {
            GstAmount = 0;
            TotalAmount = discountedAmount;
        }
    }

    private void ClearItemForm()
    {
        ItemName = string.Empty;
        Weight = 0;
        Rate = 0;
        MakingCharges = 0;
    }

    private void ClearInvoice()
    {
        InvoiceItems.Clear();
        SelectedCustomer = null;
        Discount = 0;
        IsGstApplicable = false;
        CalculateTotals();
    }

    partial void OnDiscountChanged(decimal value) => CalculateTotals();
    partial void OnIsGstApplicableChanged(bool value) => CalculateTotals();
}