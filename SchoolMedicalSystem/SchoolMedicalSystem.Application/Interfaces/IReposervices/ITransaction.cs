﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Interfaces.IReposervices
{
    public interface ITransaction: IAsyncDisposable
    {
        Task CommitAsync();
        Task RollbackAsync();
    }
}
