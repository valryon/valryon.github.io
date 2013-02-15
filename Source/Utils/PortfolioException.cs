using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portfolio.Utils
{
    [Serializable]
    public class PortfolioException : Exception
    {
        public PortfolioException() { }
        public PortfolioException(string message) : base(message) { }
        public PortfolioException(string message, Exception inner) : base(message, inner) { }
        protected PortfolioException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
