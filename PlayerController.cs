using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public ParticleSystem coinSound;
    public GameObject DeathButton;

    public GameObject PauseButton;
    public static int coins = 0;
    public static bool slideDone = true;
    public static Vector3 manBeforeSlide;
    public float distance;
    public static bool died = false;
    public static bool OneTimeDeath = true;
    public static bool play = false;
    public static bool helloEnded = false;
    public static bool EnviromentMoves = false;
    public static bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;

    public static AudioSource collisionSound;
    public static Animator animator;
    private CharacterController cc;

    private int mid = 0;
    private int k = 0;
    private float time = 0.4f;
    private float dir;
    private float currentDistance = 0f;
    private float currentDir = 0f;
    private bool isInMovement = false;
    private bool jumpDone = true;
    private bool isDraging = false;
    private Vector2 startTouch, swipeDelta;

    void Start()
    {
        collisionSound = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }
    void Update()
    {
        if (!helloEnded && !animator.GetCurrentAnimatorStateInfo(0).IsName("Start"))
            helloEnded = true;

        if (play && !died)
        {
            tap = swipeDown = swipeUp = swipeLeft = swipeRight = false;
            if (Input.GetMouseButtonDown(0))
            {
                tap = true;
                isDraging = true;
                startTouch = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isDraging = false;
                Reset();
            }

            if (Input.touches.Length > 0)
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    tap = true;
                    isDraging = true;
                    startTouch = Input.touches[0].position;
                }
                else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
                {
                    isDraging = false;
                    Reset();
                }
            }
            swipeDelta = Vector2.zero;

            if (isDraging)
            {
                if (Input.touches.Length < 0)
                    swipeDelta = Input.touches[0].position - startTouch;
                else if (Input.GetMouseButton(0))
                    swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }

            if (swipeDelta.magnitude > 90)
            {
                float x = swipeDelta.x;
                float y = swipeDelta.y;
                if (Mathf.Abs(x) > Mathf.Abs(y))
                {

                    if (x < 0)
                        swipeLeft = true;
                    else
                        swipeRight = true;
                }
                else
                {

                    if (y < 0)
                        swipeDown = true;
                    else
                        swipeUp = true;
                }
            }

            if (swipeDown && slideDone && helloEnded)
            {
                slideDone = false;
                animator.SetTrigger("Slide");
                StartCoroutine(GoSlide());
            }

            if (swipeUp && helloEnded && jumpDone)
            {
                jumpDone = false;
                Debug.Log(jumpDone);
                Reset();
                animator.SetTrigger("Up");
            }

            dir = 0;

            if (swipeLeft)
                dir = -1;
            else if (swipeRight)
                dir = 1;

            if (helloEnded && !isInMovement && (((dir > 0) && (mid != 1)) || (dir < 0) & (mid != -1)))
            {
                currentDir = dir;
                currentDistance = distance;
                if ((dir > 0) && (mid < 1) && (slideDone) && (jumpDone))
                {
                    isInMovement = true;
                    animator.SetTrigger("Right");
                    mid++;
                    StartCoroutine(GoRight());
                }
                else if ((dir > 0) && (mid < 1) && (!jumpDone))
                {
                    isInMovement = true;
                    mid++;
                    StartCoroutine(GoRight());
                }
                else if ((dir > 0) && (mid < 1) && (!slideDone))
                {
                    isInMovement = true;
                    animator.SetTrigger("RightAfterSlide");
                    mid++;
                    StartCoroutine(GoRight());
                }

                if ((dir < 0) && (mid > -1) && (slideDone) && (jumpDone))
                {
                    isInMovement = true;
                    animator.SetTrigger("Left");
                    mid--;
                    StartCoroutine(GoLeft());
                }
                else if ((dir < 0) && (mid > -1) && (!jumpDone))
                {
                    isInMovement = true;
                    mid--;
                    StartCoroutine(GoLeft());
                }
                else if ((dir < 0) && (mid > -1) && (!slideDone))
                {
                    isInMovement = true;
                    animator.SetTrigger("LeftAfterSlide");
                    mid--;
                    StartCoroutine(GoLeft());
                }
            }
        }
    }
    IEnumerator GoLeft()
    {
        Reset();
        while (currentDistance >= 0)
        {
            yield return new WaitForEndOfFrame();
            float speed1 = distance / time * 2;
            float tmpDist = Time.deltaTime * speed1;
            animator.SetFloat("SpeedAnim", WorldController.speedOfAnim);
            cc.Move(Vector3.right * currentDir * tmpDist);
            currentDistance -= tmpDist;
        }

        if (transform.position.x > 10f)
            transform.position = new Vector3(11f, transform.position.y, transform.position.z);
        else if (transform.position.x > 8f)
            transform.position = new Vector3(9.62f, transform.position.y, transform.position.z);

        isInMovement = false;
    }

    IEnumerator GoRight()
    {
        Reset();
        while (currentDistance >= 0)
        {
            yield return new WaitForEndOfFrame();
            float speed1 = distance / time * 2;
            float tmpDist = Time.deltaTime * speed1;
            animator.SetFloat("SpeedAnim", WorldController.speedOfAnim);
            cc.Move(Vector3.right * currentDir * tmpDist);
            currentDistance -= tmpDist;
        }

        if (transform.position.x > 11.6f)
            transform.position = new Vector3(12.38f, transform.position.y, transform.position.z);
        else if (transform.position.x > 10f)
            transform.position = new Vector3(11f, transform.position.y, transform.position.z);

        isInMovement = false;
    }
    IEnumerator GoSlide()
    {
        Reset();
        currentDistance = transform.position.y - 4.797371f;
        while (currentDistance > 0)
        {
            yield return new WaitForEndOfFrame();
            float tmpDist = Time.deltaTime * 3.5f;
            cc.Move(Vector3.down * tmpDist);
            currentDistance -= tmpDist;
        }
        slideDone = true;
    }
    public void StartLevel()
    {
        play = true;
        EnviromentMoves = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (play)
        {
            if (other.CompareTag("Danger") && OneTimeDeath)
            {
                OneTimeDeath = false;
                PauseButton.SetActive(false);
                died = true;
                EnviromentMoves = false;
                animator.SetTrigger("Death");
                DeathButton.SetActive(true);
                play = false;
                helloEnded = false;
                EnviromentMoves = false;
            }

            if (other.CompareTag("Coin"))
            {
                Destroy(other.gameObject);
                collisionSound.Play();
                coinSound.transform.position = new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z);
                coinSound.Play();
                coins++;
            }
        }
    }

    private void Reset()
    {
        tap = swipeDown = swipeUp = swipeLeft = swipeRight = false;
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Ground") && k == 0 && !jumpDone)
        {
            k++;
            Invoke(nameof(JumpTrue), 1.25f);
        }
    }

    private void JumpTrue()
    {
        jumpDone = true;
        k--;
    }
}