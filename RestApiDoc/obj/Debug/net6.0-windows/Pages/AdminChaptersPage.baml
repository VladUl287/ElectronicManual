   M S B A M L   `   `   `  ����E  ARestApiDoc, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:RestApiDoc.ControlsRestApiDoc.Controls  5      =#clr-namespace:RestApiDoc.ViewModelsRestApiDoc.ViewModels  5      (    "RestApiDoc.Pages.AdminChaptersPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [MaterialDesignThemes.Wpf, Version=4.5.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a:md1http://materialdesigninxaml.net/winfx/xaml/themes  5      0controls!clr-namespace:RestApiDoc.Controls  5      4
viewmodels#clr-namespace:RestApiDoc.ViewModels  5        N� Title$  AdminChaptersPage��5       � 	Resources 5       �� 
TargetType �� Property �� Value   MaterialDesignTextBox �� BasedOn.  (MaterialDesignThemes.Wpf.TextFieldAssist  MaterialDesignRaisedButton��%j  '��       '�� K     7   '�� @    '��     7  '�� �    �� 5   
   " ��6   .�5      ��  � �� �#fff6+   5      �� 5   
   " ��6   8   .�5      ��  � �� �#fff6+   �� 5       � �� �#fff6,   �� 5       � ��$ 10q�6'   �� 5       � ��$ 15(�6)   �� 5       �@ UnderlineBrush �#fff6B   5      �� 5   
   " ��6   .�5      ��  � ��$ 5q�6'   �� 5       � ��$ 1q�60   �� 5       � ��
 ���� 6+   �� 5       � �� �#fff6,   �� 5       � �� �#fff6+   5      �� 5   
   " ��6   8   .�5       ��  � ��$ 0 0 10 0q�6'   �� 5!       � ��$ 0q�60   �� 5"       � �� �#00b4d86+   5#      �� 5$   
   " ��6   .�5%      ��  � �� �#fff6+   �� 5&       � �� �#fff6,   5'      5(      .��5)      � -   5         �Name$ StackPanelMain��5)      $��2��6#   
������  � RowDefinitions 5*   
   �� 5+      $
��Auto �6   6,   �� 5,      6   .+�5.   
   �� $��
Главы��6   $��0��6"   .��5/      �  50      �� 51      �� 52       � ColumnDefinitions 54      �� 55      $
��Auto �6'   $	��250��64   �� 56      �� 57      $
��Auto �6'   .+�5:      �� -   5        	 ��Name$	 chaptersList��5:      $��0��5;   *   $��2��6:   $	��150��6K   $	��350��6[   ��5:   0   �� *
Chapters+
 �� SelectedItem
 6Q   �� *SelectedChapter+��5<      �� 5=      .R�5>      �� ���#fff$
��Wrap=�6P   ��6(   �� *Name+6P   5?      5A      �� 5C      $��1��6   $��Center=�6-   .+�5D      �� -   5         ��Name$ chapterName��5D      5E      �� 5F      $��Добавить��6   $��2��5G   )   ��5H   )   �� *AddChapterCommand+��5I   )   ��  �� ElementName$ chapterName�� �� Path$
 Text��� 5J      $��1��6   $��1��6-   $��Center=�6:   .+�5K      �� ��6   �� *SelectedChapter.Name+5L      �� 5M      $��2��6   $��1��6-    �� Orientation$ 
Horizontal=�6:   .+�5N      �� $��Обновить��6   ��60   �� *EditChapterCommand+�� 5O      $��Удалить��6   ��6/   �� *DeleteChapterCommand+5P      5Q      5R      �� 5S   
   $��Разделы��6   $��1��6$   .��5T      �  5U      �� 5V      $
��Auto �6$   64   �� 5W      6&   �� 5X      $
��Auto �6$   64    5Z      �� 5[      $
��Auto �6'   $	��250��64   $	��350��6C   6U   �� 5\      6)   .+�5_      �� $��3��6   .+�5`      �� $ 
Horizontal=�6!   $��Right=�6:   $��5q�6V   .+�5a      �� $��5q�6!   $	��5 0q�5b   1   ��5c   1   0  *MaterialDesignThemes.Wpf.PackIconExtension  *Plus+��5d   1   �� *AddPartitionCommand+�� 5e      $��5q�6!   $��0q�5f   1   ��5g   1     *Minus+��5h   1   �� *DeletePartitionCommand+5i      �� 5j      -   5        $	 partitionsList��5j      $��3��5m   .   ��5k   .   �� *SelectedChapter.Partitions+
 5l   .   �� *SelectedPartition+��5n      �� 5o      .R�5p   "   �� ���#fff$
��Wrap=�6T   ��6,   �� *Name+6T   5q      5s      5t      �� 5v      $��0��6O   $��1��6\   ��6   �� *SelectedPartition.Name+ �� Mode$ TwoWay=�6\   +   %RestApiDoc.Controls.RichTextBoxEditor  5w      -   5          Name$ partitionText��5w   -   $��1��6D   $��1��6Q   �� 5x      $��2��6   $��1��5y   %   $ 
Horizontal=�5z   %   $��Right=�5{   %   .+�5|      �� -   5        $��Обновить��5|      $��5q�5}   )   5~   )   5      5�      5�      5�      5�      