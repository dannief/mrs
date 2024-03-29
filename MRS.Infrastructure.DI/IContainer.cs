﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MRS.Infrastructure.DI
{
    public interface IContainer
    {
        T GetInstance<T>() where T : class;
    }
}
