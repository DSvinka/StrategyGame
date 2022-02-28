using System;
using Abstractions;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UserControlSystem.Models;
using Zenject;

namespace UserControlSystem.Presenters
{
    public sealed class BottomLeftPresenter: MonoBehaviour
    {
        #region Serialize Fields

        [Header("Selected Information")]
        [SerializeField] private Image _iconImage;
        [SerializeField] private TMP_Text _nameText;
        
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private Slider _healthSlider;
        
        [Inject] private IObservable<ISelectable> _selectedValue;

        #endregion

        [Inject]
        private void Init()
        {
            _selectedValue.Subscribe(OnSelected);
        }

        private void OnSelected(ISelectable selected)
        {
            var isActive = selected != null;
            
            _iconImage.enabled = isActive;
            _nameText.enabled = isActive;
            _healthText.enabled = isActive;
            _healthSlider.gameObject.SetActive(isActive);

            if (selected != null)
            {
                _iconImage.sprite = selected.Icon;
                _nameText.text = selected.Name;
                _healthText.text = $"{selected.Health}/{selected.MaxHealth}";
                _healthSlider.value = selected.Health / selected.MaxHealth;
            }
        }
    }
}