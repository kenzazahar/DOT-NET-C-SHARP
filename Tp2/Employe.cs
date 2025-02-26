using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp2_1
{
    class Employe
    {
        int id;
        string nom;
        double salaire;
        string poste;
        DateTime date_embauche;

        

        public Employe(int id, string nom, double salaire, string poste, DateTime dateEmbauche)
        {
            this.id = id;
            this.nom = nom;
            this.salaire = salaire;
            this.poste = poste;
            this.date_embauche = dateEmbauche;
        }

        public double Salaire
        {
            get { return salaire; }
            set { salaire = value; }
        }
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }
        public string Poste
        {
            get { return poste; }
            set { poste = value; }
        }
        public override string ToString()
        {
            return $"Nom: {nom}, Salaire: {salaire} Dh, Poste: {poste}, Date d'embauche: {date_embauche.ToShortDateString()}";
        }
    }
} 

