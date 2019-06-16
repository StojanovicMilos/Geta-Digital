using System;

namespace Task3.BL
{
    public class DropdownListItem
    {

        public DropdownListItem(string value) : this(value, value)
        {
        }

        public DropdownListItem(string text, string value)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string Text { get; }
        public string Value { get; }
    }
}