namespace DataAccessLayer.DataModel.Test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CourseSession")]
    public partial class CourseSession
    {
        public long Id { get; set; }

        public long? CourseID { get; set; }

        public string Topic { get; set; }

        public string Document1 { get; set; }

        public string Document2 { get; set; }

        public string AudioLink { get; set; }

        public string VideoLink { get; set; }

        public DateTime? CreatedOn { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime? LastSendDate { get; set; }

        public virtual Course Course { get; set; }
    }
}
