using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 moveInput;
    Vector2 screenBoundary;
    [SerializeField] float playerHealth = 100f;
    [SerializeField] float hitImunity = 1f;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float rotationSpeed = 700f;
    [SerializeField] float bulletSpeed = 7f;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject gun;

    float targetAngle;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
       screenBoundary = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)); 
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnAttack()
    {
        Rigidbody2D bullet = Instantiate(bulletPrefab, gun.transform.position, gun.transform.rotation).GetComponent<Rigidbody2D>();
        bullet.AddForce(gun.transform.up * bulletSpeed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = moveInput * moveSpeed;
        if (moveInput != Vector2.zero)
        {
            targetAngle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg - 90f;
        }

        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, -screenBoundary.x, screenBoundary.x),
            Mathf.Clamp(transform.position.y, -screenBoundary.y, screenBoundary.y)
        );
    }

    void FixedUpdate()
    {
        float rotation = Mathf.MoveTowardsAngle(rb.rotation, targetAngle, rotationSpeed * Time.fixedDeltaTime);
        rb.MoveRotation(rotation);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerHealth -= 20f;
            Debug.Log(playerHealth);
            Destroy(collision.gameObject);
            if (playerHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
        Invoke("OnCollisionEnter2D", hitImunity);

    }
}
