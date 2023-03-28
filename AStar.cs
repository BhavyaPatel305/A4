using System.Collections.Generic;
using EasyAI.Navigation.Nodes;
using UnityEngine;
using System;
using System.Linq;

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

            AStarNode startNode = new AStarNode(current, goal);
            AStarNode endNode = new AStarNode(goal, goal);

            List<AStarNode> nodeList = new List<AStarNode> { startNode };

            while (nodeList.Any(node => node.IsOpen))
            {
                AStarNode currentNode = nodeList.Where(node => node.IsOpen).OrderBy(node => node.CostF).First();

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

                currentNode.IsOpen = false;

                if (connections == null)
                {
                    throw new ArgumentNullException(nameof(connections));
                }

                foreach (Connection connection in connections)
                {
                    if (connection.A == currentNode.Position || connection.B == currentNode.Position)
                    {
                        Vector3 neighborPosition = connection.A == currentNode.Position ? connection.B : connection.A;
                        AStarNode neighborNode = nodeList.FirstOrDefault(node => node.Position == neighborPosition);

                        if (neighborNode == null)
                        {
                            neighborNode = new AStarNode(neighborPosition, goal, currentNode);
                            nodeList.Add(neighborNode);
                        }
                        else if (!neighborNode.IsOpen)
                        {
                            continue;
                        }

                        float gScore = currentNode.CostG + Vector3.Distance(currentNode.Position, neighborNode.Position);

                        if (gScore < neighborNode.CostG)
                        {
                            neighborNode.UpdatePrevious(currentNode);
                        }
                    }
                }
            }

            return null;
        }

    }
}
