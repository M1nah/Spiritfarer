using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StellaMove : MonoBehaviour
{
    Rigidbody2D rigidbody2D; //���� �̵��� ���� ���� ����
    SpriteRenderer spriteRenderer;//������ȯ�� ���� ����

    Animator anim;

    public float maxSpeed; //�ִ�ӷº���

    public float Jump;
    private int jump_count = 0;
    private bool isGrounded = false;
    private bool isJump = false;
    private bool isLadder = false;

    private AudioSource audio;
    public AudioClip jumpSound;

    static AudioSource audioSource;
    public AudioClip audioclip;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>(); 
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        this.audio = this.gameObject.AddComponent<AudioSource>();
        this.audio.clip = this.jumpSound;
        this.audio.loop = false;
    }

    void Update()
    {
        //stop speed
        if (Input.GetButtonUp("Horizontal")) 
        {
            rigidbody2D.velocity = new Vector2(0.5f * rigidbody2D.velocity.normalized.x, rigidbody2D.velocity.y);
        }

        //DirectionSprite
        if (Input.GetButtonDown("Horizontal")) spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1; 

        //��ư���� ���� ���� �ܹ����� Ű���� �Է��� FixedUpdate���� Update�� ���°� Ű���� �Է��� ������ Ȯ���� ��������
        //Jump
        if (Input.GetKeyDown(KeyCode.W) && jump_count < 1)
        {
            jump_count++;
            rigidbody2D.AddForce(Vector2.up * Jump, ForceMode2D.Impulse);
            isJump = true;
            this.audio.Play();
        }
        anim.SetBool("isJumping", isJump);

        //Animation stand-run  �ִϸ��̼� ����ġ
        if (rigidbody2D.velocity.normalized.x == 0) //Ⱦ�̵� �������� 0�϶�(��������)
        {
            anim.SetBool("isRun", false);
        }
        else
        {
            anim.SetBool("isRun", true);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //�ٴڿ� ������� �����ϴ� ó��
        //��� Ŭ���̴��� �������, �浹 ǥ���� ������ ���� �ִٸ�? 
        if (collision.contacts[0].normal.y > 0.7f) //playercollider�� �پ��ִ� �Լ� 
        {
            //���� ������� ǥ���ϴ� bool��
            isGrounded = true;
            isJump = false;
            //���� ������� jump_count�� �ʱ�ȭ
            jump_count = 0;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
        }
        if (collision.tag == "ShootingStar")
        {
            audioSource.PlayOneShot(audioclip);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
        }
    }

    void FixedUpdate()
    {
        //Move by key
        float h = Input.GetAxisRaw("Horizontal");
        rigidbody2D.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        //Max speed Right
        if (rigidbody2D.velocity.x > maxSpeed) //���������� �̵�(+), �ִ�ӷ��� ������
        {
            rigidbody2D.velocity = new Vector2(maxSpeed, rigidbody2D.velocity.y); //�ش� ������Ʈ�� �ӷ��� maxSpeed
        }

        //Max speed Left
        else if (rigidbody2D.velocity.x < maxSpeed * (-1))  //�������� �̵�(-)
        {
            rigidbody2D.velocity = new Vector2(maxSpeed * (-1), rigidbody2D.velocity.y);//y���� ������ �����̹Ƿ� 0���� ���� �θ� �ȵ�
        }

        //raycast ������Ʈ �˻��� ���� Ray�� ��� ��� <(?)
        //Landing Platform
        if (rigidbody2D.velocity.y < 0)
        {
            Debug.DrawRay(rigidbody2D.position, Vector3.down, new Color(0, 1, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(rigidbody2D.position, Vector3.down, LayerMask.GetMask("Platform"));

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 0.5f)
                {
                    Debug.Log(rayHit.collider.tag);
                    anim.SetBool("isJumping", false);
                }
            }
        }

        if (isLadder)
        {
            float ver = Input.GetAxisRaw("Vertical");
           rigidbody2D.gravityScale = 0;
           rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, ver * maxSpeed);
        }
        else
        {
            rigidbody2D.gravityScale = 8;
        }
    }
}

