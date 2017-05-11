using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2.SemesterProjekt.Model
{
   public class Barn
    {
        public int Barn_Id { get; set; }

        private string forNavn;
        public string Fornavn
        {
            get { return forNavn; }
            set {
                if (string.IsNullOrWhiteSpace(value))
                
                    throw new ArgumentException("Husk Fornavn");
                    forNavn = value;
            }
        }

        private string efterNavn;
        public string Efternavn
        {
            get { return efterNavn; }
            set {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Husk Efternavn");
                efterNavn = value;

                
            }
        }


        //husk år først altså fx 19921210 for 12 10 1992

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

        public override string ToString()
        {
            return $"{Barn_Id} {Fornavn} {Efternavn}";
        }

    }

    
}
