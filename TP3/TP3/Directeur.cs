namespace TP3;

public class Directeur:Personnel
{
    public Directeur(int code, string nom, string prenom, string bureau, double salaireBase, double prime) : base(code,
        nom, prenom, bureau, salaireBase, prime)
    {
    }
    public override double CalculSalaire()
    {
        return SalaireBasePersonnel+PrimePersonnel ;
    }
    public static Directeur GetInstance(int code, string nom, string prenom, string bureau, double salaireBase)
    
}