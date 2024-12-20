using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using System.Collections.ObjectModel;
using System.ComponentModel;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using SkiaSharp;
using System.Globalization;
using MedSy.Services.Consultation;
using MedSy.Services.Prescription;

namespace MedSy.ViewModels;
public class StatisticViewModel:INotifyPropertyChanged
{
    public ObservableCollection<string> Months { get; set; }
    public string selectedMonth { get; set; }
    public ObservableCollection<int> Years { get; set; }
    public int selectedYear { get; set; }
    public ObservableCollection<string> XLabels { get; set; } 
    public ISeries[] ColumnSeries { get; set; }
    public ISeries[] LineSeries { get; set; }
    public ISeries[] LineSeries2 { get; set; }

    public Axis[] ColumnXAxes { get; set; }
    public Axis[] ColumnYAxes { get; set; }

    public Axis[] LineXAxes1 { get; set; }
    public Axis[] LineYAxes1 { get; set; }
    public Axis[] LineXAxes2 { get; set; }
    public Axis[] LineYAxes2 { get; set; }
    public StatisticViewModel()
    {
        Months = new ObservableCollection<string>
        {
            "January", "February", "March", "April", "May", "June",
            "July", "August", "September", "October", "November", "December"
        };

        Years = new ObservableCollection<int>
        {
            2021, 2022, 2023, 2024
        };
        int currentMonthIndex = DateTime.Now.Month;
        selectedMonth = Months[currentMonthIndex - 1];
        selectedYear = DateTime.Now.Year;
        XLabels = new ObservableCollection<string>();
        configureAxes();
        LoadOnlineConsultationStatistics();
        LoadRevenueStatistics(); 
        LoadPathologyStatistics();
    }

    public void LoadRevenueStatistics()
    {
        IPrescriptionDao prescriptionDao = (Application.Current as App).locator.prescriptionDao;
        var revenueData = prescriptionDao.calculateRevenueEachMonth(selectedYear);

        LineSeries<int> lineSeries = new LineSeries<int>
        {
            Name = "Revenue",
            Values = new List<int>(),
            GeometrySize = 5,
            LineSmoothness = 0,
            GeometryFill = new SolidColorPaint(new SKColor(51, 102, 153, 255)),
            GeometryStroke = new SolidColorPaint(SKColors.Gray) { StrokeThickness = 1 },
        };

        for (int i = 1; i <= 12; i++)
        {
            if (revenueData.ContainsKey(i))
            {
                lineSeries.Values.Add(revenueData[i]);
            }
            else
            {
                lineSeries.Values.Add(0);
            }
        }
        LineSeries2 = new ISeries[] { lineSeries };
    }

    public void LoadOnlineConsultationStatistics()
    {
        IConsultationDao consultationDao = (Application.Current as App).locator.consultationDao;
        var onlineConsultationData = consultationDao.countOnlineConsultationEachMonth(selectedYear);

        LineSeries<int> lineSeries = new LineSeries<int>
        {
            Name = "Online Consultation",
            Values = new List<int>(),
            GeometrySize = 5,
            LineSmoothness = 0,
            GeometryFill = new SolidColorPaint(new SKColor(51, 102, 153, 255)),
            GeometryStroke = new SolidColorPaint(SKColors.Gray) { StrokeThickness = 1 },
        };

        for(int i = 1; i <= 12; i++)
        {
            lineSeries.Values.Add(onlineConsultationData[i]);
        }
        LineSeries = new ISeries[] { lineSeries };
    }

    public void LoadPathologyStatistics()
    {
        IConsultationDao consultationDao = (Application.Current as App).locator.consultationDao;
        var pathologyData = consultationDao.GetPathologyCountByMonth(Months.IndexOf(selectedMonth) + 1);

        XLabels.Clear();
        ColumnSeries<int> columnSeries = new ColumnSeries<int>
        {
            Name = "Pathology",
            Values = new List<int>(),
            Fill = new SolidColorPaint(new SKColor(51, 102, 153, 255)),
        };
        foreach (var kvp in pathologyData)
        {
            XLabels.Add(kvp.Key);
            columnSeries.Values.Add(kvp.Value);
        }
        ColumnSeries = new ISeries[] { columnSeries };
    }

    public void configureAxes()
    {
        ColumnXAxes = new Axis[]
        {
            new Axis
            {
                TextSize = 15,
                Labels = XLabels,
                ForceStepToMin = true,
                MinStep = 1,
                SeparatorsPaint = new SolidColorPaint(new SKColor(200, 200, 200)),
                SeparatorsAtCenter = false,
                TicksAtCenter = false,
            }
        };
        ColumnYAxes = new Axis[]
        {
            new Axis
            {
                TextSize = 15,
                MinLimit = 0,
                MinStep = 1,
                TicksPaint = new SolidColorPaint(new SKColor(35, 35, 35)),
                //ForceStepToMin = true,
                SeparatorsAtCenter = false,
                //MaxLimit = 3,
            }
        };

        LineXAxes1 = new Axis[]
        {
            new Axis
            {
                TextSize = 15,
                Labels = Months,
                ForceStepToMin = true,
                MinStep = 1,
                SeparatorsPaint = new SolidColorPaint(new SKColor(200, 200, 200)),
                SeparatorsAtCenter = false,
                TicksAtCenter = true,
            }
        };

        LineYAxes1 = new Axis[]
        {
            new Axis
            {
                TextSize = 15,
                MinLimit = 0,
                MinStep = 1,
                TicksPaint = new SolidColorPaint(new SKColor(35, 35, 35)),
                //ForceStepToMin = true,
                SeparatorsAtCenter = false,
                
            }
        };

        LineXAxes2 = new Axis[]
        {
            new Axis
            {
                TextSize = 15,
                Labels = Months,
                ForceStepToMin = true,
                MinStep = 1,
                SeparatorsPaint = new SolidColorPaint(new SKColor(200, 200, 200)),
                SeparatorsAtCenter = false,
                TicksAtCenter = false,
            }
        };

        LineYAxes2 = new Axis[]
        {
            new Axis
            {
                TextSize = 15,
                MinLimit = 0,
                MinStep = 1,
                TicksPaint = new SolidColorPaint(new SKColor(35, 35, 35)),
                Labeler = value => string.Format(CultureInfo.GetCultureInfo("vi-VN"), "{0:N0} VND", value), 
                //ForceStepToMin = true,
                SeparatorsAtCenter = false,

            }
        };
    }
    public event PropertyChangedEventHandler PropertyChanged;
};
