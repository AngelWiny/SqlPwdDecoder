using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlPwdDecoder
{
    public class PwdCodder
    {
        public PwdCodder()
        {

        }

        public string CodePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return string.Empty;

            string result = "";
            var bytes = Encoding.ASCII.GetBytes(password.Reverse().ToArray()).Select(x => x.ToString());

            foreach (var digit in bytes)
            {
                if (digit.Length == 2)
                {
                    result += "4";
                    result += digit;
                }
                else
                {
                    result += "6";
                    result += digit;
                }
            }
            return result;
        }

        public string DecodePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return string.Empty;

            List<byte> partes = new List<byte>();
            while (!string.IsNullOrWhiteSpace(password))
            {
                if (password.First() == '4')
                {
                    partes.Add(byte.Parse(password.Substring(1, 2)));
                    password = password.Remove(0, 3);
                }
                else if (password.First() == '6')
                {
                    partes.Add(byte.Parse(password.Substring(1, 3)));
                    password = password.Remove(0, 4);
                }
                else
                {
                    throw new ArgumentException("password is not in the correct format");
                }
            }

            string result = Encoding.ASCII.GetString(partes.ToArray());
            return new string(result.Reverse().ToArray());
        }
    }
}
