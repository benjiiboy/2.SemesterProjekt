using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2.SemesterProjekt.Model;
using System.ComponentModel;
using System.Windows.Input;
using _2.SemesterProjekt.Common;
using System.Collections.ObjectModel;

namespace _2.SemesterProjekt.Viewmodel
{
   public class VaccAppViewModel : INotifyPropertyChanged
    {
        public Singleton Singleton { get; set; }


        private int _TeleFonNr;

        public int TelefonNr
        {
            get { return _TeleFonNr; ; }
            set { _TeleFonNr = value; OnPropertyChanged(nameof(TelefonNr)); }
        }

        private int barn_Id;

        public int Barn_Id
        {
            get { return barn_Id; ; }
            set { barn_Id = value; OnPropertyChanged(nameof(Barn_Id)); }
        }

        private DateTimeOffset fødselsdato;

        public DateTimeOffset Fødselsdato
        {
            get { return fødselsdato;; }
            set { fødselsdato = value; OnPropertyChanged(nameof(Fødselsdato)); }
        }

        private string fornavn;

        public string ForNavn
        {
            get { return fornavn; }
            set { fornavn = value; OnPropertyChanged(nameof(ForNavn)); }
        }

        private string efternavn;

        public string EfterNavn
        {
            get { return efternavn; }
            set { efternavn = value; OnPropertyChanged(nameof(EfterNavn)); }
        }

        #region Selectedbarn metode
        /*Valgte barn*/
        private static Barn selectedbarn;

        public Barn SelectedBarn
        {
            get { return selectedbarn; }
            set { selectedbarn = value; OnPropertyChanged(nameof(SelectedBarn)); }
        }
        #endregion 
        #region propertychanged
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

        public ICommand PutBarnCommand { get; set; }

        private ICommand opretBarnCommand;
        
        public ICommand OpretBarnCommand
        {
            get { return opretBarnCommand; }
            set { opretBarnCommand = value; }
        }

        private ICommand sletBarnCommand;

        public ICommand SletBarnCommand
        {
            get { return sletBarnCommand; }
            set { sletBarnCommand = value; }
        }

        private ICommand seVaccinerCommand;

        public ICommand SeVaccinerCommand
        {
            get { return seVaccinerCommand; }
            set { seVaccinerCommand = value; }
        }


        public Handler.BarnHandler BarnHandler { get; set; }

        public VaccAppViewModel()
        {
            BarnHandler = new Handler.BarnHandler(this);
            Singleton = Singleton.Instance;

            OpretBarnCommand = new RelayCommand(BarnHandler.OpretBarn);
            SletBarnCommand = new RelayCommand(BarnHandler.SletBarn,TomListeCheck);
            PutBarnCommand = new RelayCommand(BarnHandler.PutBarn,TomListeCheck);
            SeVaccinerCommand = new RelayCommand(BarnHandler.HentVacciner,TomListeCheck);

            DateTime dt = System.DateTime.Now;
            fødselsdato = new DateTimeOffset(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0, new TimeSpan());
        }


        public bool TomListeCheck()
        {
            return Model.Singleton.Instance.Børn.Count() > 0;
        }


    }
}
