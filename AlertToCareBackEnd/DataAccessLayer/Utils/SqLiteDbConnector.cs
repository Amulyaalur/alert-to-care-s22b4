using System.Data.SQLite;
using System.IO;

namespace DataAccessLayer.Utils
{
    public static class SqLiteDbConnector
    {
        public static SQLiteConnection GetSqLiteDbConnection()
        {
            var path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var cs = $@"URI=file:{Path.GetFullPath(Path.Combine(path!, @"..\..\..\"))}AlertToCare.db";
            var con = new SQLiteConnection(cs);
            return con;
        }
    }
}