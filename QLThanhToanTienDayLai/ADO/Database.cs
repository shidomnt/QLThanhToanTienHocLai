using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace QLThanhToanTienDayLai.ADO
{
    class Database
    {
        private static Database DatabaseInstance { get; set; }

        public SqlConnection Connection { get; set; }

        private Database()
        {
            var connectionString = ConfigurationManager
                .ConnectionStrings["SqlServer"]
                .ConnectionString;
            var connection = new SqlConnection(connectionString);
            Connection = connection;
        }

        public static Database GetDatabase() {
            if (DatabaseInstance == null) {
                DatabaseInstance = new Database();
            }
            return DatabaseInstance;
        }

        public void FromSqlCommand(SqlCommand cmd, Action<Exception, SqlDataReader> callback)
        {
            try
            {
                Connection.Open();

                var reader = cmd.ExecuteReader();

                callback(null, reader);

                MessageBox.Show("Dữ liệu được cập nhật vào Database", "Thông báo", MessageBoxButtons.OK);
            }
            catch (Exception ex) { 
                callback(ex, null);
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Connection.Close();
            }
        }

    }
}
