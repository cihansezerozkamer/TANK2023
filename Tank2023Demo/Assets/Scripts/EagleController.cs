using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleController : MonoBehaviour
{

    public Transform center;
    private void Start()
    {
        GameManager.Instance.OnPlayerLose += LoseColor;
        GameManager.Instance.OnStart += MoveToCenter;
        GameManager.Instance.OnStart += NormalColor;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.GetComponent<BulletController>() != null)
        {
            GameManager.Instance.GameOver();
        }
    }
    private void LoseColor()
    {
        this.gameObject.GetComponentInChildren<Renderer>().material.color = Color.red;
    }
    private void NormalColor()
    {
        this.gameObject.GetComponentInChildren<Renderer>().material.color = Color.black;
    }
    private void MoveToCenter()
    {
        this.gameObject.transform.position = center.position;
    }
}
