using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using QLThanhToanTienDayLai.Attributes;
using QLThanhToanTienDayLai.ADO.Models;

namespace QLThanhToanTienDayLai.ADO.Controllers
{
    internal class Controller<EntityT> where EntityT : Model
    {
        public Database Db { get; set; }

        Type typeParameterType;

        public Controller()
        {
            Db = Database.GetDatabase();
            typeParameterType = typeof(EntityT);
        }

        public void Add(EntityT entity)
        {
            var tuple = GetProperiesName();

            var properties = tuple.Item1;
            var propertiesWithPreFix = tuple.Item2;

            var sql = string.Format(
                @"
                    INSERT INTO {0}
                    ({1})
                    VALUES({2})",
                entity.GetType().Name,
                string.Join(",", properties.ToArray()),
                string.Join(",", propertiesWithPreFix.ToArray())
                );
            SqlCommand command;
            FillParameters(sql, entity, out command);
            Db.FromSqlCommand(command, (err, reader) => { });
        }

        public void Update(EntityT entity)
        {
            var tuple = GetProperiesName();

            var properties = tuple.Item1;
            var propertiesWithPreFix = tuple.Item2;

            var sql = string.Format(
                @"
                    UPDATE {0} 
                    SET 
                    {1}
                    WHERE (({2}))
                ",
                entity.GetType().Name,
                string.Join(",", properties
                .ToArray()
                .Skip(1)
                .Select(property => string.Format("{0} = {1}", property, '@' + property))),
                string.Format("{0} = {1}", properties[0], propertiesWithPreFix[0])
                );
            SqlCommand command;
            FillParameters(sql, entity, out command);
            Db.FromSqlCommand(command, (err, reader) => { });
        }

        public void Delete(EntityT entity)
        {
            var tuple = GetProperiesName();

            var properties = tuple.Item1;
            var propertiesWithPreFix = tuple.Item2;

            var sql = string.Format(
                @"
                    DELETE FROM {0}
                    WHERE {1}
                ",
                entity.GetType().Name,
                string.Format("{0} = {1}", properties[0], propertiesWithPreFix[0])
                );
            SqlCommand command;
            FillParameters(sql, entity, out command);
            Db.FromSqlCommand(command, (err, reader) => { });
        }

        public DataTable GetDataTable()
        {
            DataTable dataTable = new DataTable();
            var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = string.Format("SELECT * FROM {0}", typeParameterType.Name);
            var adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dataTable);
            return dataTable;
        }

        private void FillParameters(string sql, EntityT entity, out SqlCommand command)
        {
            var tuple = GetProperiesName();
            var properties = typeParameterType
                .GetProperties()
                .ToList();
            var propertiesWithPreFix = tuple.Item2;
            command = Db.Connection.CreateCommand();
            command.CommandText = sql;

            for (var i = 0; i < properties.Count; i++)
            {
                var propertyType = SqlDbType.NVarChar;

                var attributeList = properties[i].GetType().GetCustomAttributes(typeof(ColumnAttribute), false);

                if (attributeList.Any())
                {
                    var attribute = (ColumnAttribute)attributeList.First();
                    propertyType = attribute.DataType;
                }

                command
                    .Parameters
                    .Add
                    (propertiesWithPreFix[i], propertyType).Value = properties[i].GetValue(entity);
            }
        }

        private Tuple<string[], string[]> GetProperiesName()
        {
            var properties = typeParameterType
                .GetProperties()
                .Select(property => property.Name);
            var propertiesWithPreFix = properties
                .Select(property => '@' + property);
            return Tuple.Create(properties.ToArray(), propertiesWithPreFix.ToArray());
        }

    }
}
