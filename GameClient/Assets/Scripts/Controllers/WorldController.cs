using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{ 
    public LayerMask _unwalkableMask;
    public Vector2 _numberOfGrids;
    public float _nodeSize;
    Node[,] _grid;

    public Transform _start; 
    public Transform _destination;
    private Vector3  _cacheStart;
    private Vector3  _cacheDest;

    float   _nodeHalfSize;
    int     _gridSizeX;
    int     _gridSizeY;

    List<Node> _path;

    PathFinding _pathFinding;

    public List<Node> Path
    {
        get
        { 
            return _path; 
        }
        set { _path = value; }  
    }

    public Node[,] Grid
    {
        get
        {
            return _grid;
        }
    }

    public int GridSizeX
    {
        get
        {
            return _gridSizeX;
        }
    }

    public int GridSizeY
    {
        get
        {
            return _gridSizeY;
        }
    }

    void Awake()
    {
        CreateGrid();
    }

    private void Start()
    {
        _pathFinding = GetComponent<PathFinding>(); 
    }

    public void CreateGrid()
    {
        _nodeHalfSize = _nodeSize * 0.5f;
        _gridSizeX = Mathf.RoundToInt(_numberOfGrids.x / _nodeSize);
        _gridSizeY = Mathf.RoundToInt(_numberOfGrids.y / _nodeSize);

        _grid = new Node[_gridSizeX, _gridSizeY];

        Vector3 bottomLeft = transform.position;

        for (int y = 0; y < _gridSizeY; y++)
        {
            for (int x = 0; x < _gridSizeX; x++)
            {
                Vector3 nodePosition = bottomLeft + Vector3.right * (x * _nodeSize + _nodeHalfSize) + Vector3.forward * (y * _nodeSize + _nodeHalfSize);
                bool walkable = !(Physics.CheckSphere(nodePosition, _nodeHalfSize, _unwalkableMask));
                _grid[y,x] = new Node(walkable, nodePosition, x, y);
            }
        }

    }


    void Update()
    {
        if (_start.position != _cacheStart || _destination.position != _cacheDest)
        {
            _pathFinding.FindPath( _start.position, _destination.position );

            _cacheStart = _start.position;
            _cacheDest = _destination.position;
        }
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        for (int y = -1; y <= 1; ++y)
        {
            for (int x = -1; x <= 1; ++x)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }

                int checkX = node._gridX + x;
                int checkY = node._gridY + y;

                if (checkX >= 0 && checkX < _gridSizeX && checkY >= 0 && checkY < _gridSizeY)
                {
                    neighbours.Add(_grid[checkY, checkX]);
                }
            }
        }

        return neighbours;
    }

    public Node GetNodeFromPosition(Vector3 position)
    {
        float percentX = (position.x + _numberOfGrids.x / 2) / _numberOfGrids.x;
        float percentY = (position.z + _numberOfGrids.y / 2) / _numberOfGrids.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((_gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((_gridSizeY - 1) * percentY);
        return _grid[y,x];
    }
    
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(_numberOfGrids.x, 1, _numberOfGrids.y));

        if (_grid != null)
        {
            Node playernode = GetNodeFromPosition(_start.position);

            foreach (Node n in _grid)
            {
                Gizmos.color = (n._isWalkable) ? Color.white : Color.red;

                if (playernode == n)
                {
                    Gizmos.color = Color.black;
                }
                else
                {
                    if (_path != null && _path.Contains(n))
                        Gizmos.color = Color.black;
                }

                Gizmos.DrawCube(n._position, Vector3.one * (_nodeSize - 0.1f));
            }
        }
    }
}
