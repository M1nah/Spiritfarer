using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetMove : MonoBehaviour
{
    public float speed;
    public float distance;
    public float teleportDistance;
    Transform player;
    Rigidbody2D rgd;

    Animator anim;

    public float jumpPower;

    public bool isRun = false;

   public LayerMask flatformLayer;
   public LayerMask airstepLayer;

    // Start is called before the first frame update
    void Start()
    {
        rgd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("Stella").transform;
        Physics2D.IgnoreLayerCollision(7, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(transform.position.x - player.position.x)> distance)
        {
            transform.Translate(new Vector2(1, 0) * Time.deltaTime* speed);
            isRun = true;

           RaycastHit2D hit= Physics2D.Raycast(transform.position, transform.right * 1f, 1f, flatformLayer);
           RaycastHit2D hitdia= Physics2D.Raycast(transform.position, new Vector2(-3* DirectionPet(), 3), 2f, airstepLayer);

            if(player.position.y - transform.position.y <= 0)
            {
                hitdia = new RaycastHit2D();
            }

            if (hit|| hitdia)
            {
                rgd.velocity = Vector2.up * jumpPower;
            }
        }
        else if(Mathf.Abs(transform.position.x - player.position.x) <= distance)
        {
            isRun = false;
        }
        anim.SetBool("isRun", isRun);

        //플레이어와 떨어지면 플레이어의 위치로 리스폰
        if (Vector2.Distance(player.position, transform.position) > teleportDistance)
        {
            transform.position = player.position;
        }
    }

    void PetIsRun()
    {

    }

    float DirectionPet()
    {
        if(transform.position.x - player.position.x < 0) 
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            return -1;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            return 1;
        }
    }

}
