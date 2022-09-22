using InState.Abstracts;
using InState.Behaviors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace InState.Tests
{

    [TestClass]
    public class WhenUsingTransitionBehavior
    {
        private State<string, Triggers> finishState;
        private State<string, Triggers> startState;
        
        [TestInitialize]
        public void Initialize()
        {
            Mock<State<string, Triggers>> mockStartState = new Mock<State<string, Triggers>>();
            mockStartState.Setup(s => s.Name)
                .Returns("Start");

            Mock<State<string, Triggers>> mockFinishState = new Mock<State<string, Triggers>>();
            mockFinishState.Setup(s => s.Name)
                .Returns("Finish");

            finishState = mockFinishState.Object;
            startState = mockStartState.Object;
        }
        
        [TestMethod]
        public void TransitionToShouldReturnNewActivityBehaviorWhenGivenStateToTransitionTo()
        {
            TransitionBehavior<string, Triggers> transitionBehavior = new TransitionBehavior<string, Triggers>(
                Triggers.Start,
                startState
            );
            
            ActivityBehavior<string, Triggers> activityBehavior = transitionBehavior
                .TransitionTo(finishState);

            Assert.IsNotNull(activityBehavior);
        }

        [TestMethod]
        public void TransitionToShouldReturnNullWhenGivenNullForStateToTransitionTo()
        {
            TransitionBehavior<string, Triggers> transitionBehavior = new TransitionBehavior<string, Triggers>(
                Triggers.Start,
                startState
            );

            ActivityBehavior<string, Triggers> activityBehavior = transitionBehavior
                .TransitionTo(null);

            Assert.IsNull(activityBehavior);
        }
    }
}