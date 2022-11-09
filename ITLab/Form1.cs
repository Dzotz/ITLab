using ITLab.Classes.Database;
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
using System.Xml.Linq;


namespace ITLab
{
    public partial class DBForm : Form
    {
        public DBForm()
        {
            InitializeComponent();
            DatabaseManager.Instance.CreateDatabase("DB");
            Text = "DB";
        }

        private void createDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string value = "";
            if (InputBox("Dialog Box", "Database Name", ref value) == DialogResult.OK)
            {
                Form.ActiveForm.Text = value;
                try
                {
                    DatabaseManager.Instance.CreateDatabase(value);
                    tableTab.Controls.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();
            form.Text = title;
            label.Text = promptText;
            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;
            label.SetBounds(36, 36, 372, 13);
            textBox.SetBounds(36, 86, 700, 20);
            buttonOk.SetBounds(228, 160, 160, 60);
            buttonCancel.SetBounds(400, 160, 160, 60);
            label.AutoSize = true;
            form.ClientSize = new Size(796, 307);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;
            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        private void createTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string value = "";
            if (InputBox("Dialog Box", "Table Name", ref value) == DialogResult.OK)
            {
                try
                {
                    DatabaseManager.Instance.AddTable(value);
                    TabPage tabPage = new TabPage();
                    DataGridView dataGridView = new DataGridView();
                    tabPage.Controls.Add(new DataGridView());
                    tabPage.Controls[0].Dock = DockStyle.Fill;
                    dataGridView = (DataGridView)tabPage.Controls[0];
                    dataGridView.ReadOnly = true;
                    dataGridView.AllowUserToAddRows = false;
                    dataGridView.AllowUserToOrderColumns = false;
                    tabPage.Text = value;
                    tableTab.Controls.Add(tabPage);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void deleteTableToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (tableTab.Controls.Count == 0)
            {
                MessageBox.Show("No Tables", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    DatabaseManager.Instance.DeleteTable(tableTab.SelectedTab.Text);
                    tableTab.Controls.Remove(tableTab.SelectedTab);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void addColumnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string value = "", type = "";
            if (tableTab.Controls.Count == 0)
            {
                MessageBox.Show("No Tables", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (ColumnBox("Dialog Box", "Column Name", ref value, ref type) == DialogResult.OK)
            {
                try
                {
                    DatabaseManager.Instance.AddColumn(tableTab.SelectedTab.Text, value, type);
                    DataGridView view = (DataGridView)tableTab.SelectedTab.Controls[0];
                    view.Columns.Add("", value);
                    for (int i = 0; i < view.Rows.Count; i++)
                    {
                        view.Rows[i].Cells[view.Columns.Count - 1].Value = DatabaseManager.Instance.Database.GetTable(tableTab.SelectedTab.Text).Rows[i].Values[view.Columns.Count - 1];
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public static DialogResult ColumnBox(string title, string promptText, ref string value, ref string type)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();
            ComboBox colType = new ComboBox();
            List<string> items = new List<string>();
            items.Add("String");
            items.Add("Integer");
            items.Add("Char");
            items.Add("Real");
            items.Add("Email");
            colType.DataSource = items;
            form.Text = title;
            label.Text = promptText;
            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;
            label.SetBounds(36, 36, 372, 13);
            textBox.SetBounds(36, 86, 700, 20);
            colType.SetBounds(36, 146, 700, 20);
            buttonOk.SetBounds(228, 180, 160, 60);
            buttonCancel.SetBounds(400, 180, 160, 60);
            label.AutoSize = true;
            form.ClientSize = new Size(796, 307);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel, colType });
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;
            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            type = colType.Text;
            return dialogResult;
        }

        private void deleteColumnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tableTab.Controls.Count == 0)
            {
                MessageBox.Show("No Tables", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    DataGridView dataGridView = (DataGridView)tableTab.SelectedTab.Controls[0];
                    int index = dataGridView.SelectedCells[0].ColumnIndex;
                    DatabaseManager.Instance.DeleteColumn(tableTab.SelectedTab.Text, index);
                    dataGridView.Columns.RemoveAt(index);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void addSetColumnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name = "", values = "";
            if (tableTab.Controls.Count == 0)
            {
                MessageBox.Show("No Tables", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (SetColumnBox("Dialog Box", "Column Name", ref name, ref values) == DialogResult.OK)
            {
                try
                {
                    DatabaseManager.Instance.AddSetColumn(tableTab.SelectedTab.Text, name, values);
                    DataGridView view = (DataGridView)tableTab.SelectedTab.Controls[0];
                    view.Columns.Add("", name);
                    for (int i = 0; i < view.Rows.Count; i++)
                    {
                        view.Rows[i].Cells[view.Columns.Count - 1].Value = DatabaseManager.Instance.Database.GetTable(tableTab.SelectedTab.Text).Rows[i].Values[view.Columns.Count - 1];
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public static DialogResult SetColumnBox(string title, string promptText, ref string name, ref string rvalues)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();
            TextBox values = new TextBox();

            form.Text = title;
            label.Text = promptText;
            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(36, 36, 372, 13);
            textBox.SetBounds(36, 86, 700, 20);
            values.SetBounds(36, 146, 700, 20);
            buttonOk.SetBounds(228, 180, 160, 60);
            buttonCancel.SetBounds(400, 180, 160, 60);
            label.AutoSize = true;
            form.ClientSize = new Size(796, 307);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel, values });
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;
            DialogResult dialogResult = form.ShowDialog();
            name = textBox.Text;
            rvalues = values.Text;
            return dialogResult;
        }

        private void addRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string values = "";
            if (tableTab.Controls.Count == 0)
            {
                MessageBox.Show("No Tables", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DataGridView view = (DataGridView)tableTab.SelectedTab.Controls[0];
                if (view.Columns.Count == 0)
                {
                    MessageBox.Show("No Columns", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (InputBox("Dialog Box", "Row Name", ref values) == DialogResult.OK)
                {
                    try
                    {
                        DatabaseManager.Instance.AddRow(tableTab.SelectedTab.Text, values);
                        view.Rows.Add(1);
                        for (int i = 0; i < view.ColumnCount; i++)
                        {
                            view.Rows[view.RowCount - 1].Cells[i].Value = DatabaseManager.Instance.Database.GetTable(tableTab.SelectedTab.Text).Rows[view.RowCount - 1].Values[i];
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
        }

        private void deleteRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tableTab.Controls.Count == 0)
            {
                MessageBox.Show("No Tables", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DataGridView view = (DataGridView)tableTab.SelectedTab.Controls[0];
                if (view.Rows.Count == 0)
                {
                    MessageBox.Show("No Rows", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    try
                    {
                        DataGridView dataGridView = (DataGridView)tableTab.SelectedTab.Controls[0];
                        int index = dataGridView.SelectedCells[0].RowIndex;
                        DatabaseManager.Instance.DeleteRow(tableTab.SelectedTab.Text, index);
                        dataGridView.Rows.RemoveAt(index);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private void editRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string values = "";
            if (tableTab.Controls.Count == 0)
            {
                MessageBox.Show("No Tables", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DataGridView view = (DataGridView)tableTab.SelectedTab.Controls[0];
                if (view.Columns.Count == 0)
                {
                    MessageBox.Show("No Columns", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (InputBox("Dialog Box", "New Values", ref values) == DialogResult.OK)
                {
                    int index = view.SelectedCells[0].RowIndex;
                    try
                    {
                        DatabaseManager.Instance.EditRow(tableTab.SelectedTab.Text, values, index);
                        for (int i = 0; i < view.ColumnCount; i++)
                        {
                            view.Rows[index].Cells[i].Value = DatabaseManager.Instance.Database.GetTable(tableTab.SelectedTab.Text).Rows[index].Values[i];
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
        }

        private void editCellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string values = "";
            if (tableTab.Controls.Count == 0)
            {
                MessageBox.Show("No Tables", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DataGridView view = (DataGridView)tableTab.SelectedTab.Controls[0];
                if (view.Columns.Count == 0)
                {
                    MessageBox.Show("No Columns", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (view.Rows.Count == 0)
                {
                    MessageBox.Show("No Rows", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (InputBox("Dialog Box", "New Value", ref values) == DialogResult.OK)
                {
                    try
                    {
                        int rInd = view.SelectedCells[0].RowIndex;
                        int cInd = view.SelectedCells[0].ColumnIndex;
                        DatabaseManager.Instance.EditCell(tableTab.SelectedTab.Text, cInd, rInd, values);
                        view.Rows[rInd].Cells[cInd].Value = values;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void MultiplyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string name1 = "", name2 = "";
            if (tableTab.Controls.Count == 0)
            {
                MessageBox.Show("No Tables", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (SetColumnBox("Multiply", "Enter Tables", ref name1, ref name2) == DialogResult.OK)
            {
                try
                {
                    Table res = DatabaseManager.Instance.Multiply(name1, name2);

                    Form form = new Form();

                    DataGridView dataGridView = new DataGridView();
                    form.Controls.Add(new DataGridView());
                    form.Controls[0].Dock = DockStyle.Fill;
                    dataGridView = (DataGridView)form.Controls[0];
                    dataGridView.ReadOnly = true;
                    dataGridView.AllowUserToAddRows = false;
                    dataGridView.AllowUserToOrderColumns = false;
                    form.Text = "Multiply " + name1 + " " + name2;
                    for (int j = 0; j < res.Columns.Count; j++)
                    {
                        dataGridView.Columns.Add("", res.Columns[j].Name);

                    }
                    for (int j = 0; j < res.Rows.Count; j++)
                    {
                        dataGridView.Rows.Add(1);
                        for (int i = 0; i < dataGridView.ColumnCount; i++)
                        {
                            dataGridView.Rows[j].Cells[i].Value = res.Rows[j].Values[i];
                        }
                    }
                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    DatabaseManager.Instance.DatabaseToFile(myStream);
                    myStream.Close();
                }
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream;

            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = openFileDialog1.OpenFile()) != null)
                {
                    DatabaseManager.Instance.FileToDatabase(myStream);
                    myStream.Flush();
                    myStream.Close();
                }
            }
            foreach(Table table in DatabaseManager.Instance.Database.Tables)
            {
                TabPage tabPage = new TabPage();
                DataGridView dataGridView = new DataGridView();
                tabPage.Controls.Add(new DataGridView());
                tabPage.Controls[0].Dock = DockStyle.Fill;
                dataGridView = (DataGridView)tabPage.Controls[0];
                dataGridView.ReadOnly = true;
                dataGridView.AllowUserToAddRows = false;
                dataGridView.AllowUserToOrderColumns = false;
                tabPage.Text = table.Name;
                tableTab.Controls.Add(tabPage);
                foreach(Column column in table.Columns)
                {
                    DataGridView view = (DataGridView)tableTab.SelectedTab.Controls[0];
                    view.Columns.Add("", column.Name);
                    
                }
                foreach(Row row in table.Rows)
                {
                    DataGridView view = (DataGridView)tableTab.SelectedTab.Controls[0];
                    view.Rows.Add(1);
                    for (int i = 0; i < view.ColumnCount; i++)
                    {
                        view.Rows[view.RowCount - 1].Cells[i].Value = DatabaseManager.Instance.Database.GetTable(tableTab.SelectedTab.Text).Rows[view.RowCount - 1].Values[i];
                    }
                }
            }
        }
    }
}
