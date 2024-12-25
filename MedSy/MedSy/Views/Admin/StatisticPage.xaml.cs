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
using MedSy.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MedSy.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StatisticPage : Page
    {
        public StatisticViewModel statisticViewModel { get; set; }
        public StatisticPage()
        {
            this.InitializeComponent();
            statisticViewModel = new StatisticViewModel();
            this.DataContext = statisticViewModel;
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void YearPathologyRateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WeekPathologyRateComboBox.SelectedItem != null)
            {
                statisticViewModel.LoadPathologyStatisticsByWeek(WeekPathologyRateComboBox.SelectedIndex + 1, MonthPathologyRateComboBox.SelectedIndex + 1, (int)YearPathologyRateComboBox.SelectedItem
                );
            }
            else if (MonthPathologyRateComboBox.SelectedItem != null)
            {
                statisticViewModel.LoadPathologyStatisticsByMonth(MonthPathologyRateComboBox.SelectedIndex + 1, (int)YearPathologyRateComboBox.SelectedItem
                );
            }
            else
            {
                statisticViewModel.LoadPathologyStatisticsByYear((int)YearPathologyRateComboBox.SelectedItem);
            }
        }
        private void MonthPathologyRateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WeekPathologyRateComboBox.SelectedItem != null)
            {
                statisticViewModel.LoadPathologyStatisticsByWeek(WeekPathologyRateComboBox.SelectedIndex + 1, MonthPathologyRateComboBox.SelectedIndex + 1, (int)YearPathologyRateComboBox.SelectedItem
                );
            }
            else
            {
                if(MonthPathologyRateComboBox.SelectedItem != null)
                {
                    WeekPathologyRateComboBox.Visibility = Visibility.Visible;
                    reset.Visibility = Visibility.Visible;
                    statisticViewModel.LoadPathologyStatisticsByMonth(MonthPathologyRateComboBox.SelectedIndex + 1, (int)YearPathologyRateComboBox.SelectedItem);
                }
            }
        }

        private void WeekPathologyRateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WeekPathologyRateComboBox.SelectedItem != null)
            {
                statisticViewModel.LoadPathologyStatisticsByWeek(WeekPathologyRateComboBox.SelectedIndex + 1, MonthPathologyRateComboBox.SelectedIndex + 1, (int)YearPathologyRateComboBox.SelectedItem);
            }
        }

        private void ResetPathologyRate(object sender, RoutedEventArgs e)
        {
            WeekPathologyRateComboBox.Visibility = Visibility.Collapsed;
            reset.Visibility = Visibility.Collapsed;
            WeekPathologyRateComboBox.SelectedItem = null;
            MonthPathologyRateComboBox.SelectedItem = null;
            statisticViewModel.LoadPathologyStatisticsByYear((int)YearPathologyRateComboBox.SelectedItem);
        }

        private void YearConsultationChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MonthConsultationStatisticComboBox.SelectedItem != null)
            {
                if (WeekConsultationStatisticComboBox.SelectedItem != null)
                {
                    statisticViewModel.LoadOnlineConsultationStatisticsByWeek(WeekConsultationStatisticComboBox.SelectedIndex + 1, MonthConsultationStatisticComboBox.SelectedIndex + 1, (int)YearConsultationStatisticComboBox.SelectedItem);
                }
                else
                {
                    statisticViewModel.LoadOnlineConsultationStatisticsByMonth(MonthConsultationStatisticComboBox.SelectedIndex + 1,(int)YearConsultationStatisticComboBox.SelectedItem);
                }
            }
            else
            {
                statisticViewModel.LoadOnlineConsultationStatisticsByYear((int)YearConsultationStatisticComboBox.SelectedItem);
            }
        }

        private void MonthConsultationChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WeekConsultationStatisticComboBox.SelectedItem != null)
            {
                statisticViewModel.LoadOnlineConsultationStatisticsByWeek(WeekConsultationStatisticComboBox.SelectedIndex + 1, MonthConsultationStatisticComboBox.SelectedIndex + 1,(int)YearConsultationStatisticComboBox.SelectedItem);
            }
            else if (MonthConsultationStatisticComboBox.SelectedItem != null)
            {
                WeekConsultationStatisticComboBox.Visibility = Visibility.Visible;
                resetOnlineConsultation.Visibility = Visibility.Visible;

                statisticViewModel.LoadOnlineConsultationStatisticsByMonth(MonthConsultationStatisticComboBox.SelectedIndex + 1,(int)YearConsultationStatisticComboBox.SelectedItem);
            }
            else
            {
                statisticViewModel.LoadOnlineConsultationStatisticsByYear((int)YearConsultationStatisticComboBox.SelectedItem);
            }
        }

        private void WeekConsultationChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WeekConsultationStatisticComboBox.SelectedItem != null)
            {
                statisticViewModel.LoadOnlineConsultationStatisticsByWeek(WeekConsultationStatisticComboBox.SelectedIndex + 1, MonthConsultationStatisticComboBox.SelectedIndex + 1,(int)YearConsultationStatisticComboBox.SelectedItem);
            }
        }

        private void ResetOnlineConsultation(object sender, RoutedEventArgs e)
        {
            WeekConsultationStatisticComboBox.Visibility = Visibility.Collapsed;
            resetOnlineConsultation.Visibility = Visibility.Collapsed;

            WeekConsultationStatisticComboBox.SelectedItem = null;
            MonthConsultationStatisticComboBox.SelectedItem = null;

            statisticViewModel.LoadOnlineConsultationStatisticsByYear((int)YearConsultationStatisticComboBox.SelectedItem);
        }

        private void ResetRevenueStatistic(object sender, RoutedEventArgs e)
        {
            WeekRevenueStatisticComboBox.Visibility = Visibility.Collapsed;
            resetRevenueStatistic.Visibility = Visibility.Collapsed;

            WeekRevenueStatisticComboBox.SelectedItem = null;
            MonthRevenueStatisticComboBox.SelectedItem = null;

            statisticViewModel.LoadRevenueStatisticsByYear((int)YearRevenueStatisticComboBox.SelectedItem);
        }

        private void WeekRevenueChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WeekRevenueStatisticComboBox.SelectedItem != null)
            {
                statisticViewModel.LoadRevenueStatisticsByWeek(WeekRevenueStatisticComboBox.SelectedIndex + 1, MonthRevenueStatisticComboBox.SelectedIndex + 1, (int)YearRevenueStatisticComboBox.SelectedItem);
            }
        }

        private void MonthRevenueChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WeekRevenueStatisticComboBox.SelectedItem != null)
            {
                statisticViewModel.LoadRevenueStatisticsByWeek(WeekRevenueStatisticComboBox.SelectedIndex + 1, MonthRevenueStatisticComboBox.SelectedIndex + 1, (int)YearRevenueStatisticComboBox.SelectedItem);
            }
            else if (MonthRevenueStatisticComboBox.SelectedItem != null)
            {
                WeekRevenueStatisticComboBox.Visibility = Visibility.Visible;
                resetRevenueStatistic.Visibility = Visibility.Visible;
                statisticViewModel.LoadRevenueStatisticsByMonth(MonthRevenueStatisticComboBox.SelectedIndex + 1, (int)YearRevenueStatisticComboBox.SelectedItem);
            }
            else
            {
                statisticViewModel.LoadRevenueStatisticsByYear((int)YearRevenueStatisticComboBox.SelectedItem);
            }

        }

        private void YearRevenueChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MonthRevenueStatisticComboBox.SelectedItem != null)
            {
                if (WeekRevenueStatisticComboBox.SelectedItem != null)
                {
                    statisticViewModel.LoadRevenueStatisticsByWeek(WeekRevenueStatisticComboBox.SelectedIndex + 1, MonthRevenueStatisticComboBox.SelectedIndex + 1, (int)YearRevenueStatisticComboBox.SelectedItem);
                }
                else
                {
                    statisticViewModel.LoadRevenueStatisticsByMonth(MonthRevenueStatisticComboBox.SelectedIndex + 1, (int)YearRevenueStatisticComboBox.SelectedItem);
                }
            }
            else
            {
                statisticViewModel.LoadRevenueStatisticsByYear((int)YearRevenueStatisticComboBox.SelectedItem);
            }
        }
    }
}
