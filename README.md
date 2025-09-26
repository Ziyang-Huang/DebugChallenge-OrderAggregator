# Order Aggregator Debug Challenge

## Scenario
An e‑commerce aggregation service consolidates multi‑currency orders and outputs key business metrics. The current version contains several compile-time and runtime (logic) defects. Your task is to fix these issues so the program produces correct metrics, and optionally perform modest engineering improvements.

## Target Metrics (Correct Business Logic)
1. **Total Revenue (USD)**: Net revenue (excluding tax) for all non‑cancelled and non‑refunded orders. Each line item amount = unit price * quantity. Convert to USD when currency != USD.
2. **Top 3 Customers by Revenue**: Same revenue rule, sorted by revenue desc, then CustomerId asc for ties.
3. **Avg Order Value Last 30 Days (USD)**: Average net (pre‑tax) order value for non‑cancelled, non‑refunded orders whose creation timestamp is within the past 30 days (inclusive).
4. **Tax By Region**: Sum of tax per region (line net amount * region tax rate).

## Defect Categories
This challenge intentionally preserves some compile errors and logic bugs. Examples:
- Compile Errors: interface signature mismatch, missing parameter, return type mismatch, undefined variable, spelling error.
- Logic Bugs: wrong date window direction, tax included in revenue, cancelled/refunded orders counted, premature rounding causing precision loss, reversed time filter comparison.

## Your Tasks
1. Resolve all compilation errors so the project builds successfully.  
2. Identify and fix logic defects so output matches the specified rules.  

## Sample Output Format (Values Illustrative Only)
```
Total Revenue (USD): 5331.90
Top Customers:
  C002 1429.45
  C003 1306.50
  C001 1211.96
Avg Order Value Last 30d (USD): 644.85
Tax By Region:
  US: 114.79
  EU: 592.41
  APAC: 57.40
```

## Data File
Located at `data/orders.json`, containing scenarios:
- Cancelled order (IsCancelled = true)
- Refunded order (IsRefunded = true)
- Multiple currencies (USD / EUR / CNY)
- Orders spanning the 30‑day boundary window

## Getting Started (Local)
```powershell
# In solution directory
pwsh
dotnet restore
dotnet build -c Debug
# Initial run (output will be incorrect before fixes)
dotnet run --project ./src/OrderAggregator/OrderAggregator.csproj
# or
dotnet run --project .\\src\\OrderAggregator\\OrderAggregator.csproj
```

## Acceptance Criteria
- Build succeeds: no compile errors.  
- Correct output: metrics align with business rules.  

Good luck debugging!
