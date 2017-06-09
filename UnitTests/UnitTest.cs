using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using _2.SemesterProjekt.Persistency;
using System.Collections.ObjectModel;
using _2.SemesterProjekt.Model;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        public Barn Barno { get; set; }
        [TestMethod]
        public void TestGetBarn()
        {
            //Arrange & Act
            var result = PersistencyService.GetBarn();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(ObservableCollection<Barn>), result.GetType());
        }

        [TestMethod]
        public void TestGetVaccine()
        {
            //Arrance & Act
            var result = PersistencyService.GetVaccine();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(ObservableCollection<Vaccine>), result.GetType());
        }

        [TestMethod]
        public void TestGetVacPlan()
        {
            //Arrange & Act
            var result = PersistencyService.GetVacPlan();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(typeof(ObservableCollection<VacPlan>), result.GetType());
        }

    }
}
