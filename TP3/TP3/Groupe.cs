namespace TP3;

public class Groupe
{
    private string Nom;
    private List<Etudiant> Etudiants;
    public Groupe(string nom, List<Etudiant> etudiants)
    {
        Nom = nom;
        Etudiants = etudiants;
    }
    public string NomGroupe { get => Nom; set => Nom = value; }
    public List<Etudiant> EtudiantsGroupe { get => Etudiants; set => Etudiants = value; }
    public void AjouterEtudiant(Etudiant etudiant)
    {
        Etudiants.Add(etudiant);
    }
    public void AfficherEtudiants()
    {
        foreach (Etudiant etudiant in Etudiants)
        {
            etudiant.Afficher();
        }
    }
    public Etudiant GetEtudiant(int index)
    {
        return Etudiants[index];
    }
    
}