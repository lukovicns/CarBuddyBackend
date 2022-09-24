using System;

namespace CarBuddy.Application.Exceptions
{
    public class EntityAlreadyExists : Exception
    {
        public EntityAlreadyExists(string name) : base($"{name} already exists.")
        {
        }
    }
}
