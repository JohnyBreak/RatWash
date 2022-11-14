using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameOn.TagMaskField
{
	[System.Serializable]
	public struct TagMask
	{
		#region Fields

		[SerializeField]
		private int mask;

		[SerializeField]
		private string[] tags;

		#endregion

		#region Properties

		public static TagMask Everything
        {
			get
            {
				var newMask = new TagMask();
				newMask.mask = -1;
				newMask.tags = new string[] { "Everything" };
				return newMask;
			}
        }

		public static TagMask Nothing
        {
			get
            {
				var newMask = new TagMask();
				newMask.mask = 0;
				newMask.tags = new string[] { "Nothing" };
				return newMask;
			}
        }

		public int Mask => mask;
		public string[] Tags => tags;

		#endregion

		public TagMask(params string[] tags)
        {
			if (ContainsString(tags, "Everything"))
            {
				mask = -1;
				this.tags = new string[] { "Everything" };
            }
			else if (ContainsString(tags, "Nothing"))
            {
				mask = 0;
				this.tags = new string[] { "Nothing" };
			}
            else
            {
				mask = 1;
				this.tags = tags;
			}
        }

		public static TagMask GetMask(params string[] tags)
        {
			return new TagMask(tags);
        }

		public static implicit operator int(TagMask tagMask)
		{
			return tagMask.mask;
		}

		public static bool operator ==(TagMask a, TagMask b)
		{
			return IsEqual(a, b);
		}

		public static bool operator !=(TagMask a, TagMask b)
		{
			return !IsEqual(a, b);
		}

		public static bool IsEqual(TagMask a, TagMask b)
		{
			bool fullmasks = a.mask == -1 && b.mask == -1 || a.mask == 0 || b.mask == 0;

			if (fullmasks)
            {
				return true;
            }
			else
            {
				if (a.tags.Length == b.tags.Length)
                {
					for (int i = 0; i < a.tags.Length; i++)
					{
						if (a.tags[i] != b.tags[i])
                        {
							return false;
                        }
					}

					return true;
				}
            }

			return false;
		}

		public bool Contains(string tag)
		{
			if (mask == -1)
            {
				return true;
            }
			else if (mask == 0)
            {
				return false;
            }
			else
            {
				return ContainsString(tags, tag);
			}
		}

		internal static bool ContainsString(string[] array, string item)
		{
			int index = System.Array.IndexOf(array, item);

			if (index >= 0 && index < array.Length)
			{
				return true;
			}

			return false;
		}
	}
}
