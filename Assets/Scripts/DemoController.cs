using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoController : MonoBehaviour
{
  public MapData mapData;
  public Graph graph;

  public PathFinder pathFinder;
  public int startX = 4;
  public int startY = 2;
  public int endX = 15;
  public int endY = 6;
  public float timeStep = 0.1f;

  private void Start() 
  {
    if (mapData != null && graph != null)
    {
      int[,] mapInstance = mapData.MakeMap();
      graph.Init(mapInstance);

      GraphView graphView = graph.GetComponent<GraphView>();

      if (graphView != null)
      {
        graphView.Init(graph);
      }

      if (graph.IsWithinBounds(startX, startY) && graph.IsWithinBounds(endX, endY) && pathFinder != null)
      {
        Node startNode = graph.nodes[startX, startY];
        Node endNode = graph.nodes[endX, endY];

        pathFinder.Init(graph, graphView, startNode, endNode);
        StartCoroutine(pathFinder.SearchRoutine(timeStep));
      }

    }
  }
}
