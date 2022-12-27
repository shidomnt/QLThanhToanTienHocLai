using QLThanhToanTienDayLai.ADO.Controllers;
using QLThanhToanTienDayLai.ADO.Models;
using System;
using System.Data;
using System.Windows.Forms;

namespace QLThanhToanTienDayLai
{
    public partial class FormKhoa : Form
    {
        public FormKhoa()
        {
            InitializeComponent();
            Controller = new KhoaController();
        }

        private Khoa _selectedKhoa = null;

        private KhoaController Controller { get; set; }

        public DataTable DataTableKhoa { get; set; }

        public Khoa SelectedKhoa
        {
            get
            {
                return _selectedKhoa;
            }
            set
            {
                _selectedKhoa = value;
                LoadSelectedKhoaToInput();
            }
        }

        private void Btn_Them_Click(object sender, EventArgs e)
        {
            var Khoa = MakeKhoaFromInput();
            Controller.Add(Khoa);
            FeedDataGridView();
        }

        private void Btn_Sua_Click(object sender, EventArgs e)
        {
            var Khoa = MakeKhoaFromInput();
            if (SelectedKhoa == null)
            {
                MessageBox.Show("Chọn một khoa để sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Controller.Update(Khoa);
            FeedDataGridView();
        }

        private void Btn_Xoa_Click(object sender, EventArgs e)
        {
            if (SelectedKhoa == null)
            {
                MessageBox.Show("Chọn một khoa để xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Controller.Delete(SelectedKhoa);
            FeedDataGridView();
        }

        private void FormKhoa_Load(object sender, EventArgs e)
        {
            FeedDataGridView();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            var selectedRowCount = dataGridView1.SelectedRows.Count;
            if (selectedRowCount <= 0)
            {
                SelectedKhoa = null;
                return;
            }

            var firstRowSelected = dataGridView1.SelectedRows[0].Cells;

            Khoa Khoa = new Khoa()
            {
                Ma = firstRowSelected["Ma"].Value.ToString(),
                Ten = firstRowSelected["Ten"].Value.ToString(),
                MaCoSo = firstRowSelected["MaCoSo"].Value.ToString(),
                LienHe = firstRowSelected["LienHe"].Value.ToString(),
                GhiChu = firstRowSelected["GhiChu"].Value.ToString(),
            };
            SelectedKhoa = Khoa;
        }

        public void LoadSelectedKhoaToInput()
        {
            if (SelectedKhoa != null)
            {
                TextBox_Ma.Text = SelectedKhoa.Ma;
                TextBox_Ten.Text = SelectedKhoa.Ten;
                TextBox_MaCoSo.Text = SelectedKhoa.MaCoSo;
                TextBox_LienHe.Text = SelectedKhoa.LienHe;
                TextBox_GhiChu.Text = SelectedKhoa.GhiChu;
            }
        }

        public Khoa MakeKhoaFromInput()
        {
            var Khoa = new Khoa()
            {
                Ma = TextBox_Ma.Text,
                Ten = TextBox_Ten.Text,
                MaCoSo = TextBox_MaCoSo.Text,
                LienHe = TextBox_LienHe.Text,
                GhiChu = TextBox_GhiChu.Text,
            };
            return Khoa;
        }

        private void FeedDataGridView()
        {
            DataTableKhoa = Controller.GetDataTable();
            dataGridView1.DataSource = DataTableKhoa;
        }

    }
}
