using System.Linq;
using Akka.TestKit.VsTest;
using AkkaPlayground.Actors;
using AkkaPlayground.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AkkaPlayground.Test
{
    [TestClass]
    public class TestControllerActorTest : TestKit
    {
        [TestMethod]
        public void ShouldHaveChild()
        {
            var controller = ActorOfAsTestActorRef<TestControllerActor>();            
            controller.Tell(new CreateTestActor("Actor1"));

            Assert.IsTrue(controller.UnderlyingActor.Children.Any(x => x.Key == "Actor1"));
        }
    }
}
