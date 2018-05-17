using System;
using System.Diagnostics;
using NUnit.Framework;

namespace sandbox.CSharp
{
    [TestFixture]
    public class ExceptionsTests
    {
        [Test]
        public void Test_Exception_Rethrows_true()
        {
            try
            {
                Assert.Throws<ArgumentException>(() => { Throw(new ArgumentException()); });
            }
            catch (ArgumentException ex)
            {
            }
            catch (Exception ex)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void Test_Exception_Rethrows_true2()
        {
            try
            {
                Throw(new ArgumentException());
            }
            catch (ArgumentException ex)
            {
            }

            Debug.WriteLine("All OK!");
        }

        [Test]
        public void Test_Exception_Rethrows_true3()
        {
            ArgumentException argumentException = new ArgumentException();

            try
            {
                try
                {
                    Throw(argumentException);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                Assert.AreEqual(argumentException,ex);
                return;
            }

            Assert.Fail();
        }

        [Test]
        public void Test_Exception_Rethrows_true4()
        {
            ArgumentException argumentException = new ArgumentException();

            try
            {
                try
                {
                    try
                    {
                        Throw(argumentException);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                catch (IgnoreException dummyExceptionHandler)
                {
                    Assert.Fail();
                }
                catch
                {
                    throw;
                }

            }
            catch (Exception ex)
            {
                Assert.AreEqual(argumentException,ex);
                return;
            }

            Assert.Fail();
        }


        [Test]
        public void Test_Exception_Rethrows_true5()
        {
            ArgumentException argumentException = new ArgumentException();

            try
            {
                try
                {
                    try
                    {
                        Throw(argumentException);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                catch (IgnoreException dummyExceptionHandler)
                {
                    Assert.Fail();
                }
            }
            catch (Exception ex)
            {
                Assert.AreEqual(argumentException,ex);
                return;
            }

            Assert.Fail();
        }


        private static void Throw(Exception ex)
        {
            throw ex;
        }
    }
}
