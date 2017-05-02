using _2.SemesterProjekt.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.SemesterProjekt.Viewmodel
{
    class FamilieViewModel : INotifyPropertyChanged
    {

        //prop

        //ctor
        public FamilieViewModel()
        {
               
        }

        //standard se evt Gist.

        #region vores PropertyChangedEventHandler 
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
