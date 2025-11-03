using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabelPrinting
{
    /// <summary>
    /// Represents an item in a combo box, containing a display text and an associated value.
    /// </summary>
    public class ComboBoxItem
    {
        /// <summary>
        /// Represents the value associated with this instance.
        /// </summary>
        public string Value;

        /// <summary>
        /// Represents the text content associated with this instance.
        /// </summary>
        public string Text;


        /// <summary>
        /// Represents an item in a combo box with an associated value and display text.
        /// </summary>
        /// <param name="val">The value associated with the combo box item. This value is typically used for data binding or internal
        /// logic.</param>
        /// <param name="text">The text displayed in the combo box for this item. This is what the user sees in the UI.</param>
        public ComboBoxItem(string val, string text)
        {
            Value = val;
            Text = text;
        }

        /// <summary>
        /// Returns a string representation of the current object.
        /// </summary>
        /// <returns>The text content of the object.</returns>
        public override string ToString()
        {
            return Text;
        }
    }
}
