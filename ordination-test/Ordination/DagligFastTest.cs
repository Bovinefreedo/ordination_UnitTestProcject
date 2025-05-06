using shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordination_test.Ordination
{
    [TestClass]
    public class DagligFastTest
    {
        [TestMethod]
        public void DagligFastDoegnDosis() { 
            Laegemiddel laegemiddel = new Laegemiddel("Test", 1, 1, 1, "tablet");
            DateTime start = new DateTime(2025, 10, 10, 12, 0, 0);
            DateTime slut = new DateTime(2025, 10, 12, 12, 0, 0);
            DagligFast ordination1 = new DagligFast( start , slut , laegemiddel, 1, 1, 0, 1);
            DagligFast ordination2 = new DagligFast(start, slut, laegemiddel, 3, 4, 10, 1);

            Assert.AreEqual(3, ordination1.doegnDosis());
            Assert.AreEqual(18, ordination2.doegnDosis());
        }

        [TestMethod]
        public void DagligFastSamletDosisTest()
        {
            Laegemiddel laegemiddel = new Laegemiddel("Test", 1, 1, 1, "tablet");
            DateTime start = new DateTime(2025, 10, 10, 12, 0, 0);
            DateTime start2 = new DateTime(2025, 10, 12, 12, 0, 0);
            DateTime slut1 = new DateTime(2025, 10, 12, 12, 0, 0);
            DateTime slut2 = new DateTime(2025, 10, 13, 12, 0, 0);
            DateTime slut3 = new DateTime(2025, 10, 13, 18, 0, 0);
            DagligFast ordination1 = new DagligFast(start, slut1, laegemiddel, 1, 1, 1, 1);
            DagligFast ordination2 = new DagligFast(start, slut2, laegemiddel, 1, 1, 1, 1);
            DagligFast ordination3 = new DagligFast(start, slut3, laegemiddel, 1, 1, 1, 1);
            DagligFast ordination4 = new DagligFast(start2, slut1, laegemiddel, 1, 1, 1, 1);

            Assert.AreEqual(9.0d, ordination1.samletDosis());
            Assert.AreEqual(13.0d, ordination2.samletDosis());
            Assert.AreEqual(14.0d, ordination3.samletDosis());
            Assert.AreEqual(1.0d, ordination4.samletDosis());
        }
        [TestMethod]
        public void negativeNumberOfDoses() { 
            Assert.ThrowsException<ArgumentException>(() => new DagligFast(new DateTime(2025, 10, 10, 12, 0, 0), new DateTime(2025, 10, 12, 12, 0, 0), new Laegemiddel("Test", 1, 1, 1, "tablet"), -1, 0, 0, 0));
            Assert.ThrowsException<ArgumentException>(() => new DagligFast(new DateTime(2025, 10, 10, 12, 0, 0), new DateTime(2025, 10, 12, 12, 0, 0), new Laegemiddel("Test", 1, 1, 1, "tablet"), 0, -1, 0, 0));
            Assert.ThrowsException<ArgumentException>(() => new DagligFast(new DateTime(2025, 10, 10, 12, 0, 0), new DateTime(2025, 10, 12, 12, 0, 0), new Laegemiddel("Test", 1, 1, 1, "tablet"), 0, 0,-1, 0));
            Assert.ThrowsException<ArgumentException>(() => new DagligFast(new DateTime(2025, 10, 10, 12, 0, 0), new DateTime(2025, 10, 12, 12, 0, 0), new Laegemiddel("Test", 1, 1, 1, "tablet"), 0, 0, 0, -1));
        }
    }

   
}
