using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UFO.Domain;
using UFO.Server;

namespace UFO.Commander.ViewModels
{
    public class PerformanceVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IUFOServer server;
        private Performance performance;
        private PerformanceArtistVM performanceArtistVM;

        private PerformanceRowVM performanceRowVM;

        private IList<Country> countries;
        private IList<Category> categories;

        public PerformanceVM(Performance performance, PerformanceRowVM performanceRowVM, IList<Country> countries, IList<Category> categories, IUFOServer server)
        {
            this.performance = performance;
            this.performanceRowVM = performanceRowVM;
            this.server = server;

            this.countries = countries;
            this.categories = categories;

            if (performance.Id != 0)
            {
                Artist artist = server.FindArtistById(performance.ArtistId);

                Country country = null;
                Category category = null;

                foreach (Country c in countries)
                {
                    if (c.Abbreviation.Equals(artist.CountryId))
                    {
                        country = c;
                    }
                }

                foreach (Category c in categories)
                {
                    if (c.Id.Equals(artist.CategoryId))
                    {
                        category = c;
                    }
                }

                performanceArtistVM = new PerformanceArtistVM(artist, category, country, this, server);
            }
        }


        public int Id
        {
            get { return performance.Id; }
            set
            {
                if (performance.Id != value)
                {
                    performance.Id = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
                }
            }
        }

        public PerformanceArtistVM PerformanceArtistVM
        {
            get { return performanceArtistVM; }
            set
            {
                if (performanceArtistVM != value)
                {
                    performanceArtistVM = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PerformanceArtistVM)));
                }
            }
        }

        public Performance Performance
        {
            get { return performance; }
            set
            {
                if (performance != value)
                {
                    performance = value;

                    Artist artist = server.FindArtistById(performance.ArtistId);

                    Country country = null;
                    Category category = null;

                    foreach (Country c in countries)
                    {
                        if (c.Abbreviation.Equals(artist.CountryId))
                        {
                            country = c;
                        }
                    }

                    foreach (Category c in categories)
                    {
                        if (c.Id.Equals(artist.CategoryId))
                        {
                            category = c;
                        }
                    }

                    performanceArtistVM = new PerformanceArtistVM(artist, category, country, this, server);

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Performance)));
                }
            }
        }

        public PerformanceRowVM PerformanceRowVM
        {
            get { return performanceRowVM; }
            set
            {
                if (performanceRowVM != value)
                {
                    performanceRowVM = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PerformanceRowVM)));
                }
            }
        }
    }
}
