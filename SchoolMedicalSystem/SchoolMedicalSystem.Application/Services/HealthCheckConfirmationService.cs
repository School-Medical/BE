using SchoolMedicalSystem.Application.Interfaces.IReposervices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMedicalSystem.Application.Services
{
    public class HealthCheckConfirmationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public HealthCheckConfirmationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

    }
}
