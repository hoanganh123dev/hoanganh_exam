namespace Identity.API
{
    public class AppSettings
    {
        //3 client => 3 key
        public string ExamWebAppClient { set; get; }
        public string ExamWebAdminClient { set; get; }
        public string ExamWebApiClient { set; get; }
    }
}