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

    void IgnoreLayerTrue() //������ ������Ʈ���� �浹�ǰų� Ȥ�� ���õɰ��
    {
        Physics2D.IgnoreLayerCollision(playerLayer, groundLayer, true);
    }
    void IgnoreLayerFalse()
    {
        Physics2D.IgnoreLayerCollision(playerLayer, groundLayer, false);
    }


    //�������鿡�� �������� Ű�� �������� 0.2�ʰ� ���̾� �浹�� ���õ� �� �ٽ� �����
    //��0.2�ʰ� ���� ���ȴٰ� ����
    IEnumerator LayerOpenClose()
    {
        fallGround = true;
        IgnoreLayerTrue();
        yield return new WaitForSeconds(0.2f);
        IgnoreLayerFalse();
        fallGround = false;
    }

    private void Start() //rigid,playerLayer, groundLayer ���� ����
    {
        rigid = GetComponent<Rigidbody2D>();
        playerLayer = LayerMask.NameToLayer("Player");
        groundLayer = LayerMask.NameToLayer("Ground");
    }


    private void FixedUpdate()
    {
        //���⼭ �¿��̵�Ű? ��? 
        moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        rigid.velocity = new Vector2(moveX, rigid.velocity.y);
    }

    //�����鿡 �����鼭 jumpŰ�� �������� ������ �ϰ� SŰ�� ������ �� �ڷ�ƾ�� �����Ͽ� ������
    //�������Ʈ(�÷��̾�)�� �����ϰ� �ִ� ���ȿ��� �浹�� ����(����)�ϰ� �� �ܿ� �浹 ����(����)
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
