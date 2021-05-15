using System;
using System.Collections.Generic;
using Akka.Actor;
using Akka.Event;

namespace ActorCluster
{
    public class TopSupervisor : UntypedActor
    {
        public ICollection<GroupGuide> Guides { get; }

        public ILoggingAdapter Log { get; } = Context.GetLogger();

        public TopSupervisor()
        {
            Guides = new List<GroupGuide>();
        }

        protected override void PreStart() => Log.Info("Application started");

        protected override void PostStop() => Log.Info("Application stopped");

        protected override void OnReceive(object message) => Log.Info("Top Supervisor received message: " + Convert.ToString(message));

        public static Props Props() => Akka.Actor.Props.Create<TopSupervisor>();
    }
}