using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
  public void ActivateMe()
    {
        this.gameObject.SetActive(true);

    }
    public void DeActivate()
    {
        this.gameObject.SetActive(false);
    }
}
