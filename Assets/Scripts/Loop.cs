using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop : MonoBehaviour
{
    [SerializeField] public float _secondsPerUpdate;
    private IEnumerable<ICustomUpdatable> _updatables;

    private float _lastUpdateTime;

    void Start()
    {
        _updatables = GetComponentsInChildren<ICustomUpdatable>();
        _lastUpdateTime = Time.time;
    }

    void Update()
    {
        if (Time.time - _lastUpdateTime >= _secondsPerUpdate)
        {
            foreach (var updatable in _updatables)
                updatable.CustomUpdate();

            _lastUpdateTime = Time.time;
        }
    }


}
