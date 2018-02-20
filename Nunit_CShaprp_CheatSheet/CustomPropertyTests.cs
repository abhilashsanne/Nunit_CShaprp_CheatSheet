using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Nunit_CShaprp_CheatSheet
{
   public class CustomPropertyTests
    {

        [Test, CustomPropertyAttribute(CustomPropertyValue.One)]
        public void CustomPropertyAttributeTest()
        {
            //Custom Property: categorises the test within the test output XML.

            //Customer Property Attributes are extension to Property attribute concept
            //Here you 
        }


            #region Custom Attribute

            #endregion


        public enum CustomPropertyValue
        {
            One,
            Two,
        }
        [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
        public class CustomPropertyAttribute : PropertyAttribute
        {
            public CustomPropertyAttribute(CustomPropertyValue value)
                : base("Custom", value.ToString())
            {
            }
        }
    }
}
