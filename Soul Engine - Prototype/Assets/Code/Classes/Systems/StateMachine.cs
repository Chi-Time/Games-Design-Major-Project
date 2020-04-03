using UnityEngine;
using Utilities;

namespace SoulEngine
{
	public class StateMachine<T> where T : class
	{
		/// <summary>The owner of the state machine instance.</summary>
		private readonly T _Owner = null;
		/// <summary>The current state executing globally.</summary>
		private IState<T> _GlobalState = null;
		/// <summary>The current state being executed.</summary>
		private IState<T> _CurrentState = null;
		/// <summary>The state that we previously exited from.</summary>
		private IState<T> _PreviousState = null;
		/// <summary>Regulator for all non-physics updates.</summary>
		private readonly Regulator _ExecutionRegulator = null;
		/// <summary>Regulator for all physics updates.</summary>
		private readonly Regulator _PhysicsRegulator = null;

		/// <summary>Constructs a new state machine instance.</summary>
		/// <param name="owner">The agent which owns this instance and will be affected.</param>
		public StateMachine (T owner)
		{
			_Owner = owner;
			_ExecutionRegulator = new Regulator (0.0f);
			_PhysicsRegulator = new Regulator (0.0f);
		}
		
		/// <summary>Constructs a new state machine instance.</summary>
		/// <param name="owner">The agent which owns this instance and will be affected.</param>
		/// <param name="tickInterval">How often (in seconds) should the AI update it's logic.</param>
		public StateMachine (T owner, float tickInterval)
		{
			_Owner = owner;
			_ExecutionRegulator = new Regulator (tickInterval);
			_PhysicsRegulator = new Regulator (0.0f);
		}

		/// <summary>Constructs a new state machine instance.</summary>
		/// <param name="owner">The agent which owns this instance and will be affected.</param>
		/// <param name="tickInterval">How often (in seconds) should the AI update it's logic.</param>
		/// <param name="physicsTickInterval">How often (in seconds) should the AI update it's logic in the physics step.</param>
		public StateMachine (T owner, float tickInterval, float physicsTickInterval)
		{
			_Owner = owner;
			_ExecutionRegulator = new Regulator (tickInterval);
			_PhysicsRegulator = new Regulator (physicsTickInterval);
		}

		/// <summary>Executes the logic contained in it's current states if they exist.</summary>
		public void Run ()
		{
			if (!_ExecutionRegulator.HasElapsed (true)) 
				return;
			
			_GlobalState?.Execute (_Owner);
			_CurrentState?.Execute (_Owner);
		}

		/// <summary>Executes the logic contained in it's current states if they exist.</summary>
		public void PhysicsRun ()
		{
			if (!_ExecutionRegulator.HasElapsed (true))
				return;

			_GlobalState?.PhysicsExecute (_Owner);
			_CurrentState?.PhysicsExecute (_Owner);
		}

		/// <summary>Changes the current state of the machine.</summary>
		/// <param name="state">The new state to switch to.</param>
		public void ChangeState (IState<T> state)
		{
			_PreviousState = _CurrentState;
			_CurrentState?.Exit (_Owner);
			_CurrentState = state;
			_CurrentState?.Enter (_Owner);
		}

		/// <summary>Changes the current state of the machine back to a previous member.</summary>
		public void RevertToPreviousState ()
		{
			ChangeState (_PreviousState);
		}

		/// <summary>Changes the current global state of the machine.</summary>
		/// <param name="state">The new global state to update.</param>
		public void ChangeGlobalState (IState<T> state)
		{
			_GlobalState?.Exit (_Owner);
			_GlobalState = state;
			_GlobalState?.Enter (_Owner);
		}
		
		/// <summary>Determines if the given instance is the owner of the machine.</summary>
		/// <param name="owner">The owner instance to check for.</param>
		/// <returns>True: If the machine is owned by the given instance.</returns>
		public bool IsOwner (T owner)
		{
			return Equals (_Owner, owner);
		}

		/// <summary>Determines if the given state is the one the machine is currently in.</summary>
		/// <param name="state">The state to check for.</param>
		/// <returns>True: If the machine is in the given state.</returns>
		public bool IsInCurrentState (IState<T> state)
		{
			return Equals (_CurrentState, state);
		}
	}
}
