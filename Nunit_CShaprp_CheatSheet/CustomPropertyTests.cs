using System;
using System.Collections;
using NUnit.Framework;

namespace Nunit_CShaprp_CheatSheet
{
    /// <summary>
    /// This test class contains example tests utilizing C# custom attributes for Nunit tests
    /// </summary>
    [TestFixture]
    public class CustomPropertyTests
    {
        private string _testCaseName;
        private string _testDescription;
        private string _testCategory;
        private string _testCustomAttributeValue;

        /// <summary>
        /// Runs one time for every test case execution
        /// </summary>
        [SetUp]
        public void SetupMethodForEveryTest()
        {
            //Read and store current test case details from the test case attributes
            ReadingTestCaseAttributes();
        }

        [Test, Category("CustomAttributeTest"), CustomPropertyAttribute(CustomPropertyValue.One), Description("Custom Attribute Testing")]
        public void CustomPropertyAttributeTest()
        {
            //Custom Property: categorises the test within the test output XML.

            //Customer Property Attributes are extension to Property attribute concept
            //NOTE: Your test case execution did not begin yet but you have its attributes read
        }


        #region Custom Attribute

        /// <summary>
        /// This custom property can be project specific like Priority, Severity, Role, Country
        /// </summary>
        public enum CustomPropertyValue
        {
            One,
            Two,
        }

        /// <summary>
        /// Here you defie how you want usage of your custom attribute to be
        /// </summary>
        [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
        public class CustomPropertyAttribute : PropertyAttribute
        {
            public CustomPropertyAttribute(CustomPropertyValue value)
                : base("Custom", value.ToString())
            {
            }
        }

        /// <summary>
        /// Get details of current test case like name, descript, category, custom attributes
        /// </summary>
        public void ReadingTestCaseAttributes()
        {
            //NOTE: Your test case execution did not begin yet but you are reading its attributes now

            _testCaseName = TestContext.CurrentContext.Test.Name;
            _testDescription = TestContext.CurrentContext.Test.Properties["_DESCRIPTION"] == null
                ? ""
                : TestContext.CurrentContext.Test.Properties["_DESCRIPTION"].ToString();
            _testCustomAttributeValue = TestContext.CurrentContext.Test.Properties["Custom"] == null
                ? ""
                : TestContext.CurrentContext.Test.Properties["Custom"].ToString();
            ArrayList temp = (ArrayList)TestContext.CurrentContext.Test.Properties["_CATEGORIES"];
            _testCategory = temp[0].ToString();
        }

        #endregion

        #region About TestContext, CurrentContext

        //TestContext
        //Each NUnit test runs in an execution context, which includes information about the environment as well as the test itself. 
        //The TestContext class allows tests to access certain information about the execution context. 


        //CurrentContext
        //The context of the currently executing test may be accessed by use of the static property CurrentContext, 
        //which gets a reference to the current TestContext. This context is created separately for each test before it begins execution.


        //NOTE: context may not be changed - all properties are read-only.
        [TearDown]
        public void Demo_TestContext_CurrentContext()
        {
            var test = TestContext.CurrentContext.Test;
            var testName = TestContext.CurrentContext.Test.Name;
            var fullName = TestContext.CurrentContext.Test.FullName;

            //Gets the full path of the directory containing the current test assembly.
            var testDirectory = TestContext.CurrentContext.TestDirectory;

            //Gets the full path of the directory to be used for output from this test run
            //This is normally the directory that was current when execution of NUnit began
            //but may be changed by use of the /work option of nunit-console
            var workingDirectory = TestContext.CurrentContext.WorkDirectory;

            //A TestStatus with possible values Inconclusive Skipped Passed Failed
            //The Status value should be used in preference to State wherever possible, since the latter will not be available in future releases.
            var testStatus = TestContext.CurrentContext.Result.Status;


            //A test can be in these states: Inconclusive NotRunnable Skipped Ignored Success Failure Error Cancelled
            //The result of the test may be accessed during setup or test execution, but it only has a useful value at in the TearDown method.
            var testState = TestContext.CurrentContext.Result.State;

            Console.WriteLine(test);
            Console.WriteLine(testName);
            Console.WriteLine(fullName);
            Console.WriteLine(testDirectory);
            Console.WriteLine(workingDirectory);
            Console.WriteLine(testStatus);
            Console.WriteLine(testState);
        }

        #endregion

    }
}
