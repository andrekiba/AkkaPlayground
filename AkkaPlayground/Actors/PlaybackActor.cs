using System;
using Akka.Actor;
using AkkaPlayground.Messages;

namespace AkkaPlayground.Actors
{
    public class PlaybackActor : ReceiveActor
    {
        public PlaybackActor()
        {
            Console.WriteLine("Creating a PlaybackActor");

            Receive<PlayMovieMessage>(HandlePlayMovieMessage, message => message.UserId == 43);
        }

        private static void HandlePlayMovieMessage(PlayMovieMessage message)
        {
            Console.WriteLine("Received movie title " + message.MovieTitle);
            Console.WriteLine("Received user Id " + message.UserId);
        }
    }
}
