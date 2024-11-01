using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedSy.Models;
using Microsoft.UI.Xaml.Controls;
namespace MedSy.ViewModels;
public partial class DoctorViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Doctor> Doctors { get; set;}

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
    public int SelectedYearExperience { get; set; }
    public Doctor SelectedDoctor { get; set; }

    public void LoadData()
    {
        IDoctorDao doctor_dao = new DoctorMockDao();
        var (items,count) = doctor_dao.GetDoctors(
            CurrentPage, RowsPerPage, Keyword, SelectedSpecialty, SelectedGender, SelectedYearExperience);

        Doctors = new ObservableCollection<Doctor>(
            items);

        TotalItems = count;
        TotalPages = (TotalItems / RowsPerPage) +
            (((TotalItems % RowsPerPage) == 0) ? 0 : 1);

        SelectedPageIndex = CurrentPage - 1;
        CreatePageInfos();
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
    public List<string> Genders { get; set; } = new List<string> { "Male", "Female" };
    public string SelectedGender { get; set; }
    // Load unique value for experience Year and Specialty
    public List<string> UniqueSpecialties { get; private set; }
    public List<int> UniqueExperienceYears { get; private set; }

    private void LoadUniqueValues()
    {
        UniqueSpecialties = Doctors.Select(d => d.Specialty).Distinct().ToList();
        UniqueExperienceYears = Doctors.Select(d => d.ExperienceYear).Distinct().OrderBy(y => y).ToList();
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

}

