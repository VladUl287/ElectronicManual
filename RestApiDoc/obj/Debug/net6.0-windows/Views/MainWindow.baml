   M S B A M L   `   `   `  ����E  ARestApiDoc, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:RestApiDoc.ViewModelsRestApiDoc.ViewModels  5   	       RestApiDoc.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   Bd2http://schemas.microsoft.com/expression/blend/2008      5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :
viewmodels#clr-namespace:RestApiDoc.ViewModels     5   	   _ [MaterialDesignThemes.Wpf, Version=4.5.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a:md1http://materialdesigninxaml.net/winfx/xaml/themes  6@   Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  5   	   -   5          � Title$#  Разработка REST Api��5
   	   $	��600��6%   $
��1000��62   ���#2b373d � WindowStartupLocation$ CenterScreen=�6T    � 	Resources 5       �� Source�� 5   
   $6 0/RestApiDoc;component/Styles/MainDictionary.xaml?�6   %    .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5      6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   ]� $��0��6   $��2��6!   .+�5      �� -   5        ��5   4   0  *MaterialDesignThemes.Wpf.PackIconExtension  *Menu+   Size 30�� 5      -   5        ��5   +     *Book+ 25�� 5      -   5        ��5   2     *Puzzle+ 25�� 5      -   5        ��5   2     *User+ 30�� 5       �� Orientation$ 
Horizontal=�6   $��Right=�62   .+�5      �� -   5        $��5q�5      $
��3 10q�6%   $��28��63   ���#00b4d8��6?     *Minus+6r   �� 5      -   5        $��5q�5      $
��3 10q�6%   $��28��63   ���#00b4d8��6?     *Plus+6q   5       5!      � 5#   
   -   5         �Name$ DataLoading��5#      $��1��6$   $��2��61   	 K� ZIndex$	 1��6E   .+�5$      "  FontAwesome.WPF.ImageAwesome  5%      ]� 5&   
   $��1��6   $��2��6!   .+�5'      �� -	   5        
 ��Name$
 SideBarMenu��5'      $
��Left=�60   $��Stretch=�5(      $��Disabled=�5)      $
��Auto=�5*      .��5+      �� ��6   �� *
Chapters+ �� SelectedItem 6<   �� *SelectedChapter+��5,      �� 5-       �� 
TargetType" ��6    .�5.      ��  �� Property � �� �� Value$ 0q�68   5/      ��51      �� 52      .R�53      9� ��54   "   �� 55   &   ��60   �� *Name+.��57   "   �� .+�58   &   �� ���#fff$��Disabled=�5<   +   ��59   +   �� *
Partitions+ 5:   +   ��  �� Path$# DataContext.SelectedPartition� �� Mode$ TwoWay=� �� RelativeSource ��  �� AncestorType" ����5=   *   �� 5>   .   .R�5?   2   ��  �� Padding$ 15 5 0 5q�6R   ��6<   �� *Name+6R   5@   /   5B   '   �� 5C   &   ���#fff$��Disabled=�5F   +   ��5D   +   �� *Tests+ 5E   +   �� $ DataContext.SelectedTest� �� " ����5G   *   �� 5H   .   .R�5I   2   �� $ 15 5 0 5q�6R   ��6<   �� *Name+6R   5J   /   5L   '   5M   #   5N      5O      5Q      5R      � 5S      ���#ced4da.+�5T      �� -
   5         ��Name$ PartitionTextRtb��5T      $��10q�68   5U      5V      5W      5X      