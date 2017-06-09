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

        private int _TeleFonNr;
        private int barn_Id;
        private DateTimeOffset fødselsdato;
        private string fornavn;
        private string efternavn;
        private ICommand opretBarnCommand;
        private ICommand sletBarnCommand;
        private ICommand seVaccinerCommand;

        public Handler.BarnHandler BarnHandler { get; set; }
        public Singleton Singleton { get; set; }

        public int TelefonNr
        {
            get { return _TeleFonNr; ; }
            set { _TeleFonNr = value; OnPropertyChanged(nameof(TelefonNr)); }
        }

        public int Barn_Id
        {
            get { return barn_Id; ; }
            set { barn_Id = value; OnPropertyChanged(nameof(Barn_Id)); }
        }

        public DateTimeOffset Fødselsdato
        {
            get { return fødselsdato;; }
            set { fødselsdato = value; OnPropertyChanged(nameof(Fødselsdato)); }
        }

        public string ForNavn
        {
            get { return fornavn; }
            set { fornavn = value; OnPropertyChanged(nameof(ForNavn)); }
        }

        public string EfterNavn
        {
            get { return efternavn; }
            set { efternavn = value; OnPropertyChanged(nameof(EfterNavn)); }
        }

        public ICommand OpretBarnCommand
        {
            get { return opretBarnCommand; }
            set { opretBarnCommand = value; }
        }

        public ICommand SletBarnCommand
        {
            get { return sletBarnCommand; }
            set { sletBarnCommand = value; }
        }

        public ICommand SeVaccinerCommand
        {
            get { return seVaccinerCommand; }
            set { seVaccinerCommand = value; }
        }

        public VaccAppViewModel()
        {
            BarnHandler = new Handler.BarnHandler(this);
            Singleton = Singleton.Instance;

            OpretBarnCommand = new RelayCommand(BarnHandler.OpretBarn);
            SletBarnCommand = new RelayCommand(BarnHandler.SletBarn,TomListeCheck);
            //PutBarnCommand = new RelayCommand(BarnHandler.PutBarn,TomListeCheck);
            SeVaccinerCommand = new RelayCommand(BarnHandler.HentVacciner,TomListeCheck);

            DateTimeOffset dt = System.DateTime.Now;
            fødselsdato = new DateTimeOffset(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0, new TimeSpan());
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

        public bool TomListeCheck()
        {
            return Model.Singleton.Instance.Børn.Count() > 0;
        }


    }
}
