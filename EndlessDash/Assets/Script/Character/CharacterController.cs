using System.Collections;
using DG.Tweening;
using UnityEngine;

public class CharacterController : MonoBehaviour
{  
    [SerializeField]
    private HeroConfig HeroConfig;

    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private CapsuleCollider _capsuleCollider;

    private float _moveSpeed = 11f;
    private bool _isLeftMove = false;
    private bool _isRightMove = false;
    private bool _isJumpMove = false;
    private bool _isSlideMove = false;
    private int _maxRoadLine = 3;
    private int _currentLine = 0;

    private int _lastAction = (int)Constant.HeroAction.Idle;

    IEnumerator _coroutine = null;
    private void Awake()
    {
        gameObject.SetActive(false);
        _moveSpeed = HeroConfig.DefaultMoveSpeed;
    }
    void Update()
    {        
        if (!GameManager.Instance.IsAppPause)
        {
            if (_moveSpeed < HeroConfig.MaxMoveSpeed)
            {
                _moveSpeed += Time.deltaTime * HeroConfig.MoveSpeedIncFactor;
            }
        }
    }

    void TakeDamage(int damage)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }

        _coroutine = SetSpeedAfterDamage();
        StartCoroutine(_coroutine);
    }

    void SetPlayerToCenter()
    {
        Vector3 pos = transform.position;
        pos.x = 0;
        transform.position = pos;
    }

    IEnumerator SetSpeedAfterDamage()
    {
        yield return new WaitForSeconds(0);

        Health health = gameObject.GetComponent<Health>();
        if (health != null && health.Live > 0)
        {
            EventManager.RaiseAppPause(true);

            yield return new WaitForSeconds(0.5f);

            transform.DOScale(Vector3.zero, 0.25f);

            yield return new WaitForSeconds(0.25f);

            SetPlayerToCenter();
            Init();
            transform.DOScale(Vector3.one, 0.25f);

            yield return new WaitForSeconds(0.25f);

            _moveSpeed = HeroConfig.DefaultMoveSpeed;
            EventManager.RaiseAppPause(false);
        }
    }

    void Init()
    {
        _isLeftMove = false;
        _isRightMove = false;
        _isJumpMove = false;
        _isSlideMove = false;
        _maxRoadLine = 3;
        _currentLine = 0;
        _moveSpeed = HeroConfig.DefaultMoveSpeed;
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.IsAppPause)
        {
            UpdatePlayerState((int)Constant.HeroAction.Idle);
        }
        else
        {
            if (_isLeftMove)
            {
                MovePlatform(new Vector3(-0.27f, 0, 1));
            }
            else if (_isRightMove)
            {
                MovePlatform(new Vector3(0.27f, 0, 1));
            }
            else if (_isJumpMove)
            {
                MovePlatform(new Vector3(0, 0.4f, 1));
            }
            else
            {
                // Move the platform forward along the z-axis
                MovePlatform(Vector3.forward);
            }

            if (_isSlideMove)
            {
                UpdateCapsuleCollider(0.5f, 0.65f);
            }
            else
            {
                UpdateCapsuleCollider(1, 1.5f);
            }

            if (_lastAction == (int)Constant.HeroAction.Idle)
            {
                UpdatePlayerState((int)Constant.HeroAction.Run);
            }
        }
    }

    void UpdateCapsuleCollider(float yCcenter, float height)
    {
        _capsuleCollider.center = new Vector3(0, yCcenter, 0);
        _capsuleCollider.height = height;
    }

    void MovePlatform(Vector3 direction)
    {
        // Calculate the movement vector
        Vector3 movement = direction * _moveSpeed * Time.fixedDeltaTime;
        movement.x = direction.x * 11* Time.fixedDeltaTime;//X direction fixed movment.
        // Apply the movement to the platform using physics
        GetComponent<Rigidbody>().MovePosition(transform.position + movement);
    }

    private void OnEnable()
    {
        EventManager.OnAddActions += HandleHeroAction;
        EventManager.OnInit += Init;

        EventManager.OnTakeDamage += TakeDamage;
    }
    private void OnDisable()
    {
        EventManager.OnAddActions -= HandleHeroAction;
        EventManager.OnInit -= Init;
        EventManager.OnTakeDamage -= TakeDamage;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }
    private void HandleHeroAction(int action)
    {
        if (_isLeftMove || _isRightMove || _isJumpMove)
            return;

        UpdatePlayerState(action);
    }

    private void UpdatePlayerState(int action)
    {
        _isLeftMove = false;
        _isRightMove = false;
        _isJumpMove = false;
        _isSlideMove = false;

        if (_lastAction == action)
            return;

        CancelInvoke("SetNextAnimState");

        _lastAction = action;

        if (action == (int)Constant.HeroAction.Idle)
        {
            _animator.SetInteger("anim", (int)Constant.HeroAnimType.Idle);
        }
        else if (action == (int)Constant.HeroAction.Run)
        {
            _animator.SetInteger("anim", (int)Constant.HeroAnimType.Run);
        }
        else if (action == (int)Constant.HeroAction.TurnLeft && _currentLine > -1)
        {
            _currentLine--;
            _isLeftMove = true;
            _animator.SetInteger("anim", (int)Constant.HeroAnimType.Left);
            Invoke("SetNextAnimState", (float)Constant.AnimEndTime.Lefl / 1000f);
        }
        else if (action == (int)Constant.HeroAction.TurnRight && _currentLine < 1)
        {
            _currentLine++;
            _isRightMove = true;
            _animator.SetInteger("anim", (int)Constant.HeroAnimType.Right);
            Invoke("SetNextAnimState", (float)Constant.AnimEndTime.Right / 1000f);
        }
        else if (action == (int)Constant.HeroAction.Jump)
        {
            _isJumpMove = true;
            _animator.SetInteger("anim", (int)Constant.HeroAnimType.Jump);
            Invoke("SetNextAnimState", (float)Constant.AnimEndTime.Jump / 1000f);
        }
        else if (action == (int)Constant.HeroAction.Slide)
        {
            _isSlideMove = true;
            _animator.SetInteger("anim", (int)Constant.HeroAnimType.Slide);
            Invoke("SetNextAnimState", (float)Constant.AnimEndTime.Slide / 1000f);
        }
    }
    void SetNextAnimState()
    {
        CancelInvoke("SetNextAnimState");
        UpdatePlayerState((int)Constant.HeroAction.Run);
    }
}