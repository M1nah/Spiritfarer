using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingBoatove : MonoBehaviour
{
    [SerializeField] GameObject boat;
    public float boatSpeed;
    Rigidbody2D rigidbody;
    private void Awake()
    {
        boat = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(-1, 0) * Time.deltaTime * boatSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            rigidbody.velocity = Vector3.zero;
            boatSpeed = 0;
        }
    }
}
