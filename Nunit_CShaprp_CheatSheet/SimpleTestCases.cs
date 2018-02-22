using System;
using System.Globalization;
using NUnit.Framework;

namespace Nunit_CShaprp_CheatSheet
{
    /// <summary>
    /// This Test Calss is collection of Basic Nunit Test cases and attributes
    /// </summary>
    [TestFixture, Category("SimpleTests")]
    public class SimpleTestCases
    {
        readonly string _runId;
        public static bool PreviousTestExecutionPassed = true;
        /// <summary>
        /// Constructor in a test class can be used to initialize ReadOnly variables
        /// These variable could be server details or data like time stamp
        /// </summary>
        public SimpleTestCases()
        {
            _runId = DateTime.Now.ToString(CultureInfo.InvariantCulture);
        }

        #region Setup Methods
        /// <summary>
        /// A method with 'TestFixtureSetUp' attribute will run once before
        /// the first test case in the 'TestFixture Class' or test class gets executed
        /// </summary>
        [TestFixtureSetUp]
        public void TestClassFixture()
        {
            Console.WriteLine("Unique Run ID is: " + _runId);
            //We can have in this section code performing actions like 
            //Read configuration settings
            //Login into application
            //Initialize browser or application
            //One time setup actions required for test execution
        }

        /// <summary>
        /// A method with 'SetUp' attribute will run once before every test in TestFixture
        /// It does not matter if previous passed or failed
        /// </summary>
        [SetUp]
        public void TestSetup()
        {
            //We can have in this section code performing actions like 
            //Check if previous test case failed (previousTestExecutionPassed) and kill session/process, launch new session
            //Start timer if you are tracking execution of every test
            //Initialize logger object
            //setup activities required before every test execution
        }
        #endregion

        #region Simple Individual Tests
        [Test]
        public void BasicTest()
        {
            //This is the basic format of test method in Nunit Unit testing framework
        }

        [Test, Description("Title of Test case or its functionality")]
        public void DescriptionAttribute_Test()
        {
            //The description attribute would allow you to easily identify what the purpose of the test case is
            //In most of the cases we use the functional test case title as description attribute
            //You can also include user story number or Test case number in test case description or name for easy identification
        }

        [Test, Ignore("Reason for keeping test on hold or ignored")]
        public void Igonred_Test()
        {
            //The reason provided in ignore attribute is optional
            //Ignore attribute is used for test cases in the following cases mostly:
            //Test case is not completely developed
            //Test case is put out of regression suite and is not supposed to run
            //You dont want to run this test case, not even manually triggering it

            //Providing ignoring reason for the test is a good practice
        }

        [Test, Explicit("Reason for keeping test on Explict run mode")]
        public void Explicit_Test()
        {
            //The reason provided in Explicit attribute is optional
            //Some test cases might lock resources or you might have observed it leading to blocking execution of other tests
            //These are the tests which you do not want to run in bulk runs
            //These belong to group of test generally known as 'ExecuteLast' or 'RunLast'

            //To run a explicit attribute test, you must run is specifically selected manually via Nunit console

            //Providing reason for why the test is be run in 'Explicit' mode is good practice
        }

        [Test, Timeout(25000)]
        public void Timeout_Test()
        {
            //Timeout value provided in milliseconds defines the maximum time allowed for execution
            //If used on TestFixture, it will be set for each contained Test's   

            //Sometime you come across test which run into infinite loop or taking long execution time and blockin the regression runs
            //NOTE: The test case execution will be stopped when Timeout attribute is used
        }

        [Test, MaxTimeAttribute(2000)]
        public void MaxTimeAttribute_Test()
        {
            //MaxTimeAttribute, value provided in milliseconds defines the maximum time in which test should finish execution
            //The test case execution continues after time out but the test is marked failed

            //Sometime you come across API test which should return response within specified time
        }


        [Test, Platform("Mono"), Culture("en-GB")]
        public void Platform_Test()
        {
            // Can also provide exclusion culture and platform
            // If run Platform or Cultrue can not be provided then test case run will be ignored
        }

        [Test, Category("ModuleName")]
        public void Single_Category_Test()
        {
            //Used to categorise or group your test cases
            //Via nunit UI or Nunit console window you can execute tests using the category attribute value

            //Example command:
            //nunit-console myassembly.dll /include:ModuleName
            //nunit-console myassembly.dll /exclude:ModuleName

            //All test belongining the given category will be run or excluded
            //A test case can have multiple categories
        }

        [Test, Category("Module Name Category"), Category("SmokeTestCategory")]
        public void Multiple_Category_Test()
        {
            //A test case can have multiple categories
            //Category attributed when used on test calss, groups all test methods under it to same categoryf
        }

        [Test, Property("PropertyName", "Value"), Property("Priority", "1"), Property("Severity", "3")]
        public void PropertyAttributeTest()
        {
            //Property: categorises the test witin the test output XML.

            //In bigger test projects you might have to categorise test further 
            //Property attribute allows to categorise them as per user defined properties
            //Eg: Color property, group property, assignee property

            //Refer CustomPropertyAttributeTest for generating custom attributes matching your project requirements
        }

        [Test, Property("Severity", "2")]
        public void PropertyAttributeTest_Severity()
        {
            //Another example for understanding property attribute
        }

        /// <summary>
        /// ExpectedException defines what exception and how it is configured
        /// </summary>
        [Test, ExpectedException(typeof(Exception), ExpectedMessage = "Testing", MatchType = MessageMatch.Contains)]
        public void ExpectedExceptionAttributeTest()
        {
            // MessageMatch is an enum of: Contains, Exact, Regex, StartsWith
            throw new Exception("Testing Expected Exception");
        }

        #endregion

        #region TearDown Methods

        /// <summary>
        ///  A method with 'TearDown' attribute will run everytime after a test execution is completed in TestFixture
        /// </summary>
        [TearDown]
        public void TestTearDown()
        {
            PreviousTestExecutionPassed = true;
            //Stop Timers and record test case execution time
            //Clean up activities required after every test execution
            //update test execution result to Pass/Fail
            //Check if test execution is pass or fail and set flag value to False (previousTestExecutionPassed)
        }

        /// <summary>
        /// A method with 'TestFixtureTearDown' attribute will run once after all selected tests execution is completed in TestFixture
        /// </summary>
        [TestFixtureTearDown]
        public void TestClassTearDown()
        {
            //Kill application instances/processes launched
            //Logout from the application
            //Generate test run report  
            //Free up resources occupied
        }
        #endregion

    }
}

