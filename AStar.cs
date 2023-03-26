using System.Collections.Generic;
using EasyAI.Navigation.Nodes;
using UnityEngine;

namespace EasyAI.Navigation
{
    /// <summary>
    /// A* pathfinding.
    /// </summary>
    public static class AStar
    {
        /// <summary>
        /// Perform A* pathfinding.
        /// </summary>
        /// <param name="current">The starting position.</param>
        /// <param name="goal">The end goal position.</param>
        /// <param name="connections">All node connections in the scene.</param>
        /// <returns>The path of nodes to take to get from the starting position to the ending position.</returns>
        public static List<Vector3> Perform(Vector3 current, Vector3 goal, List<Connection> connections)
        {
            /**
            List<AStarNode> openList = new List<AStarNode>();
            List<AStarNode> closedList = new List<AStarNode>();
            AStarNode startNode = new AStarNode(current, goal);
            AStarNode endNode = new AStarNode(goal, goal);
            openList.Add(startNode);

            while (openList.Count > 0)
            {
                AStarNode currentNode = openList[0];
                for (int i = 1; i < openList.Count; i++)
                {
                    if (openList[i].CostF < currentNode.CostF || openList[i].CostF == currentNode.CostF && openList[i].CostH < currentNode.CostH)
                    {
                        currentNode = openList[i];
                    }
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode);

                if (currentNode == endNode)
                {
                    List<Vector3> path = new List<Vector3>();
                    AStarNode node = currentNode;
                    while (node != startNode)
                    {
                        path.Add(node.Position);
                        node = node.Previous;
                    }
                    path.Reverse();
                    return path;
                }

                foreach (Connection connection in connections)
                {
                    if (connection.Start == currentNode.Position)
                    {
                        AStarNode neighbour = new AStarNode(connection.End, goal, currentNode);
                        if (closedList.Contains(neighbour))
                        {
                            continue;
                        }

                        float newCostG = currentNode.CostG + Vector3.Distance(currentNode.Position, neighbour.Position);
                        if (newCostG < neighbour.CostG || !openList.Contains(neighbour))
                        {
                            neighbour.CostG = newCostG;
                            neighbour.UpdatePrevious(currentNode);
                            if (!openList.Contains(neighbour))
                            {
                                openList.Add(neighbour);
                            }
                        }
                    }
                }
            }
            **/
            return null;
        }
    }
}
