using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public float speed;

    Star_Obj_Pool star_Parent;

    [SerializeField] GameObject meteor;
    
    [SerializeField] Rigidbody2D rdb;
    public int scoreCount;

    public float time;

    private void Awake()
    {
        star_Parent = GetComponentInParent<Star_Obj_Pool>();
    }
    private void OnEnable()
    {
        int i = Random.Range(60, 80);
        speed = i;
        time = 0;
    }

    private void Start()
    {
        rdb = gameObject.GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            scoreCount++;//별똥별에 닿았을때 점수카운트가 올라가게
            gameObject.SetActive(false);
            //Destroy(gameObject);
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
        //Destroy(gameObject, 5f); //5초있다 자동으로 오브젝트 파괴

        time += Time.deltaTime;

        if(time >= 3)
        {

            gameObject.SetActive(false);
        }

        if (transform.position.y <= -35f)
        {

            gameObject.SetActive(false);
        }
    }

    //에어스텝 랜덤 떨구기..

}
