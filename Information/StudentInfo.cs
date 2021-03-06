using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Information
{
    /// <summary>
    /// 學生
    /// </summary>
    public partial class StudentInfo
    {

        /// <summary>
        /// 流水號
        /// </summary>
        [DisplayName("流水號")]
        public int Sn{ get; set; }

        /// <summary>
        /// 名子
        /// </summary>
        [DisplayName("名子")]
        public string Name{ get; set; }

        /// <summary>
        /// 班級代碼
        /// </summary>
        [DisplayName("班級代碼")]
        public string ClassId{ get; set; }

        /// <summary>
        /// 身高
        /// </summary>
        [DisplayName("身高")]
        public int Hight{ get; set; }

        /// <summary>
        /// 體重
        /// </summary>
        [DisplayName("體重")]
        public double Weight{ get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [DisplayName("生日")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Birthday{ get; set; }

        /// <summary>
        /// 大頭照
        /// </summary>
        [DisplayName("大頭照")]
        public string Photo{ get; set; }

        /// <summary>
        /// 備註
        /// </summary>
        [DisplayName("備註")]
        public string Memo{ get; set; }
    }
}
