using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace RestApiDoc.Controls
{
    public partial class UserControlPartitionText : UserControl
    {
        public UserControlPartitionText(string text)
        {
            InitializeComponent();
            var stream = new MemoryStream(Encoding.Default.GetBytes(text));
            PartitionText.Selection.Load(stream, DataFormats.Rtf);
        }
    }
}