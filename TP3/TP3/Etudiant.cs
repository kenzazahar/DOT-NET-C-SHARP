namespace TP3;

public class Etudiant : Personne
{
    private int Niveau;
    private double Moyenne;

    public Etudiant(int code, string nom, string prenom, int niveau,
        double moyenne) : base(code, nom, prenom)
    {
        this.Niveau = niveau;
        this.Moyenne = moyenne;
    }
    public int NiveauEtudiant { get => Niveau; set => Niveau = value; }
    public double MoyenneEtudiant { get => Moyenne; set => Moyenne = value; }

    public override void Afficher()
    {
        base.Afficher();
        Console.WriteLine($"Niveau : {Niveau} Moyenne : {Moyenne}");
    }

}