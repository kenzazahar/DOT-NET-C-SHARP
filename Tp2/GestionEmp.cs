using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp2_1
{
    class GestionEmp
    {
        private List<Employe> employes;

        // Constructeur
        public GestionEmp()
        {
            employes = new List<Employe>();
        }

        // Ajouter un employé
        public void AjouterEmploye(Employe employe)
        {
            employes.Add(employe);
        }

        // Supprimer un employé par son nom
        public void SupprimerEmploye(string nom)
        {
            employes.RemoveAll(e => e.Nom == nom);
        }

        // Calculer le salaire total de l'entreprise
        public double SalaireTotal()
        {
            return employes.Sum(e => e.Salaire);
        }

        // Calculer le salaire moyen
        public double SalaireMoyen()
        {
            return employes.Count > 0 ? employes.Average(e => e.Salaire) : 0;
        }

        // Afficher la liste des employés
        public void AfficherEmployes()
        {
            if (employes.Count == 0)
            {
                Console.WriteLine("Aucun employé enregistré.");
                return;
            }

            Console.WriteLine("Liste des employés :");
            foreach (var employe in employes)
            {
                Console.WriteLine(employe);
            }
        }
    }
}