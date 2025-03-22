using Microsoft.EntityFrameworkCore;
using SFinance.Data.DataBase;
using System.Data;
using System.Data.Common;

namespace SFinance.Data.Services
{
    public class HandbookServices : IHandbookServices
    {
        public readonly Context Context;

        public HandbookServices(Context context)
        {
            Context = context;
        }

        public List<HandbookEntity> GetHandbookEntities()
        {
	        return Context.Handbooks.OrderBy(o => o.NameHandbook).ToList();
        }

        public HandbookEntity GetHandbookById(int idHandbook)
        {
            HandbookEntity result = new HandbookEntity();

            var handbook = Context.Handbooks.Include(h => h.Fields).ThenInclude(f => f.TypeDataEntity);

            result = handbook.Where(h => h.Id == idHandbook).FirstOrDefault();

            return result;
        }

        public void UpdateHandbook(HandbookEntity model, out string message)
        {
	        if (model == null)
	        {
		        message = "Ошибка!";
                return;
	        }

	        var entity = Context.Handbooks.Where(w => w.Id == model.Id).FirstOrDefault();

	        if (entity != null)
	        {
		        entity.NameHandbook = model.NameHandbook;
		        entity.TableName = model.TableName;
		        entity.Request = model.Request;
		        entity.KeyField = model.KeyField;
		        entity.SelectionField = model.SelectionField;
		        entity.Width = model.Width;
		        entity.Height = model.Height;

		        try
		        {
			        Context.SaveChanges();
			        message = "Обновление выполнено успешно";
		        }
		        catch (Exception e)
		        {
			        message = e.Message;
		        }
	        }
	        else
	        {
				message = $"Справочника с id {model.Id} не существует";
	        }
        }

        public void AddHandbook(HandbookEntity model, out string message)
        {
	        if (model == null)
	        {
		        message = "Ошибка!";
		        return;
	        }

	        var entity = new HandbookEntity()
	        {
		        NameHandbook = model.NameHandbook,
		        TableName = model.TableName,
		        Request = model.Request,
		        KeyField = model.KeyField,
		        SelectionField = model.SelectionField,
		        Width = model.Width,
		        Height = model.Height
			};

			try
			{
				Context.Handbooks.Add(entity);

				Context.SaveChanges();

				message = "Добавление выполнено успешно";
			}
			catch (Exception e)
			{
				message = e.Message;
			}
		}

        public void DeleteHandbook(int idHandbook, out string message)
        {
	        var entity = Context.Handbooks.Where(w => w.Id == idHandbook).FirstOrDefault();

	        try
	        {
		        Context.Handbooks.Remove(entity);

		        Context.SaveChanges();

		        message = "Удаление выполнено успешно";
	        }
	        catch (Exception e)
	        {
		        message = e.Message;
	        }
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

		public List<FieldEntity> GetFieldsEntityByIdHandbook(int idHandbook)
		{
			return Context.Fields.Where(w => w.IdHandbook == idHandbook).ToList();
		}

		public void UpdateField(FieldEntity model, out string message)
		{
			if (model == null)
			{
				message = "Ошибка!";
				return;
			}

			var entity = Context.Fields.Where(w => w.IdField == model.IdField).FirstOrDefault();

			if (entity != null)
			{
				entity.IndexField = model.IndexField;
				entity.NameToQuery = model.NameToQuery;
				entity.NameVisible = model.NameVisible;
				entity.IdTypeData = model.IdTypeData;
				entity.RefHandbookToField = model.RefHandbookToField;
				entity.IsVisible = model.IsVisible;
				entity.IsEdit = model.IsEdit;
				entity.IsNull = model.IsNull;
				entity.IsCheckDuplicate = model.IsCheckDuplicate;

				try
				{
					Context.SaveChanges();
					message = "Обновление выполнено успешно";
				}
				catch (Exception e)
				{
					message = e.Message;
				}
			}
			else
			{
				message = $"Поле с id {model.IdField} не существует";
			}
		}

		public void AddField(FieldEntity model, out string message)
		{
			if (model == null)
			{
				message = "Ошибка!";
				return;
			}

			var entity = new FieldEntity()
			{
				IdHandbook = model.IdHandbook,
				IndexField = model.IndexField,
				NameToQuery = model.NameToQuery,
				NameVisible = model.NameVisible,
				IdTypeData = model.IdTypeData,
				RefHandbookToField = model.RefHandbookToField,
				IsVisible = model.IsVisible,
				IsEdit = model.IsEdit,
				IsNull = model.IsNull,
				IsCheckDuplicate = model.IsCheckDuplicate
			};

			try
			{
				Context.Fields.Add(entity);

				Context.SaveChanges();

				message = "Добавление выполнено успешно";
			}
			catch (Exception e)
			{
				message = e.Message;
			}
		}

		public void DeleteField(int idField, out string message)
		{
			var entity = Context.Fields.Where(w => w.IdField == idField).FirstOrDefault();

			try
			{
				Context.Fields.Remove(entity);

				Context.SaveChanges();

				message = "Удаление выполнено успешно";
			}
			catch (Exception e)
			{
				message = e.Message;
			}
		}

		public Dictionary<string, string> GetKeyValueDictionary(int idHandbook, int idSelectedValue)
		{
			Dictionary<string, string> result = new Dictionary<string, string>();

			var handbook = Context.Handbooks.Where(w => w.Id == idHandbook).FirstOrDefault();

			var valueTable = GetDataFromDirectoryQuery(handbook.Request);

			foreach (Dictionary<string, object> value in valueTable)
			{
				foreach (var objValue in value)
				{
					if (objValue.Key == handbook.KeyField
					    && objValue.Value.ToString() == idSelectedValue.ToString())
					{
						var selectKeyValue = value.ToDictionary(x => x.Key, k => k.Value.ToString());

						result = GetKeyValueToDictionary(selectKeyValue, handbook.KeyField, handbook.SelectionField);
					}
				}
			}

			return result;
		}

		private Dictionary<string, string> GetKeyValueToDictionary(Dictionary<string, string> selectKeyValue,
			string keyField, string selectionField)
		{
			Dictionary<string, string> result = new Dictionary<string, string>();

			foreach (var value in selectKeyValue)
			{
				if (value.Key == keyField)
				{
					result.Add("key", value.Value);
				}
				else if (value.Key == selectionField)
				{
					result.Add("value", value.Value);
				}
			}

			return result;
		}
	}
}
