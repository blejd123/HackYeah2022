using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class Audience : MonoBehaviour
    {
        [Inject] private readonly Podium _podium;

        [SerializeField] private Transform _center;
        [SerializeField] private List<RowConfig> _rowConfigs;
        [SerializeField] private List<Spectator> _spectatorPrefabs;

        private List<Spectator> _spectators;

        public void Initialize()
        {
            _spectators = new List<Spectator>();

            foreach (var rowConfig in _rowConfigs)
            {
                var diff = rowConfig.Origin.position - _center.position;
                diff.y = 0.0f;
                var radius = diff.magnitude;
                var angleDelta = (rowConfig.MaxAngle - rowConfig.MinAngle) / (rowConfig.Count - 1);

                for (int i = 0; i < rowConfig.Count; i++)
                {
                    var spectator = Instantiate(_spectatorPrefabs[Random.Range(0, _spectatorPrefabs.Count)]);
                    spectator.transform.SetParent(_center);
                    var angle = rowConfig.MinAngle + angleDelta * i;
                    angle += Random.Range(-0.2f * angleDelta, 0.2f * angleDelta);
                    var pos = Quaternion.AngleAxis(angle, Vector3.up) * _center.right * radius;
                    pos.y = rowConfig.Origin.position.y;
                    spectator.transform.localPosition = pos;
                    var dir = _center.position - spectator.transform.position;
                    dir.y = 0.0f;
                    spectator.transform.rotation = Quaternion.LookRotation(dir);
                    _spectators.Add(spectator);       
                    spectator.AnimateDefault();
                }
            }
        }

        [Serializable]
        public class RowConfig
        {
            [SerializeField] private Transform _origin;
            [SerializeField] private float _minAngle;
            [SerializeField] private float _maxAngle;
            [SerializeField] private int _count;

            public Transform Origin => _origin;
            public float MinAngle => _minAngle;
            public float MaxAngle => _maxAngle;
            public int Count => _count;
        }
    }
}
