using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.SemesterProjekt.Model
{
   public class Singleton
    {


        private ObservableCollection<Barn> barn;

        public  ObservableCollection<Barn> Barn
        {
            get { return barn; }
            set { barn = value; }
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
            Barn = new ObservableCollection<Barn>();
            //TODO: der skal lægges hent json ind her
        }





    }
}
