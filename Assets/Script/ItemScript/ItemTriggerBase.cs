using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemTriggerBase : MonoBehaviour
{
    [HideInInspector]
    public GameManager gameManager;
	[HideInInspector]
	public ItemDisplay item;

	public virtual void Single(MascotDisplay mascot) { }

    public virtual void Multiple() { }
}
