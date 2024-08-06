using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;

public class SQLiteDbHelper : IDisposable
{
    private readonly string _connectionString;
    private SQLiteConnection _connection;
    public SQLiteConnection Connection => _connection;

    public SQLiteDbHelper(string databasePath = "example.db")
    {
        _connectionString = $"Data Source={databasePath};Version=3;";
        try
        {
            _connection = new SQLiteConnection(_connectionString);
            _connection.Open();
        }
        catch (SQLiteException ex)
        {
            Console.WriteLine($"SQLite connection error: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
            throw;
        }
    }

    public int ExecuteNonQuery(string query)
    {
        try
        {
            using (var command = new SQLiteCommand(query, _connection))
            {
                return command.ExecuteNonQuery();
            }
        }
        catch (SQLiteException ex)
        {
            Console.WriteLine($"SQLite error: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
            throw;
        }
    }

    public DataSet ExecuteDataSet(string query)
    {
        try
        {
            using (var command = new SQLiteCommand(query, _connection))
            using (var adapter = new SQLiteDataAdapter(command))
            {
                var dataSet = new DataSet();
                adapter.Fill(dataSet);
                return dataSet;
            }
        }
        catch (SQLiteException ex)
        {
            Console.WriteLine($"SQLite error: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
            throw;
        }
    }

    public DataTable ExecuteDataTable(string query)
    {
        try
        {
            using (var command = new SQLiteCommand(query, _connection))
            using (var adapter = new SQLiteDataAdapter(command))
            {
                var dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }
        catch (SQLiteException ex)
        {
            Console.WriteLine($"SQLite error: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
            throw;
        }
    }

    public List<T> ExecuteList<T>(string query) where T : new()
    {
        var result = new List<T>();

        try
        {
            using (var command = new SQLiteCommand(query, _connection))
            using (var reader = command.ExecuteReader())
            {
                var properties = typeof(T).GetProperties();
                var columnNames = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();

                while (reader.Read())
                {
                    var item = new T();
                    foreach (var property in properties)
                    {
                        if (columnNames.Contains(property.Name) && !Equals(reader[property.Name], DBNull.Value))
                        {
                            property.SetValue(item, Convert.ChangeType(reader[property.Name], property.PropertyType), null);
                        }
                    }
                    result.Add(item);
                }
            }
        }
        catch (SQLiteException ex)
        {
            Console.WriteLine($"SQLite error: {ex.Message}");
            throw;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
            throw;
        }

        return result;
    }

    public void Dispose()
    {
        if (_connection != null)
        {
            try
            {
                _connection.Close();
                _connection.Dispose();
            }
            catch (SQLiteException ex)
            {
                Console.WriteLine($"SQLite dispose error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected dispose error: {ex.Message}");
            }
            finally
            {
                _connection = null;
            }
        }
    }
}
