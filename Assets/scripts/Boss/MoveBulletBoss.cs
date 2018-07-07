using UnityEngine;

public class MoveBulletBoss : MonoBehaviour {

    public GameObject Laser;

    public float BulletSpeed = 10f;
    public float Damage = 10f;
    public float LifeTime = 15f;

    private void Start()
    {
        int randomNumber = Random.Range(0,100);
        if(randomNumber <= 30)
        {

        }
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(-1 * BulletSpeed, GetComponent<Rigidbody2D>().velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.tag == "base")|| (collision.tag == "Player"))
        {
            Destroy(gameObject);
        }
    }

}
