using System;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Collections;
using Information;
using Commons;
namespace DataAccess
{
    /// <summary>
    /// 社團
    /// </summary>
    public partial class ClubMDB
    {
        /// <returns>ClubMInfo的泛型集合</returns>
        public IList<ClubMInfo> StudData(int StudId)
        {
            Database db = DatabaseFactory.CreateDatabase();

            StringBuilder sqlStatement = new StringBuilder();
            sqlStatement.Append("select t1.* from club_m t1 ");
            sqlStatement.Append("inner join stud_club t2 ");
            sqlStatement.Append("on t1.sn = t2.club_id and t2.stud_id = @StudId ");

            DbCommand dbCommand = db.GetSqlStringCommand(sqlStatement.ToString());
            db.AddInParameter(dbCommand, "@StudId", DbType.Int32, StudId);

            return GetList(db, dbCommand);
        }

        /// <summary>
        /// 判斷ClubM此筆資料是否存在
        /// </summary>
        /// <param name="Sn">
        /// 流水號
        /// </param>
        /// <returns>Boolean</returns>
        public bool Exists(int Sn)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder sqlStatement = new StringBuilder();
            sqlStatement.Append("SELECT count(1) FROM club_m WHERE sn = @sn");
            DbCommand dbCommand = db.GetSqlStringCommand(sqlStatement.ToString());
            db.AddInParameter(dbCommand, "@sn", DbType.Int64, Sn);
            int cmdResult = int.Parse(db.ExecuteScalar(dbCommand).ToString());
            if (cmdResult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 讀取全部資料
        /// </summary>
        /// <returns>ClubMInfo的泛型集合</returns>
        public IList<ClubMInfo> GetAll()
        {
            Database db = DatabaseFactory.CreateDatabase();

            StringBuilder sqlStatement = new StringBuilder();
            sqlStatement.Append("SELECT * FROM club_m");

            DbCommand dbCommand = db.GetSqlStringCommand(sqlStatement.ToString());
            return GetList(db, dbCommand);

        }

        /// <summary>
        /// 讀取ClubM1筆資料
        /// </summary>
        /// <param name="Sn">
        /// 流水號
        /// </param>
        /// <returns>ClubMInfo</returns>
        public ClubMInfo GetInfo(int Sn)
        {
            Database db = DatabaseFactory.CreateDatabase();

            StringBuilder sqlStatement = new StringBuilder();
            sqlStatement.Append("SELECT * FROM club_m WHERE sn = @sn");

            DbCommand dbCommand = db.GetSqlStringCommand(sqlStatement.ToString());
            db.AddInParameter(dbCommand, "@sn", DbType.Int64, Sn);

            ClubMInfo myInfo = new ClubMInfo();
            IList<ClubMInfo> myList = new List<ClubMInfo>();
            myList = GetList(db, dbCommand);
            if (myList.Count > 0) {
                myInfo = myList[0];
            }

            return myInfo;
        }

        /// <summary>
        /// 新增ClubM1筆資料
        /// </summary>
        /// <param name="ClubM">
        /// ClubM新資料
        /// </param>
        /// <returns>Boolean</returns>
        public bool AddNew(ClubMInfo ClubM)
        {
            Database db = DatabaseFactory.CreateDatabase();

            StringBuilder sqlStatement = new StringBuilder();
            sqlStatement.Append("INSERT INTO club_m ");
            sqlStatement.Append("(name)");
            sqlStatement.Append("VALUES(@name)");

            DbCommand dbCommand = db.GetSqlStringCommand(sqlStatement.ToString());
            db.AddInParameter(dbCommand, "@name", DbType.String, ClubM.Name);

            bool result = false;
            try
            {
                db.ExecuteNonQuery(dbCommand);
                dbCommand = db.GetSqlStringCommand("SELECT max(sn) FROM club_m ");
                ClubM.Sn =  int.Parse(db.ExecuteScalar(dbCommand).ToString());
                result = true;
            }
            catch (DbException ex)
            {
                LogController.WriteLog("ClubMDB.AddNew", ex.ToString());
                throw (ex);
            }
            return result;
        }

        /// <summary>
        /// 更新ClubM某筆資料
        /// </summary>
        /// <returns>Boolean</returns>
        public bool Update(ClubMInfo ClubM)
        {
            Database db = DatabaseFactory.CreateDatabase();

            StringBuilder sqlStatement = new StringBuilder();
            sqlStatement.Append("UPDATE club_m SET ");
            sqlStatement.Append("name = @name");
            sqlStatement.Append(" WHERE sn = @sn");

            DbCommand dbCommand = db.GetSqlStringCommand(sqlStatement.ToString());
            db.AddInParameter(dbCommand, "@sn", DbType.Int64, ClubM.Sn);
            db.AddInParameter(dbCommand, "@name", DbType.String, ClubM.Name);

            bool result = false;
            try
            {
                db.ExecuteNonQuery(dbCommand);
                result = true;
            }
            catch (DbException ex)
            {
                LogController.WriteLog("ClubMDB.Update", ex.ToString());
                throw (ex);
            }
            return result;
        }

        /// <summary>
        /// 刪除ClubM某筆資料
        /// </summary>
        /// <param name="Sn">
        /// 流水號
        /// </param>
        /// <returns>Boolean</returns>
        public bool Del(int Sn)
        {
            Database db = DatabaseFactory.CreateDatabase();

            StringBuilder sqlStatement = new StringBuilder();
            sqlStatement.Append("DELETE FROM club_m WHERE sn = @sn");

            DbCommand dbCommand = db.GetSqlStringCommand(sqlStatement.ToString());
            db.AddInParameter(dbCommand, "@sn", DbType.Int64, Sn);

            bool result = false;
            try
            {
                db.ExecuteNonQuery(dbCommand);
                result = true;
            }
            catch (DbException ex)
            {
                LogController.WriteLog("ClubMDB.Del", ex.ToString());
                throw (ex);
            }
            return result;
        }

        private IList<ClubMInfo> GetList(Database db, DbCommand dbCommand)
        {
            IList<ClubMInfo> myList = new List<ClubMInfo>();

            try
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        myList.Add(BindInfo(dataReader));
                    }
                }
            }
            catch (DbException ex)
            {
                LogController.WriteLog("ClubMDB.GetList", ex.ToString());
                throw (ex);
            }
            return myList;
        }

        private ClubMInfo BindInfo(IDataReader dr)
        {
            ClubMInfo myInfo = new ClubMInfo();

            try
            {
                if (!dr["sn"].Equals(DBNull.Value))
                {
                    myInfo.Sn = int.Parse(dr["sn"].ToString());
                }
                if (!dr["name"].Equals(DBNull.Value))
                {
                    myInfo.Name = dr["name"].ToString();
                }
            }
            catch (Exception ex)
            {
                LogController.WriteLog("ClubMDB.BindInfo", ex.ToString());
                throw (ex);
            }
            return myInfo;
        }
    }
}
