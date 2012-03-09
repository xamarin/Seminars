using System;
using NUnit.Framework;
using MT52Demo;

namespace MT52DemoTests
{
    [TestFixture]
    public class DemoTests
    {
        [Test]
        public void Pass ()
        {
            Assert.True (true);
        }

        [Test]
        public void Fail ()
        {
            Assert.False (true);
        }

        [Test]
        [Ignore ("another time")]
        public void Ignore ()
        {
            Assert.True (false);
        }
        
        [Test]
        public void DefaultTaskNameTest ()
        {
            var task = new Task ();
            Assert.False (String.IsNullOrEmpty (task.Name), "Tasks created without a name should have a default name");
        }
    }
}