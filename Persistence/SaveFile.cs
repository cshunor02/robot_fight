namespace Persistence
{
    public class SaveFile
    {
        #region Variables
        private static string prev_table = "";
        #endregion

        #region Clear Table
        // Clear the previous game's table
        public static void ClearFile()
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
            string subfolderPath = Path.Combine(projectDirectory, "Persistence");
            string filePath = Path.Combine(subfolderPath, "log.txt");

            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                fs.SetLength(0);
            }

            filePath = Path.Combine(subfolderPath, "table.txt");

            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                fs.SetLength(0);
            }
        }
        #endregion

        #region Write log.txt file
        public static void WriteInFile(string message)
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
            string subfolderPath = Path.Combine(projectDirectory, "Persistence");
            string filePath = Path.Combine(subfolderPath, "log.txt");

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(message);
            }
        }
        #endregion

        #region Write table.txt file
        public static void WriteTable(string table)
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
            string subfolderPath = Path.Combine(projectDirectory, "Persistence");
            string filePath = Path.Combine(subfolderPath, "table.txt");

            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                fs.SetLength(0);
            }

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(table);
            }
        }
        #endregion

        #region Read log.txt file
        public static string ReadFromLog()
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
            string subfolderPath = Path.Combine(projectDirectory, "Persistence");
            string path = Path.Combine(subfolderPath, "log.txt");

            string readText = File.ReadAllText(path);
            return readText;
        }
        #endregion

        #region Read from table.txt file

        // If there were no updates in table.txt
        // the first variable will give back false
        public static (bool, string) ReadFromTable()
        {
            bool modified = true;
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
            string subfolderPath = Path.Combine(projectDirectory, "Persistence");
            string path = Path.Combine(subfolderPath, "table.txt");

            string readText = File.ReadAllText(path);

            if(prev_table.Equals(readText))
            {
                modified = false;
            } else
            {
                prev_table = readText;
            }

            return (modified, readText);
        }
        #endregion
    }
}
