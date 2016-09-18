using KeyGeneratorService;

namespace KeyGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            AuthenticationValidator authenticationValidator = new AuthenticationValidator();
            bool isSuccess = authenticationValidator.GenerateSubscriptionKey("hansamali", 1111);
            bool isValid = authenticationValidator.ValidateSubscriptionKey("hansamali", 1111);

        }
    }
}
