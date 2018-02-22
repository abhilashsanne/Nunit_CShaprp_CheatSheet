using NUnit.Framework;
using System;
using System.Xml;
using System.Collections;
using System.Linq;
using System.Xml.Linq;

namespace Nunit_CShaprp_CheatSheet
{
    /// <summary>
    /// This test class contains examples of different types of data driven tests in Nunit 
    /// </summary>
    [TestFixture]
    public class DataDrivenTests
    {
        #region Parameterized
        [Test, Sequential]
        public void Addition([Values(4.5D, 15D, 55D, 125.99)] decimal inputA, [Values(4.5D, 15D, 55D, 125D)] decimal inputB, [Values(9, 30, 110.0D, 250.99)] double expectedResult)
        {
            //parameters will be applied in sequential order, so the first value of each parameters will be a test. 
            //It is important to note that this does not guarantee the order that the tests will be executed. The sequence is about the parameter order not the test order.
            decimal actualRate = inputA+inputB;
            //actualRate = _shipper.getFlatRateByWeight(packageWeight);

            Assert.That(actualRate, Is.EqualTo(expectedResult));
        }
 
        /// <summary>
        /// Combinatorial: The test is run with each combination of Values for each parameter
        /// </summary>

        [Test, Combinatorial]
        public void CombinatorialAttributeTest([Values(1, 2, 3)] int a, [Values("a", "b", "c")] string b)
        {
            // Called 9 times with  parameter pairs of: {1,a}, {1,b}, {1,c}, {2,a}, {3,b}....
        }

        /// <summary>
        /// Random: Test can be run with a random value. Random(Min,Max,Count)
        /// </summary>
        [Test]
        public void RandomAttributeTest(
        [Random(1, 10, 2)] int value)
        {
            // Called 2 times with a random integer between 1 and 10
            Assert.That(value, Is.InRange(1, 10));
        }

        /// <summary>
        /// Sequential: Parameters with values are run in sequence
        /// </summary>
        [Test, Sequential]
        public void SequentialAttributeTest(
        [Values(1, 2, 3)] int x,
        [Values("A", "B")] string s)
        {
            // Test runs for parameter pairs {1,A}, {2,B}, {3, null}
        }

        /// <summary>
        /// Range: Parameter is run with a sequence of values incremented. Range(Min,Max,Increment).
        /// </summary>
        [Test]
        public void RangeAttributeTest(
        [Range(0.0, 2, 0.5)] decimal value)
        {
            // Test run for parameters, 0.0, 0.5, 1.0, 1.5, 2.0
            Assert.That(value, Is.InRange(0m, 2m));
        }

        /// <summary>
        /// TestCaseSource: referencing a public property which privides a sequence of test data
        /// </summary>
        [Test, TestCaseSource("CaseSourceTestData")]
        public void CaseSourceTest(int a, decimal b, string c)
        {
            // Can also specify the class to which the property is found upon.
            Assert.That(a + b, Is.EqualTo(Decimal.Parse(c)));
        }
         #region Source
        /// <summary>
        /// TestCaseSource attributes can use static properties to return an array of test data
        /// </summary>
        public static object[] CaseSourceTestData =
        {
            new object[] { 1, 1.1m, "2.1" },
            new object[] { 2, 2.2m, "4.2" },
            new object[] { 3, 3.3m, "6.3" },
        };
        #endregion
        /// <summary>
        /// TestCaseSource: referncing a class and property which provides a sequence of test data and expected output.
        /// </summary>
        [Test, TestCaseSource(typeof(TestCaseDataFactory), "TestCasesDataForTestCaseSourceTest")]
        public decimal TestCaseSourceTest(int a, string b)
        {
            int bInt;
            if (!int.TryParse(b, out bInt))
                throw new ArgumentException(string.Format("Can not parse '{0}' to an integer", b), "b");

            return a + bInt;
        }
         #region TestCaseDataFactory
        /// <summary>
        /// Factory class for providing test data for tests with the TestCaseSource attribue.
        /// </summary>
        public class TestCaseDataFactory
        {
            /// <summary>
            /// TestCaseSource tests can consume IEnumerable properties which return TestCaseData
            /// </summary>
            public static IEnumerable TestCasesDataForTestCaseSourceTest
            {
                get
                {
                    yield return new TestCaseData(1, "1").Returns(2); // Defines the test data and the expected return
                    yield return new TestCaseData(2, "2").Returns(4);
                    //yield return new TestCaseData(0, "a").Throws(typeof(ArgumentException)); // Defines the test data and the expected throw exception
                }
            }
        }
        #endregion

        /// <summary>
        /// TestCase: provides a test input data and expected result.
        /// </summary>
        [TestCase(1, 1, Result = 2)]
        [TestCase(2, 2, Result = 4)]
        [TestCase(3, 3, Result = 6)]
        public int TestCaseTest(int a, int b)
        {
            return (a + b);
        }


        /// <summary>
        /// TestCaseSource: referencing a public property which privides a sequence of test data
        /// </summary>
        [Test, TestCaseSource("CaseSourceTestData1")]
        public void CaseSourceTest1( string c)
        {
            // Can also specify the class to which the property is found upon.
            
            Assert.That("308865", Is.EqualTo(c));
        }
        #region Source
        /// <summary>
        /// TestCaseSource attributes can use static properties to return an array of test data
        /// </summary>
        private IEnumerable CaseSourceTestData1()
        {
            var arr=XDocument.Load("Testdata.xml").Root.Elements("patch").Select(element => (element.Value).ToString()).ToList();
            foreach(var test in arr)
            yield return new[]{ test };
        }
        #endregion

        #endregion
    }
        
}
