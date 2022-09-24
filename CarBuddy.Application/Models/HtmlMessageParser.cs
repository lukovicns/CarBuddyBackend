using System.IO;

namespace CarBuddy.Application.Models
{
    public class HtmlMessageParser
    {
        private readonly string _filePath;
        private readonly string _recipient;
        private readonly string _url;

        public HtmlMessageParser(string filePath, string recipient, string url)
        {
            _filePath = filePath;
            _recipient = recipient;
            _url = url;
        }

        public string CreateHtmlBody()
        {
            string body = string.Empty;

            using (var reader = new StreamReader(_filePath))
                body = reader.ReadToEnd();

            body = body.Replace("{Email}", _recipient);
            body = body.Replace("{Url}", _url);
            body = body.Replace("{ButtonText}", "Click here to confirm");
            return body;
        }
    }
}
