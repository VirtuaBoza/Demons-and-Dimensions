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

                int[] startAndRange = GetSpriteStartIndexAndRange(animationType, animationCategories);
                ObjectReferenceKeyframe[] spriteKeyFrames = new ObjectReferenceKeyframe[startAndRange[1]];
                float timeValue = 0;
                for (int i = 0; i < startAndRange[1]; i++)
                {
                    spriteKeyFrames[i] = new ObjectReferenceKeyframe();
                    spriteKeyFrames[i].time = timeValue;
                    spriteKeyFrames[i].value = sprites[i + startAndRange[0]];
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

    private static int[] GetSpriteStartIndexAndRange(AnimationType animationType, AnimationCategories animationCategories)
    {
        switch (animationCategories)
        {
            case AnimationCategories.All:
                switch (animationType)
                {
                    case AnimationType.IdleUp:
                        return new int[] { 60, 1 };
                    case AnimationType.IdleLeft:
                        return new int[] { 69, 1 };
                    case AnimationType.IdleDown:
                        return new int[] { 78, 1 };
                    case AnimationType.IdleRight:
                        return new int[] { 87, 1 };

                    case AnimationType.WalkUp:
                        return new int[] { 61, 8 };
                    case AnimationType.WalkLeft:
                        return new int[] { 70, 8 };
                    case AnimationType.WalkDown:
                        return new int[] { 79, 8 };
                    case AnimationType.WalkRight:
                        return new int[] { 88, 8 };

                    case AnimationType.SlashUp:
                        return new int[] { 96, 6 };
                    case AnimationType.SlashLeft:
                        return new int[] { 102, 6 };
                    case AnimationType.SlashDown:
                        return new int[] { 108, 6 };
                    case AnimationType.SlashRight:
                        return new int[] { 114, 6 };

                    case AnimationType.ThrustUp:
                        return new int[] { 28, 8 };
                    case AnimationType.ThrustLeft:
                        return new int[] { 36, 8 };
                    case AnimationType.ThrustDown:
                        return new int[] { 44, 8 };
                    case AnimationType.ThrustRight:
                        return new int[] { 52, 8 };

                    case AnimationType.LooseUp:
                        return new int[] { 120, 13 };
                    case AnimationType.LooseLeft:
                        return new int[] { 133, 13 };
                    case AnimationType.LooseDown:
                        return new int[] { 146, 13 };
                    case AnimationType.LooseRight:
                        return new int[] { 159, 13 };

                    case AnimationType.CastUp:
                        return new int[] { 0, 7 };
                    case AnimationType.CastLeft:
                        return new int[] { 7, 7 };
                    case AnimationType.CastDown:
                        return new int[] { 14, 7 };
                    case AnimationType.CastRight:
                        return new int[] { 21, 7 };

                    case AnimationType.Die:
                        return new int[] { 172, 6 };

                    default:
                        throw new InvalidEnumArgumentException();
                }
            case AnimationCategories.WalkAndSlash:
                switch(animationType)
                {
                    case AnimationType.IdleUp:
                        return new int[] { 0, 1 };
                    case AnimationType.IdleLeft:
                        return new int[] { 9, 1 };
                    case AnimationType.IdleDown:
                        return new int[] { 18, 1 };
                    case AnimationType.IdleRight:
                        return new int[] { 27, 1 };
                    case AnimationType.WalkUp:
                        return new int[] { 1, 8 };
                    case AnimationType.WalkLeft:
                        return new int[] { 10, 8 };
                    case AnimationType.WalkDown:
                        return new int[] { 19, 8 };
                    case AnimationType.WalkRight:
                        return new int[] { 28, 8 };
                    case AnimationType.SlashUp:
                        return new int[] { 36, 6 };
                    case AnimationType.SlashLeft:
                        return new int[] { 42, 6 };
                    case AnimationType.SlashDown:
                        return new int[] { 48, 6 };
                    case AnimationType.SlashRight:
                        return new int[] { 54, 6 };
                    default:
                        throw new ArgumentException("Something must be wrong with AnimationGenerator.DetermineIfHasAnimationType");
                }
            default:
                throw new InvalidEnumArgumentException();
        }
        
    }
}
