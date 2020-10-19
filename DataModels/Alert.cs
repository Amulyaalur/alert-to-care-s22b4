namespace DataModels
{
    public class Alert
    {
        public string AlertId { get; set; }
        public string LayoutId { get; set; }
        public string IcuId { get; set; }
        public string PatientId { get; set; }
        public bool AlertStatus { get; set; }

    }
}