using ITLab.Classes.Database.Columns;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace ITLab.Classes.Database
{
    public class DatabaseManager
    {
        private static DatabaseManager _instance;

        public static DatabaseManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DatabaseManager();
                }

                return _instance;
            }
        }

        private DatabaseManager() { }

        public Database Database { get; set; }

        public void CreateDatabase(string name)
        {
            if(Database != null && name == Database.Name)
            {
                throw new Exception("You work with this DB right now");
            }
            Database = new Database(name);
        }
        
        public void AddTable(string name)
        {
            if (Database == null)
            {
                throw new Exception("No Database");
            }
            if (Database.GetTable(name) != null)
            {
                throw new Exception("Table exists");
            }
            Database.Tables.Add(new Table(name));
        }

        public void DeleteTable(string tabName)
        {
            if (Database == null)
            {
                throw new Exception("No Database");
            }
            if (Database.GetTable(tabName) == null)
            {
                throw new Exception("No Table Found");
            }
            Database.Tables.Remove(Database.GetTable(tabName));   
        }

        public void AddColumn(string tableName, string colName, string type)
        {
            if (Database.GetTable(tableName) == null)
            {
                throw new Exception("No Table Found");
            }
            else
            {
                if (Database.GetTable(tableName).Columns.Find(x => x.Name == colName)!=null)
                {
                    throw new Exception("Column exists");
                }
                switch (type)
                {
                    case "String": case "STRING":
                        Database.GetTable(tableName).Columns.Add(new StrCol(colName));
                        foreach (Row row in Database.GetTable(tableName).Rows)
                        {
                            row.Values.Add("");
                        }
                        
                        break;
                    case "Integer": case "INT":
                        Database.GetTable(tableName).Columns.Add(new IntCol(colName));
                        foreach (Row row in Database.GetTable(tableName).Rows)
                        {
                            row.Values.Add("0");
                        }
                       
                        break;
                    case "Char":
                    case "CHAR":
                        Database.GetTable(tableName).Columns.Add(new CharCol(colName));
                        foreach (Row row in Database.GetTable(tableName).Rows)
                        {
                            row.Values.Add("");
                        }
                        if (Database.GetTable(tableName).Rows.Count == 0)
                        {
                            Row row = new Row();
                            row.Values.Add("");
                            Database.GetTable(tableName).Rows.Add(row);
                        }
                        break;
                    case "Real":
                    case "REAL":
                        Database.GetTable(tableName).Columns.Add(new RealCol(colName));
                        foreach (Row row in Database.GetTable(tableName).Rows)
                        {
                            row.Values.Add("0");
                        }
                        
                        break;
                    case "Email":
                    case "EMAIL":
                        Database.GetTable(tableName).Columns.Add(new EmailCol(colName));
                        foreach (Row row in Database.GetTable(tableName).Rows)
                        {
                            row.Values.Add("email@email.com");
                        }
                        
                        break;
                }
            }
        }

        public void AddSetColumn(string tableName, string colName, string values)
        {
            if (Database.GetTable(tableName) == null)
            {
                throw new Exception("No Table Found");
            }
            else
            {
                if (Database.GetTable(tableName).Columns.Find(x => x.Name == colName) != null)
                {
                    throw new Exception("Column exists");
                }
                SetCol sCol = new SetCol(colName, values);
                Database.GetTable(tableName).Columns.Add(sCol);
                foreach (Row row in Database.GetTable(tableName).Rows)
                {
                    row.Values.Add(sCol.Values.First());
                }
                
            }
        }

        public void DeleteColumn(string tableName, int colIndex)
        {
            foreach (Row row in Database.GetTable(tableName).Rows)
            {
                row.Values.RemoveAt(colIndex);
            }
            Database.GetTable(tableName).Columns.RemoveAt(colIndex);
        }

        public void AddRow(string tableName, string values)
        {
            List<string> val = values.Split(';').ToList();
            Row row = new Row();
            if(val.Count!= Database.GetTable(tableName).Columns.Count)
            {
                throw new Exception("Wrong row");
            }
            for(int i=0; i<Database.GetTable(tableName).Columns.Count; i++)
            {
                if (!Database.GetTable(tableName).Columns[i].Validate(val[i])){
                    throw new Exception("Wrong row");
                }
                row.Values.Add(val[i]);
            }
            Database.GetTable(tableName).Rows.Add(row);
        }

        public void EditRow(string tableName, string values, int rowIndex)
        {
            List<string> val = values.Split(';').ToList();
            Row row = new Row();
            if (val.Count > Database.GetTable(tableName).Columns.Count)
            {
                throw new Exception("Wrong row");
            }
            for (int i = 0; i < Database.GetTable(tableName).Columns.Count; i++)
            {
                if (!Database.GetTable(tableName).Columns[i].Validate(val[i]))
                {
                    throw new Exception("Wrong row");
                }
                row.Values.Add(val[i]);
            }
            Database.GetTable(tableName).Rows[rowIndex] = row;
        }

        public void DeleteRow(string tableName, int rowIndex)
        {
            Database.GetTable(tableName).Rows.RemoveAt(rowIndex);
        }

        public void EditCell(string tableName, int colIndex, int rowIndex, string val)
        {
            if (!Database.GetTable(tableName).Columns[colIndex].Validate(val))
            {
                throw new Exception("Wrong value");
            }
            Database.GetTable(tableName).Rows[rowIndex].Values[colIndex] = val;
        }

        public Table Multiply(string tableName1, string tableName2)
        {
            Table res = new Table("Result");
            if (Database.GetTable(tableName1) == null || Database.GetTable(tableName2) == null)
            {
                throw new Exception("Tables do not exist");
            }
            Table table1 = Database.GetTable(tableName1);
            Table table2 = Database.GetTable(tableName2);
            res.Columns.AddRange(table1.Columns);
            res.Columns.AddRange(table2.Columns);
            for (int i = 0; i < table1.Rows.Count; i++)
            {
                for (int j = 0; j < table2.Rows.Count; j++)
                {
                    Row row = new Row();
                    row.Values.AddRange(table1.Rows[i].Values);
                    row.Values.AddRange(table2.Rows[j].Values);
                    res.Rows.Add(row);
                }
            }
            return res;
        }

        public void DatabaseToFile(Stream path)
        {
            //path+="\\"+Database.Name;
            StreamWriter streamWriter = new StreamWriter(path);
            streamWriter.AutoFlush = true;
            streamWriter.WriteLine(Database.Tables.Count);
            foreach(Table t in Database.Tables)
            {
                streamWriter.WriteLine(t.Name);
                streamWriter.WriteLine(t.Columns.Count);
                streamWriter.WriteLine(t.Rows.Count);
                foreach(Column column in t.Columns)
                {
                    if(column.Type!="SET")
                    streamWriter.WriteLine(column.Name+";"+column.Type);
                    else
                    {
                        SetCol set = (SetCol)column;
                        List<string> val = set.Values.ToList();
                        string vals = "";
                        foreach(string v in val)
                        {
                            vals += v + ";";
                        }
                        vals = vals.Trim(';');
                        streamWriter.WriteLine(set.Name + ";" + set.Type + ";" + vals);
                    }
                   
                }
                foreach (Row row in t.Rows)
                {
                    string vals = "";
                    foreach (string v in row.Values)
                    {
                        vals += v + ";";
                    }
                    vals = vals.Trim(';');
                    streamWriter.WriteLine(vals);
                }
                streamWriter.Close();
            }
            
        }

        public void FileToDatabase(Stream path)
        {
            StreamReader streamReader = new StreamReader(path);
            //string dbName = path.Substring(path.LastIndexOf("\\"));
            CreateDatabase("test");
            string line = streamReader.ReadLine();
            int tableCount = Int32.Parse(line);
            for(int i=0; i < tableCount; i++)
            {
                line = streamReader.ReadLine();
                string tableName = line;
                AddTable(line);
                line = streamReader.ReadLine();
                int colCount = Int32.Parse(line);
                line = streamReader.ReadLine();
                int rowCount = Int32.Parse(line);
                for(int j=0; j<colCount; j++)
                {
                    line = streamReader.ReadLine();
                    List<string> strings = line.Split(';').ToList();
                    if (strings[1] == "SET")
                    {
                        string values = line.Substring(line.IndexOf(";")+1);
                        values = values.Substring(values.IndexOf(";") + 1);
                        AddSetColumn(tableName, strings[0], values);
                    }
                    else
                    {
                        AddColumn(tableName, strings[0], strings[1]);
                    }
                }
                for(int j=0; j<rowCount; j++)
                {
                    line =streamReader.ReadLine();
                    
                    AddRow(tableName, line);
                }
            }
        }
    }
}
