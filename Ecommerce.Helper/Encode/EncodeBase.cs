using System.Text;

namespace Ecommerce.Helper.Encode
{
    public class EncodeBase
    {
        public static string EncodeBase64(string value)
        {
            var valueBytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(valueBytes);
        }
    }
}
