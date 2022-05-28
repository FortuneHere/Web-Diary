namespace WebDiary.ViewModels
{
    public class SchedulerItemModel
    {
        public long id { get; set; }

        public string text { get; set; }

        public string start_date { get; set; }

        public string end_date { get; set; }

        public long? room_id { get; set; }

        public long? user_id { get; set; }
    }
}