using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLThanhToanTienDayLai.ADO.Controllers;
using QLThanhToanTienDayLai.ADO.Models;

namespace QLThanhToanTienDayLai
{
    public partial class FormKhoaHoc : Form
    {
        public FormKhoaHoc()
        {
            InitializeComponent();
            KhoaHocController = new KhoaHocController();
            KhoaController = new KhoaController();
        }

        private KhoaHoc _selectedKhoaHoc = null;

        private KhoaHocController KhoaHocController
        {
            get;
            set;
        }

        private KhoaController KhoaController
        {
            get;
            set;
        }

        public DataTable DataTableKhoaHoc { get; set; }

        public KhoaHoc SelectedKhoaHoc
        {
            get
            {
                return _selectedKhoaHoc;
            }
            set
            {
                _selectedKhoaHoc = value;
                LoadSelectedKhoaHocToInput();
            }
        }

        private void FormKhoaHoc_Load (object sender, EventArgs e)
        {
            var listKhoa = KhoaController.GetAll().ToList();
            ComboBox_MaKhoa.DataSource = listKhoa;
            ComboBox_MaKhoa.DisplayMember = "Ten";
            ComboBox_MaKhoa.ValueMember = "Ma";
            FeedDataGridView();
        }

        private void Btn_Them_Click(object sender, EventArgs e)
        {
            var KhoaHoc = MakeKhoaHocFromInput();
            KhoaHocController.Add(KhoaHoc);
            FeedDataGridView();
        }

        private void Btn_Sua_Click(object sender, EventArgs e)
        {
            var KhoaHoc = MakeKhoaHocFromInput();
            if (SelectedKhoaHoc == null)
            {
                MessageBox.Show("Chọn một KhoaHoc để sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            KhoaHocController.Update(KhoaHoc);
            FeedDataGridView();
        }

        private void Btn_Xoa_Click(object sender, EventArgs e)
        {
            if (SelectedKhoaHoc == null)
            {
                MessageBox.Show("Chọn một KhoaHoc để xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            KhoaHocController.Delete(SelectedKhoaHoc);
            FeedDataGridView();
        }


        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            var selectedRowCount = dataGridView1.SelectedRows.Count;
            if (selectedRowCount <= 0)
            {
                SelectedKhoaHoc = null;
                return;
            }
            
            var firstRowSelected = dataGridView1.SelectedRows[0].Cells;

            KhoaHoc KhoaHoc = new KhoaHoc()
            {
                Ma = firstRowSelected["Ma"].Value.ToString(),
                Ten = firstRowSelected["Ten"].Value.ToString(),
                MaKhoa = firstRowSelected["MaKhoa"].Value.ToString()
            };
            SelectedKhoaHoc = KhoaHoc;
        }

        public void LoadSelectedKhoaHocToInput()
        {
            if (SelectedKhoaHoc != null)
            {
                TextBox_Ma.Text = SelectedKhoaHoc.Ma;
                TextBox_Ten.Text = SelectedKhoaHoc.Ten;
                ComboBox_MaKhoa.SelectedValue = SelectedKhoaHoc.MaKhoa;
            }
        }

        public KhoaHoc MakeKhoaHocFromInput()
        {
            var KhoaHoc = new KhoaHoc()
            {
                Ma = TextBox_Ma.Text,
                Ten = TextBox_Ten.Text,
                MaKhoa = ComboBox_MaKhoa.SelectedValue.ToString()
            };
            return KhoaHoc;
        }

        private void FeedDataGridView()
        {
            DataTableKhoaHoc = KhoaHocController.GetDataTable();
            dataGridView1.DataSource = DataTableKhoaHoc;
        }
    }
}
