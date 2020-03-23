using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using System;

public class AsteroidModel : MonoBehaviour
{
	public GameObject playerExplosion;
    private ObservableTriggerTrigger trigger;

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
        if (coll.tag == "Player")
        {
            Instantiate(playerExplosion, coll.transform.position, coll.transform.rotation);
            GameController.Instance.LiveReduce();           
        }
        else if (coll.tag == "BoltTag")
        {
            GameController.Instance.AddScore();
            coll.gameObject.SetActive(false);
        }

        gameObject.SetActive(false);
    }

    void Start ()
	{
        GetComponent<Rigidbody>().velocity = transform.forward * -5f;
        Init();
    }


}