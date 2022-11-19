using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class Podium : MonoBehaviour
    {
        [SerializeField] private Transform _center;
        [SerializeField] private List<Transform> _positions;

        public Transform Center => _center;
        public List<Transform> Positions => _positions;
    }
}
