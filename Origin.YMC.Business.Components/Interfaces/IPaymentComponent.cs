using Origin.YMC.Business.Entities.Domain.Payments.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Origin.YMC.Business.Components.Interfaces
{
    public interface IPaymentComponent
    {
        Guid? Add(PaymentViewModel payment);
        Guid? UpdatePatientPayment(PaymentViewModel payment,Guid Id);
    }
}
