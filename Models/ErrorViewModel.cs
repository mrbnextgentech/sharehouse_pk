namespace Gharshare.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public string? ErrorMessage { get; set; }
        public string? ErrorSource { get; set; }
        public string? StackTrace { get; set; }
        private bool _showRequestId = true;
        //public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public bool ShowRequestId
        {
            get { return _showRequestId; }
            set { _showRequestId = value; }
        }
    }
}