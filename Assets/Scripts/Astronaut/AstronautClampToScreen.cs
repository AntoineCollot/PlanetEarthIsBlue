using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronautClampToScreen : MonoBehaviour
{
    [SerializeField] Vector2 margins = Vector2.zero;
    Camera cam;
    new Rigidbody2D rigidbody;
    Animator anim;

    public enum ScreenState {InScreen, OutOfScreenX, OutOfScreenY}
    ScreenState state;

    public bool IsInScreenX
    {
        get
        {
            Vector2 viewportPos = cam.WorldToViewportPoint(transform.position);
            return viewportPos.x >= margins.x && viewportPos.x <= 1 - margins.x;
        }
    }

    public bool IsInScreenY
    {
        get
        {
            Vector2 viewportPos = cam.WorldToViewportPoint(transform.position);
            return viewportPos.y >= margins.y && viewportPos.y <= 1 - margins.y;
        }
    }

    public bool IsInScreen
    {
        get
        {
            Vector2 viewportPos = cam.WorldToViewportPoint(transform.position);
            return viewportPos.x >= margins.x && viewportPos.x <= 1 - margins .x && viewportPos.y >= margins.y && viewportPos.y <= 1 - margins.y;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateScreenState();

       if(state != ScreenState.InScreen)
        {
            transform.position = ClampToScreen(transform.position);
            ReflectTrajectory(state);
            anim.SetTrigger("Bump");
        }
    }

    void UpdateScreenState()
    {
        if(!IsInScreenX)
        {
            state = ScreenState.OutOfScreenX;
        }
        else if(!IsInScreenY)
        {
            state = ScreenState.OutOfScreenY;
        }
        else
        {
            state = ScreenState.InScreen;
        }
    }

    Vector2 ClampToScreen(Vector2 position)
    {
        Vector2 viewportPos = cam.WorldToViewportPoint(position);
        viewportPos.x = Mathf.Clamp(viewportPos.x, margins.x, 1 - margins.x);
        viewportPos.y = Mathf.Clamp(viewportPos.y, margins.y, 1 - margins.y);
        return cam.ViewportToWorldPoint(viewportPos);
    }

    void ReflectTrajectory(ScreenState state)
    {
        Vector2 velocity = rigidbody.velocity;
        switch (state)
        {
            case ScreenState.OutOfScreenX:
                velocity.x *= -1;
                break;
            case ScreenState.OutOfScreenY:
                velocity.y *= -1;
                break;
        }

        rigidbody.velocity = velocity;
    }

}
