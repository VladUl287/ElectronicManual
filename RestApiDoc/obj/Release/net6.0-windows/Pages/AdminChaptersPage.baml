   M S B A M L   `   `   `  ���� E  ARestApiDoc, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:RestApiDoc.ControlsRestApiDoc.Controls  =#clr-namespace:RestApiDoc.ViewModelsRestApiDoc.ViewModels  (    "RestApiDoc.Pages.AdminChaptersPage   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     _ [MaterialDesignThemes.Wpf, Version=4.5.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a:md1http://materialdesigninxaml.net/winfx/xaml/themes  0controls!clr-namespace:RestApiDoc.Controls  4
viewmodels#clr-namespace:RestApiDoc.ViewModels    N� Title$  AdminChaptersPage�� � 	Resources  �� 
TargetType �� Property �� Value   MaterialDesignTextBox �� BasedOn.  (MaterialDesignThemes.Wpf.TextFieldAssist  MaterialDesignRaisedButton��%  '��       '�� &     7   '�� �     '�� ?    7  '�� �    �� " ��.���  � �� �#fff�� " ��8   .���  � �� �#fff��  � �� �#fff��  � ��$ 10q���  � ��$ 15(���  �@ UnderlineBrush �#fff�� " ��.���  � ��$ 5q���  � ��$ 1q���  � ��
 ���� ��  � �� �#fff��  � �� �#fff�� " ��8   .���  � ��$ 0 0 10 0q���  � ��$ 0q���  � �� �#00b4d8�� " ��.���  � �� �#fff��  � �� �#fff.��� -    �Name$ StackPanelMain��$��2��
������  � RowDefinitions �� $
��Auto ��� .+��� $��
Главы��$��0��.���  �� ��  � ColumnDefinitions �� $
��Auto �$	��250���� �� $
��Auto �.+��� -   	 ��Name$	 chaptersList��$��0��$��2��$	��150��$	��350������ *
Chapters+
 �� SelectedItem
 �� *SelectedChapter+���� .R��� ���#fff$
��Wrap=����� *Name+�� $��1��$��Center=�.+��� -    ��Name$ chapterName���� $��Добавить��$��2������ *AddChapterCommand+����  �� ElementName$ chapterName�� �� Path$
 Text��� $��1��$��1��$��Center=�.+��� ���� *SelectedChapter.Name+�� $��2��$��1�� �� Orientation$ 
Horizontal=�.+��� $��Обновить������ *EditChapterCommand+�� $��Удалить������ *DeleteChapterCommand+�� $��Разделы��$��1��.���  �� $
��Auto ��� �� $
��Auto � �� $
��Auto �$	��250��$	��350���� .+��� $��3��.+��� $ 
Horizontal=�$��Right=�$��5q�.+��� $��5q�$	��5 0q���0  *MaterialDesignThemes.Wpf.PackIconExtension  *Plus+���� *AddPartitionCommand+�� $��5q�$��0q���  *Minus+���� *DeletePartitionCommand+�� -   $	 partitionsList��$��3������ *SelectedChapter.Partitions+
 �� *SelectedPartition+���� .R��� ���#fff$
��Wrap=����� *Name+�� $��0��$��1������ *SelectedPartition.Name+ �� Mode$ TwoWay=�+   %RestApiDoc.Controls.RichTextBoxEditor  -     Name$ partitionText��$��1��$��1���� $��2��$��1��$ 
Horizontal=�$��Right=�.+��� -   $��Обновить��$��5q