using System;
using System.Collections.Generic;
using System.Text;

namespace AdminDashboard.Shared.Utilities.Results.ComplexTypes
{
    public enum ResultStatus
    {
        Success = 0,
        Error = 1,
        Warning = 2, // ResultStatus.Warning
        Info = 3 // ResultStatus.Info
    }
}
