using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace GestionAbsences
{
    class Program
    {
        static MySqlConnection connexion;
        
        static void Main(string[] args)
        {
            try
            {
                // Établir la connexion à la base de données
                connexion = new MySqlConnection("Server=localhost;Database=ensat;User Id=root;Password=;");
                connexion.Open();
                Console.WriteLine("Connexion à la base de données établie avec succès.");
                
                bool continuer = true;
                while (continuer)
                {
                    Console.WriteLine("\n===== GESTION DES ABSENCES =====");
                    Console.WriteLine("1. Gérer les élèves");
                    Console.WriteLine("2. Gérer les absences");
                    Console.WriteLine("3. Consulter les absences d'un élève par semaine");
                    Console.WriteLine("4. Consulter le nombre total d'absences d'un élève");
                    Console.WriteLine("0. Quitter");
                    Console.Write("Votre choix: ");
                    
                    string choix = Console.ReadLine();
                    
                    switch (choix)
                    {
                        case "1":
                            GererEleves();
                            break;
                        case "2":
                            GererAbsences();
                            break;
                        case "3":
                            ConsulterAbsencesParSemaine();
                            break;
                        case "4":
                            ConsulterTotalAbsences();
                            break;
                        case "0":
                            continuer = false;
                            break;
                        default:
                            Console.WriteLine("Choix invalide. Veuillez réessayer.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur: " + ex.Message);
            }
            finally
            {
                if (connexion != null && connexion.State == ConnectionState.Open)
                {
                    connexion.Close();
                    Console.WriteLine("Connexion à la base de données fermée.");
                }
            }
        }
        
        static void GererEleves()
        {
            Console.WriteLine("\n--- GESTION DES ÉLÈVES ---");
            Console.WriteLine("1. Ajouter un élève");
            Console.WriteLine("2. Modifier un élève");
            Console.WriteLine("3. Supprimer un élève");
            Console.WriteLine("4. Rechercher un élève");
            Console.WriteLine("5. Afficher tous les élèves");
            Console.WriteLine("0. Retour au menu principal");
            Console.Write("Votre choix: ");
            
            string choix = Console.ReadLine();
            
            switch (choix)
            {
                case "1":
                    AjouterEleve();
                    break;
                case "2":
                    ModifierEleve();
                    break;
                case "3":
                    SupprimerEleve();
                    break;
                case "4":
                    RechercherEleve();
                    break;
                case "5":
                    AfficherTousEleves();
                    break;
                case "0":
                    // Retour au menu principal
                    break;
                default:
                    Console.WriteLine("Choix invalide. Veuillez réessayer.");
                    break;
            }
        }
        
        static void GererAbsences()
        {
            Console.WriteLine("\n--- GESTION DES ABSENCES ---");
            Console.WriteLine("1. Ajouter des absences pour un élève");
            Console.WriteLine("2. Modifier des absences pour un élève");
            Console.WriteLine("3. Supprimer des absences pour un élève");
            Console.WriteLine("4. Afficher toutes les absences");
            Console.WriteLine("0. Retour au menu principal");
            Console.Write("Votre choix: ");
            
            string choix = Console.ReadLine();
            
            switch (choix)
            {
                case "1":
                    AjouterAbsences();
                    break;
                case "2":
                    ModifierAbsences();
                    break;
                case "3":
                    SupprimerAbsences();
                    break;
                case "4":
                    AfficherToutesAbsences();
                    break;
                case "0":
                    // Retour au menu principal
                    break;
                default:
                    Console.WriteLine("Choix invalide. Veuillez réessayer.");
                    break;
            }
        }
        
        // Méthodes pour gérer les élèves
        static void AjouterEleve()
        {
            Console.WriteLine("\nAJOUT D'UN ÉLÈVE");
            
            Console.Write("Nom: ");
            string nom = Console.ReadLine();
            
            Console.Write("Prénom: ");
            string prenom = Console.ReadLine();
            
            Console.Write("Groupe: ");
            int groupe;
            while (!int.TryParse(Console.ReadLine(), out groupe))
            {
                Console.Write("Veuillez entrer un nombre valide pour le groupe: ");
            }
            
            try
            {
                MySqlCommand cmd = connexion.CreateCommand();
                cmd.CommandText = "INSERT INTO Eleves (nom, prenom, groupe) VALUES (@nom, @prenom, @groupe)";
                cmd.Parameters.AddWithValue("@nom", nom);
                cmd.Parameters.AddWithValue("@prenom", prenom);
                cmd.Parameters.AddWithValue("@groupe", groupe);
                
                int lignesAffectees = cmd.ExecuteNonQuery();
                if (lignesAffectees > 0)
                {
                    Console.WriteLine("Élève ajouté avec succès.");
                }
                else
                {
                    Console.WriteLine("Échec de l'ajout de l'élève.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de l'ajout de l'élève: " + ex.Message);
            }
        }
        
        static void ModifierEleve()
        {
            Console.WriteLine("\nMODIFICATION D'UN ÉLÈVE");
            
            Console.Write("ID de l'élève à modifier: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("Veuillez entrer un nombre valide pour l'ID: ");
            }
            
            // Vérifier si l'élève existe
            if (!EleveExiste(id))
            {
                Console.WriteLine("Aucun élève trouvé avec cet ID.");
                return;
            }
            
            Console.Write("Nouveau nom (laisser vide pour ne pas modifier): ");
            string nom = Console.ReadLine();
            
            Console.Write("Nouveau prénom (laisser vide pour ne pas modifier): ");
            string prenom = Console.ReadLine();
            
            Console.Write("Nouveau groupe (laisser vide pour ne pas modifier): ");
            string groupeStr = Console.ReadLine();
            int? groupe = null;
            if (!string.IsNullOrEmpty(groupeStr))
            {
                while (!int.TryParse(groupeStr, out int g))
                {
                    Console.Write("Veuillez entrer un nombre valide pour le groupe: ");
                    groupeStr = Console.ReadLine();
                    if (string.IsNullOrEmpty(groupeStr)) break;
                }
                if (!string.IsNullOrEmpty(groupeStr))
                {
                    groupe = int.Parse(groupeStr);
                }
            }
            
            try
            {
                MySqlCommand cmd = connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Eleves WHERE ID = @id";
                cmd.Parameters.AddWithValue("@id", id);
                
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string nomActuel = reader["nom"].ToString();
                        string prenomActuel = reader["prenom"].ToString();
                        int groupeActuel = Convert.ToInt32(reader["groupe"]);
                        
                        reader.Close();
                        
                        cmd = connexion.CreateCommand();
                        cmd.CommandText = "UPDATE Eleves SET nom = @nom, prenom = @prenom, groupe = @groupe WHERE ID = @id";
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@nom", string.IsNullOrEmpty(nom) ? nomActuel : nom);
                        cmd.Parameters.AddWithValue("@prenom", string.IsNullOrEmpty(prenom) ? prenomActuel : prenom);
                        cmd.Parameters.AddWithValue("@groupe", groupe ?? groupeActuel);
                        
                        int lignesAffectees = cmd.ExecuteNonQuery();
                        if (lignesAffectees > 0)
                        {
                            Console.WriteLine("Élève modifié avec succès.");
                        }
                        else
                        {
                            Console.WriteLine("Échec de la modification de l'élève.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Aucun élève trouvé avec cet ID.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la modification de l'élève: " + ex.Message);
            }
        }
        
        static void SupprimerEleve()
        {
            Console.WriteLine("\nSUPPRESSION D'UN ÉLÈVE");
            
            Console.Write("ID de l'élève à supprimer: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("Veuillez entrer un nombre valide pour l'ID: ");
            }
            
            // Vérifier si l'élève existe
            if (!EleveExiste(id))
            {
                Console.WriteLine("Aucun élève trouvé avec cet ID.");
                return;
            }
            
            try
            {
                // Supprimer d'abord les absences associées à cet élève
                MySqlCommand cmdAbsences = connexion.CreateCommand();
                cmdAbsences.CommandText = "DELETE FROM Absences WHERE ID = @id";
                cmdAbsences.Parameters.AddWithValue("@id", id);
                cmdAbsences.ExecuteNonQuery();
                
                // Supprimer l'élève
                MySqlCommand cmdEleve = connexion.CreateCommand();
                cmdEleve.CommandText = "DELETE FROM Eleves WHERE ID = @id";
                cmdEleve.Parameters.AddWithValue("@id", id);
                
                int lignesAffectees = cmdEleve.ExecuteNonQuery();
                if (lignesAffectees > 0)
                {
                    Console.WriteLine("Élève supprimé avec succès.");
                }
                else
                {
                    Console.WriteLine("Échec de la suppression de l'élève.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la suppression de l'élève: " + ex.Message);
            }
        }
        
        static void RechercherEleve()
        {
            Console.WriteLine("\nRECHERCHE D'UN ÉLÈVE");
            
            Console.Write("ID de l'élève à rechercher: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("Veuillez entrer un nombre valide pour l'ID: ");
            }
            
            try
            {
                MySqlCommand cmd = connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Eleves WHERE ID = @id";
                cmd.Parameters.AddWithValue("@id", id);
                
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine("\nDétails de l'élève:");
                        Console.WriteLine($"ID: {reader["ID"]}");
                        Console.WriteLine($"Nom: {reader["nom"]}");
                        Console.WriteLine($"Prénom: {reader["prenom"]}");
                        Console.WriteLine($"Groupe: {reader["groupe"]}");
                    }
                    else
                    {
                        Console.WriteLine("Aucun élève trouvé avec cet ID.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la recherche de l'élève: " + ex.Message);
            }
        }
        
        static void AfficherTousEleves()
        {
            Console.WriteLine("\nLISTE DE TOUS LES ÉLÈVES");
            
            try
            {
                MySqlCommand cmd = connexion.CreateCommand();
                cmd.CommandText = "SELECT * FROM Eleves ORDER BY ID";
                
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        Console.WriteLine("\nID | Nom | Prénom | Groupe");
                        Console.WriteLine("--------------------------------");
                        
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["ID"]} | {reader["nom"]} | {reader["prenom"]} | {reader["groupe"]}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Aucun élève trouvé dans la base de données.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de l'affichage des élèves: " + ex.Message);
            }
        }
        
        // Méthodes pour gérer les absences
        static void AjouterAbsences()
        {
            Console.WriteLine("\nAJOUT D'ABSENCES");
            
            Console.Write("ID de l'élève: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("Veuillez entrer un nombre valide pour l'ID: ");
            }
            
            // Vérifier si l'élève existe
            if (!EleveExiste(id))
            {
                Console.WriteLine("Aucun élève trouvé avec cet ID.");
                return;
            }
            
            Console.Write("Numéro de semaine (1-52): ");
            int numSemaine;
            while (!int.TryParse(Console.ReadLine(), out numSemaine) || numSemaine < 1 || numSemaine > 52)
            {
                Console.Write("Veuillez entrer un nombre valide pour la semaine (1-52): ");
            }
            
            Console.Write("Nombre d'absences: ");
            int nbrAbsences;
            while (!int.TryParse(Console.ReadLine(), out nbrAbsences) || nbrAbsences < 0)
            {
                Console.Write("Veuillez entrer un nombre valide pour les absences: ");
            }
            
            try
            {
                // Vérifier si une entrée existe déjà pour cet élève et cette semaine
                MySqlCommand checkCmd = connexion.CreateCommand();
                checkCmd.CommandText = "SELECT * FROM Absences WHERE ID = @id AND Num_semaine = @numSemaine";
                checkCmd.Parameters.AddWithValue("@id", id);
                checkCmd.Parameters.AddWithValue("@numSemaine", numSemaine);
                
                bool entreeExiste = false;
                using (MySqlDataReader reader = checkCmd.ExecuteReader())
                {
                    entreeExiste = reader.HasRows;
                }
                
                MySqlCommand cmd = connexion.CreateCommand();
                if (entreeExiste)
                {
                    // Mettre à jour l'entrée existante
                    cmd.CommandText = "UPDATE Absences SET Nbr_absences = @nbrAbsences WHERE ID = @id AND Num_semaine = @numSemaine";
                }
                else
                {
                    // Créer une nouvelle entrée
                    cmd.CommandText = "INSERT INTO Absences (ID, Num_semaine, Nbr_absences) VALUES (@id, @numSemaine, @nbrAbsences)";
                }
                
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@numSemaine", numSemaine);
                cmd.Parameters.AddWithValue("@nbrAbsences", nbrAbsences);
                
                int lignesAffectees = cmd.ExecuteNonQuery();
                if (lignesAffectees > 0)
                {
                    Console.WriteLine("Absences ajoutées avec succès.");
                }
                else
                {
                    Console.WriteLine("Échec de l'ajout des absences.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de l'ajout des absences: " + ex.Message);
            }
        }
        
        static void ModifierAbsences()
        {
            Console.WriteLine("\nMODIFICATION D'ABSENCES");
            
            Console.Write("ID de l'élève: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("Veuillez entrer un nombre valide pour l'ID: ");
            }
            
            // Vérifier si l'élève existe
            if (!EleveExiste(id))
            {
                Console.WriteLine("Aucun élève trouvé avec cet ID.");
                return;
            }
            
            Console.Write("Numéro de semaine (1-52): ");
            int numSemaine;
            while (!int.TryParse(Console.ReadLine(), out numSemaine) || numSemaine < 1 || numSemaine > 52)
            {
                Console.Write("Veuillez entrer un nombre valide pour la semaine (1-52): ");
            }
            
            // Vérifier si des absences existent pour cet élève et cette semaine
            bool absencesExistent = AbsencesExistent(id, numSemaine);
            if (!absencesExistent)
            {
                Console.WriteLine("Aucune absence trouvée pour cet élève et cette semaine.");
                return;
            }
            
            Console.Write("Nouveau nombre d'absences: ");
            int nbrAbsences;
            while (!int.TryParse(Console.ReadLine(), out nbrAbsences) || nbrAbsences < 0)
            {
                Console.Write("Veuillez entrer un nombre valide pour les absences: ");
            }
            
            try
            {
                MySqlCommand cmd = connexion.CreateCommand();
                cmd.CommandText = "UPDATE Absences SET Nbr_absences = @nbrAbsences WHERE ID = @id AND Num_semaine = @numSemaine";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@numSemaine", numSemaine);
                cmd.Parameters.AddWithValue("@nbrAbsences", nbrAbsences);
                
                int lignesAffectees = cmd.ExecuteNonQuery();
                if (lignesAffectees > 0)
                {
                    Console.WriteLine("Absences modifiées avec succès.");
                }
                else
                {
                    Console.WriteLine("Échec de la modification des absences.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la modification des absences: " + ex.Message);
            }
        }
        
        static void SupprimerAbsences()
        {
            Console.WriteLine("\nSUPPRESSION D'ABSENCES");
            
            Console.Write("ID de l'élève: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("Veuillez entrer un nombre valide pour l'ID: ");
            }
            
            // Vérifier si l'élève existe
            if (!EleveExiste(id))
            {
                Console.WriteLine("Aucun élève trouvé avec cet ID.");
                return;
            }
            
            Console.Write("Numéro de semaine (1-52): ");
            int numSemaine;
            while (!int.TryParse(Console.ReadLine(), out numSemaine) || numSemaine < 1 || numSemaine > 52)
            {
                Console.Write("Veuillez entrer un nombre valide pour la semaine (1-52): ");
            }
            
            // Vérifier si des absences existent pour cet élève et cette semaine
            bool absencesExistent = AbsencesExistent(id, numSemaine);
            if (!absencesExistent)
            {
                Console.WriteLine("Aucune absence trouvée pour cet élève et cette semaine.");
                return;
            }
            
            try
            {
                MySqlCommand cmd = connexion.CreateCommand();
                cmd.CommandText = "DELETE FROM Absences WHERE ID = @id AND Num_semaine = @numSemaine";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@numSemaine", numSemaine);
                
                int lignesAffectees = cmd.ExecuteNonQuery();
                if (lignesAffectees > 0)
                {
                    Console.WriteLine("Absences supprimées avec succès.");
                }
                else
                {
                    Console.WriteLine("Échec de la suppression des absences.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la suppression des absences: " + ex.Message);
            }
        }
        
        static void AfficherToutesAbsences()
        {
            Console.WriteLine("\nLISTE DE TOUTES LES ABSENCES");
            
            try
            {
                MySqlCommand cmd = connexion.CreateCommand();
                cmd.CommandText = @"
                    SELECT a.ID, e.nom, e.prenom, a.Num_semaine, a.Nbr_absences 
                    FROM Absences a
                    JOIN Eleves e ON a.ID = e.ID
                    ORDER BY a.Num_semaine, a.ID";
                
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        Console.WriteLine("\nID | Nom | Prénom | Semaine | Nombre d'absences");
                        Console.WriteLine("------------------------------------------------");
                        
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["ID"]} | {reader["nom"]} | {reader["prenom"]} | {reader["Num_semaine"]} | {reader["Nbr_absences"]}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Aucune absence trouvée dans la base de données.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de l'affichage des absences: " + ex.Message);
            }
        }
        
        // Méthodes de consultation
        static void ConsulterAbsencesParSemaine()
        {
            Console.WriteLine("\nCONSULTATION DES ABSENCES PAR SEMAINE");
            
            Console.Write("ID de l'élève: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("Veuillez entrer un nombre valide pour l'ID: ");
            }
            
            // Vérifier si l'élève existe
            if (!EleveExiste(id))
            {
                Console.WriteLine("Aucun élève trouvé avec cet ID.");
                return;
            }
            
            Console.Write("Numéro de semaine (1-52): ");
            int numSemaine;
            while (!int.TryParse(Console.ReadLine(), out numSemaine) || numSemaine < 1 || numSemaine > 52)
            {
                Console.Write("Veuillez entrer un nombre valide pour la semaine (1-52): ");
            }
            
            try
            {
                // Récupérer les informations de l'élève
                MySqlCommand cmdEleve = connexion.CreateCommand();
                cmdEleve.CommandText = "SELECT * FROM Eleves WHERE ID = @id";
                cmdEleve.Parameters.AddWithValue("@id", id);
                
                string nom = "";
                string prenom = "";
                
                using (MySqlDataReader readerEleve = cmdEleve.ExecuteReader())
                {
                    if (readerEleve.Read())
                    {
                        nom = readerEleve["nom"].ToString();
                        prenom = readerEleve["prenom"].ToString();
                    }
                }
                
                // Récupérer les absences pour la semaine spécifiée
                MySqlCommand cmdAbsences = connexion.CreateCommand();
                cmdAbsences.CommandText = "SELECT * FROM Absences WHERE ID = @id AND Num_semaine = @numSemaine";
                cmdAbsences.Parameters.AddWithValue("@id", id);
                cmdAbsences.Parameters.AddWithValue("@numSemaine", numSemaine);
                
                using (MySqlDataReader readerAbsences = cmdAbsences.ExecuteReader())
                {
                    Console.WriteLine($"\nAbsences pour {prenom} {nom} (ID: {id}) - Semaine {numSemaine}:");
                    
                    if (readerAbsences.Read())
                    {
                        Console.WriteLine($"Nombre d'absences: {readerAbsences["Nbr_absences"]}");
                    }
                    else
                    {
                        Console.WriteLine("Aucune absence enregistrée pour cette semaine.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la consultation des absences: " + ex.Message);
            }
        }
        
        static void ConsulterTotalAbsences()
        {
            Console.WriteLine("\nCONSULTATION DU TOTAL DES ABSENCES");
            
            Console.Write("ID de l'élève: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("Veuillez entrer un nombre valide pour l'ID: ");
            }
            
            // Vérifier si l'élève existe
            if (!EleveExiste(id))
            {
                Console.WriteLine("Aucun élève trouvé avec cet ID.");
                return;
            }
            
            try
            {
                // Récupérer les informations de l'élève
                MySqlCommand cmdEleve = connexion.CreateCommand();
                cmdEleve.CommandText = "SELECT * FROM Eleves WHERE ID = @id";
                cmdEleve.Parameters.AddWithValue("@id", id);
                
                string nom = "";
                string prenom = "";
                
                using (MySqlDataReader readerEleve = cmdEleve.ExecuteReader())
                {
                    if (readerEleve.Read())
                    {
                        nom = readerEleve["nom"].ToString();
                        prenom = readerEleve["prenom"].ToString();
                    }
                }
                
                // Calculer le total des absences
                MySqlCommand cmdTotal = connexion.CreateCommand();
                cmdTotal.CommandText = "SELECT SUM(Nbr_absences) as total FROM Absences WHERE ID = @id";
                cmdTotal.Parameters.AddWithValue("@id", id);
                
                object result = cmdTotal.ExecuteScalar();
                int totalAbsences = result == DBNull.Value ? 0 : Convert.ToInt32(result);
                
                Console.WriteLine($"\nTotal des absences pour {prenom} {nom} (ID: {id}): {totalAbsences}");
                
                // Afficher le détail des absences par semaine
                MySqlCommand cmdDetails = connexion.CreateCommand();
                cmdDetails.CommandText = "SELECT * FROM Absences WHERE ID = @id ORDER BY Num_semaine";
                cmdDetails.Parameters.AddWithValue("@id", id);
                
                using (MySqlDataReader readerDetails = cmdDetails.ExecuteReader())
                {
                    if (readerDetails.HasRows)
                    {
                        Console.WriteLine("\nDétail par semaine:");
                        Console.WriteLine("Semaine | Nombre d'absences");
                        Console.WriteLine("------------------------");
                        
                        while (readerDetails.Read())
                        {
                            Console.WriteLine($"{readerDetails["Num_semaine"]} | {readerDetails["Nbr_absences"]}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Aucune absence enregistrée pour cet élève.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la consultation du total des absences: " + ex.Message);
            }
        }
        
        // Méthodes utilitaires
        static bool EleveExiste(int id)
        {
            try
            {
                MySqlCommand cmd = connexion.CreateCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM Eleves WHERE ID = @id";
                cmd.Parameters.AddWithValue("@id", id);
                
                long count = (long)cmd.ExecuteScalar();
                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la vérification de l'existence de l'élève: " + ex.Message);
                return false;
            }
        }
        
        static bool AbsencesExistent(int id, int numSemaine)
        {
            try
            {
                MySqlCommand cmd = connexion.CreateCommand();
                cmd.CommandText = "SELECT COUNT(*) FROM Absences WHERE ID = @id AND Num_semaine = @numSemaine";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@numSemaine", numSemaine);
                
                long count = (long)cmd.ExecuteScalar();
                return count > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors de la vérification de l'existence des absences: " + ex.Message);
                return false;
            }
        }
    }
}