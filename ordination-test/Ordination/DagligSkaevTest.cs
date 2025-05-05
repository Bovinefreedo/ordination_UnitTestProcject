using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using shared.Model;

namespace ordination_test.Ordination
{
    [TestClass]
    public class DagligSkaevTest
    {
        [TestMethod]
        public void DagligSkeavDoegnDosisTest()
        {
            Laegemiddel laegemiddel = new Laegemiddel("Test", 1, 1, 1, "tablet");
            DagligSkæv dagligSkaev = new DagligSkæv(new DateTime(2025, 10, 10, 12, 0, 0), new DateTime(2025, 10, 12, 12, 0 ,0), laegemiddel);

            Assert.AreEqual(0.0d, dagligSkaev.doegnDosis());

            dagligSkaev.opretDosis(new DateTime(2025, 10, 10, 12, 0, 1), 1);

            Assert.AreEqual(1.0d, dagligSkaev.doegnDosis());

            dagligSkaev.opretDosis(new DateTime(2025, 10, 14, 11, 59, 59), 2);

            Assert.AreEqual(3.0d, dagligSkaev.doegnDosis());
        }

        [TestMethod]
        public void DagligSkævPositivtAntalDoser() {
            Laegemiddel laegemiddel = new Laegemiddel("Test", 1, 1, 1, "tablet");
            DagligSkæv dagligSkaev = new DagligSkæv(new DateTime(2025, 10, 10, 12, 0, 0), new DateTime(2025, 10, 12, 12, 0, 0), laegemiddel);
            dagligSkaev.opretDosis(new DateTime(2025, 10, 10, 12, 0, 1), 1);

            Assert.ThrowsException<ArgumentException>((() => dagligSkaev.opretDosis(new DateTime(2025, 10, 10, 12, 0, 1), -1)));
        }

        [TestMethod]
        public void DagligSkeavSamletDosisTest()
        {
            Laegemiddel laegemiddel = new Laegemiddel("Test", 1, 1, 1, "tablet");
            DagligSkæv dagligSkaev = new DagligSkæv(new DateTime(2025, 10, 10, 12, 0, 0), new DateTime(2025, 10, 14, 12, 0, 0), laegemiddel);

            Assert.AreEqual(0, dagligSkaev.samletDosis());

            dagligSkaev.opretDosis(new DateTime(2025, 10, 10, 12, 0, 1), 1);
            dagligSkaev.opretDosis(new DateTime(2025, 10, 14, 11, 59, 59), 2);

            Assert.AreEqual(12.0d, dagligSkaev.samletDosis());

            dagligSkaev.opretDosis(new DateTime(1020, 10, 10, 12, 0, 0), 1);

            Assert.AreEqual(17.0d, dagligSkaev.samletDosis());
        }
    }
}
