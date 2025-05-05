using System.Runtime.ExceptionServices;

namespace shared.Model;

public class PN : Ordination {
	public double antalEnheder { get; set; }
    public List<Dato> dates { get; set; } = new List<Dato>();

    public PN (DateTime startDen, DateTime slutDen, double antalEnheder, Laegemiddel laegemiddel) : base(laegemiddel, startDen, slutDen) {
        if (antalEnheder < 0) { 
            throw new ArgumentException("Doserne kan ikke være negative");
        }
        
        this.antalEnheder = antalEnheder;
	}

    public PN() : base(null!, new DateTime(), new DateTime()) {
    }

    /// <summary>
    /// Registrerer at der er givet en dosis på dagen givesDen
    /// Returnerer true hvis givesDen er inden for ordinationens gyldighedsperiode og datoen huskes
    /// Returner false ellers og datoen givesDen ignoreres
    /// </summary>
    public bool givDosis(Dato givesDen) {
        if (givesDen.dato >= startDen && givesDen.dato < slutDen)
        {
            dates.Add(givesDen);
            return true;
        }
        return false;
    }

    public override double doegnDosis() {
        if (dates.Count == 0)
        {
            return 0;
        }

        DateTime firstDose = DateTime.MaxValue;
        DateTime lastDose = DateTime.MinValue;

        foreach (Dato d in dates)
        {
            if (d.dato < firstDose)
            {
                firstDose = d.dato;
            }
            if (d.dato > lastDose)
            {
                lastDose = d.dato;
            }
        }
        int totalDays = (lastDose - firstDose).Days + 1;
        double totalDose = dates.Count * antalEnheder;
        return totalDose/totalDays;
    }


    public override double samletDosis() {
        return dates.Count() * antalEnheder;
    }

    public int getAntalGangeGivet() {
        return dates.Count();
    }

	public override String getType() {
		return "PN";
	}
}
