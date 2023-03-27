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

            // Create the start and end nodes
            AStarNode startNode = new AStarNode(current, goal);
            AStarNode endNode = new AStarNode(goal, goal);

            // Create the open and closed sets
            List<AStarNode> openSet = new List<AStarNode> { startNode };
            HashSet<AStarNode> closedSet = new HashSet<AStarNode>();

            while (openSet.Count > 0)
            {
                // Get the node with the lowest f cost
                AStarNode currentNode = openSet[0];
                for (int i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].CostF < currentNode.CostF)
                    {
                        currentNode = openSet[i];
                    }
                }

                // Check if we've reached the goal
                if (currentNode == endNode)
                {
                    // Reconstruct the path and return it
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

                // Move the current node from the open set to the closed set
                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                // Check each neighbor of the current node
                foreach (Connection connection in connections)
                {
                    // Check if the connection contains the current node
                    if (connection.A == currentNode.Position || connection.B == currentNode.Position)
                    {
                        // Get the neighbor node
                        Vector3 neighborPosition = connection.A == currentNode.Position ? connection.B : connection.A;
                        AStarNode neighborNode = new AStarNode(neighborPosition, goal, currentNode);

                        // Check if the neighbor node is already in the closed set
                        if (closedSet.Contains(neighborNode))
                        {
                            continue;
                        }

                        // Calculate the neighbor node's g score
                        float gScore = currentNode.CostG + Vector3.Distance(currentNode.Position, neighborNode.Position);

                        // Check if the neighbor node is already in the open set
                        if (!openSet.Contains(neighborNode))
                        {
                            // Add the neighbor node to the open set
                            openSet.Add(neighborNode);
                        }
                        else if (gScore >= neighborNode.CostG)
                        {
                            continue;
                        }

                        // Update the neighbor node's previous node and g score
                        neighborNode.UpdatePrevious(currentNode);
                    }
                }
            }

            // No path was found
            return null;
        }

    }
}
