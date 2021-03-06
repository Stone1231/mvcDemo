using System;
using System.Collections.Generic;
using Information;
using DataAccess;
namespace Business
{
    /// <summary>
    /// 班級
    /// </summary>
    public partial class ClassMBiz
    {
        private static ClassMDB myDB = new ClassMDB();

        /// <summary>
        /// 判斷ClassM此筆資料是否存在
        /// </summary>
        /// <param name="Id">
        /// 班級代碼
        /// </param>
        /// <returns>Boolean</returns>
        public static bool Exists(string Id)
        {
            return myDB.Exists(Id);
        }

        /// <summary>
        /// 讀取全部資料
        /// </summary>
        /// <returns>ClassMInfo的泛型集合</returns>
        public static IList<ClassMInfo> GetAll()
        {
            return myDB.GetAll();
        }

        /// <summary>
        /// 讀取ClassM1筆資料
        /// </summary>
        /// <param name="Id">
        /// 班級代碼
        /// </param>
        /// <returns>ClassMInfo</returns>
        public static ClassMInfo GetInfo(string Id)
        {
            return myDB.GetInfo(Id);
        }

        /// <summary>
        /// 新增ClassM1筆資料
        /// </summary>
        /// <param name="ClassM">
        /// ClassM的新資料
        /// </param>
        /// <returns>Boolean</returns>
        public static bool AddNew(ClassMInfo ClassM)
        {
            return myDB.AddNew(ClassM);
        }

        /// <summary>
        /// 更新ClassM某筆資料
        /// </summary>
        /// <param name="ClassM">
        /// ClassM的某筆資料
        /// </param>
        /// <returns>Boolean</returns>
        public static bool Update(ClassMInfo ClassM)
        {
            return myDB.Update(ClassM);
        }

        /// <summary>
        /// 刪除ClassM某筆資料
        /// </summary>
        /// <param name="Id">
        /// 班級代碼
        /// </param>
        /// <returns>Boolean</returns>
        public static bool Del(string Id)
        {
            return myDB.Del(Id);
        }
    }
}
