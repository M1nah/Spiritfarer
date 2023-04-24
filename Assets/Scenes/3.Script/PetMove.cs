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

    public float jumpPower;

   public LayerMask flatformLayer;
   public LayerMask airstepLayer;
    // Start is called before the first frame update
    void Start()
    {
        rgd = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Stella").transform;
        Physics2D.IgnoreLayerCollision(7, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(transform.position.x - player.position.x)> distance)
        {
        transform.Translate(new Vector2(1, 0) * Time.deltaTime* speed);
            
           RaycastHit2D hit= Physics2D.Raycast(transform.position, transform.right * 1f, 1f, flatformLayer);

           RaycastHit2D hitdia= Physics2D.Raycast(transform.position, new Vector2(-2* DirectionPet(), 2), 2f, airstepLayer);

            if(player.position.y - transform.position.y <= 0)
            {
                hitdia = new RaycastHit2D();
            }

            if (hit|| hitdia)
            {
                rgd.velocity = Vector2.up * jumpPower;
            }
        }

        //�÷��̾�� �������� �÷��̾��� ��ġ�� ������
        if(Vector2.Distance(player.position, transform.position) > teleportDistance)
        {
            transform.position = player.position;
        }
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
