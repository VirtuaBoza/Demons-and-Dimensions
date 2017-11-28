using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Equipment
{
    public Dictionary<AnimationType, AnimationClip> AnimClipDictionary { get; protected set; }

    
}
