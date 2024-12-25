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
public class StatisticViewModel : INotifyPropertyChanged
{
    public ObservableCollection<string> Months { get; set; } = new ObservableCollection<string>
        {
            "January", "February", "March", "April", "May", "June",
            "July", "August", "September", "October", "November", "December"
        };
    public ObservableCollection<int> Years { get; set; } = new ObservableCollection<int>
        {
            2021, 2022, 2023, 2024
        };
    public ObservableCollection<string> Weeks { get; set; } = new ObservableCollection<string>
    {
        "Week 1", "Week 2",  "Week 3",  "Week 4",  "Week 5"
    };

    public int defaultYear = DateTime.Now.Year;
    public ObservableCollection<string> XLabels { get; set; }
    public ObservableCollection<string> DayOfWeek { get; set; } = new ObservableCollection<string>
    {
        "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"
    };
    public ObservableCollection<ISeries> ColumnSeries { get; set; }

    public ObservableCollection<ISeries> LineSeries { get; set; }
    public ObservableCollection<ISeries> LineSeries2 { get; set; }

    public Axis[] ColumnXAxes { get; set; }
    public Axis[] ColumnYAxes { get; set; }

    public Axis[] LineXAxes1 { get; set; }
    public Axis[] LineYAxes1 { get; set; }
    public Axis[] LineXAxes2 { get; set; }
    public Axis[] LineYAxes2 { get; set; }
    public StatisticViewModel()
    {
        XLabels = new ObservableCollection<string>();
        configureAxesForPathologyRate();
        LoadPathologyStatisticsByYear(defaultYear);

        configureAxesForConsultationStatistic(Months);
        LoadOnlineConsultationStatisticsByYear(defaultYear);
        LoadRevenueStatisticsByYear(defaultYear);

    }
    public void LoadPathologyStatisticsByYear(int year)
    {
        IConsultationDao consultationDao = (Application.Current as App).locator.consultationDao;
        var pathologyData = consultationDao.GetPathologyCountByYear(year);

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
        ColumnSeries = new ObservableCollection<ISeries> { columnSeries };
    }

    public void LoadPathologyStatisticsByMonth(int month, int year)
    {
        IConsultationDao consultationDao = (Application.Current as App).locator.consultationDao;
        var pathologyData = consultationDao.GetPathologyCountByMonth(month, year);

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
        ColumnSeries = new ObservableCollection<ISeries> { columnSeries };
    }

    public void LoadPathologyStatisticsByWeek(int week, int month, int year)
    {
        IConsultationDao consultationDao = (Application.Current as App).locator.consultationDao;
        var pathologyData = consultationDao.GetPathologyCountByWeek(week, month, year);

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
        ColumnSeries = new ObservableCollection<ISeries> { columnSeries };
    }

    public void LoadOnlineConsultationStatisticsByYear(int year)
    {
        IConsultationDao consultationDao = (Application.Current as App).locator.consultationDao;
        var onlineConsultationData = consultationDao.countOnlineConsultationByYear(year);

        LineSeries<int> lineSeries = new LineSeries<int>
        {
            Name = "Online Consultation",
            Values = new List<int>(),
            GeometrySize = 5,
            LineSmoothness = 0,
            GeometryFill = new SolidColorPaint(new SKColor(51, 102, 153, 255)),
            GeometryStroke = new SolidColorPaint(SKColors.Gray) { StrokeThickness = 1 },
        };

        for (int i = 1; i <= 12; i++)
        {
            lineSeries.Values.Add(onlineConsultationData[i]);
        }
        LineSeries = new ObservableCollection<ISeries> { lineSeries };
        configureAxesForConsultationStatistic(Months);
    }

