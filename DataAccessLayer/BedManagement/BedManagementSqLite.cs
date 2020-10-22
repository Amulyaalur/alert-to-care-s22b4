using System.Collections.Generic;
using System.Data.SQLite;
using DataAccessLayer.Utils;
using DataModels;

namespace DataAccessLayer.BedManagement
{
    public class BedManagementSqLite:IBedManagement
    {
        public IEnumerable<Bed> GetAllAvailableBedsByIcuId(string icuId)
        {
            var con = SqLiteDbConnector.GetSqLiteDbConnection();
            con.Open();
            var cmd = new SQLiteCommand(con)
            {
                CommandText = @"SELECT BedId,
                                       IcuId,
                                       Status
                                  FROM Beds
                                  WHERE IcuId = @IcuId AND
                                  Status = false"
            };
            cmd.Parameters.AddWithValue("@IcuId", icuId);
            cmd.Prepare();
            var reader = cmd.ExecuteReader();
            var listOfAvailableBeds = new List<Bed>();

            while (reader.Read())
            {
                listOfAvailableBeds.Add(new Bed()
                {
                    BedId = reader.GetString(0),
                    IcuId = reader.GetString(1),
                    Status = reader.GetBoolean(2)
                });
            }
            reader.Dispose();
            con.Dispose();

            return listOfAvailableBeds;
        }
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
            con.Dispose();
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
            con.Dispose();

            return rowsAffected;
        }
        public static void ChangeBedStatusToTrueByBedId(string bedId)
        {
            var con = SqLiteDbConnector.GetSqLiteDbConnection();
            con.Open();
            var cmd = new SQLiteCommand(con)
            {
                CommandText = @"UPDATE Beds
                                    SET 
                                        Status = true
                                        WHERE BedId = @BedId"
            };
            cmd.Parameters.AddWithValue("@BedId", bedId);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            
            con.Dispose();
        }
        public static void ChangeBedStatusToFalseByPatientId(string patientId)
        {
            var con = SqLiteDbConnector.GetSqLiteDbConnection();
            con.Open();
            var cmd = new SQLiteCommand(con)
            {
                CommandText = @"UPDATE Beds
                                    SET 
                                        Status = false
                                        WHERE BedId = (Select BedId from Patients where PatientId=@PatientId)"
            };
            cmd.Parameters.AddWithValue("@PatientId", patientId);
            cmd.Prepare();
            cmd.ExecuteNonQuery();

            con.Dispose();
        }
    }
}