using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class BoltModel : MonoBehaviour
{
    private ObservableTriggerTrigger trigger;
    [SerializeField]
    LayerMask layer;

    private void Init()
    {
        trigger = GetComponent<ObservableTriggerTrigger>();
        trigger
            .OnTriggerEnterAsObservable()
            .Subscribe(CollisionAction)
            .AddTo(this);
    }

    private void CollisionAction(Collider coll)
    {
        gameObject.SetActive(false);
    }

    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * 10f;
        Init();
    }
}
