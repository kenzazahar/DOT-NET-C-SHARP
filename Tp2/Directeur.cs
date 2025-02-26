using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp2_1
{
    class Directeur
    {
        private static Directeur instance;
        private GestionEmp gestionEmployes;

        // Constructeur privé (Singleton)
        private Directeur()
        {
            gestionEmployes = new GestionEmp();
        }

        // Méthode pour récupérer l'instance unique du directeur
        public static Directeur GetInstance()
        {
            if (instance == null)
            {
                instance = new Directeur();
            }
            return instance;
        }

        // Permet de modifier la gestion des employés
        public void SetGestionEmployes(GestionEmp gestion)
        {
            gestionEmployes = gestion;
        }

        // Afficher le salaire total
        public void AfficherSalaireTotal()
        {
            Console.WriteLine($"Salaire total de l'entreprise : {gestionEmployes.SalaireTotal()} DH");
        }

        // Afficher le salaire moyen
        public void AfficherSalaireMoyen()
        {
            Console.WriteLine($"Salaire moyen des employés : {gestionEmployes.SalaireMoyen()}DH");
        }                                                                                                                                                                                       

        // Afficher tous les employés
        public void AfficherEmployes()
        {
            gestionEmployes.AfficherEmployes();
        }
    }
}
