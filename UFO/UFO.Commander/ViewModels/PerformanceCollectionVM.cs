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

        public ObservableCollection<PerformanceRowVM> PerformanceRows { get; private set; }

        public IEnumerable<DateTime> performanceDays { get; private set; }
        public IEnumerable<Area> areas { get; private set; }

        private PerformanceVM currentPerformance;
        private DateTime currentDate;
        private Area currentArea;

        public PerformanceCollectionVM(IUFOServer server)
        {
            this.server = server;
            PerformanceRows = new ObservableCollection<PerformanceRowVM>();

            LoadPerformanceDays();
            LoadAreas();
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

        private async void LoadPerformances()
        {
            PerformanceRows.Clear();
            IEnumerable<Venue> venues = server.FindVenuesByAreaId(currentArea.Id);

            IEnumerator<Venue> enumerator = venues.GetEnumerator();

            // creates a row for each venue in the timetable
            while (await Task.Run(() => enumerator.MoveNext()))
            {
                IEnumerable<Performance> performances = server.FindPerformancesByDateAndVenue(currentDate, enumerator.Current);

                PerformanceRows.Add(new PerformanceRowVM(performances, enumerator.Current, this, server));
            }

            if (PerformanceRows.Count > 0)
            {
                currentPerformance = PerformanceRows[0].VenuePerformances[0];
            }
        }

        private void LoadPerformanceDays()
        {
            IList<DateTime> dates = server.GetPerformanceDates().ToList();

            performanceDays = days;

            if (days.Count() > 0)
            {
                currentDate = days[0];
            }
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
