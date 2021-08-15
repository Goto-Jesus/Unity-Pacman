using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps; 

public class Ghost : MonoBehaviour, ICustomUpdatable
{
    [SerializeField] private Tilemap _level // ����� �� ���� ������ ������ ���� � ���� 
    [SerializeField] private Pacman _pacman; // �����
    
    private Vector3 GhostPosition; // ������� ����

    void Start()
    {
        GhostPosition = _level.WorldToCell(transform.position); // ���������� ������� ���� � �������� ������ �����
    }


    public void CustomUpdate()// ����� ���������� �� ������� ���������� � �������
    {
        GhostPosition = GetNextPositionInPathToTarget(_pacman._playerPosition); // ���� ������ 
    ]
	
	// �������� ��������� ������� �� ���� � ����
    private Vector3Int GetNextPositionInPathToTarget(Vector3Int target) 
    {
        var _startPosition = _level.WorldToCell(GhostPosition); // ��������� ������� � �� �����
        var _openList = new Queue<Vector3Int>(); // ������� 
        var _closedList = new List<Vector3Int>(); // ���� ���������� ���� 

        _openList.Enqueue(target); // ��������� ������� � ����� �������
        _closedList.Add(target); // ���������� ������ �������� � ������

        while (_openList.Count > 0)
        {
            var currentPosition = _openList.Dequeue(); // ��������� � ���������� ������ ������� ������� (������� �������)

            for(int x = -1; x<=1;x++)
            {
                for(int y = -1; y<= 1;y++)
                {
                    if (x != 0 && y != 0)
                        continue;
                    var researchedPoint = currentPosition + new Vector3Int(x, y, 0);
                    if (_level.GetTile(researchedPoint) != null) // ���� �����
                        continue;   // ����������
                    if (_closedList.Contains(researchedPoint)) // ���� ����� � �������� ������, �� ���� ��� ����
                        continue;   // ����������
                    if (researchedPoint == _startPosition)  // ���� ����� ������������ � ��� ��� ���� �� ����� ����
                        return currentPosition; // �������� ���� ����

                    // ����� ���� ������ �� ����������
                    _openList.Enqueue(researchedPoint); // ��������� ����� � �������� ������
                    _closedList.Add(researchedPoint);   // � �������� ������
                }
            }
        }

        return _startPosition;
    }

}
