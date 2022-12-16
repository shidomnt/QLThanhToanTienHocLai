using System;
using System.Data;
using System.Windows.Forms;
using QLThanhToanTienDayLai.ADO.Controllers;
using QLThanhToanTienDayLai.ADO.Models;

namespace QLThanhToanTienDayLai
{
    internal partial class FormCoSo : Form
    {
        private CoSo _selectedCoSo = null;

        private Controller<CoSo> Controller { get; set; }

        public DataTable DataTableCoSo { get; set; }

        public CoSo SelectedCoSo { 
            get
            {
                return _selectedCoSo;
            }
            set
            {
                _selectedCoSo = value;
                LoadSelectedCoSoToInput();
            }
        }

        public FormCoSo()
        {
            InitializeComponent();
            Controller = new Controller<CoSo>(new CoSo());
        }

        private void Btn_Them_Click(object sender, EventArgs e)
        {
            var coSo = MakeCoSoFromInput();
            Controller.Add(coSo);
            FeedDataGridView();
        }

        private void Btn_Sua_Click(object sender, EventArgs e)
        {
            var coSo = MakeCoSoFromInput();
            Controller.Update(coSo);
            FeedDataGridView();
        }

        private void Btn_Xoa_Click(object sender, EventArgs e)
        {
            if (SelectedCoSo == null)
            {
                MessageBox.Show("Chọn một cơ sở để xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Controller.Delete(SelectedCoSo);
            FeedDataGridView();
        }

        private void FormCoSo_Load(object sender, EventArgs e)
        {
            FeedDataGridView();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            var selectedRowCount = dataGridView1.SelectedRows.Count;
            if (selectedRowCount <= 0)
            {
                SelectedCoSo = null;
                return;
            }

            var firstRowSelected = dataGridView1.SelectedRows[0].Cells;

            CoSo coSo = new CoSo()
            {
                Ma = firstRowSelected["Ma"].Value.ToString(),
                Ten = firstRowSelected["Ten"].Value.ToString(),
                DiaChi = firstRowSelected["DiaChi"].Value.ToString(),
                LienHe = firstRowSelected["LienHe"].Value.ToString(),
                GhiChu = firstRowSelected["GhiChu"].Value.ToString(),
            };
            SelectedCoSo = coSo;
        }

        public void LoadSelectedCoSoToInput()
        {
            if (SelectedCoSo != null)
            {
                TextBox_Ma.Text = SelectedCoSo.Ma;
                TextBox_Ten.Text = SelectedCoSo.Ten;
                TextBox_DiaChi.Text = SelectedCoSo.DiaChi;
                TextBox_LienHe.Text = SelectedCoSo.LienHe;
                TextBox_GhiChu.Text = SelectedCoSo.GhiChu;
            }
        }

        public CoSo MakeCoSoFromInput()
        {
            var coSo = new CoSo()
            {
                Ma = TextBox_Ma.Text,
                Ten = TextBox_Ten.Text,
                DiaChi = TextBox_DiaChi.Text,
                LienHe = TextBox_LienHe.Text,
                GhiChu = TextBox_GhiChu.Text,
            };
            return coSo;
        }

        private void FeedDataGridView()
        {
            DataTableCoSo = Controller.GetDataTable();
            dataGridView1.DataSource = DataTableCoSo;
        }

    }
}
