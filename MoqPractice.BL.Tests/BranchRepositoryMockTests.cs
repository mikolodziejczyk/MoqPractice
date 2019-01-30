using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq.Protected;

namespace MoqPractice.BL.Tests
{
    [TestFixture]
    class BranchRepositoryMockTests
    {
        BranchRepository branchRepository;
        Mock<BranchRepository> branchRepositoryMock;
        string[] allBranches = new string[] { "A", "B", "C" };
        string[] company3Branches = new string[] { "X", "Y", "Z" };

        [SetUp]
        public void SetUp()
        {
            branchRepositoryMock = new Mock<BranchRepository>(MockBehavior.Loose);

            branchRepositoryMock.Setup(x => x.GetAllBranches()).Returns(this.allBranches);
            branchRepositoryMock.Setup(x => x.GetBranchesForCompany(It.IsAny<int>(), It.IsAny<int>())).Returns(this.allBranches);
            branchRepositoryMock.Setup(x => x.GetBranchesForCompany(3, It.IsAny<int>())).Returns(this.company3Branches);
            branchRepositoryMock.Setup(x => x.GetBranchesForCompany(It.IsAny<int>(), 10)).Returns((int companyId, int branchType) => new string[] { String.Format("{0}_{1}", companyId, branchType) });
            branchRepositoryMock.Setup(x => x.GetBranchesForCompany(It.Is<int>(cid => cid > 10), It.IsAny<int>())).Returns(new string[0]);
            branchRepositoryMock.Setup(x => x.GetBranchesForCompany(100, It.IsAny<int>())).Throws(new InvalidOperationException("Company doesn't exist"));
            int itemCount = 100;
            branchRepositoryMock.Setup(x => x.GetBranchPage(It.IsAny<int>(), It.IsAny<int>(), out itemCount)).Returns(this.allBranches);
            branchRepositoryMock.Setup(x => x.CompanyCount).Returns(23);
            branchRepositoryMock.SetupProperty(x => x.Connection, "TEST");
            branchRepositoryMock.Setup(x => x.Ping()).Returns(true);
            branchRepositoryMock.Protected().Setup<string>("GetRepositoryType").Returns("MyRepository");

            branchRepository = branchRepositoryMock.Object;
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
        public void MockedMethodCallback_WrapperMethodCalled_MockedMethodCalledAsExpected()
        {
            List<string> branchesAdded = new List<string>();
            branchRepositoryMock.Setup(x => x.CreateBranch(It.IsAny<string>())).Returns(true).Callback((string bn) => branchesAdded.Add(bn));
            branchRepositoryMock.CallBase = true;

            branchRepository.AddBranches(new string[] { "XXX", "YYY", "ZZZ" });
            Assert.AreEqual(new string[] { "XXX", "YYY", "ZZZ" }, branchesAdded);

            branchRepositoryMock.Verify(x => x.CreateBranch("XXX"), Times.Once);
            branchRepositoryMock.Verify(x => x.CreateBranch("YYY"), Times.Once);
            branchRepositoryMock.Verify(x => x.CreateBranch("ZZZ"), Times.Once);
            branchRepositoryMock.Verify(x => x.CreateBranch(It.IsAny<string>()), Times.Exactly(3));
        }
    }

}
