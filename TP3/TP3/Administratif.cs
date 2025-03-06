namespace TP3;

public class Administratif : Personnel
{
    public Administratif(int code, string nom, string prenom, string bureau, double salaireBase, double prime) : base(code,
        nom, prenom, bureau, salaireBase, prime)
    {
    }
    public override double CalculSalaire()
    {
        return SalaireBasePersonnel ;
    }
    
}