    public void LoadOnlineConsultationStatisticsByMonth(int month, int year)
    {
        IConsultationDao consultationDao = (Application.Current as App).locator.consultationDao;
        var onlineConsultationData = consultationDao.countOnlineConsultationByMonth(month, year);

        LineSeries<int> lineSeries = new LineSeries<int>
        {
            Name = "Online Consultation",
            Values = new List<int>(),
            GeometrySize = 5,
            LineSmoothness = 0,
            GeometryFill = new SolidColorPaint(new SKColor(51, 102, 153, 255)),
            GeometryStroke = new SolidColorPaint(SKColors.Gray) { StrokeThickness = 1 },
        };

        for (int i = 1; i <= 5; i++)
        {
            if (onlineConsultationData.ContainsKey(i))
            {
                lineSeries.Values.Add(onlineConsultationData[i]);
            }
            else
            {
                lineSeries.Values.Add(0);
            }
        }
        LineSeries = new ObservableCollection<ISeries> { lineSeries };
        configureAxesForConsultationStatistic(Weeks);
    }
    public void LoadOnlineConsultationStatisticsByWeek(int week, int month, int year)
    {
        IConsultationDao consultationDao = (Application.Current as App).locator.consultationDao;
        var onlineConsultationData = consultationDao.countOnlineConsultationByWeek(week, month, year);

        LineSeries<int> lineSeries = new LineSeries<int>
        {
            Name = "Online Consultation",
            Values = new List<int>(),
            GeometrySize = 5,
            LineSmoothness = 0,
            GeometryFill = new SolidColorPaint(new SKColor(51, 102, 153, 255)),
            GeometryStroke = new SolidColorPaint(SKColors.Gray) { StrokeThickness = 1 },
        };

        for (int i = 1; i <= DayOfWeek.Count; i++)
        {
            if(onlineConsultationData.ContainsKey(i))
            {
                lineSeries.Values.Add(onlineConsultationData[i]);
            }
            else
            {
                lineSeries.Values.Add(0);
            }
        }
        LineSeries = new ObservableCollection<ISeries> { lineSeries };
        configureAxesForConsultationStatistic(DayOfWeek);
    }

    public void LoadRevenueStatisticsByYear(int year)
    {
        IPrescriptionDao prescriptionDao = (Application.Current as App).locator.prescriptionDao;
        var revenueData = prescriptionDao.calculateRevenueByYear(year);

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
        LineSeries2 = new ObservableCollection<ISeries> { lineSeries };
        configureAxesForRevenueStatistic(Months);
    }
    public void LoadRevenueStatisticsByMonth(int month, int year)
    {
        IPrescriptionDao prescriptionDao = (Application.Current as App).locator.prescriptionDao;
        var revenueData = prescriptionDao.calculateRevenueByMonth(month, year);

        LineSeries<int> lineSeries = new LineSeries<int>
        {
            Name = "Revenue",
            Values = new List<int>(),
            GeometrySize = 5,
            LineSmoothness = 0,
            GeometryFill = new SolidColorPaint(new SKColor(51, 102, 153, 255)),
            GeometryStroke = new SolidColorPaint(SKColors.Gray) { StrokeThickness = 1 },
        };

        for (int i = 1; i <= 5; i++)
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
        LineSeries2 = new ObservableCollection<ISeries> { lineSeries };
        configureAxesForRevenueStatistic(Weeks);
    }

    public void LoadRevenueStatisticsByWeek(int week, int month, int year)
    {
        IPrescriptionDao prescriptionDao = (Application.Current as App).locator.prescriptionDao;
        var revenueData = prescriptionDao.calculateRevenueByWeek(week, month, year);

        LineSeries<int> lineSeries = new LineSeries<int>
        {
            Name = "Revenue",
            Values = new List<int>(),
            GeometrySize = 5,
            LineSmoothness = 0,
            GeometryFill = new SolidColorPaint(new SKColor(51, 102, 153, 255)),
            GeometryStroke = new SolidColorPaint(SKColors.Gray) { StrokeThickness = 1 },
        };

        for (int i = 1; i <= 5; i++)
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
        LineSeries2 = new ObservableCollection<ISeries> { lineSeries };
        configureAxesForRevenueStatistic(DayOfWeek);
    }
    public void configureAxesForPathologyRate()
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
                //MinStep = 1,
                Labeler = value => string.Format("{0} consultation",value),
                TicksPaint = new SolidColorPaint(new SKColor(35, 35, 35)),
                //ForceStepToMin = true,
                SeparatorsAtCenter = false,
                //MaxLimit = 3,
            }
        };
    }
    public void configureAxesForConsultationStatistic(ObservableCollection<string> labels)
    {
        LineXAxes1 = new Axis[]
        {
            new Axis
            {
                TextSize = 15,
                Labels = labels,
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
                Labeler = value => string.Format("{0} consultation",value),
                TicksPaint = new SolidColorPaint(new SKColor(35, 35, 35)),
                //ForceStepToMin = true,
                SeparatorsAtCenter = false,

            }
        };
    }
    public void configureAxesForRevenueStatistic(ObservableCollection<string> labels)
    {
        LineXAxes2 = new Axis[]
            {
            new Axis
            {
                TextSize = 15,
                Labels = labels,
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
