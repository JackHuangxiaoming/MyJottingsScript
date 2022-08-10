using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class RigidbodyAndBoxColliderByMap : MonoBehaviour
{
    private BoxCollider[] _boxColliders;
    private Rigidbody _rigidbody;
    private float _uiWidth = 0;
    private float _uiHeight = 0;
    private Vector3 size = new Vector3()
    {
        x = 0,
        y = 0,
        z = 40
    };
    private Vector3 offset = new Vector3()
    {
        x = 0,
        y = 0,
        z = 20
    };
    private void Awake()
    {
        gameObject.AddComponent<BoxCollider>();
        gameObject.AddComponent<BoxCollider>();
        gameObject.AddComponent<BoxCollider>();
        _boxColliders = gameObject.GetComponents<BoxCollider>();
        _rigidbody = gameObject.GetComponent<Rigidbody>();

        _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        _rigidbody.useGravity = false;
        _rigidbody.mass = 1;
        _rigidbody.drag = 1;
        _rigidbody.angularDrag = 1;
        UpdateBound(_uiWidth, _uiHeight);
    }
    public void UpdateBound(float width, float height, int ltopWidth = 700, int ltopHeight = 700, int rBottomWidth = 0, int rBottomHeight = 0)
    {
        if (_boxColliders == null)
        {
            _uiWidth = width;
            _uiHeight = height;
            return;
        }
        rBottomWidth = rBottomWidth == 0 ? ltopWidth : rBottomWidth;
        rBottomHeight = rBottomHeight == 0 ? ltopHeight : rBottomHeight;
        BoxCollider bc2;
        for (int i = 0; i < 4; i++)
        {
            if (i >= _boxColliders.Length)
            {
                break;
            }
            bc2 = _boxColliders[i];
            switch (i)
            {
                case 0:
                    size.Set(width + ltopWidth, ltopHeight, size.z);
                    offset.Set(width / 2, 0, size.z);
                    break;
                case 1:
                    size.Set(width + rBottomWidth, rBottomHeight, size.z);
                    offset.Set(width / 2, -height, size.z);
                    break;
                case 2:
                    size.Set(ltopWidth, height + ltopHeight, size.z);
                    offset.Set(0, -height / 2, size.z);
                    break;
                case 3:
                    size.Set(rBottomWidth, height + rBottomHeight, size.z);
                    offset.Set(width, -height / 2, size.z);
                    break;
                default:
                    break;
            }

            _boxColliders[i].size = size;
            _boxColliders[i].center = offset;
        }
    }
}
