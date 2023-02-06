# Employee-and-Department-Management-System-using-Layered-Architecture
A Windows Form application for managing employees and departments, utilizing a 3-layer architecture and database connection.

The project is a 3-layer architecture for managing employees and departments. The project was developed as a test project to demonstrate the use of the layered architecture.

The project consists of the following layers:

Data Access Layer (DAL): This layer is responsible for interacting with the database and retrieving employee and department data. It contains the data access logic and provides a consistent interface for working with the data.

Business Logic Layer (BLL): This layer is the middle layer between the DAL and the UI layer. It contains the business logic and validation for the project and communicates with the DAL to perform the data access operations.

User Interface Layer (UI): This layer consists of a Windows Form that allows the user to choose a department from a drop-down list, and displays all employees in the selected department in a ListView. The user can select an employee, perform actions such as updating the salary, name, or birthdate, or deleting the employee. Additionally, the user can also insert a new employee.

Please note that this is a test project and is not intended for production use. However, the project serves as a demonstration of the implementation of the layered architecture.
