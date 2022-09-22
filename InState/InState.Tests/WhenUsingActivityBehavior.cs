using InState.Abstracts;
using InState.Behaviors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace InState.Tests
{
    [TestClass]
    public class WhenUsingActivityBehavior
    {
        private Activity<string, Triggers> finishActivity;
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

            Mock<Activity<string, Triggers>> mockFinishActivity = new Mock<Activity<string, Triggers>>(finishState);
            mockFinishActivity.Setup(s => s.Execute(It.IsAny<object>()))
                .Returns((string d) =>
                {
                    mockFinishState.Object.Data.Add(d);
                    return mockFinishState.Object;
                });
            mockFinishActivity.Setup(s => s.Execute(It.IsAny<object>(), It.IsAny<object>()))
                .Returns((string d, string e) =>
                {
                    mockFinishState.Object.Data.Add(d);
                    mockFinishState.Object.Data.Add(e);
                    return mockFinishState.Object;
                });

            finishActivity = mockFinishActivity.Object;
            finishState = mockFinishState.Object;
            startState = mockStartState.Object;
        }

        [TestMethod]
        public void ThenPopulatesActivityWhenGivenNonNullActivity()
        {
            ActivityBehavior<string, Triggers> activityBehavior = new ActivityBehavior<string, Triggers>(
                startState,
                finishState
            );

            activityBehavior.Then(finishActivity);

            Assert.IsNotNull(activityBehavior.Activity);
            Assert.AreEqual(finishActivity, activityBehavior.Activity);
        }

        [TestMethod]
        public void ThenPopulatesActivityWithNullWhenGivenNullActivity()
        {
            ActivityBehavior<string, Triggers> activityBehavior = new ActivityBehavior<string, Triggers>(
                startState,
                finishState
            );

            activityBehavior.Then(null);

            Assert.IsNull(activityBehavior.Activity);
            Assert.AreNotEqual(finishActivity, activityBehavior.Activity);
        }

        [TestMethod]
        public void WhenReturnsTransactionBehaviorAssociatedWithOriginatingStateAndUpdatesOriginatingStatePermittedTransitionsWhenGivenTrigger()
        {
            ActivityBehavior<string, Triggers> activityBehavior = new ActivityBehavior<string, Triggers>(
                startState,
                finishState
            );

            TransitionBehavior<string, Triggers> startStateTransitionBehavior = activityBehavior.When(Triggers.Finish);

            Assert.IsNotNull(startStateTransitionBehavior);
            Assert.AreEqual(startStateTransitionBehavior.OriginatingState, startState);
            Assert.AreEqual(1, startStateTransitionBehavior.OriginatingState.PermittedTransitions.Count());
        }
    }
}