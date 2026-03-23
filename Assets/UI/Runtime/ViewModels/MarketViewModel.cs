using UnityEngine;
using UnityEngine.UIElements;

namespace IronArteries.UI.Runtime.ViewModels
{
    /// <summary>
    /// ViewModel using Unity 6 Data Binding system (MVVM pattern) for Market Allocation
    /// </summary>
    [UxmlElement]
    public partial class MarketViewModel : BindableElement
    {
        private float _allocationPercent = 50f;
        
        [UxmlAttribute]
        public float AllocationPercent
        {
            get => _allocationPercent;
            set
            {
                if (Mathf.Approximately(_allocationPercent, value))
                    return;
                    
                _allocationPercent = Mathf.Clamp(value, 0f, 100f);
                
                // Notify UI about changes
                NotifyPropertyChanged(nameof(AllocationPercent));
                UpdateVisuals();
            }
        }
        
        public MarketViewModel()
        {
            // Register callback to capture slider changes from UI
            RegisterCallback<ChangeEvent<float>>(evt => 
            {
                if (evt.target is Slider)
                {
                    AllocationPercent = evt.newValue;
                }
            });
        }
        
        private void UpdateVisuals()
        {
            var slider = this.Q<Slider>();
            if (slider != null && !Mathf.Approximately(slider.value, _allocationPercent))
            {
                slider.SetValueWithoutNotify(_allocationPercent);
            }
        }
    }
}
