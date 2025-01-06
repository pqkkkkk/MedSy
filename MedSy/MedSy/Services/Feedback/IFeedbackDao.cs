using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MedSy.Services.Feedback
{
    public interface IFeedbackDao
    {
        public List<Models.Feedback> GetFeedback();
        public void AddFeedback(Models.Feedback feedback);
    }
}
