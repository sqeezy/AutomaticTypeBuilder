/*
 * Interface Object Factory
 * 
 * Automatically generates objects that implement a given interface without the need to provide a 
 * pre-existing class
 * 
 * (c) 2007 Andrew Rondeau
 * http://andrewrondeau.com
 * 
 * Permission is granted to use this program freely in any computer program provided that this 
 * notice is not removed from the source code.  Modified versions of this source code may be 
 * published provided that this notice is altered 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using Memmexx.InterfaceImplementor;

namespace Memmexx.InterfaceImplementor.UnitTests
{
    [TestFixture]
    public class UnitTests
    {
        public interface ITestInterface1 { }

        [Test]
        public void TestSanity()
        {
            ITestInterface1 i = InterfaceObjectFactory.New<ITestInterface1>();

            Assert.IsTrue(i is ITestInterface1, "Wrong interface generated");
        }

        [Test]
        [ExpectedException(typeof(InterfaceObjectFactory.TypeIsNotAnInterface))]
        public void TestErrorOnClass()
        {
            UtilFunctions.SendExceptionToConsole(delegate()
            {
                InterfaceObjectFactory.New<UnitTests>();
            });
        }

        public interface ITestPrimitiveInterface
        {
            int Int { get; set;}
            long Long { get; set;}
            bool Bool { get;set;}
            DateTime DateTime { get; set;}
            string String { get; set;}
        }

        [Test]
        public void TestInterfaceWithPrimitivesSanity()
        {
            ITestPrimitiveInterface i = InterfaceObjectFactory.New<ITestPrimitiveInterface>();

            Assert.IsTrue(i is ITestPrimitiveInterface, "Wrong interface generated");
        }

        [Test]
        public void TestInterfaceWithPrimitives()
        {
            ITestPrimitiveInterface i = InterfaceObjectFactory.New<ITestPrimitiveInterface>();

            Assert.IsTrue(i is ITestPrimitiveInterface, "Wrong interface generated");

            i.Int = int.MinValue;
            i.Long = long.MinValue;
            i.Bool = false;
            i.DateTime = DateTime.MinValue;
            i.String = "This is the lows";

            Assert.IsTrue(i.Int == int.MinValue);
            Assert.IsTrue(i.Long == long.MinValue);
            Assert.IsTrue(i.Bool == false);
            Assert.IsTrue(i.DateTime == DateTime.MinValue);
            Assert.IsTrue(i.String.Equals("This is the lows"));

            i.Int = int.MaxValue;
            i.Long = long.MaxValue;
            i.Bool = true;
            i.DateTime = DateTime.MaxValue;
            i.String = "This is the highs";

            Assert.IsTrue(i.Int == int.MaxValue);
            Assert.IsTrue(i.Long == long.MaxValue);
            Assert.IsTrue(i.Bool == true);
            Assert.IsTrue(i.DateTime == DateTime.MaxValue);
            Assert.IsTrue(i.String.Equals("This is the highs"));
        }

        public interface ITestWithMethodsSimple
        {
            void foo();
        }

        [Test]
        public void TestMethodsSimple()
        {
            ITestWithMethodsSimple testWithMethodsSimple = InterfaceObjectFactory.New<ITestWithMethodsSimple>();
            testWithMethodsSimple.foo();
        }

        public interface ITestWithMethodsReturnValuesPrimitive
        {
            int Int();
            long Long();
            bool Bool();
            DateTime DateTime();
            string String();
        }

        [Test]
        public void TestWithMethodsReturnValuesPrimitive()
        {
            ITestWithMethodsReturnValuesPrimitive testWithMethodsReturnValuesPrimitive = InterfaceObjectFactory.New<ITestWithMethodsReturnValuesPrimitive>();

            testWithMethodsReturnValuesPrimitive.Bool();
            testWithMethodsReturnValuesPrimitive.DateTime();
            testWithMethodsReturnValuesPrimitive.Int();
            testWithMethodsReturnValuesPrimitive.Long();
            testWithMethodsReturnValuesPrimitive.String();
        }

        public interface ITestWithMethodsReturnValuesObject
        {
            UnitTests UnitTests();
        }

        [Test]
        public void TestWithMethodsReturnValuesObject()
        {
            ITestWithMethodsReturnValuesObject testWithMethodsReturnValuesObject = InterfaceObjectFactory.New<ITestWithMethodsReturnValuesObject>();

            testWithMethodsReturnValuesObject.UnitTests();
        }

        public interface ITestWithMethodsArguments
        {
            void TestMethod(int i, long l, bool b, DateTime d, UnitTests u);
        }

        [Test]
        public void TestWithMethodsArguments()
        {
            ITestWithMethodsArguments testWithMethodsArguments = InterfaceObjectFactory.New<ITestWithMethodsArguments>();

            testWithMethodsArguments.TestMethod(1, 2, false, DateTime.Now, this);
        }

        public interface ITestWithOnlyGet
        {
            int OnlyGet { get;}
        }

        [Test]
        public void TestOnlyGet()
        {
            ITestWithOnlyGet testWithOnlyGet = InterfaceObjectFactory.New<ITestWithOnlyGet>();

            Assert.IsTrue(testWithOnlyGet is ITestWithOnlyGet, "Wrong type returned");

            int i = testWithOnlyGet.OnlyGet;
        }

        public interface ITestWithOnlySet
        {
            int OnlySet { set;}
        }

        [Test]
        public void TestOnlySet()
        {
            ITestWithOnlySet testWithOnlySet = InterfaceObjectFactory.New<ITestWithOnlySet>();

            Assert.IsTrue(testWithOnlySet is ITestWithOnlySet, "Wrong type returned");

            testWithOnlySet.OnlySet = 18;
        }

        public interface ITestWithObjects
        {
            ITestWithObjects SubInterface { get;set;}
            UnitTests SubObject { get;set;}
        }

        [Test]
        public void TestWithObjectsSanity()
        {
            ITestWithObjects obj = InterfaceObjectFactory.New<ITestWithObjects>();
        }

        [Test]
        public void TestWithObjects()
        {
            ITestWithObjects obj = InterfaceObjectFactory.New<ITestWithObjects>();

            obj.SubInterface = obj;
            obj.SubObject = this;

            Assert.IsTrue(obj.SubInterface == obj);
            Assert.IsTrue(obj.SubObject == this);
        }

        public interface IBaseInterface
        {
            int MyInt { get;set;}
            void BaseMethod();
        }

        public interface ISuperInterface : IBaseInterface
        {
            long MyLong {get;set;}
            void SuperMethod();
        }

        [Test]
        public void TestInheritance()
        {
            ISuperInterface obj = InterfaceObjectFactory.New<ISuperInterface>();

            obj.BaseMethod();
            obj.SuperMethod();

            obj.MyInt = 4;
            obj.MyLong = -333;

            Assert.AreEqual(4, obj.MyInt, "MyInt set incorrectly");
            Assert.AreEqual(-333, obj.MyLong, "MyLong set incorrectly");
        }

        public interface IGeneric<T>
        {
            T MyT { get; set;}
        }

        [Test]
        public void TestGeneric()
        {
            IGeneric<int> gInt = InterfaceObjectFactory.New<IGeneric<int>>();

            gInt.MyT = 54;

            Assert.AreEqual(54, gInt.MyT, "Generic integer doesn't work");
        }
    }
}
