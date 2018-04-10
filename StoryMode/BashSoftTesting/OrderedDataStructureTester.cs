using System;
using System.Linq;
using BashSoft.Contracts;
using BashSoft.DataStructures;
using NUnit.Framework;

namespace BashSoftTesting
{
    public class OrderedDataStructureTester
    {
        private ISimpleOrderedBag<string> names;

        [Test]
        public void TestEmptyCtor()
        {
            this.names = new SimpleSortedList<string>();
            Assert.AreEqual(this.names.Capacity, 16);
            Assert.AreEqual(this.names.Size, 0);
        }

        [Test]
        public void TestCtorWithInitialCapacity()
        {
            this.names = new SimpleSortedList<string>(20);
            Assert.AreEqual(this.names.Capacity, 20);
            Assert.AreEqual(this.names.Size, 0);
        }

        [Test]
        public void TestCtorWithAllParams()
        {
            this.names = new SimpleSortedList<string>(StringComparer.OrdinalIgnoreCase, 30);
            Assert.AreEqual(this.names.Capacity, 30);
            Assert.AreEqual(this.names.Size, 0);
        }

        [Test]
        public void TestCtorWithInitialComparer()
        {
            this.names = new SimpleSortedList<string>(StringComparer.OrdinalIgnoreCase);
            Assert.AreEqual(this.names.Capacity, 16);
            Assert.AreEqual(this.names.Size, 0);
        }

        [SetUp]
        public void SetUp()
        {
            this.names = new SimpleSortedList<string>();
        }

        [Test]
        public void TestAddIncresesSize()
        {
            this.names.Add("Pesho");
            Assert.AreEqual(1, this.names.Size);
        }

        [Test]
        public void TestAddNullThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => this.names.Add(null));
        }

        [Test]
        public void TestAddUnsortedDataIsHeldSorted()
        {
            this.names.Add("Rosen");
            this.names.Add("Georgi");
            this.names.Add("Balkan");

            var expectedOrder = new string[] {"Balkan", "Georgi", "Rosen"};
            var index = 0;

            foreach (var name in this.names)
            {
                Assert.AreEqual(name, expectedOrder[index]);
                index++;
            }
        }

        [Test]
        public void TestAddingMoreThanInitialCapacity()
        {
            for (int i = 0; i < 17; i++)
            {
                this.names.Add("Element");
            }

            Assert.IsTrue(this.names.Size == 17);
            Assert.IsTrue(this.names.Capacity != 16);
        }

        [Test]
        public void TestAddingAllFromCollectionIncreasesSize()
        {
            var element = new string[] { "1", "2", "3"};
            this.names.AddAll(element);

            Assert.AreEqual(this.names.Size, 3);
        }

        [Test]
        public void TestAddAllNullThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => this.names.AddAll(null));
        }

        [Test]
        public void TestAddAllKeepsSorted()
        {
            var actual = new string[]
            {
                "Rosen", "Georgi","Balkan"
            };

            this.names.AddAll(actual);

            var expectedOrder = new string[] { "Balkan", "Georgi", "Rosen" };
            var index = 0;

            foreach (var name in this.names)
            {
                Assert.AreEqual(name, expectedOrder[index]);
                index++;
            }
        }

        [Test]
        public void TestRemoveValidElementDecreasesSize()
        {
            this.names.Add("1");
            this.names.Remove("1");

            Assert.AreEqual(0, this.names.Size);
        }

        [Test]
        public void TestRemoveValidElementRemovesSelectedOne()
        {
            this.names.Add("Rosen");
            this.names.Add("Georgi");
            this.names.Add("Balkan");

            this.names.Remove("Georgi");

            Assert.IsFalse(this.names.Any(n => n == "Georgi"));
        }

        [Test]
        public void TestRemovingNullThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => this.names.Remove(null));
        }

        [Test]
        public void TestJoinWithNull()
        {
            this.names.Add("Rosen");
            this.names.Add("Georgi");
            this.names.Add("Balkan");

            Assert.Throws<ArgumentNullException>(() => this.names.JoinWith(null));
        }

        [Test]
        public void TestJoinWorksFine()
        {
            this.names.Add("Rosen");
            this.names.Add("Georgi");
            this.names.Add("Balkan");

            var actualResult = this.names.JoinWith(", ");

            var expectedResult = "Balkan, Georgi, Rosen";

            Assert.AreEqual(actualResult, expectedResult);
        }
    }
}
