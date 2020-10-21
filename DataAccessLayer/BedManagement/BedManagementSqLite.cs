using System.Data.SQLite;
using DataAccessLayer.Utils;

namespace DataAccessLayer.BedManagement
{
    public class BedManagementSqLite:IBedManagement
    {
        public static void AddBeds(string icuId, int bedsCount)
        {
            var con = SqLiteDbConnector.GetSqLiteDbConnection();
            var cmd = new SQLiteCommand(con);
            con.Open();

            for (var i = 1; i <= bedsCount; i++)
            {
                cmd.CommandText = @"INSERT INTO Beds (
                                         BedId,
                                         IcuId
                                     )
                                     VALUES (
                                         @BedId,
                                         @IcuId
                                     )";
                cmd.Parameters.AddWithValue("@BedId", icuId + "BED" + i);
                cmd.Parameters.AddWithValue("@IcuId", icuId);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
            con.Close();
        }
        public static int DeleteBedsByIcuId(string icuId)
        {
            var con = SqLiteDbConnector.GetSqLiteDbConnection();
            con.Open();

            var cmd = new SQLiteCommand(con)
            {
                CommandText = @"DELETE FROM Beds
                                    WHERE IcuId = @icuId AND 
                                    (SELECT count(*) 
                                        FROM Beds
                                            WHERE IcuId = @icuId AND
                                            Status = @status) = 0"
            };
            cmd.Parameters.AddWithValue("@icuId", icuId);
            cmd.Parameters.AddWithValue("@status", true);
            cmd.Prepare();
            var rowsAffected = cmd.ExecuteNonQuery();
            con.Close();

            return rowsAffected;
        }
        public static void ChangeBedStatusToTrueByBedId(string bedId)
        {
            var con = SqLiteDbConnector.GetSqLiteDbConnection();
            var cmd = new SQLiteCommand(con);
            con.Open();

            
            cmd.CommandText = @"UPDATE Beds
                                    SET 
                                        Status = true
                                        WHERE BedId = @BedId";
            cmd.Parameters.AddWithValue("@BedId", bedId);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            
            con.Close();
        }
        public static void ChangeBedStatusToFalseByPatientId(string patientId)
        {
            var con = SqLiteDbConnector.GetSqLiteDbConnection();
            var cmd = new SQLiteCommand(con);
            con.Open();

            cmd.CommandText = @"UPDATE Beds
                                    SET 
                                        Status = false
                                        WHERE BedId = (Select BedId from Patients where PatientId=@PatientId)";
            cmd.Parameters.AddWithValue("@PatientId", patientId);
            cmd.Prepare();
            cmd.ExecuteNonQuery();

            con.Close();
        }
    }
}