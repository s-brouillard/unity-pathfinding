using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphView : MonoBehaviour
{
  public GameObject nodeViewPrefab;
  public NodeView[,] nodeViews;

  public Color baseColor = Color.white;
  public Color wallColor = Color.black;

  public void Init(Graph graph)
  {
    if (graph == null)
    {
      Debug.LogWarning("GRAPHVIEW: No graph to initialize!");
      return;
    }
    nodeViews = new NodeView[graph.GetWidth(), graph.GetHeight()];

    foreach (Node node in graph.nodes)
    {
      GameObject instance = Instantiate(nodeViewPrefab, Vector3.zero, Quaternion.identity);
      NodeView nodeView = instance.GetComponent<NodeView>();

      if (nodeView != null)
      {
        nodeView.Init(node);
        nodeViews[node.xIndex, node.yIndex] = nodeView;

        if (node.nodeType == NodeType.Blocked)
        {
          nodeView.ChangeNodeColor(wallColor);
        }
        else 
        {
          nodeView.ChangeNodeColor(baseColor);
        }

      }
    }
  }


  public void ColorNodes(List<Node> nodes, Color color)
  {
    foreach (Node node in nodes)
    {
      if (node != null)
      {
        NodeView nodeView = nodeViews[node.xIndex, node.yIndex];

        if (nodeView != null)
        {
          nodeView.ChangeNodeColor(color);
        }
      }
    }
  }

  public void ShowNodeArrows(Node node)
  {
    if (node != null)
    {
      NodeView nodeView = nodeViews[node.xIndex, node.yIndex];
      if (nodeView != null)
      {
        nodeView.ShowArrow();
      }
    }
  }

  public void ShowNodeArrows(List<Node> nodes)
  {
    foreach (Node node in nodes)
    {
      ShowNodeArrows(node);
    }
  }
}
