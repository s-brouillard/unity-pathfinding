using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour
{

  public int width = 12;
  public int height = 12;
  

  public int [,] MakeMap()
  {

    // Sets all the values of the array to 0
    int[,] map = new int[width, height];
    for (int y = 0; y < height; y++)
    {
      for (int x = 0; x < width; x++)
      {
        map[x, y] = 0;
      }
    }

    // Manually setting blocked node to 1
    map[4, 5] = 1;
    map[5, 5] = 1;
    map[6, 5] = 1;
    map[7, 5] = 1;
    map[7, 4] = 1;
    map[7, 3] = 1;
    map[7, 2] = 1;

    map[10, 0] = 1;
    map[10, 1] = 1;
    map[10, 2] = 1;
    map[10, 3] = 1;
    map[10, 4] = 1;
    map[10, 5] = 1;
    map[10, 7] = 1;
    map[10, 9] = 1;
    map[10, 10] = 1;
    map[10, 11] = 1;



    return map;
  }
}
