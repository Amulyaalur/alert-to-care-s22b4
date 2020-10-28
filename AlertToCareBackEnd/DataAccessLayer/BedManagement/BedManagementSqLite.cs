using System.Collections.Generic;
using System.Data.SQLite;
using DataAccessLayer.IcuManagement;
using DataAccessLayer.Utils;
using DataModels;

namespace DataAccessLayer.BedManagement
{
    public class BedManagementSqLite:IBedManagement
    {
        public IEnumerable<Bed> GetAllAvailableBedsByIcuId(string icuId)
        {
            if (IcuManagementSqLite.CheckIfIcuIdExists(icuId) == 0) throw new SQLiteException(SQLiteErrorCode.Constraint_PrimaryKey, message: "IcuId does not exists");
            
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
            if (IcuManagementSqLite.CheckIfIcuIdExists(icuId) == 0) throw new SQLiteException(SQLiteErrorCode.Constraint_PrimaryKey, message: "IcuId does not exists");
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
        public static void DeleteBedsByIcuId(string icuId)
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
            cmd.ExecuteNonQuery();
            con.Dispose();
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
        public static void ThrowExceptionIfBedIdDoesNotExists(string bedId)
        {
            var con = SqLiteDbConnector.GetSqLiteDbConnection();
            con.Open();

            var cmd = new SQLiteCommand(con)
            {
                CommandText = @"SELECT COUNT(*) from Beds WHERE BedId = @BedId"
            };

            cmd.Parameters.AddWithValue("@BedId", bedId);
            cmd.Prepare();
            var count = (long)cmd.ExecuteScalar();
            con.Dispose();
            if (count == 0) throw new SQLiteException(SQLiteErrorCode.Constraint_PrimaryKey, message: "BedId does not exists");
        }
        public static void ThrowExceptionIfBedStatusIsTrueByBedIdAndIcuId(string icuId, string bedId)
        {
            var con = SqLiteDbConnector.GetSqLiteDbConnection();
            con.Open();

            var cmd = new SQLiteCommand(con)
            {
                CommandText = @"SELECT Status from Beds WHERE BedId = @BedId AND IcuId = @IcuId"
            };

            cmd.Parameters.AddWithValue("@BedId", bedId);
            cmd.Parameters.AddWithValue("@IcuId", icuId);
            cmd.Prepare();
            var reader = cmd.ExecuteReader();
            reader.Read();
            var status = reader.GetBoolean(0);
            reader.Dispose();
            con.Dispose();
            if (status) throw new SQLiteException(SQLiteErrorCode.NotFound, message: "Bed is not Available");
        }
    }
}