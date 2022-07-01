namespace Timesheet.DataAccess.CSV
{
    public class CsvSettings
    {
        public CsvSettings(char delimeter, string path)
        {
            Delimeter = delimeter;
            Path = path;
        }

        public char Delimeter { get; }
        public string Path { get; }
    }
}
