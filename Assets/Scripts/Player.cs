using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 1f;
    int missIndex = 0;
    public GameObject[] missilePrefab;
    public Transform spPostion;

    [SerializeField]
    private float shootInverval = 0.05f;

    private float lastshotTime = 0f;

    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector3 moveTo = new Vector3(horizontalInput, 0, 0);
        transform.position += moveTo * moveSpeed * Time.deltaTime;

        if (horizontalInput < 0)
        {
            animator.Play("Left");
        }
        else if (horizontalInput > 0)
        {
            animator.Play("Right");
        }
        else
        {
            animator.Play("Idle");
        }
        Shoot();
    }

    void Shoot()
    {
        if (Time.time - lastshotTime > shootInverval)
        {
            Instantiate(missilePrefab[missIndex], spPostion.position, Quaternion.identity);
            lastshotTime = Time.time;
        }
    }

    public void MissileUp()
    {
        missIndex++;
        shootInverval = shootInverval - 0.1f;
        if (shootInverval <= 0.1f)
        {
            shootInverval = 0.1f;
        }
        if (missIndex >= missilePrefab.Length)
        {
            missIndex = missilePrefab.Length - 1;
        }
    }
}
