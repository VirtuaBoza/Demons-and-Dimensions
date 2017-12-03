using System.Collections.Generic;
using UnityEngine;

public interface IEquipable
{
    Dictionary<AnimationType,AnimationClip> AnimClipDictionary { get; }
    EquipType EquipType { get;  }
}
