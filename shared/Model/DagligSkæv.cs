using System.Security.Cryptography;

namespace shared.Model;

public class DagligSkæv : Ordination {
    public List<Dosis> doser { get; set; } = new List<Dosis>();

    public DagligSkæv(DateTime startDen, DateTime slutDen, Laegemiddel laegemiddel) : base(laegemiddel, startDen, slutDen) {
	}

    public DagligSkæv(DateTime startDen, DateTime slutDen, Laegemiddel laegemiddel, Dosis[] doser) : base(laegemiddel, startDen, slutDen) {
        this.doser = doser.ToList();
    }    

    public DagligSkæv() : base(null!, new DateTime(), new DateTime()) {
    }

	public void opretDosis(DateTime tid, double antal) {
        if (antal < 0) {
            throw new ArgumentException("Doserne kan ikke være negative");
        }
        doser.Add(new Dosis(tid, antal));
    }

	public void opretDosis(Dosis dosis) {
        opretDosis(dosis.tid, dosis.antal);
    }

	public override double samletDosis() {
		TimeOnly startTime = TimeOnly.FromDateTime(startDen);
        TimeOnly endTime = TimeOnly.FromDateTime(slutDen);
		double adjustTotalDose = 0;
        foreach (Dosis dosis in doser) {
            TimeOnly time = TimeOnly.FromDateTime(dosis.tid);

            if (time < startTime)
            {
                adjustTotalDose -= dosis.antal;
            }
			if(time > endTime){
                adjustTotalDose -= dosis.antal;
            }
        }
		return antalDage() * doegnDosis() + adjustTotalDose;
	}

	public override double doegnDosis() {
		double totalDose = 0;
		foreach (Dosis d in doser) {
			totalDose += d.antal;
		}
        return totalDose;
	}

	public override String getType() {
		return "DagligSkæv";
	}
}
