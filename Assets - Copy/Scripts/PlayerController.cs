using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject WinTextObject;
    public AudioSource PickupSounds;
    public LevelTimer levelTimer;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        WinTextObject.SetActive(false);

    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "count" + count.ToString();
        if(count >= 7)
        {
            WinTextObject.SetActive(true);
            levelTimer.Stop = true;
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("cube"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
        }
        if(other.gameObject.CompareTag("sphere"))
        {
            levelTimer.currentTime -= 3f;
        }

        SetCountText();
        other.gameObject.SetActive(false);
        PickupSounds.Play();
    }
}
