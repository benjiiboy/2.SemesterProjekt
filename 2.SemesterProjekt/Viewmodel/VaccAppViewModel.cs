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

        private string fornavn;

        public string ForNavn
        {
            get { return fornavn; }
            set { fornavn = value; }
        }

        private string efternavn;

        public string EfterNavn
        {
            get { return efternavn; }
            set { efternavn = value; }
        }

        #region Selectedbarn metode
        /*Valgte barn*/
        private Barn selectedbarn;

        public Barn SelectedBarn
        {
            get { return selectedbarn; }
            set { selectedbarn = value; OnPropertyChanged(nameof(SelectedBarn)); }
        }
        #endregion 
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

            //TODO: insitaliserer knapper 
        }

    }
}
