using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorControl : MonoBehaviour
{
    public GameObject meteorPool;

    float timer;

    private void Awake()
    {
        meteorPool = GetComponent<GameObject>();
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 1f)
        {
            timer = 0;
            Spawn();
        }
    }

    public void Spawn()
    {
      Debug.Log("별똥별나오니?");
      GameManager.instance.pool.Get(0);
    }

}

