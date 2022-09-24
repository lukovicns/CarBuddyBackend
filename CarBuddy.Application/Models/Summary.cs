namespace CarBuddy.Application.Models
{
    public class Summary
    {
        public string Value { get; private set; }

        public Summary(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value))
            {
                Value = value;
                return;
            }

            if (value.Length <= maxLength)
            {
                Value = value;
                return;
            }

            Value = $"{value.Substring(0, maxLength)}...";
        }

        public static implicit operator string(Summary summary) => summary.Value;
    }
}
