using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2.SemesterProjekt.Persistency;
using System.ComponentModel;
using _2.SemesterProjekt.Handler;

namespace _2.SemesterProjekt.Model
{
    public class Singleton : INotifyPropertyChanged
    {
        private ObservableCollection<Barn> børn;
        private static Singleton instance;
        private ObservableCollection<Vaccine> vaccineListe;

        public ObservableCollection<Vaccine> VaccineCollectionIkkesort { get; set; }
        public ObservableCollection<VacSkemaBarnPlan> VacPlanCollection { get; set; }
        public ObservableCollection<Vaccine> VaccineCollection { get; set; }

        public ObservableCollection<Barn> Børn
        {
            get { return børn; }
            set { børn = value; }
        }

        public static Singleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singleton();
                }
                return instance;
            }
        }

        public ObservableCollection<Vaccine> VaccineListe
        {
            get { return vaccineListe; }
            set { vaccineListe = value; }
        }


        /*CTOR*/
        private Singleton()
        {
            Børn = new ObservableCollection<Barn>();
            VaccineCollectionIkkesort = new ObservableCollection<Vaccine>();
            VacPlanCollection = new ObservableCollection<VacSkemaBarnPlan>();
            VaccineCollection = new ObservableCollection<Vaccine>();
            hent();
        }

        public void TilføjBarn(Barn b)
        {
            PersistencyService.PostBarn(b);
            Børn.Add(b);
        }

        public void FjernBarn(Barn b)
        {
            Børn.Remove(b);
            PersistencyService.DeleteBarn(b);
        }


        public async Task HentVacSkema()
        {
            VacPlanCollection.Clear();
            foreach (var item in await PersistencyService.GetVacSkemaBarnPlanAsync())
            {
                VacPlanCollection.Add(item);
            }
        }

        public void hent()
        {
            Børn = PersistencyService.GetBarn();
        }

        public async Task GetVaccineAsync()
        {
            VaccineCollection.Clear();
            foreach (var i in await PersistencyService.GetSorteredeVaccineAsync())
            {
                this.VaccineCollection.Add(i);
            }
        }

        public async Task GetvaccineAsyncIkkeSort()
        {
            VaccineCollectionIkkesort.Clear();
            foreach (var i in await PersistencyService.GetVaccineAsync())
            {
                this.VaccineCollectionIkkesort.Add(i);
            }
        }

        #region onpropertychanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}

