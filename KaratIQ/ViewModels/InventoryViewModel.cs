using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KaratIQ.Models;
using KaratIQ.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace KaratIQ.ViewModels;

public partial class InventoryViewModel : ObservableObject
{
    private readonly KaratIQContext _context;

    [ObservableProperty]
    private ObservableCollection<JewelleryItem> items = new();

    [ObservableProperty]
    private JewelleryItem selectedItem = new();

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private string searchText = string.Empty;

    public InventoryViewModel(KaratIQContext context)
    {
        _context = context;
        LoadItemsCommand.Execute(null);
    }

    [RelayCommand]
    private async Task LoadItems()
    {
        IsLoading = true;
        try
        {
            var items = await _context.JewelleryItems
                .Where(i => i.IsActive)
                .OrderBy(i => i.Name)
                .ToListAsync();

            Items.Clear();
            foreach (var item in items)
                Items.Add(item);
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private async Task SaveItem()
    {
        if (string.IsNullOrWhiteSpace(SelectedItem.Name)) return;

        if (SelectedItem.Id == 0)
            _context.JewelleryItems.Add(SelectedItem);
        else
            _context.JewelleryItems.Update(SelectedItem);

        await _context.SaveChangesAsync();
        await LoadItems();
        SelectedItem = new JewelleryItem();
    }

    [RelayCommand]
    private async Task DeleteItem(JewelleryItem item)
    {
        item.IsActive = false;
        _context.JewelleryItems.Update(item);
        await _context.SaveChangesAsync();
        await LoadItems();
    }

    [RelayCommand]
    private void NewItem()
    {
        SelectedItem = new JewelleryItem();
    }

    [RelayCommand]
    private async Task Search()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            await LoadItems();
            return;
        }

        IsLoading = true;
        try
        {
            var items = await _context.JewelleryItems
                .Where(i => i.IsActive && i.Name.Contains(SearchText))
                .OrderBy(i => i.Name)
                .ToListAsync();

            Items.Clear();
            foreach (var item in items)
                Items.Add(item);
        }
        finally
        {
            IsLoading = false;
        }
    }

    public List<JewelleryCategory> Categories => Enum.GetValues<JewelleryCategory>().ToList();
    public List<MetalType> MetalTypes => Enum.GetValues<MetalType>().ToList();
    public List<string> Purities => new() { "18K", "22K", "24K", "925", "999" };
}