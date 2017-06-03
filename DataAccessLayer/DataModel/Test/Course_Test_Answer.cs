namespace DataAccessLayer.DataModel.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Course_Test_Answer
    {
        public long Id { get; set; }

        public long? CourseID { get; set; }

        public long? CourseTestID { get; set; }

        [StringLength(50)]
        public string Answer { get; set; }

        public bool? IsCorrect { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? UserID { get; set; }

        public virtual User User {get;set;}
    }
}
