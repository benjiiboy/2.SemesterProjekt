using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2.SemesterProjekt.Model;
using System.ComponentModel;

namespace _2.SemesterProjekt.Viewmodel
{
   public class VaccAppViewModel : INotifyPropertyChanged
    {
        public Singleton Singleton { get; set; }

        private int fødselsdato;

        public int Fødselsdato
        {
            get { return fødselsdato;; }
            set { fødselsdato = value; }
        }

        private string navn;

        public string Navn
        {
            get { return navn; }
            set { navn = value; }
        }

        /*Valgte barn*/
        private Barn selectedbarn;

        

        public Barn SelectedBarn
        {
            get { return selectedbarn; }
            set { selectedbarn = value; OnPropertyChanged(nameof(SelectedBarn)); }
        }

        #region propertycahnged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion


        //TODO: Mangler at implenmtere Commands til knapper

        public Handler.BarnHandler BarnHandler { get; set; }

        public VaccAppViewModel()
        {
            BarnHandler = new Handler.BarnHandler(this);
            Singleton = Singleton.VaccAppSingletion;
        }

    }
}
