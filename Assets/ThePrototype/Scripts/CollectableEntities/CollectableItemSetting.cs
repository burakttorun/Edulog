using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThePrototype.Scripts.CollectableEntities
{
    [CreateAssetMenu(menuName = "Edulog/CollectableEntity/Collectable")]
    public class CollectableSetting : ScriptableObject
    {
        [field: SerializeField] public int Id { get; set; }
        [field: SerializeField] public string ItemName { get; set; }
        [field: SerializeField] public int Value { get; set; }
        [field: SerializeField] public Sprite Icon { get; set; }
        [field: SerializeField] public GameObject Prefab { get; set; }
    }
}
