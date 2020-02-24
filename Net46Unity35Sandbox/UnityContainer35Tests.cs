using Microsoft.Practices.Unity;
using Moq;
using NUnit.Framework;

namespace Net46Unity35Sandbox
{
    [TestFixture]
    public class UnityContainer35Tests
    {
        [SetUp]
        public void SetUp()
        {
        }

        [TearDown]
        public void TearDown()
        {
        }

        public interface II1
        {
            int f1(int v1);
        }
        public interface II2
        {
            int f2(int v1);
        }
        public interface II3
        {
            int f3(int v1);
        }

        [Test]
        public void Test_CreateChildContainer_ChildAddsInstance_ExpectingParentSeeThatInstanceAndCanUseIt()
        {
            var parentContainer = new UnityContainer();
            var i1Mock = new Mock<II1>();
            i1Mock.Setup(i1 => i1.f1(1)).Returns(42);
            parentContainer.RegisterInstance(i1Mock.Object);

            var i2Mock = new Mock<II2>();
            i2Mock.Setup(i1 => i1.f2(1)).Returns(24);

            //parent
            var i1Obj = parentContainer.Resolve<II1>();
            Assert.Throws<ResolutionFailedException>(() => parentContainer.Resolve<II2>());
            Assert.IsNotNull(i1Obj);
            Assert.AreEqual(0, i1Obj.f1(0));
            Assert.AreEqual(42, i1Obj.f1(1));

            //child
            IUnityContainer childContainer = parentContainer.CreateChildContainer();
            parentContainer.RegisterInstance(i2Mock.Object);

            i1Obj = childContainer.Resolve<II1>();
            Assert.IsNotNull(childContainer.Resolve<II1>());
            Assert.IsNotNull(childContainer.Resolve<II2>());
            Assert.AreEqual(0, i1Obj.f1(0));
            Assert.AreEqual(42, i1Obj.f1(1));

            //parent
            Assert.DoesNotThrow(() => parentContainer.Resolve<II2>());
            childContainer.Dispose();
            childContainer = null;

            Assert.DoesNotThrow(() => parentContainer.Resolve<II2>());
        }
        [Test]

        public void Test_CreateChildContainer_ChildAddsInstance_ExpectingParentSeeThatInstanceAndCanNotUseIt()
        {
            var parentContainer = new UnityContainer();
            var i1Mock = new Mock<II1>();
            i1Mock.Setup(i1 => i1.f1(1)).Returns(42);
            parentContainer.RegisterInstance(i1Mock.Object);

            //parent
            var i1Obj = parentContainer.Resolve<II1>();
            Assert.Throws<ResolutionFailedException>(() => parentContainer.Resolve<II2>());
            Assert.IsNotNull(i1Obj);
            Assert.AreEqual(0, i1Obj.f1(0));
            Assert.AreEqual(42, i1Obj.f1(1));

            var i2Mock = new Mock<II2>();
            i2Mock.Setup(i1 => i1.f2(1)).Returns(24);
            //child
            IUnityContainer childContainer = parentContainer.CreateChildContainer();
            childContainer.RegisterInstance(i2Mock.Object/*, new HierarchicalLifetimeManager()*/);

            i1Obj = childContainer.Resolve<II1>();
            Assert.IsNotNull(childContainer.Resolve<II1>());
            Assert.IsNotNull(childContainer.Resolve<II2>());
            Assert.AreEqual(0, i1Obj.f1(0));
            Assert.AreEqual(42, i1Obj.f1(1));

            //parent
            Assert.Throws<ResolutionFailedException>(() => parentContainer.Resolve<II2>());
        }
    }
}