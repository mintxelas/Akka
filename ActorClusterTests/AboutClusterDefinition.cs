using ActorCluster;
using Akka.Actor;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ActorClusterTests
{
    public class AboutClusterDefinition: IDisposable
    {
        private const string TestActorSystemName = "TestActorSystemName";
        private const string TopSupervisorActorName = "TopSupervisor";
        private const int TimeoutForLocatingActor = 3;

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
        public async Task top_supervisor_is_started_within_time()
        {
            var expectedActor = actorSystem.ActorOf<TopSupervisor>(TopSupervisorActorName);
            
            var topSupervisorRef = actorSystem.ActorSelection(TopSupervisorActorPath);
            var topSupervisor = await topSupervisorRef.ResolveOne(TimeSpan.FromSeconds(TimeoutForLocatingActor));
            Assert.Equal(expectedActor, topSupervisor);
        }

        public void Dispose()
        {
            actorSystem?.Dispose();
        }
    }
}
