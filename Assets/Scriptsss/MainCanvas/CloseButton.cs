using UnityEngine;
using UnityEngine.UI;
namespace QuachDai.NinjaSchool.MainCanvas
{
    public class CloseButton : MonoBehaviour
    {
        Button button;
        [SerializeField]
        Button ThisButton
        {
            get
            {
                if (button == null)
                    button = GetComponent<Button>();
                return button;
            }
        }
        private void OnEnable()
        {
            ThisButton.onClick.AddListener(ListenerMethod);
        }

        private void OnDisable()
        {
            ThisButton.onClick.RemoveListener(ListenerMethod);
        }
        public void ListenerMethod()
        {
            GameManager.Instance.IsPlayGame = true;
        }
    }
}
