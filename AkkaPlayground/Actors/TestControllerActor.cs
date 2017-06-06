using System;
using System.Drawing.Text;
using System.Threading.Tasks;
using Akka.Actor;
using AkkaPlayground.Messages;

namespace AkkaPlayground.Actors
{
    public class TestControllerActor : ReceiveActor
    {
        public TestControllerActor()
        {
            
            Receive<CreateTestActor>(m =>
            {
                ColorConsole.WriteOrange($"The Sender of CreateTestActor is '{Sender}'");
                Context.ActorOf(Props.Create(() => new TestActor(m.ActorName)), m.ActorName);
            });

            Receive<AsyncResponseMessage>(m => ColorConsole.WriteGreen($"Async result from {Sender}"));           
        }
    }
}
