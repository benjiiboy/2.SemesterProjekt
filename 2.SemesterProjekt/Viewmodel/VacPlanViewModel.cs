using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2.SemesterProjekt.Model;

namespace _2.SemesterProjekt.Viewmodel
{
    class VacPlanViewModel : INotifyPropertyChanged
    {

        public Singleton Singleton  { get; set; }

        #region valg af plan
        private VacPlan _valgtPlan;

        public VacPlan ValgtPlan
        {
            get { return _valgtPlan; }
            set
            {
                _valgtPlan = value;
                OnPropertyChanged(nameof(ValgtPlan));
            }
        }
#endregion



        //ctor
        public VacPlanViewModel()
        {

            Singleton = Singleton.VaccAppSingletion;

        }

        #region propchanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion


    }
}
