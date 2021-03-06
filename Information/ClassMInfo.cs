using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Information
{
    /// <summary>
    /// 班級
    /// </summary>
    public partial class ClassMInfo
    {

        /// <summary>
        /// 班級代碼
        /// </summary>
        [DisplayName("班級代碼")]
        public string Id{ get; set; }

        /// <summary>
        /// 班級名
        /// </summary>
        [DisplayName("班級名")]
        public string Name{ get; set; }
    }
}
