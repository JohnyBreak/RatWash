Use "TagMask" like you use LayerMask but you can choose multiple tags to match

Usage:

```csharp

using UnityEngine;
using GameOn.TagMaskField;

public class TagMaskDemo : MonoBehaviour
{
	[SerializeField]
	private TagMask tagMask = TagMask.Everything;
	
	private bool ShouldTrigger(Collider other)
	{
		return tagMask.Contains(other.gameObject.tag);
	}
}

```