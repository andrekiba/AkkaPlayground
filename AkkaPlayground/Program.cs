using System;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using AkkaPlayground.Actors;
using AkkaPlayground.Messages;
using Nito.AsyncEx;

namespace AkkaPlayground
{
    internal class Program
    {
        private static ActorSystem ActorSystem { get; set; }

        private static IActorRef TestController { get; set; }

        private static void Main(string[] args)
        {
            try
            {
                AsyncContext.Run(() => MainAsync(args));
            }
            catch (Exception ex)
            {
                ColorConsole.WriteRed(ex.Message);
            }
        }

        private static async Task MainAsync(string[] args)
        {
            ActorSystem = ActorSystem.Create("TestActorSystem");

            //default
            //var controllerProp = Props.Create<TestControllerActor>()
            //    .WithSupervisorStrategy(new OneForOneStrategy(e => Directive.Restart));
            //TestController = ActorSystem.ActorOf(controllerProp, "TestController");
            TestController = ActorSystem.ActorOf<TestControllerActor>("TestController");

            DisplayInstructions();

            var stop = false;
            do
            {
                var action = Console.ReadLine();

                if (action == null)
                    continue;

                string actorName;

                if (Regex.IsMatch(action, @"create\s\w+"))
                {
                    actorName = action.Split(' ')[1];
                    CreateTestActor(actorName);
                }              
                else if (Regex.IsMatch(action, @"task\s\w+"))
                {
                    actorName = action.Split(' ')[1];
                    DoAsyncTask(actorName);
                }
                else if (Regex.IsMatch(action, @"poison\s\w+"))
                {
                    actorName = action.Split(' ')[1];
                    PoisonPillTestActor(actorName);
                }
                else if (action == "stop")
                {
                    await StopSystem();
                    stop = true;
                }
                else
                {
                    ColorConsole.WriteRed("Unknown command");
                }
            } while (!stop);
        }

        #region Methods    

        private static void DoAsyncTask(string actorName)
        {
            ActorSystem.ActorSelection($"/user/TestController/{actorName}")
                  .Tell(new AsyncTaskMessage());
        }

        private static void PoisonPillTestActor(string actorName)
        {
            ActorSystem.ActorSelection($"/user/TestController/{actorName}")
                  .Tell(PoisonPill.Instance);
        }

        private static void CreateTestActor(string actorName)
        {
            TestController.Tell(new CreateTestActor(actorName));
        }

        private static async Task StopSystem()
        {
            await ActorSystem.Terminate();
            ColorConsole.WriteGreen("Actor system shutdown");
            Console.ReadLine();
        }

        private static void DisplayInstructions()
        {
            Thread.Sleep(500);
            ColorConsole.WriteWhite("Available commands:");
            ColorConsole.WriteWhite("create\t<actorname>");
            ColorConsole.WriteWhite("task\t<actorname>");
            ColorConsole.WriteWhite("poison\t<actorname>");
            ColorConsole.WriteWhite("stop");
            Console.WriteLine();
        }

        #endregion
    }
}
