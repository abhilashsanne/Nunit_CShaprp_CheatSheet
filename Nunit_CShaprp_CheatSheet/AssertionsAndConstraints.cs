using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace Nunit_CShaprp_CheatSheet
{
    [TestFixture]
    public class AssertionsAndConstraints
    {
        /// <summary>
        /// This test case give you example implementation and usage of all Nunit Assert types available
        /// This approach is termed as Nunit Classic model of assertion
        /// The classic Assert model uses a separate method to express each individual assertion of which it is capable.
        /// </summary>
        [Test(), Category("Nunit Assertions")]
        public void TestAssertions()
        {
            #region Condition assertions

            Assert.True(true, "Assert.True and Assert.IsTrue test that the specified condition is true. ");
            

            Assert.IsTrue(true);

            Assert.False(false, "Assert.False and Assert.IsFalse test that the specified condition is false.");
            Assert.IsFalse(false);
            
            Assert.Null(null);
            Assert.IsNull(null, "Assert.Null and Assert.IsNull test that the specified object is null.");
            Assert.IsNotNull("10");
            Assert.NotNull("10", "");

            Assert.IsNaN(Double.NaN, "Assert.IsNaN tests that the specified double value is NaN (Not a number).");
            
            Assert.IsEmpty("");
            Assert.IsEmpty(new List<object>());
            Assert.IsNotEmpty(new List<object> {1});
            Assert.IsNotEmpty("MyTestString");

            #endregion

            #region Equality

            Assert.AreEqual(true, true, "Assert.AreEqual tests whether the two arguments are equal.");
            Assert.AreNotEqual(true, false);

            #endregion

            #region Identity

            Assert.AreSame(string.Empty, string.Empty,
                "Assert.AreNotSame tests that the two arguments do not reference the same object.");
            Assert.AreNotSame(new object(), new object());
            
            //both objects are refering to same object
            object a = new object();
            object b = a;
            Assert.AreSame(a,b);
            #endregion

            #region Comparision

            //Contrary to the normal order of Asserts, these methods are designed to be read in the "natural" English-language or mathematical order.
            //Thus Assert.Greater(x, y) asserts that x is greater than y (x > y).
            Assert.Greater(Decimal.MaxValue, Decimal.MinValue,
                "Assert.Greater tests whether one object is greater than than another");
            Assert.GreaterOrEqual(Decimal.MinValue, Decimal.MinValue);

            Assert.Less(Decimal.MinValue, Decimal.MaxValue);
            Assert.LessOrEqual(Decimal.MinValue, Decimal.MinValue);

            #endregion

            #region Types

            Assert.IsInstanceOf<decimal>(decimal.MinValue,
                "Assert.IsInstanceOf succeeds if the object provided as an actual value is an instance of the expected type.");
            Assert.IsNotInstanceOf<int>(decimal.MinValue);

            Assert.IsNotAssignableFrom(typeof(List<Type>), string.Empty,
                "Assert.IsAssignableFrom succeeds if the object provided may be assigned a value of the expected type.");
            Assert.IsAssignableFrom(typeof(List<decimal>), new List<decimal>());

            Assert.IsNotAssignableFrom<List<Type>>(string.Empty);
            Assert.IsAssignableFrom<List<decimal>>(new List<decimal>());

            #endregion            
          

            #region Strings

            //The StringAssert class provides a number of methods that are useful when examining string values
            StringAssert.Contains("london", "london");
            StringAssert.StartsWith("Food", "FoodPanda");
            StringAssert.EndsWith("rangers", "Powerrangers");
            StringAssert.AreEqualIgnoringCase("DOG", "dog");
            StringAssert.IsMatch("[10-29]", "15");
            StringAssert.DoesNotContain("abc", "defghijk");
            StringAssert.DoesNotEndWith("abc", "abcdefgh");
            StringAssert.DoesNotMatch("abc", "def");
            string a1="abc";
            string b1="abcd";
            //StringAssert.ReferenceEquals(a1, b1); need to debug why it's failing
            
            Assert.Contains(string.Empty, new List<object> {string.Empty},
                "Assert.Contains is used to test whether an object is contained in a collection.");

            #endregion

            #region Collections

            //The CollectionAssert class provides a number of methods that are useful when examining collections and 
            //their contents or for comparing two collections. These methods may be used with any object implementing IEnumerable.


            //The AreEqual overloads succeed if the corresponding elements of the two collections are equal. 
            //AreEquivalent tests whether the collection contents are equal, but without regard to order.
            //In both cases, elements are compared using NUnit's default equality comparison.

            CollectionAssert.AllItemsAreInstancesOfType(new List<decimal> {0m}, typeof(decimal));
            CollectionAssert.AllItemsAreNotNull(new List<decimal> {0m});
            CollectionAssert.AllItemsAreUnique(new List<decimal> {0m, 1m});
            CollectionAssert.AreEqual(new List<decimal> {0m}, new List<decimal> {0m});
            CollectionAssert.AreEquivalent(new List<decimal> {0m, 1m},
                new List<decimal> {1m, 0m}); // Same as AreEqual though order does not mater
            CollectionAssert.AreNotEqual(new List<decimal> {0m}, new List<decimal> {1m});
            CollectionAssert.AreNotEquivalent(new List<decimal> {0m, 1m},
                new List<decimal> {1m, 2m}); // Same as AreNotEqual though order does not matter
            CollectionAssert.Contains(new List<decimal> {0m, 1m}, 1m);
            CollectionAssert.DoesNotContain(new List<decimal> {0m, 1m}, 2m);
            CollectionAssert.IsSubsetOf(new List<decimal> {1m},
                new List<decimal> {0m, 1m}); // {1} is considered a SubSet of {1,2}
            CollectionAssert.IsNotSubsetOf(new List<decimal> {1m, 2m}, new List<decimal> {0m, 1m});
            CollectionAssert.IsEmpty(new List<decimal>());
            CollectionAssert.IsNotEmpty(new List<decimal> {1m});
            CollectionAssert.IsOrdered(new List<decimal> {1m, 2m, 3m});
            CollectionAssert.IsOrdered(new List<string> {"a", "A", "b"}, StringComparer.CurrentCultureIgnoreCase);
            CollectionAssert.IsOrdered(new List<int> {3, 2, 1},
                "The list is ordered"); // Only supports ICompare and not ICompare<T> as of version 2.6

            #endregion

            #region File Assert

            //Various ways to compare a stream or file.
            //The FileAssert class provides methods for comparing or verifying the existence of files
            //Files may be provided as Streams, as FileInfos or as strings giving the path to each file.
            FileAssert.AreEqual(new MemoryStream(), new MemoryStream());
            FileAssert.AreEqual(new FileInfo("MyFile.txt"), new FileInfo("MyFile.txt"));
            FileAssert.AreEqual("MyFile.txt", "MyFile.txt");
            FileAssert.AreNotEqual(new FileInfo("MyFile.txt"), new FileInfo("MyFile2.txt"));
            FileAssert.AreNotEqual("MyFile.txt", "MyFile2.txt");
            FileAssert.AreNotEqual(new FileStream("MyFile.txt", FileMode.Open),
                new FileStream("MyFile2.txt", FileMode.Open));

            FileAssert.Equals(new FileInfo("MyFile.txt"), new FileInfo("MyFile.txt"));
            FileAssert.ReferenceEquals(new FileInfo("MyFile.txt"), new FileInfo("MyFile.txt"));

            #endregion

            #region Utilities

            // Used to stop test execution and mark them pass or fail or skip

            if (Convert.ToInt32(DateTime.Now.Second) > 30)
            {
                Console.WriteLine("Exaple on Utilities assertions");
                Assert.Ignore();
            }

            if (Convert.ToInt32(DateTime.Now.Second) < 30)
            {
                Assert.Inconclusive();
            }

            // Defining the failed message
            Assert.IsTrue(true, "A failed message here");

            Assert.Pass();
            Assert.Fail();

            #endregion
        }
       
        /// <summary>
        /// Constraint based testing: Test cases with a fluent design pattern
        /// </summary>
        [Test, Category("Constraint based testing")]
        public void TestConstraints()
        {

            // Numerical Equality
            Assert.That(1, Is.EqualTo(1));
            Assert.That(1, Is.Not.EqualTo(2));
            Assert.That(1.1, Is.EqualTo(1.2).Within(0.1)); // Allow a tollerance
            Assert.That(1.1, Is.EqualTo(1.01).Within(10).Percent); // Pass tollerance within percent

            // Float Equality
            Assert.That(20000000000000004.0,
                Is.EqualTo(20000000000000000.0).Within(1).Ulps); // Pass tollerance with units of last place

            // String Equality
            Assert.That("Foo!", Is.EqualTo("Foo!"));
            Assert.That("Foo!", Is.Not.EqualTo("FOO!"));
            Assert.That("Foo!", Is.EqualTo("FOO!").IgnoreCase);
            Assert.That(new List<string> {"FOO!"}, Is.EqualTo(new List<string> {"Foo!"}).IgnoreCase);

            // Date, Time and TimeSpan equality
            Assert.That(DateTime.Today, Is.EqualTo(DateTime.Today));
            Assert.That(DateTime.Now, Is.Not.EqualTo(DateTime.Now));
            Assert.That(DateTime.Now,
                Is.EqualTo(DateTime.Now).Within(TimeSpan.FromSeconds(1))); // Time based pass tollerances
            Assert.That(DateTime.Now, Is.EqualTo(DateTime.Now).Within(1).Days);
            Assert.That(DateTime.Now, Is.EqualTo(DateTime.Now).Within(1).Hours);
            Assert.That(DateTime.Now, Is.EqualTo(DateTime.Now).Within(1).Minutes);
            Assert.That(DateTime.Now, Is.EqualTo(DateTime.Now).Within(1).Seconds);
            Assert.That(DateTime.Now, Is.EqualTo(DateTime.Now).Within(1).Milliseconds);

            // Collection equality
            Assert.That(new[] {1.0, 2.0, 3.0}, Is.EqualTo(new[] {1, 2, 3}));
            Assert.That(new[] {1.0, 2.0, 3.0}, Is.Not.EqualTo(new[] {1, 2, 3, 4}));
            Assert.That(new[] {1.0, 2.0, 3.0, 4.0},
                Is.EqualTo(new[,] {{1, 2}, {3, 4}})
                    .AsCollection); // Compare data and not collection (flattens a collection of collections)

            // Customer Comparer
            //Assert.That(int.MinValue, Is.EqualTo(int.MaxValue).Using(new WhoCaresComparer()));

            // Identity (Same instance of)
            Assert.That(string.Empty, Is.SameAs(string.Empty));
            Assert.That(new object(), Is.Not.SameAs(new object()));

            // Condition
            Assert.That(string.Empty, Is.Not.Null);
            Assert.That(true, Is.True);
            Assert.That(true, Is.Not.False);
            Assert.That(double.NaN, Is.NaN);
            Assert.That(string.Empty, Is.Empty);
            Assert.That(new List<int>(), Is.Empty);
            Assert.That(new List<int> {1, 2, 3}, Is.Unique);

            // Comparision
            Assert.That(1, Is.LessThan(2));
            Assert.That(1, Is.GreaterThan(0));
            Assert.That(1, Is.LessThanOrEqualTo(1));
            Assert.That(1, Is.GreaterThanOrEqualTo(1));
            Assert.That(1, Is.AtLeast(0)); // Same as GreaterThanOrEqualTo
            Assert.That(1, Is.AtMost(2)); // Same as LessThanOrEqualTo
            Assert.That(1, Is.InRange(1, 2));

            // Path: comparision on file path only without comparing the contents.
            // All paths are converted to 'canonical' path before comparing; full direct path to file.
            Assert.That("MyFile.txt", Is.SamePath("MyFile.txt"));
            Assert.That("MyFile.txt", Is.SamePath("MYFILE.TXT").IgnoreCase);
            Assert.That("MyFile.txt", Is.SamePath("MyFile.txt").RespectCase);
            //Assert.That("/usr/bin", Is.SubPath("/usr"));           // directory exists within another
            Assert.That("/usr/bin", Is.SamePathOrUnder("/usr")); // SamePath or SubPath

            // Type Constraints
            Assert.That(new MemoryStream(), Is.InstanceOf(typeof(Stream))); // Is type or ancestor
            Assert.That(new MemoryStream(), Is.TypeOf(typeof(MemoryStream))); // Is type and not ancestor
            Assert.That(new object(), Is.AssignableFrom(typeof(MemoryStream))); // Can be assigned from
            Assert.That(new MemoryStream(), Is.AssignableTo(typeof(Stream))); // Can be assignable to

            Assert.That(new MemoryStream(), Is.InstanceOf<Stream>()); // Is type or ancestor
            Assert.That(new MemoryStream(), Is.TypeOf<MemoryStream>()); // Is type and not ancestor
            Assert.That(new object(), Is.AssignableFrom<MemoryStream>()); // Can be assigned from
            Assert.That(new MemoryStream(), Is.AssignableTo<Stream>()); // Can be assignable to

            // String Comparision
            Assert.That("Foo", Is.StringContaining("o"));
            Assert.That("Foo", Is.StringContaining("O").IgnoreCase);
            Assert.That("Foo", Is.StringStarting("F"));
            Assert.That("Foo", Is.StringEnding("o"));
            Assert.That("Foo", Is.StringMatching("^[F]")); // Regular ecpression match

            // Collection Comparison
            Assert.That(new List<int> {1, 2, 3}, Has.All.GreaterThan(0));
            Assert.That(new List<int> {1, 2, 3}, Is.All.GreaterThan(0));
            Assert.That(new List<int> {1, 2, 3}, Has.None.GreaterThan(4));
            Assert.That(new List<int> {1, 2, 3}, Has.Some.GreaterThan(2));
            Assert.That(new List<int> {1, 2, 3}, Has.Count.EqualTo(3));
            Assert.That(new List<int> {1, 2, 3}, Is.Unique);
            Assert.That(new List<int> {1, 2, 3}, Has.Member(1)); // Contains
            Assert.That(new List<int> {1, 2, 3},
                Is.EquivalentTo(new List<int> {3, 2, 1})); // Same data without carring about the order
            Assert.That(new List<int> {1, 2,}, Is.SubsetOf(new List<int> {3, 2, 1})); // All are cotained.

            // Property Constraint
            Assert.That(new List<int>(), Has.Property("Count").EqualTo(0)); // Checks public property
            Assert.That(new List<int>(), Has.Count.EqualTo(0)); // Same as Has.Property("Count").EqualTo(0)
            Assert.That(string.Empty, Has.Length.EqualTo(0)); // Same as Has.Property("Length").EqualTo(0)
            Assert.That(new Exception("Foo"), Has.Message.EqualTo("Foo")); // Exception has message
            Assert.That(
                new Exception("Foo", new ArgumentException("Moo")), // Inner exception is of type and has message
                Has.InnerException.InstanceOf<ArgumentException>().And.InnerException.Message.EqualTo("Moo"));

            // Throws Constraint
            // See also Property constrains for Message and Inner Exception
            // Throws.Exception allow AND, Throws.InstanceOf<> is short hand
            #region exceptions
            Assert.That(() => new ArgumentException("Foo"),
                Throws.Exception.InstanceOf<ArgumentException>().And.Message.EqualTo("Foo"));

            Assert.That(() => new ArgumentNullException(null),
                Throws.Exception.TypeOf<ArgumentNullException>());

            Assert.That(() => new ArgumentNullException(null),
                Throws.InstanceOf<ArgumentNullException>());

            Assert.That(() => { }, Throws.Nothing);

            Assert.That(() => new Exception("Foo", new ArgumentException("Moo")),
                Throws.Exception.Message.EqualTo("Foo").And.InnerException.InstanceOf<ArgumentException>());

            Assert.That(() => new ArgumentException(), Throws.ArgumentException);
#endregion
            #region Compound Constraints

            Assert.That(2, Is.Not.EqualTo(1));
            Assert.That(new List<int> {1, 2, 3}, Is.All.GreaterThan(0));

            Assert.That(1, Is.GreaterThan(0).And.LessThan(2)); // .And amd & are the same
            Assert.That(1, Is.GreaterThan(0) & Is.LessThan(2));

            Assert.That(1, Is.LessThan(10) | Is.GreaterThan(0)); // .Or and | are the same
            Assert.That(1, Is.LessThan(10).Or.GreaterThan(0));

            #endregion


            #region Delayed Assertions

            Assert.That(() => true, Is.True.After(10)); // Passes after x time
            Assert.That(() => true, Is.True.After(30, 10)); // Passes after x time and polling..

            #endregion

            #region List Mapper

            string[] strings = {"a", "ab", "abc"};
            int[] lengths = {1, 2, 3};

            Assert.That(List.Map(strings).Property("Length"), Is.EqualTo(lengths));

            Assert.That(new ListMapper(strings).Property("Length"),
                Is.EqualTo(lengths));

            Assert.That(
                List.Map(new List<string> {"1", "22", "333"})
                    .Property("Length"), // Allows all elememts in a list to have a condition provided for them
                Is.EqualTo(new List<int> {1, 2, 3}));

            #endregion
        }

        [Test]
        public void ExceptionAssertions()
        {
            #region Exceptions

            //Using lambda functions with no parameters for ease of explanation/Implementation

            Assert.Throws(typeof(ArgumentNullException), () => new ArgumentNullException());

            Assert.Throws<ArgumentNullException>(() => new ArgumentNullException());

            Assert.DoesNotThrow(() => { });

            Assert.Catch(typeof(Exception), () => new ArgumentNullException());

            // Similar as Throws but allows any derrived class of the thrown exception
            Assert.Catch<Exception>(() => new ArgumentNullException());

            #endregion
        }
    }
}
