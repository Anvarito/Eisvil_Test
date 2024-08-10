    using UnityEngine.Events;

    public interface IDamageReceiver
    {
        public UnityAction<int> OnApplyDamage { get; set; }
        public void ApplyDamage(int damageValue);
    }
