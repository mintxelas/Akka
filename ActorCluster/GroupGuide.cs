using System;
using Akka.Actor;
using Akka.Event;

namespace ActorCluster
{
    public class GroupGuide: UntypedActor
    {
        private readonly string groupName;
        private readonly string name;

        public GroupGuide(string groupName, string name)
        {
            this.groupName = groupName;
            this.name = name;
        }

        public ILoggingAdapter Log { get; } = Context.GetLogger();

        protected override void PreStart() => Log.Info($"GroupGuide {groupName}-{name} joined");

        protected override void PostStop() => Log.Info($"GroupGuide {groupName}-{name} left");
        protected override void OnReceive(object message) => Log.Info($"GroupGuide {groupName}-{name} received message: " + Convert.ToString(message));

        public static Props Props(string groupName, string name) =>
            Akka.Actor.Props.Create(() => new GroupGuide(groupName, name));
    }
}