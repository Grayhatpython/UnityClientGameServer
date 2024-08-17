using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public Transform start, destination;

    private Vector3 cacheStart, cacheDest;
    private World world;

    void Awake()
    {
        world = GetComponent<World>();
    }

    void Update()
    {
        if (start.position != cacheStart || destination.position != cacheDest)
        {
            FindPath(start.position, destination.position);

            cacheStart = start.position;
            cacheDest = destination.position;
        }
    }

    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = world.GetNodeFromPosition(startPos);
        Node targetNode = world.GetNodeFromPosition(targetPos);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();

        openSet.Add(startNode);
        while (openSet.Count > 0)
        {
            #region 가장 낮은 값을 가진 노드를 선택한다.
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || (openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost))
                {
                    currentNode = openSet[i];
                }
            }
            #endregion

            #region 가장 낮은 값을 가진 노드가 종착노드면 탐색을 종료한다.
            if (currentNode == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }
            #endregion

            #region 현재 노드를 오픈 셋에서 빼서 클로즈드 셋으로 이동한다.
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);
            #endregion

            #region 이웃노드를 가져와서 값을 계산한 후 오픈 셋에 추가한다.
            foreach (Node n in world.GetNeighbours(currentNode))
            {
                if (!n.isWalkable || closedSet.Contains(n))
                {
                    continue;
                }

                int g = currentNode.gCost + GetDistance(currentNode, n);
                int h = GetDistance(n, targetNode);
                int f = g + h;

                // 오픈 셋에 이미 중복 노드가 있는 경우 값이 작은 쪽으로 변경한다.
                if (!openSet.Contains(n))
                {
                    n.gCost = g;
                    n.hCost = h;
                    n.parent = currentNode;
                    openSet.Add(n);
                }
                else
                {
                    if (n.fCost > f)
                    {
                        n.gCost = g;
                        n.parent = currentNode;
                    }
                }
            }
            #endregion
        }
    }

    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        world.path = path;
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
        {
            return 14 * dstY + 10 * (dstX - dstY);
        }

        return 14 * dstX + 10 * (dstY - dstX);
    }
}
