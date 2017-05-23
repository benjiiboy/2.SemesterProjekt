using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.SemesterProjekt.Model
{
   public class Barn
    {
        private string forNavn;
        private string efterNavn;


        public int Barn_Id { get; set; }


        public string Fornavn
        {
            get { return forNavn; }
            set {
                if (string.IsNullOrWhiteSpace(value))
                
                    throw new ArgumentException("Husk Fornavn");
                    forNavn = value;
            }
        }

        public string Efternavn
        {
            get { return efterNavn; }
            set {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Husk Efternavn");
                efterNavn = value;

                
            }
        }

        public DateTime Fødselsdato { get; set; }
        public int TelefonNr { get; set; }

        public Barn(int id, string fornavn, string efternavn, DateTime føds, int tlf)
        {
            this.Barn_Id = id;
            this.Fornavn = fornavn;
            this.Efternavn = efternavn;
            this.Fødselsdato = føds;
            this.TelefonNr = tlf;
        }

        public Barn()
        {
        }

    }
}
