import json
import pandas as pd
import matplotlib.pyplot as plt
from collections import defaultdict

# Specify the path to your .ndjson file
ndjson_file = "C:\\Users\\OliverHannappel\\source\\repos\\Bank_Projekt\\Bank_Projekt\\bin\\Debug\\net7.0\\eventlogs.ndjson"

# Read data from the .ndjson file
with open(ndjson_file, 'r', encoding='utf-8') as file:
    log_data = [json.loads(line) for line in file]

# Extract relevant information from log data and create a list of dictionaries for fund transfers
parsed_data = []

# Initialize balances for all customers using a defaultdict
customer_balances = defaultdict(int)

for entry in log_data:
    properties = entry.get("Properties", {})
    customer_id = properties.get("0")
    balance_change = int(properties.get("3", 0))
    
    # Check if the log entry is related to fund transfer (Einzahlung or Auszahlung)
    if "Einzahlung" in entry["MessageTemplate"] or "Auszahlung" in entry["MessageTemplate"]:
        # Update the balance for the customer
        if "Auszahlung" in entry["MessageTemplate"]:
            balance_change = -balance_change  # Deduction (downward movement)
        
        customer_balances[customer_id] += balance_change
        
        parsed_data.append({"Timestamp": entry["Timestamp"], "CustomerID": customer_id, "Balance": customer_balances[customer_id]})

# Create a DataFrame from the parsed data
df = pd.DataFrame(parsed_data)

# Convert Timestamp to datetime format
df["Timestamp"] = pd.to_datetime(df["Timestamp"])

# Get the customer ID input from the user
user_input_customer_id = int(input())

# Filter the data for the specified customer
filtered_df = df[df["CustomerID"] == user_input_customer_id]

# Check if the specified customer exists in the data
if not filtered_df.empty:

    first_balance = filtered_df.iloc[0]["Balance"]
    filtered_df["Balance"] -= first_balance
    # Plot the changes in customer balance over time for the specified customer
    plt.figure(figsize=(10, 6))
    plt.plot(filtered_df["Timestamp"], filtered_df["Balance"], marker='o', linestyle='-')
    plt.title(f"Changes in Customer {user_input_customer_id} Balance Over Time (Fund Transfers)")
    plt.xlabel("Timestamp")
    plt.ylabel("Balance")
    plt.grid(True)
    plt.xticks(rotation=45)
    plt.tight_layout()
    plt.show()
else:
    print(f"Customer with ID {user_input_customer_id} not found in the data.")





























