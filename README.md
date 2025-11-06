# List of Products

## Getting Started

### 1. Clone the Repository

git clone <https://github.com/Lithinas/list_of_products>
cd list_of_products

### 2. Trust development certificate (optional if not done already)

dotnet dev-certs https --trust

### 2. Restore Dependencies

dotnet restore

### 3. Build the Project

dotnet build

### 4. Run the Application

dotnet run

The application will start on:

- **HTTPS**: `https://localhost:7021`
- **HTTP**: `http://localhost:5000`

Navigate to `https://localhost:7021/Products` to view the product list.

## Running Tests

dotnet test
