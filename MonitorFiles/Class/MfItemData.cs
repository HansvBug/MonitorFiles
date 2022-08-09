namespace MonitorFiles.Class
{
    public class MfItemData
    {
        public int ID { get; set; }
        public string FILE_NAME { get; set; }
        public string FOLDER_NAME { get; set; }
        public int SOURCE_ID { get; set; }
        public int TONWSHIP_ID { get; set; }

        private string ConcatItemData { get; set; }

        public MfItemData()
        {
            FILE_NAME = string.Empty;
            FOLDER_NAME = string.Empty;
            SOURCE_ID = -1;
            TONWSHIP_ID = -1;
            ConcatItemData = string.Empty;
        }

        public string Concat()
        {
            ConcatItemData = FILE_NAME + ";" + FOLDER_NAME + ";" + SOURCE_ID.ToString() + ";" + TONWSHIP_ID.ToString();
            return ConcatItemData;
        }
    }
}
