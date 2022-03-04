using System;
using Abstractions;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UserControlSystem.Views
{
    public sealed class BottomCenterView: MonoBehaviour
    {
        public IObservable<int> CancelButtonClick => _cancelButtonClick;

        [SerializeField] private Slider _productionProgressSlider;
        [SerializeField] private TMP_Text _currentUnitNameText;

        [SerializeField] private Image[] _images;
        [SerializeField] private GameObject[] _imageHolders;
        [SerializeField] private Button[] _buttons;

        private Subject<int> _cancelButtonClick = new Subject<int>();
        private IDisposable _unitProductionTask;

        [Inject]
        private void Init()
        {
            for (var i = 0; i < _buttons.Length; i++)
            {
                var index = i;
                _buttons[i].onClick.AddListener(() => _cancelButtonClick.OnNext(index));
            }
        }

        public void Clear()
        {
            for (var i = 0; i < _images.Length; i++)
            {
                _images[i].sprite = null;
                _imageHolders[i].SetActive(false);
            }
            
            _productionProgressSlider.gameObject.SetActive(false);
            _currentUnitNameText.text = string.Empty;
            _currentUnitNameText.enabled = false;
            _unitProductionTask?.Dispose();
        }

        public void SetTask(IUnitProductionTask task, int index)
        {
            if (task == null)
            {
                _imageHolders[index].SetActive(false);
                _images[index].sprite = null;

                if (index == 0)
                {
                    _productionProgressSlider.gameObject.SetActive(false);
                    _currentUnitNameText.text = string.Empty;
                    _currentUnitNameText.enabled = false;
                    _unitProductionTask?.Dispose();
                }
            }
            else
            {
                _imageHolders[index].SetActive(true);
                _images[index].sprite = task.Icon;

                if (index == 0)
                {
                    _productionProgressSlider.gameObject.SetActive(true);
                    _currentUnitNameText.text = task.UnitName;
                    _currentUnitNameText.enabled = true;
                    _unitProductionTask = Observable.EveryUpdate().Subscribe(_ =>
                        _productionProgressSlider.value = task.LeftTime / task.ProductionTime);
                }
            }
        }
    }
}