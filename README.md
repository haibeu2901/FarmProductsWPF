# 🌾 Farm Products Selling and Tracking System

A comprehensive farm products management system built with WPF and .NET 8. This desktop application provides complete management of farm operations with role-based access control and inventory tracking.

System Preview

<img width="120" height="120" alt="Image" src="https://github.com/user-attachments/assets/92290e2e-5919-4af0-ac83-3dcf50547c50" />
<img width="120" height="120" alt="Image" src="https://github.com/user-attachments/assets/e6b12e65-044c-4509-a587-fd850ea3efc5" />
<img width="120" height="120" alt="Image" src="https://github.com/user-attachments/assets/364c1d05-e945-404a-b2f1-b26ca7456edb" />
<img width="120" height="120" alt="Image" src="https://github.com/user-attachments/assets/c86794c4-1783-4309-9943-84e22980d9f3" />
<img width="120" height="120" alt="Image" src="https://github.com/user-attachments/assets/a0452372-276c-434e-9a5f-3fb55be456a8" />
<img width="120" height="120" alt="Image" src="https://github.com/user-attachments/assets/3da5930b-4113-4eb2-9369-c76e6e0b57c8" />
<img width="120" height="120" alt="Image" src="https://github.com/user-attachments/assets/5906aa74-43ea-4f14-a577-b0cffcfab2fc" />
<img width="120" height="120" alt="Image" src="https://github.com/user-attachments/assets/202f6877-1359-46ce-8e09-9625c515b3a7" />

## 🚀 Features

### 👥 **Multi-Role Access System**
- **Owner Access**: Complete system oversight with analytics and management tools
- **Staff Access**: Operational tools for inventory and order management  
- **Customer Access**: Self-service ordering and account management
- **Secure Login**: Role-based authentication and authorization

### 🌾 **Product Management**
- ✅ Complete CRUD operations for farm products
- ✅ Detailed product information tracking
- ✅ Price management
- ✅ Product search and filtering capabilities
- ✅ Product categorization
<img width="180" height="180" alt="Image" src="https://github.com/user-attachments/assets/d20ea77b-49d0-4502-8a4c-5c7164b201fa" />

### 📦 **Inventory Management**
- ✅ Real-time stock level monitoring
- ✅ Stock status indicators (In Stock, Low Stock, Out of Stock)
- ✅ Stock adjustment tracking
- ✅ Stock history logging
- ✅ Stock update capabilities
<img width="180" height="180" alt="Image" src="https://github.com/user-attachments/assets/9c66b810-a475-4b87-b536-66e82b8c4ad0" />

### 🛒 **Order Management**
- ✅ Complete order lifecycle management
- ✅ Order creation and processing
- ✅ Customer order history
- ✅ Order details tracking
- ✅ Staff order handling
<img width="180" height="180" alt="Image" src="https://github.com/user-attachments/assets/673f76c3-f281-41fd-8d3a-ddc86820e944" />

### 🏷️ **Category Management**
- ✅ Category creation and management
- ✅ Category-based product organization
- ✅ Product-category relationships
- ✅ Category editing and deletion
<img width="180" height="180" alt="Image" src="https://github.com/user-attachments/assets/57199824-75f6-4dfa-821f-725b6a146a8d" />

### 👥 **Account Management**
- ✅ Role-based user accounts (Owner, Staff, Customer)
- ✅ User profile management
- ✅ Account creation and editing
- ✅ Secure authentication
- ✅ Account status management
<img width="180" height="180" alt="Image" src="https://github.com/user-attachments/assets/ae1f5fee-a9af-4d5e-9e11-feb8d245f2b6" />

## 🛠️ Technology Stack

- **Framework**: .NET 8 with WPF
- **Database**: SQL Server with Entity Framework Core
- **Architecture**: Repository pattern
- **UI Design**: Modern WPF styling with custom controls
- **Authentication**: Custom authentication system

## 📋 Prerequisites

Before running this application, make sure you have:

- Visual Studio 2022 or later
- .NET 8 SDK
- SQL Server (Express or higher)
- Windows 10 or later

## 🚀 Installation

1. **Clone the repository**
   `git clone https://github.com/haibeu2901/FarmProductsWPF.git`

2. **Open the solution in Visual Studio**
- Open `FarmProductsWPF.sln` in Visual Studio

3. **Set up the database**
- Update the connection string in `appsettings.json` to point to your SQL Server instance
- Run the database migration or script to create the database schema

4. **Build and run the application**
- Build the solution in Visual Studio
- Run the application

