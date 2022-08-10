using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColtroller : Singleton<PlayerColtroller>
{
    /// <summary>
    /// 移动速度和旋转速度
    /// </summary>
    public float MoveSpeed = 10f, RotationSpeed = 10f;
    /// <summary>
    /// CharacterController的实例
    /// </summary>
    private CharacterController MoveController;
    /// <summary>
    /// 开始的生命值和魔法值
    /// </summary>
    public float StartHealth, StartMana;
    /// <summary>
    /// 最大生命值和魔法值
    /// </summary>
    private float MaxHealth, MaxMana;
    /// <summary>
    /// 生命值和魔法值
    /// </summary>
    private float health, mana;
    /// <summary>
    /// 生命值
    /// </summary>
    public float Health
    {
        get
        {
            return health;
        }

        set
        {
            health = Mathf.Clamp(value, 0, MaxHealth);
        }
    }
    /// <summary>
    /// 魔法值
    /// </summary>
    public float Mana
    {
        get
        {
            return mana;
        }

        set
        {
            mana = Mathf.Clamp(value, 0, MaxMana);
        }
    }
    /// <summary>
    /// 现在的交互物体
    /// </summary>
    public IInteraction InteractionObject { get; private set; }
    /// <summary>
    /// 上一帧的交互物体
    /// </summary>
    public IInteraction LastInteractionObject { get; private set; }
    /// <summary>
    /// 跳跃的速度
    /// </summary>
    private float UpSpeed = -1;

    /// <summary>
    /// 射线可检测的 交互层级
    /// </summary>
    public LayerMask InteractionLayerMask;
    /// <summary>
    /// 当交互发生时 事件
    /// </summary>
    public event System.Action<IInteraction> OnInteraction;
    private void Awake()
    {
        MoveController = GetComponent<CharacterController>();

        MaxHealth = StartHealth;
        MaxMana = StartMana;

        Health = MaxHealth;
        Mana = MaxMana;
    }


    private void Update()
    {
        if (GameMarager.Single.State != State.Game) return;

        //player旋转
        transform.Rotate(0, InputMarager.Horizontal * RotationSpeed * Time.deltaTime, 0);
        //player移动 方向
        if (InputMarager.Space) { UpSpeed = 1; Invoke("ResetUpSpeed", 0.2f); }
        Vector3 Direction = new Vector3(InputMarager.LeftRight, UpSpeed, InputMarager.ForwardBack);
        //把方向改变为自己的移动方向
        Direction = transform.TransformDirection(Direction);
        //角色控制器 控制移动
        MoveController.Move(Direction * MoveSpeed * Time.deltaTime);

        //发射线检测是否有交互物体
        InteractionObject = null;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit, 4f, InteractionLayerMask))
        {
            InteractionObject = hit.collider.gameObject.GetComponent<IInteraction>();
            //如果找到的物体不为空 并且是未激活
            if (InteractionObject != null && InteractionObject.IsActive == false)
            {
                InteractionObject = null;
            }

            if (InteractionObject != null && InteractionObject != LastInteractionObject)
            {
                OnInteraction(InteractionObject);
            }
            //如果不为空 并且按下E 展开交互的ui
            if (InteractionObject != null)
            {
                if (InputMarager.Use)
                {
                    InteractionObject.Use();
                }
            }
        }
        else
        {
            OnInteraction(InteractionObject);
        }
        LastInteractionObject = InteractionObject;
    }
    /// <summary>
    /// 重置跳跃值
    /// </summary>
    void ResetUpSpeed()
    {
        UpSpeed = -1;
    }
}
