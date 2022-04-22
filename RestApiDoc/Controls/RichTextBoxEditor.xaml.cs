using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace RestApiDoc.Controls
{
    public partial class RichTextBoxEditor : UserControl
    {
        public string Rtf { get; private set; }

        public RichTextBoxEditor()
        {
            InitializeComponent();
            cmbFontFamily.ItemsSource = Fonts.SystemFontFamilies;
            cmbFontSize.ItemsSource = new[] { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
        }

        private void RtbEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            object temp = rtbEditor.Selection.GetPropertyValue(TextElement.FontWeightProperty);
            btnBold.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold));
            temp = rtbEditor.Selection.GetPropertyValue(TextElement.FontStyleProperty);
            btnItalic.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic));
            temp = rtbEditor.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            btnUnderline.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline));

            temp = rtbEditor.Selection.GetPropertyValue(TextElement.FontFamilyProperty);
            cmbFontFamily.SelectedItem = temp;
            temp = rtbEditor.Selection.GetPropertyValue(TextElement.FontSizeProperty);
            cmbFontSize.Text = temp.ToString();
        }

        private void CmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFontFamily.SelectedItem != null)
            {
                rtbEditor.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, cmbFontFamily.SelectedItem);
            }
        }

        private void CmbFontSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                rtbEditor.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, cmbFontSize.Text);
            }
            catch (ArgumentException)
            {

            }
        }

        private void RtbEditor_TextChanged(object sender, TextChangedEventArgs e)
        {
            string rtfFromRtb = string.Empty;
            using (var ms = new MemoryStream())
            {
                var range2 = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                range2.Save(ms, DataFormats.Rtf);
                ms.Seek(0, SeekOrigin.Begin);
                using var sr = new StreamReader(ms);
                rtfFromRtb = sr.ReadToEnd();
            }
            Rtf = rtfFromRtb;
        }

        public void SetRtf(string text)
        {
            var stream = new MemoryStream(Encoding.Default.GetBytes(text));
            rtbEditor.Selection.Load(stream, DataFormats.Rtf);
        }
    }
}