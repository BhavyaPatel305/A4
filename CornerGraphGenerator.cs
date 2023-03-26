using UnityEngine;
using System.Collections.Generic;
using EasyAI.Navigation.Nodes;

namespace EasyAI.Navigation.Generators
{
    /// <summary>
    /// Convex corner graph placement for nodes.
    /// </summary>
    public class CornerGraphGenerator : NodeGenerator
    {
        [SerializeField]
        [Min(0)]
        [Tooltip("How far away from corners should the nodes be placed.")]
        private int cornerNodeSteps = 3;

        [SerializeField]
        [Tooltip("The NodeArea to use for node placement.")]
        private NodeArea nodeArea;

        private bool open_corner = false;

        /// <summary>
        /// Place nodes at convex corners.
        /// </summary>
        public override void Generate()
        {
            // TODO - Assignment 4 - Complete corner-graph node generation.

            // loop
            for (int i = 0; i < nodeArea.RangeX; i++)
            {
                for (int j = 0; j < nodeArea.RangeZ; j++)
                {
                    open_corner = IsCorner(i, j);// true if corner
                    if (open_corner)
                    {
                        nodeArea.AddNode(i, j);
                    }
                }
            }
        }

        private bool IsCorner(int x, int z)
        {
            // Check if the current coordinate is open.
            if (!nodeArea.IsOpen(x, z))
            {
                return false;
            }

            // Check if the current coordinate is within bounds.
            if (x - 1 < 0 || x + 1 >= nodeArea.RangeX || z - 1 < 0 || z + 1 >= nodeArea.RangeZ)
            {
                return false;
            }

            // Check if the current coordinate is surrounded by closed spaces on two sides.
            int closedCount = 0;

            // Check the left and right sides.
            if (!nodeArea.IsOpen(x - 1, z))
            {
                closedCount++;
            }
            if (!nodeArea.IsOpen(x + 1, z))
            {
                closedCount++;
            }

            // Check the top and bottom sides.
            if (!nodeArea.IsOpen(x, z - 1))
            {
                closedCount++;
            }
            if (!nodeArea.IsOpen(x, z + 1))
            {
                closedCount++;
            }

            return closedCount == 2;
        }
    }
}
