using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedSy.Models;
using MedSy.Services.Drug;
using Microsoft.UI.Xaml;
namespace MedSy.ViewModels;
public class PharmacyViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    public ObservableCollection<Drug> drugs { get; set; }
    public ObservableCollection<string> types { get; set; }

    public double minPrice { get; set; }
    public double maxPrice { get; set; }


    public Drug selectedDrug { get; set; }
    IDrugDao drugDao { get; set; }
    public string Keyword { get; set; } = "";
    public string selectedType { get; set; }
    public ObservableCollection<PageInfo> PageInfos { get; set; }
    public int SelectedPageIndex { get; set; }
    public int CurrentPage { get; set; } = 1;
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
    public int RowsPerPage { get; set; } = 9;
    public PharmacyViewModel()
    {
        drugDao = (Application.Current as App).locator.drugDao;
        types = new ObservableCollection<string>(drugDao.getAllDrugType());
        types.Add("All");
        selectedType = types.FirstOrDefault(name => name == "All");
        LoadData();
    }

    public List<string> GetSuggestions(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return new List<string>();

        return drugs
            .Where(d => d.name.Contains(query, StringComparison.OrdinalIgnoreCase))
            .Select(d=>d.name)
            .ToList();
    }
    public void LoadData()
    {
        var (list, totalitem) = drugDao.GetDrugs(CurrentPage, RowsPerPage, Keyword, selectedType, minPrice, maxPrice);

        drugs = new ObservableCollection<Drug>(list);

        TotalItems = totalitem;
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
    public void GoToNextPage()
    {
        if (CurrentPage < TotalPages)
        {
            CurrentPage++;
            LoadData();
        }
    }

    public void GoToPreviousPage()
    {
        if (CurrentPage > 1)
        {
            CurrentPage--;
            LoadData();
        }
    }

    public void GoToPage(int page)
    {
        if (page >= 1 && page <= TotalPages)
        {
            CurrentPage = page;
            LoadData();
        }
    }


    public bool search()
    {
        if(minPrice > maxPrice)
        {
            return false;
        }
        LoadData();
        return true;

    }

}
