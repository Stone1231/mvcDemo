using System;
using System.Collections.Generic;
using Information;
using DataAccess;
namespace Business
{
    /// <summary>
    /// 學生
    /// </summary>
    public partial class StudentBiz
    {
        private static StudentDB myDB = new StudentDB();

        /// <param name="ClassId">
        /// 班級代碼
        /// </param>
        /// <returns>StudentInfo的泛型集合</returns>
        public static IList<StudentInfo> ClassData(string ClassId)
        {
            return myDB.ClassData(ClassId);
        }

        /// <returns>StudentInfo的泛型集合</returns>
        public static IList<StudentInfo> ClubData(int ClubId)
        {
            return myDB.ClubData(ClubId);
        }

        /// <param name="ClassId">
        /// 班級代碼
        /// </param>
        /// <returns>StudentInfo的泛型集合</returns>
        public static IList<StudentInfo> QueryData(string ClassId, string Name)
        {
            return myDB.QueryData(ClassId, Name);
        }

        /// <summary>
        /// 判斷Student此筆資料是否存在
        /// </summary>
        /// <param name="Sn">
        /// 流水號
        /// </param>
        /// <returns>Boolean</returns>
        public static bool Exists(int Sn)
        {
            return myDB.Exists(Sn);
        }

        /// <summary>
        /// 讀取全部資料
        /// </summary>
        /// <returns>StudentInfo的泛型集合</returns>
        public static IList<StudentInfo> GetAll()
        {
            return myDB.GetAll();
        }

        /// <summary>
        /// 讀取Student1筆資料
        /// </summary>
        /// <param name="Sn">
        /// 流水號
        /// </param>
        /// <returns>StudentInfo</returns>
        public static StudentInfo GetInfo(int Sn)
        {
            return myDB.GetInfo(Sn);
        }

        /// <summary>
        /// 新增Student1筆資料
        /// </summary>
        /// <param name="Student">
        /// Student的新資料
        /// </param>
        /// <returns>Boolean</returns>
        public static bool AddNew(StudentInfo Student)
        {
            return myDB.AddNew(Student);
        }

        /// <summary>
        /// 更新Student某筆資料
        /// </summary>
        /// <param name="Student">
        /// Student的某筆資料
        /// </param>
        /// <returns>Boolean</returns>
        public static bool Update(StudentInfo Student)
        {
            return myDB.Update(Student);
        }

        /// <summary>
        /// 刪除Student某筆資料
        /// </summary>
        /// <param name="Sn">
        /// 流水號
        /// </param>
        /// <returns>Boolean</returns>
        public static bool Del(int Sn)
        {
            return myDB.Del(Sn);
        }
    }
}
