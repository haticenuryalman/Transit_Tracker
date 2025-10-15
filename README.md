# Bus Trip Management System

C# **WinForms** application for managing bus trips and passengers.

---

## Overview

**Bus Trip Management System** is a desktop application built with **C# and Windows Forms** that allows users to manage transportation data such as trips, passengers, and seat assignments.  
All information is stored in a **SQL Server database** using direct SQL commands via `System.Data.SqlClient`.

This project was developed as a **final exam project**, demonstrating database integration, user interface management, and CRUD (Create, Read, Update, Delete) operations in WinForms.

---

## Main Features

### Trip Management
- Create new trips with **trip number, departure city, arrival city, and departure time**  
- Prevents duplicate trip numbers by checking the existing database records  
- Automatically displays all trips in the **ListBox** after creation  

### Passenger Management
- Add passengers to an existing trip  
- Input fields include **name, phone number, gender, and seat number**  
- The system checks if the selected seat is already occupied for that trip  
- Each passenger is stored both in memory (`List<Sefer>`) and in the **Yolcular** table in SQL Server  

### Data Display
- Trips are listed in a **ListBox**  
- Selecting `"*"` displays **all passengers from all trips**  
- Selecting a specific trip shows only that trip’s passengers in a **DataGridView**  
- Data is automatically loaded from the database when the program starts  

### Updates
- Users can update a passenger’s **seat number** or **gender** directly from the **DataGridView**  
- All updates are instantly reflected both in the UI and in the database  

### Data Reset
- Includes a **“Delete All Trips and Passengers”** button that clears both the **Seferler** and **Yolcular** tables  
- After deletion, the local list and DataGridView are refreshed automatically  

---

## Database Structure

**Database Name:** `SeferYonetim`

### Table: `Seferler`

| Column | Type | Description |
|--------|------|-------------|
| SeferID | int (PK, IDENTITY) | Auto-incremented trip ID |
| SeferNumarasi | nvarchar(50) | Trip number |
| KalkisSehri | nvarchar(100) | Departure city |
| VarisSehri | nvarchar(100) | Arrival city |
| KalkisSaati | nvarchar(50) | Departure time |

### Table: `Yolcular`

| Column | Type | Description |
|--------|------|-------------|
| YolcuID | int (PK, IDENTITY) | Passenger ID |
| SeferID | int (FK) | Linked trip ID |
| YolcuAdi | nvarchar(100) | Passenger name |
| TelefonNumarasi | nvarchar(20) | Phone number |
| Cinsiyet | nvarchar(10) | Gender |
| KoltukNumarasi | nvarchar(10) | Seat number |

---

## Database Connection

The application connects to SQL Server using the following connection string:

```csharp
private const string ConnectionString =
    "Server=DESKTOP-0O4CJLI\\MSSQLSERVER01;Database=SeferYonetim;Trusted_Connection=True;";
```

> 📝 Replace `DESKTOP-0O4CJLI\\MSSQLSERVER01` with your own SQL Server name if running locally.

---

## How to Run the Project

1. Open **`SQLQuery1.sql`** in SQL Server and execute it to create the database  
2. Update the connection string in **`Form1.cs`** if necessary  
3. Open **`20360859011_finalsinavi.sln`** in Visual Studio  
4. Build and run the project (**F5**)  
5. You can now add trips, register passengers, and view all data through the interface  

---

## Technologies Used

- **C# (.NET Framework, WinForms)**  
- **SQL Server**  
- **System.Data.SqlClient**  
- **Object-Oriented Programming (OOP)**  
- **DataGridView** for dynamic data visualization  



