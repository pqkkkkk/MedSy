using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.UI.Xaml;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using MedSy.Models;
using MedSy.Services.Drug;
using System.Diagnostics.Eventing.Reader;

namespace MedSy.ViewModels
{
    public class DrugManagementViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Drug> drugs { get; set; }
        public Drug selectedDrug { get; set; }
        public string Keyword { get; set; } = "";
        public int SelectedPageIndex { get; set; }
        public int CurrentPage { get; set; } = 1;
        public ObservableCollection<PageInfo> PageInfos { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public int RowsPerPage { get; set; } = 10;
        IDrugDao drugDao { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public DrugManagementViewModel()
        {
            drugDao = (Application.Current as App).locator.drugDao;
            LoadData();
            selectedDrug = new Drug();
        }
        public void LoadData()
        {
            var (list, totalitem) = drugDao.GetDrugs(CurrentPage, RowsPerPage, Keyword, null, 0, 0);

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


        public void Search()
        {
            CurrentPage = 1;
            LoadData();
        }

        public List<string> GetSuggestions(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return new List<string>();

            return drugs
                .Where(d => d.name.Contains(query, StringComparison.OrdinalIgnoreCase))
                .Select(d => d.name)
                .ToList();
        }

        public bool addDrug(Drug newDrug)
        {
            if (drugDao.addDrug(newDrug))
            {
                LoadData();
                return true;
            }
            return false;
        }

        public void updateQuantity(int quantityToAdd)
        {
            int newQuantity = selectedDrug.quantity + quantityToAdd;
            drugDao.updateQuantity(newQuantity ,selectedDrug.drugId);
            LoadData();
            
        }
    }
}
