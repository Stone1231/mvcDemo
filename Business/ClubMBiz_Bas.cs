using System;
using System.Collections.Generic;
using Information;
using DataAccess;
namespace Business
{
    /// <summary>
    /// 社團
    /// </summary>
    public partial class ClubMBiz
    {
        private static ClubMDB myDB = new ClubMDB();

        /// <returns>ClubMInfo的泛型集合</returns>
        public static IList<ClubMInfo> StudData(int StudId)
        {
            return myDB.StudData(StudId);
        }

        /// <summary>
        /// 判斷ClubM此筆資料是否存在
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
        /// <returns>ClubMInfo的泛型集合</returns>
        public static IList<ClubMInfo> GetAll()
        {
            return myDB.GetAll();
        }

        /// <summary>
        /// 讀取ClubM1筆資料
        /// </summary>
        /// <param name="Sn">
        /// 流水號
        /// </param>
        /// <returns>ClubMInfo</returns>
        public static ClubMInfo GetInfo(int Sn)
        {
            return myDB.GetInfo(Sn);
        }

        /// <summary>
        /// 新增ClubM1筆資料
        /// </summary>
        /// <param name="ClubM">
        /// ClubM的新資料
        /// </param>
        /// <returns>Boolean</returns>
        public static bool AddNew(ClubMInfo ClubM)
        {
            return myDB.AddNew(ClubM);
        }

        /// <summary>
        /// 更新ClubM某筆資料
        /// </summary>
        /// <param name="ClubM">
        /// ClubM的某筆資料
        /// </param>
        /// <returns>Boolean</returns>
        public static bool Update(ClubMInfo ClubM)
        {
            return myDB.Update(ClubM);
        }

        /// <summary>
        /// 刪除ClubM某筆資料
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
