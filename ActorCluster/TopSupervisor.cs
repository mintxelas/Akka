using Akka.Actor;
using Akka.Event;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ActorCluster
{
    public class TopSupervisor : UntypedActor
    {
        private long addedGuideCount = 0;

        private ICollection<GroupGuide> Guides { get; }

        private ILoggingAdapter Log { get; } = Context.GetLogger();

        public TopSupervisor() => Guides = new List<GroupGuide>();

        protected override void PreStart() => Log.Info("Application started");

        protected override void PostStop() => Log.Info("Application stopped");

        protected override void OnReceive(object message)
        {
            Log.Info("Top Supervisor received message: " + Convert.ToString(message));
            switch (message)
            {
                case GetGuidesQuery:
                    Sender.Tell(Guides.ToImmutableArray());
                    break;
                case AddGuideCommand addCommand:
                    GroupGuide.Props(addCommand.GroupName, addCommand.GuideName + addedGuideCount);
                    break;
            }
        }

        //public static Props Props() => Akka.Actor.Props.Create<TopSupervisor>();

        public class GetGuidesQuery
        {
            public string CorrelationId { get; }

            public GetGuidesQuery(string correlationId)
            {
                CorrelationId = correlationId;
            }
        }

        public class AddGuideCommand
        {
            public string CorrelationId { get; }
            public string GroupName { get; }
            public string GuideName { get; }

            public AddGuideCommand(string correlationId, string groupName, string guideName)
            {
                CorrelationId = correlationId;
                GroupName = groupName;
                GuideName = guideName;
            }
        }

    }
}