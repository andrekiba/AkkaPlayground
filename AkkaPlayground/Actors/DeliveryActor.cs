using System;
using Akka.Actor;
using AkkaPlayground.Messages;

namespace AkkaPlayground.Actors
{
    public class DeliveryActor : ReceiveActor
    {
        private readonly ActorSelection target;
        private readonly object message;
        private readonly int maxRetries;
        private readonly TimeSpan timeout;

        public DeliveryActor(ActorSelection target, object message, int maxRetries, TimeSpan timeout)
        {
            this.target = target;
            this.message = message;
            this.maxRetries = maxRetries;
            this.timeout = timeout;
            var retryCount = 0;

            Receive<ReceiveTimeout>(t =>
            {
                if (retryCount > this.maxRetries)
                    throw new TimeoutException("Unable to deliver the message within the specified number of attempts");
                target.Tell(message);
                retryCount++;
            });

            Receive<Ack>(a =>
            {
                SetReceiveTimeout(null);
                Context.Stop(Self);
            });
        }

        protected override void PreStart()
        {
            SetReceiveTimeout(timeout);
            target.Tell(message);
        }
    }

}
