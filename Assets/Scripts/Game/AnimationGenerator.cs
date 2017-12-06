using System;
using System.ComponentModel;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AnimationGenerator
{
    public static Dictionary<AnimationType, AnimationClip> CreateAnimationClips(string spriteSheetName, AnimationCategories animationCategories)
    {
        var animClipDictionary = new Dictionary<AnimationType, AnimationClip>();
        Sprite[] sprites = Resources.LoadAll<Sprite>("Spritesheets/" + spriteSheetName);

        foreach (AnimationType animationType in Enum.GetValues(typeof(AnimationType)))
        {
            AnimationClip animClip = new AnimationClip
            {
                    name = spriteSheetName + " " + animationType.ToString()
            };
            if (DetermineIfHasAnimationType(animationType, animationCategories))
            {
                EditorCurveBinding spriteBinding = new EditorCurveBinding
                {
                    type = typeof(SpriteRenderer),
                    path = "",
                    propertyName = "m_Sprite"
                };

                StartIndexAndLength startAndLength = GetSpriteStartIndexAndRange(animationType, animationCategories);
                ObjectReferenceKeyframe[] spriteKeyFrames = new ObjectReferenceKeyframe[startAndLength.Length];
                float timeValue = 0;
                for (int i = 0; i < startAndLength.Length; i++)
                {
                    spriteKeyFrames[i] = new ObjectReferenceKeyframe();
                    spriteKeyFrames[i].time = timeValue;
                    spriteKeyFrames[i].value = sprites[i + startAndLength.StartIndex];
                    timeValue += 1 / 15f;
                }
                AnimationUtility.SetObjectReferenceCurve(animClip, spriteBinding, spriteKeyFrames);

                AnimationClipSettings animClipSett = new AnimationClipSettings
                {
                    loopTime = true
                };

                AnimationUtility.SetAnimationClipSettings(animClip, animClipSett);
                animClip.frameRate = 15f;
                
            } 
            animClipDictionary.Add(animationType, animClip);
        }
        return animClipDictionary;
    }

    private static bool DetermineIfHasAnimationType(AnimationType animationType, AnimationCategories animationCategories)
    {
        switch (animationCategories)
        {
            case AnimationCategories.All:
                return true;
            case AnimationCategories.WalkAndSlash:
                switch (animationType)
                {
                    case AnimationType.IdleDown:
                    case AnimationType.IdleLeft:
                    case AnimationType.IdleRight:
                    case AnimationType.IdleUp:
                    case AnimationType.WalkDown:
                    case AnimationType.WalkLeft:
                    case AnimationType.WalkRight:
                    case AnimationType.WalkUp:
                    case AnimationType.SlashDown:
                    case AnimationType.SlashLeft:
                    case AnimationType.SlashRight:
                    case AnimationType.SlashUp:
                        return true;
                    default:
                        return false;
                }
            default:
                throw new InvalidEnumArgumentException();
        }
    }

    private static StartIndexAndLength GetSpriteStartIndexAndRange(AnimationType animationType, AnimationCategories animationCategories)
    {
        switch (animationCategories)
        {
            case AnimationCategories.All:
                switch (animationType)
                {
                    case AnimationType.IdleUp:
                        return new StartIndexAndLength(60, 1);
                    case AnimationType.IdleLeft:
                        return new StartIndexAndLength(69, 1);
                    case AnimationType.IdleDown:
                        return new StartIndexAndLength(78, 1);
                    case AnimationType.IdleRight:
                        return new StartIndexAndLength(87, 1);

                    case AnimationType.WalkUp:
                        return new StartIndexAndLength(61, 8);
                    case AnimationType.WalkLeft:
                        return new StartIndexAndLength(70, 8);
                    case AnimationType.WalkDown:
                        return new StartIndexAndLength(79, 8);
                    case AnimationType.WalkRight:
                        return new StartIndexAndLength(88, 8);

                    case AnimationType.SlashUp:
                        return new StartIndexAndLength(96, 6);
                    case AnimationType.SlashLeft:
                        return new StartIndexAndLength(102, 6);
                    case AnimationType.SlashDown:
                        return new StartIndexAndLength(108, 6);
                    case AnimationType.SlashRight:
                        return new StartIndexAndLength(114, 6);

                    case AnimationType.ThrustUp:
                        return new StartIndexAndLength(28, 8);
                    case AnimationType.ThrustLeft:
                        return new StartIndexAndLength(36, 8);
                    case AnimationType.ThrustDown:
                        return new StartIndexAndLength(44, 8);
                    case AnimationType.ThrustRight:
                        return new StartIndexAndLength(52, 8);

                    case AnimationType.LooseUp:
                        return new StartIndexAndLength(120, 13);
                    case AnimationType.LooseLeft:
                        return new StartIndexAndLength(133, 13);
                    case AnimationType.LooseDown:
                        return new StartIndexAndLength(146, 13);
                    case AnimationType.LooseRight:
                        return new StartIndexAndLength(159, 13);

                    case AnimationType.CastUp:
                        return new StartIndexAndLength(0, 7);
                    case AnimationType.CastLeft:
                        return new StartIndexAndLength(7, 7);
                    case AnimationType.CastDown:
                        return new StartIndexAndLength(14, 7);
                    case AnimationType.CastRight:
                        return new StartIndexAndLength(21, 7);

                    case AnimationType.Die:
                        return new StartIndexAndLength(172, 6);

                    default:
                        throw new InvalidEnumArgumentException();
                }
            case AnimationCategories.WalkAndSlash:
                switch(animationType)
                {
                    case AnimationType.IdleUp:
                        return new StartIndexAndLength(0, 1);
                    case AnimationType.IdleLeft:
                        return new StartIndexAndLength(9, 1);
                    case AnimationType.IdleDown:
                        return new StartIndexAndLength(18, 1);
                    case AnimationType.IdleRight:
                        return new StartIndexAndLength(27, 1);
                    case AnimationType.WalkUp:
                        return new StartIndexAndLength(1, 8);
                    case AnimationType.WalkLeft:
                        return new StartIndexAndLength(10, 8);
                    case AnimationType.WalkDown:
                        return new StartIndexAndLength(19, 8);
                    case AnimationType.WalkRight:
                        return new StartIndexAndLength(28, 8);
                    case AnimationType.SlashUp:
                        return new StartIndexAndLength(36, 6);
                    case AnimationType.SlashLeft:
                        return new StartIndexAndLength(42, 6);
                    case AnimationType.SlashDown:
                        return new StartIndexAndLength(48, 6);
                    case AnimationType.SlashRight:
                        return new StartIndexAndLength(54, 6);
                    default:
                        throw new ArgumentException("Something must be wrong with AnimationGenerator.DetermineIfHasAnimationType");
                }
            default:
                throw new InvalidEnumArgumentException();
        }
        
    }
}
