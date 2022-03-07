using System.Text;
using Abstractions;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace UserControlSystem.Presenters
{
    public sealed class GameOverScreenPresenter: MonoBehaviour
    {
        [Inject] private IGameStatus _gameStatus;

        [SerializeField] private TMP_Text _text;
        [SerializeField] private GameObject _view;

        [Inject]
        private void Init()
        {
            _gameStatus.Status.ObserveOnMainThread().Subscribe(result =>
            {
                if (result == 0)
                {
                    _text.text = "Ничья!";
                }
                else
                {
                    _text.text = $"Победила фракция №{result}";
                }
                _view.SetActive(true);
                Time.timeScale = 0;
            });
        }

    }
}