## 👥 User Roles & Access

### 🔑 **Owner Role**
- Full system access and management
- User account management
- System configuration
- Analytics and reporting
- Financial oversight

### 👨‍💼 **Staff Role**
- Product and inventory management
- Order processing
- Customer service
- Basic reporting

### 🛒 **Customer Role**
- Product browsing and ordering
- Order history and tracking
- Profile management

## 📱 Usage Guide

### **Getting Started**
1. **Login**: Use your assigned credentials to access the system
2. **Navigation**: Use the navigation bar at the top to access different modules
3. **Search**: Use the search functionality to find products, orders, or accounts
<img width="120" height="120" alt="Image" src="https://github.com/user-attachments/assets/f3db4db0-ce56-432d-b25e-828a605c1958" />
<img width="120" height="120" alt="Image" src="https://github.com/user-attachments/assets/c478cd08-4b6d-410f-a549-86bf3c80af88" />

### **Product Management**
- **View Products**: Access all products from the Products tab
- **Add Products**: Click "Add Product" and fill in the required information
- **Edit Products**: Click the edit icon in the actions column
- **Delete Products**: Use the delete icon with confirmation prompt
<img width="120" height="120" alt="Image" src="https://github.com/user-attachments/assets/4cb51ddc-6f3d-4fb4-81a8-7d3c545ba13c" />
<img width="120" height="120" alt="Image" src="https://github.com/user-attachments/assets/a3fbb8ea-ddf1-482c-a51a-568b7def00bd" />
<img width="120" height="120" alt="Image" src="https://github.com/user-attachments/assets/6c61bd66-a278-4512-ac37-4cb1b5709b1f" />
<img width="120" height="120" alt="Image" src="https://github.com/user-attachments/assets/5e7357d0-3c3f-40b0-871b-1673ce48cfba" />

### **Stock Management**
- **View Stock**: Access the Stock Management tab
- **Update Stock**: Click on "Update Stock" to adjust quantities
- **Monitor Status**: Visual indicators show stock status (In Stock, Low Stock, Out of Stock)
<img width="120" height="120" alt="Image" src="https://github.com/user-attachments/assets/685215d5-dd11-4285-a32a-845b20900eb1" />
<img width="120" height="120" alt="Image" src="https://github.com/user-attachments/assets/9302b8ab-bd68-4ee9-bda2-cdba8d486004" />
<img width="120" height="120" alt="Image" src="https://github.com/user-attachments/assets/cb685f04-21e1-4e50-85aa-959fd251a35a" />

### **Order Processing**
- **Create Orders**: Use the Create Order tab to start a new order
- **View History**: Check past orders in the Order History tab
- **Process Orders**: Staff can update order status and details
<img width="120" height="120" alt="Image" src="https://github.com/user-attachments/assets/a16eb5bb-6580-4b55-aaad-7216906ec916" />
<img width="120" height="120" alt="Image" src="https://github.com/user-attachments/assets/d3a3040f-4f45-418f-94c6-78ec9dae0273" />
<img width="120" height="120" alt="Image" src="https://github.com/user-attachments/assets/0a5fb09c-33e5-4cc6-93af-3b3e9cf4ff1d" />

## 🔧 Configuration

### **Database Connection**
Configure your database connection in the `appsettings.json` file: 
`{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;uid=sa;pwd=12345;database=FarmProductsDB;TrustServerCertificate=True;"
  }
}`

## 🏗️ Project Structure

- **FarmProductsWPF**: Main WPF application with user interface
- **FarmProductsWPF_BOs**: Business Objects/Entity models
- **FarmProductsWPF_DAOs**: Data Access Objects
- **FarmProductsWPF_Repositories**: Repository implementations

## 🔒 Security Features

- **Role-based Access Control**: Secure user permissions
- **Password Protection**: Secure password storage
- **Input Validation**: Data validation throughout the application
- **Session Management**: Secure user session handling

## 🚀 Deployment

1. **Build the application in Release mode**
2. **Create an installer using the Visual Studio Setup Project**
3. **Distribute the installer to users**

## 📞 Support

For support and questions:

- **Email**: beu2901@gmail.com
- **Documentation**: [github.com/education/students](https://github.com/education/students)
- **Issues**: Contact me via my github

## 🛠️ Development Guidelines

- Follow standard C# coding conventions
- Use the repository pattern for data access
- Implement proper error handling
- Document code changes
- Test thoroughly before committing

---

**Built with ❤️ for modern farm management**

*Last updated: July 2025*
