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
using NUnit.Framework;

namespace Memmexx.InterfaceImplementor.UnitTests
{
    [TestFixture]
    public class UnitTests
    {
        public interface ITestInterface1
        {
        }

        public interface ITestPrimitiveInterface
        {
            int Int { get; set; }
            long Long { get; set; }
            bool Bool { get; set; }
            DateTime DateTime { get; set; }
            string String { get; set; }
        }

        public interface ITestWithMethodsSimple
        {
            void Foo();
        }

        public interface ITestWithMethodsReturnValuesPrimitive
        {
            int Int();
            long Long();
            bool Bool();
            DateTime DateTime();
            string String();
        }

        public interface ITestWithMethodsReturnValuesObject
        {
            UnitTests UnitTests();
        }

        public interface ITestWithMethodsArguments
        {
            void TestMethod(int i, long l, bool b, DateTime d, UnitTests u);
        }

        public interface ITestWithOnlyGet
        {
            int OnlyGet { get; }
        }

        public interface ITestWithOnlySet
        {
            int OnlySet { set; }
        }

        public interface ITestWithObjects
        {
            ITestWithObjects SubInterface { get; set; }
            UnitTests SubObject { get; set; }
        }

        public interface IBaseInterface
        {
            int MyInt { get; set; }
            void BaseMethod();
        }

        public interface ISuperInterface : IBaseInterface
        {
            long MyLong { get; set; }
            void SuperMethod();
        }

        public interface IGeneric<T>
        {
            T MyT { get; set; }
        }

        [Test]
        [ExpectedException(typeof (InterfaceObjectFactory.TypeIsNotAnInterface))]
        public void TestErrorOnClass()
        {
            UtilFunctions.SendExceptionToConsole(delegate { InterfaceObjectFactory.New<UnitTests>(); });
        }

        [Test]
        public void TestGeneric()
        {
            var gInt = InterfaceObjectFactory.New<IGeneric<int>>();

            gInt.MyT = 54;

            Assert.AreEqual(54, gInt.MyT, "Generic integer doesn't work");
        }

        [Test]
        public void TestInheritance()
        {
            var obj = InterfaceObjectFactory.New<ISuperInterface>();

            obj.BaseMethod();
            obj.SuperMethod();

            obj.MyInt = 4;
            obj.MyLong = -333;

            Assert.AreEqual(4, obj.MyInt, "MyInt set incorrectly");
            Assert.AreEqual(-333, obj.MyLong, "MyLong set incorrectly");
        }

        [Test]
        public void TestInterfaceWithPrimitives()
        {
            var i = InterfaceObjectFactory.New<ITestPrimitiveInterface>();

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
            Assert.IsTrue(i.Bool);
            Assert.IsTrue(i.DateTime == DateTime.MaxValue);
            Assert.IsTrue(i.String.Equals("This is the highs"));
        }

        [Test]
        public void TestInterfaceWithPrimitivesSanity()
        {
            var i = InterfaceObjectFactory.New<ITestPrimitiveInterface>();

            Assert.IsTrue(i is ITestPrimitiveInterface, "Wrong interface generated");
        }

        [Test]
        public void TestMethodsSimple()
        {
            var testWithMethodsSimple = InterfaceObjectFactory.New<ITestWithMethodsSimple>();
            testWithMethodsSimple.Foo();
        }

        [Test]
        public void TestOnlyGet()
        {
            var testWithOnlyGet = InterfaceObjectFactory.New<ITestWithOnlyGet>();

            Assert.IsTrue(testWithOnlyGet is ITestWithOnlyGet, "Wrong type returned");

            int i = testWithOnlyGet.OnlyGet;
        }

        [Test]
        public void TestOnlySet()
        {
            var testWithOnlySet = InterfaceObjectFactory.New<ITestWithOnlySet>();

            Assert.IsTrue(testWithOnlySet is ITestWithOnlySet, "Wrong type returned");

            testWithOnlySet.OnlySet = 18;
        }

        [Test]
        public void TestSanity()
        {
            var i = InterfaceObjectFactory.New<ITestInterface1>();

            Assert.IsTrue(i is ITestInterface1, "Wrong interface generated");
        }

        [Test]
        public void TestWithMethodsArguments()
        {
            var testWithMethodsArguments = InterfaceObjectFactory.New<ITestWithMethodsArguments>();

            testWithMethodsArguments.TestMethod(1, 2, false, DateTime.Now, this);
        }

        [Test]
        public void TestWithMethodsReturnValuesObject()
        {
            var testWithMethodsReturnValuesObject = InterfaceObjectFactory.New<ITestWithMethodsReturnValuesObject>();

            testWithMethodsReturnValuesObject.UnitTests();
        }

        [Test]
        public void TestWithMethodsReturnValuesPrimitive()
        {
            var testWithMethodsReturnValuesPrimitive =
                InterfaceObjectFactory.New<ITestWithMethodsReturnValuesPrimitive>();

            testWithMethodsReturnValuesPrimitive.Bool();
            testWithMethodsReturnValuesPrimitive.DateTime();
            testWithMethodsReturnValuesPrimitive.Int();
            testWithMethodsReturnValuesPrimitive.Long();
            testWithMethodsReturnValuesPrimitive.String();
        }

        [Test]
        public void TestWithObjects()
        {
            var obj = InterfaceObjectFactory.New<ITestWithObjects>();

            obj.SubInterface = obj;
            obj.SubObject = this;

            Assert.IsTrue(obj.SubInterface == obj);
            Assert.IsTrue(obj.SubObject == this);
        }

        [Test]
        public void TestWithObjectsSanity()
        {
            var obj = InterfaceObjectFactory.New<ITestWithObjects>();
        }
    }
}