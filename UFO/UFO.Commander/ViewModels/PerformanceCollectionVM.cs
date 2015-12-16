using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UFO.Domain;
using UFO.Server;

namespace UFO.Commander.ViewModels
{
    public class PerformanceCollectionVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IUFOServer server;

        public ObservableCollection<PerformanceVM> Performances14 { get; private set; }
        public ObservableCollection<PerformanceVM> Performances15 { get; private set; }
        public ObservableCollection<PerformanceVM> Performances16 { get; private set; }
        public ObservableCollection<PerformanceVM> Performances17 { get; private set; }
        public ObservableCollection<PerformanceVM> Performances18 { get; private set; }
        public ObservableCollection<PerformanceVM> Performances19 { get; private set; }
        public ObservableCollection<PerformanceVM> Performances20 { get; private set; }
        public ObservableCollection<PerformanceVM> Performances21 { get; private set; }
        public ObservableCollection<PerformanceVM> Performances22 { get; private set; }

        public IEnumerable<DateTime> performanceDays { get; private set; }
        public IEnumerable<Area> areas { get; private set; }
        public IEnumerable<Venue> venues { get; private set; }

        private PerformanceVM currentPerformance;
        private DateTime currentDate;
        private Area currentArea;

        public PerformanceCollectionVM(IUFOServer server)
        {
            this.server = server;
            Performances14 = new ObservableCollection<PerformanceVM>();
            Performances15 = new ObservableCollection<PerformanceVM>();
            Performances16 = new ObservableCollection<PerformanceVM>();
            Performances17 = new ObservableCollection<PerformanceVM>();
            Performances18 = new ObservableCollection<PerformanceVM>();
            Performances19 = new ObservableCollection<PerformanceVM>();
            Performances20 = new ObservableCollection<PerformanceVM>();
            Performances21 = new ObservableCollection<PerformanceVM>();
            Performances22 = new ObservableCollection<PerformanceVM>();

            LoadPerformanceDays();
            LoadAreas();
            LoadVenues();
            LoadPerformances();
        }

        public PerformanceVM CurrentPerformance
        {
            get { return currentPerformance; }
            set
            {
                if (currentPerformance != value)
                {
                    currentPerformance = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentPerformance)));
                }
            }
        }

        public DateTime CurrentDate
        {
            get { return currentDate; }
            set
            {
                if (currentDate != value)
                {
                    currentDate = value;

                    LoadPerformances();

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentDate)));
                }
            }
        }

        public Area CurrentArea
        {
            get { return currentArea; }
            set
            {
                if (currentArea != value)
                {
                    currentArea = value;

                    LoadPerformances();

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentArea)));
                }
            }
        }

        private void LoadPerformances()
        {
            IEnumerable<Performance> performances = server.FindPerformancesByDate(currentDate);

            Performances14.Clear();
            Performances15.Clear();
            Performances16.Clear();
            Performances17.Clear();
            Performances18.Clear();
            Performances19.Clear();
            Performances20.Clear();
            Performances21.Clear();
            Performances22.Clear();

            foreach (Performance performance in performances)
            {
                switch (performance.DateTime.Hour)
                {
                    case 14:
                        Performances14.Add(new PerformanceVM(performance, this, server));
                        break;
                    case 15:
                        Performances15.Add(new PerformanceVM(performance, this, server));
                        break;
                    case 16:
                        Performances16.Add(new PerformanceVM(performance, this, server));
                        break;
                    case 17:
                        Performances17.Add(new PerformanceVM(performance, this, server));
                        break;
                    case 18:
                        Performances18.Add(new PerformanceVM(performance, this, server));
                        break;
                    case 19:
                        Performances19.Add(new PerformanceVM(performance, this, server));
                        break;
                    case 20:
                        Performances20.Add(new PerformanceVM(performance, this, server));
                        break;
                    case 21:
                        Performances21.Add(new PerformanceVM(performance, this, server));
                        break;
                    case 22:
                        Performances22.Add(new PerformanceVM(performance, this, server));
                        break;
                    default:
                        break;
                }
            }
        }

        private void LoadPerformanceDays()
        {
            IEnumerable<DateTime> dates = server.GetPerformanceDates();
            IList<DateTime> days = new List<DateTime>();

            foreach (DateTime day in dates)
            {
                days.Add(day);
            }

            performanceDays = days;

            if (days.Count() > 0)
            {
                currentDate = days[0];
            }
        }

        private void LoadVenues()
        {
            venues = server.FindAllVenues();
        }

        private void LoadAreas()
        {
            areas = server.FindAllAreas();

            if (areas.Count() > 0)
            {
                currentArea = areas.First();
            }
            else
            {
                currentArea = null;
            }
        }
    }
}
