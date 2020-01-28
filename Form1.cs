using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Invalidu
{
    public partial class Form1 : Form
    {
        private readonly List<DataGridViewCell> _selectedCells;


        public Form1()
        {
            InitializeComponent();
            _selectedCells = new List<DataGridViewCell>();
            table.ColumnCount = 20;
            table.RowCount = 20;

            table.ClearSelection();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = e.RowIndex;
            var column = e.ColumnIndex;

            var selectedCell = table[column, row];
            table.ClearSelection();

            if (!_selectedCells.Contains(selectedCell))
            {
                _selectedCells.Add(selectedCell);
            }
            else
            {
                _selectedCells.Remove(selectedCell);
            }

            _selectedCells.ForEach(c => c.Selected = true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string result = string.Empty;

            var rows = _selectedCells.Select(c => 19 - c.RowIndex).ToList();
            var columns = _selectedCells.Select(c => c.ColumnIndex).ToList();

            for (int i = 0; i < _selectedCells.Count; i++)
            {
                var tmp = $"{{ {columns[i]}, {rows[i]} }}";
                if (i < _selectedCells.Count - 1)
                {
                    tmp += ",\n";
                }

                result += tmp;
            }

            var file = File.CreateText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "file.txt"));

            file.WriteLine(result);
            file.Close();
        }

        private void table_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
    }
}
