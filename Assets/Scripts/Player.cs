using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Vector3 direction;
    public float Speed;
    public float JumpForce;

    [SerializeField] float sideWaysMovementDuration = .5f;

    Coroutine coroutine;
    //bool isGrounded;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] LayerMask groundLayer;

    public int CurrentDificulty = 1;
    public bool isGameStarted;
    float tempSpeedSaved;
    float timeToIncreaseSpeed = 3;
    float elapsedTimeToIncreaseSpeed;

    private void OnEnable()
    {
        GameManager.OnStartGame += onStartGame;
        GameManager.OnEndGame += onEndGame;
        rb.useGravity = false;
        tempSpeedSaved = Speed;
        elapsedTimeToIncreaseSpeed = 0;
    }
    private void OnDisable()
    {
        GameManager.OnStartGame -= onStartGame;
        GameManager.OnEndGame -= onEndGame;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isGameStarted) { return; }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (rb.position.x > -1)
            {
                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                    coroutine = null;
                }
                coroutine = StartCoroutine(MovePlayerInAxisX(new Vector3(rb.position.x - 1, rb.position.y, 0)));
            }

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (rb.position.x < 2)
            {
                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                    coroutine = null;
                }
                coroutine = StartCoroutine(MovePlayerInAxisX(new Vector3(rb.position.x + 1, rb.position.y, 0)));
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded())
            {
                rb.AddForce(new Vector3(0, JumpForce, 0));
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isGrounded())
            {

            }
            else
            {
                rb.AddForce(new Vector3(0, -JumpForce * 2, 0));
            }
        }

        if (elapsedTimeToIncreaseSpeed < timeToIncreaseSpeed)
        {
            
            elapsedTimeToIncreaseSpeed += Time.deltaTime;
        }
        else if (elapsedTimeToIncreaseSpeed >= timeToIncreaseSpeed)
        {
            elapsedTimeToIncreaseSpeed = 0;
            Speed += 2;
        }
    }
    bool isGrounded()
    {
        return Physics.CheckSphere(groundCheckPoint.transform.position, 0.1f, groundLayer);
    }
    private void FixedUpdate()
    {
        movePlayer(direction);
    }

    void movePlayer(Vector3 movementInAxisZ)
    {
        rb.MovePosition((rb.position + (movementInAxisZ * Speed * Time.deltaTime)));
    }
    IEnumerator MovePlayerInAxisX(Vector3 targetPosition)
    {
        float timeElapsed = 0;
        Vector3 startPosition = rb.position;
        while (timeElapsed < sideWaysMovementDuration)
        {
            rb.MovePosition(new Vector3(Mathf.Lerp(startPosition.x, targetPosition.x, timeElapsed / sideWaysMovementDuration), rb.position.y, rb.position.z));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        rb.MovePosition(new Vector3((targetPosition.x), rb.position.y, rb.position.z));
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Platform")
        {
            other.gameObject.SetActive(false);
            ObjectPool.Instance.DoPool();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameManager.Instance.EndGame();
            UIManager.Show<GameOverView>();
        }
    }
    void onStartGame()
    {
        direction = new Vector3(0, 0, 0.1f);
        this.transform.position = new Vector3(0,1, -5);
        rb.useGravity = true;
        isGameStarted = true;
        Speed = tempSpeedSaved;
    }
    void onEndGame()
    {
        direction = new Vector3(0, 0, 0);
        rb.useGravity = false;
        isGameStarted = false;
    }

}
