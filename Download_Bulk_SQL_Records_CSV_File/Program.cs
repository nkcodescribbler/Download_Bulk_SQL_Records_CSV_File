using System.Data.SqlClient;

string connectionString = "CONNECTION_STRING";
string query = "SQL_QUERY";
string csvFilePath = "Bulk_Restore.csv";

using (SqlConnection connection = new SqlConnection(connectionString))
{
    SqlCommand command = new SqlCommand(query, connection);
    connection.Open();
    SqlDataReader reader = command.ExecuteReader();
    bool _istrue = true;

    using (StreamWriter writer = new StreamWriter(csvFilePath))
    {
        // Write the header row
        for (int i = 0; i < reader.FieldCount; i++)
        {
            writer.Write(reader.GetName(i));
            if (i < reader.FieldCount - 1)
                writer.Write(",");
        }
        //writer.WriteLine();

        // Write the data rows
        while (reader.Read())
        {
            if (_istrue)
                writer.WriteLine();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                writer.Write(reader.GetValue(i).ToString());
                if (i < reader.FieldCount - 1)
                    writer.Write(",");
            }
            writer.WriteLine();
            _istrue = false;
        }
    }
}

Console.WriteLine("CSV file has been created successfully.");