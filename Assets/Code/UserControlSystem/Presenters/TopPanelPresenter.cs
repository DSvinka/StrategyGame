using System;
using Abstractions;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UserControlSystem.Presenters
{
    public sealed class TopPanelPresenter: MonoBehaviour
    {
        [SerializeField] private TMP_Text _timeText;
        [SerializeField] private Button _menuButton;
        [SerializeField] private GameObject _menuGameObject;

        [Inject]
        private void Init(ITimeModel timeModel)
        {
            timeModel.GameTime.Subscribe(seconds =>
            {
                var timeSpan = TimeSpan.FromSeconds(seconds);
                _timeText.text = $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
            });

            _menuButton.OnClickAsObservable().Subscribe(_ => _menuGameObject.SetActive(true));
        }
    }
}