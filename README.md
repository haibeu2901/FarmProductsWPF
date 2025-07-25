# 🌾 Farm Products Selling and Tracking System

A comprehensive farm products management system built with WPF and .NET 8. This desktop application provides complete management of farm operations with role-based access control and inventory tracking.

![System Preview]<img width="1470" height="759" alt="Image" src="https://github.com/user-attachments/assets/442f22c5-33bc-41c9-9c3d-1a7498a2c169" />

## 🚀 Features

### 👥 **Multi-Role Access System**
- **Owner Access**: Complete system oversight with analytics and management tools
- **Staff Access**: Operational tools for inventory and order management  
- **Customer Access**: Self-service ordering and account management
- **Secure Login**: Role-based authentication and authorization
<img width="544" height="429" alt="Image" src="https://github.com/user-attachments/assets/852a07d6-e954-4fa5-9183-849b83ed061e" />

### 🌾 **Product Management**
- ✅ Complete CRUD operations for farm products
- ✅ Detailed product information tracking
- ✅ Price management
- ✅ Product search and filtering capabilities
- ✅ Product categorization
[<img width="1711" height="933" alt="Image" src="https://github.com/user-attachments/assets/4f652ec0-5f46-47e6-9222-1a4629412e3f" />,<img width="1715" height="943" alt="Image" src="https://github.com/user-attachments/assets/26b9361b-9c98-4f77-8ea9-226820820d39" />,
<img width="1720" height="954" alt="Image" src="https://github.com/user-attachments/assets/1fac9ec4-4ef1-49c6-a245-4e00bf092b63" />,
<img width="1714" height="950" alt="Image" src="https://github.com/user-attachments/assets/120f16c4-e108-4ba2-9077-4315cda43e2e" />]

### 📦 **Inventory Management**
- ✅ Real-time stock level monitoring
- ✅ Stock status indicators (In Stock, Low Stock, Out of Stock)
- ✅ Stock adjustment tracking
- ✅ Stock history logging
- ✅ Stock update capabilities

### 🛒 **Order Management**
- ✅ Complete order lifecycle management
- ✅ Order creation and processing
- ✅ Customer order history
- ✅ Order details tracking
- ✅ Staff order handling

### 🏷️ **Category Management**
- ✅ Category creation and management
- ✅ Category-based product organization
- ✅ Product-category relationships
- ✅ Category editing and deletion

### 👥 **Account Management**
- ✅ Role-based user accounts (Owner, Staff, Customer)
- ✅ User profile management
- ✅ Account creation and editing
- ✅ Secure authentication
- ✅ Account status management

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

### **Product Management**
- **View Products**: Access all products from the Products tab
- **Add Products**: Click "Add Product" and fill in the required information
- **Edit Products**: Click the edit icon in the actions column
- **Delete Products**: Use the delete icon with confirmation prompt

### **Stock Management**
- **View Stock**: Access the Stock Management tab
- **Update Stock**: Click on "Update Stock" to adjust quantities
- **Monitor Status**: Visual indicators show stock status (In Stock, Low Stock, Out of Stock)

### **Order Processing**
- **Create Orders**: Use the Create Order tab to start a new order
- **View History**: Check past orders in the Order History tab
- **Process Orders**: Staff can update order status and details

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
