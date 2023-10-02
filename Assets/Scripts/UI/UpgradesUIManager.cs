using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace LD54
{
	public class UpgradesUIManager : MonoBehaviour
	{
		[SerializeField]
		private UpgradePanel _bulletSpeed;
		[SerializeField]
		private UpgradePanel _bulletSpawn;
		[SerializeField]
		private UpgradePanel _attractionRadius;
		[SerializeField]
		private UpgradePanel _multiBullets;
		[SerializeField]
		private DescriptionPanel _descriptionPanel;

		public void ShowPanels()
		{
			StartCoroutine(ShowAll());
		}

		public void HidePanels()
		{
			_bulletSpeed.GetComponent<RectTransform>().DOScale(0.0f, 0.5f);
			_bulletSpeed.Hide();

			_bulletSpawn.GetComponent<RectTransform>().DOScale(0.0f, 0.5f);
			_bulletSpawn.Hide();

			_attractionRadius.GetComponent<RectTransform>().DOScale(0.0f, 0.5f);
			_attractionRadius.Hide();

			_multiBullets.Hide();
			_multiBullets.GetComponent<RectTransform>().DOScale(0.0f, 0.5f);

			_descriptionPanel.Hide();
			_descriptionPanel.GetComponent<RectTransform>().DOScale(0.0f, 0.5f);
		}

		private IEnumerator ShowAll()
		{
			_bulletSpeed.Show();
			_bulletSpeed.GetComponent<RectTransform>().DOScale(1.0f, 0.5f);

			yield return new WaitForSeconds(0.5f);

			_bulletSpawn.Show();
			_bulletSpawn.GetComponent<RectTransform>().DOScale(1.0f, 0.5f);

			yield return new WaitForSeconds(0.5f);

			_attractionRadius.Show();
			_attractionRadius.GetComponent<RectTransform>().DOScale(1.0f, 0.5f);

			yield return new WaitForSeconds(0.5f);

			_multiBullets.Show();
			_multiBullets.GetComponent<RectTransform>().DOScale(1.0f, 0.5f);

			yield return new WaitForSeconds(0.5f);
			
			_descriptionPanel.Show();
			_descriptionPanel.GetComponent<RectTransform>().DOScale(1.0f, 0.5f);
		}
	} 
}
