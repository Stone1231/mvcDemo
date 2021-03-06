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
    /// 學生
    /// </summary>
    public partial class StudentDB
    {
        /// <param name="ClassId">
        /// 班級代碼
        /// </param>
        /// <returns>StudentInfo的泛型集合</returns>
        public IList<StudentInfo> ClassData(string ClassId)
        {
            Database db = DatabaseFactory.CreateDatabase();

            StringBuilder sqlStatement = new StringBuilder();
            sqlStatement.Append("SELECT * FROM student ");
            sqlStatement.Append("WHERE ");
            sqlStatement.Append("class_id = @ClassId ");

            DbCommand dbCommand = db.GetSqlStringCommand(sqlStatement.ToString());
            db.AddInParameter(dbCommand, "@ClassId", DbType.String, ClassId);

            return GetList(db, dbCommand);
        }

        /// <returns>StudentInfo的泛型集合</returns>
        public IList<StudentInfo> ClubData(int ClubId)
        {
            Database db = DatabaseFactory.CreateDatabase();

            StringBuilder sqlStatement = new StringBuilder();
            sqlStatement.Append("SELECT t1.* FROM student t1 ");
            sqlStatement.Append("inner join stud_club t2 ");
            sqlStatement.Append("on t1.sn = t2.stud_id and t2.club_id = @ClubId ");

            DbCommand dbCommand = db.GetSqlStringCommand(sqlStatement.ToString());
            db.AddInParameter(dbCommand, "@ClubId", DbType.Int32, ClubId);

            return GetList(db, dbCommand);
        }

        /// <param name="ClassId">
        /// 班級代碼
        /// </param>
        /// <returns>StudentInfo的泛型集合</returns>
        public IList<StudentInfo> QueryData(string ClassId, string Name)
        {
            Database db = DatabaseFactory.CreateDatabase();

            StringBuilder sqlStatement = new StringBuilder();
            sqlStatement.Append("SELECT * FROM student ");
            sqlStatement.Append("WHERE ");
            sqlStatement.Append("(class_id = @ClassId or '' = @ClassId) ");
            sqlStatement.Append("and name like '%' || @Name || '%' ");

            DbCommand dbCommand = db.GetSqlStringCommand(sqlStatement.ToString());
            db.AddInParameter(dbCommand, "@ClassId", DbType.String, ClassId);
            db.AddInParameter(dbCommand, "@Name", DbType.String, Name);

            return GetList(db, dbCommand);
        }

        /// <summary>
        /// 判斷Student此筆資料是否存在
        /// </summary>
        /// <param name="Sn">
        /// 流水號
        /// </param>
        /// <returns>Boolean</returns>
        public bool Exists(int Sn)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder sqlStatement = new StringBuilder();
            sqlStatement.Append("SELECT count(1) FROM student WHERE sn = @sn");
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
        /// <returns>StudentInfo的泛型集合</returns>
        public IList<StudentInfo> GetAll()
        {
            Database db = DatabaseFactory.CreateDatabase();

            StringBuilder sqlStatement = new StringBuilder();
            sqlStatement.Append("SELECT * FROM student");

            DbCommand dbCommand = db.GetSqlStringCommand(sqlStatement.ToString());
            return GetList(db, dbCommand);

        }

        /// <summary>
        /// 讀取Student1筆資料
        /// </summary>
        /// <param name="Sn">
        /// 流水號
        /// </param>
        /// <returns>StudentInfo</returns>
        public StudentInfo GetInfo(int Sn)
        {
            Database db = DatabaseFactory.CreateDatabase();

            StringBuilder sqlStatement = new StringBuilder();
            sqlStatement.Append("SELECT * FROM student WHERE sn = @sn");

            DbCommand dbCommand = db.GetSqlStringCommand(sqlStatement.ToString());
            db.AddInParameter(dbCommand, "@sn", DbType.Int64, Sn);

            StudentInfo myInfo = new StudentInfo();
            IList<StudentInfo> myList = new List<StudentInfo>();
            myList = GetList(db, dbCommand);
            if (myList.Count > 0) {
                myInfo = myList[0];
            }

            return myInfo;
        }

        /// <summary>
        /// 新增Student1筆資料
        /// </summary>
        /// <param name="Student">
        /// Student新資料
        /// </param>
        /// <returns>Boolean</returns>
        public bool AddNew(StudentInfo Student)
        {
            Database db = DatabaseFactory.CreateDatabase();

            StringBuilder sqlStatement = new StringBuilder();
            sqlStatement.Append("INSERT INTO student ");
            sqlStatement.Append("(name,class_id,hight,weight,birthday,photo,memo)");
            sqlStatement.Append("VALUES(@name,@class_id,@hight,@weight,@birthday,@photo,@memo)");

            DbCommand dbCommand = db.GetSqlStringCommand(sqlStatement.ToString());
            db.AddInParameter(dbCommand, "@name", DbType.String, Student.Name);
            db.AddInParameter(dbCommand, "@class_id", DbType.String, Student.ClassId);
            db.AddInParameter(dbCommand, "@hight", DbType.Int64, Student.Hight);
            db.AddInParameter(dbCommand, "@weight", DbType.Double, Student.Weight);
            if (Student.Birthday.Year != 1) { db.AddInParameter(dbCommand, "@birthday", DbType.DateTime, Student.Birthday); } else { db.AddInParameter(dbCommand, "@birthday", DbType.DateTime, DBNull.Value); }
            db.AddInParameter(dbCommand, "@photo", DbType.String, Student.Photo);
            db.AddInParameter(dbCommand, "@memo", DbType.String, Student.Memo);

            bool result = false;
            try
            {
                db.ExecuteNonQuery(dbCommand);
                dbCommand = db.GetSqlStringCommand("SELECT max(sn) FROM student ");
                Student.Sn =  int.Parse(db.ExecuteScalar(dbCommand).ToString());
                result = true;
            }
            catch (DbException ex)
            {
                LogController.WriteLog("StudentDB.AddNew", ex.ToString());
                throw (ex);
            }
            return result;
        }

        /// <summary>
        /// 更新Student某筆資料
        /// </summary>
        /// <returns>Boolean</returns>
        public bool Update(StudentInfo Student)
        {
            Database db = DatabaseFactory.CreateDatabase();

            StringBuilder sqlStatement = new StringBuilder();
            sqlStatement.Append("UPDATE student SET ");
            sqlStatement.Append("name = @name");
            sqlStatement.Append(",class_id = @class_id");
            sqlStatement.Append(",hight = @hight");
            sqlStatement.Append(",weight = @weight");
            sqlStatement.Append(",birthday = @birthday");
            sqlStatement.Append(",photo = @photo");
            sqlStatement.Append(",memo = @memo");
            sqlStatement.Append(" WHERE sn = @sn");

            DbCommand dbCommand = db.GetSqlStringCommand(sqlStatement.ToString());
            db.AddInParameter(dbCommand, "@sn", DbType.Int64, Student.Sn);
            db.AddInParameter(dbCommand, "@name", DbType.String, Student.Name);
            db.AddInParameter(dbCommand, "@class_id", DbType.String, Student.ClassId);
            db.AddInParameter(dbCommand, "@hight", DbType.Int64, Student.Hight);
            db.AddInParameter(dbCommand, "@weight", DbType.Double, Student.Weight);
            if (Student.Birthday.Year != 1) { db.AddInParameter(dbCommand, "@birthday", DbType.DateTime, Student.Birthday); } else { db.AddInParameter(dbCommand, "@birthday", DbType.DateTime, DBNull.Value); }
            db.AddInParameter(dbCommand, "@photo", DbType.String, Student.Photo);
            db.AddInParameter(dbCommand, "@memo", DbType.String, Student.Memo);

            bool result = false;
            try
            {
                db.ExecuteNonQuery(dbCommand);
                result = true;
            }
            catch (DbException ex)
            {
                LogController.WriteLog("StudentDB.Update", ex.ToString());
                throw (ex);
            }
            return result;
        }

        /// <summary>
        /// 刪除Student某筆資料
        /// </summary>
        /// <param name="Sn">
        /// 流水號
        /// </param>
        /// <returns>Boolean</returns>
        public bool Del(int Sn)
        {
            Database db = DatabaseFactory.CreateDatabase();

            StringBuilder sqlStatement = new StringBuilder();
            sqlStatement.Append("DELETE FROM student WHERE sn = @sn");

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
                LogController.WriteLog("StudentDB.Del", ex.ToString());
                throw (ex);
            }
            return result;
        }

        private IList<StudentInfo> GetList(Database db, DbCommand dbCommand)
        {
            IList<StudentInfo> myList = new List<StudentInfo>();

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
                LogController.WriteLog("StudentDB.GetList", ex.ToString());
                throw (ex);
            }
            return myList;
        }

        private StudentInfo BindInfo(IDataReader dr)
        {
            StudentInfo myInfo = new StudentInfo();

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
                if (!dr["class_id"].Equals(DBNull.Value))
                {
                    myInfo.ClassId = dr["class_id"].ToString();
                }
                if (!dr["hight"].Equals(DBNull.Value))
                {
                    myInfo.Hight = int.Parse(dr["hight"].ToString());
                }
                if (!dr["weight"].Equals(DBNull.Value))
                {
                    myInfo.Weight = double.Parse(dr["weight"].ToString());
                }
                if (!dr["birthday"].Equals(DBNull.Value))
                {
                    myInfo.Birthday = (DateTime)dr["birthday"];
                }
                if (!dr["photo"].Equals(DBNull.Value))
                {
                    myInfo.Photo = dr["photo"].ToString();
                }
                if (!dr["memo"].Equals(DBNull.Value))
                {
                    myInfo.Memo = dr["memo"].ToString();
                }
            }
            catch (Exception ex)
            {
                LogController.WriteLog("StudentDB.BindInfo", ex.ToString());
                throw (ex);
            }
            return myInfo;
        }
    }
}
