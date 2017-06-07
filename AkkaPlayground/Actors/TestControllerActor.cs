using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Threading.Tasks;
using Akka.Actor;
using AkkaPlayground.Messages;

namespace AkkaPlayground.Actors
{
    public class TestControllerActor : ReceiveActor
    {
        public Dictionary<string, IActorRef> Children { get; } = new Dictionary<string, IActorRef>();
        public TestControllerActor()
        {            
            Receive<CreateTestActor>(m =>
            {
                ColorConsole.WriteOrange($"The Sender of CreateTestActor is '{Sender}'");
                var child = Context.ActorOf(Props.Create(() => new TestActor(m.ActorName)), m.ActorName);
                Children.Add(m.ActorName, child);
            });

            Receive<AsyncResponseMessage>(m => ColorConsole.WriteGreen($"Async result from {Sender}"));           
        }
    }
}
