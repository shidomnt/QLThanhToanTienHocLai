﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLThanhToanTienDayLai
{
    public partial class FormMDI : Form
    {
        public FormMDI()
        {
            InitializeComponent();
        }

        private void cơSởToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form_CoSo = new FormCoSo();
            form_CoSo.MdiParent = this;
            form_CoSo.Show();
        }

        private void khoaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form_CoSo = new FormKhoa();
            form_CoSo.MdiParent = this;
            form_CoSo.Show();
        }
    }
}
