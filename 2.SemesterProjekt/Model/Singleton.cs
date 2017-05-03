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
        }

        public void FjernBarn(Barn b)
        {
            Børn.Remove(b);
            PersistencyService.PutBarn(b);

            //TODO: PersistencyService.(opdaterbarnliste)(g); fra persistencyservice classen som kan opdatere tabellen med børn i
        }

        public void HentJson()
        {
           //TODO: barn = PersistencyService.(loadbarnfromjsonasync)();
        }




        //test

        public void hent()
        {
            Børn = PersistencyService.GetBarn();
        }



    }
}
