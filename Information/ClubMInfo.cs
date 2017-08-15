using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Information
{
    /// <summary>
    /// 社團
    /// </summary>
    public partial class ClubMInfo
    {

        /// <summary>
        /// 流水號
        /// </summary>
        [DisplayName("流水號")]
        public int Sn{ get; set; }

        /// <summary>
        /// 社團名
        /// </summary>
        [DisplayName("社團名")]
        public string Name{ get; set; }
    }
}
