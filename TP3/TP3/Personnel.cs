namespace TP3;

public abstract class Personnel : Personne
{
    private string Bureau;
    private double SalaireBase;
    private double Prime;

    public Personnel(int code, string nom, string prenom, string bureau, double salaireBase, double prime) : base(code,
        nom, prenom)
    {
        this.Bureau = bureau;
        this.SalaireBase = salaireBase;
        this.Prime = prime;
    }
    public string BureauPersonnel { get => Bureau; set => Bureau = value; }
    public double SalaireBasePersonnel { get => SalaireBase; set => SalaireBase = value; }
    public double PrimePersonnel { get => Prime; set => Prime = value; }
    
    public override void Afficher()
    {
        base.Afficher();
        Console.WriteLine($"Bureau : {Bureau} SalaireBase : {SalaireBase} Prime : {Prime}");
    }
    public abstract double CalculSalaire();
}