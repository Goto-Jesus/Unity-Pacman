using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pacman : MonoBehaviour, ICustomUpdatable
{
    [SerializeField] private Tilemap _level;
    [SerializeField] private Loop _loop;
    public Vector3Int _playerPosition;
    private Vector3Int _direction = Vector3Int.right;
    public Transform toTarget;
    
    void Start()
    {
        _playerPosition = _level.WorldToCell(transform.position);
    }

    void Update()
    {
        InputChech();
        toTarget.position = _playerPosition + new Vector3(.5f, .5f, 0);
        Move();
    }

    void InputChech()
    {
        if (Input.GetKeyDown(KeyCode.W))
            _direction = Vector3Int.up;
        else if (Input.GetKeyDown(KeyCode.S))
            _direction = Vector3Int.down;
        else if (Input.GetKeyDown(KeyCode.D))
            _direction = Vector3Int.right;
        else if (Input.GetKeyDown(KeyCode.A))
            _direction = Vector3Int.left;
    }

    public void Move()
    {
        float _distance = Vector3.Distance(_playerPosition, transform.position);
        if (Vector3.Distance(_playerPosition, transform.position) <= 0.1f)
            transform.position += (_playerPosition - transform.position) * Time.deltaTime * ((_distance / _loop._secondsPerUpdate));
        else
        {
            transform.position += (_playerPosition - transform.position) * Time.deltaTime * ((_distance / _loop._secondsPerUpdate) + _loop._secondsPerUpdate);
        }

    }

    public void CustomUpdate()
    {
        var celledPosition = _level.WorldToCell(_playerPosition); // 
        var nextPosition = celledPosition + _direction;

        if (_level.GetTile(nextPosition) == null)   // проверка на стену
            _playerPosition = nextPosition;
        

    }

}
