using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using Windows.UI;
using Windows.UI.Text;
using Microsoft.UI;
using MedSy.ViewModels;
using MedSy.Converter;
using MedSy.Converter.WorkSchedule;
using Microsoft.UI.Text;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MedSy.Views.Doctor
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WorkSchedulePage : Page
    {
        public WorkScheduleViewModel workScheduleViewModel { get; set; }
        public WorkSchedulePage()
        {
            this.InitializeComponent();
            workScheduleViewModel = new WorkScheduleViewModel();
            DefineScheduleGrid();
        }
        private void DefineScheduleGrid()
        {

            for (int i = 0; i < 8; i++)
            {
                ColumnDefinition column = new ColumnDefinition();
                column.Width = new GridLength(1, GridUnitType.Star);
                schedule.ColumnDefinitions.Add(column);
            }
            for (int i = 0; i < 25; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(40);
                schedule.RowDefinitions.Add(row);
            }
            
            MarkerItemConverter markerItemConverter = new MarkerItemConverter();
            DateBgConverter dateBgConverter = new DateBgConverter();
            DateValueConverter dateValueConverter = new DateValueConverter();
            Color textColor = Color.FromArgb(255, 51, 102, 153);

            for (int i = 0;i<25;i++)
            {
                for(int j = 0;j<8;j++)
                {
                    Binding binding = new Binding()
                    {
                        Mode = BindingMode.OneWay,
                        Source = workScheduleViewModel,
                        Path = new PropertyPath("marker"),
                        Converter = markerItemConverter,
                        ConverterParameter = $"{i},{j}"
                    };
                    Border border = new Border
                    {
                        
                        BorderThickness = new Thickness(1),
                        VerticalAlignment = VerticalAlignment.Stretch,
                        HorizontalAlignment = HorizontalAlignment.Stretch
                    };
                    if(i == 0 && j > 0)
                    {
                        string dayOfWeek = "Monday";
                        switch (j)
                        {
                            case 1:
                                dayOfWeek = "Sun";
                                break;
                            case 2:
                                dayOfWeek = "Mon";
                                break;
                            case 3:
                                dayOfWeek = "Tue";
                                break;
                            case 4:
                                dayOfWeek = "Wed";
                                break;
                            case 5:
                                dayOfWeek = "Thu";
                                break;
                            case 6:
                                dayOfWeek = "Fri";
                                break;
                            case 7:
                                dayOfWeek = "Sat";
                                break;
                        };

                        TextBlock dateStringValue = new TextBlock()
                        {
                            Text = dayOfWeek,
                            FontWeight = FontWeights.Bold,
                            Foreground = new SolidColorBrush(textColor),
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            Margin = new Thickness(0, 0, 0, 3)
                        };

                        TextBlock dateValue = new TextBlock()
                        {
                            FontWeight = FontWeights.SemiBold,
                            Foreground = new SolidColorBrush(textColor),
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center
                        };
                        Binding dateValueBinding = new Binding()
                        {
                            Mode = BindingMode.OneWay,
                            Source = workScheduleViewModel,
                            Path = new PropertyPath("dateItems"),
                            Converter = dateValueConverter,
                            ConverterParameter = $"{j}"
                        };
                        dateValue.SetBinding(TextBlock.TextProperty, dateValueBinding);

                        StackPanel stackPanel = new StackPanel()
                        {
                            Orientation = Orientation.Vertical
                        };
                        Binding dateBgBinding = new Binding()
                        {
                            Mode = BindingMode.OneWay,
                            Source = workScheduleViewModel,
                            Path = new PropertyPath("dateItems"),
                            Converter = dateBgConverter,
                            ConverterParameter = $"{j}"
                        };
                        stackPanel.SetBinding(StackPanel.BackgroundProperty, dateBgBinding);
                        stackPanel.Children.Add(dateStringValue);
                        stackPanel.Children.Add(dateValue);
                        border.Child = stackPanel;
                    }
                    if(j == 0 && i > 0)
                    {
                        TextBlock textBlock = new TextBlock()
                        {
                            Text = (i - 1).ToString() + ":00",
                            FontWeight = FontWeights.SemiBold,
                            Foreground = new SolidColorBrush(textColor),
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Top
                        };
                        border.Child = textBlock;
                    }
                    border.SetBinding(Border.BackgroundProperty, binding);

                    DateOnly today = DateOnly.FromDateTime(DateTime.Now);
                    int dayOfWeekToday = (int)today.DayOfWeek;
                    if (i == 0 && j == dayOfWeekToday + 1)
                    {
                        border.Background = new SolidColorBrush(Color.FromArgb(255, 217, 217, 217));
                    }

                    Grid.SetColumn(border, j);
                    Grid.SetRow(border, i);
                    schedule.Children.Add(border);
                }
            }
        }
       
        private void selectDateClicked(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);

            if (calendar.SelectedDates.Count > 0)
            {
                DateTime selectedDate = calendar.SelectedDates[0].DateTime;
                date = DateOnly.FromDateTime(selectedDate);
            }

            workScheduleViewModel.getConsultations(date);
            workScheduleViewModel.refreshMarker();
            workScheduleViewModel.updateMarker();
            workScheduleViewModel.updateDateItems(date);
        }
    }
}
