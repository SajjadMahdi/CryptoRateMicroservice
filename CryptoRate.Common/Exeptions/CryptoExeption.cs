using System;
using System.Collections.Generic;
using System.Text;

namespace CryptoRate.Common.Exeptions
{
   public class CryptoExeption:Exception
    {
        public string Code { get; set; }

        public CryptoExeption()
        {
            
        }

        public CryptoExeption(string code)
        {
            this.Code = code;
        }

        public CryptoExeption(string message,params object[] args):this(string.Empty,message,args)
        {
            
        }
        public CryptoExeption(string code,string message, params object[] args) : this(null,code, message, args)
        {

        }
        public CryptoExeption(Exception innerException , string message, params object[] args) 
            : this(innerException,string.Empty, message, args)
        {

        }
        public CryptoExeption(Exception innerException,string code, string message, params object[] args)
            : base(string.Format(message,args),innerException)
        {
            Code = code;
        }

    }
}
