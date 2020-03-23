using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public interface IPlayerListener
{
    void OnChangeState(PlayerState newState);
}

public enum PlayerState
{
    None,
    Game,
}

[System.Serializable]
public class Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour, IPlayerListener
{
	public float speed;
	public Boundary boundary;
    Collider coll;
	public GameObject shot;
	public Transform shotSpawn;
    public float fireRate;
    private PlayerState state;
    List<GameObject> boltList = new List<GameObject>();
    private float nextFire;

    Rigidbody rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        GameController.Instance.AddPlayerListener(this);
        coll = GetComponent<Collider>();
        GameController.Instance.InitPool(20, shot, boltList);
    }

    private void OnDestroy()
    {
        GameController.Instance.RemovePlayerListener(this);
    }

    void Update ()
	{
        if(state == PlayerState.Game)
        {
            if (Input.GetButton("Fire1") && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                for (int i = 0; i < boltList.Count; i++)
                {
                    if (!boltList[i].activeInHierarchy)
                    {
                        boltList[i].transform.position = shotSpawn.position;
                        boltList[i].SetActive(true);
                        break;
                    }
                }
            }
        }
	}

	void FixedUpdate ()
	{
        if (state == PlayerState.Game)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            rigid.velocity = movement * speed;

            rigid.position = new Vector3
            (
                Mathf.Clamp(rigid.position.x, boundary.xMin, boundary.xMax),
                0.0f,
                Mathf.Clamp(rigid.position.z, boundary.zMin, boundary.zMax)
            );
        }
	}


    public void OnChangeState(PlayerState newState)
    {
        state = newState;
        switch (state)
        {
            case PlayerState.None:
                rigid.velocity = Vector3.zero;
                rigid.position = Vector3.zero;
                coll.enabled = false;
                break;
            case PlayerState.Game:
                coll.enabled = true;
                break;
        }

    }

}
