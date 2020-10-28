using System.Collections.Generic;
using System.Data.SQLite;
using DataAccessLayer.Utils;
using DataModels;

namespace DataAccessLayer.AlertManagement
{
    public class AlertManagementSqLite:IAlertManagement
    {
        public IEnumerable<Alert> GetAllAlerts()
        {
            var con = SqLiteDbConnector.GetSqLiteDbConnection();
            con.Open();
            var cmd = new SQLiteCommand(con)
            {
                CommandText = @"SELECT a.AlertId,
                                       a.LayoutId,
                                       a.IcuId,
                                       a.BedId,
                                       a.PatientId,
                                       p.PatientName,
                                       v.Bpm,
                                       v.Spo2,
                                       v.RespRate,
                                       a.AlertStatus
                                  FROM Alerts as a
                                  INNER JOIN Patients as p on p.PatientId = a.PatientId
                                  INNER JOIN Vitals as v on v.PatientId = a.PatientId"
            };

            var reader = cmd.ExecuteReader();
            var listOfAlerts = new List<Alert>();

            while (reader.Read())
            {
                listOfAlerts.Add(new Alert()
                {
                    AlertId = reader.GetInt32(0),
                    LayoutId = reader.GetString(1),
                    IcuId = reader.GetString(2),
                    BedId = reader.GetString(3),
                    PatientId = reader.GetString(4),
                    PatientName = reader.GetString(5),
                    Bpm = reader.GetFloat(6),
                    Spo2 = reader.GetFloat(7),
                    RespRate = reader.GetFloat(8),
                    AlertStatus = reader.GetBoolean(9)
                });
            }
            reader.Dispose();
            con.Dispose();

            return listOfAlerts;
        }
        public void ToggleAlertStatusByAlertId(int alertId)
        {
            ThrowExceptionIfAlertIdDoesNotExists(alertId);
            var con = SqLiteDbConnector.GetSqLiteDbConnection();
            con.Open();
            var cmd = new SQLiteCommand(con)
            {
                CommandText = @"SELECT AlertStatus
                                    FROM Alerts
                                    WHERE AlertId=@AlertId"
            };
            cmd.Parameters.AddWithValue("@AlertId", alertId);
            cmd.Prepare();
            var reader = cmd.ExecuteReader();
            reader.Read();
            var currentStatus = reader.GetBoolean(0);
            reader.Dispose();

            cmd.CommandText = @"UPDATE Alerts
                                        SET AlertStatus = @AlertStatus
                                        WHERE AlertId = @AlertId";
            cmd.Parameters.AddWithValue("@AlertId", alertId);
            cmd.Parameters.AddWithValue("@AlertStatus", !currentStatus);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            con.Dispose();
        }
        public static void AddToAlertsTable(string patientId)
        {
            var con = SqLiteDbConnector.GetSqLiteDbConnection();
            con.Open();

            var cmd = new SQLiteCommand(con)
            {
                CommandText = @"SELECT i.LayoutId,
                                       p.IcuId,
                                       p.BedId
                                    FROM Patients as p
                                    INNER JOIN Icu as i on p.IcuId = i.IcuId
                                    WHERE p.PatientId = @PatientId"
            };
            cmd.Parameters.AddWithValue("@PatientId", patientId);
            cmd.Prepare();
            var reader = cmd.ExecuteReader();
            reader.Read();
            var layoutId = reader.GetString(0);
            var icuId = reader.GetString(1);
            var bedId = reader.GetString(2);
            reader.Dispose();

            cmd.CommandText = @"INSERT INTO Alerts (
                                           LayoutId,
                                           IcuId,
                                           BedId,
                                           PatientId
                                           )
                                           VALUES (
                                           @LayoutId,
                                           @IcuId,
                                           @BedId,
                                           @PatientId
                                           )";
            cmd.Parameters.AddWithValue("@LayoutId", layoutId);
            cmd.Parameters.AddWithValue("@IcuId", icuId);
            cmd.Parameters.AddWithValue("@BedId", bedId);
            cmd.Parameters.AddWithValue("@PatientId", patientId);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            con.Dispose();
        }
        public void DeleteAlertByAlertId(int alertId)
        {
            ThrowExceptionIfAlertIdDoesNotExists(alertId);
            var con = SqLiteDbConnector.GetSqLiteDbConnection();
            con.Open();

            var cmd = new SQLiteCommand(con)
            {
                CommandText = @"DELETE FROM Alerts WHERE AlertId = @AlertId"
            };
            cmd.Parameters.AddWithValue("@AlertId", alertId);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            con.Dispose();
        }
        public static void ThrowExceptionIfAlertIdDoesNotExists(int alertId)
        {
            var con = SqLiteDbConnector.GetSqLiteDbConnection();
            con.Open();

            var cmd = new SQLiteCommand(con)
            {
                CommandText = @"SELECT COUNT(*) from Alerts WHERE AlertId = @AlertId"
            };

            cmd.Parameters.AddWithValue("@AlertId", alertId);
            cmd.Prepare();
            var count = (long)cmd.ExecuteScalar();
            con.Dispose();
            if (count == 0) throw new SQLiteException(SQLiteErrorCode.Constraint_PrimaryKey, message: "AlertId does not exists");
            
        }
    }
}