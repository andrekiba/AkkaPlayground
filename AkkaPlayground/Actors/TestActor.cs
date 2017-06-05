using System;
using System.Threading.Tasks;
using Akka.Actor;
using AkkaPlayground.Messages;

namespace AkkaPlayground.Actors
{
    public class TestActor : ReceiveActor
    {
        private readonly string actorName;

        public TestActor(string actorName)
        {
            this.actorName = actorName;

            Receive<AsyncTaskMessage>(m => HandleTaskMessage(m));
            Receive<int>(m =>
            {
                ColorConsole.WriteOrange($"The Recipient of AsyncTaskResponse is '{Sender}'");
                Sender.Tell(new AsyncResponseMessage());
            });

            ColorConsole.WriteViolet($"TestActor '{actorName}' created");
        }

        #region Methods
        private void HandleTaskMessage(AsyncTaskMessage message)
        {
            var originalSender = Sender;
            ColorConsole.WriteOrange($"The Sender of AsyncTaskMessage is '{originalSender}'");
            DelayAndReturnZero().PipeTo(Self, originalSender);
        }

        private static async Task<int> DelayAndReturnZero()
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            return 0;
        }

        #endregion

        #region Lifecycle

        #endregion
    }
}
