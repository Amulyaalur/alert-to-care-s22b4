using System.Collections.Generic;
using System.Data.SQLite;
using DataAccessLayer.Utils;
using DataModels;

namespace DataAccessLayer.LayoutManagement
{
    public class LayoutManagementSqLite:ILayoutManagement
    {
        public IEnumerable<Layout> GetAllLayouts()
        {
            var con = SqLiteDbConnector.GetSqLiteDbConnection();
            con.Open();
            var cmd = new SQLiteCommand(con)
            {
                CommandText = @"SELECT LayoutId,
                                       LayoutName
                                  FROM Layouts"
            };

            var reader = cmd.ExecuteReader();
            var listOfLayouts= new List<Layout>();

            while (reader.Read())
            {
                listOfLayouts.Add(new Layout()
                {
                    LayoutId = reader.GetString(0),
                    LayoutName = reader.GetString(1)
                });
            }
            reader.Dispose();
            con.Dispose();

            return listOfLayouts;
        }
    }
}