namespace AkkaPlayground.Messages
{
    public class CreateTestActor
    {
        public string ActorName { get; }

        public CreateTestActor(string actorName)
        {
            ActorName = actorName;
        }
    }
}
