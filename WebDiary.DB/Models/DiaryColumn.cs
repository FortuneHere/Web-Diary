namespace WebDiary.DB.Models
{
    // public class DiaryColumn //Столбец дневника
    // {
    //     [Key] public int ClmnId { get; set; }
    //
    //     public int SbjctId { get; set; }
    //
    //     public int GrpId { get; set; }
    //     public int TeacherId { get; set; }
    //
    //     [DataType(DataType.Date)]
    //     [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
    //     public DateTime EventDate { get; set; }
    //
    //
    //     public virtual ICollection<DiaryCell> DiaryCells { get; set; } //отправил Icollection в DiaryColumn
    //
    //
    //     [ForeignKey("SbjctId")] public virtual Course Course { get; set; }
    //
    //     [ForeignKey("GrpId")] public virtual Group Group { get; set; }
    //
    //     [ForeignKey("TeacherId")] public virtual Teacher Teacher { get; set; }
    // }
}