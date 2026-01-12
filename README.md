# 💎 KaratIQ - Jewellery Shop Management App

A comprehensive .NET MAUI application designed specifically for jewellery shop management with offline-first architecture.

## 🚀 Features Implemented

### ✅ Core MVP Features
- **Inventory Management**: Complete jewellery item tracking with weight, purity, and categories
- **Billing System**: Professional invoice generation with GST support
- **Metal Rate Management**: Daily rate updates for Gold/Silver with different purities
- **Offline-First**: Works without internet using SQLite database

### 🏗️ Architecture
- **.NET MAUI** (Multi-platform App UI)
- **MVVM Pattern** with CommunityToolkit.MVVM
- **Entity Framework Core** with SQLite
- **Dependency Injection** for services
- **Android-First** approach (iOS ready)

## 📱 App Structure

```
KaratIQ/
├── Models/           # Data models (JewelleryItem, Customer, Order, etc.)
├── ViewModels/       # MVVM ViewModels with commands
├── Views/           # XAML pages (Inventory, Billing, etc.)
├── Services/        # Business logic (BillingService)
├── Data/           # Entity Framework DbContext
└── Resources/      # Styles, colors, images
```

## 🛠️ Build Instructions

### Prerequisites
- Visual Studio 2022 (17.8+) or VS Code
- .NET 8.0 SDK
- Android SDK (for Android development)

### Build Steps
1. Clone the repository
2. Open `KaratIQ.sln` in Visual Studio
3. Restore NuGet packages
4. Set startup project to `KaratIQ`
5. Select Android emulator or device
6. Build and run (F5)

### Package Dependencies
- Microsoft.Maui.Controls (8.0.3)
- CommunityToolkit.Mvvm (8.2.2)
- Microsoft.EntityFrameworkCore.Sqlite (8.0.0)
- QuestPDF (2023.12.6) - for invoice generation

## 📊 Database Schema

### Key Tables
- `JewelleryItems` - Inventory with weight, purity, making charges
- `MetalRates` - Daily rates for different metals/purities
- `Customers` - Customer information
- `Orders` - Custom orders with advance tracking
- `Invoices` - Generated bills with items
- `CustomerTransactions` - Payment history

## 🎯 Current Status

### ✅ Completed
- Project structure and configuration
- Core data models
- Inventory management (full CRUD)
- Billing system with calculations
- Database setup with Entity Framework
- MVVM architecture with proper bindings
- Navigation shell with flyout menu

### 🚧 Next Phase (Ready to implement)
- Customer management page
- Order tracking system
- Reports and analytics
- PDF invoice generation
- Metal rate management UI
- Backup/restore functionality

## 💡 Key Business Logic

### Billing Calculation
```
Metal Value = Net Weight × Rate × Purity Factor
+ Making Charges
+ Stone Charges
- Discount
+ GST (if applicable)
= Final Amount
```

### Inventory Tracking
- Supports multiple categories (Ring, Chain, Necklace, etc.)
- Weight-based stock management
- Purity tracking (18K, 22K, 24K, 925 Silver)
- Making charges and wastage calculations

## 🔧 Development Notes

- **Offline-First**: All data stored locally in SQLite
- **Android-Optimized**: Primary target platform
- **Scalable**: Ready for cloud sync and multi-user
- **Jeweller-Friendly**: UI designed for shop owners

## 📈 Future Enhancements

- Cloud synchronization
- Multi-store support
- Advanced reporting
- Barcode scanning
- WhatsApp integration
- AI-powered insights

---

**Built with ❤️ for Jewellery Shop Owners**