using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThePrototype.Scripts.InteractableEntities
{
    [CreateAssetMenu(menuName = "Edulog/HoldableEntities/Holdable")]
    public class HoldableSetting : ScriptableObject
    {
        [field: SerializeField] public String PromptMessageOnGround { get; set; }
        [field: SerializeField] public String PromptMessageOnHand { get; set; }
    }
}