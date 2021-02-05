/*   SmartCheck License
*******************************************************************************
*                                                                             *
*    Copyright (c) 2021 Luciano Gorosito <lucianogorosito@hotmail.com>        *
*                                                                             *
*    This file is part of SmartCheck                                          *
*                                                                             *
*    SmartCheck is free software: you can redistribute it and/or modify       *
*    it under the terms of the GNU General Public License as published by     *
*    the Free Software Foundation, either version 3 of the License, or        *
*    (at your option) any later version.                                      *
*                                                                             *
*    SmartCheck is distributed in the hope that it will be useful,            *
*    but WITHOUT ANY WARRANTY; without even the implied warranty of           *
*    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the            *
*    GNU General Public License for more details.                             *
*                                                                             *
*    You should have received a copy of the GNU General Public License        *
*    along with this program.  If not, see <https://www.gnu.org/licenses/>.   *
*                                                                             *
*******************************************************************************
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rvt = Autodesk.Revit.DB;

namespace SmartCheck
{
    public partial class frmCheck : Form
    {
        public Rvt.Document _doc;

        public frmCheck(Rvt.Document doc)
        {
            InitializeComponent();
            this.Text = Tools._title;
            this.lblProjectName.Text = doc.Title;
            _doc = doc;
            FillInitialData();
        }

        private void FillInitialData()
        {
            Main.lstChecks = Tools.InitialList();
            this.dgvChecks.DataBindings.Clear();
            this.dgvChecks.DataSource = Main.lstChecks;
            
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            Tools.RunTests();
            this.btnExportSelected.Enabled = true;
            this.btnExportAll.Enabled = true;
            this.btnOK.Enabled = false;
            this.Refresh();
        }

        private void DgvChecks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Check selected = (Check)this.dgvChecks.CurrentRow.DataBoundItem;
            this.txtWeb.DocumentText = selected.ToHtml();
        }

        private void frmCheck_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void dgvChecks_Paint(object sender, PaintEventArgs e)
        {
            foreach (DataGridViewRow row in this.dgvChecks.Rows)
            {
                if (row.Cells[0].Value.ToString() == "")
                {
                    row.DefaultCellStyle.Font = new Font(this.dgvChecks.DefaultCellStyle.Font.FontFamily, 10, FontStyle.Bold);
                }
            }
        }

        private void btnExportSelected_Click(object sender, EventArgs e)
        {
            Check selected = (Check)this.dgvChecks.CurrentRow.DataBoundItem;
            if (selected.order == "")
            {
                MessageBox.Show("Select a Check in the list", Tools._title,  
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                Tools.ExportCheck(selected);
            }
            
        }

        private void btnExportAll_Click(object sender, EventArgs e)
        {
            Tools.ExportAllChecks();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            (new frmAbout()).ShowDialog();
        }
    }
}
