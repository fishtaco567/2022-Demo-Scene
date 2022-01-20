using UnityEngine;
using System.Collections.Generic;

namespace MeshGeneration.Marching
{
    public abstract class MarchingBase
    {

        protected float Surface;

        /// <summary>
        /// Defines the 8 verticies of a cube
        /// </summary>
        protected static readonly int[,] CubeVerticies = new int[,]
        {
                {0, 0, 0}, {1, 0, 0}, {1, 1, 0}, {0, 1, 0},
                {0, 0, 1}, {1, 0, 1}, {1, 1, 1}, {0, 1, 1}
        };

        /// <summary>
        /// Base class for 'Marching' algorithms
        /// </summary>
        /// <param name="surface">The 'density' value which defines the surface</param>
        public MarchingBase(float surface)
        {
            this.Surface = surface;
        }

        //Linearly interpolates between y1 and y2 to find the intersection with the surface
        //Arbitrarily choose 0.5 when they are the same, eliminating issues with division by 0
        protected float GetIntersection(float y1, float y2)
        {
            return y1 == y2 ? 0.5f : (y1 - Surface) / (y1 - y2);
        }

        /// <summary>
        /// Generates a mesh. 
        /// </summary>
        /// <param name="density">Array of density values, with the surface defined in the constructor. Assumed array indexing is (x + y * width + z * width * height).
        /// This is going along the x axis, down the y axis, then in the z axis</param>
        /// <param name="width">The x axis of 'density'</param>
        /// <param name="height">The y axis of 'density'</param>
        /// <param name="depth">The z axis of 'density'</param>
        /// <param name="verts">List to store generated verticies in</param>
        /// <param name="indicies">List to store generated indicies in</param>
        public abstract void GenerateMesh(float[] density, int width, int height, int depth, List<Vector3> verts, List<int> indicies);

    }
}
