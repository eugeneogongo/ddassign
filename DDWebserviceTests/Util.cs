using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDWebserviceTests
{
   public class Util
    {
        /// <summary>
        /// Generates Random Characters
        /// </summary>
        /// <returns></returns>
        public static string GenerateRandomChar()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8);
        }
    }

}
