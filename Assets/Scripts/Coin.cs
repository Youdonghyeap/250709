using UnityEngine;

public class Coin : MonoBehaviour
{
    public static Coin Instance;
    Rigidbody2D rb;

    public bool isFromCoinBomb = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Jump();
    }

    void Jump()
    {
        rb.AddForce(new Vector2(Random.Range(-1, 1), Random.Range(3, 6)), ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.Instance.ShowCoinCount();
            Destroy(gameObject);
        }
    }
}
