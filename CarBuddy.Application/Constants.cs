namespace CarBuddy.Application
{
    public class Constants
    {
        public const string UserAlreadyExists = "User already exists.";
        public const string UserNotFound = "User not found.";
        public const string UserNotActivated = "User is not activated yet.";
        public const string UserIsAlreadyActivated = "User is already activated.";
        public const string UserNotADriver = "User is not a driver.";
        public const string UserAlreadyHasACar = "This user already has a car.";
        public const string UserIsNotADriver = "This user is not a driver.";

        public const string TripNotFound = "Trip not found.";
        public const string TripAlreadyExists = "This trip already exists.";
        public const string TripRatingSuccess = "Trip successfully rated.";
        public const string TripDeletionSuccess = "Trip has been successfully deleted!";
        public const string NoAvailableSeats = "There are no available seats left.";

        public const string TripRequestSentSuccessfully = "Request for trip is successfully sent!";
        public const string TripRequestAccepted = "Request accepted!";
        public const string TripRequestDeclined = "Request declined!";

        public const string PassengerNotFound = "Passenger not found.";
        public const string NoPassengerSeatsLeft = "There are no passenger seats left for this trip.";
        public const string PassengerAlreadyGaveRating = "You already rated this driver for this trip.";

        public const string DriverNotFound = "Driver not found.";
        public const string CannotRateDriver = "Cannot rate driver.";

        public const string CarNotFound = "Car not found.";

        public const string InvalidIdProvided = "Invalid id provided.";
        public const string DateIsLessThanToday = "Date cannot be less than today";
        public const string DateIsMoreThanTwoWeeks = "Date cannot be more than 14 days.";
        public const string EmailRequired = "Email address is required.";
        public const string EmailInRange = "Email must contain between 2 and 128 characters";
        public const string InvalidEmail = "Email is invalid.";
        public const string InvalidPassword = "Password is invalid.";
        public const string EmailConfirmationTokenIsInvalid = "Email confirmation failed. Token is invalid.";
        public const string ConversationNotFound = "Conversation not found.";
    }
}
