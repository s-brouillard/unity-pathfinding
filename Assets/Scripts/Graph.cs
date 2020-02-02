using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
  // Main data for the graph
  public Node[,] nodes;
  public List<Node> walls = new List<Node>();

  // cached data 
  int[,] mapData;
  int width;
  int height;
  public int GetWidth()
  {
    return width;
  }

  public int GetHeight()
  {
    return height;
  }

  public static readonly Vector3Int[] directions =
  {
    new Vector3Int (0, 1, 0),
    new Vector3Int (1, 0, 0),
    new Vector3Int (0, -1, 0),
    new Vector3Int (-1, 0, 0)
  };

  // Method to initialize a graph
  public void Init(int[,] mapData)
  {
    this.mapData = mapData;
    width = mapData.GetLength(0);
    height = mapData.GetLength(1);

    // Initialization of the node matrix
    nodes = new Node[width, height];
    // These loops go through the mapdata and add blocked nodes to the wall list
    for (int y = 0; y < height; y++)
    {
      for (int x = 0; x < width; x++)
      {
        // Casting of int to enum
        NodeType nodeType = (NodeType)mapData[x, y];
        // Calls the node constructor
        Node node = new Node(x, y, nodeType);
        // Adds the created node to the graph
        nodes[x, y] = node;       // Point1
        node.position = new Vector3Int(x, y, 0);  //TODO check if this works or if it should be before Point1
        // Adds wall if the node is blocked
        if (nodeType == NodeType.Blocked)
        {
          walls.Add(node);
        }
      }
    }

    // These loops go through the node matrix to find the neighbors of every open node
    for (int y = 0; y < height; y++)
    {
      for (int x = 0; x < width; x++)
      {
        // If the node is open
        if (nodes[x, y].nodeType != NodeType.Blocked)
        {
          // Find the neighbors and set them as the node's variable
          nodes[x, y].neighbors = GetNeighbors(x, y);
        }
      }
    }

  }

  // Checks to see if node is within the bounds of the graph
  public bool IsWithinBounds(int x, int y)
  {
    return (x >= 0 && x < width && y >= 0 && y < height);
  }

  // Returns all neighbors of a node in a graph
  List<Node> GetNeighbors(int x, int y, Node[,] nodeArray, Vector3Int[] directions)
  {
    // List to add found neighbors
    List<Node> neighborNodes = new List<Node>();
    // Looks in each direction to find neighbors
    foreach (Vector3Int dir in directions)
    {
      int newX = x + dir.x;
      int newY = y + dir.y;
      Node nodeToCheck = null;
      // Checks to see if the node is out of bound
      if (IsWithinBounds(newX, newY))
      { 
        nodeToCheck = nodeArray[newX, newY];
      }
      if (nodeToCheck != null && nodeToCheck.nodeType != NodeType.Blocked)
      {
        neighborNodes.Add(nodeArray[newX, newY]);
      }
    }
    return neighborNodes;
  }

  List<Node> GetNeighbors(int x, int y)
  {
    return GetNeighbors(x, y, nodes, directions);
  }

}
