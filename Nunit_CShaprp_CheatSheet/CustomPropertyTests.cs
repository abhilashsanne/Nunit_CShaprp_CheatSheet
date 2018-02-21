using System;
using System.Collections;
using NUnit.Framework;

namespace Nunit_CShaprp_CheatSheet
{
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
            //Read and store current test case details
            ReadingTestCaseAttributes();
        }

        [Test,Category("CustomAttributeTest"), CustomPropertyAttribute(CustomPropertyValue.One), Description("Custom Attribute Testing")]
        public void CustomPropertyAttributeTest()
        {
            //Custom Property: categorises the test within the test output XML.

            //Customer Property Attributes are extension to Property attribute concept
            //NOTE: Your test case execution did not begin yet but you have its attributes read
        }


        #region Custom Attribute

        #endregion

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
            ArrayList temp = (ArrayList) TestContext.CurrentContext.Test.Properties["_CATEGORIES"];
            _testCategory = temp[0].ToString();
        }
    }
}
