using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TestPlayerMovement : MonoBehaviour
{
    public Rigidbody2D body;
    public CircleCollider2D collider2D;
    public AudioSource audioSource;
    public float force = 10.0f;
    int _currentLayer = 0;
    const int topLayer = 2;
    const int downLayer = 0;
    //0 down  bigger
    //1 middle
    //2 top smaller
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<CircleCollider2D>();
        audioSource = GetComponent<AudioSource>();
        GameInput.instance.OnSwipeDown += moveDown;
        GameInput.instance.OnSwipeUp += moveUp;
        GameInput.instance.OnSwipeRight += moveRight;
        GameInput.instance.OnSwipeLeft += moveLeft;
        RestartLevel();
    }
    void RestartLevel()
    {
        transform.localScale = new Vector3(.38f, .38f, 1.0f);
        _currentLayer = 1;
        SetLayer(_currentLayer);
        transform.position = new Vector3(-31.5f, transform.position.y, transform.position.z);
        body.AddForce(new Vector2(300.0f, 0.0f));
        audioSource?.Play();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        RestartLevel();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void moveUp()
    {
        if (_currentLayer != topLayer)
        {
            SetLayer(++_currentLayer);
            //body.AddForce(new Vector2(0.0f, 1.0f * force));
        }
    }
    void moveDown()
    {
        if (_currentLayer != downLayer)
        {
            SetLayer(--_currentLayer);
            // body.AddForce(new Vector2(0.0f, -1.0f * force));
        }
    }
    void moveLeft()
    {
        //body.AddForce(new Vector2(-1.0f * force, -0.0f));
    }
    void moveRight()
    {
       // body.AddForce(new Vector2(1.0f * force, 0.0f));
    }
    void SetLayer(int layer)
    {
        switch (layer)
        {
            case 0:
                transform.localScale = new Vector3(.4f, .4f, 1.0f);
                transform.position = new Vector3(transform.position.x, -0.92f, transform.position.z);
                break;
            case 1:
                transform.localScale = new Vector3(.38f, .38f, 1.0f);
                transform.position = new Vector3(transform.position.x, -0.36f, transform.position.z);
                break;
            case 2:
                transform.localScale = new Vector3(.36f, .36f, 1.0f);
                transform.position = new Vector3(transform.position.x, 0.2f, transform.position.z);
                break;

        }
    }
}
