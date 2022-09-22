using InState.Abstracts;
using InState.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace InState.Tests;

[TestClass]
public class WhenUsingInStateMachine
{
    private Activity<string, Triggers> finishActivity;
    private State<string, Triggers> finishState;
    private InStateMachine<string, Triggers> machine;
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

        startState = mockStartState.Object;
        finishState = mockFinishState.Object;

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

        startState.Data.Add("math");
        startState.Data.Add("geography");
        startState.Data.Add("science");

        Mock<InStateMachine<string, Triggers>> mockMachine = new Mock<InStateMachine<string, Triggers>>();
        machine = mockMachine.Object;
    }

    public void InitializeStates()
    {
        machine.States.Add(startState);
        machine.States.Add(finishState);
        machine.InitialState(startState);

        startState
            .When(Triggers.Finish)
                .TransitionTo(finishState)
                    .Then(finishActivity);    
    }

    [TestMethod]
    public void CurrentStateShouldReturnCorrectState()
    {
        InitializeStates();

        IState<string, Triggers> currentState = machine.CurrentState;
        Assert.IsNotNull(currentState);
        Assert.AreEqual(startState, currentState);
    }

    [TestMethod]
    public void FireShouldTransitionToAppropriateState()
    {
        InitializeStates();

        machine.Fire(Triggers.Finish);

        IState<string, Triggers> currentState = machine.CurrentState;
        Assert.IsNotNull(currentState);
        Assert.IsNotNull(currentState.AssociatedActivity);
        Assert.AreEqual(finishState, currentState);
        Assert.AreEqual(3, currentState.Data.Count());
    }

    [TestMethod]
    public void FireShouldTransitionToFirstStateWhenStatesHaveBeenDefinedAndNoInitialStateHasBeenSet()
    {
        machine.States.Add(startState);
        machine.States.Add(finishState);

        startState
            .When(Triggers.Finish)
                .TransitionTo(finishState);

        IState<string, Triggers> currentState = machine.Fire(Triggers.Start);
        Assert.IsNotNull(currentState);
        Assert.AreEqual(startState, currentState);
    }

    [TestMethod]
    public void FireShouldNotTransitionAndReturnCurrentStateWhenNoTransitionBehaviorHasBeenDefinedForTheCurrentState()
    {
        machine.States.Add(startState);
        machine.States.Add(finishState);
        machine.InitialState(startState);

        machine.Fire(Triggers.Finish);

        IState<string, Triggers> currentState = machine.CurrentState;
        Assert.IsNotNull(currentState);
        Assert.AreEqual(startState, currentState);
    }

    [TestMethod]
    public void FireShouldNotTransitionAndReturnCurrentStateWhenNoPermittedTransitionHasBeenDefinedForTheCurrentState()
    {
        machine.States.Add(startState);
        machine.States.Add(finishState);
        machine.InitialState(startState);

        startState
            .When(Triggers.Finish);

        machine.Fire(Triggers.Finish);

        IState<string, Triggers> currentState = machine.CurrentState;
        Assert.IsNotNull(currentState);
        Assert.AreEqual(startState, currentState);
    }

    [TestMethod]
    public void FireShouldNotTransitionAndReturnNullWhenNoStatesHaveBeenDefined()
    {
        IState<string, Triggers> currentState = machine.Fire(Triggers.Start);
        Assert.IsNull(currentState);
    }

    [TestMethod]
    public void FireShouldTransitionAndProcessStateActivityWhenGivenValidTriggerAndObjectParameter()
    {
        InitializeStates();

        machine.Fire(Triggers.Finish, "history");

        IState<string, Triggers> currentState = machine.CurrentState;
        Assert.IsNotNull(currentState);
        Assert.AreEqual(finishState, currentState);
        Assert.AreEqual(4, currentState.Data.Count());
    }

    [TestMethod]
    public void FireShouldTransitionAndProcessStateActivityWhenGivenValidTriggerAndObjectParameters()
    {
        InitializeStates();

        machine.Fire(Triggers.Finish, "history", "lunch");

        IState<string, Triggers> currentState = machine.CurrentState;
        Assert.IsNotNull(currentState);
        Assert.AreEqual(finishState, currentState);
        Assert.AreEqual(5, currentState.Data.Count());
    }

    [TestMethod]
    public void FireShouldTransitionAndProcessStateActivityWhenGivenValidTriggerAndNullObjectParameter()
    {
        InitializeStates();

        machine.Fire<string>(Triggers.Finish, null);

        IState<string, Triggers> currentState = machine.CurrentState;
        Assert.IsNotNull(currentState);
        Assert.AreEqual(finishState, currentState);
        Assert.AreEqual(4, currentState.Data.Count());
    }

    [TestMethod]
    public void FireShouldTransitionAndProcessStateActivityWhenGivenValidTriggerAndNullObjectParameters()
    {
        InitializeStates();

        machine.Fire<string, string>(Triggers.Finish, null, null);

        IState<string, Triggers> currentState = machine.CurrentState;
        Assert.IsNotNull(currentState);
        Assert.AreEqual(finishState, currentState);
        Assert.AreEqual(5, currentState.Data.Count());
    }    
}
