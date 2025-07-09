using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1f;

    [SerializeField]
    public int missileDamage = 1;

    [SerializeField]
    GameObject Expeffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        if (transform.position.y > 7f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Debug.Log("Missile hit enemy!");
            GameObject effect = Instantiate(Expeffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
            Destroy(gameObject);
        }
    }
}
