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
    /// 學生社團關聯
    /// </summary>
    public partial class StudClubDB
    {
        /// <summary>
        /// 判斷StudClub此筆資料是否存在
        /// </summary>
        /// <returns>Boolean</returns>
        public bool Exists(int StudId, int ClubId)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder sqlStatement = new StringBuilder();
            sqlStatement.Append("SELECT count(1) FROM stud_club WHERE stud_id = @stud_id AND club_id = @club_id");
            DbCommand dbCommand = db.GetSqlStringCommand(sqlStatement.ToString());
            db.AddInParameter(dbCommand, "@stud_id", DbType.Int64, StudId);
            db.AddInParameter(dbCommand, "@club_id", DbType.Int64, ClubId);
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
        /// <returns>StudClubInfo的泛型集合</returns>
        public IList<StudClubInfo> GetAll()
        {
            Database db = DatabaseFactory.CreateDatabase();

            StringBuilder sqlStatement = new StringBuilder();
            sqlStatement.Append("SELECT * FROM stud_club");

            DbCommand dbCommand = db.GetSqlStringCommand(sqlStatement.ToString());
            return GetList(db, dbCommand);

        }

        /// <summary>
        /// 讀取StudClub1筆資料
        /// </summary>
        /// <returns>StudClubInfo</returns>
        public StudClubInfo GetInfo(int StudId, int ClubId)
        {
            Database db = DatabaseFactory.CreateDatabase();

            StringBuilder sqlStatement = new StringBuilder();
            sqlStatement.Append("SELECT * FROM stud_club WHERE stud_id = @stud_id AND club_id = @club_id");

            DbCommand dbCommand = db.GetSqlStringCommand(sqlStatement.ToString());
            db.AddInParameter(dbCommand, "@stud_id", DbType.Int64, StudId);
            db.AddInParameter(dbCommand, "@club_id", DbType.Int64, ClubId);

            StudClubInfo myInfo = new StudClubInfo();
            IList<StudClubInfo> myList = new List<StudClubInfo>();
            myList = GetList(db, dbCommand);
            if (myList.Count > 0) {
                myInfo = myList[0];
            }

            return myInfo;
        }

        /// <summary>
        /// 新增StudClub1筆資料
        /// </summary>
        /// <param name="StudClub">
        /// StudClub新資料
        /// </param>
        /// <returns>Boolean</returns>
        public bool AddNew(StudClubInfo StudClub)
        {
            Database db = DatabaseFactory.CreateDatabase();

            StringBuilder sqlStatement = new StringBuilder();
            sqlStatement.Append("INSERT INTO stud_club ");
            sqlStatement.Append("(stud_id,club_id)");
            sqlStatement.Append("VALUES(@stud_id,@club_id)");

            DbCommand dbCommand = db.GetSqlStringCommand(sqlStatement.ToString());
            db.AddInParameter(dbCommand, "@stud_id", DbType.Int64, StudClub.StudId);
            db.AddInParameter(dbCommand, "@club_id", DbType.Int64, StudClub.ClubId);

            bool result = false;
            try
            {
                db.ExecuteNonQuery(dbCommand);
                result = true;
            }
            catch (DbException ex)
            {
                LogController.WriteLog("StudClubDB.AddNew", ex.ToString());
                throw (ex);
            }
            return result;
        }

        /// <summary>
        /// 更新StudClub某筆資料
        /// </summary>
        /// <returns>Boolean</returns>
        public bool Update(StudClubInfo StudClub)
        {
            Database db = DatabaseFactory.CreateDatabase();

            StringBuilder sqlStatement = new StringBuilder();
            sqlStatement.Append("UPDATE stud_club SET ");
            sqlStatement.Append(" WHERE stud_id = @stud_id AND club_id = @club_id");

            DbCommand dbCommand = db.GetSqlStringCommand(sqlStatement.ToString());
            db.AddInParameter(dbCommand, "@stud_id", DbType.Int64, StudClub.StudId);
            db.AddInParameter(dbCommand, "@club_id", DbType.Int64, StudClub.ClubId);

            bool result = false;
            try
            {
                db.ExecuteNonQuery(dbCommand);
                result = true;
            }
            catch (DbException ex)
            {
                LogController.WriteLog("StudClubDB.Update", ex.ToString());
                throw (ex);
            }
            return result;
        }

        /// <summary>
        /// 刪除StudClub某筆資料
        /// </summary>
        /// <returns>Boolean</returns>
        public bool Del(int StudId, int ClubId)
        {
            Database db = DatabaseFactory.CreateDatabase();

            StringBuilder sqlStatement = new StringBuilder();
            sqlStatement.Append("DELETE FROM stud_club WHERE stud_id = @stud_id AND club_id = @club_id");

            DbCommand dbCommand = db.GetSqlStringCommand(sqlStatement.ToString());
            db.AddInParameter(dbCommand, "@stud_id", DbType.Int64, StudId);
            db.AddInParameter(dbCommand, "@club_id", DbType.Int64, ClubId);

            bool result = false;
            try
            {
                db.ExecuteNonQuery(dbCommand);
                result = true;
            }
            catch (DbException ex)
            {
                LogController.WriteLog("StudClubDB.Del", ex.ToString());
                throw (ex);
            }
            return result;
        }

        private IList<StudClubInfo> GetList(Database db, DbCommand dbCommand)
        {
            IList<StudClubInfo> myList = new List<StudClubInfo>();

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
                LogController.WriteLog("StudClubDB.GetList", ex.ToString());
                throw (ex);
            }
            return myList;
        }

        private StudClubInfo BindInfo(IDataReader dr)
        {
            StudClubInfo myInfo = new StudClubInfo();

            try
            {
                if (!dr["stud_id"].Equals(DBNull.Value))
                {
                    myInfo.StudId = int.Parse(dr["stud_id"].ToString());
                }
                if (!dr["club_id"].Equals(DBNull.Value))
                {
                    myInfo.ClubId = int.Parse(dr["club_id"].ToString());
                }
            }
            catch (Exception ex)
            {
                LogController.WriteLog("StudClubDB.BindInfo", ex.ToString());
                throw (ex);
            }
            return myInfo;
        }
    }
}
