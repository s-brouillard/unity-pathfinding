using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeView : MonoBehaviour
{
  public GameObject tile;
  public GameObject arrow;

  Node node;


  [Range(0f, 0.5f)] public float borderSize = 0.15f;

  public void Init(Node node)
  {
    if (tile != null)
    {
      gameObject.name = "Node (" + node.xIndex + ", " + node.yIndex + ")";
      gameObject.transform.position = node.position;
      tile.transform.localScale = new Vector3(1f - borderSize, 1f - borderSize, 0f);
      this.node = node;
      EnableObject(arrow, false);
    }
  }

  private void ChangeNodeColor(Color color, GameObject go)
  {
    if (go != null)
    {
      SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
      if (spriteRenderer != null)
      {
        spriteRenderer.color = color;
      }
    }
  }

  public void ChangeNodeColor(Color color)
  {
    ChangeNodeColor(color, tile);
  }

  private void EnableObject(GameObject go, bool state)
  {
    if (go != null)
    {
      go.SetActive(state);
    }
  }

  public void ShowArrow()
  {
    if (node != null && arrow != null && node.previous != null)
    {
      EnableObject(arrow, true);
      Vector3 dirToPrevious = ((Vector3)node.previous.position - node.position).normalized;
      arrow.transform.rotation = Quaternion.FromToRotation(Vector3.up, dirToPrevious);
    }
  }
}
