using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedSy.Services.Feedback
{
    public class FeedbackMockDao:IFeedbackDao
    {
        public void AddFeedback(Models.Feedback feedback)
        {
            throw new NotImplementedException();
        }

        public List<Models.Feedback> GetFeedback(/*string PatientID*/)
        {
            var Feedbacks = new List<Models.Feedback>()
            {
                 new Models.Feedback
                {
                    DoctorID = 1,
                    PatientID = 1,
                    Content = "Good!",
                    Rating = 5,
                },
                new Models.Feedback
                {
                    DoctorID = 1,
                    PatientID = 2,
                    Content = "So Good!",
                    Rating = 4,
                },
                new Models.Feedback
                {
                    DoctorID = 2,
                    PatientID = 3,
                    Content = "Excellent service, highly recommend!",
                    Rating = 5,
                },
                new Models.Feedback
                {
                    DoctorID = 2,
                    PatientID = 4,
                    Content = "Very knowledgeable and caring.",
                    Rating = 4,
                },
                new Models.Feedback
                {
                    DoctorID = 3,
                    PatientID = 5,
                    Content = "Wait time was a bit long, but the doctor was great.",
                    Rating = 3,
                },
                new Models.Feedback
                {
                    DoctorID = 3,
                    PatientID = 6,
                    Content = "I felt very comfortable and well taken care of.",
                    Rating = 5,
                },
                new Models.Feedback
                {
                    DoctorID = 4,
                    PatientID = 7,
                    Content = "Not satisfied with the treatment.",
                    Rating = 2,
                },
                new Models.Feedback
                {
                    DoctorID = 4,
                    PatientID = 8,
                    Content = "The doctor was okay, but the staff was rude.",
                    Rating = 3,
                },
                new Models.Feedback
                {
                    DoctorID = 5,
                    PatientID = 9,
                    Content = "Amazing experience, will definitely return.",
                    Rating = 5,
                },
                new Models.Feedback
                {
                    DoctorID = 5,
                    PatientID = 10,
                    Content = "Good consultation, but I expected more.",
                    Rating = 4,
                },
                new Models.Feedback
                {
                    DoctorID = 1,
                    PatientID = 11,
                    Content = "Professional and polite.",
                    Rating = 5,
                },
                new Models.Feedback
                {
                    DoctorID = 2,
                    PatientID = 12,
                    Content = "Very helpful and attentive.",
                    Rating = 4,
                },
                new Models.Feedback
                {
                    DoctorID = 3,
                    PatientID = 13,
                    Content = "The best doctor I have seen.",
                    Rating = 5,
                },
                new Models.Feedback
                {
                    DoctorID = 4,
                    PatientID = 14,
                    Content = "Could improve on patient interaction.",
                    Rating = 3,
                },
                new Models.Feedback
                {
                    DoctorID = 5,
                    PatientID = 15,
                    Content = "Great advice, really helped me out!",
                    Rating = 5,
                }, 
            };
            return Feedbacks;
        }
         
    }
}
