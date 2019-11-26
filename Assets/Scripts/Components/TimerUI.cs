using UnityEngine;
using UnityEngine.UI;

namespace SaladChef.Core
{
    [RequireComponent(typeof(TimerUI))]
    public class TimerUI : MonoBehaviour
    {
        ///<summary>///
        /// progress bar image for timer
        ///</summary>///
        [SerializeField] private Image _image = null;

        /// <summary>
        /// ref to timer component for updating countdown UI
        /// </summary>
        Timer _timer = null;

        private void OnEnable()
        {
            _image.color = Color.green;
            var orderValidator = GetComponent<OrderValidator>();
            if(orderValidator == null)
                throw new System.Exception("Cannot find OrderValidator...");
            orderValidator.onWrongOrderReceived.AddListener((gameObject) => { _image.color = Color.red; });

            _timer = GetComponent<Timer>();

            InvokeRepeating("UpdateCountdownUI", 0, Time.deltaTime);
        }

        private void OnDisable()
        {
            GetComponent<OrderValidator>().onWrongOrderReceived.RemoveAllListeners();
        }

        private void UpdateCountdownUI()
        {
            _image.fillAmount = _timer.TimeLeft / _timer.WaitingTIme;
        }
    }
}