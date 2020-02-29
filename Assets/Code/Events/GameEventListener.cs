using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class GameEventListener : MonoBehaviour, IListener 
{
	[Tooltip("Event to register to")]
	public GameEvent gameEvent;

	[Tooltip("How long to wait before invoking the response (seconds)")]
	[Range(0, 2.5f)] public float responseDelay;

	[SerializeField] private UnityEvent _response = null;

	protected virtual void OnEnable() 
	{
	    gameEvent.RegisterListener(this);
	}

	protected virtual void OnDisable() 
	{
		gameEvent.UnregisterListener(this);
	}

	public void OnEventRaised() 
	{
        if (responseDelay > 0f)
            StartCoroutine(InvokeLater(responseDelay));
        else
            _response.Invoke();
	}

	private IEnumerator InvokeLater(float p_wait)
	{
		yield return new WaitForSeconds(p_wait);
		_response.Invoke();
	}
}
