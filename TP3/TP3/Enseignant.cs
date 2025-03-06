namespace TP3;

public class Enseignant:Personnel
{
    private Grade grade;
    private int VolumeHoraire;
    private Dictionary<string,List<Etudiant>> Groups;

    public Enseignant(int code, string nom, string prenom, string bureau, double salaireBase, double prime, Grade grade,
        int volumeHoraire, Dictionary<String, List<Etudiant>> groups) : base(code,
        nom, prenom, bureau, salaireBase, prime)
    {
        this.grade = grade;
        this.VolumeHoraire = volumeHoraire;
        this.Groups = groups;
    }
    public override double CalculSalaire()
    {
        if (grade == Grade.PA)
        {
            return (VolumeHoraire*300)+PrimePersonnel ;
        }
        else  if (grade == Grade.PH)
        {
            return (VolumeHoraire*350)+PrimePersonnel ;
        }
        else if (grade == Grade.PES)
        {
            return (VolumeHoraire*400)+PrimePersonnel ;
        }
        else
        {
            return 0;
        }
    }
    public Grade GradeEnseignant { get => grade; set => grade = value; }
    public int VolumeHoraireEnseignant { get => VolumeHoraire; set => VolumeHoraire = value; }
    public Dictionary<String, List<Etudiant>> GroupsEnseignant { get => Groups; set => Groups = value; }
    public override void Afficher()
    {
        base.Afficher();
        Console.WriteLine($"Grade : {grade} VolumeHoraire : {VolumeHoraire} | Salaire : {CalculSalaire()} ");
    }

    public void AjouterGroupe(Groupe groupe)
    {
       Groups.Add(groupe.NomGroupe,groupe.EtudiantsGroupe);
    }
    
    
    
    
}