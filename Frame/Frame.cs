namespace Frame
{
    public class Frame
    {
        private readonly string Name;
        private readonly Frame Parent;
        private readonly Dictionary<string, object> Slots;
        public Frame(string name, Frame parent = null, Dictionary<string, object> slots = null)
        {
            Slots = slots ?? new();
            Name = name;
            Parent = parent;
        }
        public object GetValue(string slotName)// - получить значение слота(с учетом наследования), 
        {
            if (String.IsNullOrEmpty(slotName)) throw new ArgumentNullException("Invalid Slot Name on " + nameof(slotName));
            if (Slots.TryGetValue(slotName, out object? value))
            {
                return value;
            }
            else if (Parent != null)
            {
                return Parent.GetValue(slotName);
            }
            else
            {
                throw new KeyNotFoundException("Slot " + slotName + " not found in Frame " + Name);
            }
        }
        public Frame GetParent() //- получить родителя,
        {
            return Parent;
        }
        public string GetName() //- получить имя,
        {
            return Name;
        }
        public Dictionary<string, object> GetSlots() //- получить слот (без учета наследования),
        {
            var copy = new Dictionary<string, object>(Slots);
            return copy;
        }
        public void SetValue(string slotName, object value)// - установить значение,
        {
            if (String.IsNullOrEmpty(slotName)) throw new ArgumentNullException("Invalid Slot Name on " + nameof(slotName));
            if (!Slots.ContainsKey(slotName))
            {
                AddSlot(slotName, value);
                return;
            }
            Slots[slotName] = value;
        }
        public void AddSlot(string slotName, object defaultValue = null) //- добавить слот с дефолтом.
        {
            if (String.IsNullOrEmpty(slotName)) throw new ArgumentNullException("Invalid Slot Name on " + nameof(slotName));
            Slots.Add(slotName, defaultValue);
        }
    }
}
