using RandomNameGeneratorLibrary;

namespace SignalRStocks.Contract.User
{
    public class UserService
    {
        static readonly PersonNameGenerator personNameGenerator = new PersonNameGenerator();

        public string GetUserIdentifier()
        {
            // This might, for example, be a login service or something
            // that may return a bearer token.
            // We're just using a random name
            return personNameGenerator.GenerateRandomFirstAndLastName();
        }
    }
}
