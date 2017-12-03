using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public static class AnimatorReplicator
{
    public static AnimatorController CreateAnimatorFromPlayerAnimator(Dictionary<AnimationType, AnimationClip> animClipDictionary, Animator playerAnimator)
    {
        AnimatorController newController = new AnimatorController();
        newController.name = "newController";
        newController.AddLayer(newController.MakeUniqueLayerName("BaseLayer"));

        AddPlayerAnimatorParametersToNewController(playerAnimator, newController);
        AddPlayerAnimatorStatesToNewController(playerAnimator, newController, animClipDictionary);
        AddPlayerAnimatorTransitionsToNewController(playerAnimator, newController);

        return newController;
    }

    private static void AddPlayerAnimatorParametersToNewController(Animator playerAnimator, AnimatorController newController)
    {
        foreach (AnimatorControllerParameter param in playerAnimator.parameters)
        {
            newController.AddParameter(param.name, param.type);
        }
    }

    private static void AddPlayerAnimatorStatesToNewController(
        Animator playerAnimator, 
        AnimatorController newController, 
        Dictionary<AnimationType, AnimationClip> animClipDictionary)
    {
        AnimatorController playerAnimatorController = playerAnimator.runtimeAnimatorController as AnimatorController;
        AnimatorStateMachine rootStateMachine = newController.layers[0].stateMachine;
        foreach (ChildAnimatorState playerAnimatorState in playerAnimatorController.layers[0].stateMachine.states)
        {
            AnimatorState animatorState = new AnimatorState();
            animatorState.name = playerAnimatorState.state.name;
            animatorState.motion = GetAppropriateAnimationClip(animatorState.name, animClipDictionary);

            rootStateMachine.AddState(animatorState, playerAnimatorState.position);

            if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName(playerAnimatorState.state.name))
            {
                rootStateMachine.defaultState = animatorState;
            }
        }
    }

    private static Motion GetAppropriateAnimationClip(string stateName, Dictionary<AnimationType, AnimationClip> animClipDictionary)
    {
        AnimationType animType;
        if (!Enum.IsDefined(typeof(AnimationType), stateName))
        {
            throw new ArgumentException("GetAppropriateAnimationClip was passed a state name from which an AnimationType cannot be parsed: " + stateName);
        }
        else
        {
            animType = (AnimationType)Enum.Parse(typeof(AnimationType), stateName, true);
        }
        return animClipDictionary[animType];
    }

    private static void AddPlayerAnimatorTransitionsToNewController(Animator playerAnimator, AnimatorController newController)
    {
        AnimatorController playerAnimatorController = playerAnimator.runtimeAnimatorController as AnimatorController;
        AnimatorStateMachine rootStateMachine = newController.layers[0].stateMachine;
        foreach (ChildAnimatorState playerAnimatorState in playerAnimatorController.layers[0].stateMachine.states)
        {
            foreach (ChildAnimatorState newAnimatorState in rootStateMachine.states)
            {
                if (playerAnimatorState.state.name == newAnimatorState.state.name)
                {
                    foreach (AnimatorStateTransition playerStateTransition in playerAnimatorState.state.transitions)
                    {
                        AnimatorStateTransition newStateTransition = new AnimatorStateTransition
                        {
                            name = playerStateTransition.name,
                            hasExitTime = playerStateTransition.hasExitTime,
                            canTransitionToSelf = playerStateTransition.canTransitionToSelf,
                            conditions = playerStateTransition.conditions,
                            duration = playerStateTransition.duration
                        };

                        string nameOfDestinationState = playerStateTransition.destinationState.name;
                        foreach (ChildAnimatorState animatorState in rootStateMachine.states)
                        {
                            if (animatorState.state.name == nameOfDestinationState)
                            {
                                newStateTransition.destinationState = animatorState.state;
                                break;
                            }
                        }
                        newAnimatorState.state.AddTransition(newStateTransition);
                    }
                    break;
                }
            }
        }

        foreach (AnimatorStateTransition playerStateTransition in playerAnimatorController.layers[0].stateMachine.anyStateTransitions)
        {
            string nameOfDestinationState = playerStateTransition.destinationState.name;
            AnimatorState destinationState = new AnimatorState();
            foreach (ChildAnimatorState animatorState in rootStateMachine.states)
            {
                if (animatorState.state.name == nameOfDestinationState)
                {
                    destinationState = animatorState.state;
                    break;
                }
            }
            AnimatorStateTransition newStateTransition = rootStateMachine.AddAnyStateTransition(destinationState);
            newStateTransition.name = playerStateTransition.name;
            newStateTransition.hasExitTime = playerStateTransition.hasExitTime;
            newStateTransition.canTransitionToSelf = playerStateTransition.canTransitionToSelf;
            newStateTransition.conditions = playerStateTransition.conditions;
            newStateTransition.duration = playerStateTransition.duration;
        }
    }
}
