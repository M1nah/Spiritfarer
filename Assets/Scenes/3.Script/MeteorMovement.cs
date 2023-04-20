using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorMovement : MonoBehaviour
{
    public float speed;
    [SerializeField] GameObject meteor;
    [SerializeField] Rigidbody2D rdb;
    public int scoreCount;

    private void Start()
    {
        rdb = gameObject.GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            scoreCount++;//별똥별에 닿았을때 점수카운트가 올라가게
            Destroy(gameObject);
        }

        //↓플랫폼에 닿았을때 움직임 멈추기... 일단.. 멈춰지긴함...
        if (other.tag == "Ground")
        {
            meteor.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            speed = 0;
        }
    }

    public void Update()
    {
        //↓대각선으로 별똥별 떨어지게하기전에 좌측으로 움직이기...
       // transform.Translate(new Vector2(-1, 0) * Time.deltaTime * speed);
        rdb.velocity = new Vector2(-0.5f, -0.5f) * speed;
        Destroy(gameObject, 5f); //5초있다 자동으로 오브젝트 파괴


    }


    //에어스텝 랜덤 떨구기..

}
