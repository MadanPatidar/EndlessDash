using UnityEngine;

public class TouchDirection : MonoBehaviour
{    
    public float SwipeThreshold = 50f;

    private Vector2 _touchStartPos;
    private Vector2 _touchEndPos;
    private bool _isDragging = false;
    void Update()
    {
        HandleTouchInput();
    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _touchStartPos = touch.position;
                    _isDragging = true;
                    break;

                case TouchPhase.Moved:
                    if (_isDragging)
                    {
                        _touchEndPos = touch.position;
                        DetectSwipeDirection();
                    }
                    break;

                case TouchPhase.Ended:
                    _touchEndPos = touch.position;
                    if(_isDragging)
                        DetectSwipeDirection();
                    _isDragging = false;
                    break;
            }
        }
    }

    void DetectSwipeDirection()
    {
        float swipeX = _touchEndPos.x - _touchStartPos.x;
        float swipeY = _touchEndPos.y - _touchStartPos.y;

        if (Mathf.Abs(swipeX) > Mathf.Abs(swipeY))
        {
            // Horizontal swipe
            if (swipeX > SwipeThreshold)
            {
                //Debug.Log("Swipe Right");
                _isDragging = false;
                EventManager.RaiseAddActions((int)Constant.HeroAction.TurnRight);
            }
            else if (swipeX < -SwipeThreshold)
            {
                //Debug.Log("Swipe Left");
                _isDragging = false;
                EventManager.RaiseAddActions((int)Constant.HeroAction.TurnLeft);
            }
        }
        else
        {
            // Vertical swipe
            if (swipeY > SwipeThreshold)
            {
                //Debug.Log("Swipe Up");
                _isDragging = false;
                EventManager.RaiseAddActions((int)Constant.HeroAction.Jump);
            }
            else if (swipeY < -SwipeThreshold)
            {
                //Debug.Log("Swipe Down");
                _isDragging = false;
                EventManager.RaiseAddActions((int)Constant.HeroAction.Slide);
            }
        }
    }
}
