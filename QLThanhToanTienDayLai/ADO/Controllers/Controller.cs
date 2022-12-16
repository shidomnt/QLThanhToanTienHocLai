using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace QLThanhToanTienDayLai.ADO.Controllers
{
    internal class Controller<EntityT>
    {
        public Database Db { get; set; }

        private EntityT Entity { get; set; }

        public Controller(EntityT entity)
        {
            Db = Database.GetDatabase();
            Entity = entity;
        }

        public Tuple<string[], string[]> GetProperiesName()
        {
            var properties = Entity
                .GetType()
                .GetProperties()
                .Select(property => property.Name);
            var propertiesWithPreFix = properties
                .Select(property => '@' + property);
            return Tuple.Create(properties.ToArray(), propertiesWithPreFix.ToArray());
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
            FillParameters(sql, entity, out SqlCommand command);
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
            FillParameters(sql, entity, out SqlCommand command);
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
            FillParameters(sql, entity, out SqlCommand command);
            Db.FromSqlCommand(command, (err, reader) => { });
        }

        private void FillParameters(string sql, EntityT entity, out SqlCommand command)
        {
            var tuple = GetProperiesName();
            var properties = entity
                .GetType()
                .GetProperties()
                .ToList();
            var propertiesWithPreFix = tuple.Item2;
            command = Db.Connection.CreateCommand();
            command.CommandText = sql;

            for (var i = 0; i < properties.Count; i++)
            {
                System.Data.SqlDbType propertyType;

                switch (properties[i].GetType().Name)
                {
                    case "int":
                        propertyType = System.Data.SqlDbType.Int;
                        break;
                    case "double":
                        propertyType = System.Data.SqlDbType.Float;
                        break;
                    case "date":
                        propertyType = System.Data.SqlDbType.Date;
                        break;
                    case "string":
                    default:
                        propertyType = System.Data.SqlDbType.NVarChar;
                        break;
                }

                command
                    .Parameters
                    .Add
                    (propertiesWithPreFix[i], propertyType).Value = properties[i].GetValue(entity);
            }
        }

        public DataTable GetDataTable()
        {
            DataTable dataTable = new DataTable();
            var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = string.Format("SELECT * FROM {0}", Entity.GetType().Name);
            var adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dataTable);
            return dataTable;
        }
    }
}
