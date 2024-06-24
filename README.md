# Invoice Management

## Overview

 invoice system that allows creating invoices, paying invoices, and processing overdue invoices.

## Prerequisites

- .NET 6 SDK or higher
- Visual Studio Code or Visual Studio
- Docker (optional, if using containers)

## Installation

1. Clone the repository:
git clone https://github.com/shettyAakshatha/InvoiceManagement.git

2. Install dependencies:

3. Configure app settings:
set ASPNETCORE_ENVIRONMENT value in lauchsettings.json ( Development / Production)

4.Setp the data folder / Log Folder
 update path for the MasterdataPath in appsettings.{env}.json file ( by default ../Data/MasterData/Master.json)
 update the value of invoiceId in Data/MasterData/Master.json to have the initial value of the InvoiceId.
 update the Log file path in appsettings.env.json

5. Build and run the API:
  dotnet build [solutionName] (dotnet build InvoiceManagement.sln)
  dotnet run [solutionName] 
6. ## API Usage

### Endpoints

- `GET /invoices`: Retrieves a list of invoices
- `GET /invoices/id`: Retrieves a invoices by id
- `POST /invoices `: Creates a new invoice.
- `POST /invoices/id/payments : update payment for the requested id
- `POST /invoices/process-overdue : processes all the overdue invoices and creates an new invoice with due_date as 30 days from current date.
