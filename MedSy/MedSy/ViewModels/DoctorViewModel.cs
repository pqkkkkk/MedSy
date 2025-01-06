using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedSy.Models;
using MedSy.Services.Feedback;
using MedSy.Services.User;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Devices.AllJoyn;
namespace MedSy.ViewModels;
public partial class DoctorViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Models.Doctor> Doctors { get; set;}
    public ObservableCollection<Models.Feedback> Feedbacks { get; set;}
    public ObservableCollection<PageInfo> PageInfos { get; set; }
    public int SelectedPageIndex { get; set; }
    public string Keyword { get; set; } = "";

    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
    public int RowsPerPage { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
    public DoctorViewModel()
    {
        RowsPerPage = 9;
        CurrentPage = 1;
        LoadData();
        LoadUniqueValues();
    }

    public string SelectedSpecialty { get; set; }
    public string SelectedYearExperience { get; set; }
    public Doctor SelectedDoctor { get; set; }

    public void LoadData()
    {

        IUserDao userDao = (Application.Current as App).locator.userDao;
        
        string genderFilter = SelectedGender == "All" ? null : SelectedGender;
        string specialtyFilter = SelectedSpecialty == "All" ? null : SelectedSpecialty;
        int yearFilter = (SelectedYearExperience == "All" || string.IsNullOrEmpty(SelectedYearExperience)) ? -1: int.Parse(SelectedYearExperience);
        var (items,count) = userDao.GetDoctors(
            CurrentPage, RowsPerPage, Keyword, specialtyFilter, genderFilter, yearFilter);

        Doctors = new ObservableCollection<Doctor>(
            items);

        TotalItems = count;
        TotalPages = (TotalItems / RowsPerPage) +
            (((TotalItems % RowsPerPage) == 0) ? 0 : 1);

        SelectedPageIndex = CurrentPage - 1;
        CreatePageInfos();
        Feedbacks = new ObservableCollection<Feedback>();
    }

    public void LoadFeedback()
    {
        IFeedbackDao fb_dao = new FeedbackSqlDao();
        Feedbacks = new ObservableCollection<Feedback>(fb_dao.GetFeedback());
    }

    public void AddFeedback(Feedback feedback)
    {
        IFeedbackDao fb_dao = new FeedbackSqlDao();
        fb_dao.AddFeedback(feedback);
        LoadFeedback();
    }

    public void resetsort()
    {
        SelectedGender = null;
        SelectedSpecialty = null;
        Keyword= "";
        SelectedYearExperience = null;
        LoadData();
    }
    public void reset()
    {
        Feedbacks.Clear();
    }

    private void CreatePageInfos()
    {
        PageInfos = new ObservableCollection<PageInfo>();
        for (int i = 1; i <= TotalPages; i++)
        {
            PageInfos.Add(new PageInfo
            {
                Page = i,
                Total = TotalPages
            });
        }
    }

    // SORT
    // Gender value for sort
    public List<string> Genders { get; set; } = new List<string> { "Male", "Female", "All" };
    public string SelectedGender { get; set; }
    // Load unique value for experience Year and Specialty
    public List<string> UniqueSpecialties { get; private set; }
    public List<string> UniqueExperienceYears { get; private set; }

    private void LoadUniqueValues()
    {
        UniqueSpecialties = Doctors.Select(d => d.speciality).Distinct().ToList();
        UniqueSpecialties.Add("All");
        UniqueExperienceYears = Doctors.Select(d => d.experienceYear.ToString()).Distinct().OrderBy(y => y).ToList();
        UniqueExperienceYears.Add("All");
    }


    // GO TO PAGE
    public void GoToPage(int page)
    {
        CurrentPage = page;
        LoadData();
    }

    public void GoToPreviousPage()
    {
        if (CurrentPage > 1)
        {
            CurrentPage--;
            LoadData();
        }
    }
    public void GoToNextPage()
    {
        if (CurrentPage < TotalPages)
        {
            CurrentPage++;
            LoadData();
        }
    }
    //SEARCH
    public void Search()
    {
        CurrentPage = 1;
        LoadData();
    }
    public List<string> GetSuggestions(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return new List<string>();

        return Doctors
            .Where(d => d.fullName.Contains(query, StringComparison.OrdinalIgnoreCase))
            .Select(d => d.fullName)
            .ToList();
    }

}

