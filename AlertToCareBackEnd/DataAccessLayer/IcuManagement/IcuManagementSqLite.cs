using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Net.NetworkInformation;
using DataAccessLayer.BedManagement;
using DataAccessLayer.Utils;
using DataAccessLayer.Utils.Validators;
using DataModels;

namespace DataAccessLayer.IcuManagement
{
    public class IcuManagementSqLite:IIcuManagement
    {
        public IEnumerable<Icu> GetAllIcu()
        {
            var con = SqLiteDbConnector.GetSqLiteDbConnection();
            con.Open();
            var cmd = new SQLiteCommand(con)
            {
                CommandText = "Select IcuId,BedsCount,LayoutId from Icu"
            };
            
            var reader = cmd.ExecuteReader();
            var listOfIcu = new List<Icu>();

            while (reader.Read())
            {
                listOfIcu.Add(new Icu()
                {
                    IcuId = reader.GetString(0),
                    BedsCount = reader.GetInt32(1),
                    LayoutId = reader.GetString(2)
                });
            }
            reader.Dispose();
            con.Dispose();

            return listOfIcu;
        }
        public Icu GetIcuById(string icuId)
        {
            if (CheckIfIcuIdExists(icuId) == 0) throw new SQLiteException(SQLiteErrorCode.Constraint_PrimaryKey,"IcuId does not exists");
            var con = SqLiteDbConnector.GetSqLiteDbConnection();
            con.Open();
            var cmd = new SQLiteCommand(con)
            {
                CommandText = @"Select IcuId,BedsCount,LayoutId from Icu where IcuId=@icuId"
            };
            cmd.Parameters.AddWithValue("@icuId", icuId);
            cmd.Prepare();
            var reader = cmd.ExecuteReader();
            reader.Read();
            var icu = new Icu
            {
                IcuId = reader.GetString(0),
                BedsCount = reader.GetInt32(1),
                LayoutId = reader.GetString(2)
            };
            reader.Dispose();
            con.Dispose();
            return icu;
        }
        public void AddIcu(Icu icu)
        {
            IcuDataModelValidator.ValidateIcuDataModel(icu);
            if (CheckIfIcuIdExists(icu.IcuId) > 0) throw new SQLiteException(SQLiteErrorCode.Constraint_PrimaryKey, message: "IcuId exists");
            var con = SqLiteDbConnector.GetSqLiteDbConnection();
            con.Open();

            var cmd = new SQLiteCommand(con)
            {
                CommandText =
                    @"INSERT INTO Icu (
                IcuId,
                BedsCount,
                LayoutId
            )
            VALUES (
                @IcuId,
                @BedsCount,
                @LayoutId
            )"
            };

            cmd.Parameters.AddWithValue("@IcuId", icu.IcuId);
            cmd.Parameters.AddWithValue("@BedsCount", icu.BedsCount);
            cmd.Parameters.AddWithValue("@LayoutId", icu.LayoutId);

            cmd.Prepare();
            cmd.ExecuteNonQuery();
            con.Dispose();
            
            BedManagementSqLite.AddBeds(icu.IcuId, icu.BedsCount);
        }
        public void UpdateIcuById(string icuId, Icu icu)
        {
            IcuDataModelValidator.ValidateIcuDataModel(icu);
           // CommonFieldValidator.StringValidator(icuId);
            if (CheckIfIcuIdExists(icu.IcuId) == 0) throw new SQLiteException(SQLiteErrorCode.Constraint_PrimaryKey, message: "IcuId already exists");
            DeleteIcuById(icuId);
            AddIcu(icu);
        }
        public void DeleteIcuById(string icuId)
        {
            if (CheckIfIcuIdExists(icuId) == 0) throw new SQLiteException(SQLiteErrorCode.Constraint_PrimaryKey, message: "IcuId does not exists");
            var rowsAffected = BedManagementSqLite.DeleteBedsByIcuId(icuId);
            CheckRowAffectedException(rowsAffected);
            

            var con = SqLiteDbConnector.GetSqLiteDbConnection();
            con.Open();

            var cmd = new SQLiteCommand(con)
            {
                CommandText = @"DELETE FROM Icu WHERE IcuId = @icuId"
            };

            cmd.Parameters.AddWithValue("@icuId", icuId);
            cmd.Prepare();
            rowsAffected = cmd.ExecuteNonQuery();
            con.Dispose();
            CheckRowAffected(rowsAffected, "IcuId does not exists");
            
        }

        private static void CheckRowAffectedException(int rowsAffected)
        {
            if (rowsAffected == 0)
            {
                throw new Exception();
            }
        }
        private static void CheckRowAffected(int rowsAffected,string message)
        {
            if (rowsAffected == 0)
            {
                throw new SQLiteException(SQLiteErrorCode.NotFound, message: message);
            }
        }
        public static long CheckIfIcuIdExists(string icuId)
        {
            var con = SqLiteDbConnector.GetSqLiteDbConnection();
            con.Open();

            var cmd = new SQLiteCommand(con)
            {
                CommandText = @"SELECT COUNT(*) from Icu WHERE IcuId = @IcuId"
            };

            cmd.Parameters.AddWithValue("@IcuId", icuId);
            cmd.Prepare();
            var count = (long)cmd.ExecuteScalar();
            con.Dispose();
            return count;
        }
    }
}