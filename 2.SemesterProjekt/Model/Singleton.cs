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

        public ObservableCollection<Barn> Børn
        {
            get { return børn; }
            set { børn = value; }
        }

        private static Singleton instance;

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

        //Benji ide
        private ObservableCollection<Vaccine> vaccineListe;

        public ObservableCollection<Vaccine> VaccineListe
        {
            get { return vaccineListe; }
            set { vaccineListe = value; }
        }



        /*CTOR*/
        public Singleton()
        {
            Børn = new ObservableCollection<Barn>();
            hent();
            //mik
            VacPlanCollection = new ObservableCollection<VacSkemaBarnPlan>();
            
            //mik vaccine
            VaccineCollection = new ObservableCollection<Model.Vaccine>();
            GetVaccineAsync();

            VaccineCollectionIkkesort = new ObservableCollection<Vaccine>();
            GetvaccineAsyncIkkeSort();
        }

        public void TilføjBarn(Barn b)
        {
            Børn.Add(b);
            PersistencyService.PostBarn(b);
        }

        public void FjernBarn(Barn b)
        {
            Børn.Remove(b);
            PersistencyService.DeleteBarn(b);
        }

        public void PutBarn(Barn BarnToPut)
        {
            PersistencyService.PutBarn(BarnToPut);
            hent();
        }

        public async Task HentVacSkema()
        {
            VacPlanCollection = await PersistencyService.GetVacPlanAsync();
        }

        public void hent()
        {
            Børn = PersistencyService.GetBarn();
        }

        //PropetyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public ObservableCollection<VacSkemaBarnPlan> VacPlanCollection { get; set; }


        //mik vaccine

        public ObservableCollection<Vaccine> VaccineCollection { get; set; }

        public async Task GetVaccineAsync()
        {
            foreach (var i in await PersistencyService.GetSorteredeVaccineAsync())
            {
                this.VaccineCollection.Add(i);
            }
        }


        //ikke sååårt

        public ObservableCollection<Vaccine> VaccineCollectionIkkesort { get; set; }

        public async Task GetvaccineAsyncIkkeSort()
        {
            foreach (var i in await PersistencyService.GetVaccineAsync())
            {
                this.VaccineCollectionIkkesort.Add(i);
            }
        }
    }
}

