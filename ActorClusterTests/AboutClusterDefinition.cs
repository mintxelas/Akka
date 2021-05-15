using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ActorCluster;
using Akka.Actor;
using Akka.Configuration;
using Akka.Configuration.Hocon;
using Xunit;

namespace ActorClusterTests
{
    public class AboutClusterDefinition: IDisposable
    {
        private const string TestActorSystemName = "TestActorSystemName";
        private const string TopSupervisorActorName = "TopSupervisor";
        private const int TimeoutForActorCreation = 3;

        private static readonly string TopSupervisorActorPath = $"akka://{TestActorSystemName}/user/{TopSupervisorActorName}";

        private readonly ActorSystem actorSystem;

        public AboutClusterDefinition()
        {
            actorSystem = ActorSystem.Create(TestActorSystemName);
        }

        [Fact]
        public void actor_system_can_be_started()
        {
            Assert.Equal(TestActorSystemName, actorSystem.Name);
        }

        [Fact]
        public async Task top_supervisor_is_started_with_system_within_time()
        {
            var expectedActor = actorSystem.ActorOf(TopSupervisor.Props(), TopSupervisorActorName);

            var topSupervisorRef = actorSystem.ActorSelection(TopSupervisorActorPath);
            var topSupervisor = await topSupervisorRef.ResolveOne(TimeSpan.FromSeconds(TimeoutForActorCreation));
            Assert.Equal(expectedActor, topSupervisor);
        }

        public void Dispose()
        {
            actorSystem?.Dispose();
        }
    }
}
