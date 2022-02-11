using Abstractions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UserControlSystem.Models;

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

        [Header("Other")] 
        [SerializeField] private SelectableValue _selectedValue;

        #endregion

        private void Start()
        {
            _selectedValue.OnSelected += OnSelected;
            OnSelected(_selectedValue.CurrentValue);
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