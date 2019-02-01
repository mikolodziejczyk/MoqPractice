Moq practice

Solution MoqPractice
in
E:\VisualStudio\Learn\MoqPractice

You have two assemblies:
MoqPractice.BL with BranchRepository.cs and IGreeter.cs to mock.
MoqPractice.BL.Tests with BranchRepositoryMockTests.cs with unit tests.
Most unit tests are already done, you have to setup mock so that the tests pass.
Unless specified otherwise, you do all your job in the BranchRepositoryMockTests.cs / SetUp();

1. Install Moq in MoqPractice.BL.Tests
   - which .NET version is required?


2. Creating a mock of a class
  - in the class BranchRepositoryMockTests create a field: branchRepositoryMock that is a mock of BranchRepository.
  - in SetUp() create the mock and assign it to this field.
  - MockBehavior: Strict vs Loose - what is the difference? What is default?
  - set the branchRepository to the mocked implementation from the mock you created
    - can you make further changes in mock setup once you obtained the mock class implementation?


3. Setting up methods - a parameterless method
   Setup GetAllBranches() to return this.allBranches; check the test ParameterlessMethod_SetupReturnsSimpleValue_SetupProperly()


4  A method with parameters: setup GetBranchesForCompany() to:

   - (any parameter value) return  this.allBranches for any parameters value
     MethodWithParameters_SetupForAllParameterValue_SetupProperly()

   - (fixed parameter value) return this.company3Branches when companyId == 3, identified as the fixed setup parameter
     MethodWithParameters_SetupForASpecificParameterValue_SetupProperly()
     composing the setup - can you create one setup that handles all parameter values one way and specific ones another - how?

   - (return value calculated with a delegate) return using a delegate 
     new string[] { String.Format("{0}_{1}", companyId, branchType) }
     when the branch type has a fixed value == 10.
     MethodWithParameters_ReturnsValueCalculatedWithDelegate_SetupProperly()

   - (parameter condition matched with delegate) = return and empty string array for any companyId>10, matched with a delegate and any branchType
     MethodWithParameters_SetupMatchedWithDelegate_SetupProperly()

   - (throwning an exception) throw an InvalidOperationException("Company doesn't exist") when companyId == 100 (identified as a fixed value) and any branch type.
     MethodWithParameters_SetupThrowsExceptionForSpecificParameter_SetupProperly()


5. A method with with an out parameter: GetBranchPage()
   Setup GetBranchPage() to return this.allBranches for any parameter values. 
   The out itemCount should be set to 100 by the method setup.
   MethodWithOutParameter_SetupReturnsValueForOutParameter_SetupProperly()


6. Setting up properties
   
   - getter-only property) setup CompanyCount (to return a fixed value 23
     Getter_SetupReturnsFixedValue_SetupProperly()

   - (stub) Setup the Connection R/W property so that it will automatically start tracking its value (as if there's an uderlying field), give it an initial "TEST" value.
     GetterAndSetter_SetupReturnsHasInitialValueAndTracksCurrent_SetupProperly()

7. Internal members
   Expose internals from MoqPractice.BL so that internal members can be mocked.
   Setup an internal Ping() to return true (a fixed value).
   branchRepositoryMock.Setup(x => x.Ping()).Returns(true);
   InternalMethod_SetupReturnsValue_SetupProperly()

8. Protected members
   Setup a protected GetRepositoryType() to return "MyRepository" (fixed value).
   ProtectedMethod_SetupReturnsValue_SetupProperly()

9. Callbacks - in MockedMethodCallback_WrapperMethodCalled_MockedMethodCalledAsExpected()
   You have BranchRepository.AddBranches(string[]) that internally calls CreateBranch(string)
   Setup CreateBranch() so that for all parameter values it invokes a callback that adds the parameter value to the list.

   List<string> branchesAdded = new List<string>();
   // setup CreateBranch() so that it adds the parameter it has been called with to branchesAdded 
   branchRepository.AddBranches(new string[] { "XXX", "YYY", "ZZZ" });
   Assert.AreEqual(new string[] { "XXX", "YYY", "ZZZ" }, branchesAdded);
   
   Are methods for which you don't specify setup implemented by mock?
   How to change the mock configuration so that you don't have to setup branchRepository.AddBranches() - it should have its original implementation?

10. Verification - in MockedMethodCallback_WrapperMethodCalled_MockedMethodCalledAsExpected()
    Which exception is thrown when the verification condition is not met? What about the message?
    Add code that verifies that CreateBranch() has been called with: 
    (a) "XXX" - once, (b) "YYY" - once, (c) "ZZZ" - once,
    (d) "UUU" - never, specify a message when fails
    (e) with any parameters exactly 3 times
    How can you specifiy conditions on parameters in verification - what are options?

11. Mocking an interface - in MockedInterface_MethodMocked_ReturnsProperValue()
    Create a mock for IGreeter.
    Setup GetGreeting(string name) to return String.Format("Hello {0}!", name);

    // create a mock here
    IGreeter greeter = null; // set to the mock implementation
    // setup GetGreeting() here to return String.Format("Hello {0}!", name);
    string r = greeter.GetGreeting("World");
    Assert.That(r == "Hello World!");

    Can you create a mock that implements multiple interfaces?



