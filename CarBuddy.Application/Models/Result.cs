namespace CarBuddy.Application.Models
{
    public class Result
    {
        public object Content { get; private set; }
        public int StatusCode { get; private set; }
        public bool HasError => StatusCode >= 400;
        public string Message => Content.ToString();

        public Result(object content, int statusCode)
        {
            Content = content;
            StatusCode = statusCode;
        }

        public object GetProperty(string name)
        {
            return Content.GetType().GetProperty(name)?.GetValue(Content);
        }
    }
}
