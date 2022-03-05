using System.Text;

namespace Ecommerce.Helper.Decode
{
    public class DecodeBase
    {
        public static string DecodeBase64(string value)
        {
            var valueBytes = Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(valueBytes);
        }
    }
}
