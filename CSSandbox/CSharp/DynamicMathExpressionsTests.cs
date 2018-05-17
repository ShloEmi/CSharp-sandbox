using NUnit.Framework;
using System.Data;


namespace sandbox.CSharp
{
    [TestFixture]
    public class DynamicMathExpressionsTests

    {
        [Test]
        public void DynamicMathExpressionsTest()
        {

            DataTable dt = new DataTable();
            var v = dt.Compute("3 * (2+4)", filter: "");

            Assert.AreEqual((3*(2+4)).ToString(),v.ToString());
        }
    }
}
