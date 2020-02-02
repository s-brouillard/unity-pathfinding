using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeType
{
  Open = 0,
  Blocked = 1
}

public class Node
{
  // Node set to open by default
  public NodeType nodeType = NodeType.Open;

  // Position of the node in the grid, made to be -1 so that it cannot be used as an array index
  public int xIndex = -1;
  public int yIndex = -1;


  public Vector3Int position;

  public List<Node> neighbors = new List<Node>();

  public Node previous = null;


  public Node(int xIndex, int yIndex, NodeType nodeType)
  {
    this.xIndex = xIndex;
    this.yIndex = yIndex;
    this.nodeType = nodeType;
  }

  public void Reset()
  {
    previous = null;
  }
}
