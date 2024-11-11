using LSM_prototype.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSM_prototype.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObjects
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand ViewWonCommand { get; set; }
        public RelayCommand ViewTooCommand { get; set; }
        public RelayCommand ViewTreeCommand { get; set; }
        public RelayCommand ViewForCommand { get; set; }
        public RelayCommand ViewFaivCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public ViewWonModel ViewWonVM { get; set; }
        public ViewTooModel ViewTooVM { get; set; }
        public ViewTreeModel ViewTreeVM { get; set; }
        public ViewForModel ViewForVM { get; set; }
        public ViewFaivModel ViewFaivVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            ViewWonVM = new ViewWonModel();
            ViewTooVM = new ViewTooModel();
            ViewTreeVM = new ViewTreeModel();
            ViewForVM = new ViewForModel();
            ViewFaivVM = new ViewFaivModel();
            CurrentView = HomeVM;

            ViewWonCommand = new RelayCommand(o =>
            {
                // If already on HomeVM, reset the view to HomeVM again
                if (CurrentView == ViewWonVM)
                {
                    CurrentView = HomeVM;
                }
                else
                {
                    CurrentView = ViewWonVM;
                }
            });

            ViewTooCommand = new RelayCommand(o =>
            {
                if (CurrentView == ViewTooVM)
                {
                    // If the current view is the same, close it (set to HomeVM)
                    CurrentView = HomeVM;
                }
                else
                {
                    CurrentView = ViewTooVM;
                }
            });

            ViewTreeCommand = new RelayCommand(o =>
            {
                if (CurrentView == ViewTreeVM)
                {
                    // If the current view is the same, close it (set to HomeVM)
                    CurrentView = HomeVM;
                }
                else
                {
                    CurrentView = ViewTreeVM;
                }
            });

            ViewForCommand = new RelayCommand(o =>
            {
                if (CurrentView == ViewForVM)
                {
                    // If the current view is the same, close it (set to HomeVM)
                    CurrentView = HomeVM;
                }
                else
                {
                    CurrentView = ViewForVM;
                }
            });

            ViewFaivCommand = new RelayCommand(o =>
            {
                if (CurrentView == ViewFaivVM)
                {
                    // If the current view is the same, close it (set to HomeVM)
                    CurrentView = HomeVM;
                }
                else
                {
                    CurrentView = ViewFaivVM;
                }
            });
        }
    }
}
