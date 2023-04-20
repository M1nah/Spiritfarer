using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAndFall : MonoBehaviour
{
    Rigidbody2D rigid;

    float moveX;

    [SerializeField] [Range(400f, 800f)] float moveSpeed = 200f;
    [SerializeField] [Range(400f, 800f)] float jumpforce = 350f;

    int playerLayer, groundLayer;

    bool fallGround;

    void IgnoreLayerTrue() //점프시 오브젝트끼리 충돌되거나 혹은 무시될경우
    {
        Physics2D.IgnoreLayerCollision(playerLayer, groundLayer, true);
    }
    void IgnoreLayerFalse()
    {
        Physics2D.IgnoreLayerCollision(playerLayer, groundLayer, false);
    }


    //↓착지면에서 떨어지는 키를 눌렀을때 0.2초간 레이어 충돌이 무시된 후 다시 적용됨
    //↓0.2초간 문이 열렸다가 닫힘
    IEnumerator LayerOpenClose()
    {
        fallGround = true;
        IgnoreLayerTrue();
        yield return new WaitForSeconds(0.2f);
        IgnoreLayerFalse();
        fallGround = false;
    }

    private void Start() //rigid,playerLayer, groundLayer 변수 정의
    {
        rigid = GetComponent<Rigidbody2D>();
        playerLayer = LayerMask.NameToLayer("Player");
        groundLayer = LayerMask.NameToLayer("Ground");
    }


    private void FixedUpdate()
    {
        //여기서 좌우이동키? 또? 
        moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        rigid.velocity = new Vector2(moveX, rigid.velocity.y);
    }

    //↓지면에 있으면서 jump키를 눌렀을때 점프를 하고 S키를 눌렀을 때 코루틴을 실행하여 떨어짐
    //↓오브젝트(플레이어)가 점프하고 있는 동안에는 충돌을 무시(열림)하고 그 외엔 충돌 적용(닫힘)
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && rigid.velocity.y == 0)
        {
            rigid.AddForce(Vector2.up * jumpforce, ForceMode2D.Force);
        }
        else if (Input.GetKeyDown(KeyCode.S))
            StartCoroutine(LayerOpenClose());

        if (rigid.velocity.y > 0)
        {
            IgnoreLayerTrue();
        }
        else if (rigid.velocity.y <= 0 && !fallGround)
        {
            IgnoreLayerFalse();

        }
    }
}
