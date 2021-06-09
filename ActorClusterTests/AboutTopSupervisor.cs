using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using ActorCluster;
using Akka.Actor;
using FluentAssertions;
using Xunit;

namespace ActorClusterTests
{
    public class AboutTopSupervisor: IDisposable
    {
        private const string TestActorSystemName = "TestActorSystemName";
        private const string TopSupervisorActorName = "TopSupervisor";
        private const int TimeoutForLocatingActor = 3;

        private static readonly string TopSupervisorActorPath = $"akka://{TestActorSystemName}/user/{TopSupervisorActorName}";

        private readonly ActorSystem actorSystem;
        private readonly IActorRef topSupervisor;

        public AboutTopSupervisor()
        {
            actorSystem = ActorSystem.Create(TestActorSystemName);
            topSupervisor = actorSystem.ActorOf<TopSupervisor>(TopSupervisorActorName);
        }

        [Fact]
        public async Task it_has_the_collection_of_all_group_guides()
        {
            var guides = await topSupervisor.Ask(new TopSupervisor.GetGuidesQuery("some-correlation-Id"), TimeSpan.FromSeconds(TimeoutForLocatingActor));
            guides.Should().NotBeNull();
            guides.Should().BeOfType<ImmutableArray<GroupGuide>>();
        }

        public void Dispose()
        {
            actorSystem?.Dispose();
        }
    }
}