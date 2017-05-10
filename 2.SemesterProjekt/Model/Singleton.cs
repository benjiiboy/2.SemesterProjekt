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

        private static Singleton vaccAppSingleton;

        public static Singleton VaccAppSingletion
        {
            get
            {
                if (vaccAppSingleton == null)
                {
                    vaccAppSingleton = new Singleton();
                }
                return vaccAppSingleton;
            }
        }

        /*CTOR*/
        public Singleton()
        {
            Børn = new ObservableCollection<Barn>();
            hent();
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

        public void HentJson()
        {
            Børn = PersistencyService.GetBarn();

           //TODO: barn = PersistencyService.(loadbarnfromjsonasync)();
        }




        //test

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

    }
}
