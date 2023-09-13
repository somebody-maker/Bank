import json
import matplotlib.pyplot as plt
import pandas as pd

# Read the event log file
log_entries = []
with open('C:\\Users\\OliverHannappel\\source\\repos\\Bank_Projekt\\Bank_Projekt\\bin\\Debug\\net7.0\\eventlogs.ndjson', 'r', encoding='utf-8') as json_file:
    for line in json_file:
        log_entry = json.loads(line)
        log_entries.append(log_entry)

plt.xkcd()
# Create a DataFrame from the log entries
df = pd.DataFrame(log_entries)

# Filter log entries for adding customers
add_customer_df = df[df['MessageTemplate'].str.contains('Kunde', case=False)]

# Check if there are any log entries for customer additions
if not add_customer_df.empty:
    # Extract customer information from the 'Properties' field
    add_customer_df['CustomerInfo'] = add_customer_df['Properties'].apply(lambda x: x['0'])

    # Split the customer information into separate columns
    customer_info_df = add_customer_df['CustomerInfo'].str.split(' - ', expand=True)

    # Rename the columns for clarity
    customer_info_df.columns = ['ID', 'Vorname', 'Nachname', 'Alter', 'Adresse', 'Hausnummer', 'Konten', 'Kontostand']

    # Convert the 'Timestamp' column to datetime
    add_customer_df['Timestamp'] = pd.to_datetime(add_customer_df['Timestamp'])

    # Concatenate the customer information columns with the DataFrame
    add_customer_df = pd.concat([add_customer_df, customer_info_df], axis=1)

    max_objects = len(add_customer_df)

    # Plot a bar chart of the number of customers added over time
    plt.figure(figsize=(12, 6))
    add_customer_df['Timestamp'].dt.date.value_counts().sort_index().plot(kind='bar')
    plt.xlabel('Date')
    plt.ylabel('Number of Customers Added')
    plt.title('Customers Added Over Time')
    plt.xticks(rotation=45)
    plt.yticks(range(1, max_objects + 1))
    plt.tight_layout()
    plt.show()
else:
    print('No log entries for customer additions found.')
