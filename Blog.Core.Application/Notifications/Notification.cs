namespace Blog.Core.Application.Notifications
{
    public class Notification<T>
    {

        public T Data { get; init; }
        private List<string> ListErrors { get; init; } = new List<string>();
        public IReadOnlyCollection<string> Notifications => ListErrors;


        public Notification(T data, List<string> errors)
        {
            Data = data;
            ListErrors = errors;
        }
        public Notification(T data)
        {
            Data = data;
        }      

        public Notification(List<string> errors)
        {
            ListErrors = errors;
        }
        public Notification(string error)
        {
            ListErrors.Add(error);
        }

      
    }
}
