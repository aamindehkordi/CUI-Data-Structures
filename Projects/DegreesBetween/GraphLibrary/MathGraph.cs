using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLibrary
{
    /// <summary>
    /// Represents a vertex in a graph.
    /// </summary>
    /// <typeparam name="T">The type of the component stored in the vertex.</typeparam>
    public class Vertex<T>
    {
        // The list of edges connected to this vertex.
        public List<T> EdgeList;

        // The component stored in this vertex.
        public T Component;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vertex{T}"/> class.
        /// </summary>
        /// <param name="component">The component to store in the vertex.</param>
        public Vertex(T component = default)
        {
            EdgeList = new List<T>();
            Component = component;
        }
    }

    /// <summary>
    /// Represents a mathematical graph.
    /// </summary>
    /// <typeparam name="T">The type of the vertices in the graph.</typeparam>
    public class MathGraph<T> where T : IComparable<T>
    {
        // The name of the graph.
        private string GraphName;

        // A dictionary of vertices in the graph.
        private SortedDictionary<T, Vertex<T>> Vertices;

        // A dictionary of component weights in the graph.
        private SortedDictionary<T, int> ComponentWeights;

        /// <summary>
        /// Initializes a new instance of the <see cref="MathGraph{T}"/> class.
        /// </summary>
        /// <param name="name">The name of the graph.</param>
        public MathGraph(string name)
        {
            GraphName = name;
            Vertices = new SortedDictionary<T, Vertex<T>>();
            ComponentWeights = new SortedDictionary<T, int>();
        }

        /// <summary>
        /// Initializes the MathGraph object with the specified graph name.
        /// </summary>
        /// <param name="graphName">The name of the graph.</param>
        private void Initialize(string graphName = "None")
        {
            // Set the graph name
            GraphName = graphName;

            // Create a new dictionary to store the vertices
            Vertices = new SortedDictionary<T, Vertex<T>>();

            // Create a new dictionary to store the component weights
            ComponentWeights = new SortedDictionary<T, int>();

            // Set the edge count to 0
            edgeCount = 0;
        }
        /// <summary>
        /// Returns the number of vertices in the graph.
        /// </summary>
        /// <returns>The number of vertices in the graph.</returns>
        public int CountVertices()
        {
            // Return the count of vertices in the Vertices dictionary
            return Vertices.Count;
        }
        /// <summary>
        /// Returns the number of edges in the graph.
        /// </summary>
        /// <returns>The number of edges in the graph.</returns>
        public int CountEdges()
        {
            // Return the edge count
            return edgeCount;
        }
        /// <summary>
        /// Returns the number of components in the graph.
        /// </summary>
        /// <returns>The number of components in the graph.</returns>
        public int CountComponents()
        {
            return ComponentWeights.Count;
        }

        /// <summary>
        /// Returns the number of vertices connected to the specified vertex.
        /// </summary>
        /// <param name="vertex">The vertex to count the number of connected vertices for.</param>
        /// <returns>The number of vertices connected to the specified vertex.</returns>
        public int CountConnectedTo(T vertex)
        {
            if (!ContainsVertex(vertex))
            {
                string msg = $"Vertex '{vertex}' is not in the graph";
                throw new ArgumentException(msg);
            }

            T component = GetFinalComponentName(vertex);
            return ComponentWeights[component];
        }

        /// <summary>
        /// Returns the number of vertices adjacent to the specified vertex.
        /// </summary>
        /// <param name="vertex">The vertex to count the number of adjacent vertices for.</param>
        /// <returns>The number of vertices adjacent to the specified vertex.</returns>
        public int CountAdjacent(T vertex)
        {
            if (!ContainsVertex(vertex))
            {
                string msg = $"Vertex '{vertex}' is not in the graph";
                throw new ArgumentException(msg);
            }

            return Vertices[vertex].EdgeList.Count();
        }

        /// <summary>
        /// Adds a vertex to the graph.
        /// </summary>
        /// <param name="vertex">The vertex to add to the graph.</param>
        public void AddVertex(T vertex)
        {
            if (ContainsVertex(vertex))
            {
                string msg = $"Vertex '{vertex}' is already in the graph";
                throw new ArgumentException(msg);
            }

            Vertices.Add(vertex, new Vertex<T>(vertex));
            ComponentWeights.Add(vertex, 1);
            return;
        }
        /// <summary>
        /// Adds an edge between two vertices in the graph.
        /// </summary>
        /// <param name="vertex1">The first vertex.</param>
        /// <param name="vertex2">The second vertex.</param>
        public void AddEdge(T vertex1, T vertex2)
        {
            // Check if both vertices are in the graph
            if (!ContainsVertex(vertex1))
            {
                string msg = $"Vertex '{vertex1}' is not in the graph";
                throw new ArgumentException(msg);
            }

            if (!ContainsVertex(vertex2))
            {
                string msg = $"Vertex '{vertex2}' is not in the graph";
                throw new ArgumentException(msg);
            }

            // Add the edge to both vertices' edge lists
            Vertices[vertex1].EdgeList.Add(vertex2);
            Vertices[vertex2].EdgeList.Add(vertex1);
            edgeCount++;

            // Union Find algorithm to maintain graph components with each new edge
            T v1 = GetFinalComponentName(vertex1);
            T v2 = GetFinalComponentName(vertex2);
            if (!Equal(v1, v2))
            {
                // Merge the two components
                if (ComponentWeights[v1] < ComponentWeights[v2])
                {
                    Vertices[v1].Component = v2;
                    ComponentWeights[v2] += ComponentWeights[v1];
                    ComponentWeights.Remove(v1);
                }
                else
                {
                    Vertices[v2].Component = v1;
                    ComponentWeights[v1] += ComponentWeights[v2];
                    ComponentWeights.Remove(v2);
                }
            }
        }

        /// <summary>
        /// Returns the final component name of a vertex.
        /// </summary>
        /// <param name="vertex">The vertex to get the final component name for.</param>
        /// <returns>The final component name of the vertex.</returns>
        private T GetFinalComponentName(T vertex)
        {
            T component = vertex;
            while (!Equal(component, Vertices[component].Component))
            {
                component = Vertices[component].Component;
            }
            return component;
        }

        /// <summary>
        /// Tests if two vertices are connected.
        /// </summary>
        /// <param name="vertex1">The first vertex to test.</param>
        /// <param name="vertex2">The second vertex to test.</param>
        /// <returns>True if the vertices are connected, false otherwise.</returns>
        public bool TestConnectedTo(T vertex1, T vertex2)
        {
            if (!ContainsVertex(vertex1))
            {
                string msg = $"Vertex '{vertex1}' is not in the graph";
                throw new ArgumentException(msg);
            }

            if (!ContainsVertex(vertex2))
            {
                string msg = $"Vertex '{vertex2}' is not in the graph";
                throw new ArgumentException(msg);
            }

            T component1 = GetFinalComponentName(vertex1);
            T component2 = GetFinalComponentName(vertex2);
            return (Equal(component1, component2));
        }

        /// <summary>
        /// Checks if the graph contains a vertex.
        /// </summary>
        /// <param name="vertex">The vertex to check for.</param>
        /// <returns>True if the graph contains the vertex, false otherwise.</returns>
        public bool ContainsVertex(T vertex)
        {
            return Vertices.ContainsKey(vertex);
        }

        /// <summary>
        /// Determines whether the graph contains an edge between two vertices.
        /// </summary>
        /// <param name="vertex1">The first vertex.</param>
        /// <param name="vertex2">The second vertex.</param>
        /// <returns>True if the graph contains an edge between the two vertices, false otherwise.</returns>
        public bool ContainsEdge(T vertex1, T vertex2)
        {
            // Check if vertex1 is in the graph
            if (!ContainsVertex(vertex1))
            {
                string msg = $"Vertex '{vertex1}' is not in the graph";
                throw new ArgumentException(msg);
            }

            // Check if vertex2 is in the graph
            if (!ContainsVertex(vertex2))
            {
                string msg = $"Vertex '{vertex2}' is not in the graph";
                throw new ArgumentException(msg);
            }

            // Check if there is an edge between vertex1 and vertex2
            return Vertices[vertex1].EdgeList.Contains(vertex2);
        }

        /// <summary>
        /// Finds the first path between two vertices in the graph.
        /// </summary>
        /// <param name="vertex1">The first vertex.</param>
        /// <param name="vertex2">The second vertex.</param>
        /// <returns>A list of vertices representing the first path between the two vertices.</returns>
        public List<T> FindFirstPath(T vertex1, T vertex2)
        {
            // Check if vertex1 is in the graph
            if (!ContainsVertex(vertex1))
            {
                string msg = $"Vertex '{vertex1}' is not in the graph";
                throw new ArgumentException(msg);
            }

            // Check if vertex2 is in the graph
            if (!ContainsVertex(vertex2))
            {
                string msg = $"Vertex '{vertex2}' is not in the graph";
                throw new ArgumentException(msg);
            }

            // Initialize variables
            List<T> firstPath = new List<T>();
            SortedDictionary<T, bool> marked = ClearAllVertexMarks();
            SortedDictionary<T, T> edgeTo = new SortedDictionary<T, T>();

            // Find the first path between vertex1 and vertex2 using depth-first search
            DepthFirstPathTo(vertex1, vertex2, marked, edgeTo);

            // Check if there is a path between vertex1 and vertex2
            if (!marked[vertex1])
            {
                string msg = $"Graph does not contain a path from '{vertex1}' to '{vertex2}'";
                throw new ArgumentException(msg);
            }

            // Construct the first path
            firstPath.Add(vertex1);
            T curr = vertex1;
            while (!Equal(curr, vertex2))
            {
                curr = edgeTo[curr];
                firstPath.Add(curr);
            }

            return firstPath;
        }

        /// <summary>
        /// Recursively finds a path from the source vertex to the destination vertex using depth-first search.
        /// </summary>
        /// <param name="srcVertex">The vertex we are starting from.</param>
        /// <param name="dstVertex">The vertex we are trying to reach.</param>
        /// <param name="marked">A dictionary of vertices that have already been visited.</param>
        /// <param name="edgeTo">A dictionary of edges that lead to each vertex.</param>
        private void DepthFirstPathTo(T srcVertex,
                                    T dstVertex,
                                    SortedDictionary<T, bool> marked,
                                    SortedDictionary<T, T> edgeTo)
        {
            marked[dstVertex] = true;

            // Enumerate through all of the vertices that are adjacent to this one
            // If we have already visited the adjacent vertex, ignore it
            // Otherwise, we record it's position and then recurse deeper to it

            foreach (T adj in Vertices[dstVertex].EdgeList)
            {
                if (marked[adj])
                {
                    continue;
                }

                edgeTo[adj] = dstVertex;
                DepthFirstPathTo(srcVertex, adj, marked, edgeTo);
            }
        }

        /// <summary>
        /// Finds the shortest path between two vertices using breadth-first search.
        /// </summary>
        /// <param name="vertex1">The starting vertex.</param>
        /// <param name="vertex2">The ending vertex.</param>
        /// <returns>A list of vertices representing the shortest path between the two vertices.</returns>
        public List<T> FindShortestPath(T vertex1, T vertex2)
        {
            // Check if the vertices are in the graph
            if (!ContainsVertex(vertex1))
            {
                string msg = $"Vertex '{vertex1}' is not in the graph";
                throw new ArgumentException(msg);
            }

            if (!ContainsVertex(vertex2))
            {
                string msg = $"Vertex '{vertex2}' is not in the graph";
                throw new ArgumentException(msg);
            }

            // Initialize data structures
            List<T> shortestPath = new List<T>();
            SortedDictionary<T, bool> marked = ClearAllVertexMarks();
            SortedDictionary<T, T> edgeTo = new SortedDictionary<T, T>();

            // Find the shortest path using breadth-first search
            BreadthFirstPathTo(vertex1, vertex2, marked, edgeTo);

            // Check if a path was found
            if (!marked[vertex1])
            {
                string msg = $"Graph does not contain a path from '{vertex1}' to '{vertex2}'";
                throw new ArgumentException(msg);
            }

            // Reconstruct the shortest path
            T curr = vertex1;
            shortestPath.Add(vertex1);
            while (!Equal(curr, vertex2))
            {
                curr = edgeTo[curr];
                shortestPath.Add(curr);
            }

            return shortestPath;
        }

        /// <summary>
        /// Finds the shortest path from the source vertex to the destination vertex using breadth-first search.
        /// </summary>
        /// <param name="srcVertex">The source vertex.</param>
        /// <param name="dstVertex">The destination vertex.</param>
        /// <param name="marked">A dictionary that keeps track of which vertices have been marked.</param>
        /// <param name="edgeTo">A dictionary that keeps track of the edges that lead to each vertex.</param>
        private void BreadthFirstPathTo(T srcVertex,
                                        T dstVertex,
                                        SortedDictionary<T, bool> marked,
                                        SortedDictionary<T, T> edgeTo)
        {
            Queue<T> searchList = new Queue<T>();
            searchList.Enqueue(dstVertex);
            marked[dstVertex] = true;
            int count = 0;

            while (searchList.Count > 0)
            {
                T v = searchList.Dequeue();
                foreach (T adj in Vertices[v].EdgeList)
                {
                    if (marked[adj])
                    {
                        continue;
                    }

                    marked[adj] = true;
                    searchList.Enqueue(adj);
                    edgeTo[adj] = v;

                    if (Equal(srcVertex, adj))
                    {
                        Console.WriteLine($"Search completed in {count} steps");
                        return;
                    }
                    count++;
                }
                count++;
            }
        }

        /// <summary>
        /// Clears all vertex marks.
        /// </summary>
        /// <returns>A dictionary that maps each vertex to a boolean value indicating whether it has been marked.</returns>
        private SortedDictionary<T, bool> ClearAllVertexMarks()
        {
            SortedDictionary<T, bool> marks = new SortedDictionary<T, bool>();
            foreach (T key in Vertices.Keys)
            {
                marks[key] = false;
            }
            return marks;
        }
        /// <summary>
        /// Determines whether two vertices are equal.
        /// </summary>
        /// <param name="vertex1">The first vertex.</param>
        /// <param name="vertex2">The second vertex.</param>
        /// <returns>True if the vertices are equal, false otherwise.</returns>
        private bool Equal(T vertex1, T vertex2)
        {
            // Compare the vertices and return the result.
            return (vertex1.CompareTo(vertex2) == 0);
        }

        /// <summary>
        /// Enumerates all vertices in the graph.
        /// </summary>
        /// <returns>An enumerable collection of vertices.</returns>
        public IEnumerable<T> EnumVertices()
        {
            // Enumerate all vertices in the graph.
            foreach (T vertex in Vertices.Keys)
            {
                yield return vertex;
            }
        }

        /// <summary>
        /// Enumerates all vertices adjacent to the specified vertex.
        /// </summary>
        /// <param name="vertex">The vertex to enumerate adjacent vertices for.</param>
        /// <returns>An enumerable collection of adjacent vertices.</returns>
        public IEnumerable<T> EnumAdjacent(T vertex)
        {
            // Enumerate all vertices adjacent to the specified vertex.
            foreach (T edge in Vertices[vertex].EdgeList)
            {
                yield return edge;
            }
        }

        /// <summary>
        /// Enumerates all vertices connected to the specified vertex.
        /// </summary>
        /// <param name="vertex">The vertex to enumerate connected vertices for.</param>
        /// <returns>An enumerable collection of connected vertices.</returns>
        public IEnumerable<T> EnumConnectedTo(T vertex)
        {
            // Get the final component name for the specified vertex.
            T component = GetFinalComponentName(vertex);

            // Enumerate all vertices in the graph.
            foreach (T potentialVertex in Vertices.Keys)
            {
                // Get the final component name for the potential vertex.
                T potentialComponent = GetFinalComponentName(potentialVertex);

                // If the potential vertex is in the same component as the specified vertex, yield it.
                if (Equal(component, potentialComponent))
                {
                    yield return potentialVertex;
                }
            }
        }
        /// <summary>
        /// Dumps the graph to the console.
        /// </summary>
        public void DumpGraph()
        {
            // Print the graph name or "Unnamed Graph" if no name is set
            if (GraphName != "None")
            {
                Console.WriteLine($"Graph: {GraphName}");
            }
            else
            {
                Console.WriteLine($"Unnamed Graph:");
            }

            // Print each vertex and its adjacent vertices
            foreach (T vertex in Vertices.Keys)
            {
                Console.Write($"{vertex}: ");
                foreach (T adj in Vertices[vertex].EdgeList)
                {
                    Console.Write($"{{{adj}}} ");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Returns a string representation of the graph.
        /// </summary>
        /// <returns>A string representation of the graph.</returns>
        public override string ToString()
        {
            return $"Graph {GraphName}: {Vertices.Count} vertices and {edgeCount} edges";
        }
    }
}