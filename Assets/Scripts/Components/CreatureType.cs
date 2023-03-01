using UnityEngine;

namespace Fantasie
{
    public class CreatureType : MonoBehaviour
    {
        [field: SerializeField] public CreatureTypeEnum GetCreatureType { get; private set; }
    }
}