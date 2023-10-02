using UnityEngine;

namespace LD54
{
	public class UpgradePanel : MonoBehaviour
	{
		public void Show()
		{
			gameObject.SetActive(true);
		}

		public void Hide()
		{
			gameObject.SetActive(false);
		}
	} 
}
