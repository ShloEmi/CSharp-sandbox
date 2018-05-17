using System.Collections;
using Autofac;
using FakeItEasy;
using NUnit.Framework;

namespace sandbox.Frameworks
{
    /// <summary>
    /// Autofac Tests
    /// </summary>
    [TestFixture]
    public class AutofacTests
    {
        private ContainerBuilder _containerBuilder;

        [SetUp]
        public void SetUpTest()
        {
            _containerBuilder = new ContainerBuilder();
        }

        /// <summary>
        /// Resolve - Resolving an interface and expecting method called.
        /// </summary>
        [Test]
        public void Resolve_ResolvingAnInterface_ExpectingMethodCalled_Test()
        {
            // arrange
            // fake it
            IList fake = A.Fake<IList>();
            A.CallTo(() => fake.Contains(A<object>._)).Returns(true);

            // ioc
            _containerBuilder.RegisterInstance(fake).As<IList>();
            IContainer container = _containerBuilder.Build();

            IList testClass = container.Resolve<IList>();

            // act
            bool response = testClass.Contains(null);

            // assert
            Assert.IsTrue(response);
            A.CallTo(()=>fake.Contains(null)).MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}
