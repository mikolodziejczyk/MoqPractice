
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoqPractice.BL.Tests
{
    [TestFixture]
    class BranchRepositoryMockTests
    {
        BranchRepository branchRepository;

        string[] allBranches = new string[] { "A", "B", "C" };
        string[] company3Branches = new string[] { "X", "Y", "Z" };

        [SetUp]
        public void SetUp()
        {
            
        }

        [Test]
        public void ParameterlessMethod_SetupReturnsSimpleValue_SetupProperly()
        {
            string[] r = this.branchRepository.GetAllBranches();
            Assert.AreEqual(r, this.allBranches);
        }

        [Test]
        public void MethodWithParameters_SetupForAllParameterValue_SetupProperly()
        {
            string[] r = this.branchRepository.GetBranchesForCompany(1, 2);
            Assert.AreEqual(r, this.allBranches);

        }

        [Test]
        public void MethodWithParameters_SetupForASpecificParameterValue_SetupProperly()
        {
            string[] r = this.branchRepository.GetBranchesForCompany(3, 2);
            Assert.AreEqual(r, this.company3Branches);
        }

        [Test]
        public void MethodWithParameters_ReturnsValueCalculatedWithDelegate_SetupProperly()
        {
            string[] r = this.branchRepository.GetBranchesForCompany(3, 10);
            Assert.AreEqual(r, new string[] { "3_10" });
        }

        [Test]
        public void MethodWithParameters_SetupMatchedWithDelegate_SetupProperly()
        {
            string[] r = this.branchRepository.GetBranchesForCompany(11, 1);
            Assert.AreEqual(r, new string[0]);
        }

        [Test]
        public void MethodWithParameters_SetupThrowsExceptionForSpecificParameter_SetupProperly()
        {

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.branchRepository.GetBranchesForCompany(100, 100);
            });
        }

        [Test]
        public void MethodWithOutParameter_SetupReturnsValueForOutParameter_SetupProperly()
        {
            int itemCount;

            branchRepository.GetBranchPage(0, 10, out itemCount);
            Assert.That(itemCount == 100);

        }

        [Test]
        public void Getter_SetupReturnsFixedValue_SetupProperly()
        {
            Assert.That(branchRepository.CompanyCount == 23);
        }

        [Test]
        public void GetterAndSetter_SetupReturnsHasInitialValueAndTracksCurrent_SetupProperly()
        {
            Assert.That(branchRepository.Connection == "TEST");
            branchRepository.Connection = "TEST2";
            Assert.That(branchRepository.Connection == "TEST2");
        }

        [Test]
        public void InternalMethod_SetupReturnsValue_SetupProperly()
        {
            bool r = branchRepository.Ping();
            Assert.That(r == true);
        }

        [Test]
        public void ProtectedMethod_SetupReturnsValue_SetupProperly()
        {
            // branchRepository.RepositoryType calls the protected GetRepositoryType()
            Assert.That(branchRepository.RepositoryType == "MyRepository");
        }

        [Test]
        public void MockedMethodCallback_WrapperMethodCalled_MockedMethodCalledAsExpected()
        {
            List<string> branchesAdded = new List<string>();
            // setup CreateBranch() so that it adds the parameter it has been called with to branchesAdded 

            branchRepository.AddBranches(new string[] { "XXX", "YYY", "ZZZ" });
            Assert.AreEqual(new string[] { "XXX", "YYY", "ZZZ" }, branchesAdded);

            // add verification here
        }

        [Test]
        public void MockedInterface_MethodMocked_ReturnsProperValue()
        {

            // create a mock here
            IGreeter greeter = null; // set to the mock implementation

            // setup GetGreeting() here to return String.Format("Hello {0}!", name);

            string r = greeter.GetGreeting("World");
            Assert.That(r == "Hello World!");
        }

    }

}
