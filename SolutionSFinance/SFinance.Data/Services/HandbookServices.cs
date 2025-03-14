using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SFinance.Data.DataBase;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;

namespace SFinance.Data.Services
{
    public class HandbookServices : IHandbookServices
    {
        public readonly Context Context;

        public HandbookServices(Context context)
        {
            Context = context;
        }

        public HandbookEntity GetHandbookById(int idHandbook)
        {
            HandbookEntity result = new HandbookEntity();

            var handbook = Context.Handbooks.Include(h => h.Fields).ThenInclude(f => f.TypeDataEntity);

            result = handbook.Where(h => h.Id == idHandbook).FirstOrDefault();

            return result;
        }

        public List<Dictionary<string, object>> GetDataFromDirectoryQuery(string handbookQuery)
        {
            var results = new List<Dictionary<string, object>>();

            var connection = Context.Database.GetDbConnection();

            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = handbookQuery;

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var row = new Dictionary<string, object>();
                        
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row[reader.GetName(i)] = reader.GetValue(i);
                        }

                        results.Add(row);
                    }
                }
            }

            connection.Close();

            return results;
        }

        public void InsertValueToTable(string nameTable, Dictionary<string, string> addedValues)
        {
            var connection = Context.Database.GetDbConnection();

            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = CreateQueryToInsert(nameTable, addedValues.Keys.ToList(), addedValues.Keys.Select(s => "@" + s).ToList());

                CreateParametrsToDBCommand(command, addedValues);

                command.ExecuteReader();
            }

            connection.Close();
        }

        private string CreateQueryToInsert(string nameTable, List<string> columsName, List<string> paramsQuery)
        {
            string query = string.Empty;

            query += "Insert into " + nameTable + " (" + String.Join(", ", columsName) + ") ";

            query += "Values (" + String.Join(", ", paramsQuery) + ")";

            return query;
        }

        public void UpdateValueToTable(string nameTable, string nameKeyField, string valueKey, Dictionary<string, string> addedValues)
        {
            var connection = Context.Database.GetDbConnection();

            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = CreateQueryToUpdate(nameTable, nameKeyField, valueKey, addedValues.Select(s => $"{s.Key} = @{s.Key}").ToList());

                CreateParametrsToDBCommand(command, addedValues);

                command.ExecuteReader();
            }

            connection.Close();
        }

        private string CreateQueryToUpdate(string nameTable, string nameKeyField, string valueKey, List<string> value)
        {
            string query = string.Empty;

            query += "Update  " + nameTable + " ";

            query += "set " + String.Join(", ", value) + " ";

            query += "where " + nameKeyField + " = " + valueKey;

            return query;
        }

        public void DeleteValueToTable(string nameTable, string nameKeyField, string valueKey)
        {
            var connection = Context.Database.GetDbConnection();

            connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = CreateQueryToDelete(nameTable, nameKeyField, valueKey);

                command.ExecuteReader();
            }

            connection.Close();
        }

        private string CreateQueryToDelete(string nameTable, string nameKeyField, string valueKey)
        {
            string query = string.Empty;

            query += "DELETE " + nameTable + " ";

            query += "where " + nameKeyField + " = " + valueKey;

            return query;
        }

        private void CreateParametrsToDBCommand(DbCommand command, Dictionary<string, string> addedValues)
        {
            foreach (var value in addedValues)
            {
                var parameter = command.CreateParameter();

                parameter.ParameterName = "@" + value.Key;

                parameter.Value = (object)addedValues[value.Key] ?? DBNull.Value;

                command.Parameters.Add(parameter);
            }
        }
    }
}
