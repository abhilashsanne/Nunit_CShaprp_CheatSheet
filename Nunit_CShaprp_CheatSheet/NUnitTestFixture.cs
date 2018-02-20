using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace Nunit_CShaprp_CheatSheet
{
    [TestFixture]
    public class NUnitTestFixture
    {
        public NUnitTestFixture()
        {
        }

        #region Setup Methods
        [TestFixtureSetUp]
        public void TestClassFixture()
        {
        }
        [SetUp]
        public void TestSetup()
        {
        }
        #endregion

        #region Individual Tests
        
        #endregion

        #region Test Suites

        #endregion

        #region DataDriven Tests
        [Test]
        public void Test()
        {

        }
        #endregion
       
      
       

        #region TearDown Methods
        [TearDown]
        public void TestTearDown()
        {
        }

        [TestFixtureTearDown]
        public void TestClassTearDown()
        {

          
        }
        #endregion
       
    }
}

