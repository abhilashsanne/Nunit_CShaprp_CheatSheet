using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nunit_CShaprp_CheatSheet
{
    [TestFixture] //simple test fixture with setup and teardown
    public class TestSuites
    {

        [SetUp]
        public void firstforTC()
        {
            //executes before every test case
            Console.WriteLine("Setup for every test case");
            Assert.Pass("pass");
        }
        [Test]
        public void TestMethod()
        {
            // TODO: Add your test code here
           Assert.Pass("Your first passing test");
          
        }
        [Test]
        public void TestMethod2()
        {
            Assert.Fail("Your second failing test");
        }

        [Test]
        public void TestMethod3()
        {
            Assert.Pass("Pass");
        }
        [TearDown]
        public void lastforTC()
        {
            //executes after every tc
            Console.WriteLine("post execution for every test case");
            Assert.Pass("pass");
        }
    }

    [TestFixture(1,2)]// parameterised fixture
    public class TestSuite2
    {
        public  TestSuite2(int i, int j)
        {
            //we can use i, j
        }
        [Test]
        public void TestCase1()
        { }
    }

    //[TestFixture(typeof(int))] //generic test fixture
    //[TestFixture(typeof(List<int>))]
    
}
