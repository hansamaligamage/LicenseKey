using KeyGeneratorService;

namespace KeyGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isValid;
            AuthenticationValidator authenticationValidator = new AuthenticationValidator();
            bool isSuccess = authenticationValidator.GenerateSubscriptionKey("hansamali", 1111);
            //valid scenario
            isValid = authenticationValidator.ValidateSubscriptionKey("hansamali", 1111);
            //invalid scenario
            isValid = authenticationValidator.ValidateSubscriptionKey("hansamali", 1112);

        }
    }
}
