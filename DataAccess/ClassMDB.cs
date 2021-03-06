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
    /// 班級
    /// </summary>
    public partial class ClassMDB
    {
        /// <summary>
        /// 判斷ClassM此筆資料是否存在
        /// </summary>
        /// <param name="Id">
        /// 班級代碼
        /// </param>
        /// <returns>Boolean</returns>
        public bool Exists(string Id)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder sqlStatement = new StringBuilder();
            sqlStatement.Append("SELECT count(1) FROM class_m WHERE id = @id");
            DbCommand dbCommand = db.GetSqlStringCommand(sqlStatement.ToString());
            db.AddInParameter(dbCommand, "@id", DbType.String, Id);
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
        /// <returns>ClassMInfo的泛型集合</returns>
        public IList<ClassMInfo> GetAll()
        {
            Database db = DatabaseFactory.CreateDatabase();

            StringBuilder sqlStatement = new StringBuilder();
            sqlStatement.Append("SELECT * FROM class_m");

            DbCommand dbCommand = db.GetSqlStringCommand(sqlStatement.ToString());
            return GetList(db, dbCommand);

        }

        /// <summary>
        /// 讀取ClassM1筆資料
        /// </summary>
        /// <param name="Id">
        /// 班級代碼
        /// </param>
        /// <returns>ClassMInfo</returns>
        public ClassMInfo GetInfo(string Id)
        {
            Database db = DatabaseFactory.CreateDatabase();

            StringBuilder sqlStatement = new StringBuilder();
            sqlStatement.Append("SELECT * FROM class_m WHERE id = @id");

            DbCommand dbCommand = db.GetSqlStringCommand(sqlStatement.ToString());
            db.AddInParameter(dbCommand, "@id", DbType.String, Id);

            ClassMInfo myInfo = new ClassMInfo();
            IList<ClassMInfo> myList = new List<ClassMInfo>();
            myList = GetList(db, dbCommand);
            if (myList.Count > 0) {
                myInfo = myList[0];
            }

            return myInfo;
        }

        /// <summary>
        /// 新增ClassM1筆資料
        /// </summary>
        /// <param name="ClassM">
        /// ClassM新資料
        /// </param>
        /// <returns>Boolean</returns>
        public bool AddNew(ClassMInfo ClassM)
        {
            Database db = DatabaseFactory.CreateDatabase();

            StringBuilder sqlStatement = new StringBuilder();
            sqlStatement.Append("INSERT INTO class_m ");
            sqlStatement.Append("(id,name)");
            sqlStatement.Append("VALUES(@id,@name)");

            DbCommand dbCommand = db.GetSqlStringCommand(sqlStatement.ToString());
            db.AddInParameter(dbCommand, "@id", DbType.String, ClassM.Id);
            db.AddInParameter(dbCommand, "@name", DbType.String, ClassM.Name);

            bool result = false;
            try
            {
                db.ExecuteNonQuery(dbCommand);
                result = true;
            }
            catch (DbException ex)
            {
                LogController.WriteLog("ClassMDB.AddNew", ex.ToString());
                throw (ex);
            }
            return result;
        }

        /// <summary>
        /// 更新ClassM某筆資料
        /// </summary>
        /// <returns>Boolean</returns>
        public bool Update(ClassMInfo ClassM)
        {
            Database db = DatabaseFactory.CreateDatabase();

            StringBuilder sqlStatement = new StringBuilder();
            sqlStatement.Append("UPDATE class_m SET ");
            sqlStatement.Append("name = @name");
            sqlStatement.Append(" WHERE id = @id");

            DbCommand dbCommand = db.GetSqlStringCommand(sqlStatement.ToString());
            db.AddInParameter(dbCommand, "@id", DbType.String, ClassM.Id);
            db.AddInParameter(dbCommand, "@name", DbType.String, ClassM.Name);

            bool result = false;
            try
            {
                db.ExecuteNonQuery(dbCommand);
                result = true;
            }
            catch (DbException ex)
            {
                LogController.WriteLog("ClassMDB.Update", ex.ToString());
                throw (ex);
            }
            return result;
        }

        /// <summary>
        /// 刪除ClassM某筆資料
        /// </summary>
        /// <param name="Id">
        /// 班級代碼
        /// </param>
        /// <returns>Boolean</returns>
        public bool Del(string Id)
        {
            Database db = DatabaseFactory.CreateDatabase();

            StringBuilder sqlStatement = new StringBuilder();
            sqlStatement.Append("DELETE FROM class_m WHERE id = @id");

            DbCommand dbCommand = db.GetSqlStringCommand(sqlStatement.ToString());
            db.AddInParameter(dbCommand, "@id", DbType.String, Id);

            bool result = false;
            try
            {
                db.ExecuteNonQuery(dbCommand);
                result = true;
            }
            catch (DbException ex)
            {
                LogController.WriteLog("ClassMDB.Del", ex.ToString());
                throw (ex);
            }
            return result;
        }

        private IList<ClassMInfo> GetList(Database db, DbCommand dbCommand)
        {
            IList<ClassMInfo> myList = new List<ClassMInfo>();

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
                LogController.WriteLog("ClassMDB.GetList", ex.ToString());
                throw (ex);
            }
            return myList;
        }

        private ClassMInfo BindInfo(IDataReader dr)
        {
            ClassMInfo myInfo = new ClassMInfo();

            try
            {
                if (!dr["id"].Equals(DBNull.Value))
                {
                    myInfo.Id = dr["id"].ToString();
                }
                if (!dr["name"].Equals(DBNull.Value))
                {
                    myInfo.Name = dr["name"].ToString();
                }
            }
            catch (Exception ex)
            {
                LogController.WriteLog("ClassMDB.BindInfo", ex.ToString());
                throw (ex);
            }
            return myInfo;
        }
    }
}
