using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Origin.YMC.Business.Entities.Domain.Doctors.ViewModel;

namespace Origin.YMC.Business.Entities.Domain.Interpreter.ViewModels
{
    public class InterpreterViewModels
    {
        public decimal fees;

        #region Doctor  
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public int GenderId { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
        #endregion

        public Guid ApplicationUserId { get; set; }
        public bool IsActive { get; set; }
        public string Password { get; set; }

        public List<string> Certificates { get; set; }
        public string Bio { get; set; }
        public string ProfileImage { get; set; }
        public DateTime CreationDate { get; set; }
        //public Guid SpecialtyId { get; set; }
        public string SpecialtyName { get; set; }
    }

    public class ListInterpreterViewModel
    {
        public List<InterpreterViewModels> Items { get; set; }
        public int TotalDatalenght { get; set; }
        public int FilterDatalenght { get; set; }
        public ListInterpreterViewModel()
        {
            Items = new List<InterpreterViewModels>();
        }
    }
}
