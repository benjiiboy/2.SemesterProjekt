using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2.SemesterProjekt.Model;
namespace _2.SemesterProjekt.Viewmodel
{
   public class VaccAppViewModel
    {
        public Singleton Singletion { get; set; }

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


    }
}
