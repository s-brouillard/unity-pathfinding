using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MapDirectives", menuName = "MapDirectives")]
public class MapDirectives : ScriptableObject
{
  
  public List<int[]> wallData = new List<int[]>();
}
