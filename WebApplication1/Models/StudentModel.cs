using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Information;
namespace WebApplication1
{
    /// <summary>
    /// 學生
    /// </summary>
    public partial class StudentModel
    {
        public StudentInfo Info { get; set; }

        public IEnumerable<SelectListItem> ClassIdSelect { get; set; }

        public HttpPostedFileBase PhotoFile { get; set; }
    }

    public class StudentListModel
    {
        [DisplayName("名稱")]
        public string Name { get; set; }

        [DisplayName("班級")]
        public string ClassId { get; set; }
        public IEnumerable<SelectListItem> ClassIdSelect { get; set; }

        public MvcPaging.IPagedList<StudentInfo> List { get; set; }
        //public IList<StudentInfo> List { get; set; }
    }
}
