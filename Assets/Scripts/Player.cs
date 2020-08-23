using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] public Bullet bullet;
    [SerializeField] public float movementSpeed = 10f;
    [SerializeField] public float accelerometerMultiplier = 2.2f;

    private Rigidbody2D _rb;
    public float MaxHeight { get; private set; }

    private bool _active;

    public bool Shouted { get; private set; }


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        gameObject.SetActive(false);
    }

    void Update()
    {
        if (_active)
        {
            MovementProcess();
            FlipProcess();
            ShoutProcess();
        }
    }

    private void MovementProcess()
    {
        Vector2 velocity = _rb.velocity;
        velocity.x = Acceleration;
        _rb.velocity = velocity;

        if (transform.position.y > MaxHeight)
            MaxHeight = transform.position.y;
    }

    private float Acceleration => AccelerationCounting();

    private float AccelerationCounting()
    {
        float movement = 0f;

        float keyBoardData = Input.GetAxis("Horizontal");
        float accelerometerData = Input.acceleration.x;

        #if UNITY_EDITOR || UNITY_WII
            movement = keyBoardData * movementSpeed;
        #else
            movement = accelerometerData * movementSpeed * accelerometerMultiplier;
        #endif
        return movement;
    }

    private void FlipProcess()
    {
        var main = Camera.main;
        var screenHalfSize = main.orthographicSize * main.aspect;
        var position = transform.position;

        if (transform.position.x > screenHalfSize || transform.position.x < -screenHalfSize)
        {
            position.x = transform.position.x > 0 ? -screenHalfSize : screenHalfSize;
            transform.position = position;
        }
    }

    private void ShoutProcess()
    {
        bool shout;

        #if UNITY_EDITOR || UNITY_WII
            shout = Input.GetKeyDown(KeyCode.DownArrow);
        #else
            shout = Input.touchCount > 0;
        #endif

        if (shout)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        var createdBulled = Instantiate(bullet, transform.position, Quaternion.identity);
        var rb = createdBulled.GetComponent<Rigidbody2D>();
        
        var velocity = rb.velocity;
        velocity.y += 24;
        rb.velocity = velocity;
        
        Destroy(createdBulled.gameObject, 1);
    }    

    public void Restart()
    {
        transform.position = Vector3.zero;
        MaxHeight = transform.position.y;
        _active = false;
        gameObject.SetActive(false);
    }

    public void Activate()
    {
        _active = true;
        gameObject.SetActive(true);
    }
}