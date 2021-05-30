namespace MVM.Domain.Models
{
    public class MessageModel<T> where T : class
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public T Data { get; set; }
    }
}
