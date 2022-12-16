using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLThanhToanTienDayLai.ADO.Models;
using System.Data;
using System.Data.SqlClient;

namespace QLThanhToanTienDayLai.ADO.Controllers
{
    internal class CoSoController
    {
        public Database Db { get; set; }

        public CoSoController()
        {
            Db = Database.GetDatabase();
        }

        public void Add(CoSo coSo) {
            var sql = @"
                    INSERT INTO CoSo
                    (Ma, Ten, DiaChi, LienHe, GhiChu)
                    VALUES(@Ma,@Ten,@DiaChi,@LienHe,@GhiChu)";
            SqlCommand command;
            FillParameters(sql, coSo, out command);
            Db.FromSqlCommand(command, (err, reader) => { });
        }

        public void Update(CoSo coSo) {
            var sql = @"
                UPDATE [dbo].[CoSo] 
                SET [Ten] = @Ten, 
                [DiaChi] = @DiaChi, 
                [LienHe] = @LienHe, 
                [GhiChu] = @GhiChu 
                WHERE (([Ma] = @Ma))
            ";
            SqlCommand command;
            FillParameters(sql, coSo, out command);
            Db.FromSqlCommand(command, (err, reader) => { });
        }

        public void Delete(CoSo coSo)
        {
            var sql = @"
                DELETE FROM CoSo
                WHERE Ma = @Ma
            ";
            SqlCommand command;
            FillParameters(sql, coSo, out command);
            Db.FromSqlCommand(command, (err, reader) => { });
        }

        private void FillParameters(string sql, CoSo coSo, out SqlCommand command)
        {
            command = Db.Connection.CreateCommand();
            command.CommandText = sql;
            command.Parameters.Add("@Ma", System.Data.SqlDbType.NVarChar).Value = coSo.Ma;
            command.Parameters.Add("@Ten", System.Data.SqlDbType.NVarChar).Value = coSo.Ten;
            command.Parameters.Add("@DiaChi", System.Data.SqlDbType.NVarChar).Value = coSo.DiaChi;
            command.Parameters.Add("@LienHe", System.Data.SqlDbType.NVarChar).Value = coSo.LienHe;
            command.Parameters.Add("@GhiChu", System.Data.SqlDbType.NVarChar).Value = coSo.GhiChu;
        }

        public DataTable GetDataTable()
        {
            DataTable dataTable = new DataTable();
            var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM CoSo";
            var adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dataTable);
            return dataTable;
        }
    }
}
