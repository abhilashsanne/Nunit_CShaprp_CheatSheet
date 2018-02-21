using NUnit.Framework;
using System;

namespace Nunit_CShaprp_CheatSheet
{
    /// <summary>
    /// This test class contains examples of different types of data driven tests in Nunit 
    /// </summary>
    [TestFixture]
    public class DataDrivenTests
    {
        [Test, Sequential]
        public void Addition([Values(4.5D, 15D, 55D, 125D)] decimal inputA, [Values(4.5D, 15D, 55D, 125D)] decimal inputB, [Values(10.99, 29.99, 75.55, 999.99)] double expectedResult)
        {
            //parameters will be applied in sequential order, so the first value of each parameters will be a test. 
            //It is important to note that this does not guarantee the order that the tests will be executed. The sequence is about the parameter order not the test order.
            double actualRate = 0;
            //actualRate = _shipper.getFlatRateByWeight(packageWeight);

            Assert.That(actualRate, Is.EqualTo(expectedResult));
        }
    }
}
