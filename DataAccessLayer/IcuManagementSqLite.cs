using System;
using System.Collections.Generic;
using System.Data.SQLite;
using DataAccessLayer.Utils;
using DataAccessLayer.Utils.Validators;
using DataModels;

namespace DataAccessLayer
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
            con.Close();

            return listOfIcu;
        }
        public Icu GetIcuById(string icuId)
        {
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

            con.Close();

            return icu;
        }
        public void AddIcu(Icu icu)
        {
            IcuDataModelValidator.ValidateIcuDataModel(icu);
            
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
            con.Close();
            
            AddBeds(icu.IcuId, icu.BedsCount);
        }
        private void AddBeds(string icuId, int bedsCount)
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
        public bool UpdateIcuById(string icuId, Icu icu)
        {
            IcuDataModelValidator.ValidateIcuDataModel(icu);

            DeleteIcuById(icuId);
            AddIcu(icu);
            return true;
        }
        public bool DeleteIcuById(string icuId)
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

            if (rowsAffected == 0)
            {
                con.Close();
                throw new Exception();
            }
            cmd.CommandText = @"DELETE FROM Icu WHERE IcuId = @icuId";
            cmd.Parameters.AddWithValue("@icuId", icuId);
            cmd.Prepare();
            rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                con.Close();
                throw new Exception();
            }
            con.Close();
            return true;
        }


    }
}