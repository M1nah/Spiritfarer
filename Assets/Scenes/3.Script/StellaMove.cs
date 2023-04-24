using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StellaMove : MonoBehaviour
{
    Rigidbody2D rigidbody2D; //물리 이동을 위한 변수 선언
    SpriteRenderer spriteRenderer;//방향전환을 위한 변수

    Animator anim;

    public float maxSpeed; //최대속력변수

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

        //버튼에서 손을 떼는 단발적인 키보드 입력은 FixedUpdate보다 Update에 쓰는게 키보드 입력이 누락될 확률이 낮아진다
        //Jump
        if (Input.GetKeyDown(KeyCode.W) && jump_count < 1)
        {
            jump_count++;
            rigidbody2D.AddForce(Vector2.up * Jump, ForceMode2D.Impulse);
            isJump = true;
            this.audio.Play();
        }
        anim.SetBool("isJumping", isJump);

        //Animation stand-run  애니메이션 스위치
        if (rigidbody2D.velocity.normalized.x == 0) //횡이동 단위값이 0일때(멈췄을때)
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
        //바닥에 닿았음을 감지하는 처리
        //어떠한 클라이더가 닿았으며, 충돌 표면이 위쪽을 보고 있다면? 
        if (collision.contacts[0].normal.y > 0.7f) //playercollider에 붙어있는 함수 
        {
            //땅에 닿았음을 표시하는 bool값
            isGrounded = true;
            isJump = false;
            //땅에 닿았으니 jump_count를 초기화
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
        if (rigidbody2D.velocity.x > maxSpeed) //오른쪽으로 이동(+), 최대속력을 넘으면
        {
            rigidbody2D.velocity = new Vector2(maxSpeed, rigidbody2D.velocity.y); //해당 오브젝트의 속력은 maxSpeed
        }

        //Max speed Left
        else if (rigidbody2D.velocity.x < maxSpeed * (-1))  //왼쪽으로 이동(-)
        {
            rigidbody2D.velocity = new Vector2(maxSpeed * (-1), rigidbody2D.velocity.y);//y값은 점프의 영향이므로 0으로 제한 두면 안됨
        }

        //raycast 오브젝트 검색을 위해 Ray를 쏘는 방식 <(?)
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

