using System;
using System.Collections.Generic;
using System.Text;

namespace Meli.ApiRestDNA.Shared.Exceptions
{
    public abstract class BusinessException : Exception
    {
        protected BusinessException(string strDetails)
            : base(strDetails)
        { }

        public abstract int ErrorCode { get; }
    }
}
