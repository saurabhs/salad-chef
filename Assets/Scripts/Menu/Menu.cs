using UnityEngine;
using UnityEngine.UI;

namespace SaladChef.Menu
{
    public class Menu : MonoBehaviour
    {
        /// <summary>
        /// play button to laucch the game
        /// </summary>
        [SerializeField] private Button _play;

        /// <summary>
        /// controls button that will 
        //// popup the control help text
        /// </summary>
        [SerializeField] private Button _controls;

        /// <summary>
        /// quit button to quit
        /// </summary>
        [SerializeField] private Button _quit;

        /// <summary>
        /// quit button to quit
        /// </summary>
        [SerializeField] private Button _controlsClose;

        /// <summary>
        /// quit button to quit
        /// </summary>
        [SerializeField] private GameObject _controlsPopup;

        private void OnEnable()
        {
            _play.onClick.AddListener(() => UnityEngine.SceneManagement.SceneManager.LoadScene("Main"));
            _quit.onClick.AddListener(() => Application.Quit() );
            _controls.onClick.AddListener(() => _controlsPopup.SetActive(true));
            _controlsClose.onClick.AddListener(() => {
                print("CLose");
                _controlsPopup.SetActive(false);
            });
        }

        private void OnDisable()
        {
            _play.onClick.RemoveAllListeners();
            _quit.onClick.RemoveAllListeners();
            _controls.onClick.RemoveAllListeners();
            _controlsClose.onClick.RemoveAllListeners();
        }
    }
}