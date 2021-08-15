using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps; 

public class Ghost : MonoBehaviour, ICustomUpdatable
{
    [SerializeField] private Tilemap _level // карта по сути массив своего рода и граф 
    [SerializeField] private Pacman _pacman; // игрок
    
    private Vector3 GhostPosition; // позиция бота

    void Start()
    {
        GhostPosition = _level.WorldToCell(transform.position); // выставляем позицию бота к ближащей клетке карты
    }


    public void CustomUpdate()// метод отвечающий за частоту обновлений в секунду
    {
        GhostPosition = GetNextPositionInPathToTarget(_pacman._playerPosition); // ищем игрока 
    ]
	
	// Получить следующую позицию на пути к цели
    private Vector3Int GetNextPositionInPathToTarget(Vector3Int target) 
    {
        var _startPosition = _level.WorldToCell(GhostPosition); // стартовая позиция в на карте
        var _openList = new Queue<Vector3Int>(); // очередь 
        var _closedList = new List<Vector3Int>(); // лист пройденого пути 

        _openList.Enqueue(target); // добавляет элемент в конец очереди
        _closedList.Add(target); // добавление нового элемента в список

        while (_openList.Count > 0)
        {
            var currentPosition = _openList.Dequeue(); // извлекает и возвращает первый элемент очереди (текущая позиция)

            for(int x = -1; x<=1;x++)
            {
                for(int y = -1; y<= 1;y++)
                {
                    if (x != 0 && y != 0)
                        continue;
                    var researchedPoint = currentPosition + new Vector3Int(x, y, 0);
                    if (_level.GetTile(researchedPoint) != null) // если стена
                        continue;   // пропускаем
                    if (_closedList.Contains(researchedPoint)) // если точка в закрытом списке, то есть уже была
                        continue;   // пропускаем
                    if (researchedPoint == _startPosition)  // если точка пересикается с тем кто идет по этому пути
                        return currentPosition; // передаем этот путь

                    // иначе если ничего не происходит
                    _openList.Enqueue(researchedPoint); // добавляем точку в открытый список
                    _closedList.Add(researchedPoint);   // и закрытый список
                }
            }
        }

        return _startPosition;
    }

}
