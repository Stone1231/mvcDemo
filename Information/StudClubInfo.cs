using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Information
{
    /// <summary>
    /// 學生社團關聯
    /// </summary>
    public partial class StudClubInfo
    {

        public int StudId{ get; set; }

        public int ClubId{ get; set; }
    }
}
