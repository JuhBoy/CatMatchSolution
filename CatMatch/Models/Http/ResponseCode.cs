namespace CatMatch.Http.Models
{
    internal static class ResponseCode
    {
        internal static readonly int Success = 200;
        internal static readonly int InvalidRequest = 400;
        internal static readonly int DatabaseNotFound = 404;
        internal static readonly int UnknownError = 500;
        internal static readonly int UnavailableService = 300;
        internal static readonly int MatchDispersion = 501;
    }
}
