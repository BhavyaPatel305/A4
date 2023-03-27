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
                        if (green_area_open(i,j))
                        {
                            place_node(i,j,cornerNodeSteps);
                        }
                        
                    }
                }
            }
            
        }

        private void  place_node(int x, int z, int cornerNodeStep)
        {
            // So if empty space is at the diagonal left bottom of the corner #
            if (!NodeArea.IsOpen(x - 1, z + 1))
            {
                NodeArea.AddNode(x+3, z-3);
            }

            // If empty space is at the diagonal right bottom of the corner #
            if (!NodeArea.IsOpen(x - 1, z - 1))
            {
                NodeArea.AddNode(x + 3, z + 3);
            }

            // If empty space is at the diagonal left top of the corner #
            if (!NodeArea.IsOpen(x + 1, z + 1))
            {
                NodeArea.AddNode(x - 3, z - 3);
            }

            // If empty space is at the diagonal right top of the corner #
            if (!NodeArea.IsOpen(x + 1, z - 1))
            {
                NodeArea.AddNode(x - 3, z + 3);
            }
        }

        private bool green_area_open(int x, int z)
        {
            // So if empty space is at the diagonal left bottom of the corner #
            if (!NodeArea.IsOpen(x - 1, z + 1))
            {
                x = x - 1;
                z = z - 3;
                for (int i = x; i <= x + 4; i++)
                {
                    for (int j = z; j<z+4; j++)
                    {
                        if(i == x && j == (z + 4))
                        {
                            continue;
                        }
                        if (!NodeArea.IsOpen(i,j))
                        {
                            return false;
                        }
                    }
                }
            }

            // If empty space is at the diagonal right bottom of the corner #
            if (!NodeArea.IsOpen(x - 1, z - 1))
            {
                x = x - 1;
                z = z - 1;
                for (int i = x; i <= x + 4; i++)
                {
                    for (int j = z; j < z + 4; j++)
                    {
                        if (i == x && j == z)
                        {
                            continue;
                        }
                        if (!NodeArea.IsOpen(i, j))
                        {
                            return false;
                        }
                    }
                }
            }

            // If empty space is at the diagonal left top of the corner #
            if (!NodeArea.IsOpen(x + 1, z + 1))
            {
                x = x - 3;
                z = z - 3;
                for (int i = x; i <= x + 4; i++)
                {
                    for (int j = z; j < z + 4; j++)
                    {
                        if (i == (x+4) && j == (z+4))
                        {
                            continue;
                        }
                        if (!NodeArea.IsOpen(i, j))
                        {
                            return false;
                        }
                    }
                }
            }

            // If empty space is at the diagonal right top of the corner #
            if (!NodeArea.IsOpen(x + 1, z - 1))
            {
                x = x - 3;
                z = z - 1;
                for (int i = x; i <= x + 4; i++)
                {
                    for (int j = z; j < z + 4; j++)
                    {
                        if (i == (x + 4) && j == z)
                        {
                            continue;
                        }
                        if (!NodeArea.IsOpen(i, j))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
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

            // Check if the current coordinate is surrounded by closed spaces on 4 sides.
            int closedCount = 0;

            
            // Find the corner
            // So if empty space is at the diagonal left bottom of the corner #
            if (!NodeArea.IsOpen(x - 1, z + 1))
            {
                // check its top and right, if empty then place node
                // top
                if (NodeArea.IsOpen(x - 1, z))
                {
                    closedCount++;
                }
                // right
                if (NodeArea.IsOpen(x, z + 1))
                {
                    closedCount++;
                }
                // left
                if (NodeArea.IsOpen(x, z - 1))
                {
                    closedCount++;
                }
                // bottom
                if (NodeArea.IsOpen(x + 1, z))
                {
                    closedCount++;
                }
            }
            

            
            // If empty space is at the diagonal right bottom of the corner #
            if (!NodeArea.IsOpen(x - 1, z - 1))
            {
                // check its top and left, if empty then place node
                // top
                if (NodeArea.IsOpen(x - 1, z))
                {
                    closedCount++;
                }
                // right
                if (NodeArea.IsOpen(x, z + 1))
                {
                    closedCount++;
                }
                // left
                if (NodeArea.IsOpen(x, z - 1))
                {
                    closedCount++;
                }
                // bottom
                if (NodeArea.IsOpen(x + 1, z))
                {
                    closedCount++;
                }
            }
            

            
            // If empty space is at the diagonal left top of the corner #
            if (!NodeArea.IsOpen(x + 1, z + 1))
            {
                // top
                if (NodeArea.IsOpen(x - 1, z))
                {
                    closedCount++;
                }
                // right
                if (NodeArea.IsOpen(x, z + 1))
                {
                    closedCount++;
                }
                // left
                if (NodeArea.IsOpen(x, z - 1))
                {
                    closedCount++;
                }
                // bottom
                if (NodeArea.IsOpen(x + 1, z))
                {
                    closedCount++;
                }
            }
            
            
            // If empty space is at the diagonal right top of the corner #
            if (!NodeArea.IsOpen(x + 1, z - 1))
            {
                // top
                if (NodeArea.IsOpen(x - 1, z))
                {
                    closedCount++;
                }
                // right
                if (NodeArea.IsOpen(x, z + 1))
                {
                    closedCount++;
                }
                // left
                if (NodeArea.IsOpen(x, z - 1))
                {
                    closedCount++;
                }
                // bottom
                if (NodeArea.IsOpen(x + 1, z))
                {
                    closedCount++;
                }
            }
            

            return closedCount == 4;
        }
    }
}
