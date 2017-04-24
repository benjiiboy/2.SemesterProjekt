﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2.SemesterProjekt.Persistency;

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

        public void TilføjBarn(Barn g)
        {
            Barn.Add(g);
        }

        public void FjernBarn(Barn g)
        {
            Barn.Remove(g);
            //TODO: PersistencyService.(opdaterbarnliste)(g); fra persistencyservice classen som kan opdatere tabellen med børn i
        }

        public void HentJson()
        {
           //TODO: barn = PersistencyService.(loadbarnfromjsonasync)();
        }




    }
}
