using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathFinder : MonoBehaviour
{
  Node startNode;
  Node endNode;
  Graph graph;
  GraphView graphView;

  Queue<Node> frontierNodes;
  List<Node> exploredNodes;
  List<Node> pathNodes;
  public Color startColor = Color.green;
  public Color endColor = Color.red;
  public Color frontierColor = Color.magenta;
  public Color exploredColor = Color.gray;
  public Color pathColor = Color.cyan;

  public bool isComplete = false;
  int iterations = 0;


  public void Init(Graph graph, GraphView graphView, Node start, Node end)
  {
    if (graph == null || graphView == null || start == null || end == null)
    {
      Debug.LogWarning("Pathfinder Init error: Missing component");
      return;
    }
    if (start.nodeType == NodeType.Blocked || end.nodeType == NodeType.Blocked)
    {
      Debug.LogWarning("Pathfinder Init error: start or end node of type blocked");
      return;
    }

    this.graph = graph;
    this.graphView = graphView;
    startNode = start;
    endNode = end;

    ShowColors(graphView, startNode, endNode);

    frontierNodes = new Queue<Node>();
    frontierNodes.Enqueue(startNode);
    exploredNodes = new List<Node>();
    pathNodes = new List<Node>();

    for (int y = 0; y < this.graph.GetHeight(); y++)
    {
      for (int x = 0; x < this.graph.GetWidth(); x++)
      {
        this.graph.nodes[x, y].Reset();
      }
    }

    isComplete = false;
    iterations = 0;
  }


  private void ShowColors()
  {
    ShowColors(graphView, startNode, endNode);
  }
  private void ShowColors(GraphView graphView, Node startNode, Node endNode)
  {
    if (frontierNodes != null)
    {
      graphView.ColorNodes(frontierNodes.ToList(), frontierColor);
    }

    if (exploredNodes != null)
    {
      graphView.ColorNodes(exploredNodes, exploredColor);
    }


    if (pathNodes != null && pathNodes.Count > 0)
    {
      graphView.ColorNodes(pathNodes, pathColor);
    }

    NodeView startNodeView = graphView.nodeViews[startNode.xIndex, startNode.yIndex];

    if (startNodeView != null)
    {
      startNodeView.ChangeNodeColor(startColor);
    }

    NodeView endNodeView = graphView.nodeViews[endNode.xIndex, endNode.yIndex];

    if (endNodeView != null)
    {
      endNodeView.ChangeNodeColor(endColor);
    }
  }

  public IEnumerator SearchRoutine(float timeStep)
  {
    yield return null;

    while (!isComplete)
    {
      if  (frontierNodes.Count > 0)
      {
        Node currentNode = frontierNodes.Dequeue();
        iterations++;

        if (!exploredNodes.Contains(currentNode))
        {
          exploredNodes.Add(currentNode);
        }

        ExpandFrontier(currentNode);

        if (frontierNodes.Contains(endNode))
        {
          pathNodes = GetPathNodes(endNode);
        }
        
        ShowColors();
        if(graphView)
        {
          graphView.ShowNodeArrows(frontierNodes.ToList());
        }

        yield return new WaitForSeconds(timeStep);

      }
      else
      {
        isComplete = true;
      }

    }
  }


  private void ExpandFrontier(Node node)
  {
    if (node != null)
    {
      for (int neighborIndex = 0; neighborIndex < node.neighbors.Count; neighborIndex++)
      {
        if (!exploredNodes.Contains(node.neighbors[neighborIndex]) 
          && !frontierNodes.Contains(node.neighbors[neighborIndex])) 
          {
            node.neighbors[neighborIndex].previous = node;
            frontierNodes.Enqueue(node.neighbors[neighborIndex]);
          }
      }
    }
  }

  private List<Node> GetPathNodes(Node endNode)
  {
    List<Node> path = new List<Node>();
    if (endNode == null)
    {
      return path;
    }
    path.Add(endNode);
    Node currentNode = endNode.previous;
    // The starting node's previous node should return null
    while(currentNode != null)
    {
      path.Insert(0, currentNode);
      currentNode = currentNode.previous;
    }
    return path;
  }

}
