using isj23.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace isj23.UIScreen.UIWidgets {
    public class CountdownWidget : Singleton<CountdownWidget>, IWidget {
        public int maxTime = 60;
        private int currentTime = 60;
        Countdown countdown;
        Coroutine coroutineCD;
        public UnityEvent<float> OnCountDownStops = new();

        public TextMeshProUGUI counter;

        public int CurrentTime { get => currentTime; set => currentTime = value; }

 
        public void StartCountdown() {
            CurrentTime = maxTime;
            countdown = new Countdown(maxTime);
            countdown.OnTimeOut = (TimeOut);
            countdown.OnTimeUpdate = (TimeUpdate);
            if (coroutineCD != null) {
                StopCoroutine(coroutineCD);
            }
            coroutineCD = StartCoroutine(countdown.StartCountdown());
        }

        /// <summary>
        /// Update the UI element with the time left
        /// </summary>
        /// <param name="time">Time Left</param>
        public void TimeUpdate(float time) {
            CurrentTime = (int)time;
            counter.text = (CurrentTime).ToString();
        }

        public float GetTimePlayed() {
            return maxTime - CurrentTime;
        }

        /// <summary>
        /// Send OnCountDownStops event and winGame
        /// </summary>
        public void TimeOut() {
            OnCountDownStops?.Invoke(countdown.GetRemainingTime());
            GameManager.instance.WinGame();
        }

        /// <summary>
        /// Reactive the countdown if it was running
        /// </summary>
        public void RestartCountdown() {
            if (countdown != null) {
                coroutineCD = StartCoroutine(countdown.StartCountdown());
            }
        }

        /// <summary>
        /// Stops the countdown and sends OnCountDownStops event
        /// </summary>
        public void StopCounting() {
            if (countdown != null) {
                if (countdown.IsCountingDown()) {
                    countdown.StopCountdown();
                }
                OnCountDownStops?.Invoke(countdown.GetRemainingTime());

            }
        }
        #region IWidget
        public void Init() {
            StartCountdown();
        }

        public void Show() {
            this.RestartCountdown();
        }

        public void Hide() {
            this.StopCounting();
        } 
        #endregion
    } 
}
