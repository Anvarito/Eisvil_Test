using System;

namespace Infrastructure.Extras
{
    public class ReactiveVariable<T>
    {
        private T _value;
        public event Action<T> OnValueChanged;

        public ReactiveVariable(T initialValue = default)
        {
            _value = initialValue;
        }
        public T Value
        {
            get => _value;
            set
            {
                if (!Equals(_value, value))
                {
                    _value = value;
                    OnValueChanged?.Invoke(_value);
                }
            }
        }
    }
}