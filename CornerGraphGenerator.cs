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

      

        private bool open_corner = false;

        /// <summary>
        /// Place nodes at convex corners.
        /// </summary>
        public override void Generate()
        {
            // TODO - Assignment 4 - Complete corner-graph node generation.
            
            // loop
            for (int i = 0; i < NodeArea.RangeX; i++)
            {
                for (int j = 0; j < NodeArea.RangeZ; j++)
                {
                    open_corner = IsCorner(i, j);// true if corner
                    if (open_corner)
                    {
                        NodeArea.AddNode(i, j);
                    }
                }
            }
            
        }

        private bool IsCorner(int x, int z)
        {
            // Check if the current coordinate is open.
            if (!NodeArea.IsOpen(x, z))
            {
                return false;
            }

            // Check if the current coordinate is within bounds.
            if (x - 1 < 0 || x + 1 >= NodeArea.RangeX || z - 1 < 0 || z + 1 >= NodeArea.RangeZ)
            {
                return false;
            }

            // Check if the current coordinate is surrounded by closed spaces on two sides.
            int closedCount = 0;

            // Check the left and right sides.
            if (!NodeArea.IsOpen(x - 1, z))
            {
                closedCount++;
            }
            if (!NodeArea.IsOpen(x + 1, z))
            {
                closedCount++;
            }

            // Check the top and bottom sides.
            if (!NodeArea.IsOpen(x, z - 1))
            {
                closedCount++;
            }
            if (!NodeArea.IsOpen(x, z + 1))
            {
                closedCount++;
            }

            return closedCount == 2;
        }
    }
}
