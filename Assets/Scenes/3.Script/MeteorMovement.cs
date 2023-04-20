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
            scoreCount++;//���˺��� ������� ����ī��Ʈ�� �ö󰡰�
            Destroy(gameObject);
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
        Destroy(gameObject, 5f); //5���ִ� �ڵ����� ������Ʈ �ı�


    }


    //����� ���� ������..

}
