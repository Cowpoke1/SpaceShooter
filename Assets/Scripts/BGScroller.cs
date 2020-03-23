using UnityEngine;
using System.Collections;

public class BGScroller : MonoBehaviour,IPlayerListener
{
	public float scrollSpeed;
	public float tileSizeZ;
    PlayerState state;
	private Vector3 startPosition;

	void Start ()
	{
		startPosition = transform.position;
        GameController.Instance.AddPlayerListener(this);
    }

    private void OnDestroy()
    {
        GameController.Instance.RemovePlayerListener(this);
    }

    void Update ()
	{
        if(state == PlayerState.Game)
        {
            float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
            transform.position = startPosition + Vector3.forward * newPosition;
        }
	}

    public void OnChangeState(PlayerState newState)
    {
        state = newState;
    }
}