using _2.SemesterProjekt.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.SemesterProjekt.Viewmodel
{
    class FamilieViewModel : INotifyPropertyChanged
    {



        //fullprop
        private ObservableCollection<Barn> børnCollection;
        public ObservableCollection<Barn> BørnCollection
        {
            get { return børnCollection; }
            set { børnCollection = value; }
        }

        private ObservableCollection<Barn> visMitBarn;
        public ObservableCollection<Barn> VisMitBarn
        {
            get { return visMitBarn; }
            set
            {
                visMitBarn = value;
                OnPropertyChanged(nameof(VisMitBarn));
            }
        }



        //ctor
        public FamilieViewModel()
        {
            BørnCollection = new ObservableCollection<Barn>();
            BørnCollection = Singleton.VaccAppSingletion.Børn;
            //post
            //put
            //delete
            
            VisMitBarn = new ObservableCollection<Barn>();
            VisMitBarn = BørnCollection;
        }




        //linq
        private void VisBarn()
        {
            var MitBarn = from MBarn
                           in BørnCollection
                           select MBarn;
            VisMitBarn= new ObservableCollection<Barn>(MitBarn);


        }


        #region vores PropertyChangedEventHandler 
        //standard se evt Gist.


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Select event prop & instance field

        private static Barn selectedBarn;
        public Barn SelectedBarn
        {
            get { return selectedBarn; }
            set { selectedBarn = value; OnPropertyChanged(nameof(SelectedBarn)); }
        }

        #endregion
    }
}
