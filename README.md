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

4.Setp the data folder
 update path for the MasterdataPath in appsettings.{env}.json file ( by default ../Data/MasterData/Master.json)
 update the value of invoiceId in Data/MasterData/Master.json to have the initial value of the InvoiceId.

5. Build and run the API:
  dotnet build [solutionName] (dotnet build InvoiceManagement.sln)
  dotnet run [solutionName] 
6. ## API Usage

### Endpoints

- `GET /invoices`: Retrieves a list of invoices
- `POST /invoices `: Creates a new invoice.
