using UnityEngine;

namespace LD54
{
    public class DescriptionPanel : MonoBehaviour
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
