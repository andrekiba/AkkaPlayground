using System;
using Akka.Actor;
using AkkaPlayground.Actors;
using AkkaPlayground.Messages;

namespace AkkaPlayground
{
    internal class Program
    {
        private static ActorSystem movieStreamingActorSystem;

        private static void Main(string[] args)
        {
            movieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");
            Console.WriteLine("Actor system created");

            var playbackActorProps = Props.Create<PlaybackActor>();

            var playbackActorRef = movieStreamingActorSystem.ActorOf(playbackActorProps, "PlaybackActor");

            playbackActorRef.Tell(new PlayMovieMessage("Akka.NET: The Movie", 43));

            Console.ReadLine();

            movieStreamingActorSystem.Terminate();
        }
    }
}
