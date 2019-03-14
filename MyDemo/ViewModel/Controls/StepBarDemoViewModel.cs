﻿using System;
using System.Linq;
using System.Windows.Controls;
using GalaSoft.MvvmLight.CommandWpf;
using Xky.UI.Controls;
using MyDemo.Data;
using MyDemo.Service;

namespace MyDemo.ViewModel
{
    public class StepBarDemoViewModel : DemoViewModelBase<StepBarDemoModel>
    {
        public StepBarDemoViewModel(DataService dataService) => DataList = dataService.GetStepBarDemoDataList();

        /// <summary>
        ///     下一步
        /// </summary>
        public RelayCommand<Panel> NextCmd => new Lazy<RelayCommand<Panel>>(() => new RelayCommand<Panel>(Next)).Value;

        /// <summary>
        ///     上一步
        /// </summary>
        public RelayCommand<Panel> PrevCmd => new Lazy<RelayCommand<Panel>>(() => new RelayCommand<Panel>(Prev)).Value;

        private void Next(Panel panel)
        {
            foreach (var stepBar in panel.Children.OfType<StepBar>())
            {
                stepBar.Next();
            }
        }

        private void Prev(Panel panel)
        {
            foreach (var stepBar in panel.Children.OfType<StepBar>())
            {
                stepBar.Prev();
            }
        }
    }
}