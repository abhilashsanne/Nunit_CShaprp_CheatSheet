using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nunit_CShaprp_CheatSheet
{
    [TestFixture] //simple test fixture with setup and teardown
    public class TestFixtures
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


    //Parameterized test fixtures
    //after building the code check the test suites

    [TestFixture("hello", "hello", "goodbye")]
    [TestFixture("zip", "zip")]
    [TestFixture(42, 42, 99)]
    public class ParameterizedTestFixture
    {
        private string eq1;
        private string eq2;
        private string neq;

        public ParameterizedTestFixture(string eq1, string eq2, string neq)
        {
            this.eq1 = eq1;
            this.eq2 = eq2;
            this.neq = neq;
        }

        public ParameterizedTestFixture(string eq1, string eq2)
            : this(eq1, eq2, null) { }

        public ParameterizedTestFixture(int eq1, int eq2, int neq)
        {
            this.eq1 = eq1.ToString();
            this.eq2 = eq2.ToString();
            this.neq = neq.ToString();
        }

        [Test]
        public void TestEquality()
        {
            Assert.AreEqual(eq1, eq2);
            if (eq1 != null && eq2 != null)
                Assert.AreEqual(eq1.GetHashCode(), eq2.GetHashCode());
        }

        [Test]
        public void TestInequality()
        {
            Assert.AreNotEqual(eq1, neq);
            if (eq1 != null && neq != null)
                Assert.AreNotEqual(eq1.GetHashCode(), neq.GetHashCode());
        }
    }

    //Generic test fixtures
    [TestFixture(typeof(ArrayList))]
    [TestFixture(typeof(List<int>))]
    public class IList_Tests<TList> where TList : IList, new()
    {
        private IList list;

        [SetUp]
        public void CreateList()
        {
            this.list = new TList();
        }

        [Test]
        public void CanAddToList()
        {
            list.Add(1); list.Add(2); list.Add(3);
            Assert.AreEqual(3, list.Count);
        }
    }


    //Generic Test Fixtures with Parameters
    //Specify both sets of parameters as arguments to the TestFixtureAttribute. Leading System.Type arguments are used as type parameters, while any remaining arguments are used to construct the instance.
    [TestFixture(typeof(double), typeof(int), 100.0, 42)]
    [TestFixture(typeof(int), typeof(double), 42, 100.0)]
    public class SpecifyBothSetsOfArgs<T1, T2>
    {
        T1 t1;
        T2 t2;

        public SpecifyBothSetsOfArgs(T1 t1, T2 t2)
        {
            this.t1 = t1;
            this.t2 = t2;
        }

        [TestCase(5, 7)]
        public void TestMyArgTypes(T1 t1, T2 t2)
        {
            Assert.That(t1, Is.TypeOf<T1>());
            Assert.That(t2, Is.TypeOf<T2>());
        }
    }
    //Specify normal parameters as arguments to TestFixtureAttribute and use the named parameter TypeArgs= to specify the type arguments. Again, for this example, the type info is duplicated, but it is at least more cleanly separated from the normal arguments...
    [TestFixture(100.0, 42, TypeArgs=new Type[] { typeof(double), typeof(int) })]
    [TestFixture(42, 100.0, TypeArgs=new Type[] { typeof(int), typeof(double) })]
    public class SpecifyTypeArgsSeparately<T1, T2>
    {
        T1 t1;
        T2 t2;

        public SpecifyTypeArgsSeparately(T1 t1, T2 t2)
        {
            this.t1 = t1;
            this.t2 = t2;
        }

        [TestCase(5, 7)]
        public void TestMyArgTypes(T1 t1, T2 t2)
        {
            Assert.That(t1, Is.TypeOf<T1>());
            Assert.That(t2, Is.TypeOf<T2>());
        }
    }


    //when the constructor makes use of all the type parameters NUnit may simply be able to deduce them from the arguments provided
    [TestFixture(100.0, 42)]
    [TestFixture(42, 100.0)]
    public class DeduceTypeArgsFromArgs<T1, T2>
    {
        T1 t1;
        T2 t2;

        public DeduceTypeArgsFromArgs(T1 t1, T2 t2)
        {
            this.t1 = t1;
            this.t2 = t2;
        }

        [TestCase(5, 7)]
        public void TestMyArgTypes(T1 t1, T2 t2)
        {
            Assert.That(t1, Is.TypeOf<T1>());
            Assert.That(t2, Is.TypeOf<T2>());
        }
    }
}
