namespace shared.Model;

using static shared.Util;

public class DagligFast : Ordination
{

    public Dosis MorgenDosis { get; set; } = new Dosis();
    public Dosis MiddagDosis { get; set; } = new Dosis();
    public Dosis AftenDosis { get; set; } = new Dosis();
    public Dosis NatDosis { get; set; } = new Dosis();

    public DagligFast(DateTime startDen, DateTime slutDen, Laegemiddel laegemiddel, double morgenAntal, double middagAntal, double aftenAntal, double natAntal) : base(laegemiddel, startDen, slutDen)
    {
        if (morgenAntal < 0 || middagAntal < 0 || aftenAntal < 0 || natAntal < 0) { 
            throw new ArgumentException("Doserne kan ikke være negative");
        }
        MorgenDosis = new Dosis(CreateTimeOnly(6, 0, 0), morgenAntal);
        MiddagDosis = new Dosis(CreateTimeOnly(12, 0, 0), middagAntal);
        AftenDosis = new Dosis(CreateTimeOnly(18, 0, 0), aftenAntal);
        NatDosis = new Dosis(CreateTimeOnly(23, 59, 0), natAntal);
    }

    public DagligFast() : base(null!, new DateTime(), new DateTime())
    {
    }

    public override double samletDosis()
    {
        TimeOnly endTime = TimeOnly.FromDateTime(slutDen);
        TimeOnly startTime = TimeOnly.FromDateTime(startDen);
        TimeOnly morningTime = new TimeOnly(6, 0, 0);
        TimeOnly noonTime = new TimeOnly(12, 0, 0);
        TimeOnly eveningTime = new TimeOnly(18, 0, 0);
        TimeOnly nightTime = new TimeOnly(23, 59, 0);
        double doseAdjustment = 0;
        if (startTime > morningTime)
        {
            doseAdjustment -= MorgenDosis.antal;
        }
        if (startTime > noonTime)
        {
            doseAdjustment -= MiddagDosis.antal;
        }
        if (startTime > eveningTime)
        {
            doseAdjustment -= AftenDosis.antal;
        }
        if (startTime > nightTime) {
            doseAdjustment -= NatDosis.antal;
        }
        if (endTime < morningTime)
        {
            doseAdjustment -= MorgenDosis.antal;
        }
        if (endTime < noonTime)
        {
            doseAdjustment -= MiddagDosis.antal;
        }
        if (endTime < eveningTime)
        {
            doseAdjustment -= AftenDosis.antal;
        }
        if (endTime < nightTime)
        {
            doseAdjustment -= NatDosis.antal;
        }
        double totalDose = doegnDosis() * antalDage() + doseAdjustment;

        return totalDose;
    }

    public override double doegnDosis()
    {
        double totalDoegnDosis = MorgenDosis.antal + MiddagDosis.antal + AftenDosis.antal + NatDosis.antal;
        return totalDoegnDosis;
    }

    public Dosis[] getDoser()
    {
        Dosis[] doser = { MorgenDosis, MiddagDosis, AftenDosis, NatDosis };
        return doser;
    }

    public override String getType()
    {
        return "DagligFast";
    }
}
