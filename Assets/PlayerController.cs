using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Movement")]
    public float moveSpeed = 10f;

    [Header("Dash System")]
    public float energy = 5f;
    public float maxEnergy = 5f;
    public float dashForce = 15f;
    public float dashCost = 1f;
    public float dashCooldown = 1.5f;
    private float lastDashTime;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    private int score = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        UpdateScoreDisplay();
    }

    void Update()
    {
        HandleDashInput();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        float moveX = 0;
        float moveZ = 0;

        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) moveZ = 1;
        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) moveZ = -1;
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) moveX = 1;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) moveX = -1;

        Vector3 move = new Vector3(moveX, 0, moveZ).normalized;

        Vector3 velocity = rb.linearVelocity;
        rb.linearVelocity = new Vector3(move.x * moveSpeed, velocity.y, move.z * moveSpeed);
    }

    void HandleDashInput()
    {
        if (Keyboard.current.leftShiftKey.wasPressedThisFrame)
        {
            TryDash();
        }
    }

    void TryDash()
    {
        if (Time.time < lastDashTime + dashCooldown) return;
        if (energy < dashCost) return;

        Vector3 direction = rb.linearVelocity;
        direction.y = 0;

        if (direction.magnitude == 0) return;

        energy -= dashCost;
        lastDashTime = Time.time;

        rb.AddForce(direction.normalized * dashForce, ForceMode.Impulse);

        Debug.Log("DASH USED");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            AddScore(1);
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreDisplay();
    }

    void UpdateScoreDisplay()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }
}