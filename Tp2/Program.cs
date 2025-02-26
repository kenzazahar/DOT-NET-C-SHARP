using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp2_1
{
    class Program
    {
        static void Main(string[] args)
        {
            GestionEmp gestion = new GestionEmp();

            // Ajout d'employés
            gestion.AjouterEmploye(new Employe(1,"Safae", 30000, "Développeur", new DateTime(2020, 5, 15)));
            gestion.AjouterEmploye(new Employe(2,"Soumaia", 3500, "Chef de projet", new DateTime(2019, 3, 10)));
            gestion.AjouterEmploye(new Employe(3,"Ahmed", 2500, "Designer", new DateTime(2021, 8, 20)));
            gestion.AfficherEmployes();
            Console.WriteLine(gestion.SalaireTotal());

            Directeur directeur = Directeur.GetInstance();

            // Associer la gestion des employés au directeur
            directeur.SetGestionEmployes(gestion);

            // Afficher les employés et les statistiques
            directeur.AfficherSalaireTotal();
            directeur.AfficherSalaireMoyen();

        }
    }
}
