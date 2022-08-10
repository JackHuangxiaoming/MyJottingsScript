//#define Test_PosByFrameLog
using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class RigidbodyAndBoxCollider : MonoBehaviour
{
    private Action<Collider> _onTriggerEnterHander;
    private Action<Collider> _onTriggerStayHander;
    private Action<Collider> _onTriggerExitHander;
    private Action<Collision> _onCollisionEnterHander;
    private Action<Collision> _onCollisionStayHander;
    private Action<Collision> _onCollisionExitHander;
    private BoxCollider _boxCollider;
    private Rigidbody _rigidbody;
    private CharacterController _controller;

    private Vector3 size = Vector3.zero;
    private Vector3 offset = Vector3.zero;

    public float _speed = 0.5f;
    private Vector3 _targerPos = Vector3.zero;
    private float _targerDistance = 0f;
    private Vector3 _directionPos = Vector3.zero;

    private Vector3 _lastStartRunPos = Vector3.zero;
    private float _uiWidth = 0;
    private float _uiHeight = 0;

    public Rigidbody Rigidbody => _rigidbody;
    public CharacterController CharacterController => _controller;
    public BoxCollider BoxCollider => _boxCollider;

    private void Awake()
    {
        _boxCollider = gameObject.GetComponent<BoxCollider>();
        _rigidbody = gameObject.GetComponent<Rigidbody>();

        _rigidbody.useGravity = false;
        _rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
        _rigidbody.freezeRotation = true;
        _rigidbody.mass = 1;
        _rigidbody.drag = 1;
        _rigidbody.angularDrag = 1;
        UpdateBound(_uiWidth, _uiHeight);
    }

    public void UpdateBound(float width, float height)
    {
        if (_boxCollider == null)
        {
            _uiWidth = width;
            _uiHeight = height;
            return;
        }
        size.Set(width / 1.285f, height / 1.285f, 40);
        offset.Set(width / 2, -height / 2, 20);
        _boxCollider.size = size;
        _boxCollider.center = offset;
    }

    public void UpdateSpeed(float speed)
    {
        _speed = speed;
    }

    public void UpdateMoveTarget(float x, float y)
    {
        _lastStartRunPos = transform.localPosition;
        _targerPos.Set(x, y, 0);
        _targerDistance = Vector2.Distance(_targerPos, _lastStartRunPos);
        _directionPos = CalcDirection(_targerPos, transform.localPosition);
    }
    public void UpdateMoveTargetByDistance(float x, float y, float Distance)
    {
        _lastStartRunPos = transform.localPosition;
        _targerDistance = Distance;
        _directionPos.Set(x, -y, 0);
        _directionPos = _directionPos.normalized;
    }
    public void CancelMoveTarget()
    {
        _targerPos = Vector3.zero;
        _targerDistance = 0f;
        _directionPos.Set(0, 0, 0);
    }

    public Vector3 CalcDirection(Vector3 a, Vector3 b)
    {
        return (a - b).normalized;
    }
    public float CalcAngle(Vector3 a)
    {
        return Vector3.Angle(transform.forward, a);
    }
    public float CalcAngle(Vector3 a, Vector3 b)
    {
        //得到弧度 两个向量的长度都为1时，点乘的结果就是夹角的余弦值
        float radians = Vector3.Dot(a.normalized, b.normalized);
        //radians = Vector3.Angle(a, b);        
        // 弧度值通过常数换成角度值
        return radians * Mathf.Rad2Deg;
    }
    public float CalcLeftOrRight(Vector3 a)
    {
        //小于0 往左 大于0往右
        return Vector3.Dot(transform.right, a);
    }
    public float CalcFowardOrBack(Vector3 a)
    {
        //小于0 往后 大于0往前 等于0 就是在左右（同一个轴上）
        return Vector3.Dot(transform.forward, a);
    }
    public float CalcUpOrDown(Vector3 a)
    {
        //小于0 往下 大于0上
        return Vector3.Dot(transform.up, a);
    }

    float intervalTotal = 5;
    float interval = 0;

#if Test_PosByFrameLog
    Vector3 vector;
    float speed;
    Vector3 pos;
    float time;
    public bool isPlayer = false;
    bool Run = false;

    private void Update()
    {
        if (isPlayer)
        {
            time = 0;
            Run = true;
            isPlayer = false;
        }
    }
#endif
    private void FixedUpdate()
    {
        interval++;
        if (interval >= intervalTotal)
        {
            interval = 0;
            if (_targerPos != Vector3.zero)
            {
                if (Vector2.Distance(_targerPos, transform.localPosition) < 80)
                {
                    CancelMoveTarget();
                }
            }
            if (Vector2.Distance(_lastStartRunPos, transform.localPosition) > _targerDistance)
            {
                CancelMoveTarget();
            }
        }

        if (_targerDistance > 0 && _directionPos != Vector3.zero)
        {

#if Test_PosByFrameLog
            speed = _speed * Time.deltaTime;
            vector = _directionPos * speed;
            pos = transform.localPosition;
            transform.Translate(vector);
            if (Run)
            {
                Debug.Log($"Player Dis:{Vector3.Distance(pos, transform.localPosition)}, speed: {speed}, vectorDis: { Vector3.Distance(vector, Vector3.zero)}");
                time += Time.deltaTime;
                if (time >= 1f)
                {
                    Run = false;
                    CancelMoveTarget();
                }
            }
#else
            transform.Translate(_directionPos * _speed * Time.deltaTime);
#endif
        }
    }

    public void SetOnTriggerEnter(Action<Collider> hander)
    {
        _onTriggerEnterHander = hander;
    }
    public void SetOnTriggerStay(Action<Collider> hander)
    {
        _onTriggerStayHander = hander;
    }
    public void SetOnTriggerExit(Action<Collider> hander)
    {
        _onTriggerExitHander = hander;
    }
    public void SetOnCollisionEnter(Action<Collision> hander)
    {
        _onCollisionEnterHander = hander;
    }
    public void SetOnCollisionStay(Action<Collision> hander)
    {
        _onCollisionStayHander = hander;
    }
    public void SetOnCollisionExit(Action<Collision> hander)
    {
        _onCollisionExitHander = hander;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_onTriggerEnterHander != null)
            _onTriggerEnterHander(other);
    }
    private void OnTriggerStay(Collider other)
    {
        if (_onTriggerStayHander != null)
            _onTriggerStayHander(other);
    }
    private void OnTriggerExit(Collider other)
    {
        if (_onTriggerExitHander != null)
            _onTriggerExitHander(other);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (_onCollisionEnterHander != null)
            _onCollisionEnterHander(collision);
    }
    private void OnCollisionStay(Collision collision)
    {
        if (_onCollisionStayHander != null)
            _onCollisionStayHander(collision);
    }
    private void OnCollisionExit(Collision collision)
    {
        if (_onCollisionExitHander != null)
            _onCollisionExitHander(collision);
    }

    public void OnDestroy()
    {
        _onTriggerEnterHander = null;
        _onTriggerStayHander = null;
        _onTriggerExitHander = null;
        _onCollisionEnterHander = null;
        _onCollisionStayHander = null;
        _onCollisionExitHander = null;
    }
}
