using System;
using System.Windows.Media;

namespace RestApiDoc.Models
{
    public class PuzzlePiece
    {
        public int index = -1;

        public ImageSource Piece { get; set; }

        public Type DragFrom { get; set; }
    }
}