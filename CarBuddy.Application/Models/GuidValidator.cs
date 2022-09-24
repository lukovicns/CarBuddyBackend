using System;

namespace CarBuddy.Application.Models
{
    public class GuidValidator
    {
        public Guid Value { get; private set; }
        public bool IsValid { get; private set; }

        public GuidValidator(string value)
        {
            IsValid = Guid.TryParse(value, out var validValue);

            if (!IsValid)
                return;

            Value = validValue;
        }
    }
}
