namespace TP3;

public abstract class Personne
{
    private int Code;
    private string Nom;
    private string Prenom;
    
    public Personne(int code, string nom, string prenom)
    {
        Code = code;
        Nom = nom;
        Prenom = prenom;
    }
    
    public int CodePersonne { get => Code; set => Code = value; }
    public string NomPersonne { get => Nom; set => Nom = value; }
    public string PrenomPersonne { get => Prenom; set => Prenom = value; }

    public virtual void Afficher()
    {
        Console.WriteLine($"Code : {Code} Nom : {Nom} Prenom : {Prenom} ");
    }



}