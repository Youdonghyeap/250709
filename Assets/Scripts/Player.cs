using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [SerializeField]
    float moveSpeed = 1f;
    int missIndex = 0;
    public GameObject[] missilePrefab;
    public Transform spPostion;

    public GameObject specialMissilePrefab;

    [SerializeField]
    private float shootInverval = 0.05f;

    private float lastshotTime = 0f;
    [SerializeField]
    private float specialMissileCooldown = 5f;
    private float lastSpecialMissileTime = 0f;

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

        if (Input.GetKeyDown(KeyCode.Z))
        {
            ShootSpecialMissile();
        }

        UpdateSpecialMissileCooldown();
    }

    void Shoot()
    {
        if (Time.time - lastshotTime > shootInverval)
        {
            Instantiate(missilePrefab[missIndex], spPostion.position, Quaternion.identity);
            lastshotTime = Time.time;
        }
    }

    void ShootSpecialMissile()
    {
        if (Time.time - lastSpecialMissileTime >= specialMissileCooldown)
        {
            if (specialMissilePrefab != null)
            {
                GameObject specialMissile = Instantiate(specialMissilePrefab, spPostion.position, Quaternion.identity);

                specialMissile.transform.localScale = Vector3.one * 2f;
                lastSpecialMissileTime = Time.time;
            }
        }
    }

    void UpdateSpecialMissileCooldown()
    {
        float cooldownRemaining = Mathf.Max(0f, specialMissileCooldown - (Time.time - lastSpecialMissileTime));

        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdateSpecialMissileCooldown(cooldownRemaining, specialMissileCooldown);
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
