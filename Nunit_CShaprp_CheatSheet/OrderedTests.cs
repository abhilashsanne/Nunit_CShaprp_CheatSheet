using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using NUnit.Framework;

namespace Nunit_CShaprp_CheatSheet
{
    /// <summary>
    /// This custom attribute will help us run tests in groups or sequential order without depending on run config file order
    /// </summary>
    public class OrderedTestAttribute : Attribute
    {
        public double Order { get; set; }


        public OrderedTestAttribute(double order)
        {
            Order = order;
        }

        public static IEnumerable<TestCaseData> GetOrderedTests(double orderNumber)
        {
            var assembly = Assembly.GetExecutingAssembly();
            Dictionary<double, List<MethodInfo>> methods = assembly
                .GetTypes()
                .SelectMany(x => x.GetMethods())
                .Where(y => y.GetCustomAttributes().OfType<OrderedTestAttribute>().Any())
                .GroupBy(z => z.GetCustomAttribute<OrderedTestAttribute>().Order)
                .ToDictionary(gdc => gdc.Key, gdc => gdc.ToList());

            foreach (var order in methods.Keys.OrderBy(x => x))
            {
                foreach (var methodInfo in methods[order])
                {
                    if (Math.Floor(order) == Convert.ToInt32(orderNumber))
                    {
                        MethodInfo info = methodInfo;
                        yield return new TestCaseData(
                            new TestStructure
                            {
                                Test = () =>
                                {
                                    object classInstance = Activator.CreateInstance(info.DeclaringType, null);
                                    info.Invoke(classInstance, null);
                                }
                            }).SetName(methodInfo.Name);
                    }
                }
            }
        }
    }

    public class TestStructure
    {
        public Action Test;
    }

    /// <summary>
    /// Tells the sequence number of test cases
    /// We will group tests as per the base sequence number
    /// </summary>
    class Sequence
    {
        public double I;
    }

    /// <summary>
    /// This test class contains test cases utilizing the sequence attribute
    /// </summary>
    [TestFixture]
    public class OrderedTestsClass
    {

        #region Order1 Source

        /// <summary>
        /// This will become the Parent name of the group of test cases we are executing in order or sequence
        /// I will collect all the targeted tests under it (check Nunit GUI for tree hierarchy view)
        /// The Method name can be anything, probably related to your test case group
        /// </summary>
        /// <param name="test"></param>
        [TestCaseSource("TestSource")]
        public void Sequence1(TestStructure test)
        {
            test.Test();
        }

        /// <summary>
        /// Collects all the test cases which start from sequence number '1'
        /// Ex: 1.1, 1.23, 1.456, 1.3, 1.4, 1.5
        /// </summary>
        public IEnumerable<TestCaseData> TestSource
        {
            get
            {
                foreach (var testCaseData in OrderedTestAttribute.GetOrderedTests(1)) yield return testCaseData;
            }
        }

        #endregion

        #region Order2 Source

        [TestCaseSource("Order2Test")]
        public void Sequence2(TestStructure test)
        {
            test.Test();
        }

        public IEnumerable<TestCaseData> Order2Test
        {
            get
            {
                foreach (var testCaseData in OrderedTestAttribute.GetOrderedTests(2)) yield return testCaseData;
            }
        }

        #endregion


        #region Order 1 Tests
        /// <summary>
        /// All you need is to use OrderedTest attribute with the order number 
        /// Do not use 'Test' Attribute again here
        /// The order in which you write your test cases does not matter
        /// The execution order is determined by the value provided in OrderedTest attribute
        /// </summary>
        [OrderedTest(1.1)]
        public void Test11()
        {
            //First test Case
            Console.WriteLine("This is test One");
        }

        [OrderedTest(1.2)]
        public void Test12()
        {
            //You might think it is second test cases in the sequence or order but
            //there is 1.11 test case which will execute before this 1.2 test cases
            Console.WriteLine("This is test Three");
        }


        [OrderedTest(1.11)]
        public void Test111()
        {
            //second test case which will get executed
            Console.WriteLine("This is test Two");
        }

        [OrderedTest(1.3)]
        public void Test13()
        {
            //This is the last test in sequence as 1.3 is the greatest number in sequence 1 we created
            Console.WriteLine("This is test Four");
        }

        #endregion



        #region Order 2 Tests

        [OrderedTest(2.2)]
        public void Test22()
        {
            Console.WriteLine("This is test three");
        }

        [OrderedTest(2.3)]
        public void Test23()
        {
            Console.WriteLine("This is test three");
        }

        [OrderedTest(2.1)]
        public void Test21()
        {
            Console.WriteLine("This is test three");
        }



        #endregion



    }
}