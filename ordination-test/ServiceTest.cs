namespace ordination_test;

using Microsoft.EntityFrameworkCore;
using Service;
using Data;
using shared.Model;

[TestClass]
public class ServiceTest
{
    private DataService service;

    [TestInitialize]
    public void SetupBeforeEachTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrdinationContext>();
        optionsBuilder.UseInMemoryDatabase(databaseName: "test-database");
        var context = new OrdinationContext(optionsBuilder.Options);
        service = new DataService(context);
        service.SeedData();
    }

    [TestMethod]
    public void PatientsExist()
    {
        Assert.IsNotNull(service.GetPatienter());
    }

    [TestMethod]
    public void OpretDagligFast()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        Assert.AreEqual(1, service.GetDagligFaste().Count());

        DagligFast dagligFast =service.OpretDagligFast(patient.PatientId, lm.LaegemiddelId,
            2, 2, 1, 0, DateTime.Now, DateTime.Now.AddDays(3));

        Assert.IsTrue(dagligFast.OrdinationId > 0);
        Assert.AreEqual(2, service.GetDagligFaste().Count());
    }

    [TestMethod]
    public void OpretDagligSkæv() {

        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        Assert.AreEqual(1, service.GetDagligSkæve().Count());

        Dosis[] doser = new Dosis [] { 
            new Dosis(DateTime.Now, 1),
            new Dosis(DateTime.Now.AddHours(1), 2),
            new Dosis(DateTime.Now.AddHours(2), 3),
        };

        DagligSkæv skæv = service.OpretDagligSkaev(patient.PatientId, lm.LaegemiddelId, doser, DateTime.Now, DateTime.Now.AddDays(3));

        Assert.IsTrue(skæv.OrdinationId > 0);
        Assert.AreEqual(2, service.GetDagligSkæve().Count());
    }

    [TestMethod]
    public void OpretPN()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        Assert.AreEqual(4, service.GetPNs().Count());

        PN pn = service.OpretPN(patient.PatientId, lm.LaegemiddelId, 2, DateTime.Now, DateTime.Now.AddDays(3));

        Assert.IsTrue(pn.OrdinationId > 0);
        Assert.AreEqual(5, service.GetPNs().Count());
    }

    [TestMethod]
    public void AnvendPN()
    {
        Patient patient = service.GetPatienter().First();
        Laegemiddel lm = service.GetLaegemidler().First();

        PN pn = service.OpretPN(patient.PatientId, lm.LaegemiddelId, 2, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(3));

        int id = pn.OrdinationId;

        Assert.AreEqual(0, pn.getAntalGangeGivet());

        string response = service.AnvendOrdination(id, new Dato { dato = DateTime.Now });

        Assert.AreEqual("dosis have been given" , response);

        pn = service.GetPNs().FirstOrDefault(x => x.OrdinationId == id);

        Assert.AreEqual(1, pn.getAntalGangeGivet());
    }

    [TestMethod]
    public void weightTest() { 
        Patient p1 = service.GetPatienter().FirstOrDefault((x => x.navn == "Jane Jensen"));
        Patient p2 = service.GetPatienter().FirstOrDefault((x => x.navn == "Hans Jørgensen"));
        Patient p3 = service.GetPatienter().FirstOrDefault((x => x.navn == "Ib Hansen"));


        Laegemiddel lm = service.GetLaegemidler().FirstOrDefault((x => x.navn == "Prednisolon"));

        Assert.AreEqual(0.1d * 19, service.GetAnbefaletDosisPerDøgn(p1.PatientId, lm.LaegemiddelId));
        Assert.AreEqual(0.15d * 89, service.GetAnbefaletDosisPerDøgn(p2.PatientId, lm.LaegemiddelId));
        Assert.AreEqual(0.2d * 130, service.GetAnbefaletDosisPerDøgn(p3.PatientId, lm.LaegemiddelId));
    }

}