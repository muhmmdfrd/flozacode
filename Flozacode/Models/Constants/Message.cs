namespace Flozacode.Models.Constants
{
    public static class Message
    {
        private const string CRUD_BASE_MESSAGE = "Error while";

        // CRUD error message
        public const string ERROR_CREATE = $"{CRUD_BASE_MESSAGE} inserting data.";
        public const string ERROR_UPDATE = $"{CRUD_BASE_MESSAGE} updating data.";
        public const string ERROR_DELETE = $"{CRUD_BASE_MESSAGE} deleting data.";

        // Not Found data
        public const string ERROR_DATA_ID_NOT_FOUND = "Data with ID: {0} not found.";
    }
}
