﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kcots.Interfaces
{
    public interface ILoggingWrapper
    {
        void InitializeLogger();
        void LogInformation(string message);
        void LogError(Exception ex, string message);
    }

}
