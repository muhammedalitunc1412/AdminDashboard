using System;
using System.Collections.Generic;
using System.Text;

namespace AdminDashboard.Shared.Utilities.Results.Abstract
{
    public interface IDataResult<out T> : IResult
    {
        public T Data { get; } 
                          
    }
}
