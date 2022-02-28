using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UserControlSystem.Presenters
{
    public sealed class MenuPresenter: MonoBehaviour
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _exitButton;

        private void Start()
        {
            _backButton.OnClickAsObservable().Subscribe(_ => gameObject.SetActive(false));
            _exitButton.OnClickAsObservable().Subscribe(_ => Application.Quit());
        }
    }
}