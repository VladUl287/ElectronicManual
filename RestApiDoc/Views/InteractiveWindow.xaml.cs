using RestApiDoc.Models;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace RestApiDoc.Views
{
    public partial class InteractiveWindow : Window
    {
        private readonly Puzzle puzzle = new();
        private static readonly PuzzlePiece emptyItem = new();
        private readonly ObservableCollection<PuzzlePiece> itemPlacement = new();

        private ListBox? lbDragSource;

        private Canvas? cvDragSource;

        public InteractiveWindow()
        {
            InitializeComponent();

            puzzle.Initialize();
            puzzleItemList.ItemsSource = puzzle.puzzlePiece;

            for (int i = 0; i < 9; i++)
            {
                itemPlacement.Add(emptyItem);

                itemPlacement[i].index = i;
            }
        }

        private void PuzzleItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Canvas cParent)
            {
                cvDragSource = cParent;

                var data = GetDataFromCanvas(cvDragSource);

                if (data is not null)
                {
                    if (data is PuzzlePiece itemSelected)
                    {
                        itemSelected.DragFrom = typeof(Canvas);

                        DragDrop.DoDragDrop(cvDragSource, data, DragDropEffects.Move);
                    }
                }
            }
            else if (sender is ListBox lbParent)
            {
                lbDragSource = lbParent;

                var data = GetObjectDataFromPoint(lbDragSource, e.GetPosition(lbParent));

                if (data is not null)
                {
                    if (data is PuzzlePiece itemSelected)
                    {
                        itemSelected.DragFrom = typeof(ListBox);

                        DragDrop.DoDragDrop(lbDragSource, data, DragDropEffects.Move);
                    }
                }
            }
        }

        private object? GetDataFromCanvas(Canvas cvDragSource)
        {
            if (int.TryParse(cvDragSource.Tag.ToString(), out int canvasIndex))
            {
                return itemPlacement[canvasIndex];
            }

            return null;
        }

        private static object? GetObjectDataFromPoint(ListBox dragSource, Point point)
        {
            if (dragSource.InputHitTest(point) is UIElement element)
            {
                var data = DependencyProperty.UnsetValue;

                while (data == DependencyProperty.UnsetValue)
                {
                    data = dragSource.ItemContainerGenerator.ItemFromContainer(element);

                    if (data == DependencyProperty.UnsetValue)
                    {
                        element = (UIElement)VisualTreeHelper.GetParent(element);
                    }

                    if (element == dragSource)
                    {
                        return null;
                    }
                }

                if (data != DependencyProperty.UnsetValue)
                {
                    return data;
                }
            }

            return null;
        }

        private void PuzzleItemDrop(object sender, DragEventArgs e)
        {
            if (sender is not Canvas destination)
            {
                return;
            }

            if (e.Data?.GetData(typeof(PuzzlePiece)) is not PuzzlePiece puzzlePiece)
            {
                return;
            }

            var imageControl = new Image
            {
                Width = destination.ActualWidth,
                Height = destination.ActualHeight,
                Source = puzzlePiece.Piece,
                Stretch = Stretch.Fill
            };

            if (destination.Children.Count == 0)
            {
                destination.Children.Add(imageControl);

                if (!int.TryParse(destination.Tag.ToString(), out int indexToUpdate))
                {
                    return;
                }

                if (puzzlePiece.DragFrom == typeof(ListBox))
                {
                    itemPlacement[indexToUpdate] = puzzlePiece;

                    if (lbDragSource is not null)
                    {
                        ((IList)lbDragSource.ItemsSource).Remove(puzzlePiece);
                    }
                }
                else if (puzzlePiece.DragFrom == typeof(Canvas))
                {
                    int previousIndex = itemPlacement.IndexOf(puzzlePiece);

                    itemPlacement[indexToUpdate] = puzzlePiece;
                    itemPlacement[previousIndex] = emptyItem;

                    var associated = GetAssociatedCanvasByIndex(previousIndex);

                    associated?.Children.Clear();
                }
            }
            else if (destination.Children.Count > 0)
            {
                if (puzzlePiece.DragFrom == typeof(ListBox))
                {
                    return;
                }
                else if (puzzlePiece.DragFrom == typeof(Canvas))
                {
                    int sourceIndex = itemPlacement.IndexOf(puzzlePiece);

                    if (!int.TryParse(destination.Tag.ToString(), out int destinationIndex))
                    {
                        return;
                    }

                    if (sourceIndex == destinationIndex)
                    {
                        return;
                    }

                    var firstImage = new Image()
                    {
                        Width = destination.ActualWidth,
                        Height = destination.ActualHeight,
                        Stretch = Stretch.Fill
                    };

                    firstImage.Source = itemPlacement[sourceIndex].Piece;

                    var secondImage = new Image()
                    {
                        Width = destination.ActualWidth,
                        Height = destination.ActualHeight,
                        Stretch = Stretch.Fill
                    };

                    secondImage.Source = itemPlacement[destinationIndex].Piece;

                    GetAssociatedCanvasByIndex(sourceIndex)?.Children.Clear();

                    GetAssociatedCanvasByIndex(destinationIndex)?.Children.Clear();

                    GetAssociatedCanvasByIndex(sourceIndex)?.Children.Add(secondImage);

                    GetAssociatedCanvasByIndex(destinationIndex)?.Children.Add(firstImage);

                    var buffer = itemPlacement[sourceIndex];

                    itemPlacement[sourceIndex] = itemPlacement[destinationIndex];

                    itemPlacement[destinationIndex] = buffer;
                }
            }

            puzzlePiece.DragFrom = null;
        }

        private Canvas? GetAssociatedCanvasByIndex(int index)
        {
            return index switch
            {
                0 => puzzlePart1,
                1 => puzzlePart2,
                2 => puzzlePart3,
                3 => puzzlePart4,
                4 => puzzlePart5,
                5 => puzzlePart6,
                6 => puzzlePart7,
                7 => puzzlePart8,
                8 => puzzlePart9,
                _ => null,
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (puzzle.Validate(itemPlacement))
            {
                MessageBox.Show("Вы решили пазл!");
            }
        }
    }
}