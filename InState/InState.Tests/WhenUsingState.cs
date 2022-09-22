using InState.Abstracts;
using InState.Behaviors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace InState.Tests
{
    [TestClass]
    public class WhenUsingState
    {
        private State<string, Triggers> state;

        [TestInitialize]
        public void Initialize()
        {
            Mock<State<string, Triggers>> mockState = new Mock<State<string, Triggers>>();
            mockState.Setup(s => s.Name)
                .Returns("Start");

            state = mockState.Object;
        }

        [TestMethod]
        public void WhenReturnsTransactionBehaviorAssociatedWithStateAndUpdatesStatePermittedTransitionsWhenGivenTrigger()
        {
            TransitionBehavior<string, Triggers> transitionBehavior = state.When(Triggers.Finish);

            Assert.IsNotNull(transitionBehavior);
            Assert.AreEqual(transitionBehavior.OriginatingState, state);
            Assert.AreEqual(1, state.PermittedTransitions.Count());
            Assert.AreEqual(1, transitionBehavior.OriginatingState.PermittedTransitions.Count());
        }
    }
}