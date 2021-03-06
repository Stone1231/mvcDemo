using System;
using System.Collections.Generic;
using Information;
using DataAccess;
namespace Business
{
    /// <summary>
    /// 學生社團關聯
    /// </summary>
    public partial class StudClubBiz
    {
        private static StudClubDB myDB = new StudClubDB();

        /// <summary>
        /// 判斷StudClub此筆資料是否存在
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool Exists(int StudId, int ClubId)
        {
            return myDB.Exists(StudId, ClubId);
        }

        /// <summary>
        /// 讀取全部資料
        /// </summary>
        /// <returns>StudClubInfo的泛型集合</returns>
        public static IList<StudClubInfo> GetAll()
        {
            return myDB.GetAll();
        }

        /// <summary>
        /// 讀取StudClub1筆資料
        /// </summary>
        /// <returns>StudClubInfo</returns>
        public static StudClubInfo GetInfo(int StudId, int ClubId)
        {
            return myDB.GetInfo(StudId, ClubId);
        }

        /// <summary>
        /// 新增StudClub1筆資料
        /// </summary>
        /// <param name="StudClub">
        /// StudClub的新資料
        /// </param>
        /// <returns>Boolean</returns>
        public static bool AddNew(StudClubInfo StudClub)
        {
            return myDB.AddNew(StudClub);
        }

        /// <summary>
        /// 更新StudClub某筆資料
        /// </summary>
        /// <param name="StudClub">
        /// StudClub的某筆資料
        /// </param>
        /// <returns>Boolean</returns>
        public static bool Update(StudClubInfo StudClub)
        {
            return myDB.Update(StudClub);
        }

        /// <summary>
        /// 刪除StudClub某筆資料
        /// </summary>
        /// <returns>Boolean</returns>
        public static bool Del(int StudId, int ClubId)
        {
            return myDB.Del(StudId, ClubId);
        }
    }
}
