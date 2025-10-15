# Transit Tracker  
*A C# WinForms application for managing trips and passengers.*

---

## Overview
**Transit Tracker** is a desktop application built with **C# and Windows Forms** to manage basic transportation data.  
Users can create new trips, register passengers, assign seat numbers, and display all data in a structured table.  
It’s designed as a simple, educational “transport management” system built entirely with WinForms controls.

---

## How It Works

### 1. Initialization
- A list named `yolcular` is created to store all passenger and trip information.  
- The trip list (`listBox1`) starts with `"*"` as a default view item.  
- The seat dropdown (`comboBox1`) is filled with numbers **1–42**.

### 2. Add Trip
When the user enters trip details and clicks **Add Trip**:

Trip No - Departure City - Arrival City - Departure Time
- The trip is added to `listBox1`.  
- It’s also stored in the `yolcular` list.  
- Input boxes are cleared automatically.

### 3. Add Passenger
When the user enters passenger info and clicks **Add Passenger**:
Name - Phone - Gender - Trip No - Departure - Arrival - Time - Seat No: X
- The data is added to `yolcular`.  
- All fields (text boxes, checkboxes, combo box) are reset.

### 4. View All Passengers
When `"*"` is selected in `listBox1`:
- The table (`dataGridView1`) clears old data.
- Eight columns appear:
  1. Passenger Name  
  2. Phone Number  
  3. Gender  
  4. Trip Number  
  5. Departure City  
  6. Arrival City  
  7. Departure Time  
  8. Seat Number  
- All passengers stored in `yolcular` are displayed in the table.

---

## Key Features
Add new trips  
Register passengers with seat numbers  
Automatic DataGridView update  
In-memory data storage (no database)  
Simple and easy-to-understand WinForms interface  

---

## Technologies Used
- **Language:** C# (.NET Framework)  
- **Framework:** Windows Forms (WinForms)  
- **Core Components:** `DataGridView`, `ListBox`, `ComboBox`, `CheckBox`, `TextBox`  
- **Paradigm:** Object-Oriented Programming (OOP)

---

## Run the Application

### On Windows
1. Install [Visual Studio](https://visualstudio.microsoft.com/).  
2. Open `TransitTracker.sln`.  
3. Press **Run ▶️**.  
4. The main form window will appear automatically.

### On macOS
WinForms requires Windows APIs, so it doesn’t run natively on macOS.  
You can:
- Use a Windows virtual machine (Parallels, VMware, or UTM), or  
- Rebuild it with **.NET MAUI** or **Avalonia** for cross-platform support.

---

## Future Improvements
- Use typed models (`Passenger`, `Trip`) instead of `List<string>`.  
- Add persistent storage (SQLite or JSON).  
- Improve gender selection with radio buttons.  
- Add editing and search functionality.  
- Implement a simple login system for multiple users.

---

## Developer
**Haticenur Yalman**  
🎓 Bursa Technical University — Computer Engineering  



