namespace CuentasAhorro.Services.Helpers
{
    public class GeneralHelper
    {
        public static readonly TimeZoneInfo TimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");

        public static string AccountNumber()
        {
            Random random = new Random();
            string accoundNumber = "CA";

            for (int i = 0; i < 14; i++)
            {
                int number = random.Next(10);

                accoundNumber += $"{number}";
            }

            return accoundNumber;
        }
    }
}