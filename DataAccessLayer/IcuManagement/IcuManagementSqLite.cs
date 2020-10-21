using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
            
            BedManagementSqLite.AddBeds(icu.IcuId, icu.BedsCount);
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
            var rowsAffected = BedManagementSqLite.DeleteBedsByIcuId(icuId);

            if (rowsAffected == 0)
            {
                throw new Exception();
            }

            var con = SqLiteDbConnector.GetSqLiteDbConnection();
            con.Open();

            var cmd = new SQLiteCommand(con)
            {
                CommandText = @"DELETE FROM Icu WHERE IcuId = @icuId"
            };

            cmd.Parameters.AddWithValue("@icuId", icuId);
            cmd.Prepare();
            rowsAffected = cmd.ExecuteNonQuery();
            con.Close();
            if (rowsAffected == 0)
            {
                throw new Exception();
            }
            return true;
        }
    }
}