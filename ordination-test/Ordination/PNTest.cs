using shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordination_test.Ordination
{
    [TestClass]
    public class PNTest
    {
        [TestMethod]
        public void testValidDate() { 
            Laegemiddel lm = new Laegemiddel("Test", 1, 1, 1, "Test");
            PN pn = new PN(new DateTime(2025, 1, 1, 10, 0, 0), new DateTime(2025, 1, 10, 10, 0, 0), 2 , lm);
            Dato d1 = new Dato
            {
                DatoId = 1,
                dato = new DateTime(2025, 1, 1, 10, 0, 0)
            };
            Dato d2 = new Dato
            {
                DatoId = 1,
                dato = new DateTime(2025, 1, 10, 10, 0, 0)
            };
            Dato d3 = new Dato
            {
                DatoId = 1,
                dato = new DateTime(2025, 1, 1, 9, 59, 59)
            };
            Dato d4 = new Dato
            {
                DatoId = 1,
                dato = new DateTime(2025, 1, 10, 9, 59, 59)
            };

            Assert.IsTrue(pn.givDosis(d1));
            Assert.IsTrue(pn.givDosis(d4));
            Assert.IsFalse(pn.givDosis(d2));
            Assert.IsFalse(pn.givDosis(d3));
        }

        [TestMethod]
        public void samletDosisTest() {
            Laegemiddel lm = new Laegemiddel("Test", 1, 1, 1, "Test");
            PN pn = new PN(new DateTime(2025, 1, 1, 10, 0, 0), new DateTime(2025, 1, 10, 10, 0, 0), 2, lm);

            Dato d1 = new Dato
            {
                DatoId = 1,
                dato = new DateTime(2025, 1, 1, 10, 0, 0)
            };
            Dato d2 = new Dato
            {
                DatoId = 1,
                dato = new DateTime(2025, 1, 10, 10, 0, 0)
            };

            Assert.AreEqual(0, pn.samletDosis());
            pn.givDosis(d1);
            Assert.AreEqual(2, pn.samletDosis());
            pn.givDosis(d2);
            Assert.AreEqual(2, pn.samletDosis());
        }

        [TestMethod]
        public void antalGangeTest() {
            Laegemiddel lm = new Laegemiddel("Test", 1, 1, 1, "Test");
            PN pn = new PN(new DateTime(2025, 1, 1, 10, 0, 0), new DateTime(2025, 1, 10, 10, 0, 0), 2, lm);

            Dato d1 = new Dato
            {
                DatoId = 1,
                dato = new DateTime(2025, 1, 1, 10, 0, 0)
            };
            Dato d2 = new Dato
            {
                DatoId = 1,
                dato = new DateTime(2025, 1, 10, 10, 0, 0)
            };

            Assert.AreEqual(0, pn.getAntalGangeGivet());
            pn.givDosis(d1);
            Assert.AreEqual(1, pn.getAntalGangeGivet());
            pn.givDosis(d2);
            Assert.AreEqual(1, pn.getAntalGangeGivet());
        }

        [TestMethod]
        public void doegnDosisTest()
        {
            Laegemiddel lm = new Laegemiddel("Test", 1, 1, 1, "Test");
            PN pn = new PN(new DateTime(2025, 1, 1, 10, 0, 0), new DateTime(2025, 1, 10, 10, 0, 0), 2, lm);

            Dato d1 = new Dato
            {
                DatoId = 1,
                dato = new DateTime(2025, 1, 1, 10, 0, 0)
            };
            Dato d2 = new Dato
            {
                DatoId = 1,
                dato = new DateTime(2025, 1, 10, 9, 59, 59)
            };

            Assert.AreEqual(0.0d, pn.doegnDosis());
            pn.givDosis(d1);
            Assert.AreEqual(2.0d / (1), pn.doegnDosis());
            pn.givDosis(d2);
            Assert.AreEqual(4.0d / (9), pn.doegnDosis());
        }

        [TestMethod]
        public void notNegativeDose() {
            Laegemiddel lm = new Laegemiddel("Test", 1, 1, 1, "Test");
            DateTime start = new DateTime(2025, 10, 10, 12, 0 ,0);
            DateTime end = new DateTime(2025, 10, 10, 14, 0, 0);
            Assert.ThrowsException<ArgumentException>(()=> new PN(start, end, -1, lm));
        }
    }
}
