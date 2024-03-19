using ICSharpCode.AvalonEdit.Rendering;
using ICSharpCode.AvalonEdit.Document;
using System;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;
using System.Windows.Media;
using System.Windows;

namespace wpfCopilator
{
    using ICSharpCode.AvalonEdit.Document;
    using ICSharpCode.AvalonEdit.Rendering;
    using System.Reflection.Metadata;
    using System.Windows;
    using System.Windows.Documents;
    using System.Windows.Media;

    public class UnderlineColorizer : DocumentColorizingTransformer
    {
        public int StartOffset { get; set; }
        public int EndOffset { get; set; }

        protected override void ColorizeLine(DocumentLine line)
        {
            if (line.Length == 0 || line.Offset > EndOffset || line.EndOffset < StartOffset)
                return;

            base.ChangeLinePart(
                Math.Max(line.Offset, StartOffset), // начало подчеркивания
                Math.Min(line.EndOffset, EndOffset), // конец подчеркивания
                (VisualLineElement element) =>
                {
                    element.TextRunProperties.SetTextDecorations(TextDecorations.Strikethrough);
                    element.TextRunProperties.SetForegroundBrush(Brushes.Red);


                });
        }
    }
}
