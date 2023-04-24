using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public float speed;

    Star_Obj_Pool star_Parent;

    [SerializeField] GameObject meteor;
    static AudioSource audioSource;
    public AudioClip audioclip;


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
        audioSource = GetComponent<AudioSource>();
        //audioclip = Resources.Load<AudioClip>("SFX_Event_Meteor_Impact_06.wav");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            GameManager.instance.Addscore(30);//���˺��� ������� ����ī��Ʈ�� �ö󰡰�
            // audioSource.PlayOneShot(audioclip);
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }

        //���÷����� ������� ������ ���߱�... �ϴ�.. ����������...
        if (other.tag == "Ground")
        {
            meteor.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
           speed = 0;
        }
    }

    public void Update()
    {
        //��밢������ ���˺� ���������ϱ����� �������� �����̱�...
       // transform.Translate(new Vector2(-1, 0) * Time.deltaTime * speed);
        rdb.velocity = new Vector2(-0.5f, -0.5f) * speed;
        //Destroy(gameObject, 5f); //5���ִ� �ڵ����� ������Ʈ �ı�

        time += Time.deltaTime;

        if(time >= 10)
        {
            gameObject.SetActive(false);
        }

        if (transform.position.y <= -35f)
        {
            gameObject.SetActive(false);
        }
    }
}
